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
using DevExpress.Data;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using SP_Sklad.Common;
using SP_Sklad.Properties;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;

namespace SP_Sklad.WBForm
{
    public partial class frmWaybillTemplate : DevExpress.XtraEditors.XtraForm
    {
        private Guid? _wbt_id { get; set; }
        public BaseEntities _db { get; set; }
        public int? doc_id { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private WaybillTemplate wbt { get; set; }

        private v_WaybillTemplateDet focused_dr => WaybillTemplateDetGrid.GetFocusedRow() as v_WaybillTemplateDet;

        public frmWaybillTemplate(Guid? wbt_id = null)
        {
            InitializeComponent();

            _wbt_id = wbt_id;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction();
        }
          
        private void frmPriceList_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpEdit2.DataSource = new BaseEntities().Warehouse.AsNoTracking().ToList();

            if (_wbt_id == null)
            {
                wbt = _db.WaybillTemplate.Add(new WaybillTemplate
                {
                    Id = Guid.NewGuid(),
                });

                _db.SaveChanges();

                _wbt_id = wbt.Id;
            }
            else
            {
                wbt = _db.WaybillTemplate.Find(_wbt_id);
            }

            if (wbt != null)
            {
                WaybillTemplateBS.DataSource = wbt;
            }

            foreach (var item in _db.Kagent.Where(w => w.Deleted == 0 && (w.Archived == 0 || w.Archived == null)).OrderBy(o => o.Name).Select(s => new
            {
                s.KaId,
                s.Name,
                IsWork = s.WaybillTemplate.Any(a => a.Id == _wbt_id),
                s.PriceList
            }))
            {
                checkedComboBoxEdit1.Properties.Items.Add(item.KaId, item.Name, item.IsWork ? CheckState.Checked : CheckState.Unchecked, true);
            }

            GetDetail();

            MatGridView.ExpandAllGroups();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var wbt = _db.WaybillTemplate.Find(_wbt_id);
            wbt.Kagent.Clear();

            foreach (var item in checkedComboBoxEdit1.Properties.Items.Where(w => w.CheckState == CheckState.Checked))
            {
                var kaid = (int)item.Value;
                wbt.Kagent.Add(_db.Kagent.Find(kaid));
            }

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
 
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        void GetDetail()
        {
            int top_row = WaybillTemplateDetGrid.TopRowIndex;
            WaybillTemplateDetBS.DataSource = _db.v_WaybillTemplateDet.AsNoTracking().Where(w=> w.WaybillTemplateId == _wbt_id).ToList();
            WaybillTemplateDetGrid.ExpandAllGroups();
            WaybillTemplateDetGrid.TopRowIndex = top_row;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.WaybillTemplateDet.Remove(_db.WaybillTemplateDet.FirstOrDefault(w => w.Id == focused_dr.Id));
            _db.SaveChanges();
            GetDetail();

        }


        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintDoc.PrintWaybillTemplate(_wbt_id.Value, _db, "WaybillTemplate.xlsx");
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(focused_dr.MatId);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

             
        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var mat_id = IHelper.ShowDirectList(null, 5);
            if (mat_id != null)
            {
                var id = Convert.ToInt32(mat_id);
                var mat = _db.Materials.Find(id);
                if (!_db.WaybillTemplateDet.Where(w => w.MatId == id && w.WaybillTemplateId == _wbt_id.Value).Any())
                {
                    _db.WaybillTemplateDet.Add(new WaybillTemplateDet
                    {
                        Id = Guid.NewGuid(),
                        MatId = mat.MatId,
                        WaybillTemplateId = _wbt_id.Value
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

                    if (!_db.WaybillTemplateDet.Where(w => w.MatId == item.MatId && w.WaybillTemplateId == _wbt_id.Value).Any())
                    {
                        _db.WaybillTemplateDet.Add(new WaybillTemplateDet
                        {
                            Id = Guid.NewGuid(),
                            MatId = item.MatId,
                            WaybillTemplateId = _wbt_id.Value
                        });
                    }
                }
                _db.SaveChanges();
                GetDetail();
            }
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; WaybillTemplateDetGrid.RowCount > i; i++)
            {
                var row = WaybillTemplateDetGrid.GetRow(i) as v_WaybillTemplateDet;
                if (row != null)
                {
                    var wbd = _db.WaybillTemplateDet.Find(row.Id);

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
                this.TemplateListPopupMenu.ShowPopup(p2);
            }
        }


        private void PriceListPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {

        }


        private void PriceListGrid_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            var wh_row = WaybillTemplateDetGrid.GetRow(e.RowHandle) as GetPriceListDet_Result;

            if (wh_row != null && wh_row.Price < Math.Max(wh_row.LastInPrice ?? 0, wh_row.LastOutPrice ?? 0))
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }

  
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           
        }

        private void MatListSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var mat = _db.v_Materials.Where(w=> w.Archived == 0 &&  w.TypeId == 1).AsQueryable();

            e.QueryableSource = mat;
        }


        private void AddbarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Int32[] selectedRowHandles = MatGridView.GetSelectedRows();
            AddSelectedRow(selectedRowHandles);
            GetDetail();
        }

        private void AddSelectedRow(Int32[] selectedRowHandles)
        {
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    while (MatGridView.GetRow(selectedRowHandle) is NotLoadedObject)
                    {
                        Application.DoEvents();
                    }

                    var mat = MatGridView.GetRow(selectedRowHandle) as v_Materials;
                    if (!_db.WaybillTemplateDet.Where(w => w.MatId == mat.MatId && w.WaybillTemplateId == _wbt_id.Value).Any())
                    {
                        _db.WaybillTemplateDet.Add(new WaybillTemplateDet
                        {
                            Id = Guid.NewGuid(),
                            MatId = mat.MatId,
                            WaybillTemplateId = _wbt_id.Value
                        });
                    }
                }
            }

            MatGridView.ClearSelection();

            _db.SaveChanges();
        }


        private void dragDropEvents2_DragOver(object sender, DragOverEventArgs e)
        {
      //      var args = DragOverGridEventArgs.GetDragOverGridEventArgs(e);
         /*   e.InsertType = args.InsertType;
            e.InsertIndicatorLocation = args.InsertIndicatorLocation;
            e.Action = args.Action;
            Cursor.Current = args.Cursor;
            */
            e.Handled = true;
        }

        private void dragDropEvents2_DragDrop(object sender, DragDropEventArgs e)
        {
            AddSelectedRow((int[])e.Data);
            GetDetail();
        }

        private void MatGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.TreePopupMenu.ShowPopup(p2);
            }
        }

        private void WaybillTemplateDetGrid_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var wbtd = _db.WaybillTemplateDet.Find(focused_dr.Id);

            if (e.Column.FieldName == "Notes")
            {
                wbtd.Notes = Convert.ToString(e.Value);
            }

            if (e.Column.FieldName == "WId")
            {
                wbtd.WId = Convert.ToInt32(e.Value);
            }

            _db.SaveChanges();
        }
    }
}
