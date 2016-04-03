using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Saklad.SpData;
using SP_Saklad;
using SP_Saklad.WBForm;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SP_Saklad.Properties;

namespace SP_Saklad.MainTabs
{
    public partial class DocsUserControl : UserControl
    {
        const int USER_ID = 0;
        int cur_wtype = 0;
        int show_null_balance = 1;
        BaseEntities _db { get; set; }

        public DocsUserControl()
        {
            InitializeComponent();
        }

        private void DocumentsPanel_Load(object sender, EventArgs e)
        {

        }

        public void OnLoad()
        {
            _db = new BaseEntities();

            wbKagentList.Properties.DataSource = new List<object>() { new { KAID = 0, NAME = "Усі" } }.Concat(_db.KAGENT.Select(s => new { s.KAID, s.NAME }));
            wbKagentList.EditValue = 0;

            wbSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
            wbSatusList.EditValue = -1;

            wbStartDate.EditValue = DateTime.Now.AddDays(-30);
            wbEndDate.EditValue = DateTime.Now;

            DocsTreeList.DataSource = _db.v_GetDocsTree.Where(w => w.USERID == null || w.USERID == USER_ID).OrderBy(o => o.NUM).ToList();
            DocsTreeList.ExpandAll();
        }

        void GetWBlist(int wtyp)
        {
            var ff = DocsTreeList.FocusedNode;

            if (wbSatusList.EditValue == null || wbKagentList.EditValue == null || DocsTreeList.FocusedNode==null)
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;
            var bookmark = WbGridView.FocusedRowHandle;

            gridControl1.DataSource = _db.GetWayBillList(satrt_date.Date, end_date.Date.AddDays(1), wtyp, (int)wbSatusList.EditValue, (int)wbKagentList.EditValue, show_null_balance, "*", 0).OrderByDescending(o => o.OnDate);

            WbGridView.FocusedRowHandle = bookmark;
        }


        private void DocsTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            cur_wtype = e.Node.GetValue("WTYPE") != null ? Convert.ToInt32(e.Node.GetValue("WTYPE")) : 0;
            var typ = Convert.ToInt32(e.Node.GetValue("GTYPE"));
            switch (typ)
            {
                case 1:
                    //GET_RelDocList->DataSource = WayBillListDS;
                    GetWBlist(cur_wtype);
                    //                    WayBillListAfterScroll(WayBillList);
                    break;

                /*        case 4: GET_RelDocList->DataSource = PayDocDS;
                            PayDocTopPanelDate->Edit();
                            if (DocsTreeDataID->Value == 103) PayDocTopPanelDateDOCTYPE->Value = -2;
                            if (DocsTreeDataID->Value == 30) PayDocTopPanelDateDOCTYPE->Value = -1;
                            if (DocsTreeDataID->Value == 29) PayDocTopPanelDateDOCTYPE->Value = 1;
                            PayDocTopPanelDate->Post();
                            PayDoc->FullRefresh();
                            PayDocAfterScroll(PayDoc);
                            break;

                        case 5: PriceList->CloseOpen(true);
                            PriceListAfterScroll(PriceList);
                            break;

                        case 6: GET_RelDocList->DataSource = ContractsListDS;
                            if (DocsTreeDataID->Value == 47) ContractsList->ParamByName("IN_DOCTYPE")->AsVariant = -1;
                            if (DocsTreeDataID->Value == 46) ContractsList->ParamByName("IN_DOCTYPE")->AsVariant = 1;
                            ContractsList->CloseOpen(true);
                            ContractsListAfterScroll(ContractsList);
                            break;

                        case 7: GET_RelDocList->DataSource = TaxWBListDS;
                            TaxWBList->CloseOpen(true);
                            break;*/
            }
            wbContentTab.SelectedTabPageIndex = typ;

        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {
            GetWBlist(cur_wtype);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var dr = WbGridView.GetRow(e.FocusedRowHandle) as GetWayBillList_Result;

            if (dr != null)
            {
                gridControl2.DataSource = _db.GetWaybillDetIn(dr.WbillId);
            }
        }


        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int GTYPE = (int)DocsTreeList.FocusedNode.GetValue("GTYPE");
            int ID = (int)DocsTreeList.FocusedNode.GetValue("ID");

            switch (GTYPE)
            {
                case 1:	/*if(DocsTreeDataID->Value == 27 ||DocsTreeDataID->Value == 39 || DocsTreeDataID->Value == 107)
				 {
					frmWayBillOut = new  TfrmWayBillOut(Application);
					frmWayBillOut->WayBillList->Open();
					frmWayBillOut->WayBillList->Append();
					if(DocsTreeDataID->Value == 27) frmWayBillOut->WayBillListWTYPE->Value = -1 ;
					if(DocsTreeDataID->Value == 39) frmWayBillOut->WayBillListWTYPE->Value = 2 ;
					if(DocsTreeDataID->Value == 107) frmWayBillOut->WayBillListWTYPE->Value  = -16;
					frmWayBillOut->WayBillListENTID->Value = SkladData->EnterpriseKAID->Value ;
					frmWayBillOut->WayBillList->Post();
					frmWayBillOut->WayBillList->Edit();
					frmWayBillOut->ShowModal() ;
					delete frmWayBillOut;
				 }*/

                    if (cur_wtype == 1 || ID == 16)  //Прибткова накладна , замовлення постачальникам
                    {
                        using (var wb_in = new frmWayBillIn(cur_wtype, null))
                        {
                            wb_in.ShowDialog();
                        }
                    }

                    /*		if(DocsTreeDataID->Value == 57) // Повернення від клієнта
                            {
                                frmWBReturnIn = new  TfrmWBReturnIn(Application);
                                frmWBReturnIn->WayBillList->Open();
                                frmWBReturnIn->WayBillList->Append();
                                frmWBReturnIn->WayBillListWTYPE->Value  = 6;
                                frmWBReturnIn->WayBillList->Post();
                                frmWBReturnIn->WayBillList->Edit();
                                frmWBReturnIn->ShowModal() ;
                            }
                            if(DocsTreeDataID->Value == 56 ) //Повернення постачальнику
                            {
                                frmWBReturnOut = new  TfrmWBReturnOut(Application);
                                frmWBReturnOut->WayBillList->Open();
                                frmWBReturnOut->WayBillList->Append();
                                frmWBReturnOut->WayBillListWTYPE->Value  = -6;
                                frmWBReturnOut->WayBillList->Post();
                                frmWBReturnOut->WayBillList->Edit();
                                frmWBReturnOut->ShowModal() ;
                            }*/
                    break;

                /*	case 4: frmPayDoc = new  TfrmPayDoc(Application);
                            frmPayDoc->PayDoc->Open();
                            frmPayDoc->PayDoc->Append();
                            if(DocsTreeDataID->Value == 103) frmPayDoc->PayDocDOCTYPE->Value = -2;
                            if(DocsTreeDataID->Value == 30) frmPayDoc->PayDocDOCTYPE->Value = -1;
                            if(DocsTreeDataID->Value == 29) frmPayDoc->PayDocDOCTYPE->Value = 1;
                            frmPayDoc->ShowModal() ;
                            delete frmPayDoc;
                            break;

                    case 5: frmPriceList = new  TfrmPriceList(Application);
                            frmPriceList->PriceList->Open();
                            frmPriceList->PriceList->Append();
                            frmPriceList->ShowModal() ;
                            delete frmPriceList;
                            break;

                    case 6: frmContr = new  TfrmContr(Application);
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
            }
            // DocsTreeData->Refresh();
            // RefreshBarBtn->Click();


        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int GTYPE = (int)DocsTreeList.FocusedNode.GetValue("GTYPE");
          
            String msg1 = "Для редагування документа потрібно скасувати проводку.\nВи упевнені що прагнете цього? ";
            String Deadlock = "Не можливо переглянути властивості, \nдокумент вже відкрито на одній із робочих станцій!";
            String NotFind = "Документ не знайдено.";

            switch (GTYPE)
            {
                case 1:
                    var dr = WbGridView.GetFocusedRow() as GetWayBillList_Result;
                    if (dr == null)
                    {
                        return;
                    }

                    var wb = new BaseEntities().WaybillList.FirstOrDefault(w => w.WbillId == dr.WbillId);
                    if (wb != null)
                    {
                        if (wb.Checked == 1)
                        {
                            /*if(MessageDlg(msg1,mtConfirmation,TMsgDlgButtons() << mbYes << mbNo ,0)==mrYes)
                                ExecuteBtn->Click();*/
                        }

                        if (wb.Checked == 0)
                        {
                            if (cur_wtype == 1 || cur_wtype == 16)
                            {
                                try
                                {
                                    using (var wb_in = new frmWayBillIn(cur_wtype, wb.WbillId))
                                    {
                                        wb_in.ShowDialog();
                                    }
                                }
                                catch
                                {
                                    ; //	ShowMessage(Deadlock) ;
                                }
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show(NotFind);
                    }

                    /*	if(DocsTreeDataID->Value == 27 || DocsTreeDataID->Value == 39 || DocsTreeDataID->Value == 107)
                         {
                            try
                            {
                              try
                              {
                                frmWayBillOut = new  TfrmWayBillOut(Application);
                                frmWayBillOut->WayBillList->ParamByName("WBILLID")->Value = WayBillListWBILLID->Value;
                                frmWayBillOut->WayBillList->Open();
                                frmWayBillOut->WayBillList->Edit();
                                frmWayBillOut->WayBillList->LockRecord(true)  ;
                                frmWayBillOut->ShowModal();
                              }
                              catch(const Exception& e)
                              {
                                frmWayBillOut->Close();
                                if(e.Message.Pos("Deadlock") > 0) 	ShowMessage(Deadlock) ;
                                else   ShowMessage(e.Message) ;
                              }
                            }
                            __finally
                            {
                               delete frmWayBillOut ;
                            }

                         }

                        if(DocsTreeDataID->Value == 57) // Повернення від кліента
                         {
                            try
                            {
                              try
                              {
                                frmWBReturnIn  = new TfrmWBReturnIn(Application);
                                frmWBReturnIn->WayBillList->ParamByName("WBILLID")->Value = WayBillListWBILLID->Value;
                                frmWBReturnIn->WayBillList->Open();
                                frmWBReturnIn->WayBillList->Edit();
                                frmWBReturnIn->WayBillList->LockRecord()  ;
                                frmWBReturnIn->ShowModal();
                              }
                              catch(const Exception& e)
                              {
                                frmWBReturnIn->Close();
                                if(e.Message.Pos("Deadlock") > 0) 	ShowMessage(Deadlock) ;
                                else   ShowMessage(e.Message) ;
                              }
                            }
                            __finally
                            {
                                delete frmWBReturnIn ;
                            }
                         }

                        if(DocsTreeDataID->Value == 56) //Повернення постачальнику
                         {
                            try
                            {
                              try
                              {
                                frmWBReturnOut  = new  TfrmWBReturnOut(Application);
                                frmWBReturnOut->WayBillList->ParamByName("WBILLID")->Value = WayBillListWBILLID->Value;
                                frmWBReturnOut->WayBillList->Open();
                                frmWBReturnOut->WayBillList->Edit();
                                frmWBReturnOut->WayBillList->LockRecord()  ;
                                frmWBReturnOut->ShowModal();
                              }
                              catch(const Exception& e)
                              {
                                frmWBReturnOut->Close();
                                if(e.Message.Pos("Deadlock") > 0) 	ShowMessage(Deadlock) ;
                                  else ShowMessage(e.Message) ;
                              }
                            }
                            __finally
                            {
                                  delete frmWBReturnOut ;
                            }
                         }*/

                    break;
                /*
                        case 4: PayDoc->Refresh();
                                if(PayDocCHECKED->Value == 1)
                                    if(MessageDlg(msg1,mtConfirmation,TMsgDlgButtons() << mbYes << mbNo ,0)==mrYes)
                                       ExecuteBtn->Click();
                                if(PayDocCHECKED->Value == 0)
                                 {
                                     TfrmPayDoc *frmPD = new  TfrmPayDoc(Application);
                                     try
                                     {
                                        try
                                        {
                                          frmPD = new  TfrmPayDoc(Application);
                                          frmPD->PayDoc->ParamByName("PAYDOCID")->Value = PayDocPAYDOCID->Value;
                                          frmPD->PayDoc->Open();
                                          frmPD->PayDoc->Edit();
                                          frmPD->PayDoc->LockRecord()  ;
                                          frmPD->ShowModal() ;
                                        }
                                        catch(const Exception& e)
                                        {
                                            if(e.Message.Pos("Deadlock") > 0) 	ShowMessage(Deadlock) ;
                                               else  ShowMessage(e.Message) ;
                                        }
                                     }
                                     __finally
                                     {
                                          delete frmPD ;
                                     }
                                 }
                                break;

                        case 5: {
                                  try
                                  {
                                    try
                                    {
                                       frmPriceList = new  TfrmPriceList(Application);
                                       frmPriceList->PriceList->ParamByName("PLID")->Value = PriceListPLID->Value;
                                       frmPriceList->PriceList->Open();
                                       frmPriceList->PriceList->Edit();
                                       frmPriceList->PriceList->LockRecord()  ;
                                       frmPriceList->ShowModal() ;
                                    }
                                    catch(const Exception& e)
                                    {
                                       frmPriceList->Close();
                                       if(e.Message.Pos("Deadlock") > 0) 	ShowMessage(Deadlock) ;
                                           else ShowMessage(e.Message) ;
                                    }
                                  }
                                  __finally
                                  {
                                     delete frmPriceList ;
                                  }
                                }
                                break;

                        case 6: ContractsList->Refresh();
                                if(ContractsListCHECKED->Value == 1)
                                    if(MessageDlg(msg1,mtConfirmation,TMsgDlgButtons() << mbYes << mbNo ,0)==mrYes)
                                       ExecuteBtn->Click();

                                if(ContractsListCHECKED->Value == 0)
                                 {
                                    try
                                    {
                                      try
                                      {
                                        frmContr = new  TfrmContr(Application);
                                        frmContr->CONTRACTS->ParamByName("CONTRID")->Value = ContractsListCONTRID->Value;
                                        frmContr->CONTRACTS->Open();
                                        frmContr->CONTRACTS->Edit();
                                        frmContr->CONTRACTS->LockRecord()  ;
                                        frmContr->ShowModal() ;

                                      }
                                      catch(const Exception& e)
                                      {
                                         frmContr->Close();
                                         if(e.Message.Pos("Deadlock") > 0) 	ShowMessage(Deadlock) ;
                                            else   ShowMessage(e.Message) ;
                                      }
                                    }
                                    __finally
                                    {
                                        delete frmContr;
                                    }
                                 }
                                break;

                        case 7: try
                                {
                                   try
                                   {
                                     frmTaxWB = new  TfrmTaxWB(Application);
                                     frmTaxWB->TaxWB->ParamByName("TWBID")->Value = TaxWBListTWBID->Value;
                                     frmTaxWB->TaxWB->Open();
                                     frmTaxWB->TaxWB->Edit();
                                     frmTaxWB->TaxWB->LockRecord()  ;
                                     frmTaxWB->ShowModal() ;
                                   }
                                   catch(const Exception& e)
                                   {
                                      frmTaxWB->Close();
                                      if(e.Message.Pos("Deadlock") > 0) 	ShowMessage(Deadlock) ;
                                            else   ShowMessage(e.Message) ;
                                   }
                                }
                                __finally
                                {
                                    delete frmTaxWB;
                                }
                                break;*/

            }

            GetWBlist(cur_wtype);
            // RefreshBarBtn->Click();

        }

        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = view.CalcHitInfo(pt);

            if (info.InRow || info.InRowCell)
            {
                EditItemBtn.PerformClick();
            }

        }

        private void WbGridView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetWBlist(cur_wtype);
        }

        private void ExecuteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var g_type = (int)DocsTreeList.FocusedNode.GetValue("GTYPE");

            using (var db = new BaseEntities())
            {
                switch (g_type)
                {
                    case 1:
                        var dr = WbGridView.GetFocusedRow() as GetWayBillList_Result;
                        if (dr == null)
                        {
                            return;
                        }

                        var wb = db.WaybillList.Find(dr.WbillId);
                        if (wb != null)
                        {
                            if (wb.Checked == 1)
                            {
                                var result = db.StornoWayBill (wb.WbillId).FirstOrDefault();
                           
                                if (result == 1)
                                {
                                    MessageBox.Show("Відміна проводки", Resources.not_storno_wb, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                if (wb.WType == -1)
                                {
                                    //   if (!SkladData->CheckActiveSuppliers(WayBillListWBILLID->Value, DocPAnelTransaction)) return;
                                }

                                var result = db.ExecuteWayBill(wb.WbillId, null).FirstOrDefault();
                                if (result != null && result.Checked == 0)
                                {
                                    MessageBox.Show("Проведення документа", Resources.not_execute_wb, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(Resources.not_find_wb);
                        }
                        break;
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void PrintItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
