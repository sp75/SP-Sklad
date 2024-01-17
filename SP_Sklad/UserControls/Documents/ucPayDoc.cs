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
using SP_Sklad.FinanseForm;

namespace SP_Sklad.UserControls
{
    public partial class ucPayDoc : DevExpress.XtraEditors.XtraUserControl
    {
        public int w_type { get; set; }
        public int fun_id { get; set; }
        public string reg_layout_path { get; set; } //= "ucPayDoc\\PayDocGridView";
        BaseEntities _db { get; set; }
        public BarButtonItem ExtEditBtn { get; set; }
        public BarButtonItem ExtDeleteBtn { get; set; }
        public BarButtonItem ExtExecuteBtn { get; set; }
        public BarButtonItem ExtCopyBtn { get; set; }
        public BarButtonItem ExtPrintBtn { get; set; }

        private v_PayDoc focused_row => PayDocGridView.GetFocusedRow() is NotLoadedObject ? null : PayDocGridView.GetFocusedRow() as v_PayDoc;

        private UserAccess user_access { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private Guid prev_focused_id = Guid.Empty;
        private int prev_top_row_index = 0;
        private Guid? find_id { get; set; }
        private bool restore = false;

        public ucPayDoc()
        {
            InitializeComponent();
        }

        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        private void ucProjectManagement_Load(object sender, EventArgs e)
        {
            PayDocGridView.SaveLayoutToStream(wh_layout_stream);
            PayDocGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                _db = new BaseEntities();
                user_access = _db.UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);

                PDStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                PDEndDate.EditValue = DateTime.Now.Date.SetEndDay();

                PDKagentList.Properties.DataSource = DBHelper.KagentsList;// new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(_db.Kagent.Select(s => new { s.KaId, s.Name }));
                PDKagentList.EditValue = 0;

                PDSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                PDSatusList.EditValue = -1;

                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);
                PayDocGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

                GetData();
            }
        }

        public void NewItem()
        {
            using (var pd = new frmPayDoc(w_type != -2 ? w_type / 3 : w_type, null) { _ka_id = (int)PDKagentList.EditValue == 0 ? null : (int?)PDKagentList.EditValue })
            {
                pd.ShowDialog();
            }
        }

        public void CopyItem()
        {
            var p_doc = DB.SkladBase().DocCopy(focused_row.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();
            using (var pdf = new frmPayDoc(w_type != -2 ? w_type / 3 : w_type, p_doc.out_wbill_id))
            {
                pdf.ShowDialog();
            }
        }

        public void EditItem()
        {
            if (focused_row == null)
            {
                return;
            }

            DocEdit.PDEdit(focused_row.PayDocId, focused_row.DocType);
        }

        public void DeleteItem()
        {
            var _pd = _db.PayDoc.Find(focused_row.PayDocId);

            if (_pd != null)
            {
                _db.PayDoc.Remove(_pd);
                _db.SaveChanges();
            }
            else
            {
                MessageBox.Show(string.Format("Документ #{0} не знайдено", focused_row.DocNum));
            }
        }

        public void ExecuteItem()
        {
            var pd = _db.PayDoc.Find(focused_row.PayDocId);
            if (pd != null)
            {
                if (pd.OnDate > _db.CommonParams.First().EndCalcPeriod)
                {
                    pd.Checked = focused_row.Checked == 0 ? 1 : 0;
                    _db.SaveChanges();
                }
                else
                {
                    XtraMessageBox.Show("Період вже закритий. Змініть дату документа!", "Відміна/Проведення платіжного документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show(string.Format("Документ #{0} не знайдено", focused_row.DocNum));
            }
        }

        public void PrintItem()
        {
            PrintDoc.Show(focused_row.Id, focused_row.ExDocType.Value, _db);
        }

        public void FindItem(Guid id, DateTime on_date)
        {
            find_id = id;
            PDStartDate.DateTime = on_date.Date;
            PDEndDate.DateTime = on_date.Date.SetEndDay();
            PDSatusList.EditValue = -1;

            GetData();
        }

        public void ExportToExcel()
        {
            IHelper.ExportToXlsx(PayDocGridControl);
        }

        public void GetData()
        {
            if (focused_row != null && !find_id.HasValue)
            {
                prev_top_row_index = PayDocGridView.TopRowIndex;
                prev_focused_id = focused_row.Id;
            }

            if (find_id.HasValue)
            {
                prev_top_row_index = -1;
                prev_focused_id = find_id.Value;
                find_id = null;
            }

            restore = true;

            PayDocGridControl.DataSource = null;
            PayDocGridControl.DataSource = GgridDataSource;

            SetWBEditorBarBtn();
        }

        public void SaveGridLayouts()
        {
            PayDocGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        private void ProjectManagementStartDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (PDStartDate.ContainsFocus)
            {
                GetData();
            }
        }

        private void ProjectManagementEndDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (PDEndDate.ContainsFocus)
            {
                GetData();
            }
        }

        private void PMStatusList_EditValueChanged(object sender, EventArgs e)
        {
            if (PDSatusList.ContainsFocus)
            {
                GetData();
            }
        }

        private void SetWBEditorBarBtn()
        {
            xtraTabControl4_SelectedPageChanged(null, null);

            ExtDeleteBtn.Enabled = false;
            ExtExecuteBtn.Enabled = false;
            ExtEditBtn.Enabled = false;
            ExtCopyBtn.Enabled = false;
            ExtPrintBtn.Enabled = false;

            if (focused_row == null)
            {
                return;
            }

            bool isModify = (focused_row != null && (DBHelper.CashDesks.Any(a => a.CashId == focused_row.CashId) || focused_row.CashId == null));

            ExtDeleteBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && user_access?.CanDelete == 1);
            ExtExecuteBtn.Enabled = (focused_row != null && user_access?.CanPost == 1 && isModify);
            ExtEditBtn.Enabled = (focused_row != null && user_access?.CanModify == 1 && isModify);
            ExtCopyBtn.Enabled = (focused_row != null && user_access?.CanModify == 1 && isModify);
            ExtPrintBtn.Enabled = (focused_row != null);
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
            if (PayDocGridView.IsValidRowHandle(rowHandle))
            {
                FocusRow(PayDocGridView, rowHandle);
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
            PrintItem();
        }

        private void RestoreSettingsGridBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            wh_layout_stream.Seek(0, System.IO.SeekOrigin.Begin);

            PayDocGridView.RestoreLayoutFromStream(wh_layout_stream);
        }


        private void KAgentAdjustmentSource_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (e.Tag == null)
                return;

            ((BaseEntities)e.Tag).Dispose();
        }

        private void KAgentAdjustmentSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (PDSatusList.EditValue == null || PDKagentList.EditValue == null)
            {
                return;
            }

            var satrt_date = PDStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : PDStartDate.DateTime;
            var end_date = PDEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : PDEndDate.DateTime;
            var status = (int)PDSatusList.EditValue;
            var ka_id = (int)PDKagentList.EditValue;

            BaseEntities objectContext = new BaseEntities();


            var list = objectContext.v_PayDoc.Join(objectContext.EnterpriseWorker.Where(eww => eww.WorkerId == DBHelper.CurrentUser.KaId), pd => pd.EntId, ew => ew.EnterpriseId, (pd, ew) => pd)
                .Where(w => w.OnDate >= satrt_date && w.OnDate <= end_date && (status == -1 || w.Checked == status) && (w.ExDocType == w_type || w_type == -1) && (w.KaId == ka_id || ka_id == 0));
            e.QueryableSource = list;
            e.Tag = objectContext;
        }

        private void KAgentAdjustmentGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (focused_row == null || !restore)
            {
                return;
            }

            int rowHandle = PayDocGridView.LocateByValue("Id", prev_focused_id, OnRowSearchComplete);
            if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
            {
                FocusRow(PayDocGridView, rowHandle);
            }

            restore = false;
        }

        private void xtraTabControl4_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (focused_row == null)
            {
                vGridControl2.DataSource = null;
                ucRelDocGrid2.GetRelDoc(Guid.Empty);

                return;
            }

            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:

                    vGridControl2.DataSource = _db.v_PayDoc.Where(w => w.Id == focused_row.Id).ToList();
                    break;

                case 1:
                    ucRelDocGrid2.GetRelDoc(focused_row.Id);
                    break;

            }
        }

        private void KAgentAdjustmentGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void KAgentAdjustmentGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void KAgentAdjustmentGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItem();
            GetData();
        }

        private void KAgentAdjustmentGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                GridPopupMenu.ShowPopup(p2);
            }
        }

        private void PDKagentList_EditValueChanged(object sender, EventArgs e)
        {
            if (PDKagentList.ContainsFocus)
            {
                GetData();
            }
        }
        private void PDKagentList_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                PDKagentList.EditValue = IHelper.ShowDirectList(PDKagentList.EditValue, 1);
            }
        }

        private void PeriodComboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            PDEndDate.DateTime = DateTime.Now.Date.SetEndDay();
            switch (PeriodComboBoxEdit.SelectedIndex)
            {
                case 1:
                    PDStartDate.DateTime = DateTime.Now.Date;
                    break;

                case 2:
                    PDStartDate.DateTime = DateTime.Now.Date.StartOfWeek(DayOfWeek.Monday);
                    break;

                case 3:
                    PDStartDate.DateTime = DateTime.Now.Date.FirstDayOfMonth();
                    break;

                case 4:
                    PDStartDate.DateTime = new DateTime(DateTime.Now.Year, 1, 1);
                    break;
            }

            GetData();
        }
    }
}
