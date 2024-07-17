using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.SkladData;
using DevExpress.XtraBars;
using SP_Sklad.Common;
using SP_Sklad.Reports;
using SP_Sklad.Properties;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;
using SP_Sklad.ViewsForm;
using SP_Sklad.WBForm;

namespace SP_Sklad.UserControls
{
    public partial class ucDiscountManagement : DevExpress.XtraEditors.XtraUserControl
    {
        private int fun_id = 95;
        private string reg_layout_path = "ucDiscountManagement\\DiscountManagementGridView";
        BaseEntities _db { get; set; }
        public BarButtonItem ExtEditBtn { get; set; }
        public BarButtonItem ExtDeleteBtn { get; set; }
        public BarButtonItem ExtExecuteBtn { get; set; }
        public BarButtonItem ExtCopyBtn { get; set; }
        public BarButtonItem ExtPrintBtn { get; set; }

        public v_ProjectManagement pm_focused_row => ProjectManagementGridView.GetFocusedRow() is NotLoadedObject ? null : ProjectManagementGridView.GetFocusedRow() as v_ProjectManagement;

        private UserAccess user_access { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private Guid prev_focused_id = Guid.Empty;
        private int prev_top_row_index = 0;
        private int prev_rowHandle = 0;
        private Guid? find_id { get; set; }
        private bool restore = false;

        public ucDiscountManagement()
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
            ProjectManagementGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        public void NewItem()
        {
            using (var frm = new frmProjectManagement())
            {
                frm.ShowDialog();
            }
        }

        public void CopyItem()
        {
            ;
        }

        public void EditItem()
        {
            if (pm_focused_row == null)
            {
                return;
            }

            using (var frm = new frmProjectManagement(pm_focused_row.Id))
            {
                frm.ShowDialog();
            }
        }

        public void DeleteItem()
        {
            if (pm_focused_row == null)
            {
                return;
            }

            if (XtraMessageBox.Show($"Ви дійсно бажаєте видалити документ #{pm_focused_row.Num}?", "Видалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                var wb = _db.ProjectManagement.FirstOrDefault(w => w.Id == pm_focused_row.Id && (w.SessionId == null || w.SessionId == UserSession.SessionId) && w.Checked == 0);
                if (wb != null)
                {
                    _db.ProjectManagement.Remove(wb);

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
            if (pm_focused_row == null)
            {
                return;
            }

            using (var db = new BaseEntities())
            {
                var pm = db.ProjectManagement.Find(pm_focused_row.Id);

                if (pm == null)
                {
                    MessageBox.Show(Resources.not_find_wb);
                    return;
                }
                if (pm.SessionId != null)
                {
                    XtraMessageBox.Show(Resources.deadlock);
                    return;
                }

                var rel = db.GetRelDocList(pm_focused_row.Id).OrderBy(o => o.OnDate).ToList();
                if (rel.Any())
                {
                    XtraMessageBox.Show(Resources.not_storno_wb, "Відміна проводки", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (pm.Checked == 1)
                {
                    pm.Checked = 0;
                }

                db.SaveChanges();
            }
        }

        public void PrintItem()
        {
            if (pm_focused_row == null)
            {
                return;
            }
        }

        public void FindItem(Guid id, DateTime on_date)
        {
            find_id = id;
            ProjectManagementGridView.ClearColumnsFilter();
            ProjectManagementGridView.ClearFindFilter();
            ucDocumentFilterPanel.ClearFindFilter(on_date);

            GetData();
        }
        public void ExportToExcel()
        {
            IHelper.ExportToXlsx(ProjectManagementGridControl);
        }

        public void GetData()
        {
            prev_rowHandle = ProjectManagementGridView.FocusedRowHandle;

            if (pm_focused_row != null && !find_id.HasValue)
            {
                prev_top_row_index = ProjectManagementGridView.TopRowIndex;
                prev_focused_id = pm_focused_row.Id;
            }

            if (find_id.HasValue)
            {
                prev_top_row_index = -1;
                prev_focused_id = find_id.Value;
                find_id = null;
            }

            restore = true;

            ProjectManagementGridControl.DataSource = null;
            ProjectManagementGridControl.DataSource = ProjectManagementSource;

            SetWBEditorBarBtn();
        }

     

        private void xtraTabControl7_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (pm_focused_row == null)
            {
                ProjectManagementDetGridControl.DataSource = null;
                ucRelDocGrid6.GetRelDoc(Guid.Empty);
                return;
            }

            switch (xtraTabControl7.SelectedTabPageIndex)
            {
                case 0:
                    ProjectManagementDetGridControl.DataSource = _db.v_ProjectManagementDet.AsNoTracking().Where(w => w.ProjectManagementId == pm_focused_row.Id).OrderBy(o => o.Num).ToList();
                    break;

                case 2:
                    ucRelDocGrid6.GetRelDoc(pm_focused_row.Id);
                    break;
            }
        }

        private void ProjectManagementSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            BaseEntities objectContext = new BaseEntities();
            var list = objectContext.v_ProjectManagement.Where(w => w.OnDate >= ucDocumentFilterPanel.StartDate && w.OnDate < ucDocumentFilterPanel.EndDate && (ucDocumentFilterPanel.StatusId == -1 || w.Checked == ucDocumentFilterPanel.StatusId));
            e.QueryableSource = list;
            e.Tag = objectContext;
        }

        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        private void ucProjectManagement_Load(object sender, EventArgs e)
        {
            ProjectManagementGridView.SaveLayoutToStream(wh_layout_stream);
            ProjectManagementGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                _db = new BaseEntities();
                user_access = _db.UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);

                GetData();
            }
        }

        private void SetWBEditorBarBtn()
        {
            xtraTabControl7_SelectedPageChanged(null, null);

            ExtDeleteBtn.Enabled = false;
            ExtExecuteBtn.Enabled = false;
            ExtEditBtn.Enabled = false;
            ExtCopyBtn.Enabled = false;
            ExtPrintBtn.Enabled = false;

            if (pm_focused_row == null)
            {
                return;
            }

            ExtDeleteBtn.Enabled = (pm_focused_row != null && pm_focused_row.Checked == 0 && user_access.CanDelete == 1);
            ExtExecuteBtn.Enabled = (pm_focused_row != null && user_access.CanPost == 1);
            ExtEditBtn.Enabled = (pm_focused_row != null && user_access.CanModify == 1 && pm_focused_row.Checked == 0);
            ExtCopyBtn.Enabled = (pm_focused_row != null && user_access.CanModify == 1);
            ExtPrintBtn.Enabled = (pm_focused_row != null);
        }

        private void PMGridPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            DeleteItemBtn.Enabled = ExtDeleteBtn.Enabled;
            ExecuteItemBtn.Enabled = ExtExecuteBtn.Enabled;
            EditItemBtn.Enabled = ExtEditBtn.Enabled;
            CopyItemBtn.Enabled = ExtCopyBtn.Enabled;
            PrintItemBtn.Enabled = ExtPrintBtn.Enabled;

            WbHistoryBtn.Enabled = IHelper.GetUserAccess(39)?.CanView == 1;
        }

        private void ProjectManagementGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (pm_focused_row == null || !restore)
            {
                return;
            }

            int rowHandle = ProjectManagementGridView.LocateByValue("Id", prev_focused_id, OnRowSearchComplete);
            if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
            {
                FocusRow(ProjectManagementGridView, rowHandle);
            }
            else
            {
                ProjectManagementGridView.FocusedRowHandle = prev_rowHandle;
            }

            restore = false;
        }

        void OnRowSearchComplete(object rh)
        {
            int rowHandle = (int)rh;
            if (ProjectManagementGridView.IsValidRowHandle(rowHandle))
            {
                FocusRow(ProjectManagementGridView, rowHandle);
            }
        }

        public void FocusRow(GridView view, int rowHandle)
        {
            view.TopRowIndex = prev_top_row_index == -1 ? rowHandle : prev_top_row_index;
            view.FocusedRowHandle = rowHandle;
        }

        private void ProjectManagementSource_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (e.Tag == null)
                return;

            ((BaseEntities)e.Tag).Dispose();
        }

        private void ProjectManagementGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void ProjectManagementGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
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
         /*   using (var frm = new frmLogHistory(24, wb_focused_row.WbillId))
            {
                frm.ShowDialog();
            }*/
        }

        private void ExportToExcelBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportToExcel();
        }

        private void ProjectManagementGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditItemBtn.PerformClick();
        }

        private void ProjectManagementGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                ProjectManagementPopupMenu.ShowPopup(p2);
            }
        }

        private void DeleteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteItem();
            GetData();
        }

        private void RefrechItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetData();
        }

        private void ExecuteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExecuteItem();
            GetData();
        }

        private void EditItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditItem();
            GetData();
        }

        private void PrintItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ;
        }

        private void RestoreSettingsGridBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            wh_layout_stream.Seek(0, System.IO.SeekOrigin.Begin);

            ProjectManagementGridView.RestoreLayoutFromStream(wh_layout_stream);
        }

        private void ucDocumentFilterPanel_FilterChanged(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
