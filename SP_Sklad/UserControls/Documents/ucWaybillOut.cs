﻿using System;
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
using SkladEngine.ExecuteDoc;

namespace SP_Sklad.UserControls
{
    public partial class ucWaybillOut : DevExpress.XtraEditors.XtraUserControl
    {
        int w_type = -1;
        private int fun_id = 23;
        private string reg_layout_path = "ucWaybillOut\\WbInGridView";

        public v_WayBillOut wb_focused_row => WbGridView.GetFocusedRow() is NotLoadedObject ? null : WbGridView.GetFocusedRow() as v_WayBillOut;

        private UserAccess user_access { get; set; }
        private UserSettingsRepository user_settings { get; set; }
        private FocusGridRow fgr;

        public ucWaybillOut()
        {
            InitializeComponent();
            WBGridControl.DataSource = null;
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
            using (var wb_in = new frmWayBillOut(w_type, null))
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

            using (var wb_in = new frmWayBillOut(w_type, doc.out_wbill_id))
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

            if (XtraMessageBox.Show($"Ви дійсно бажаєте видалити видаткову накладну {wb_focused_row.Num}?", "Видалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                using (var db = new BaseEntities())
                {
                    var wb = db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId && (w.SessionId == null || w.SessionId == UserSession.SessionId) && w.Checked == 0);

                    if (!DBHelper.CheckExpedition(wb_focused_row.WbillId, db))
                    {
                        return;
                    }

                    if (wb != null)
                    {
                        db.WaybillList.Remove(wb);

                        db.SaveChanges();
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
                    if (wb.WType == -1)
                    {

                        if (!DBHelper.CheckExpedition(wb_focused_row.WbillId, db)) return;
                    }

                    DBHelper.StornoOrder(db, wb_focused_row.WbillId);
                }
                else
                {
                    if (wb.WType == -1)
                    {
                        if (!DBHelper.CheckOrderedInSuppliers(wb_focused_row.WbillId, db)) return;
                    }

                    DBHelper.ExecuteOrder(db, wb_focused_row.WbillId);
                }

                /*var wb = db.WaybillList.Find(wb_focused_row.WbillId);
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
                }*/
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

        private void WayBillInSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            BaseEntities objectContext = new BaseEntities();
            var list = objectContext.v_WayBillOut.Join(objectContext.EnterpriseWorker.Where(eww => eww.WorkerId == DBHelper.CurrentUser.KaId), pd => pd.EntId, ew => ew.EnterpriseId, (pd, ew) => pd)
                .Where(w => w.WType == w_type && w.OnDate > ucDocumentFilterPanel.StartDate && w.OnDate <= ucDocumentFilterPanel.EndDate && (w.Checked == ucDocumentFilterPanel.StatusId || ucDocumentFilterPanel.StatusId == -1) && (w.KaId == ucDocumentFilterPanel.KagentId || ucDocumentFilterPanel.KagentId == 0) );
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
                user_access = new BaseEntities().UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);

                WbBalansGridColumn.Visible = (DBHelper.CurrentUser.ShowBalance == 1);
                WbBalansGridColumn.OptionsColumn.ShowInCustomizationForm = WbBalansGridColumn.Visible;

                WbSummPayGridColumn.Visible = WbBalansGridColumn.Visible;
                WbSummPayGridColumn.OptionsColumn.ShowInCustomizationForm = WbBalansGridColumn.Visible;

                gridColumn44.Caption = "Сума в нац. валюті, " + DBHelper.NationalCurrency.ShortName;

                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());
                WbGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
             
                repositoryItemLookUpEdit3.DataSource = DBHelper.PayTypes;
                repositoryItemLookUpEdit5.DataSource = DBHelper.EnterpriseList;

                gridColumn111.Caption = $"К-сть всього, {DBHelper.MeasuresList?.FirstOrDefault(w => w.Def == 1)?.ShortName}";

                GetData();
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
            if (wb_focused_row == null)
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
            ExecuteItemBtn.Enabled = (wb_focused_row != null && wb_focused_row.WType != 2 && wb_focused_row.WType != -16 && wb_focused_row.WType != 16 && user_access.CanPost == 1);
            EditItemBtn.Enabled = (wb_focused_row != null && user_access.CanModify == 1 && (wb_focused_row.Checked == 0 || (wb_focused_row.Checked == 1 && wb_focused_row.WType != -16 && wb_focused_row.WType != 16 )));
            CopyItemBtn.Enabled = (wb_focused_row != null && user_access.CanModify == 1);
            PrintItemBtn.Enabled = (wb_focused_row != null);

            if (wb_focused_row?.Checked == 0) ExecuteItemBtn.ImageIndex = 23;
            else ExecuteItemBtn.ImageIndex = 6;
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (wb_focused_row == null)
            {
                ucWayBillOutDet.GetDate(0);
                ucRelDocGrid1.GetRelDoc(Guid.Empty);
                vGridControl1.DataSource = null;
                ucDocumentPaymentGrid.GetPaymentDoc(Guid.Empty);

                return;
            }

            switch (xtraTabControl2.SelectedTabPageIndex)
            {
                case 0:
                    ucWayBillOutDet.GetDate(wb_focused_row.WbillId);
                    break;

                case 1:
                    vGridControl1.DataSource = new BaseEntities().v_WayBillOut.AsNoTracking().Where(w => w.WbillId == wb_focused_row.WbillId).ToList();
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
            if (!EditItemBtn.Enabled)
            {
                return;
            }

            var PTypeId = Convert.ToInt32(((LookUpEdit)sender).EditValue);
            using (var db = new BaseEntities())
            {
                var wb = db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);

                wb.PTypeId = PTypeId;
                db.SaveChanges();
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

            var EntId = Convert.ToInt32(((LookUpEdit)sender).EditValue);
            using (var db = new BaseEntities())
            {
                var wb = db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);

                wb.EntId = EntId;
                db.SaveChanges();
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

            var s_date = ((DateEdit)sender).DateTime;
            using (var db = new BaseEntities())
            {
                var wb = db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);

                wb.ShipmentDate = s_date;
                db.SaveChanges();
            }
            RefrechItemBtn.PerformClick();
        }

        private void ChangeWaybillKagentBtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
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
                var dr = WbGridView.GetRow(i) as v_WayBillOut;
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

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var db = new BaseEntities())
            {
                for (int i = 0; i < WbGridView.RowCount; i++)
                {
                    var dr = WbGridView.GetRow(i) as v_WayBillOut;

                    if (dr != null)
                    {
                        if (dr.WType == -1)
                        {
                            var data_report = PrintDoc.WayBillOutReport(dr.Id, db);
                            IHelper.Print(data_report, TemlateList.wb_out, false);
                        }

                        if (dr.WType == -16)
                        {
                            var ord_out = PrintDoc.WayBillOrderedOutReport(dr.Id, db);
                            IHelper.Print(ord_out, TemlateList.ord_out, false);
                        }
                    }
                }
            }

            MessageBox.Show("Експортовано " + WbGridView.RowCount.ToString() + " документів !");

        }

        private void ucDocumentFilterPanel1_FilterChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void MoveToStoreWarehouseBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(wb_focused_row == null)
            {
                return;
            }

            var new_doc = new ExecuteWayBill().MoveToStoreWarehouse(wb_focused_row.WbillId, false);
            if (new_doc.HasValue)
            {
                using (var wb_in = new frmWayBillIn(1, new_doc))
                {
                    wb_in.is_new_record = true;
                    wb_in.ShowDialog();
                }
            }
            else
            {
                XtraMessageBox.Show("Прибуткова накладана вже створена!");
            }

         
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }
    }
}
