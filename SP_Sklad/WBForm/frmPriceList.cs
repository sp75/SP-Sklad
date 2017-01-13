using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using SP_Sklad.Common;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;

namespace SP_Sklad.WBForm
{
    public partial class frmPriceList : DevExpress.XtraEditors.XtraForm
    {
        private int? _pl_id { get; set; }
        public BaseEntities _db { get; set; }
        public int? doc_id { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private PriceList pl { get; set; }

        public frmPriceList(int? pl_id = null)
        {
            InitializeComponent();

            _pl_id = pl_id;

            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction();
        }

        private void frmPriceList_Load(object sender, EventArgs e)
        {
            PTypeEdit.Properties.DataSource = DB.SkladBase().PriceTypes.Select(s => new { s.PTypeId, s.Name }).ToList();

            if (_pl_id == null)
            {
                pl = _db.PriceList.Add(new PriceList
                {
                    Id = Guid.NewGuid(),
                    Deleted = 0,
                    UseLogo = 0,
                    CurrId = 2,
                    OnDate = DateTime.Now
                });

                _db.SaveChanges();

                _pl_id = pl.PlId;
            }
            else
            {
                pl = _db.PriceList.Find(_pl_id);

                if (pl != null)
                {
                    PriceListBS.DataSource = pl;
                }
            }

            GetDetail();
            GetMatTree();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
            current_transaction.Commit();
            Close();
        }

        private void frmPriceList_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void GetMatTree()
        {
            int? p_type = PTypeEdit.EditValue == null || PTypeEdit.EditValue == DBNull.Value ? null : (int?)PTypeEdit.EditValue;
            MatTreeList.DataSource = _db.GetMatTree(p_type, 2).ToList();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetMatTree();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MatTreeList.DataSource = _db.GetSvcTree().ToList();
        }

        private void treeList1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeList treeList = sender as TreeList;
                TreeListHitInfo info = treeList.CalcHitInfo(e.Location);
                if (info.Node != null)
                {
                    treeList.FocusedNode = info.Node;
                }
            }
        }

        private void treeList1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            Point p2 = Control.MousePosition;
            TreePopupMenu.ShowPopup(p2);
        }

        void AddMat(GetMatTree_Result row)
        {
            if (!_db.PriceListDet.Where(w => w.MatId == row.Id && w.PlDetType == 0).Any())
            {
                _db.PriceListDet.Add(new PriceListDet
                {
                    PlId = _pl_id.Value,
                    MatId = row.Id,
                    Price = row.Price ?? 0,
                    GrpId = row.Pid * -1,
                    PlDetType = 0
                });
            }
        }

        void AddSvc(GetSvcTree_Result row)
        {

            if (!_db.PriceListDet.Where(w => w.MatId == row.Id && w.PlDetType == 1).Any())
            {
                _db.PriceListDet.Add(new PriceListDet
                {
                    PlId = _pl_id.Value,
                    MatId = row.Id,
                    Price = row.Price ?? 0,
                    GrpId = row.Pid * -1,
                    PlDetType = 1
                });
            }

        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barButtonItem2.Down)
            {
                var row = MatTreeList.GetDataRecordByNode(MatTreeList.FocusedNode) as GetMatTree_Result;
                if (row.Id < 0)
                {
                    var grp = MatTreeList.DataSource as List<GetMatTree_Result>;
                    foreach (var item in grp.Where(w => w.Pid == row.Id))
                    {
                        AddMat(item);
                    }
                }
                else AddMat(row);
            }

            if (barButtonItem3.Down)
            {
                var row = MatTreeList.GetDataRecordByNode(MatTreeList.FocusedNode) as GetSvcTree_Result;
                if (row.Id < 0)
                {
                    var grp = MatTreeList.DataSource as List<GetSvcTree_Result>;
                    foreach (var item in grp.Where(w => w.Pid == row.Id))
                    {
                        AddSvc(item);
                    }
                }
                else AddSvc(row);
            }

            _db.SaveChanges();
            GetDetail();
        }

        void GetDetail()
        {
            PriceListDetBS.DataSource = _db.GetPriceListDet(_pl_id);  
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var dr = gridView1.GetRow(e.RowHandle) as GetPriceListDet_Result;
            var pld = _db.PriceListDet.Find(dr.PlDetId);
            pld.Price = Convert.ToDecimal(e.Value);
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = gridView1.GetFocusedRow() as GetPriceListDet_Result;
            _db.DeleteWhere<PriceListDet>(w => w.PlDetId == dr.PlDetId);
            GetDetail();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.DeleteWhere<PriceListDet>(w => w.PlId == _pl_id);
            var wh_remain = _db.WhMatGet(0, 0, 0, DateTime.Now, 0, "*", 0, "", DBHelper.CurrentUser.UserId, 0).ToList();

            foreach (var item in wh_remain)
            {
                _db.PriceListDet.Add(new PriceListDet
                {
                    PlId = _pl_id.Value,
                    MatId = item.MatId,
                    Price =  GetPrice(item.MatId),
                    GrpId = item.OutGrpId,
                    PlDetType = 0
                });
            }

            GetDetail();
        }

        private decimal GetPrice(int? mat_id)
        {
            int? p_type = PTypeEdit.EditValue == null || PTypeEdit.EditValue == DBNull.Value ? null : (int?)PTypeEdit.EditValue;
            var mat_price = _db.GetListMatPrices(mat_id, pl.CurrId, p_type).FirstOrDefault();

            return mat_price != null ? mat_price.Price.Value : 0.00m;
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barButtonItem2.Down)
            {
                var rows = MatTreeList.DataSource as List<GetMatTree_Result>;

                foreach (var item in rows.Where(w => w.Id > 0))
                {
                    AddMat(item);
                }

            }

            if (barButtonItem3.Down)
            {
                var rows = MatTreeList.DataSource as List<GetSvcTree_Result>;
                foreach (var item in rows.Where(w => w.Id > 0))
                {
                    AddSvc(item);
                }
            }

            _db.SaveChanges();
            GetDetail();
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();
            PrintDoc.Show(pl.Id, 10, _db);
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = gridView1.GetFocusedRow() as GetPriceListDet_Result;
            IHelper.ShowMatInfo(dr.MatId);
        }
    }
}
