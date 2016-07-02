using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP_Sklad.SkladData;
using SpreadsheetReportBuilder;

namespace SP_Sklad.Reports
{
    class PrintReport
    {
        public static string template_path
        {
            get
            {
#if DEBUG
                return Path.Combine(@"c:\WinVSProjects\SP-Sklad\SP_Sklad\", "TempLate");
#else
               return Path.Combine(Application.StartupPath, "TempLate" );
#endif
            }
        }

        public static string rep_path
        {
            get
            {
#if DEBUG
                return Path.Combine(@"c:\WinVSProjects\SP-Sklad\SP_Sklad\", "Rep");
#else
               return Path.Combine(Application.StartupPath, "Rep" );
#endif
            }
        }

        public static void CreateReport(int idx, DateTime OnDate, DateTime StartDate, DateTime EndDate, dynamic MatGroup, dynamic Kagent, dynamic Warehouse, object MATID, String DocStr)
        {
            //  if( !frmRegistrSoft->unLock ) return ;
            var db = DB.SkladBase();
            var dataForReport = new Dictionary<string, IList>();
            List<Object> rel = new List<object>();

            if (idx == 1)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wh = Convert.ToString(Warehouse.WId);
                var mat = db.REP_1(StartDate, EndDate, grp, (int)Kagent.KaId, wh, DocStr).ToList();
                
                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == grp || grp == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "MatInDet"
                });

                List<Object> ob = new List<object>();
                ob.Add(new { StartDate = StartDate.ToShortDateString(), EndDate = EndDate.ToShortDateString(), GRP = MatGroup.Name, WH = Warehouse.Name, KAID = Kagent.Name });
                dataForReport.Add("XLRPARAMS", ob);
                dataForReport.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                dataForReport.Add("MatInDet", mat);
                dataForReport.Add("_realation_", rel);

                String result_file = Path.Combine(rep_path, TemlateList.rep_1);
                String template_file = Path.Combine(template_path, TemlateList.rep_1);
                if (File.Exists(template_file))
                {
                    ReportBuilder.GenerateReport(dataForReport, template_file, result_file, false);
                }

                if (File.Exists(result_file))
                {
                    Process.Start(result_file);
                }
            }
            /*
               if(idx == 2)
                {
                   MatGroup->ParamByName("grp")->Value = GRP ;

                   MatOut_2->DataSource = MatGroupDS ;
                   MatOut_2->ParamByName("IN_KAID")->Value = KAID ;
                   MatOut_2->ParamByName("IN_WH")->Value = WH;
                   MatOut_2->ParamByName("IN_FROMDATE")->Value = StartDate ;
                   MatOut_2->ParamByName("IN_TODATE")->Value =  EndDate ;
                   MatOut_2->ParamByName("IN_TYPE")->Value = DocStr ;

                   xlReport_2->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_2->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_2->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_2->Params->Items[3]->Value = SkladData->WhComboBoxNAME->Value;
                   xlReport_2->Params->Items[4]->Value = SkladData->MatGroupComboBoxNAME->Value;
                   xlReport_2->Report();

                   MatOut_2->DataSource = NULL ;
                }

               if(idx == 25)
                {
                   MatGroup->ParamByName("grp")->Value = GRP ;

                   MatInDet_4_25->DataSource = MatGroupDS ;
                   MatInDet_4_25->ParamByName("in_fromdate")->Value = StartDate ;
                   MatInDet_4_25->ParamByName("in_todate")->Value =  EndDate ;
                   MatInDet_4_25->ParamByName("KAID")->Value = KAID ;
                   MatInDet_4_25->ParamByName("in_wid")->Value = WH ;
                   MatInDet_4_25->ParamByName("IN_TYPE")->Value = DocStr ;

                   xlReport_25->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_25->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_25->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_25->Params->Items[3]->Value = SkladData->WhComboBoxNAME->Value;
                   xlReport_25->Params->Items[4]->Value = SkladData->MatGroupComboBoxNAME->Value;
                   xlReport_25->Report();
                   MatInDet_4_25->DataSource = NULL ;
                }

                if(idx == 14)
                {
                   MatGroup->ParamByName("grp")->Value = GRP ;

                   MatInDet_3_14->DataSource = MatGroupDS ;
                   MatInDet_3_14->ParamByName("in_fromdate")->Value = StartDate ;
                   MatInDet_3_14->ParamByName("in_todate")->Value =  EndDate ;
                   MatInDet_3_14->ParamByName("KAID")->Value = KAID ;
                   MatInDet_3_14->ParamByName("in_wid")->Value = WH ;
                   MatInDet_3_14->ParamByName("IN_TYPE")->Value = DocStr ;

                   xlReport_14->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_14->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_14->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_14->Params->Items[3]->Value = SkladData->WhComboBoxNAME->Value;
                   xlReport_14->Params->Items[4]->Value = SkladData->MatGroupComboBoxNAME->Value;
                   xlReport_14->Report();
                   MatInDet_3_14->DataSource = NULL ;
                }

                if(idx == 4)
                {
                   KAgent->ParamByName("kaid")->Value = KAID ;

                   MatInDet_4_25->DataSource =  KAgentDS ;
                   MatInDet_4_25->ParamByName("in_fromdate")->Value = StartDate ;
                   MatInDet_4_25->ParamByName("in_todate")->Value =  EndDate ;
                   MatInDet_4_25->ParamByName("GRPID")->Value = GRP ;
                   MatInDet_4_25->ParamByName("in_wid")->Value = WH ;
                   MatInDet_4_25->ParamByName("IN_TYPE")->Value = DocStr ;

                   xlReport_4->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_4->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_4->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_4->Params->Items[3]->Value = SkladData->WhComboBoxNAME->Value;
                   xlReport_4->Params->Items[4]->Value = SkladData->MatGroupComboBoxNAME->Value;
                   xlReport_4->Report();
                   MatInDet_4_25->DataSource = NULL ;
                }

               if(idx == 3)
                {
                   KAgent->ParamByName("kaid")->Value = KAID ;

                   MatInDet_3_14->DataSource =  KAgentDS ;
                   MatInDet_3_14->ParamByName("in_fromdate")->Value = StartDate ;
                   MatInDet_3_14->ParamByName("in_todate")->Value =  EndDate ;
                   MatInDet_3_14->ParamByName("GRPID")->Value = GRP ;
                   MatInDet_3_14->ParamByName("in_wid")->Value = WH ;
                   MatInDet_3_14->ParamByName("IN_TYPE")->Value = DocStr ;

                   xlReport_3->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_3->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_3->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_3->Params->Items[3]->Value = SkladData->WhComboBoxNAME->Value;
                   xlReport_3->Params->Items[4]->Value = SkladData->MatGroupComboBoxNAME->Value;
                   xlReport_3->Report();
                   MatInDet_3_14->DataSource = NULL ;
                }

               if(idx == 5)
                {
                    KAgent->ParamByName("WDATE")->Value = OnDate ;
                    KAgent->ParamByName("kaid")->Value = KAID ;
                    KAgent->Filter = "SALDO > 0 and SALDO is not null" ;
                    xlReport_5->Params->Items[0]->Value = OnDate.DateString() ;
                    xlReport_5->Report();
                }

               if(idx == 6)
                {
                    KAgent->ParamByName("WDATE")->Value = OnDate ;
                    KAgent->ParamByName("kaid")->Value = KAID ;
                    KAgent->Filter = "SALDO < 0 and SALDO is not null" ;
                    xlReport_6->Params->Items[0]->Value = OnDate.DateString() ;
                    xlReport_6->Report();
                }

               if(idx == 7)
                {
                   SP_WMAT_GET->DataSource = MatGroupDS ;
                   MatGroup->ParamByName("grp")->Value = GRP ;

                   SP_WMAT_GET->ParamByName("ONDATE")->Value = OnDate ;
                   SP_WMAT_GET->ParamByName("GRPID")->Value = GRP ;
                   if(WH == "*" ) SP_WMAT_GET->ParamByName("WID")->Value = 0 ;
                      else SP_WMAT_GET->ParamByName("WID")->Value = WH;
                   SP_WMAT_GET->ParamByName("MINREST")->Value = 0 ;

                   xlReport_7->Params->Items[0]->Value = SkladData->WhComboBoxNAME->Value;
                   xlReport_7->Params->Items[1]->Value = OnDate;
                   xlReport_7->Params->Items[2]->Value = SkladData->MatGroupComboBoxNAME->Value;
                   xlReport_7->Report();
                   SP_WMAT_GET->DataSource = NULL ;
                }

               if(idx == 18)
                {
                   SP_WMAT_GET->DataSource = MatGroupDS ;
                   MatGroup->ParamByName("grp")->Value = GRP ;

                   SP_WMAT_GET->ParamByName("ONDATE")->Value = OnDate ;
                   SP_WMAT_GET->ParamByName("GRPID")->Value = GRP ;
                   if(WH == "*" ) SP_WMAT_GET->ParamByName("WID")->Value = 0 ;
                      else SP_WMAT_GET->ParamByName("WID")->Value = WH;
                   SP_WMAT_GET->ParamByName("MINREST")->Value = 1 ;

                   xlReport_18->Params->Items[0]->Value = SkladData->WhComboBoxNAME->Value;
                   xlReport_18->Params->Items[1]->Value = OnDate;
                   xlReport_18->Params->Items[2]->Value = SkladData->MatGroupComboBoxNAME->Value;
                   xlReport_18->Report();
                   SP_WMAT_GET->DataSource = NULL ;
                }

               if(idx == 8)
                {
                   DocList->ParamByName("IN_KAID")->Value = KAID ;
                   DocList->ParamByName("IN_WTYPE")->Value = 0;
                   DocList->ParamByName("IN_FROMDATE")->Value = StartDate ;
                   DocList->ParamByName("IN_TODATE")->Value =  EndDate ;
                   xlReport_8->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_8->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_8->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_8->Report();
                }

                if(idx == 11)
                {
                   DocList->ParamByName("IN_KAID")->Value = 0;
                   DocList->ParamByName("IN_WTYPE")->Value = SkladData->DocTypeComboBoxDOCTYPE->Value;
                   DocList->ParamByName("IN_FROMDATE")->Value = StartDate ;
                   DocList->ParamByName("IN_TODATE")->Value =  EndDate ;
                   xlReport_11->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_11->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_11->Report();
                }

               if(idx == 9)
                {
                   MatMove->ParamByName("KAID_IN")->Value = KAID ;
                   MatMove->ParamByName("MATID")->Value = MATID;
                   if(WH == "*") WH = 0 ;
                   MatMove->ParamByName("WID_IN")->Value = WH;
                   MatMove->ParamByName("FROMDATE")->Value = StartDate ;
                   MatMove->ParamByName("TODATE")->Value =  EndDate ;
                   MatMove->ParamByName("WTYPE_IN")->Value =  SkladData->DocTypeComboBoxDOCTYPE->Value;
                   xlReport_9->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_9->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_9->Params->Items[2]->Value = SkladData->MatComboBoxNAME->Value;
                   xlReport_9->Params->Items[3]->Value = SkladData->WhComboBoxNAME->Value;
                   xlReport_9->Params->Items[4]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_9->Report();
                }

               if(idx == 19)
                {
                   MatMove->ParamByName("KAID_IN")->Value = KAID ;
                   MatMove->ParamByName("MATID")->Value = MATID;
                   if(WH == "*") WH = 0 ;
                   MatMove->ParamByName("WID_IN")->Value = WH;
                   MatMove->ParamByName("FROMDATE")->Value = StartDate ;
                   MatMove->ParamByName("TODATE")->Value =  EndDate ;
                   MatMove->ParamByName("WTYPE_IN")->Value =  SkladData->DocTypeComboBoxDOCTYPE->Value;

                   Balances->ParamByName("MATID")->Value = MATID;
                   Balances->ParamByName("WID_IN")->Value = WH;
                   Balances->ParamByName("KAID_IN")->Value = KAID ;
                   Balances->ParamByName("FROMDATE")->Value = StartDate ;
                   Balances->ParamByName("TODATE")->Value =  EndDate ;
                   Balances->ParamByName("WH")->Value =  "*" ;

                   xlReport_19->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_19->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_19->Params->Items[2]->Value = SkladData->MatComboBoxNAME->Value;
                   xlReport_19->Params->Items[3]->Value = SkladData->WhComboBoxNAME->Value;
                   xlReport_19->Params->Items[4]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_19->Report();
                }

               if(idx == 10)
                {
                   Oborotka->DataSource = MatGroupDS ;
                   MatGroup->ParamByName("grp")->Value = GRP ;
                   Oborotka->ParamByName("WH")->Value = WH;
                   Oborotka->ParamByName("FROMDATE")->Value = StartDate ;
                   Oborotka->ParamByName("TODATE")->Value =  EndDate ;
                   Oborotka->ParamByName("SHOWALLMAT")->Value = frmReport->ShAllMatCheckBox->Checked  ;
                   xlReport_10->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_10->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_10->Params->Items[2]->Value = SkladData->MatGroupComboBoxNAME->Value;
                   xlReport_10->Params->Items[3]->Value = SkladData->WhComboBoxNAME->Value;
                   xlReport_10->Report();
                   Oborotka->DataSource = NULL ;
                }


               if(idx == 13)
                {
                   MatSelPr->DataSource = MatGroupDS ;
                   MatGroup->ParamByName("grp")->Value = GRP ;

                   MatSelPr->ParamByName("IN_WID")->Value = WH;
                   MatSelPr->ParamByName("IN_FROMDATE")->Value = StartDate ;
                   MatSelPr->ParamByName("IN_TODATE")->Value =  EndDate ;
                   MatSelPr->ParamByName("IN_KAID")->Value =  KAID ;
                   MatSelPr->ParamByName("ONLYRETURN")->Value =  0 ;

                   xlReport_13->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_13->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_13->Params->Items[2]->Value = SkladData->MatGroupComboBoxNAME->Value;
                   xlReport_13->Params->Items[3]->Value = SkladData->WhComboBoxNAME->Value;
                   xlReport_13->Params->Items[4]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_13->Report();
                   MatSelPr->DataSource = NULL ;
                }

               if(idx == 26)
                {
                   MakedProduct->ParamByName("in_fromdate")->Value = StartDate ;
                   MakedProduct->ParamByName("in_todate")->Value =  EndDate ;
                   MakedProduct->ParamByName("GRPID")->Value = GRP ;
                   MakedProduct->ParamByName("WH")->Value = WH ;
                   xlReport_26->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_26->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_26->Params->Items[2]->Value = SkladData->MatGroupComboBoxNAME->Value;
                   xlReport_26->Params->Items[3]->Value = SkladData->WhComboBoxNAME->Value;
                   xlReport_26->Report();
                }

                if(idx == 27)
                {
                   RepOrdKAID->ParamByName("IN_FROMDATE")->Value = StartDate;
                   RepOrdKAID->ParamByName("IN_TODATE")->Value =  EndDate;
                   RepOrdKAID->ParamByName("GRPID")->Value = GRP;
                   RepOrdKAID->ParamByName("IN_KAID")->Value = KAID;
                   RepOrdKAID->ParamByName("IN_MATID")->Value = MATID;
                   xlReport_27->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_27->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_27->Params->Items[2]->Value = SkladData->MatGroupComboBoxNAME->Value;
                   xlReport_27->Params->Items[3]->Value = SkladData->MatComboBoxNAME->Value;
                   xlReport_27->Params->Items[4]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_27->Report();
                }

               if(idx == 20)
                {
                   SvcGroup->ParamByName("grp")->Value = GRP ;

                   SvcOut_20->DataSource = SvcGroupDS ;
                   SvcOut_20->ParamByName("IN_KAID")->Value = KAID ;
                   SvcOut_20->ParamByName("IN_FROMDATE")->Value = StartDate ;
                   SvcOut_20->ParamByName("IN_TODATE")->Value =  EndDate ;

                   xlReport_20->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_20->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_20->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_20->Params->Items[3]->Value = SkladData->SvcGroupComboBoxNAME->Value;
                   xlReport_20->Report();

                   SvcOut_20->DataSource = NULL ;
                }

               if(idx == 16)
                {
                   DocList_16->ParamByName("IN_KAID")->Value = KAID ;
                   DocList_16->ParamByName("IN_FROMDATE")->Value = StartDate ;
                   DocList_16->ParamByName("IN_TODATE")->Value =  EndDate ;
                   DocList_16->ParamByName("IN_ctype")->Value = SkladData->ChTypeComboBoxCTYPEID->Value ;
                   DocList_16->ParamByName("showall")->Value = 1 ;

                   xlReport_16->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_16->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_16->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_16->Params->Items[3]->Value = SkladData->ChTypeComboBoxNAME->Value;
                   xlReport_16->Report();
                }

                if(idx == 17)
                {
                   MatGroup->ParamByName("grp")->Value = GRP ;
                   MatSelPr->DataSource = MatGroupDS ;
                   MatSelPr->ParamByName("IN_WID")->Value = WH;
                   MatSelPr->ParamByName("IN_FROMDATE")->Value = StartDate ;
                   MatSelPr->ParamByName("IN_TODATE")->Value =  EndDate ;
                   MatSelPr->ParamByName("IN_KAID")->Value =  0 ;

                   SvcGroup->ParamByName("grp")->Value = GRP ;
                   SvcOut_20->DataSource = SvcGroupDS ;
                   SvcOut_20->ParamByName("IN_KAID")->Value = 0 ;
                   SvcOut_20->ParamByName("IN_FROMDATE")->Value = StartDate ;
                   SvcOut_20->ParamByName("IN_TODATE")->Value =  EndDate ;

                   DocList_16->ParamByName("IN_KAID")->Value = 0 ;
                   DocList_16->ParamByName("IN_FROMDATE")->Value = StartDate ;
                   DocList_16->ParamByName("IN_TODATE")->Value =  EndDate ;
                   DocList_16->ParamByName("IN_ctype")->Value = 0 ;
                   DocList_16->ParamByName("showall")->Value = 0 ;

                  // WBWriteOff->DataSource = MatGroupDS ;
                   WBWriteOff->ParamByName("IN_FROMDATE")->Value = StartDate ;
                   WBWriteOff->ParamByName("IN_TODATE")->Value =  EndDate ;

                   xlReport_17->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_17->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_17->Report();

                   MatSelPr->DataSource = NULL ;
                   SvcOut_20->DataSource = NULL ;
                   WBWriteOff->DataSource = NULL ;
                }

                if(idx == 23)
                {
                   PayDocList->ParamByName("IN_FROMDATE")->Value = StartDate ;
                   PayDocList->ParamByName("IN_TODATE")->Value =  EndDate ;
                   MONEY_ONDATE->ParamByName("IN_ONDATE")->Value =  EndDate ;

                   xlReport_23->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_23->Params->Items[1]->Value = EndDate.DateString() ;
                 //  xlReport_16->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                 //  xlReport_16->Params->Items[3]->Value = SkladData->ChTypeComboBoxNAME->Value;
                   xlReport_23->Report();
                }

                if(idx == 28)
                {
                   MatGroup->ParamByName("grp")->Value = GRP ;

                   ORDERED_LIST->DataSource = MatGroupDS ;
                   ORDERED_LIST->ParamByName("IN_FROMDATE")->Value = StartDate ;
                   ORDERED_LIST->ParamByName("IN_TODATE")->Value =  EndDate ;
                   ORDERED_LIST->ParamByName("IN_KAID")->Value = KAID ;

                   xlReport_28->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_28->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_28->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_28->Report();

                   ORDERED_LIST->DataSource = NULL ;
                }
                if(idx == 29)
                {
                   MatGroup->ParamByName("grp")->Value = GRP ;

                   MakedProductShort_29->DataSource = MatGroupDS ;
                   MakedProductShort_29->ParamByName("in_fromdate")->Value = StartDate ;
                   MakedProductShort_29->ParamByName("in_todate")->Value =  EndDate ;
                   MakedProductShort_29->ParamByName("IN_KAID")->Value = KAID ;
                   MakedProductShort_29->ParamByName("IN_WH")->Value = WH ;

                   xlReport_29->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_29->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_29->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_29->Params->Items[3]->Value = SkladData->WhComboBoxNAME->Value;
                   xlReport_29->Params->Items[4]->Value = SkladData->MatGroupComboBoxNAME->Value;
                   xlReport_29->Report();

                   MakedProductShort_29->DataSource = NULL ;
                }
               if(idx == 30)
                {
                   Shahmatka->ParamByName("IN_KAID")->Value = KAID ;
                   Shahmatka->ParamByName("IN_WTYPE")->Value = 0;
                   Shahmatka->ParamByName("IN_FROMDATE")->Value = StartDate ;
                   Shahmatka->ParamByName("IN_TODATE")->Value =  EndDate ;
                   xlReport_30->Params->Items[0]->Value = StartDate.DateString() ;
                   xlReport_30->Params->Items[1]->Value = EndDate.DateString() ;
                   xlReport_30->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                   xlReport_30->Report();
                }*/


      //      db.PrintLog.Add(new PrintLog { PrintType = 1, RepId = idx, UserId = DBHelper.CurrentUser.UserId, OnDate = DateTime.Now });
       //     db.SaveChanges();

        }




    }
}
