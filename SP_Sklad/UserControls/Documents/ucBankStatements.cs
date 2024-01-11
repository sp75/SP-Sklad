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
    public partial class ucBankStatements : DevExpress.XtraEditors.XtraUserControl
    {
        private int fun_id = 90;
        private string reg_layout_path = "ucBankStatements\\BankStatementsGridView";

        BaseEntities _db { get; set; }
        public BarButtonItem ExtEditBtn { get; set; }
        public BarButtonItem ExtDeleteBtn { get; set; }
        public BarButtonItem ExtExecuteBtn { get; set; }
        public BarButtonItem ExtCopyBtn { get; set; }
        public BarButtonItem ExtPrintBtn { get; set; }

        public v_BankStatements bank_statements_row => BankStatementsGridView.GetFocusedRow() is NotLoadedObject ? null : BankStatementsGridView.GetFocusedRow() as v_BankStatements;

        private UserAccess user_access { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private Guid prev_focused_id = Guid.Empty;
        private int prev_top_row_index = 0;
        private Guid? find_id { get; set; }
        private bool restore = false;

        public ucBankStatements()
        {
            InitializeComponent();
        }

        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        private void ucProjectManagement_Load(object sender, EventArgs e)
        {
            BankStatementsGridView.SaveLayoutToStream(wh_layout_stream);
            BankStatementsGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                _db = new BaseEntities();
                user_access = _db.UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);


                BSStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                BSStatusList.EditValue = -1;

                BSStartDate.EditValue = DateTime.Now.Date.FirstDayOfMonth();
                BSEndDate.EditValue = DateTime.Now.Date.SetEndDay();

                GetData();
            }
        }

        public void NewItem()
        {
            using (var frm = new frmBankStatements())
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
            if (bank_statements_row == null)
            {
                return;
            }

            using (var frm = new frmBankStatements(bank_statements_row.Id))
            {
                frm.ShowDialog();
            }
        }

        public void DeleteItem()
        {
            var bs = _db.BankStatements.Find(bank_statements_row.Id);

            if (bs != null)
            {
                _db.BankStatements.Remove(bs);

                _db.SaveChanges();
            }
            else
            {
                MessageBox.Show(string.Format("Документ #{0} не знайдено", bank_statements_row.Num));
            }
        }

        public void ExecuteItem()
        {
            ;
        }

        public void PrintItem()
        {
            ;
        }

        public void FindItem(Guid id, DateTime on_date)
        {
            find_id = id;
            BSStartDate.DateTime = on_date.Date;
            BSEndDate.DateTime = on_date.Date.SetEndDay();
            BSStatusList.EditValue = -1;

            GetData();
        }
        public void ExportToExcel()
        {
            IHelper.ExportToXlsx(BankStatementsGridControl);
        }

        public void GetData()
        {
            if (bank_statements_row != null && !find_id.HasValue)
            {
                prev_top_row_index = BankStatementsGridView.TopRowIndex;
                prev_focused_id = bank_statements_row.Id;
            }

            if (find_id.HasValue)
            {
                prev_top_row_index = -1;
                prev_focused_id = find_id.Value;
                find_id = null;
            }

            restore = true;

            BankStatementsGridControl.DataSource = null;
            BankStatementsGridControl.DataSource = BankStatementsSource;

            xtraTabControl6_SelectedPageChanged(null, null);
        }


        private void ProjectManagementSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (BSStatusList.EditValue == null )
            {
                return;
            }


            BaseEntities objectContext = new BaseEntities();
            var list = objectContext.v_BankStatements.AsNoTracking().Where(w => w.OnDate >= BSStartDate.DateTime && w.OnDate < BSEndDate.DateTime && ((int)BSStatusList.EditValue == -1 || w.Checked == (int)BSStatusList.EditValue));
            e.QueryableSource = list;
            e.Tag = objectContext;
        }

       

        public void SaveGridLayouts()
        {
            BankStatementsGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        private void ProjectManagementStartDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (BSStartDate.ContainsFocus)
            {
                GetData();
            }
        }

        private void ProjectManagementEndDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (BSEndDate.ContainsFocus)
            {
                GetData();
            }
        }

        private void PMStatusList_EditValueChanged(object sender, EventArgs e)
        {
            if (BSStatusList.ContainsFocus)
            {
                GetData();
            }
        }

        private void SetWBEditorBarBtn()
        {
            xtraTabControl6_SelectedPageChanged(null, null);

            ExtDeleteBtn.Enabled = (bank_statements_row != null && bank_statements_row.Checked == 0 && user_access.CanDelete == 1);
            ExtExecuteBtn.Enabled = (bank_statements_row != null && user_access.CanPost == 1);
            ExtEditBtn.Enabled = (bank_statements_row != null && user_access.CanModify == 1 && bank_statements_row.Checked == 0);
            ExtCopyBtn.Enabled = (bank_statements_row != null && user_access.CanModify == 1);
            ExtPrintBtn.Enabled = (bank_statements_row != null);
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

    
        void OnRowSearchComplete(object rh)
        {
            int rowHandle = (int)rh;
            if (BankStatementsGridView.IsValidRowHandle(rowHandle))
            {
                FocusRow(BankStatementsGridView, rowHandle);
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

            BankStatementsGridView.RestoreLayoutFromStream(wh_layout_stream);
        }

        private void xtraTabControl6_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (bank_statements_row == null)
            {
                BankStatementsDetGridControl.DataSource = null;
                ucRelDocGrid5.GetRelDoc(Guid.Empty);


                return;
            }

            switch (xtraTabControl6.SelectedTabPageIndex)
            {
                case 0:

                    BankStatementsDetGridControl.DataSource = _db.v_BankStatementsDet.AsNoTracking().Where(w => w.BankStatementId == bank_statements_row.Id).ToList();
                    break;

                case 2:
                    ucRelDocGrid5.GetRelDoc(bank_statements_row.Id);
                    break;
            }
        }

        private void BankStatementsGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void BankStatementsGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (bank_statements_row == null || !restore)
            {
                return;
            }

            int rowHandle = BankStatementsGridView.LocateByValue("Id", prev_focused_id, OnRowSearchComplete);
            if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
            {
                FocusRow(BankStatementsGridView, rowHandle);
            }

            restore = false;
        }

        private void BankStatementsGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                ProjectManagementPopupMenu.ShowPopup(p2);
            }
        }

        private void BankStatementsGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void BankStatementsGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditItemBtn.PerformClick();
        }
    }
}
