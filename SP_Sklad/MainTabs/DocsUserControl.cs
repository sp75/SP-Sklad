using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.Properties;
using SP_Sklad.Common;
using DevExpress.XtraBars;
using SP_Sklad.ViewsForm;
using DevExpress.XtraEditors;
using SP_Sklad.Reports;

namespace SP_Sklad.MainTabs
{
    public partial class DocsUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        int cur_wtype = 0;

        BaseEntities _db { get; set; }
        v_GetDocsTree focused_tree_node { get; set; }
        public int? set_tree_node { get; set; }

        public DocsUserControl()
        {
            InitializeComponent();
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
                ucPayDoc.w_types = "";
                if (IHelper.GetUserAccess(25)?.CanView == 1)
                {
                    ucPayDoc.w_types = "-3,";
                }
                if (IHelper.GetUserAccess(26)?.CanView == 1)
                {
                    ucPayDoc.w_types += "3,";
                }
                if (IHelper.GetUserAccess(52)?.CanView == 1)
                {
                    ucPayDoc.w_types += "-2,";
                }
                wbContentTab.SelectedTabPageIndex = 6;
                //  ucPayDoc.GetData();
            }
            else if (focused_tree_node.Id == 32) //Прибуткові , видаткові , рахунки
            {
                ucWayBill.w_types = "";
                if (IHelper.GetUserAccess(21)?.CanView == 1)
                {
                    ucWayBill.w_types = "1,";
                }
                if (IHelper.GetUserAccess(23)?.CanView == 1)
                {
                    ucWayBill.w_types += "-1,";
                }
                if (IHelper.GetUserAccess(30)?.CanView == 1)
                {
                    ucWayBill.w_types += "2,";
                }

                wbContentTab.SelectedTabPageIndex = 1;
            }
            else if (focused_tree_node.Id == 106) //Замовлення
            {
                ucWayBill.w_types = "";
                if (IHelper.GetUserAccess(65)?.CanView == 1)
                {
                    ucWayBill.w_types = "16,";
                }
                if (IHelper.GetUserAccess(64)?.CanView == 1)
                {
                    ucWayBill.w_types += "-16,";
                }
                wbContentTab.SelectedTabPageIndex = 1;
            }
            else if (focused_tree_node.Id == 55) //Повеорнення
            {
                ucWayBill.w_types = "";
                if (IHelper.GetUserAccess(42)?.CanView == 1)
                {
                    ucWayBill.w_types = "6,";
                }
                if (IHelper.GetUserAccess(43)?.CanView == 1)
                {
                    ucWayBill.w_types += "-6,";
                }
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
                    } else
                    if (cur_wtype == -3)
                    {
                        ucPayDocOut.NewItem();
                    } else
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
                        else if (focused_tree_node.WType == 6)
                        {
                            ucWayBillReturnСustomers.EditItem();
                        }
                        else if (focused_tree_node.WType == -6)
                        {
                            ucWaybillReturnSuppliers.EditItem();
                        }
                        break;

                    case 4:
                        if (cur_wtype == 3)
                        {
                            ucPayDocIn.EditItem();
                        } else
                        if (cur_wtype == -3)
                        {
                            ucPayDocOut.EditItem();
                        } else
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


        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var frm = new frmBarCode())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (int.TryParse(frm.BarCodeEdit.Text, out int wbill_id))
                    {
                        var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wbill_id);
                        if (wb != null)
                        {
                            FindDoc.Find(wb.Id, wb.WType, wb.OnDate);
                        }
                        else
                        {
                            XtraMessageBox.Show("Документ не знайдено!");
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Невірний формат штрихкода!");
                    }
                }
            }
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            new frmWaybillCorrectionsView().ShowDialog();
        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {
            new frmDocumentDetails().Show();
        }

        private void hyperlinkLabelControl2_Click(object sender, EventArgs e)
        {
            new frmReport51().Show();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            new frmWaybillCorrectionsView().ShowDialog();
        }
    }
}
