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

namespace SP_Sklad.UserControls
{
    public partial class ucActServicesProvidedIn : DevExpress.XtraEditors.XtraUserControl
    {
        private int fun_id = 94;
        int w_type = 29;
        private string reg_layout_path = "ucActServicesProvidedIn\\WbGridView";
        BaseEntities _db { get; set; }
        public BarButtonItem ExtEditBtn { get; set; }
        public BarButtonItem ExtDeleteBtn { get; set; }
        public BarButtonItem ExtExecuteBtn { get; set; }
        public BarButtonItem ExtCopyBtn { get; set; }
        public BarButtonItem ExtPrintBtn { get; set; }

        public v_WayBillIn wb_focused_row => WbGridView.GetFocusedRow() is NotLoadedObject ? null : WbGridView.GetFocusedRow() as v_WayBillIn;

        private UserAccess user_access { get; set; }
        private UserSettingsRepository user_settings { get; set; }
        private FocusGridRow fgr;

        public ucActServicesProvidedIn()
        {
            InitializeComponent();
            fgr = new FocusGridRow(WbGridView);
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WbGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        public void NewItem()
        {
            using (var wb_in = new frmActServicesProvided(w_type, null))
            {
                wb_in.ShowDialog();
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

            using (var wb_in = new frmActServicesProvided(w_type, doc.out_wbill_id))
            {
                wb_in.is_new_record = true;
                wb_in.ShowDialog();
            }
        }

        public void EditItem()
        {
            if(wb_focused_row == null)
            {
                return;
            }

            DocEdit.WBEdit(wb_focused_row.WbillId, wb_focused_row.WType);
        }

        public void DeleteItem()
        {
            if (wb_focused_row == null)
            {
                return;
            }

            if (XtraMessageBox.Show($"Ви дійсно бажаєте видалити акт про надання послуг {wb_focused_row.Num}?", "Видалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
            fgr.find_id = new BaseEntities().WaybillList.FirstOrDefault(w => w.Id == id)?.WbillId;
            WbGridView.ClearColumnsFilter();
            WbGridView.ClearFindFilter();
            ucDocumentFilterPanel.ClearFindFilter(on_date);

            GetData();
        }
        public void ExportToExcel()
        {
            IHelper.ExportToXlsx(WBGridControl);
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void WayBillInSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            BaseEntities objectContext = new BaseEntities();
            var list = objectContext.v_WayBillIn.Where(w => w.WType == w_type && w.OnDate > ucDocumentFilterPanel.StartDate && w.OnDate <= ucDocumentFilterPanel.EndDate && (w.Checked == ucDocumentFilterPanel.StatusId || ucDocumentFilterPanel.StatusId == -1) && (w.KaId == ucDocumentFilterPanel.KagentId || ucDocumentFilterPanel.KagentId == 0) && w.WorkerId == DBHelper.CurrentUser.KaId);
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

                WbBalansGridColumn.Visible = (DBHelper.CurrentUser.ShowBalance == 1);
                WbBalansGridColumn.OptionsColumn.ShowInCustomizationForm = WbBalansGridColumn.Visible;

                WbSummPayGridColumn.Visible = WbBalansGridColumn.Visible;
                WbSummPayGridColumn.OptionsColumn.ShowInCustomizationForm = WbBalansGridColumn.Visible;

                gridColumn44.Caption = "Сума в нац. валюті, " + DBHelper.NationalCurrency.ShortName;

                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);
                WbGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
             

                repositoryItemLookUpEdit3.DataSource = DBHelper.PayTypes;
                repositoryItemLookUpEdit5.DataSource = DBHelper.EnterpriseList;
            }
        }

        public void GetData(bool restore = true)
        {
            fgr.SetPrevData((wb_focused_row?.WbillId ?? 0), restore);

            WBGridControl.DataSource = null;
            WBGridControl.DataSource = WayBillInSource;

            SetWBEditorBarBtn();
        }

        private void WbGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (wb_focused_row == null )
            {
                return;
            }

            fgr.SetRowFocus("WbillId");
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

        private void SetWBEditorBarBtn()
        {
            xtraTabControl2_SelectedPageChanged(null, null);

            ExtDeleteBtn.Enabled = false;
            ExtExecuteBtn.Enabled = false;
            ExtEditBtn.Enabled = false;
            ExtCopyBtn.Enabled = false;
            ExtPrintBtn.Enabled = false;

            if (wb_focused_row == null)
            {
                return;
            }

            ExtDeleteBtn.Enabled = (wb_focused_row != null && wb_focused_row.Checked == 0 && user_access.CanDelete == 1);
            ExtExecuteBtn.Enabled = (wb_focused_row != null && wb_focused_row.WType != 2 && user_access.CanPost == 1);
            ExtEditBtn.Enabled = (wb_focused_row != null && user_access.CanModify == 1 && (wb_focused_row.Checked == 0 || wb_focused_row.Checked == 1));
            ExtCopyBtn.Enabled = (wb_focused_row != null && user_access.CanModify == 1);
            ExtPrintBtn.Enabled = (wb_focused_row != null);
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (wb_focused_row == null)
            {
                ucActServicesProvidedInDet.GetDate(0);
                ucRelDocGrid1.GetRelDoc(Guid.Empty);
                vGridControl1.DataSource = null;
                ucDocumentPaymentGrid.GetPaymentDoc(Guid.Empty);

                return;
            }

            switch (xtraTabControl2.SelectedTabPageIndex)
            {
                case 0:
                    ucActServicesProvidedInDet.GetDate(wb_focused_row.WbillId);
                    break;

                case 1:
                    vGridControl1.DataSource = new BaseEntities().v_WayBillIn.AsNoTracking().Where(w => w.WbillId == wb_focused_row.WbillId && w.WorkerId == DBHelper.CurrentUser.KaId).ToList();
                    break;

                case 2:
                    ucRelDocGrid1.GetRelDoc(wb_focused_row.Id);
                    break;

                case 3:
                    ucDocumentPaymentGrid.GetPaymentDoc(wb_focused_row.Id);
                    break;
            }
        }

        private void WbListPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            DeleteItemBtn.Enabled = ExtDeleteBtn.Enabled;
            ExecuteItemBtn.Enabled = ExtExecuteBtn.Enabled;
            EditItemBtn.Enabled = ExtEditBtn.Enabled;
            CopyItemBtn.Enabled = ExtCopyBtn.Enabled;
            PrintItemBtn.Enabled = ExtPrintBtn.Enabled;

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

        private void repositoryItemLookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            if (!ExtEditBtn.Enabled)
            {
                return;
            }

            var PTypeId = Convert.ToInt32(((LookUpEdit)sender).EditValue);

            var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);

            wb.PTypeId = PTypeId;
            _db.SaveChanges();

            RefrechItemBtn.PerformClick();
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

        private void repositoryItemLookUpEdit5_EditValueChanged(object sender, EventArgs e)
        {
            if (!ExtEditBtn.Enabled)
            {
                return;
            }

            var EntId = Convert.ToInt32(((LookUpEdit)sender).EditValue);

            var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);

            wb.EntId = EntId;
            _db.SaveChanges();

            GetData();
        }

        private void NewPayDocBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if ((wb_focused_row.SummInCurr - wb_focused_row.SummPay) <= 0)
            {
                MessageBox.Show("Документ вже оплачено!");
                return;
            }

            var frm = new frmPayDoc(-1, null, wb_focused_row.SummInCurr)
            {
                PayDocCheckEdit = { Checked = true },
                TypDocsEdit = { EditValue = wb_focused_row.WType },
                _ka_id = wb_focused_row.KaId,
                KagentComboBox = { EditValue = wb_focused_row.KaId }
            };

            frm.GetDocList();
            frm.DocListEdit.EditValue = wb_focused_row.Id;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                GetData();
            }
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

        private void ucDocumentFilterPanel_FilterChanged(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
