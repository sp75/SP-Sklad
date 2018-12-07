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
            }

            if (pl != null)
            {
                PriceListBS.DataSource = pl;
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
            var sql = @"
	select mats.matid as Id, -1*mats.grpid as Pid, mats.Name, mats.Artikul as art, msr.shortname as MsrName, 0 as ImgIndex,
    0.00 Price
    from  materials mats, measures msr
    where  msr.mid=mats.mid and mats.deleted=0

    union all
    select  -1*mg.grpid as id, -1*mg.pid as pid,  mg.name ,'', '', 2 , 0
    from matgroup mg
    where mg.deleted=0";

            int? p_type = PTypeEdit.EditValue == null || PTypeEdit.EditValue == DBNull.Value ? null : (int?)PTypeEdit.EditValue;
            var list = _db.GetMatTree(p_type, 2).ToList();
    //        var list = _db.Database.SqlQuery<GetMatTree_Result>(sql).ToList();
            MatTreeList.BeginUpdate();
            MatTreeList.DataSource = list;
            MatTreeList.EndUpdate();
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
            if (!_db.PriceListDet.Where(w => w.MatId == row.Id && w.PlDetType == 0 && w.PlId == _pl_id.Value).Any())
            {
                _db.PriceListDet.Add(new PriceListDet
                {
                    PlId = _pl_id.Value,
                    MatId = row.Id,
                    Price = GetPrice(row.Id.Value),
                    GrpId = row.Pid * -1,
                    PlDetType = 0,
                    Discount = 0,
                    Num = pl.PriceListDet.Count()
                });
            }
            else
            {
                //gridView1.set
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
                    PlDetType = 1,
                    Num = pl.PriceListDet.Count()
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
            int top_row = PriceListGrid.TopRowIndex;
            PriceListDetBS.DataSource = _db.GetPriceListDet(_pl_id);
            PriceListGrid.ExpandAllGroups();
            PriceListGrid.TopRowIndex = top_row;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var dr = PriceListGrid.GetRow(e.RowHandle) as GetPriceListDet_Result;
            var pld = _db.PriceListDet.Find(dr.PlDetId);
            if (e.Column.FieldName == "Price")
            {
                pld.Price = Convert.ToDecimal(e.Value);
            }

            if (e.Column.FieldName == "Discount")
            {
                pld.Discount = Convert.ToDecimal(e.Value);
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = PriceListGrid.GetFocusedRow() as GetPriceListDet_Result;

            if (PriceListGrid.IsGroupRow(PriceListGrid.FocusedRowHandle))
            {
                _db.DeleteWhere<PriceListDet>(w => w.GrpId == dr.GrpId);
            }
            else
            {
                _db.DeleteWhere<PriceListDet>(w => w.PlDetId == dr.PlDetId);
            }

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

            return mat_price != null ? (mat_price.Price != null ? mat_price.Price.Value : 0.00m) : 0.00m;
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
            var dr = PriceListGrid.GetFocusedRow() as GetPriceListDet_Result;
            IHelper.ShowMatInfo(dr.MatId);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && AddMaterialBtn.Enabled && !String.IsNullOrEmpty(BarCodeEdit.Text))
            {
                var BarCodeText = BarCodeEdit.Text.Split('+');
                string kod = BarCodeText[0];
                var item = _db.Materials.Where(w => w.BarCode == kod).Select(s => s.MatId).FirstOrDefault();

                if (item > 0)
                {
                    BarCodeEdit.BackColor = Color.PaleGreen;
                    MatTreeList.FocusedNode = MatTreeList.FindNodeByFieldValue("Id", item);
                    barButtonItem5.PerformClick();
                }
                else
                {
                    BarCodeEdit.BackColor = Color.Pink;
                }

                BarCodeEdit.Text = "";
            }
        }

        private void BarCodeEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !String.IsNullOrEmpty(BarCodeEdit1.Text))
            {
                var BarCodeText = BarCodeEdit1.Text.Split('+');
                string kod = BarCodeText[0];

                int rowHandle = PriceListGrid.LocateByValue("BarCode", kod);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    PriceListGrid.FocusedRowHandle = rowHandle;
                    PriceListGrid.FocusedColumn = colPrice;
                    PriceListGrid.ShowEditor();

                    BarCodeEdit1.BackColor = Color.PaleGreen;
                }
                else
                {
                    BarCodeEdit1.BackColor = Color.Pink;
                }

                BarCodeEdit1.Text = "";
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var f = _db.PriceListDet.Where(w => w.PlId == _pl_id && w.PlDetType == 0).ToList();
            foreach (var item in f)
            {
                item.Price = GetPrice(item.MatId.Value);
            }

            _db.SaveChanges();
            GetDetail();
        }

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           var mat_id = IHelper.ShowDirectList(null, 5);
           if (mat_id != null)
           {
               var id = Convert.ToInt32(mat_id);
               var mat = _db.Materials.Find(id);
               if (!_db.PriceListDet.Where(w => w.MatId == id && w.PlDetType == 0 && w.PlId == _pl_id.Value).Any())
               {
                   _db.PriceListDet.Add(new PriceListDet
                   {
                       PlId = _pl_id.Value,
                       MatId = id,
                       Price = GetPrice(id),
                       GrpId = mat.GrpId,
                       PlDetType = 0
                   });

                   _db.SaveChanges();
                   GetDetail();
               }
              
           }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var mat_id = IHelper.ShowDirectList(null, 5);
            if (mat_id != null)
            {
                var id = Convert.ToInt32(mat_id);
                var mat = _db.Materials.Find(id);
                foreach (var item in _db.Materials.Where(w => w.GrpId == mat.GrpId).ToList())
                {

                    if (!_db.PriceListDet.Where(w => w.MatId == item.MatId && w.PlDetType == 0 && w.PlId == _pl_id).Any())
                    {
                        _db.PriceListDet.Add(new PriceListDet
                        {
                            PlId = _pl_id.Value,
                            MatId = item.MatId,
                            Price = GetPrice(item.MatId),
                            GrpId = item.GrpId,
                            PlDetType = 0
                        });
                    }
                }
                _db.SaveChanges();
                GetDetail();
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var SvcId = IHelper.ShowDirectList(null, 11);
            if (SvcId != null)
            {
                var id = Convert.ToInt32(SvcId);
                var svc = _db.Services.Find(id);
                if (!_db.PriceListDet.Where(w => w.MatId == id && w.PlDetType == 1 && w.PlId == _pl_id.Value).Any())
                {
                    _db.PriceListDet.Add(new PriceListDet
                    {
                        PlId = _pl_id.Value,
                        MatId = id,
                        Price = svc.Price ?? 0,
                        GrpId = svc.GrpId,
                        PlDetType = 1
                    });

                    _db.SaveChanges();
                    GetDetail();
                }

            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var SvcId = IHelper.ShowDirectList(null, 11);
            if (SvcId != null)
            {
                var id = Convert.ToInt32(SvcId);
                var svc = _db.Services.Find(id);
                foreach (var item in _db.Services.Where(w => w.GrpId == svc.GrpId).ToList())
                {

                    if (!_db.PriceListDet.Where(w => w.MatId == item.SvcId && w.PlDetType == 1 && w.PlId == _pl_id.Value).Any())
                    {
                        _db.PriceListDet.Add(new PriceListDet
                        {
                            PlId = _pl_id.Value,
                            MatId = item.SvcId,
                            Price = item.Price ?? 0,
                            GrpId = svc.GrpId,
                            PlDetType = 1
                        });
                    }
                }

                _db.SaveChanges();
                GetDetail();
            }
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; PriceListGrid.RowCount > i; i++)
            {
                var row = PriceListGrid.GetRow(i) as GetPriceListDet_Result;
                if (row != null)
                {
                    var wbd = _db.PriceListDet.Find(row.PlDetId);

                    wbd.Num = i + 1;
                }
            }

            _db.SaveChanges();

            GetDetail();
        }

        private void PriceListGrid_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.PriceListPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = PriceListGrid.GetFocusedRow() as GetPriceListDet_Result;
            var pld = _db.PriceListDet.Find(dr.PlDetId);
            pld.Price = GetPrice(dr.MatId.Value);
            dr.Price = pld.Price;
            _db.SaveChanges();

            PriceListGrid.RefreshRow(PriceListGrid.FocusedRowHandle);
        }

        private void PriceListPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            var dr = PriceListGrid.GetFocusedRow() as GetPriceListDet_Result;

            if (PriceListGrid.IsGroupRow(PriceListGrid.FocusedRowHandle))
            {
                DelMaterialBtn.Caption = "Видалити групу товарів";
            }
            else
            {
                DelMaterialBtn.Caption = "Видалити";
            }
        }
    }
}
