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

namespace SP_Sklad.MainTabs
{
    public partial class DocsUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        int cur_wtype = 0;
        int show_null_balance = 1;
        BaseEntities _db { get; set; }
        v_GetDocsTree focused_tree_node { get; set; }

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
        }

        private void DocumentsPanel_Load(object sender, EventArgs e)
        {
            wbContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            if (!DesignMode)
            {
                _db = new BaseEntities();

                wbKagentList.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(_db.Kagent.Select(s => new { s.KaId, s.Name }));
                wbKagentList.EditValue = 0;

                wbStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                wbStatusList.EditValue = -1;

                wbStartDate.EditValue = DateTime.Now.AddDays(-30);
                wbEndDate.EditValue = DateTime.Now;

                PDStartDate.EditValue = DateTime.Now.AddDays(-30);
                PDEndDate.EditValue = DateTime.Now;

                PDKagentList.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(_db.Kagent.Select(s => new { s.KaId, s.Name }));
                PDKagentList.EditValue = 0;

                PDSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                PDSatusList.EditValue = -1;

                DocsTreeList.DataSource = _db.v_GetDocsTree.Where(w => w.UserId == null || w.UserId == DBHelper.CurrentUser.UserId).OrderBy(o => o.Num).ToList();
                DocsTreeList.ExpandAll();
            }
        }

        void GetWayBillList(int wtyp)
        {
            if (wbStatusList.EditValue == null || wbKagentList.EditValue == null || DocsTreeList.FocusedNode == null)
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;

            int top_row = WbGridView.TopRowIndex;
            GetWayBillListBS.DataSource = _db.GetWayBillList(satrt_date.Date, end_date.Date.AddDays(1), wtyp, (int)wbStatusList.EditValue, (int)wbKagentList.EditValue, show_null_balance, "*", 0).OrderByDescending(o => o.OnDate).ToList();
            WbGridView.TopRowIndex = top_row;
        }

        void GetPayDocList(int doc_typ)
        {
            if (PDSatusList.EditValue == null || PDKagentList.EditValue == null || DocsTreeList.FocusedNode == null)
            {
                return;
            }

            var satrt_date = PDStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : PDStartDate.DateTime;
            var end_date = PDEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : PDEndDate.DateTime;

            int top_row = PayDocGridView.TopRowIndex;
            GetPayDocListBS.DataSource = _db.GetPayDocList(doc_typ, satrt_date.Date, end_date.Date.AddDays(1), (int)PDKagentList.EditValue, (int)PDSatusList.EditValue, -1).OrderByDescending(o => o.OnDate).ToList();
            PayDocGridView.TopRowIndex = top_row;
        }

        private void DocsTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            CopyItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;

            focused_tree_node = DocsTreeList.GetDataRecordByNode(e.Node) as v_GetDocsTree;

            cur_wtype = focused_tree_node.WType != null ? focused_tree_node.WType.Value : 0;
            RefrechItemBtn.PerformClick();

            wbContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;

            if (focused_tree_node.FunId != null)
            {
                History.AddEntry(new HistoryEntity { FunId = focused_tree_node.FunId.Value, MainTabs = 0 });
            }
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {
            GetWayBillList(cur_wtype);
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
                    if (cur_wtype == -1 || cur_wtype == -16 || cur_wtype == 2) //Відаткова , замолення клиента , рахунок
                    {
                        using (var wb_in = new frmWayBillOut(cur_wtype, null))
                        {
                            wb_in.ShowDialog();
                        }

                    }
                    if (cur_wtype == 1 || cur_wtype == 16)  //Прибткова накладна , замовлення постачальникам
                    {
                        using (var wb_in = new frmWayBillIn(cur_wtype, null))
                        {
                            wb_in.ShowDialog();
                        }
                    }

                    if (cur_wtype == 6) // Повернення від клієнта
                    {
                        using (var wb_re_in = new frmWBReturnIn(cur_wtype, null))
                        {
                            wb_re_in.ShowDialog();
                        }
                    }

                    if (cur_wtype == -6) //Повернення постачальнику
                    {
                        using (var wb_re_out = new frmWBReturnOut(null))
                        {
                            wb_re_out.ShowDialog();
                        }

                    }
                    break;

                case 4:

                    int? w_type = focused_tree_node.WType != -2 ? focused_tree_node.WType / 3 : focused_tree_node.WType;
                    using (var pd = new frmPayDoc(w_type, null))
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
                        DocEdit.WBEdit(WbGridView.GetFocusedRow() as GetWayBillList_Result);
                        break;

                    case 4:
                        DocEdit.PDEdit(PayDocGridView.GetFocusedRow() as GetPayDocList_Result);
                        break;
                    case 5:
                        var pl_row = PriceListGridView.GetFocusedRow() as v_PriceList;
                        new frmPriceList(pl_row.PlId).ShowDialog();
                        break;

                    /*           case 6: ContractsList->Refresh();
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
                //    current_transaction.Rollback();
            }

            RefrechItemBtn.PerformClick();
        }

        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int gtype = (int)DocsTreeList.FocusedNode.GetValue("GType");
            var dr = WbGridView.GetFocusedRow() as GetWayBillList_Result;
            var pd_row = PayDocGridView.GetFocusedRow() as GetPayDocList_Result;

            //    var trans = _db.Database.BeginTransaction(IsolationLevel.RepeatableRead);
            using (var db = new BaseEntities())
            {

                try
                {
                    switch (gtype)
                    {
                        case 1: db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK) where WbillId = {0}", dr.WbillId).FirstOrDefault(); break;
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
                                var wb = db.WaybillList.FirstOrDefault(w => w.WbillId == dr.WbillId && w.SessionId == null);
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
                                db.PayDoc.Remove(db.PayDoc.Find(pd_row.PayDocId));
                                break;

                            case 5:
                                db.DeleteWhere<PriceList>(w => w.PlId == pl_focused_row.PlId);
                                break;

                            //	   case 6: ContractsList->Delete();  break;
                            //	   case 7: TaxWBList->Delete();  break;
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
                    //GET_RelDocList->DataSource = WayBillListDS;
                    GetWayBillList(cur_wtype);
                    break;

                case 4:
                    if (cur_wtype == -2) GetPayDocList(-2);
                    else GetPayDocList(cur_wtype / 3);
                    break;


                case 5:
                    int top_row = PriceListGridView.TopRowIndex;
                    PriceListBS.DataSource = DB.SkladBase().v_PriceList.ToList();
                    PriceListGridView.TopRowIndex = top_row;
                    break;

                /*      case 6: ContractsList->Refresh();
                          ContractsList->FullRefresh();
                          ContrDet->FullRefresh();
                          break;

                      case 7: TaxWBList->Refresh();
                          TaxWBList->FullRefresh();
                          TaxWBDet->FullRefresh();
                          break;*/
            }
            //    GET_RelDocList->CloseOpen(true);
        }

        private void ExecuteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var g_type = (int)DocsTreeList.FocusedNode.GetValue("GType");

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

                    case 4:
                        var pd_row = PayDocGridView.GetFocusedRow() as GetPayDocList_Result;
                        var pd = _db.PayDoc.Find(pd_row.PayDocId);
                        pd.Checked = pd_row.Checked == 0 ? 1 : 0;
                        _db.SaveChanges();
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
                    var dr = WbGridView.GetFocusedRow() as GetWayBillList_Result;
                    if (dr == null)
                    {
                        return;
                    }

                    PrintDoc.Show(dr.DocId.Value, dr.WType, _db);
                    break;

                case 4:
                    var pd = PayDocGridView.GetFocusedRow() as GetPayDocList_Result;
                    PrintDoc.Show(pd.DocId.Value, pd.DocType == -2 ? pd.DocType : pd.DocType * 3, _db);
                    break;

                case 5:
                    var p_l = PriceListGridView.GetFocusedRow() as v_PriceList;
                    PrintDoc.Show(p_l.PlId, 10, _db);

                    break;

                //      case 6: frmReportModule->PrintWB(ContractsListDOCID->AsVariant, ContractsListDOCTYPE->Value * 8, DocPAnelTransaction);
                //      case 7: frmReportModule->PrintWB(TaxWBListDOCID->AsVariant, -7, DocPAnelTransaction);
            }
        }

        private void PayDocGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

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
                    var dr = WbGridView.GetFocusedRow() as GetWayBillList_Result;
                    var doc = DB.SkladBase().DocCopy(dr.DocId).FirstOrDefault();

                    if (cur_wtype == -1 || cur_wtype == -16) //Відаткова , замолення клиента 
                    {
                        using (var wb_in = new frmWayBillOut(cur_wtype, doc.out_wbill_id))
                        {
                            wb_in.ShowDialog();
                        }

                    }
                    if (cur_wtype == 1 || cur_wtype == 16)  //Прибткова накладна , замовлення постачальникам
                    {
                        using (var wb_in = new frmWayBillIn(cur_wtype, doc.out_wbill_id))
                        {
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

                    /*         if(DocsTreeDataID->Value == 56 ) //Повернення постачальнику
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

                case 4:
                    var pd = PayDocGridView.GetFocusedRow() as GetPayDocList_Result;
                    var p_doc = DB.SkladBase().DocCopy(pd.DocId).FirstOrDefault();

                    int? w_type = focused_tree_node.WType != -2 ? focused_tree_node.WType / 3 : focused_tree_node.WType;
                    using (var pdf = new frmPayDoc(w_type, p_doc.out_wbill_id))
                    {
                        pdf.ShowDialog();
                    }
                    break;

                /*        case 5: frmPriceList = new  TfrmPriceList(Application);
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

            RefrechItemBtn.PerformClick();
        }

        private void DocsPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            if (cur_wtype == -16 || cur_wtype == 2) ExecuteInvBtn.Visibility = BarItemVisibility.Always;
            else ExecuteInvBtn.Visibility = BarItemVisibility.Never;

            if (cur_wtype == 16) ExecuteInBtn.Visibility = BarItemVisibility.Always;
            else ExecuteInBtn.Visibility = BarItemVisibility.Never;

            if (cur_wtype == -1) createTaxWBbtn.Visibility = BarItemVisibility.Always;
            else createTaxWBbtn.Visibility = BarItemVisibility.Never;
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
                    var result = f._db.ExecuteWayBill(wbl.WbillId, null).ToList().FirstOrDefault();
                    f.doc_id = result.NewDocId;
                    f.is_new_record = true;
                    f.ShowDialog();
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void ExecuteInBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            var wbl = DB.SkladBase().WaybillList.FirstOrDefault(w => w.WbillId == wb_focused_row.WbillId);
            if (wbl == null)
            {
                return;
            }

            if (wbl.Checked == 0)
            {
                using (var f = new frmWayBillIn(1))
                {
                    var result = f._db.ExecuteWayBill(wbl.WbillId, null).ToList().FirstOrDefault();

                    f.doc_id = result.NewDocId;
                    f.ShowDialog();
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void NewPayDocBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if ((wb_focused_row.SummAll - wb_focused_row.SummPay) <= 0)
            {
                MessageBox.Show("Документ вже оплачено!");
                return;
            }

            int? doc_type;
            if (new[] { 26, 57, 108 }.Any(a => a == focused_tree_node.Id))
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

            var frm = new frmPayDoc(doc_type, null)
            {
                PayDocCheckEdit = { Checked = true },
                TypDocsEdit = { EditValue = wb_focused_row.WType },
                _ka_id = wb_focused_row.KaId,
                KagentComboBox = { EditValue = wb_focused_row.KaId }
            };

            frm.GetDocList();
            frm.DocListEdit.EditValue = wb_focused_row.DocId;

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

            FindDoc.Find(row.DocId, row.DocType, row.OnDate);
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

            PrintDoc.Show(row.DocId.Value, row.DocType.Value, DB.SkladBase());
        }

        private void WbGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var dr = e.Row as GetWayBillList_Result;

            xtraTabControl2_SelectedPageChanged(sender, null);

            DeleteItemBtn.Enabled = (dr != null && dr.Checked == 0 && focused_tree_node.CanDelete == 1);
            ExecuteItemBtn.Enabled = (dr != null && dr.WType != 2 && dr.WType != -16 && dr.WType != 16 && focused_tree_node.CanPost == 1);
            EditItemBtn.Enabled = (dr != null && focused_tree_node.CanModify == 1 && (dr.Checked == 0 || dr.Checked == 1));
            CopyItemBtn.Enabled = (dr != null && focused_tree_node.CanModify == 1);
            PrintItemBtn.Enabled = (dr != null);
        }

        private void PayDocGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var dr = e.Row as GetPayDocList_Result;
            PayDocListInfoBS.DataSource = dr;

            if (dr != null)
            {
                RelPayDocGridControl.DataSource = _db.GetRelDocList(dr.DocId).OrderBy(o => o.OnDate).ToList();
            }

            var tree_row = DocsTreeList.GetDataRecordByNode(DocsTreeList.FocusedNode) as v_GetDocsTree;

            DeleteItemBtn.Enabled = (dr != null && dr.Checked == 0 && tree_row.CanDelete == 1);
            ExecuteItemBtn.Enabled = (dr != null && tree_row.CanPost == 1);
            EditItemBtn.Enabled = (dr != null && tree_row.CanModify == 1);
            CopyItemBtn.Enabled = EditItemBtn.Enabled;
            PrintItemBtn.Enabled = (dr != null);
        }

        private void PriceListGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var pl_dr = e.Row as v_PriceList;
            if (pl_dr != null)
            {
                PriceListDetBS.DataSource = _db.GetPriceListDet(pl_dr.PlId);
            }
            else PriceListDetBS.DataSource = null;


            var tree_row = DocsTreeList.GetDataRecordByNode(DocsTreeList.FocusedNode) as v_GetDocsTree;

            DeleteItemBtn.Enabled = (pl_focused_row != null && tree_row.CanDelete == 1);
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = (pl_focused_row != null && tree_row.CanModify == 1);
            CopyItemBtn.Enabled = EditItemBtn.Enabled;
            PrintItemBtn.Enabled = (pl_focused_row != null);
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
                                   WType = -5,
                                   DefNum = 1,
                                   OnDate = DBHelper.ServerDateTime(),
                                   Num = new BaseEntities().GetDocNum("wb_write_off").FirstOrDefault(),
                                   CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                                   OnValue = 1,
                                   PersonId = DBHelper.CurrentUser.KaId,
                                   WaybillMove = new WaybillMove { SourceWid = wid.Value },
                                   Nds = 0,
                                   Docs = new Docs { DocType = -5 },
                                   UpdatedBy = DBHelper.CurrentUser.UserId
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

                                var pos_in = db.GetPosIn(wb.OnDate, _wbd.MatId, _wbd.WId, 0).Where(w => w.CurRemain >= _wbd.Amount && w.PosId == det_item.PosId).FirstOrDefault();
                                if (pos_in != null)
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
                    new frmWBWriteOff(item).ShowDialog();
                }
            }
        }

        private void barSubItem1_Popup(object sender, EventArgs e)
        {
            if (wb_focused_row == null)
            {
                return;
            }

            barButtonItem11.Enabled = wb_focused_row.WType == 6;
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            var dr = WbGridView.GetFocusedRow() as GetWayBillList_Result;
          
            if (dr == null)
            {
                gridControl2.DataSource = null;
                gridControl3.DataSource = null;
                WayBillListInfoBS.DataSource = null;

                return;
            }

            switch (xtraTabControl2.SelectedTabPageIndex)
            {
                case 0:
                    gridControl2.DataSource = _db.GetWaybillDetIn(dr.WbillId);
                    break;

                case 1:
                    WayBillListInfoBS.DataSource = dr;
                    break;

                case 2:
                    gridControl3.DataSource = _db.GetRelDocList(dr.DocId).OrderBy(o => o.OnDate).ToList();
                    break;
            }
          
        }
    }
}
