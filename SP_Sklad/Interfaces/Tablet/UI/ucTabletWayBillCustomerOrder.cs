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
using OpenStore.Tranzit.Base;

namespace SP_Sklad.Interfaces.Tablet.UI
{
    public partial class ucTabletWayBillCustomerOrder : DevExpress.XtraEditors.XtraUserControl
    {
        int w_type = -16;
        private int fun_id = 64;
        private string reg_layout_path = "ucTabletWayBillCustomerOrder\\WbGridView";

        public v_WayBillCustomerOrder wb_focused_row => WbGridView.GetFocusedRow() is NotLoadedObject ? null : WbGridView.GetFocusedRow() as v_WayBillCustomerOrder;

        private UserAccess user_access { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private int prev_focused_id = 0;
        private int prev_top_row_index = 0;
        private int prev_rowHandle = 0;
        private int? _find_id { get; set; }
        private bool _restore = false;

        public ucTabletWayBillCustomerOrder()
        {
            InitializeComponent();

            windowsUIButtonPanel.BackColor = new Color();
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

            if (XtraMessageBox.Show($"Ви дійсно бажаєте видалити замовлення від клієнта {wb_focused_row.Num}?", "Видалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                using (var _db = new BaseEntities())
                {
                    var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId && (w.SessionId == null || w.SessionId == UserSession.SessionId) && w.Checked == 0);
                    if (wb != null)
                    {
                        _db.DeleteWhere<WaybillList>(w => w.WbillId == wb_focused_row.WbillId && (w.SessionId == null || w.SessionId == UserSession.SessionId) && w.Checked == 0);
                    }
                    else
                    {
                        XtraMessageBox.Show(Resources.deadlock);
                    }
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

            PrintDoc.Show(wb_focused_row.Id, w_type, new BaseEntities());
        }
        public void ExportToExcel()
        {
            IHelper.ExportToXlsx(WBGridControl);
        }

        public void FindItem(Guid id, DateTime on_date)
        {
            _find_id = new BaseEntities().WaybillList.FirstOrDefault(w => w.Id == id).WbillId;
            WbGridView.ClearColumnsFilter();
            WbGridView.ClearFindFilter();

            GetData();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void WayBillInSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            BaseEntities objectContext = new BaseEntities();
            var ka = KagentList.GetSelectedDataRow() as Kontragent;
            var tt = objectContext.EmployeeKagent.Where(w => w.EmployeeId == DBHelper.CurrentUser.KaId && w.Kagent1.OpenStoreAreaId != null).Select(s => s.KaId).ToList();
            var list = objectContext.v_WayBillCustomerOrder.Where(w => w.WType == w_type && w.OnDate > wbStartDate.DateTime && w.OnDate <= wbEndDate.DateTime && (w.KaId == ka.KaId || ka.KaId == 0) && w.WorkerId == DBHelper.CurrentUser.KaId)
                .Join(objectContext.EmployeeKagent.Where(eww => eww.EmployeeId == DBHelper.CurrentUser.KaId), wb => wb.KaId, ek => ek.KaId, (wb, ek) => wb);
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
                var tt = new BaseEntities().EmployeeKagent.Where(w => w.EmployeeId == DBHelper.CurrentUser.KaId).Select(s =>  new { s.Kagent1.KaId, s.Kagent1.Name }).ToList();
                if (tt.Any())
                {
                    KagentList.Properties.DataSource = new List<Kontragent>() { new Kontragent { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().EmployeeKagent.Where(w => w.EmployeeId == DBHelper.CurrentUser.KaId).Select(s => new Kontragent { KaId = s.Kagent1.KaId, Name = s.Kagent1.Name }).ToList()).ToList();
                    KagentList.EditValue = 0;
                
                }
               
                KagentList.EditValue = 0;

                user_access = new BaseEntities().UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);

             
                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());
                WbGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
                WaybillDetGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);


                repositoryItemLookUpEdit3.DataSource = DBHelper.PayTypes;
                repositoryItemLookUpEdit5.DataSource = DBHelper.EnterpriseList;

                PeriodComboBoxEdit.SelectedIndex = 1;
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

        private void SetWBEditorBarBtn()
        {
            xtraTabControl2_SelectedPageChanged(null, null);

            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            CopyItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;

            if (wb_focused_row == null)
            {
                return;
            }

            DeleteItemBtn.Enabled = (wb_focused_row != null && wb_focused_row.Checked == 0 && user_access.CanDelete == 1);
            ExecuteItemBtn.Enabled = (wb_focused_row != null && wb_focused_row.Checked == 0 && user_access.CanPost == 1);
            EditItemBtn.Enabled = (wb_focused_row != null && user_access.CanModify == 1 && wb_focused_row.Checked == 0);
            CopyItemBtn.Enabled = (wb_focused_row != null && user_access.CanModify == 1);
            PrintItemBtn.Enabled = (wb_focused_row != null);
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (wb_focused_row == null)
            {
                gridControl2.DataSource = null;
                ucRelDocGrid1.GetRelDoc(Guid.Empty);
                vGridControl1.DataSource = null;
                ucDocumentPaymentGrid.GetPaymentDoc(Guid.Empty);

                return;
            }

            switch (xtraTabControl2.SelectedTabPageIndex)
            {
                case 0:

                    gridControl2.DataSource = new BaseEntities().v_WayBillCustomerOrderDet.AsNoTracking().Where(w => w.WbillId == wb_focused_row.WbillId).OrderBy(o => o.Num).ToList();
                    break;

                case 1:
                    vGridControl1.DataSource = new BaseEntities().v_WayBillCustomerOrder.AsNoTracking().Where(w => w.WbillId == wb_focused_row.WbillId && w.WorkerId == DBHelper.CurrentUser.KaId).ToList();
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
            if (!EditItemBtn.Enabled || wb_focused_row == null)
            {
                return;
            }

            using (var _db = new BaseEntities())
            {
                var edit_v = ((LookUpEdit)sender).EditValue;
                if(edit_v == null)
                {
                    return;
                }

                var PTypeId = Convert.ToInt32(edit_v);

                var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);

                if (wb != null)
                {
                    wb.PTypeId = PTypeId;
                    _db.SaveChanges();
                }
            }

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
            if (!EditItemBtn.Enabled)
            {
                return;
            }

            using (var _db = new BaseEntities())
            {
                var EntId = Convert.ToInt32(((LookUpEdit)sender).EditValue);

                var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);
                if (wb != null)
                {
                    wb.EntId = EntId;
                    _db.SaveChanges();
                }
                else
                {
                    XtraMessageBox.Show(Resources.not_find_wb);
                }
            }

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
            using (var _db = new BaseEntities())
            {
                var s_date = ((DateEdit)sender).DateTime;

                var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);

                wb.ShipmentDate = s_date;
                _db.SaveChanges();
            }

            RefrechItemBtn.PerformClick();
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
                            new BaseEntities().ChangeWaybillKagent(new_id, wb_focused_row.WbillId);

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

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<Guid> doc_list = new List<Guid>();
            for (int i = 0; i < WbGridView.RowCount; i++)
            {
                var dr = WbGridView.GetRow(i) as v_WayBillCustomerOrder;
                if (dr != null)
                {
                    doc_list.Add(dr.Id);
                }
            }
            if (doc_list.Any())
            {
                IHelper.PrintSelectedWayBill(w_type, doc_list);
            }
        }

        private void ucDocumentFilterPanel_FilterChanged(object sender, EventArgs e)
        {
            GetData();
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

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbStartDate.ContainsFocus)
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

        private void KagentList_EditValueChanged(object sender, EventArgs e)
        {
            if (KagentList.ContainsFocus)
            {
                GetData();
            }
        }

        private void WaybillDetGridView_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == CustomSummaryProcess.Finalize && gridControl2.DataSource != null)
            {
                var def_m = DBHelper.MeasuresList.FirstOrDefault(w => w.Def == 1);

                GridSummaryItem item = e.Item as GridSummaryItem;

                if (item.FieldName == "Amount")
                {
                    var mat_list = gridControl2.DataSource as List<v_WayBillCustomerOrderDet>;
                    if (mat_list != null)
                    {
                        var amount_sum = mat_list.Where(w => w.MId == def_m.MId).Sum(s => s.Amount);

                        e.TotalValue = amount_sum.ToString() + " " + def_m.ShortName;
                    }
                }
            }
        }

        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {

            if (e.Button.Properties.VisibleIndex == 0)
            {
                PrintItem();
            }
            else if (e.Button.Properties.VisibleIndex == 1)
            {
                GetData();
            }
            else if (e.Button.Properties.VisibleIndex == 2)
            {
                IHelper.ExportToXlsx(WBGridControl);
            }
        }
    }
}
