using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using DevExpress.XtraBars;
using SP_Sklad.Common;
using SP_Sklad.WBForm;
using DevExpress.Data;
using SP_Sklad.ViewsForm;
using DevExpress.XtraGrid;
using SP_Sklad.Properties;
using SP_Sklad.Reports;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;

namespace SP_Sklad.UserControls
{
    public partial class ucExpedition : DevExpress.XtraEditors.XtraUserControl
    {
        private int fun_id = 103;
        private string reg_layout_path = "ucExpedition\\ExpeditionsGridView";

        BaseEntities _db { get; set; }
        public BarButtonItem EditBtn { get; set; }
        public BarButtonItem DeleteBtn { get; set; }
        public BarButtonItem ExecuteBtn { get; set; }
        public BarButtonItem CopyBtn { get; set; }
        public BarButtonItem PrintBtn { get; set; }

        public v_Expedition row_exp => ExpeditionsGridView.GetFocusedRow() is NotLoadedObject ? null : ExpeditionsGridView.GetFocusedRow() as v_Expedition;
        public v_ExpeditionDet row_smp_det => ExpeditionDetGridView.GetFocusedRow() as v_ExpeditionDet;

        private UserAccess user_access { get; set; }

        private Guid prev_focused_id = Guid.Empty;
        private Guid? find_id;
        private int prev_rowHandle = 0;
        private int prev_top_row_index = 0;

        private bool _restore = false;
        public ucExpedition()
        {
            InitializeComponent();
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExpeditionsGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }
        public void GetData()
        {
            prev_rowHandle = ExpeditionsGridView.FocusedRowHandle;

            if (row_exp != null && !find_id.HasValue)
            {
                prev_focused_id = row_exp.Id;
            }

            if(find_id.HasValue)
            {
                prev_focused_id = find_id.Value;
                find_id = null;
            }

            _restore = true;

            ExpeditionsGridControl.DataSource = null;
            ExpeditionsGridControl.DataSource = ExpeditionsSource;

            GetDetailData();
        }

        public void NewItem()
        {
            using (var frm = new frmExpedition())
            {
                frm.ShowDialog();
            }
        }

        public void EditItem()
        {
            using (var ex_frm = new frmExpedition(row_exp.Id))
            {
                ex_frm.ShowDialog();
            }
        }

        public void DeleteItem()
        {
            var exp = _db.Expedition.Find(row_exp.Id);
            if (exp != null)
            {
                if (XtraMessageBox.Show($"Ви дійсно бажаєте видалити екпедицію #{exp.Num}", "Видалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    _db.Expedition.Remove(exp);
                    _db.SaveChanges();
                }
            }
        }

        public void ExecuteItem()
        {
            if (row_exp == null)
            {
                return;
            }

            var exp = _db.Expedition.Find(row_exp?.Id);

            if (exp == null)
            {
                MessageBox.Show(Resources.not_find_wb);
                return;
            }
            if (exp.SessionId != null)
            {
                MessageBox.Show(Resources.deadlock);
                return;
            }

            if (exp.Checked == 1)
            {
                exp.Checked = 0;
            }
            else
            {
                exp.Checked = 1;

                foreach (var item in _db.v_ExpeditionDet.Where(w => w.ExpeditionId == exp.Id))
                {
                    if (item.RouteId.HasValue && item.Checked == 1)
                    {
                        var exp_wb = _db.WaybillList.Find(item.WbillId);
                        exp_wb.ShipmentDate = exp.OnDate.AddTicks(item.RouteDuration ?? 0);
                    }
                }
            }

            _db.SaveChanges();
        }

        public void PrintItem()
        {
            PrintDoc.ExpeditionReport(row_exp.Id, _db);
        }

        public void ExportToExcel()
        {
            IHelper.ExportToXlsx(ExpeditionsGridControl);
        }

        private void SettingMaterialPricesGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DeleteBtn.Enabled = (row_exp != null && row_exp.Checked == 0 && user_access.CanDelete == 1);
            ExecuteBtn.Enabled = (row_exp != null && user_access.CanPost == 1);
            EditBtn.Enabled = (row_exp != null && row_exp.Checked == 0 && user_access.CanModify == 1);
            CopyBtn.Enabled = (row_exp != null && user_access.CanModify == 1);
            PrintBtn.Enabled = (row_exp != null);

            GetDetailData();

            if (row_exp != null)
            {
                ucRelDocGrid1.GetRelDoc(row_exp.Id);
            }
            else
            {
                ucRelDocGrid1.GetRelDoc(Guid.Empty);
            }
        }

        public void FindItem(Guid id, DateTime on_date)
        {
            find_id = id;

            ExpeditionsGridView.ClearColumnsFilter();
            ExpeditionsGridView.ClearFindFilter();
            ucDocumentFilterPanel.ClearFindFilter(on_date);
            GetData();
        }

        private void GetDetailData()
        {
            if (row_exp != null)
            {
                ExpeditionDetBS.DataSource = DB.SkladBase().v_ExpeditionDet.AsNoTracking().Where(w => w.ExpeditionId == row_exp.Id).OrderBy(o => o.CreatedAt).ToList();
            }
            else
            {
                ExpeditionDetBS.DataSource = null;
            }
        }

        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        private void SettingMaterialPricesUserControl_Load(object sender, EventArgs e)
        {
            ExpeditionsGridView.SaveLayoutToStream(wh_layout_stream);
            ExpeditionsGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                _db = new BaseEntities();
                user_access = _db.UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);
            }
        }

         private void SettingMaterialPricesGridView_DoubleClick(object sender, EventArgs e)
        {
            EditBtn.PerformClick();
        }

        private void SettingMaterialPricesGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (row_exp == null || !_restore)
            {
                return;
            }

            int rowHandle = ExpeditionsGridView.LocateByValue("Id", prev_focused_id, OnRowSearchComplete);
            if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
            {
                FocusRow(ExpeditionsGridView, rowHandle);
            }
            else
            {
                ExpeditionsGridView.FocusedRowHandle = prev_rowHandle;
            }

            _restore = false;


/*
            if (row_exp == null || !_restore)
            {
                return;
            }

            int rowHandle = ExpeditionsGridView.LocateByValue("Id", prev_focused_id);
            if (rowHandle != GridControl.InvalidRowHandle)
            {
                ExpeditionsGridView.FocusedRowHandle = rowHandle;
                ExpeditionsGridView.TopRowIndex = rowHandle;
            }
            else
            {
                ExpeditionsGridView.FocusedRowHandle = prev_rowHandle;
            }

            _restore = false;*/
        }

        void OnRowSearchComplete(object rh)
        {
            int rowHandle = (int)rh;
            if (ExpeditionsGridView.IsValidRowHandle(rowHandle))
            {
                FocusRow(ExpeditionsGridView, rowHandle);
            }
        }

        public void FocusRow(GridView view, int rowHandle)
        {
            view.TopRowIndex = prev_top_row_index == -1 ? rowHandle : prev_top_row_index;
            view.FocusedRowHandle = rowHandle;
        }

        private void NewItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            NewItem();
        }

        private void SettingMaterialPricesGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                ExpeditionsPopupMenu.ShowPopup(p2);
            }
        }

        private void DeleteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteBtn.PerformClick();
        }

        private void RefrechItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetData();
        }

        private void ExecuteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExecuteBtn.PerformClick();
        }

        private void PrintItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            PrintItem();
        }

        private void EditItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
             EditBtn.PerformClick();
        }

        private void SettingMaterialPricesDetGrid_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                ExpeditionsDetPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindDoc.Find(row_smp_det.WaybillListId, -1, row_smp_det.OnDate);
        }

        private void ExpeditionsSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            BaseEntities objectContext = new BaseEntities();
            var list = objectContext.v_Expedition.Where(w => w.OnDate >= ucDocumentFilterPanel.StartDate && w.OnDate <= ucDocumentFilterPanel.EndDate && (ucDocumentFilterPanel.StatusId == -1 || w.Checked == ucDocumentFilterPanel.StatusId));
            e.QueryableSource = list;
            e.Tag = objectContext;
        }

        private void ExpeditionsPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            DeleteItemBtn.Enabled = DeleteBtn.Enabled;
            ExecuteItemBtn.Enabled = ExecuteBtn.Enabled;
            EditItemBtn.Enabled =  EditBtn.Enabled ;
            CopyItemBtn.Enabled = CopyBtn.Enabled ;
            PrintItemBtn.Enabled = PrintBtn.Enabled;
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportToExcel();
        }

        private void ucDocumentFilterPanel_FilterChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void ExpeditionsSource_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (e.Tag == null)
                return;

            ((BaseEntities)e.Tag).Dispose();
        }
    }
}
