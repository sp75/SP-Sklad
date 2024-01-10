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
using DevExpress.XtraEditors;
using System.Data.Entity.SqlServer;
using System.Data.Entity;

namespace SP_Sklad.MainTabs
{
    public partial class DocsUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        int cur_wtype = 0;
        int show_null_balance = 1;
        BaseEntities _db { get; set; }
        v_GetDocsTree focused_tree_node { get; set; }
        public int? set_tree_node { get; set; }
        private UserSettingsRepository user_settings { get; set; }
        private List<KaTemplateList> ka_template_list { get; set; }

        private GetWayBillList_Result wb_focused_row
        {
            get
            {
                return WbGridView.GetFocusedRow() as GetWayBillList_Result;
            }
        }

        private GetWaybillDetIn_Result wb_det_focused_row
        {
            get
            {
                return WaybillDetGridView.GetFocusedRow() as GetWaybillDetIn_Result;
            }
        }

        private v_PriceList pl_focused_row
        {
            get
            {
                return PriceListGridView.GetFocusedRow() as v_PriceList;
            }
        }

        public DocsUserControl()
        {
            InitializeComponent();
            ka_template_list = new List<KaTemplateList>();
        }

        private void DocumentsPanel_Load(object sender, EventArgs e)
        {
            wbContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            WbGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + "DocsUserControl\\WbGridView");
            PayDocGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + "DocsUserControl\\PayDocGridView");

            if (!DesignMode)
            {
                _db = new BaseEntities();

                wbKagentList.Properties.DataSource = DBHelper.KagentsList;//new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(_db.Kagent.Select(s => new { s.KaId, s.Name }));
                wbKagentList.EditValue = 0;

                wbStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                wbStatusList.EditValue = -1;

                kaaStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                kaaStatusList.EditValue = -1;

                wbStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                wbEndDate.EditValue = DateTime.Now.Date.SetEndDay();

                PDStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                PDEndDate.EditValue = DateTime.Now.Date.SetEndDay();

                kaaStartDate.EditValue = DateTime.Now.Date.FirstDayOfMonth();
                kaaEndDate.EditValue = DateTime.Now.Date.SetEndDay();


                PDKagentList.Properties.DataSource = DBHelper.KagentsList;// new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(_db.Kagent.Select(s => new { s.KaId, s.Name }));
                PDKagentList.EditValue = 0;

                PDSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                PDSatusList.EditValue = -1;

                DocsTreeList.DataSource = _db.v_GetDocsTree.Where(w => w.UserId == null || w.UserId == DBHelper.CurrentUser.UserId).OrderBy(o => o.Num).ToList();
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

                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);
                WbGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
                PayDocGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

                repositoryItemLookUpEdit3.DataSource = DBHelper.PayTypes;
                repositoryItemLookUpEdit5.DataSource = DBHelper.EnterpriseList;

                gridColumn111.Caption = $"К-сть всього, {DBHelper.MeasuresList?.FirstOrDefault(w => w.Def == 1)?.ShortName}";
            }

        }

        void GetWayBillList(string wtyp)
        {
            if (wbStatusList.EditValue == null || wbKagentList.EditValue == null || DocsTreeList.FocusedNode == null)
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;

            int top_row = WbGridView.TopRowIndex;
            var focused_id = wb_focused_row?.Id;

            GetWayBillListBS.DataSource = _db.GetWayBillList(satrt_date, end_date, wtyp, (int)wbStatusList.EditValue, (int)wbKagentList.EditValue, show_null_balance, "*", DBHelper.CurrentUser.KaId).OrderByDescending(o => o.OnDate).ToList();
            WbGridView.TopRowIndex = top_row;

            int rowHandle = WbGridView.LocateByValue("Id", focused_id);
            if (rowHandle != GridControl.InvalidRowHandle)
            {
                WbGridView.FocusedRowHandle = rowHandle;
            }
        }

        void GetPayDocList(string doc_typ)
        {
            if (PDSatusList.EditValue == null || PDKagentList.EditValue == null || DocsTreeList.FocusedNode == null)
            {
                return;
            }

            var satrt_date = PDStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : PDStartDate.DateTime;
            var end_date = PDEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : PDEndDate.DateTime;

            int top_row = PayDocGridView.TopRowIndex;
            GetPayDocListBS.DataSource = _db.GetPayDocList(doc_typ, satrt_date.Date, end_date.Date.AddDays(1), (int)PDKagentList.EditValue, (int)PDSatusList.EditValue, -1, DBHelper.CurrentUser.KaId).OrderByDescending(o => o.OnDate).ToList();
            PayDocGridView.TopRowIndex = top_row;
        }

        void GetKAgentAdjustment(string wtyp)
        {
            if (kaaStatusList.EditValue == null || DocsTreeList.FocusedNode == null)
            {
                return;
            }

            var satrt_date = kaaStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : kaaStartDate.DateTime;
            var end_date = kaaEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : kaaEndDate.DateTime;
            int status = (int)kaaStatusList.EditValue;

            int top_row = KAgentAdjustmentGridView.TopRowIndex;
            using (var db = new BaseEntities())
            {
                KAgentAdjustmentBS.DataSource = db.v_KAgentAdjustment.Where(w => w.OnDate >= satrt_date && w.OnDate <= end_date && (status == -1 || w.Checked == status)).OrderByDescending(o => o.OnDate).ToList();
            }
            KAgentAdjustmentGridView.TopRowIndex = top_row;
        }

        private void DocsTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DocsTreeList.GetDataRecordByNode(e.Node) as v_GetDocsTree;
            if (focused_tree_node == null)
            {
                return;
            }

            NewItemBtn.Enabled = (focused_tree_node != null && focused_tree_node.CanInsert == 1);

            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            CopyItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;

            cur_wtype = focused_tree_node.WType != null ? focused_tree_node.WType.Value : 0;

               RefrechItemBtn.PerformClick();

            if (focused_tree_node.FunId == 21) //Прибуткова накладна
            {
                wbContentTab.SelectedTabPageIndex = 14;
            }
            else if (focused_tree_node.FunId == 65) //Замовлення постачальникам
            {
                wbContentTab.SelectedTabPageIndex = 15;
            }
            else if (focused_tree_node.FunId == 23) //Видаткова постачальникам
            {
                wbContentTab.SelectedTabPageIndex = 16;
            }
            else if (focused_tree_node.FunId == 30) //Рахунок
            {
                wbContentTab.SelectedTabPageIndex = 17;
            }
            else if (focused_tree_node.FunId == 64) //Замовлення від клієнта
            {
                wbContentTab.SelectedTabPageIndex = 18;
            }
            else if (focused_tree_node.FunId == 94) //Акт послуг
            {
                wbContentTab.SelectedTabPageIndex = 19;
            }
            else if (focused_tree_node.FunId == 42) //Повернення від клієнта
            {
                wbContentTab.SelectedTabPageIndex = 20;
            }
            else if (focused_tree_node.FunId == 43) //Повернення постачальнику
            {
                wbContentTab.SelectedTabPageIndex = 21;
            }
            else
            {
                wbContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;
            }

            if (focused_tree_node.FunId != null)
            {
                History.AddEntry(new HistoryEntity
                {
                    FunId = focused_tree_node.FunId.Value,
                    MainTabs = 0
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
                    if (cur_wtype == 1) //Прибткова накладна 
                    {
                        wayBillInUserControl.NewItem();
                    }

                    if (cur_wtype == -1) //Відаткова накладна 
                    {
                        ucWaybillOut.NewItem();
                    }

                    if (cur_wtype == 2) //рахунок
                    {
                        ucInvoices.NewItem();
                    }

                    if (cur_wtype == 16) //Замовлення постачальнику
                    {
                        ucWBOrdersOut.NewItem();
                    }

                    if (cur_wtype == -16) //Замовлення від клієнта
                    {
                        ucWBOrdersIn.NewItem();
                    }

                    if (cur_wtype == 6) // Повернення від клієнта
                    {
                        ucWayBillReturnСustomers.NewItem();
                    }

                    if (cur_wtype == -6) //Повернення постачальнику
                    {
                        ucWaybillReturnSuppliers.NewItem();
                    }

                    if (cur_wtype == 29)  //Акти наданих послуг
                    {
                        ucServicesIn.NewItem();
                    }


                    break;

                case 4:

                    int? w_type = focused_tree_node.WType != -2 ? focused_tree_node.WType / 3 : focused_tree_node.WType;
                    using (var pd = new frmPayDoc(w_type, null) { _ka_id = (int)PDKagentList.EditValue == 0 ? null : (int?)PDKagentList.EditValue })
                    {
                        pd.ShowDialog();
                    }
                    break;

                case 5:
                    new frmPriceList().ShowDialog();
                    break;

                /*           case 6: frmContr = new  TfrmContr(Application);
                                   frmContr->CONTRACTS->Open();
                                   frmContr->CONTRACTS->Append();
                                   if(DocsTreeDataID->Value == 47) frmContr->CONTRACTSDOCTYPE->Value = -1;
                                   if(DocsTreeDataID->Value == 46) frmContr->CONTRACTSDOCTYPE->Value = 1;
                                   frmContr->CONTRACTS->Post();
                                   frmContr->CONTRACTS->Edit();

                                   frmContr->CONTRPARAMS->Append();
                                   frmContr->CONTRPARAMS->Post();
                                   frmContr->CONTRRESULTS->Append();

                                   frmContr->ShowModal() ;
                                   delete frmContr;
                                   break;

                           case 7: frmTaxWB = new  TfrmTaxWB(Application);
                                   frmTaxWB->TaxWB->Open();
                                   frmTaxWB->TaxWB->Append();
                                   frmTaxWB->TaxWB->Post();
                                   frmTaxWB->TaxWB->Edit();
                                   frmTaxWB->ShowModal() ;
                                   delete frmTaxWB;
                                   break;*/
                case 8:
                    new frmKAgentAdjustment().ShowDialog();

                    break;

                case 10:
                    ucBankStatements.NewItem();
                    break;

                case 11:
                    ucProjectManagement.NewItem();
                    break;

                case 12:
                    settingMaterialPricesUserControl1.NewItem();
                    break;

                case 13:
                    expeditionUserControl1.NewItem();
                    break;

            }

            RefrechItemBtn.PerformClick();
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            using (var db = new BaseEntities())
            {
                switch (focused_tree_node.GType)
                {
                    case 1:
                        if (focused_tree_node.WType == 1)
                        {
                            wayBillInUserControl.EditItem();
                        }
                        else if (focused_tree_node.WType == 16)
                        {
                            ucWBOrdersOut.EditItem();
                        }
                        else if (focused_tree_node.WType == -1)
                        {
                            ucWaybillOut.EditItem();
                        }
                        else if (focused_tree_node.WType == 2)
                        {
                            ucInvoices.EditItem();
                        }
                        else if (focused_tree_node.WType == -16)
                        {
                            ucWBOrdersIn.EditItem();
                        }
                        else if (focused_tree_node.WType == 29)
                        {
                            ucServicesIn.EditItem();
                        }
                        else if(focused_tree_node.WType == 6)
                        {
                            ucWayBillReturnСustomers.EditItem();
                        }
                        else if(focused_tree_node.WType == -6)
                        {
                            ucWaybillReturnSuppliers.EditItem();
                        }
                        else
                        {
                            DocEdit.WBEdit(wb_focused_row.WbillId, wb_focused_row.WType);
                        }
                        break;

                    case 4:
                        var pd_row = PayDocGridView.GetFocusedRow() as GetPayDocList_Result;
                        DocEdit.PDEdit(pd_row.PayDocId, pd_row.DocType);
                        break;
                    case 5:
                        var pl_row = PriceListGridView.GetFocusedRow() as v_PriceList;
                        using (var pl_frm = new frmPriceList(pl_row.PlId))
                        {
                            pl_frm.ShowDialog();
                        }

                        break;

                    case 8:
                        var kaa_row = KAgentAdjustmentGridView.GetFocusedRow() as v_KAgentAdjustment;
                        using (var kaa_frm = new frmKAgentAdjustment(kaa_row.Id))
                        {
                            kaa_frm.ShowDialog();
                        }
                        break;

                    case 10:
                        ucBankStatements.EditItem();
                        break;

                    case 11:
                        ucProjectManagement.EditItem();
                        break;

                    case 12:
                        using (var smp_frm = new frmSettingMaterialPrices(settingMaterialPricesUserControl1.row_smp.Id))
                        {
                            smp_frm.ShowDialog();
                        }
                        break;

                    case 13:
                        using (var ex_frm = new frmExpedition(expeditionUserControl1.row_exp.Id))
                        {
                            ex_frm.ShowDialog();
                        }
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
            if (focused_tree_node == null)
            {
                return;
            }

            if (focused_tree_node.WType == 1)
            {
                wayBillInUserControl.DeleteItem();
            }
            else if (focused_tree_node.WType == 16)
            {
                ucWBOrdersOut.DeleteItem();
            }
            else if (focused_tree_node.WType == -1)
            {
                ucWaybillOut.DeleteItem();
            }
            else if (focused_tree_node.WType == 2)
            {
                ucInvoices.DeleteItem();
            }
            else if (focused_tree_node.WType == -16)
            {
                ucWBOrdersIn.DeleteItem();
            }
            else if (focused_tree_node.WType == 29)
            {
                ucServicesIn.DeleteItem();
            }
            else if (focused_tree_node.WType == 6)
            {
                ucWayBillReturnСustomers.DeleteItem();
            }
            else if (focused_tree_node.WType == -6)
            {
                ucWaybillReturnSuppliers.DeleteItem();
            }
            else
            {
                int gtype = focused_tree_node.GType.Value;

                var pd_row = PayDocGridView.GetFocusedRow() as GetPayDocList_Result;
                var adj_row = KAgentAdjustmentGridView.GetFocusedRow() as v_KAgentAdjustment;

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


                                    var wb = db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId && (w.SessionId == null || w.SessionId == UserSession.SessionId));
                                    if (wb != null)
                                    {
                                        db.WaybillList.Remove(wb);
                                    }
                                    else
                                    {
                                        MessageBox.Show(Resources.deadlock);
                                    }

                                    break;

                                case 4:
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

                                case 5:
                                    db.DeleteWhere<PriceList>(w => w.PlId == pl_focused_row.PlId);
                                    break;

                                //	   case 6: ContractsList->Delete();  break;
                                //	   case 7: TaxWBList->Delete();  break;
                                case 8:
                                    var adj = db.KAgentAdjustment.Find(adj_row.Id);

                                    if (adj != null)
                                    {
                                        db.KAgentAdjustment.Remove(adj);
                                    }
                                    else
                                    {
                                        MessageBox.Show(string.Format("Документ #{0} не знайдено", adj.Num));
                                    }
                                    break;

                                case 10:
                                    ucBankStatements.DeleteItem();
                                    break;

                                case 11:
                                    ucProjectManagement.DeleteItem();
                                    break;

                                case 12:
                                    var smp = db.SettingMaterialPrices.Find(settingMaterialPricesUserControl1.row_smp.Id);
                                    if (smp != null)
                                    {
                                        db.SettingMaterialPrices.Remove(smp);
                                    }
                                    break;

                                case 13:
                                    var exp = db.Expedition.Find(expeditionUserControl1.row_exp.Id);
                                    if (exp != null)
                                    {
                                        db.Expedition.Remove(exp);
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

            var child_node_list = _db.v_GetDocsTree.Where(w => (w.UserId == null || w.UserId == DBHelper.CurrentUser.UserId) && w.PId == focused_tree_node.Id).ToList();
            switch (focused_tree_node.GType)
            {
                case 0:
                    bar1.Visible = false;
                    break;

                case 1:

                    if (focused_tree_node.WType == 1)
                    {
                        wayBillInUserControl.GetData();
                    }
                    else if (focused_tree_node.WType == 16)
                    {
                        ucWBOrdersOut.GetData();
                    }
                    else if (focused_tree_node.WType == -1)
                    {
                        ucWaybillOut.GetData();
                    }
                    else if (focused_tree_node.WType == 2)
                    {
                        ucInvoices.GetData();
                    }
                    else if (focused_tree_node.WType == -16)
                    {
                        ucWBOrdersIn.GetData();
                    }
                    else if (focused_tree_node.WType == 29)
                    {
                        ucServicesIn.GetData();
                    }
                    else if (focused_tree_node.WType == 6)
                    {
                        ucWayBillReturnСustomers.GetData();
                    }
                    else if (focused_tree_node.WType == -6)
                    {
                        ucWaybillReturnSuppliers.GetData();
                    }
                    else if (focused_tree_node.WType == null)
                    {
                        GetWayBillList(string.Join(",", child_node_list.Select(s => Convert.ToString(s.WType))));
                    }
                    else
                    {
                        GetWayBillList(cur_wtype.ToString());
                    }
                    break;

                case 4:
                    if (cur_wtype == -2)
                    {
                        GetPayDocList("-2");
                    }
                    else if (cur_wtype == -3 || cur_wtype == 3)
                    {
                        GetPayDocList((cur_wtype / 3).ToString());
                    }
                    else if (focused_tree_node.Id == 31)
                    {
                        if (child_node_list.Any())
                        {
                            GetPayDocList(string.Join(",", child_node_list.Select(s => Convert.ToString(s.WType != -2 ? s.WType / 3 : s.WType)).ToList()));
                        }
                    }
                    break;

                case 5:
                    int top_row = PriceListGridView.TopRowIndex;
                    PriceListBS.DataSource = DB.SkladBase().v_PriceList.ToList();
                    PriceListGridView.TopRowIndex = top_row;
                    break;

                case 8:
                    GetKAgentAdjustment("");
                    break;

                case 10:

                    ucBankStatements.GetData();
                    break;

                case 11:

                    ucProjectManagement.GetData();
                    break;

                case 12:
                    settingMaterialPricesUserControl1.GetData();
                    break;

                case 13:
                    expeditionUserControl1.GetData();
                    break;
            }

        }

        private void ExecuteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            using (var db = new BaseEntities())
            {
                switch (focused_tree_node.GType)
                {
                    case 1:

                        if (focused_tree_node.WType == 1)
                        {
                            wayBillInUserControl.ExecuteItem();
                        }
                        else if (focused_tree_node.WType == 16)
                        {
                            ucWBOrdersOut.ExecuteItem();
                        }
                        else if (focused_tree_node.WType == -1)
                        {
                            ucWaybillOut.ExecuteItem();
                        }
                        else if (focused_tree_node.WType == 2)
                        {
                            ucInvoices.ExecuteItem();
                        }
                        else if (focused_tree_node.WType == -16)
                        {
                            ucWBOrdersIn.ExecuteItem();
                        }
                        else if (focused_tree_node.WType == 29)
                        {
                            ucServicesIn.ExecuteItem();
                        }
                        else if (focused_tree_node.WType == 6)
                        {
                            ucWayBillReturnСustomers.ExecuteItem();
                        }
                        else if (focused_tree_node.WType == -6)
                        {
                            ucWaybillReturnSuppliers.ExecuteItem();
                        }

                        else
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
                        }

                        break;

                    case 4:
                        var pd_row = PayDocGridView.GetFocusedRow() as GetPayDocList_Result;
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
                                XtraMessageBox.Show("Період вже закритий. Змініть дату документа!", "Відміна/Проведення платіжного документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show(string.Format("Документ #{0} не знайдено", pd_row.DocNum));
                        }
                        break;

                    case 8:

                        var kadjustment_row = KAgentAdjustmentGridView.GetFocusedRow() as v_KAgentAdjustment;
                        var kadj = db.KAgentAdjustment.Find(kadjustment_row.Id);
                        if (kadj != null)
                        {
                            if (kadj.OnDate > db.CommonParams.First().EndCalcPeriod)
                            {
                                kadj.Checked = kadjustment_row.Checked == 0 ? 1 : 0;
                                db.SaveChanges();
                            }
                            else
                            {
                                XtraMessageBox.Show("Період вже закритий. Змініть дату документа!", "Відміна/Проведення корегуючого документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show(string.Format("Документ #{0} не знайдено", kadjustment_row.Num));
                        }
                        break;

                    case 10:
                        ucBankStatements.ExecuteItem();
                        break;

                    case 11:
                        ucProjectManagement.ExecuteItem();
                        break;

                    case 12:
                        settingMaterialPricesUserControl1.ExecuteItem();
                        break;

                    case 13:
                        expeditionUserControl1.ExecuteItem();
                        break;
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void PrintItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(focused_tree_node == null)
            {
                return;
            }

            switch (focused_tree_node.GType)
            {
                case 1:
                    Guid? doc_id;
                    if (focused_tree_node.WType == 1)
                    {
                        doc_id = wayBillInUserControl.wb_focused_row?.Id;
                    }
                    else if (focused_tree_node.WType == 16)
                    {
                        doc_id = ucWBOrdersOut.wb_focused_row?.Id;
                    }
                    else if (focused_tree_node.WType == -1)
                    {
                        doc_id = ucWaybillOut.wb_focused_row?.Id;
                    }
                    else if (focused_tree_node.WType == 2)
                    {
                        doc_id = ucInvoices.wb_focused_row?.Id;
                    }
                    else if (focused_tree_node.WType == -16)
                    {
                        doc_id = ucWBOrdersIn.wb_focused_row?.Id;
                    }
                    else if (focused_tree_node.WType == 29)
                    {
                        doc_id = ucServicesIn.wb_focused_row?.Id;
                    }
                    else if (focused_tree_node.WType == 6)
                    {
                        doc_id = ucWayBillReturnСustomers.wb_focused_row?.Id;
                    }
                    else if (focused_tree_node.WType == -6)
                    {
                        doc_id = ucWaybillReturnSuppliers.wb_focused_row?.Id;
                    }
                    else
                    {
                        doc_id = wb_focused_row?.Id;
                    }
                

                    if(!doc_id.HasValue)
                    {
                        return;
                    }

                    PrintDoc.Show(doc_id.Value, focused_tree_node.WType.Value, _db);
                    break;

                case 4:
                    var pd = PayDocGridView.GetFocusedRow() as GetPayDocList_Result;
                    PrintDoc.Show(pd.Id, pd.DocType == -2 ? pd.DocType : pd.DocType * 3, _db);
                    break;

                case 5:
                    var p_l = PriceListGridView.GetFocusedRow() as v_PriceList;
                    PrintDoc.Show(p_l.Id, 10, _db);
                    break;

                case 12:
                    PrintDoc.SettingMaterialPricesReport(settingMaterialPricesUserControl1.row_smp.PTypeId, _db);
                    break;

                case 13:
                    PrintDoc.ExpeditionReport(expeditionUserControl1.row_exp.Id, _db);
                    break;
            }
        }

        private void PDStartDate_EditValueChanged(object sender, EventArgs e)
        {
            RefrechItemBtn.PerformClick();
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (cur_wtype == 1)  //Прибткова накладна 
            {
                wayBillInUserControl.CopyItem();
            }
            else if (cur_wtype == 16) //замовлення постачальникам
            {
                ucWBOrdersOut.CopyItem();
            }
            else if (cur_wtype == -1) //видаткова
            {
                ucWaybillOut.CopyItem();
            }
            else if (cur_wtype == 2) //рахунок
            {
                ucInvoices.CopyItem();
            }
            else if (cur_wtype == -16) //Замовлення від клієнта
            {
                ucWBOrdersIn.CopyItem();
            }
            else if (cur_wtype == 29) //акт про надання послуг
            {
                ucServicesIn.CopyItem();
            }
            else if (cur_wtype == 6) //Повернення від клієнта
            {
                ucWayBillReturnСustomers.CopyItem();
            }
            else if (cur_wtype == -6) //Повернення постачальнику
            {
                ucWaybillReturnSuppliers.CopyItem();
            }
            else
            {
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
                        var doc = DB.SkladBase().DocCopy(wb_focused_row.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();

                        /*   if (cur_wtype == -1 || cur_wtype == -16) //Відаткова , замовлення клиента 
                           {
                               using (var wb_in = new frmWayBillOut(cur_wtype, doc.out_wbill_id))
                               {
                                   wb_in.is_new_record = true;
                                   wb_in.ShowDialog();
                               }

                           }*/

                        /*       if (cur_wtype == 29)  //Акт надання послуг
                               {
                                   using (var wb_in = new frmActServicesProvided(cur_wtype, doc.out_wbill_id))
                                   {
                                       wb_in.is_new_record = true;
                                       wb_in.ShowDialog();
                                   }
                               }*/

                        /*     if (cur_wtype == 6) // Повернення від клієнта
                             {
                                 using (var wb_re_in = new frmWBReturnIn(cur_wtype, doc.out_wbill_id))
                                 {
                                     wb_re_in.ShowDialog();
                                 }
                             }*/
                        break;

                    case 4:
                        var pd = PayDocGridView.GetFocusedRow() as GetPayDocList_Result;
                        var p_doc = DB.SkladBase().DocCopy(pd.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();

                        int? w_type = focused_tree_node.WType != -2 ? focused_tree_node.WType / 3 : focused_tree_node.WType;
                        using (var pdf = new frmPayDoc(w_type, p_doc.out_wbill_id))
                        {
                            pdf.ShowDialog();
                        }
                        break;

                    case 5:
                        var pl_row = PriceListGridView.GetFocusedRow() as v_PriceList;
                        var pl_id = DB.SkladBase().CopyPriceList(pl_row.PlId).FirstOrDefault();

                        using (var frm = new frmPriceList(pl_id))
                        {
                            frm.ShowDialog();
                        }

                        break;
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void DocsPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            if (cur_wtype == -16 || cur_wtype == 2) ExecuteInvBtn.Visibility = BarItemVisibility.Always;
            else ExecuteInvBtn.Visibility = BarItemVisibility.Never;

            if (cur_wtype == -1) createTaxWBbtn.Visibility = BarItemVisibility.Always;
            else createTaxWBbtn.Visibility = BarItemVisibility.Never;

            ChangeWaybillKagentBtn.Enabled = (DBHelper.is_admin || DBHelper.is_buh) && (cur_wtype == -1 || cur_wtype == 1);
            WbHistoryBtn.Enabled =  IHelper.GetUserAccess(39)?.CanView == 1;
        }

        private void WbGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                DocsPopupMenu.ShowPopup(p2);
            }
        }

        private void ExecuteInvBtn_ItemClick(object sender, ItemClickEventArgs e)
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

            RefrechItemBtn.PerformClick();
        }


        private void NewPayDocBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if ((wb_focused_row.SummInCurr - wb_focused_row.SummPay) <= 0)
            {
                MessageBox.Show("Документ вже оплачено!");
                return;
            }

            int? doc_type;
            if (new[] { 26, 57, 108, 140 }.Any(a => a == focused_tree_node.Id))
            {
                doc_type = -1;
            }
            else if (new[] { 27, 56, 39, 107 }.Any(a => a == focused_tree_node.Id))
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
          
        }

  
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void WbGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var dr = e.Row as GetWayBillList_Result;

            SetWBEditorBarBtn(dr);
        }

        private void SetWBEditorBarBtn(GetWayBillList_Result row)
        {
            /*  if (row == null)
              {
                  return;
              }*/

            xtraTabControl2_SelectedPageChanged(null, null);

            DeleteItemBtn.Enabled = (row != null && row.Checked == 0 && focused_tree_node.CanDelete == 1);
            ExecuteItemBtn.Enabled = (row != null && row.WType != 2 && row.WType != -16 && row.WType != 16 && focused_tree_node.CanPost == 1);
            EditItemBtn.Enabled = (row != null && focused_tree_node.CanModify == 1 && (row.Checked == 0 || row.Checked == 1));
            CopyItemBtn.Enabled = (row != null && focused_tree_node.CanModify == 1);
            PrintItemBtn.Enabled = (row != null);
        }

        private void PayDocGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var dr = e.Row as GetPayDocList_Result;
            PayDocListInfoBS.DataSource = dr;

            if (dr != null)
            {
                ucRelDocGrid2.GetRelDoc(dr.Id);

            }
            else
            {
                ucRelDocGrid2.GetRelDoc(Guid.Empty);
            }

            var tree_row = DocsTreeList.GetDataRecordByNode(DocsTreeList.FocusedNode) as v_GetDocsTree;

            bool isModify = (dr != null && (DBHelper.CashDesks.Any(a => a.CashId == dr.CashId) || dr.CashId == null));

            DeleteItemBtn.Enabled = (dr != null && dr.Checked == 0 && tree_row.CanDelete == 1);
            ExecuteItemBtn.Enabled = (dr != null && tree_row.CanPost == 1 && isModify);
            EditItemBtn.Enabled = (dr != null && tree_row.CanModify == 1 && isModify);
            CopyItemBtn.Enabled = EditItemBtn.Enabled;
            PrintItemBtn.Enabled = (dr != null);
        }

        private void PriceListGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var pl_dr = e.Row as v_PriceList;
            if (pl_dr != null)
            {
                using (var db = new BaseEntities())
                {
                    PriceListDetBS.DataSource = db.v_PriceListDet.AsNoTracking().Where(w => w.PlId == pl_dr.PlId).OrderBy(o => o.Num).ToList();
                    ka_template_list = db.PriceList.FirstOrDefault(w => w.PlId == pl_dr.PlId).Kagent.Select(s => new KaTemplateList
                    {
                        Check = true,
                        KaId = s.KaId,
                        KaName = s.Name
                    }).ToList();
                }
            }
            else
            {
                PriceListDetBS.DataSource = null;
                ka_template_list.Clear();
            }

            KaTemplateListGridControl.DataSource = ka_template_list;


            var tree_row = DocsTreeList.GetDataRecordByNode(DocsTreeList.FocusedNode) as v_GetDocsTree;

            DeleteItemBtn.Enabled = (pl_focused_row != null && tree_row.CanDelete == 1);
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = (pl_focused_row != null && tree_row.CanModify == 1);
            CopyItemBtn.Enabled = EditItemBtn.Enabled;
            PrintItemBtn.Enabled = (pl_focused_row != null);
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
            if ((wb_focused_row.WType == 6 || wb_focused_row.WType == 1) && wb_focused_row.Checked == 1)
            {
                DBHelper.CreateWBWriteOff(wb_focused_row.WbillId);
            }
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            var dr = WbGridView.GetFocusedRow() as GetWayBillList_Result;

            if (dr == null)
            {
                gridControl2.DataSource = null;
                ucRelDocGrid1.GetRelDoc(Guid.Empty);
                WayBillListInfoBS.DataSource = null;
                gridControl10.DataSource = null;

                return;
            }

            switch (xtraTabControl2.SelectedTabPageIndex)
            {
                case 0:
                    gridColumn37.Caption = "Сума в валюті, " + dr.CurrName;
                    gridControl2.DataSource = new BaseEntities().GetWaybillDetIn(dr.WbillId).ToList().OrderBy(o => o.Num);
                    break;

                case 1:
                    WayBillListInfoBS.DataSource = dr;
                    break;

                case 2:
                    ucRelDocGrid1.GetRelDoc(dr.Id);
                    break;

                case 3:
                    gridControl10.DataSource = _db.DocRels.Where(w => w.OriginatorId == dr.Id)
                        .Join(_db.v_PayDoc, drel => drel.RelOriginatorId, pd => pd.Id, (drel, pd) => pd).OrderBy(o => o.OnDate).ToList();
                    break;
            }

        }

        private void PriceListGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                PriceListPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            KaTemplateListGridView.CloseEditor();

            var pl_row = PriceListGridView.GetFocusedRow() as v_PriceList;
            using (var db = DB.SkladBase())
            {
                int wb_count = 0;
                foreach (var kagent in ka_template_list.Where(w => w.Check))
                {

                    var _wb = db.WaybillList.Add(new WaybillList()
                    {
                        Id = Guid.NewGuid(),
                        WType = -16,
                        OnDate = DBHelper.ServerDateTime(),
                        Num = db.GetDocNum("wb(-16)").FirstOrDefault(),
                        CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                        OnValue = 1,
                        PersonId = DBHelper.CurrentUser.KaId,
                        EntId = DBHelper.Enterprise.KaId,
                        UpdatedBy = DBHelper.CurrentUser.UserId,
                        Nds = 0,
                        KaId = kagent.KaId,
                        Notes = pl_row.Notes,
                        Kontragent = db.Kagent.Find(kagent.KaId)
                    });

                    if (_wb.Kontragent.RouteId.HasValue)
                    {
                        var r = _db.Routes.FirstOrDefault(w => w.Id == _wb.Kontragent.RouteId);
                        _wb.CarId = r.CarId;
                        _wb.RouteId = _wb.Kontragent.RouteId;
                        _wb.Received = r.Kagent1 != null ? r.Kagent1.Name : "";
                        _wb.DriverId = r.Kagent1 != null ? (int?)r.Kagent1.KaId : null;
                    }


                    db.SaveChanges();

                    db.CreateOrderByPriceList(_wb.WbillId, pl_row.PlId);

                    ++wb_count;
                }

                MessageBox.Show(string.Format("Створено {0} замовлень !", wb_count));
            }
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
                    for (int i = 0; i < WbGridView.RowCount; i++)
                    {
                        var dr = WbGridView.GetRow(i) as GetWayBillList_Result;

                        if (dr != null)
                        {
                            if (dr.WType == -1)
                            {
                                var data_report = PrintDoc.WayBillOutReport(dr.Id, _db);
                                IHelper.Print(data_report, TemlateList.wb_out, false);
                            }

                            if (dr.WType == -16)
                            {
                                var ord_out = PrintDoc.WayBillOrderedOutReport(dr.Id, _db);
                                IHelper.Print(ord_out, TemlateList.ord_out, false);
                            }
                        }

                    }

                    MessageBox.Show("Експортовано " + WbGridView.RowCount.ToString() + " документів !");

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

            barButtonItem11.Enabled = wb_focused_row.WType == 6 || wb_focused_row.WType == 1;
            
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
                    if (focused_tree_node.WType == -16)
                    {
                        ucWBOrdersIn.ExportToExcel();
                    }
                    else if (focused_tree_node.WType == 16)
                    {
                        ucWBOrdersOut.ExportToExcel();
                    }
                    else if (focused_tree_node.WType == -1)
                    {
                        ucWaybillOut.ExportToExcel();
                    }
                    else IHelper.ExportToXlsx(WBGridControl);
                    break;

                case 4:
                    IHelper.ExportToXlsx(PayDocGridControl);
                    break;
                case 5:
                    IHelper.ExportToXlsx(PriceListGridControl);
                    break;
            }
        }
        public void SaveGridLayouts()
        {
            WbGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + "\\DocsUserControl\\WbGridView");
            PayDocGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + "\\DocsUserControl\\PayDocGridView");
        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MessageBox.Show("Ви бажаєте роздрукувати " + WbGridView.RowCount.ToString() + " документів!", "Друк документів", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                switch (focused_tree_node.GType)
                {
                    case 1:
                        for (int i = 0; i < WbGridView.RowCount; i++)
                        {
                            var dr = WbGridView.GetRow(i) as GetWayBillList_Result;

                            if (dr != null)
                            {
                                if (dr.WType == -1)
                                {
                                    var data_report = PrintDoc.WayBillOutReport(dr.Id, _db);
                                    IHelper.Print(data_report, TemlateList.wb_out_print, false, true);
                                }

                                if (dr.WType == -16)
                                {
                                    var ord_out = PrintDoc.WayBillOrderedOutReport(dr.Id, _db);
                                    IHelper.Print(ord_out, TemlateList.wb_vidgruzka, false, true);
                                }
                            }

                        }
                        break;
                }
            }
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

        private void kaaStartDate_EditValueChanged(object sender, EventArgs e)
        {
            RefrechItemBtn.PerformClick();
        }

        private void KAgentAdjustmentGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var dr = e.Row as v_KAgentAdjustment;

            xtraTabControl4_SelectedPageChanged(sender, null);

            DeleteItemBtn.Enabled = (dr != null && dr.Checked == 0 && focused_tree_node.CanDelete == 1);
            ExecuteItemBtn.Enabled = (dr != null && focused_tree_node.CanPost == 1);
            EditItemBtn.Enabled = (dr != null && focused_tree_node.CanModify == 1 && dr.Checked == 0);
            CopyItemBtn.Enabled = (dr != null && focused_tree_node.CanModify == 1);
            PrintItemBtn.Enabled = (dr != null);
        }

        private void KAgentAdjustmentGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditItemBtn.PerformClick();
        }

        private void xtraTabControl4_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            var dr = KAgentAdjustmentGridView.GetFocusedRow() as v_KAgentAdjustment;

            if (dr == null)
            {
                gridControl5.DataSource = null;
                ucRelDocGrid3.GetRelDoc(Guid.Empty);
                KAgentAdjustmentInfoBS.DataSource = null;

                return;
            }

            switch (xtraTabControl4.SelectedTabPageIndex)
            {
                case 0:

                    gridControl5.DataSource = _db.v_KAgentAdjustmentDet.Where(w => w.KAgentAdjustmentId == dr.Id).OrderBy(o => o.Idx).ToList();
                    break;

                case 1:
                    KAgentAdjustmentInfoBS.DataSource = dr;
                    break;

                case 2:
                    ucRelDocGrid3.GetRelDoc(dr.Id);
                    break;
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


        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.ShowManufacturingMaterial(wb_det_focused_row.MatId);
        }

        private void repositoryItemLookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            if (!EditItemBtn.Enabled)
            {
                return;
            }

            var PTypeId = Convert.ToInt32(((LookUpEdit)sender).EditValue);

            var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);

            wb.PTypeId = PTypeId;
            _db.SaveChanges();

            RefrechItemBtn.PerformClick();
        }

        private void BankStatementsGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditItemBtn.PerformClick();
        }

        private void wbStartDate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                wbStartDate.EditValue = DBHelper.ServerDateTime().Date;
            }
        }

        private void wbEndDate_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                wbEndDate.EditValue = DBHelper.ServerDateTime().SetEndDay();
            }
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
        }

        private void repositoryItemLookUpEdit5_EditValueChanged(object sender, EventArgs e)
        {
            if (!EditItemBtn.Enabled)
            {
                return;
            }

            var EntId = Convert.ToInt32(((LookUpEdit)sender).EditValue);

            var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);

            wb.EntId = EntId;
            _db.SaveChanges();

            RefrechItemBtn.PerformClick();
        }


        private void WbGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            SetWBEditorBarBtn(wb_focused_row);
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new frmBarCode())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var wbill_id = Convert.ToInt32(frm.BarCodeEdit.Text);
                    var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wbill_id);
                    if(wb !=null)
                    {
                        FindDoc.Find(wb.Id, wb.WType, wb.OnDate);
                    }

                }
            }
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

        private void ChangeWaybillKagentBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new frmKagents(-1, ""))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var new_id = frm.focused_row?.KaId;

                    if(new_id !=null)
                    {
                        _db.ChangeWaybillKagent(new_id, wb_focused_row.WbillId);

                        RefrechItemBtn.PerformClick();
                    }
                }
            }
        }

        private void WaybillCorrectionDetBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new frmWaybillCorrection(wb_det_focused_row.PosId))
            {
                frm.ShowDialog();
            }
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            new frmWaybillCorrectionsView().ShowDialog();
        }

        private void WbDetPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            WaybillCorrectionDetBtn.Enabled =( wb_focused_row.WType == -1 && DBHelper.is_buh && wb_focused_row.Checked == 1);
        }

        private void WbHistoryBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new frmLogHistory(24, wb_focused_row.WbillId))
            {
                frm.ShowDialog();
            }
        }
    }
}
