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
    public partial class ucWayBillCustomerOrder : DevExpress.XtraEditors.XtraUserControl
    {
        int w_type = -16;
        private int fun_id = 64;
        private string reg_layout_path = "ucWBOrdersIn\\WbGridView";
        BaseEntities _db { get; set; }
        public BarButtonItem ExtEditBtn { get; set; }
        public BarButtonItem ExtDeleteBtn { get; set; }
        public BarButtonItem ExtExecuteBtn { get; set; }
        public BarButtonItem ExtCopyBtn { get; set; }
        public BarButtonItem ExtPrintBtn { get; set; }

        public v_WayBillCustomerOrder wb_focused_row => WbGridView.GetFocusedRow() is NotLoadedObject ? null : WbGridView.GetFocusedRow() as v_WayBillCustomerOrder;

        private UserAccess user_access { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private int prev_focused_id = 0;
        private int prev_top_row_index = 0;
        private int prev_rowHandle = 0;
        private int? _find_id { get; set; }
        private bool _restore = false;

        public ucWayBillCustomerOrder()
        {
            InitializeComponent();
        }

        public void NewItem()
        {
            using (var wb_in = new frmWayBillCustomerOrder(w_type, null))
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

            using (var wb_in = new frmWayBillCustomerOrder(w_type, doc.out_wbill_id))
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

            if (XtraMessageBox.Show($"Ви дійсно бажаєте видалити замовлення від клієнта {wb_focused_row.Num}?", "Відалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
            var wbl = DB.SkladBase().WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);
            if (wbl == null)
            {
                return;
            }

            if (wbl.Checked == 0)
            {
                using (var f = new frmWayBillOut(-1, null))
                {
                    var result = f._db.ExecuteWayBill(wbl.WbillId, null, DBHelper.CurrentUser.KaId).ToList().FirstOrDefault();
                    f.doc_id = result.NewDocId;
                    f.is_new_record = true;
                    f.ShowDialog();
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
        public void ExportToExcel()
        {
            IHelper.ExportToXlsx(WBGridControl);
        }

        public void FindItem(Guid id, DateTime on_date)
        {
            _find_id = new BaseEntities().WaybillList.FirstOrDefault(w => w.Id == id).WbillId;
            wbStartDate.DateTime = on_date.Date;
            wbEndDate.DateTime = on_date.Date.SetEndDay();
            wbKagentList.EditValue = 0;
            wbStatusList.EditValue = -1;

            GetData();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void WayBillInSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (wbStatusList.EditValue == null || wbKagentList.EditValue == null )
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;
            var status = (int)wbStatusList.EditValue;
            var kagent_id = (int)wbKagentList.EditValue;


            BaseEntities objectContext = new BaseEntities();
            var list = objectContext.v_WayBillCustomerOrder.Where(w => w.WType == w_type && w.OnDate > satrt_date && w.OnDate <= end_date && (w.Checked == status || status == -1) && (w.KaId == kagent_id || kagent_id == 0) && w.WorkerId == DBHelper.CurrentUser.KaId);
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

                wbKagentList.Properties.DataSource = DBHelper.KagentsList;//new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(_db.Kagent.Select(s => new { s.KaId, s.Name }));
                wbKagentList.EditValue = 0;

                wbStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                wbStatusList.EditValue = -1;

                wbStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                wbEndDate.EditValue = DateTime.Now.Date.SetEndDay();


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

        public void GetData()
        {
            prev_rowHandle = WbGridView.FocusedRowHandle;

            if (wb_focused_row != null && !_find_id.HasValue)
            {
                prev_top_row_index = WbGridView.TopRowIndex;
                prev_focused_id = wb_focused_row.WbillId;
            }

            if (_find_id.HasValue)
            {
                prev_top_row_index = -1;
                prev_focused_id = _find_id.Value;
                _find_id = null;
            }

            _restore = true;

            WBGridControl.DataSource = null;
            WBGridControl.DataSource = WayBillOutSource;

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
            if (wb_focused_row == null || !_restore)
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

            _restore = false;
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
            ExtExecuteBtn.Enabled = (wb_focused_row != null && wb_focused_row.Checked == 0 && user_access.CanPost == 1);
            ExtEditBtn.Enabled = (wb_focused_row != null && user_access.CanModify == 1 && wb_focused_row.Checked == 0);
            ExtCopyBtn.Enabled = (wb_focused_row != null && user_access.CanModify == 1);
            ExtPrintBtn.Enabled = (wb_focused_row != null);
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (wb_focused_row == null)
            {
                ucWayBillOrdersInDet.GetDate(0);
                ucRelDocGrid1.GetRelDoc(Guid.Empty);
                vGridControl1.DataSource = null;
                ucDocumentPaymentGrid.GetPaymentDoc(Guid.Empty);

                return;
            }

            switch (xtraTabControl2.SelectedTabPageIndex)
            {
                case 0:
                    ucWayBillOrdersInDet.GetDate(wb_focused_row.WbillId);
                    break;

                case 1:
                    vGridControl1.DataSource = new BaseEntities().v_WayBillCustomerOrder.Where(w => w.WbillId == wb_focused_row.WbillId && w.WorkerId == DBHelper.CurrentUser.KaId).ToList();
                    break;

                case 2:
                    ucRelDocGrid1.GetRelDoc(wb_focused_row.Id);
                    break;

                case 3:
                    ucDocumentPaymentGrid.GetPaymentDoc(wb_focused_row.Id);
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

        private void wbKagentList_EditValueChanged(object sender, EventArgs e)
        {
            if (wbKagentList.ContainsFocus)
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
            DeleteItemBtn.Enabled = ExtDeleteBtn.Enabled;
            ExecuteItemBtn.Enabled = ExtExecuteBtn.Enabled;
            EditItemBtn.Enabled = ExtEditBtn.Enabled;
            CopyItemBtn.Enabled = ExtCopyBtn.Enabled;
            PrintItemBtn.Enabled = ExtPrintBtn.Enabled;


            ChangeWaybillKagentBtnEdit.Enabled = (DBHelper.is_admin || DBHelper.is_buh) ;
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

            var frm = new frmPayDoc(1, null, wb_focused_row.SummInCurr)
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

        private void repositoryItemDateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (!EditItemBtn.Enabled)
            {
                return;
            }

            var s_date = ((DateEdit)sender).DateTime;

            var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);

            wb.ShipmentDate = s_date;
            _db.SaveChanges();

            RefrechItemBtn.PerformClick();
        }

        private void ExecuteInvBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
           
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           if(e.Button.Index == 0)
            {
                using (var frm = new frmKagents(-1, ""))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        var new_id = frm.focused_row?.KaId;

                        if (new_id != null)
                        {
                            _db.ChangeWaybillKagent(new_id, wb_focused_row.WbillId);

                            GetData();
                        }
                    }
                }
            }
        }

        private void ExportToExcelBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportToExcel();
        }
    }
}
