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
using SP_Sklad;
using SP_Sklad.WBForm;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SP_Sklad.Properties;
using System.Data.SqlClient;
using DevExpress.XtraGrid;
using SP_Sklad.FinanseForm;
using SP_Sklad.Common;
using SP_Sklad.Reports;
using DevExpress.XtraBars;
using SP_Sklad.WBDetForm;
using System.IO;
using DevExpress.Data;
using SP_Sklad.ViewsForm;
using System.Diagnostics;
using CheckboxIntegration.Client;
using CheckboxIntegration.Models;
using System.Drawing.Printing;
using DevExpress.Pdf;

namespace SP_Sklad.MainTabs
{
    public partial class TradeUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        int cur_wtype = 0;
        int show_null_balance = 1;
        BaseEntities _db { get; set; }
        v_GetRetailTree focused_tree_node { get; set; }
        public int? set_tree_node { get; set; }
        private UserSettingsRepository user_settings { get; set; }
        private List<KaTemplateList> ka_template_list { get; set; }
        private string _access_token { get; set; }

        private GetTradeWayBillList_Result wb_focused_row
        {
            get
            {
                return WbGridView.GetFocusedRow() as GetTradeWayBillList_Result;
            }
        }

        private GetWaybillDetIn_Result wb_det_focused_row
        {
            get
            {
                return WaybillDetGridView.GetFocusedRow() as GetWaybillDetIn_Result;
            }
        }


        public TradeUserControl()
        {
            InitializeComponent();
            ka_template_list = new List<KaTemplateList>();
        }

        private void DocumentsPanel_Load(object sender, EventArgs e)
        {
            wbContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            WbGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + "TradeUserControl\\WbGridView");
            PayDocGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + "TradeUserControl\\PayDocGridView");

            if (!DesignMode)
            {
                _db = new BaseEntities();
                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

                var login = new CheckboxClient().CashierSignin(new CashierSigninRequest { login = user_settings.CashierLoginCheckbox, password = user_settings.CashierPasswordCheckbox });

                _access_token = login.access_token;

                wbKagentList.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(DBHelper.TradingPoints.Select(s => new { s.KaId, s.Name }));
                PDKagentList.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(DBHelper.TradingPoints.Select(s => new { s.KaId, s.Name }));

                CashiersComboBox.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(DBHelper.Cashiers.Select(s => new { s.KaId, s.Name }));
                PDCashiersComboBox.Properties.DataSource = CashiersComboBox.Properties.DataSource;

                if (!user_settings.DefaultBuyer.HasValue)
                {
                    wbKagentList.EditValue = 0;
                    CashiersComboBox.EditValue = 0;
                    PDCashiersComboBox.EditValue = 0;
                    PDKagentList.EditValue = 0;
                }
                else
                {
                    wbKagentList.EditValue = user_settings.DefaultBuyer;
                    CashiersComboBox.EditValue = DBHelper.CurrentUser.KaId;
                    PDKagentList.EditValue = user_settings.DefaultBuyer;
                    PDCashiersComboBox.EditValue = DBHelper.CurrentUser.KaId;

                }
                wbKagentList.Enabled = DBHelper.is_main_cacher;
                CashiersComboBox.Enabled = DBHelper.is_main_cacher;
                PDKagentList.Enabled = DBHelper.is_main_cacher;
                PDCashiersComboBox.Enabled = DBHelper.is_main_cacher;

                wbStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                wbStatusList.EditValue = -1;

                wbStartDate.EditValue = DateTime.Now.Date;
                wbEndDate.EditValue = DateTime.Now.Date.SetEndDay();

                PDStartDate.EditValue = DateTime.Now.Date;
                PDEndDate.EditValue = DateTime.Now.Date.SetEndDay();

                PDSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                PDSatusList.EditValue = -1;

                DocsTreeList.DataSource = _db.v_GetRetailTree.Where(w => w.UserId == null || w.UserId == DBHelper.CurrentUser.UserId).OrderBy(o => o.Num).ToList();
                if (set_tree_node != null)
                {
                    DocsTreeList.FocusedNode = DocsTreeList.FindNodeByFieldValue("Id", set_tree_node);
                    set_tree_node = null;
                }


                DocsTreeList.ExpandAll();

                WbBalansGridColumn.Visible = (DBHelper.CurrentUser.ShowBalance == 1);
                WbBalansGridColumn.OptionsColumn.ShowInCustomizationForm = WbBalansGridColumn.Visible;

                WbSummPayGridColumn.Visible = WbBalansGridColumn.Visible;
                WbSummPayGridColumn.OptionsColumn.ShowInCustomizationForm = WbBalansGridColumn.Visible;

                gridColumn50.Caption = "Сума в нац. валюті, " + DBHelper.NationalCurrency.ShortName;
                gridColumn44.Caption = gridColumn50.Caption;


                WbGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
                PayDocGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
            }

            //    WbGridView.SaveLayoutToXml(@"D:\Program RES\AVK\t.xml");
        }

        void GetTradeWayBillList(string wtyp)
        {
            if (wbStatusList.EditValue == null || wbKagentList.EditValue == null || DocsTreeList.FocusedNode == null|| CashiersComboBox.EditValue == null)
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;

            int top_row = WbGridView.TopRowIndex;
            GetTradeWayBillListBS.DataSource = _db.GetTradeWayBillList(satrt_date, end_date, wtyp, (int)wbStatusList.EditValue, (int)wbKagentList.EditValue, show_null_balance, (int)CashiersComboBox.EditValue).OrderByDescending(o => o.OnDate).ToList();
            WbGridView.TopRowIndex = top_row;
        }

        void GetPayDocList(string doc_typ)
        {
            if (PDSatusList.EditValue == null || PDKagentList.EditValue == null || DocsTreeList.FocusedNode == null || PDCashiersComboBox.EditValue == null)
            {
                return;
            }

            var satrt_date = PDStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : PDStartDate.DateTime;
            var end_date = PDEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : PDEndDate.DateTime;

            int top_row = PayDocGridView.TopRowIndex;
            GetPayDocListBS.DataSource = _db.GetTradePayDocList(doc_typ, satrt_date.Date, end_date.Date.AddDays(1), (int)PDKagentList.EditValue, (int)PDSatusList.EditValue, -1, (int)PDCashiersComboBox.EditValue).OrderByDescending(o => o.OnDate).ToList();
            PayDocGridView.TopRowIndex = top_row;
        }

        private void DocsTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DocsTreeList.GetDataRecordByNode(e.Node) as v_GetRetailTree;

            NewItemBtn.Enabled = (focused_tree_node != null && focused_tree_node.CanInsert == 1);

            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            CopyItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;
            PrintReceiptBtn.Enabled = false;

            cur_wtype = focused_tree_node.WType != null ? focused_tree_node.WType.Value : 0;
            RefrechItemBtn.PerformClick();

            wbContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;

            if (focused_tree_node.FunId != null)
            {
                History.AddEntry(new HistoryEntity
                {
                    FunId = focused_tree_node.FunId.Value,
                    MainTabs = 3
                });

                if (DocsTreeList.ContainsFocus)
                {
                    Settings.Default.LastFunId = focused_tree_node.FunId.Value;
                }
            }
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {
            RefrechItemBtn.PerformClick();
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!(focused_tree_node != null && focused_tree_node.CanInsert == 1))
            {
                return;
            }


            switch (focused_tree_node.GType)
            {
                case 1:
                    if (cur_wtype == -25 ) 
                    {
                        using (var wb_in = new frmWBSales(cur_wtype, null))
                        {
                            wb_in.ShowDialog();
                        }

                    }

                    if (cur_wtype == 25) // Повернення від клієнта
                    {
                        using (var wb_re_in = new frmWBSalesReturn(cur_wtype, null))
                        {
                            wb_re_in.ShowDialog();
                        }
                    }

 
                    break;

                case 2:

                    int? w_type = focused_tree_node.WType == -26 ? -1 : 1;
                    using (var pd = new frmPayDoc(w_type, null) { _ka_id = (int)PDKagentList.EditValue == 0 ? null : (int?)PDKagentList.EditValue })
                    {
                        pd.ShowDialog();
                    }
                    break;

                case 5:
                   
                    break;

                case 8:
                    new frmKAgentAdjustment().ShowDialog();

                    break;

            }

            RefrechItemBtn.PerformClick();
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int gtype = (int)DocsTreeList.FocusedNode.GetValue("GType");

            using (var db = new BaseEntities())
            {
                switch (gtype)
                {
                    case 1:
                        DocEdit.WBEdit(wb_focused_row.WbillId, wb_focused_row.WType);
                        break;

                    case 2:
                        var pd_row = PayDocGridView.GetFocusedRow() as GetTradePayDocList_Result;
                        DocEdit.PDEdit(pd_row.PayDocId, pd_row.DocType);
                        break;
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DocsTreeList.FocusedNode == null) //баг з shortcut коли кнопка enabled = false
            {
                return;
            }

            int gtype = (int)DocsTreeList.FocusedNode.GetValue("GType");
            var dr = WbGridView.GetFocusedRow() as GetTradeWayBillList_Result;
            var pd_row = PayDocGridView.GetFocusedRow() as GetTradePayDocList_Result;

            //    var trans = _db.Database.BeginTransaction(IsolationLevel.RepeatableRead);
            using (var db = new BaseEntities())
            {

                try
                {
                    switch (gtype)
                    {
                        //      case 1: db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK) where WbillId = {0}", dr.WbillId).FirstOrDefault(); break;
                        case 4: db.Database.SqlQuery<PayDoc>("SELECT * from PayDoc WITH (UPDLOCK) where PayDocId = {0}", pd_row.PayDocId).FirstOrDefault(); break;
                            //	case 5: PriceList->LockRecord();  break;
                            //	case 6: ContractsList->LockRecord();  break;
                            //	case 7: TaxWBList->LockRecord();  break;
                    }
                    if (MessageBox.Show(Resources.delete_wb, "Відалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        switch (gtype)
                        {
                            case 1:
                                var wb = db.WaybillList.FirstOrDefault(w => w.WbillId == dr.WbillId && (w.SessionId == null || w.SessionId == UserSession.SessionId));
                                if (wb != null)
                                {
                                    db.WaybillList.Remove(wb);
                                }
                                else
                                {
                                    MessageBox.Show(Resources.deadlock);
                                }
                                break;

                            case 2:
                                var _pd = db.PayDoc.Find(pd_row.PayDocId);

                                if (_pd != null)
                                {
                                    db.PayDoc.Remove(_pd);
                                }
                                else
                                {
                                    MessageBox.Show(string.Format("Документ #{0} не знайдено", pd_row.DocNum));
                                }
                                break;
                        }
                        db.SaveChanges();
                    }
                }
                catch
                {
                    MessageBox.Show(Resources.deadlock);
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            bar1.Visible = true;
            switch (focused_tree_node.GType)
            {
                case 0:
                    bar1.Visible = false;
                    break;

                case 1:
                    if (focused_tree_node.Id == 129)
                    {
                        GetTradeWayBillList("-25,25");
                    }
                    else
                    {
                        GetTradeWayBillList(cur_wtype.ToString());
                    }
                    break;

                case 2:

                    if (cur_wtype == 26)
                    {
                        GetPayDocList("1");
                    }
                    else if (cur_wtype == -26)
                    {
                        GetPayDocList("-1");
                    }
                    else if (focused_tree_node.Id == 130)
                    {
                        var l = _db.v_GetRetailTree.Where(w => (w.UserId == null || w.UserId == DBHelper.CurrentUser.UserId) && w.PId == 130).ToList().Select(s=> Convert.ToString(s.WType) ).ToList();
                        if (l.Any())
                        {
                            GetPayDocList(string.Join(",", l).Replace("26","1"));
                        }
                    }
                    break;
            }

        }
        
        private void ExecuteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DocsTreeList.FocusedNode == null)
            {
                return;
            }

            var g_type = (int)DocsTreeList.FocusedNode.GetValue("GType");

            using (var db = new BaseEntities())
            {
                switch (g_type)
                {
                    case 1:
                        var dr = WbGridView.GetFocusedRow() as GetTradeWayBillList_Result;
                        if (dr == null)
                        {
                            return;
                        }

                        var wb = db.WaybillList.Find(dr.WbillId);
                        if (wb == null)
                        {
                            MessageBox.Show(Resources.not_find_wb);
                            return;
                        }
                        if (wb.SessionId != null)
                        {
                            MessageBox.Show(Resources.deadlock);
                            return;
                        }

                        if (wb.Checked == 1)
                        {
                            DBHelper.StornoOrder(db, dr.WbillId);
                        }
                        else
                        {
                            if (wb.WType == -1)
                            {
                                if (!DBHelper.CheckOrderedInSuppliers(dr.WbillId, db)) return;
                            }

                            DBHelper.ExecuteOrder(db, dr.WbillId);
                        }

                        break;

                    case 2:
                        var pd_row = PayDocGridView.GetFocusedRow() as GetTradePayDocList_Result;
                        var pd = _db.PayDoc.Find(pd_row.PayDocId);
                        if (pd != null)
                        {
                            if (pd.OnDate > _db.CommonParams.First().EndCalcPeriod)
                            {
                                pd.Checked = pd_row.Checked == 0 ? 1 : 0;
                                _db.SaveChanges();
                            }
                            else
                            {
                                MessageBox.Show("Період вже закритий. Змініть дату документа!", "Відміна/Проведення платіжного документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Документ #{0} не знайдено", pd_row.DocNum));
                        }
                        break;
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void PrintItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
                    var dr = WbGridView.GetFocusedRow() as GetTradeWayBillList_Result;
                    if (dr == null)
                    {
                        return;
                    }
                    PrintDoc.Show(dr.Id, dr.WType, _db);
                    break;

                case 2:
                    var pd = PayDocGridView.GetFocusedRow() as GetTradePayDocList_Result;
                    PrintDoc.Show(pd.Id, pd.DocType == -2 ? pd.DocType : pd.DocType * 3, _db);
                    break;

            }
        }

        private void PDStartDate_EditValueChanged(object sender, EventArgs e)
        {
            RefrechItemBtn.PerformClick();
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //user_settings.Reload();
            using (var frm = new frmMessageBox("Інформація", Resources.wb_copy))
            {
                if (!frm.user_settings.NotShowMessageCopyDocuments && frm.ShowDialog() != DialogResult.Yes)
                {
                    return;
                }
            }

            switch (focused_tree_node.GType)
            {
                case 1:
                    var dr = WbGridView.GetFocusedRow() as GetTradeWayBillList_Result;
                    var doc = DB.SkladBase().DocCopy(dr.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();

                    if (cur_wtype == -1 || cur_wtype == -16) //Відаткова , замовлення клиента 
                    {
                        using (var wb_in = new frmWayBillOut(cur_wtype, doc.out_wbill_id))
                        {
                            wb_in.is_new_record = true;
                            wb_in.ShowDialog();
                        }

                    }
                    if (cur_wtype == 1 || cur_wtype == 16)  //Прибткова накладна , замовлення постачальникам
                    {
                        using (var wb_in = new frmWayBillIn(cur_wtype, doc.out_wbill_id))
                        {
                            wb_in.is_new_record = true;
                            wb_in.ShowDialog();
                        }
                    }

                    if (cur_wtype == 6) // Повернення від клієнта
                    {
                        using (var wb_re_in = new frmWBReturnIn(cur_wtype, doc.out_wbill_id))
                        {
                            wb_re_in.ShowDialog();
                        }
                    }

                    break;

                case 4:
                    var pd = PayDocGridView.GetFocusedRow() as GetTradePayDocList_Result;
                    var p_doc = DB.SkladBase().DocCopy(pd.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();

                    int? w_type = focused_tree_node.WType != -2 ? focused_tree_node.WType / 3 : focused_tree_node.WType;
                    using (var pdf = new frmPayDoc(w_type, p_doc.out_wbill_id))
                    {
                        pdf.ShowDialog();
                    }
                    break;

            }

            RefrechItemBtn.PerformClick();
        }

        private void DocsPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            EditCheckBtn.Enabled = wb_focused_row != null && wb_focused_row.Checked == 0 && wb_focused_row.WType == -25;
        }

        private void WbGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                DocsPopupMenu.ShowPopup(p2);
            }
        }

        private void NewPayDocBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if ((wb_focused_row.SummInCurr - wb_focused_row.SummPay) <= 0)
            {
                MessageBox.Show("Документ вже оплачено!");
                return;
            }

            int? doc_type;
            if (new[] { 132 }.Any(a => a == focused_tree_node.Id))
            {
                doc_type = -1;
            }
            else if (new[] { 131 }.Any(a => a == focused_tree_node.Id))
            {
                doc_type = 1;
            }
            else
            {
                return;
            }

            var frm = new frmPayDoc(doc_type, null, wb_focused_row.SummInCurr)
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
                RefrechItemBtn.PerformClick();
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetRelDocList_Result row = new GetRelDocList_Result();

            if (gridView3.Focus())
            {
                row = gridView3.GetFocusedRow() as GetRelDocList_Result;
            }
            else if (gridView1.Focus())
            {
                row = gridView1.GetFocusedRow() as GetRelDocList_Result;
            }

            FindDoc.Find(row.Id, row.DocType, row.OnDate);
        }

        private void gridView3_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                BottomPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetRelDocList_Result row = new GetRelDocList_Result();

            if (gridView3.Focus())
            {
                row = gridView3.GetFocusedRow() as GetRelDocList_Result;
            }
            else if (gridView1.Focus())
            {
                row = gridView1.GetFocusedRow() as GetRelDocList_Result;
            }

            if (!string.IsNullOrEmpty(_access_token) && row.ReceiptId.HasValue && row.ReceiptId.Value != Guid.Empty)
            {
                IHelper.PrintReceiptPng(_access_token, row.ReceiptId.Value);
            }
            else
            {
                PrintDoc.Show(row.Id, row.DocType.Value, DB.SkladBase());
            }
        }

        private void WbGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var dr = e.Row as GetTradeWayBillList_Result;

            xtraTabControl2_SelectedPageChanged(sender, null);

            DeleteItemBtn.Enabled = (dr != null && dr.Checked == 0 && focused_tree_node.CanDelete == 1);
            ExecuteItemBtn.Enabled = (dr != null && dr.WType != 2 && dr.WType != -16 && dr.WType != 16 && focused_tree_node.CanPost == 1);
            EditItemBtn.Enabled = (dr != null && focused_tree_node.CanModify == 1 && (dr.Checked == 0 || dr.Checked == 1));
            CopyItemBtn.Enabled = (dr != null && focused_tree_node.CanModify == 1);
            PrintItemBtn.Enabled = (dr != null);
            PrintReceiptBtn.Enabled = PrintItemBtn.Enabled;
        }

        private void PayDocGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var dr = e.Row as GetTradePayDocList_Result;
            PayDocListInfoBS.DataSource = dr;

            if (dr != null)
            {
                RelPayDocGridControl.DataSource = _db.GetRelDocList(dr.Id).OrderBy(o => o.OnDate).ToList();
            }

            var tree_row = DocsTreeList.GetDataRecordByNode(DocsTreeList.FocusedNode) as v_GetRetailTree;

            bool isModify = (dr != null && (DBHelper.CashDesks.Any(a => a.CashId == dr.CashId) || dr.CashId == null));

            DeleteItemBtn.Enabled = (dr != null && dr.Checked == 0 && tree_row.CanDelete == 1);
            ExecuteItemBtn.Enabled = (dr != null && tree_row.CanPost == 1 && isModify);
            EditItemBtn.Enabled = (dr != null && tree_row.CanModify == 1 && isModify);
            CopyItemBtn.Enabled = EditItemBtn.Enabled;
            PrintItemBtn.Enabled = (dr != null);
            PrintReceiptBtn.Enabled = PrintItemBtn.Enabled;
        }

       
        private class KaTemplateList
        {
            public bool Check { get; set; }
            public int KaId { get; set; }
            public string KaName { get; set; }
        }

        private void gridView2_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                WbDetPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(wb_det_focused_row.MatId);
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.ShowTurnMaterial(wb_det_focused_row.MatId);
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(wb_det_focused_row.MatId, DB.SkladBase());
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IHelper.FindMatInWH(wb_det_focused_row.MatId))
            {
                MessageBox.Show(string.Format("На даний час товар <{0}> на складі вдсутній!", wb_det_focused_row.MatName));
            }
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IHelper.FindMatInDir(wb_det_focused_row.MatId))
            {
                MessageBox.Show(string.Format("Товар <{0}> в довіднику вдсутній, можливо він перебуває в архіві!", wb_det_focused_row.MatName));
            }
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new frmWayBillDetIn(DB.SkladBase(), wb_det_focused_row.PosId, DB.SkladBase().WaybillList.Find(wb_det_focused_row.WbillId));
            frm.OkButton.Visible = false;
            frm.ShowDialog();
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (wb_focused_row.WType == 6 && wb_focused_row.Checked == 1)
            {
                var _wbill_ids = new List<int?>();
                using (var db = DB.SkladBase())
                {
                    var wb_det = db.WaybillDet.Where(w => w.WbillId == wb_focused_row.WbillId && w.WMatTurn.Any()).ToList();
                    if (wb_det.Any())
                    {
                        foreach (var wid in wb_det.GroupBy(g => g.WId).Select(s => s.Key).ToList())
                        {
                            var wb = db.WaybillList.Add(new WaybillList()
                               {
                                   Id = Guid.NewGuid(),
                                   WType = -5,
                                   DefNum = 1,
                                   OnDate = DBHelper.ServerDateTime(),
                                   Num = new BaseEntities().GetDocNum("wb_write_off").FirstOrDefault(),
                                   CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                                   OnValue = 1,
                                   PersonId = DBHelper.CurrentUser.KaId,
                                   WaybillMove = new WaybillMove { SourceWid = wid.Value },
                                   Nds = 0,
                                   UpdatedBy = DBHelper.CurrentUser.UserId,
                                   EntId = DBHelper.Enterprise.KaId
                               });

                            db.SaveChanges();
                            _wbill_ids.Add(wb.WbillId);
                            db.Commission.Add(new Commission { WbillId = wb.WbillId, KaId = DBHelper.CurrentUser.KaId });

                            foreach (var det_item in wb_det.Where(w => w.WId == wid))
                            {
                                var _wbd = db.WaybillDet.Add(new WaybillDet()
                                {
                                    WbillId = wb.WbillId,
                                    Num = wb.WaybillDet.Count() + 1,
                                    Amount = det_item.Amount,
                                    OnValue = det_item.OnValue,
                                    WId = det_item.WId,
                                    Nds = wb.Nds,
                                    CurrId = wb.CurrId,
                                    OnDate = wb.OnDate,
                                    MatId = det_item.MatId,
                                    Price = det_item.Price,
                                    BasePrice = det_item.Price
                                });
                                db.SaveChanges();

                                var pos_in = db.GetPosIn(wb.OnDate, _wbd.MatId, _wbd.WId, 0, DBHelper.CurrentUser.UserId).Where(w => w.CurRemain >= _wbd.Amount && w.PosId == det_item.PosId).OrderBy(o=> o.OnDate).FirstOrDefault();
                                if (pos_in != null && db.UserAccessWh.Any(a => a.UserId == DBHelper.CurrentUser.UserId && a.WId == _wbd.WId && a.UseReceived))
                                {
                                    db.WMatTurn.Add(new WMatTurn
                                    {
                                        PosId = pos_in.PosId,
                                        WId = _wbd.WId.Value,
                                        MatId = _wbd.MatId,
                                        OnDate = _wbd.OnDate.Value,
                                        TurnType = 2,
                                        Amount = _wbd.Amount,
                                        SourceId = _wbd.PosId
                                    });
                                }
                            }

                        }

                    }

                    db.SaveChanges();
                }

                foreach (var item in _wbill_ids)
                {
                    using (var frm = new frmWBWriteOff(item))
                    {
                        frm.is_new_record = true;
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            var dr = WbGridView.GetFocusedRow() as GetTradeWayBillList_Result;
          
            if (dr == null)
            {
                gridControl2.DataSource = null;
                gridControl3.DataSource = null;
                WayBillListInfoBS.DataSource = null;
                gridControl10.DataSource = null;

                return;
            }

            switch (xtraTabControl2.SelectedTabPageIndex)
            {
                case 0:
                    gridColumn37.Caption = "Сума в валюті, " + dr.CurrName;
                    gridControl2.DataSource = _db.GetWaybillDetIn(dr.WbillId).ToList().OrderBy(o => o.Num);
                    break;

                case 1:
                    WayBillListInfoBS.DataSource = dr;
                    break;

                case 2:
                    gridControl3.DataSource = _db.GetRelDocList(dr.Id).OrderBy(o => o.OnDate).ToList();
                    break;
                case 3:
                    gridControl10.DataSource = _db.DocRels.Where(w=> w.OriginatorId == dr.Id)
                        .Join(_db.v_PayDoc, drel => drel.RelOriginatorId, pd => pd.Id, (drel, pd) => pd).OrderBy(o => o.OnDate).ToList();
                    break;
            }
          
        }

        private void PayDocGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                PayDocsPopupMenu.ShowPopup(p2);
            }
        }

        private void DocsPopupMenu_Popup(object sender, EventArgs e)
        {
            if (wb_focused_row == null)
            {
                return;
            }

            barButtonItem11.Enabled = wb_focused_row.WType == 6;
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
                    IHelper.ExportToXlsx(WBGridControl);
                    break;

                case 4:
                    IHelper.ExportToXlsx(PayDocGridControl);
                    break;
            }
        }

        public void SaveGridLayouts()
        {
            WbGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + "\\TradeUserControl\\WbGridView");
            PayDocGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + "\\TradeUserControl\\PayDocGridView");
        }

 
        private void wbKagentList_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                wbKagentList.EditValue = IHelper.ShowDirectList(wbKagentList.EditValue, 1);
            }
        }

        private void PDKagentList_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                PDKagentList.EditValue = IHelper.ShowDirectList(PDKagentList.EditValue, 1);
            }
        }

 
        private void WaybillDetGridView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {

            if (e.SummaryProcess == CustomSummaryProcess.Finalize && gridControl2.DataSource != null)
            {
                var def_m = DBHelper.MeasuresList.FirstOrDefault(w => w.Def == 1);

                GridSummaryItem item = e.Item as GridSummaryItem;

                if (item.FieldName == "Amount")
                {
                    var mat_list = gridControl2.DataSource as IOrderedEnumerable<GetWaybillDetIn_Result>;
                    var amount_sum = mat_list.Where(w => w.MId == def_m.MId).Sum(s => s.Amount);

                    e.TotalValue = amount_sum.ToString() + " " + def_m.ShortName;//Math.Round(amount_sum + ext_sum, 2);
                }
            }
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (wb_focused_row.ReceiptId.HasValue && wb_focused_row.ReceiptId.Value != Guid.Empty)
            {
                IHelper.PrintReceiptPng(_access_token, wb_focused_row.ReceiptId.Value);
            }
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            int gtype = (int)DocsTreeList.FocusedNode.GetValue("GType");

            using (var db = new BaseEntities())
            {
                switch (gtype)
                {
                    case 1:
                        using (var frm = new frmCashboxWBOut(_access_token, wb_focused_row.WbillId))
                        {
                            frm.ShowDialog();
                        }
                        break;
                }
            }

            RefrechItemBtn.PerformClick();
           
        }
    }
}
