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

        public DocsUserControl()
        {
            InitializeComponent();
            ka_template_list = new List<KaTemplateList>();
        }

        private void DocumentsPanel_Load(object sender, EventArgs e)
        {
            wbContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
 
            if (!DesignMode)
            {
                _db = new BaseEntities();

                DocsTreeList.DataSource = _db.v_GetDocsTree.AsNoTracking().Where(w => w.UserId == null || w.UserId == DBHelper.CurrentUser.UserId).OrderBy(o => o.Num).ToList();
                if (set_tree_node != null)
                {
                    DocsTreeList.FocusedNode = DocsTreeList.FindNodeByFieldValue("Id", set_tree_node);
                    set_tree_node = null;
                }

                DocsTreeList.ExpandAll();
            }

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

            if (focused_tree_node.Id == 31) //Платіжні документи
            {
                wbContentTab.SelectedTabPageIndex = 6;
            }
            else if(focused_tree_node.Id == 32) //Прибуткові , видаткові , рахунки
            {
                ucWayBill.w_types = "1,-1,2";
                wbContentTab.SelectedTabPageIndex = 1;
            }
            else if (focused_tree_node.Id == 106) //Замовлення
            {
                ucWayBill.w_types = "-16,16";
                wbContentTab.SelectedTabPageIndex = 1;
            }
            else if (focused_tree_node.Id == 55) //Повеорнення
            {
                ucWayBill.w_types = "-6,6";
                wbContentTab.SelectedTabPageIndex = 1;
            }
            else if (focused_tree_node.FunId == 21) //Прибуткова накладна
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
            else if (focused_tree_node.FunId == 77) //Заборгованість покупця
            {
                wbContentTab.SelectedTabPageIndex = 22;
            }
            else if (focused_tree_node.FunId == 78) //Заборгованість покупця
            {
                wbContentTab.SelectedTabPageIndex = 23;
            }
            else if (focused_tree_node.FunId == 26) //Прибутковий касовий ордер
            {
                wbContentTab.SelectedTabPageIndex = 2;
            }
            else if (focused_tree_node.FunId == 25) //Видатковий касовий ордер
            {
                wbContentTab.SelectedTabPageIndex = 3;
            }
            else if (focused_tree_node.FunId == 52) //Додаткові витрати
            {
                wbContentTab.SelectedTabPageIndex = 4;
            }
            else
            {
                wbContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;
            }

            RefrechItemBtn.PerformClick();

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

                    if (cur_wtype == 3)  
                    {
                        ucPayDocIn.NewItem();
                    }else
                    if (cur_wtype == -3)
                    {
                        ucPayDocOut.NewItem();
                    }else
                    if (cur_wtype == -2)
                    {
                        ucPayDocExtOut.NewItem();
                    }
                    break;

                case 5:
                    ucPriceList.NewItem();
                    break;

                case 8:
                    if (cur_wtype == 23) //Заборгованість покупця
                    {
                        ucKAgentAdjustmentIn.NewItem();
                    }

                    if (cur_wtype == -23) //Заборгованість постачальнику
                    {
                        ucKAgentAdjustmentOut.NewItem();
                    }
                    
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
                    expeditionUserControl.NewItem();
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
                        break;

                    case 4:
                        if (cur_wtype == 3)
                        {
                            ucPayDocIn.EditItem();
                        }else
                        if (cur_wtype == -3)
                        {
                            ucPayDocOut.EditItem();
                        }else
                        if (cur_wtype == -2)
                        {
                            ucPayDocExtOut.EditItem();
                        }
                        break;

                    case 5:
                        ucPriceList.EditItem();
                        break;

                    case 8:
                        if (focused_tree_node.WType == 23) //Заборгованість покупця
                        {
                            ucKAgentAdjustmentIn.EditItem();
                        }
                        else if (focused_tree_node.WType == -23) //Заборгованість постачальнику
                        {
                            ucKAgentAdjustmentOut.EditItem();
                        }
                        break;

                    case 10:
                        ucBankStatements.EditItem();
                        break;

                    case 11:
                        ucProjectManagement.EditItem();
                        break;

                    case 12:
                        settingMaterialPricesUserControl1.EditItem();
                        break;

                    case 13:
                        expeditionUserControl.EditItem();
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

            int gtype = focused_tree_node.GType.Value;


            try
            {
                switch (gtype)
                {
                    //      case 1: db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK) where WbillId = {0}", dr.WbillId).FirstOrDefault(); break;
                    //          case 4: db.Database.SqlQuery<PayDoc>("SELECT * from PayDoc WITH (UPDLOCK) where PayDocId = {0}", pd_row.PayDocId).FirstOrDefault(); break;
                    //	case 5: PriceList->LockRecord();  break;
                    //	case 6: ContractsList->LockRecord();  break;
                    //	case 7: TaxWBList->LockRecord();  break;
                }


                switch (gtype)
                {
                    case 1:
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
                        break;

                    case 4:
                        if (cur_wtype == 3)
                        {
                            ucPayDocIn.DeleteItem();
                        }
                        else if (cur_wtype == -3)
                        {
                            ucPayDocOut.DeleteItem();
                        }
                        else if (cur_wtype == -2)
                        {
                            ucPayDocExtOut.DeleteItem();
                        }
                        break;

                    case 5:
                        ucPriceList.DeleteItem();
                        break;

                    case 8:

                        if (focused_tree_node.WType == 23) //Заборгованість покупця
                        {
                            ucKAgentAdjustmentIn.DeleteItem();
                        }

                        if (focused_tree_node.WType == -23) //Заборгованість постачальнику
                        {
                            ucKAgentAdjustmentOut.DeleteItem();
                        }

                        break;

                    case 10:
                        ucBankStatements.DeleteItem();
                        break;

                    case 11:
                        ucProjectManagement.DeleteItem();
                        break;

                    case 12:
                        settingMaterialPricesUserControl1.DeleteItem();
                        break;

                    case 13:
                        expeditionUserControl.DeleteItem();
                        break;
                }
            }
            catch
            {
                MessageBox.Show(Resources.deadlock);
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

        //    var child_node_list = _db.v_GetDocsTree.Where(w => (w.UserId == null || w.UserId == DBHelper.CurrentUser.UserId) && w.PId == focused_tree_node.Id).ToList();
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
                    else
                    {
                        ucWayBill.GetData();
                    }
                    break;

                case 4:
                    if (cur_wtype == 3)
                    {
                        ucPayDocIn.GetData();
                    }
                    else if (cur_wtype == -3)
                    {
                        ucPayDocOut.GetData();
                    }
                    else if (cur_wtype == -2)
                    {
                        ucPayDocExtOut.GetData();
                    }
                    else
                    {
                        ucPayDoc.GetData();
                    }
                    break;

                case 5:
                    ucPriceList.GetData();
                    break;

                case 8:
                    if (focused_tree_node.WType == 23) //Заборгованість покупця
                    {
                        ucKAgentAdjustmentIn.GetData();
                    }

                    if (focused_tree_node.WType == -23) //Заборгованість постачальнику
                    {
                        ucKAgentAdjustmentOut.GetData();
                    }
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
                    expeditionUserControl.GetData();
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

                        break;

                    case 4:
                        if (cur_wtype == 3)
                        {
                            ucPayDocIn.ExecuteItem();
                        }
                        else if (cur_wtype == -3)
                        {
                            ucPayDocOut.ExecuteItem();
                        }
                        else if (cur_wtype == -2)
                        {
                            ucPayDocExtOut.ExecuteItem();
                        }
                        break;

                    case 8:

                        if (focused_tree_node.WType == 23) //Заборгованість покупця
                        {
                            ucKAgentAdjustmentIn.ExecuteItem();
                        }

                        if (focused_tree_node.WType == -23) //Заборгованість постачальнику
                        {
                            ucKAgentAdjustmentOut.ExecuteItem();
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
                        expeditionUserControl.ExecuteItem();
                        break;
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void PrintItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            switch (focused_tree_node.GType)
            {
                case 1:
                    if (focused_tree_node.WType == 1)
                    {
                        wayBillInUserControl.PrintItem();
                    }
                    else if (focused_tree_node.WType == 16)
                    {
                        ucWBOrdersOut.PrintItem();
                    }
                    else if (focused_tree_node.WType == -1)
                    {
                        ucWaybillOut.PrintItem();
                    }
                    else if (focused_tree_node.WType == 2)
                    {
                        ucInvoices.PrintItem();
                    }
                    else if (focused_tree_node.WType == -16)
                    {
                        ucWBOrdersIn.PrintItem();
                    }
                    else if (focused_tree_node.WType == 29)
                    {
                        ucServicesIn.PrintItem();
                    }
                    else if (focused_tree_node.WType == 6)
                    {
                        ucWayBillReturnСustomers.PrintItem();
                    }
                    else if (focused_tree_node.WType == -6)
                    {
                        ucWaybillReturnSuppliers.PrintItem();
                    }

                    break;

                case 4:
                    if (focused_tree_node.Id == 31) //Платіжні документи
                    {
                        ucPayDoc.PrintItem();
                    }
                    else if (cur_wtype == 3)
                    {
                        ucPayDocIn.PrintItem();
                    }
                    else if (cur_wtype == -3)
                    {
                        ucPayDocOut.PrintItem();
                    }
                    else if (cur_wtype == -2)
                    {
                        ucPayDocExtOut.PrintItem();
                    }
                    break;

                case 5:
                    ucPriceList.PrintItem();

                    break;

                case 12:
                    settingMaterialPricesUserControl1.PrintItem();
                    break;

                case 13:
                    expeditionUserControl.PrintItem();
                    break;
            }
        }

        private void PDStartDate_EditValueChanged(object sender, EventArgs e)
        {
            RefrechItemBtn.PerformClick();
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
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
                    break;

                case 4:
                    if (cur_wtype == 3)
                    {
                        ucPayDocIn.CopyItem();
                    }
                    else if (cur_wtype == -3)
                    {
                        ucPayDocOut.CopyItem();
                    }
                    else if (cur_wtype == -2)
                    {
                        ucPayDocExtOut.CopyItem();
                    }
                    break;

                case 5:
                    ucPriceList.CopyItem();

                    break;
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
          /*  var wbl = DB.SkladBase().WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);
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

            RefrechItemBtn.PerformClick();*/
        }


        private void NewPayDocBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
         /*   if ((wb_focused_row.SummInCurr - wb_focused_row.SummPay) <= 0)
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
            }*/
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


        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
          /*  if ((wb_focused_row.WType == 6 || wb_focused_row.WType == 1) && wb_focused_row.Checked == 1)
            {
                DBHelper.CreateWBWriteOff(wb_focused_row.WbillId);
            }*/
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {

        }


        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
 
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
          /*  switch (focused_tree_node.GType)
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
            }*/
        }


        private void DocsPopupMenu_Popup(object sender, EventArgs e)
        {
         /*   if (wb_focused_row == null)
            {
                return;
            }

            barButtonItem11.Enabled = wb_focused_row.WType == 6 || wb_focused_row.WType == 1;*/
            
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
                    else if (focused_tree_node.WType == 1)
                    {
                        wayBillInUserControl.ExportToExcel();
                    }
                    break;

                case 4:
                    if (cur_wtype == 3)
                    {
                        ucPayDocIn.ExportToExcel();
                    }
                    else if (cur_wtype == -3)
                    {
                        ucPayDocOut.ExportToExcel();
                    }
                    else if (cur_wtype == -2)
                    {
                        ucPayDocExtOut.ExportToExcel();
                    }
                    break;

                case 5:
                    ucPriceList.ExportToExcel();
                    break;

                case 13:
                    expeditionUserControl.ExportToExcel();
                    break;
            }
        }


        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
         /*  if (MessageBox.Show("Ви бажаєте роздрукувати " + WbGridView.RowCount.ToString() + " документів!", "Друк документів", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
            }*/
        }

        private void wbKagentList_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
         /*   if (e.Button.Index == 1)
            {
                wbKagentList.EditValue = IHelper.ShowDirectList(wbKagentList.EditValue, 1);
            }*/
        }

        


        private void WaybillDetGridView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {

         /* if (e.SummaryProcess == CustomSummaryProcess.Finalize && gridControl2.DataSource != null)
            {
                var def_m = DBHelper.MeasuresList.FirstOrDefault(w => w.Def == 1);

                GridSummaryItem item = e.Item as GridSummaryItem;

                if (item.FieldName == "Amount")
                {
                    var mat_list = gridControl2.DataSource as IOrderedEnumerable<GetWaybillDetIn_Result>;
                    var amount_sum = mat_list.Where(w => w.MId == def_m.MId).Sum(s => s.Amount);

                    e.TotalValue = amount_sum.ToString() + " " + def_m.ShortName;//Math.Round(amount_sum + ext_sum, 2);
                }
            }*/
        }


        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
        //    IHelper.ShowManufacturingMaterial(wb_det_focused_row.MatId);
        }

        private void repositoryItemLookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
         /*   if (!EditItemBtn.Enabled)
            {
                return;
            }

            var PTypeId = Convert.ToInt32(((LookUpEdit)sender).EditValue);

            var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);

            wb.PTypeId = PTypeId;
            _db.SaveChanges();

            RefrechItemBtn.PerformClick();*/
        }

        private void BankStatementsGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditItemBtn.PerformClick();
        }



        private void repositoryItemLookUpEdit5_EditValueChanged(object sender, EventArgs e)
        {
          /*  if (!EditItemBtn.Enabled)
            {
                return;
            }

            var EntId = Convert.ToInt32(((LookUpEdit)sender).EditValue);

            var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);

            wb.EntId = EntId;
            _db.SaveChanges();

            RefrechItemBtn.PerformClick();*/
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
          /*  if (!EditItemBtn.Enabled)
            {
                return;
            }

            var s_date = ((DateEdit)sender).DateTime;

            var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);

            wb.ShipmentDate = s_date;
            _db.SaveChanges();

            RefrechItemBtn.PerformClick();*/
        }

        private void ChangeWaybillKagentBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
         /*   using (var frm = new frmKagents(-1, ""))
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
            }*/
        }

        private void WaybillCorrectionDetBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
         /*   using (var frm = new frmWaybillCorrection(wb_det_focused_row.PosId))
            {
                frm.ShowDialog();
            }*/
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            new frmWaybillCorrectionsView().ShowDialog();
        }

        private void WbDetPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
         //   WaybillCorrectionDetBtn.Enabled =( wb_focused_row.WType == -1 && DBHelper.is_buh && wb_focused_row.Checked == 1);
        }

        private void WbHistoryBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
        /*    using (var frm = new frmLogHistory(24, wb_focused_row.WbillId))
            {
                frm.ShowDialog();
            }*/
        }
    }
}
