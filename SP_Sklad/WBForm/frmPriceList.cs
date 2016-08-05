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
using SP_Sklad.SkladData;

namespace SP_Sklad.WBForm
{
    public partial class frmPriceList : Form
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
            MatTreeList.DataSource = _db.GetSvcTree();
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

        void AddMat(int mat_id)
        {
            var row = MatTreeList.GetDataRecordByNode(MatTreeList.FocusedNode) as GetMatTree_Result;
            if (!_db.PriceListDet.Where(w => w.MatId == mat_id && w.PlDetType == 0).Any())
            {
                _db.PriceListDet.Add(new PriceListDet { PlId = _pl_id.Value, MatId = mat_id, Price = row.Price ?? 0, GrpId = row.Pid * -1, PlDetType = 0 });
            }

        }

        void AddSvc(int svc_id)
        {
            var row = MatTreeList.GetDataRecordByNode(MatTreeList.FocusedNode) as GetSvcTree_Result;
            if (!_db.PriceListDet.Where(w => w.MatId == svc_id && w.PlDetType == 1).Any())
            {
                _db.PriceListDet.Add(new PriceListDet { PlId = _pl_id.Value, MatId = svc_id, Price = row.Price ?? 0, GrpId = row.Pid * -1, PlDetType = 1 });
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
                        AddMat(item.Id.Value * -1);
                    }
                }
                else AddMat(row.Id.Value);
            }

            if (barButtonItem3.Down)
            {
                var row = MatTreeList.GetDataRecordByNode(MatTreeList.FocusedNode) as GetSvcTree_Result;
                if (row.Id < 0)
                {
                    var grp = MatTreeList.DataSource as List<GetMatTree_Result>;
                    foreach (var item in grp.Where(w => w.Pid == row.Id))
                    {
                        AddSvc(item.Id.Value * -1);
                    }
                }
                else AddSvc(row.Id.Value);
            }

            _db.SaveChanges();
            GetDetail();
        }

        void GetDetail()
        {
            PriceListDetBS.DataSource = _db.GetPriceListDet(_pl_id);  //  _db.PriceListDet.Where(w => w.PlId == _pl_id).ToList();

        }
    }
}
