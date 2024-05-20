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
using DevExpress.Data;
using SP_Sklad.Common;
using DevExpress.XtraGrid;
using SP_Sklad.WBForm;
using System.Data.Entity;
using DevExpress.XtraEditors;
using SP_Sklad.Properties;
using SP_Sklad.Reports;
using SP_Sklad.ViewsForm;
using SP_Sklad.FinanseForm;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace SP_Sklad.UserControls
{
    public partial class ucWaybillWriteOn : DevExpress.XtraEditors.XtraUserControl
    {
        int w_type = 5;
        private int fun_id = 44;
        private string reg_layout_path = "ucWaybillWriteOn\\WbGridView";
        BaseEntities _db { get; set; }

        public v_WaybillWriteOn wb_focused_row => WbGridView.GetFocusedRow() is NotLoadedObject ? null : WbGridView.GetFocusedRow() as v_WaybillWriteOn;

        private UserAccess user_access { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private int prev_focused_id = 0;
        private int prev_top_row_index = 0;
        private int prev_rowHandle = 0;
        private int? find_id { get; set; }
        private bool restore = false;

        public class checkedWhList
        {
            public string WId { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }
        }

        public ucWaybillWriteOn()
        {
            InitializeComponent();
        }

        public void NewItem()
        {
            using (var wb_move = new frmWBWriteOn())
            {
                wb_move.ShowDialog();
            }
        }

        public void CopyItem()
        {
            if (wb_focused_row == null)
            {
                return;
            }

            using (var frm = new frmMessageBox("Інформація", Resources.wb_copy))
            {
                if (!frm.user_settings.NotShowMessageCopyDocuments && frm.ShowDialog() != DialogResult.Yes)
                {
                    return;
                }
            }

            var doc = DB.SkladBase().DocCopy(wb_focused_row.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();

            using (var wb_re_in = new frmWBWriteOn(doc.out_wbill_id))
            {
                wb_re_in.ShowDialog();
            }
        }

        public void EditItem()
        {
            if(wb_focused_row == null)
            {
                return;
            }

            WhDocEdit.WBEdit(wb_focused_row.WbillId, wb_focused_row.WType);
        }

        public void DeleteItem()
        {
            if (wb_focused_row == null)
            {
                return;
            }

            if (XtraMessageBox.Show($"Ви дійсно бажаєте видалити введення залишків{wb_focused_row.Num}?", "Відалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId && (w.SessionId == null || w.SessionId == UserSession.SessionId) && w.Checked == 0);
                if (wb != null)
                {
                    _db.WaybillList.Remove(wb);

                    _db.SaveChanges();
                }
                else
                {
                    XtraMessageBox.Show(Resources.deadlock);
                }
            }
        }

        public void ExecuteItem()
        {
            if (wb_focused_row == null)
            {
                return;
            }

            using (var db =  new BaseEntities())
            {
                var wb = db.WaybillList.Find(wb_focused_row.WbillId);
                if (wb == null)
                {
                    XtraMessageBox.Show(Resources.not_find_wb);
                    return;
                }
                if (wb.SessionId != null)
                {
                    XtraMessageBox.Show(Resources.deadlock);
                    return;
                }

                if (wb.Checked == 1)
                {
                    DBHelper.StornoOrder(db, wb_focused_row.WbillId);
                }
                else
                {
                    DBHelper.ExecuteOrder(db, wb_focused_row.WbillId);
                }
            }
        }

        public void PrintItem()
        {
            if (wb_focused_row == null)
            {
                return;
            }

            PrintDoc.Show(wb_focused_row.Id, w_type, _db);
        }

        public void FindItem(Guid id, DateTime on_date)
        {
            find_id = new BaseEntities().WaybillList.FirstOrDefault(w => w.Id == id)?.WbillId;
            WbGridView.ClearColumnsFilter();
            WbGridView.ClearFindFilter();
            PeriodComboBoxEdit.SelectedIndex = 0;
            wbStartDate.DateTime = on_date.Date;
            wbEndDate.DateTime = on_date.Date.SetEndDay();
            wbStatusList.EditValue = -1;

            GetData();
        }

        public void ExportToExcel()
        {
            IHelper.ExportToXlsx(WBGridControl);
        }

        private void WayBillInSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (wbStatusList.EditValue == null  )
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;
            var status = (int)wbStatusList.EditValue;


            BaseEntities objectContext = new BaseEntities();
            var list = objectContext.v_WaybillWriteOn.Where(w => w.WType == w_type && w.OnDate > satrt_date && w.OnDate <= end_date && (w.Checked == status || status == -1) && w.WorkerId == DBHelper.CurrentUser.KaId);
            e.QueryableSource = list;
            e.Tag = objectContext;
        }

        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        private void WayBillInUserControl_Load(object sender, EventArgs e)
        {
            WbGridView.SaveLayoutToStream(wh_layout_stream);

            WbGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                _db = new BaseEntities();
                user_access = _db.UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);

                wbStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                wbStatusList.EditValue = -1;

                wbStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                wbEndDate.EditValue = DateTime.Now.Date.SetEndDay();

                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);
                WbGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

                GetData();
            }
        }

        public void GetData()
        {
            prev_rowHandle = WbGridView.FocusedRowHandle;

            if (wb_focused_row != null && !find_id.HasValue)
            {
                prev_top_row_index = WbGridView.TopRowIndex;
                prev_focused_id = wb_focused_row.WbillId;
            }

            if (find_id.HasValue)
            {
                prev_top_row_index = -1;
                prev_focused_id = find_id.Value;
                find_id = null;
            }

            restore = true;

            WBGridControl.DataSource = null;
            WBGridControl.DataSource = WayBillMoveSource;

            SetWBEditorBarBtn();
        }

        private void PeriodComboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            wbEndDate.DateTime = DateTime.Now.Date.SetEndDay();
            switch (PeriodComboBoxEdit.SelectedIndex)
            {
                case 1:
                    wbStartDate.DateTime = DateTime.Now.Date;
                    break;

                case 2:
                    wbStartDate.DateTime = DateTime.Now.Date.StartOfWeek(DayOfWeek.Monday);
                    break;

                case 3:
                    wbStartDate.DateTime = DateTime.Now.Date.FirstDayOfMonth();
                    break;

                case 4:
                    wbStartDate.DateTime = new DateTime(DateTime.Now.Year, 1, 1);
                    break;
            }

            GetData();
        }

        private void WbGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (wb_focused_row == null || !restore)
            {
                return;
            }

            int rowHandle = WbGridView.LocateByValue("WbillId", prev_focused_id, OnRowSearchComplete);
            if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
            {
                FocusRow(WbGridView, rowHandle);
            }
            else
            {
                WbGridView.FocusedRowHandle = prev_rowHandle;
            }

            restore = false;
        }

        void OnRowSearchComplete(object rh)
        {
            int rowHandle = (int)rh;
            if (WbGridView.IsValidRowHandle(rowHandle))
            {
                FocusRow(WbGridView, rowHandle);
            }
        }

        public void FocusRow(GridView view, int rowHandle)
        {
            view.TopRowIndex = prev_top_row_index == -1 ? rowHandle : prev_top_row_index;
            view.FocusedRowHandle = rowHandle;
        }

        private void RefrechItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetData();
        }

        private void DeleteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteItem();
            GetData();
        }

        private void ExecuteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExecuteItem();
            GetData();
        }

        private void PrintItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            PrintItem();
        }

        private void EditItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditItem();
            GetData();
        }

        private void WbGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if(e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                WbListPopupMenu.ShowPopup(p2);
            }
        }

        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItem();
            GetData();
        }

        public void SaveGridLayouts()
        {
            WbGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        private void SetWBEditorBarBtn()
        {
            xtraTabControl2_SelectedPageChanged(null, null);

            NewItemBtn.Enabled = (user_access?.CanInsert == 1);
            DeleteItemBtn.Enabled = (wb_focused_row != null && wb_focused_row.Checked == 0 && user_access?.CanDelete == 1);
            ExecuteItemBtn.Enabled = (wb_focused_row != null && user_access?.CanPost == 1);
            EditItemBtn.Enabled = (wb_focused_row != null && user_access?.CanModify == 1);
            CopyItemBtn.Enabled = (wb_focused_row != null && user_access?.CanModify == 1);
            PrintItemBtn.Enabled = (wb_focused_row != null);
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (wb_focused_row == null)
            {
                ucWayBillInDet.GetDate(0);
                ucRelDocGrid1.GetRelDoc(Guid.Empty);

                return;
            }

            switch (xtraTabControl2.SelectedTabPageIndex)
            {
                case 0:
                    ucWayBillInDet.GetDate(wb_focused_row.WbillId);
                    break;

                case 1:
                    vGridControl1.DataSource = new BaseEntities().v_WaybillWriteOn.AsNoTracking().Where(w => w.WbillId == wb_focused_row.WbillId && w.WorkerId == DBHelper.CurrentUser.KaId).ToList();
                    break;

                case 2:
                    ucRelDocGrid1.GetRelDoc(wb_focused_row.Id);
                    break;
            }
        }

        private void wbStatusList_EditValueChanged(object sender, EventArgs e)
        {
            if(wbStatusList.ContainsFocus)
            {
                GetData();
            }
        }

        private void wbEndDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbEndDate.ContainsFocus)
            {
                GetData();
            }
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbStartDate.ContainsFocus)
            {
                GetData();
            }
        }

        private void WbListPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            WbHistoryBtn.Enabled = IHelper.GetUserAccess(39)?.CanView == 1;
        }

        private void WbGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void WbGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void NewItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            NewItem();
            GetData();
        }

        private void CopyItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            CopyItem();
            GetData();
        }


        private void WbHistoryBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new frmLogHistory(24, wb_focused_row.WbillId))
            {
                frm.ShowDialog();
            }
        }

        private void WayBillInSource_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (e.Tag == null)
                return;

            ((BaseEntities)e.Tag).Dispose();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            wh_layout_stream.Seek(0, System.IO.SeekOrigin.Begin);

            WbGridView.RestoreLayoutFromStream(wh_layout_stream);
        }


        private void ExportToExcelBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportToExcel();
        }

    }
}
