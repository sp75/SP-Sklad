using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraGrid;
using DevExpress.XtraTreeList.Nodes;
using SP_Sklad.MainTabs;
using SP_Sklad.SkladData;

namespace SP_Sklad.Common
{
    public static class FindDoc
    {
        private static int PageIndex
        {
            set
            {
                mainForm.main_form.mainTabControl.SelectedTabPageIndex = value;
            }
        }

        private static Guid _doc_id { get; set; }
        private static DateTime _on_date { get; set; }

        private static DocsUserControl _docs_user_control
        {
            get
            {
              return  mainForm.main_form.docsUserControl1;
            }
        }

        private static ManufacturingUserControl _manufacturing_user_control
        {
            get
            {
                return mainForm.main_form.manufacturingUserControl1;
            }
        }

        private static WarehouseUserControl _wh_user_control
        {
            get
            {
                return mainForm.main_form.whUserControl;
            }
        }

        private static void SetDocFilter( int node_id)
        {
            PageIndex = 0;

          /*  if (_docs_user_control.wbStartDate.DateTime > _on_date)*/ _docs_user_control.wbStartDate.DateTime = _on_date.Date;
           /* if (_docs_user_control.wbEndDate.DateTime < _on_date)*/ _docs_user_control.wbEndDate.DateTime = _on_date.Date.SetEndDay();
            _docs_user_control.wbKagentList.EditValue = 0;
            _docs_user_control.wbStatusList.EditValue = -1;

            _docs_user_control.DocsTreeList.FocusedNode = _docs_user_control.DocsTreeList.FindNodeByFieldValue("Id", node_id);

            int rowHandle = _docs_user_control.WbGridView.LocateByValue("Id", _doc_id);
            if (rowHandle != GridControl.InvalidRowHandle)
            {
                _docs_user_control.WbGridView.FocusedRowHandle = rowHandle;
            }
        }

        private static void SetPayDocFilter(int node_id)
        {
            PageIndex = 0;

         /*   if (_docs_user_control.PDStartDate.DateTime > _on_date) */_docs_user_control.PDStartDate.DateTime = _on_date.Date;
          /*  if (_docs_user_control.PDEndDate.DateTime < _on_date)*/ _docs_user_control.PDEndDate.DateTime = _on_date.Date.SetEndDay(); 
            _docs_user_control.PDKagentList.EditValue = 0;
            _docs_user_control.PDSatusList.EditValue = -1;

            _docs_user_control.DocsTreeList.FocusedNode = _docs_user_control.DocsTreeList.FindNodeByFieldValue("Id", node_id);

            int rowHandle = _docs_user_control.PayDocGridView.LocateByValue("Id", _doc_id);
            if (rowHandle != GridControl.InvalidRowHandle)
            {
                _docs_user_control.PayDocGridView.FocusedRowHandle = rowHandle;
            }
        }

        private static void SetWhDocFilter(int node_id)
        {
            PageIndex = 2;

         /*   if (_wh_user_control.wbStartDate.DateTime > _on_date)*/ _wh_user_control.wbStartDate.DateTime = _on_date.Date;
          /*  if (_wh_user_control.wbEndDate.DateTime < _on_date)*/ _wh_user_control.wbEndDate.DateTime = _on_date.Date.SetEndDay();
            _wh_user_control.WhComboBox.EditValue = "*";
            _wh_user_control.wbSatusList.EditValue = -1;

            _wh_user_control.WHTreeList.FocusedNode = _wh_user_control.WHTreeList.FindNodeByFieldValue("Id", node_id);
            _wh_user_control.RefrechItemBtn.PerformClick();

            int rowHandle = _wh_user_control.WbGridView.LocateByValue("Id", _doc_id);
            if (rowHandle != GridControl.InvalidRowHandle)
            {
                _wh_user_control.WbGridView.FocusedRowHandle = rowHandle;
            }
        }

        public static void Find(Guid? doc_id, int? doc_type, DateTime? date)
        {
            _doc_id = doc_id.Value;
            _on_date = date.Value;

            switch (doc_type)
            {
                case 1:
                    PageIndex = 0;
                    _docs_user_control.DocsTreeList.FocusedNode = _docs_user_control.DocsTreeList.FindNodeByFieldValue("Id", 26);
                    _docs_user_control.wayBillInUserControl.FindItem(doc_id.Value, date.Value);
                  

                 //   SetDocFilter(26);
                    break;

                case -1:
                    PageIndex = 0;
                    _docs_user_control.DocsTreeList.FocusedNode = _docs_user_control.DocsTreeList.FindNodeByFieldValue("Id", 27);
                    _docs_user_control.ucWaybillOut.FindItem(doc_id.Value, date.Value);
                    //SetDocFilter(27);
                    break;

                case 6: SetDocFilter(57);
                    break;

                case -6: SetDocFilter(56);
                    break;

                case 3: SetPayDocFilter(29);
                    break;

                case -3: SetPayDocFilter(30);
                    break;

                case -2:
                    SetPayDocFilter(103);
                    break;

                case 2:

                    PageIndex = 0;
                    _docs_user_control.DocsTreeList.FocusedNode = _docs_user_control.DocsTreeList.FindNodeByFieldValue("Id", 39);
                    _docs_user_control.ucInvoices.FindItem(doc_id.Value, date.Value);
                   // SetDocFilter(39);
                    break;

                case -16:
                    PageIndex = 0;
                    _docs_user_control.DocsTreeList.FocusedNode = _docs_user_control.DocsTreeList.FindNodeByFieldValue("Id", 107);
                    _docs_user_control.ucWBOrdersIn.FindItem(doc_id.Value, date.Value);
                    //SetDocFilter(107);
                    break;

                case 16:
                    PageIndex = 0;
                    _docs_user_control.DocsTreeList.FocusedNode = _docs_user_control.DocsTreeList.FindNodeByFieldValue("Id", 108);
                    _docs_user_control.ucWBOrdersOut.FindItem(doc_id.Value, date.Value);
                 //  SetDocFilter(108);
                    break;

                case 8:/* MainPageControl->ActivePage = DocumentsTabSheet;
                    frmDocumentsPanel->DocsTreeData->Locate("ID", 46, TLocateOptions());
                    findRecult = frmDocumentsPanel->ContractsList->Locate("DOCID", DOCID, TLocateOptions());
                    frmDocumentsPanel->cxGrid10->SetFocus();*/
                    break;

                case -8: /*MainPageControl->ActivePage = DocumentsTabSheet;
                    frmDocumentsPanel->DocsTreeData->Locate("ID", 47, TLocateOptions());
                    findRecult = frmDocumentsPanel->ContractsList->Locate("DOCID", DOCID, TLocateOptions());
                    frmDocumentsPanel->cxGrid10->SetFocus();*/
                    break;

                case -7: /*if (frmDocumentsPanel->TaxWBStartDate->Date > date) frmDocumentsPanel->TaxWBStartDate->Date = date;
                    MainPageControl->ActivePage = DocumentsTabSheet;
                    frmDocumentsPanel->DocsTreeData->Locate("ID", 44, TLocateOptions());
                    findRecult = frmDocumentsPanel->TaxWBList->Locate("DOCID", DOCID, TLocateOptions());
                    frmDocumentsPanel->cxGrid13->SetFocus();*/
                    break;

                case 4: SetWhDocFilter(48);
                    break;

                case -5: SetWhDocFilter(54);
                    break;

                case 5: SetWhDocFilter(58);
                    break;

                case 7: SetWhDocFilter(104);
                    break;

                case -20:
                    PageIndex = 1;

               /*     if (_manufacturing_user_control.wbStartDate.DateTime > _on_date)*/ _manufacturing_user_control.wbStartDate.DateTime = _on_date.Date;
                  /*  if (_manufacturing_user_control.wbEndDate.DateTime < _on_date)*/ _manufacturing_user_control.wbEndDate.DateTime = _on_date.Date.SetEndDay(); 
                    _manufacturing_user_control.WhComboBox.EditValue = "*";
                    _manufacturing_user_control.wbSatusList.EditValue = -1;

                    _manufacturing_user_control.DocsTreeList.FocusedNode = _manufacturing_user_control.DocsTreeList.FindNodeByFieldValue("Id", 111);

                    int rowHandle = _manufacturing_user_control.WbGridView.LocateByValue("Id", _doc_id);
                    if (rowHandle != GridControl.InvalidRowHandle)
                    {
                        _manufacturing_user_control.WbGridView.FocusedRowHandle = rowHandle;
                    }
                    break;

                case -22:
                    PageIndex = 1;

                /*    if (_manufacturing_user_control.DebStartDate.DateTime > _on_date) */_manufacturing_user_control.DebStartDate.DateTime = _on_date.Date;
                  /*  if (_manufacturing_user_control.DebEndDate.DateTime < _on_date)*/ _manufacturing_user_control.DebEndDate.DateTime = _on_date.Date.SetEndDay(); 
                    _manufacturing_user_control.DebWhComboBox.EditValue = "*";
                    _manufacturing_user_control.DebSatusList.EditValue = -1;

                    _manufacturing_user_control.DocsTreeList.FocusedNode = _manufacturing_user_control.DocsTreeList.FindNodeByFieldValue("Id", 114);

                    int rowHandle2 = _manufacturing_user_control.DeboningGridView.LocateByValue("Id", _doc_id);
                    if (rowHandle2 != GridControl.InvalidRowHandle)
                    {
                        _manufacturing_user_control.DeboningGridView.FocusedRowHandle = rowHandle2;
                    }
                    break;
                case 20:
                    PageIndex = 1;

                   /* if (_manufacturing_user_control.PlanStartDate.DateTime > _on_date)*/ _manufacturing_user_control.PlanStartDate.DateTime = _on_date.Date;
                   /* if (_manufacturing_user_control.PlanEndDate.DateTime < _on_date)*/ _manufacturing_user_control.PlanEndDate.DateTime = _on_date.Date.SetEndDay();
                    _manufacturing_user_control.lookUpEdit2.EditValue = -1;

                    _manufacturing_user_control.DocsTreeList.FocusedNode = _manufacturing_user_control.DocsTreeList.FindNodeByFieldValue("Id", 116);

                    int rowHandle3 = _manufacturing_user_control.ProductionPlansGridView.LocateByValue("Id", _doc_id);
                    if (rowHandle3 != GridControl.InvalidRowHandle)
                    {
                        _manufacturing_user_control.ProductionPlansGridView.FocusedRowHandle = rowHandle3;
                    }
                    break;

                case 32:
                    PageIndex = 0;

                    mainForm.main_form.docsUserControl1.expeditionUserControl1.find_id = _doc_id;
                    _docs_user_control.DocsTreeList.FocusedNode = _docs_user_control.DocsTreeList.FindNodeByFieldValue("Id", 145);

                    break;
            }
        }
    }
}
