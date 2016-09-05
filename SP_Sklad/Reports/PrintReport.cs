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
    public class PrintReport
    {
        public DateTime OnDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public dynamic MatGroup { get; set; }
        public dynamic Kagent { get; set; }
        public dynamic Warehouse { get; set; }
        public  object MATID { get; set; }
        public String DocStr { get; set; }

        private List<object> XLRPARAMS
        {
            get
            {
                var obj = new List<object>();
                obj.Add(new
                {
                    OnDate = OnDate.ToShortDateString(),
                    StartDate = StartDate.ToShortDateString(),
                    EndDate = EndDate.ToShortDateString(),
                    GRP = MatGroup != null ? MatGroup.Name : "",
                    WH = Warehouse != null ? Warehouse.Name : "",
                    KAID = Kagent != null ? Kagent.Name : ""
                });
                return obj;
            }
        }

        public PrintReport()
        {

        }

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

        public void CreateReport(int idx)
        {
            //  if( !frmRegistrSoft->unLock ) return ;
            var db = DB.SkladBase();
            var data_for_report = new Dictionary<string, IList>();
            var rel = new List<object>();

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

                //   var ob = new List<object>();
                //     ob.Add(new { StartDate = StartDate.ToShortDateString(), EndDate = EndDate.ToShortDateString(), GRP = MatGroup.Name, WH = Warehouse.Name, KAID = Kagent.Name });
                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatInDet", mat);
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_1);
            }

            if (idx == 2)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wh = Convert.ToString(Warehouse.WId);
                var mat = db.REP_2(StartDate, EndDate, grp, (int)Kagent.KaId, wh, DocStr).ToList();

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
                    child_table = "MatOutDet"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatOutDet", mat);
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_2);
            }

            /*     if(idx == 25)
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

                */

            if (idx == 3)
            {

                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wh = Convert.ToString(Warehouse.WId);
                int kid = Convert.ToInt32(Kagent.KaId);
                var mat = db.REP_3_14(StartDate, EndDate, grp, kid, wh, "-1,").ToList();

                if (!mat.Any())
                {
                    return;
                }

                var kagents = db.Kagent.Where(w => w.Deleted == 0 && (w.KaId == kid || kid == 0)).Select(s => new { s.KaId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "KaId",
                    fk = "KaId",
                    master_table = "MatGroup",
                    child_table = "MatOutDet"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", kagents.Where(w => mat.Select(s => s.KaId).Contains(w.KaId)).ToList());
                data_for_report.Add("MatOutDet", mat);
                data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
                {
                    Amount = s.Sum(a => a.Amount),
                    Summ = s.Sum(ss => ss.Summ),
                    ReturnAmountIn = s.Sum(r => r.ReturnAmountIn),
                    ReturnSummIn = s.Sum(r => r.ReturnSummIn)
                }).ToList());
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_3);
            }

            if (idx == 4)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                int kid = Convert.ToInt32(Kagent.KaId);
                string wh = Convert.ToString(Warehouse.WId);
                var mat = db.REP_4_25(StartDate, EndDate, grp, kid, wh, "1,").ToList();

                if (!mat.Any())
                {
                    return;
                }

                var kagents = db.Kagent.Where(w => w.Deleted == 0 && (w.KaId == kid || kid == 0)).Select(s => new { s.KaId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "KaId",
                    fk = "KaId",
                    master_table = "MatGroup",
                    child_table = "MatInDet"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", kagents.Where(w => mat.Select(s => s.KaId).Contains(w.KaId)).ToList());
                data_for_report.Add("MatInDet", mat);
                data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
                {
                    SummPrice = s.Sum(r => r.SummPrice),
                    ReturnSummPriceOut = s.Sum(r => r.ReturnSummPriceOut)
                }).ToList());
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_4);
            }

            if (idx == 5)
            {
                var kagents = db.Kagent.Where(w => w.Deleted == 0 && (w.Archived ?? 0) == 0 ).Select(s => new
                {
                    s.Name,
                    Saldo = db.KAgentSaldo.Where(ks => ks.KAId == s.KaId && ks.OnDate <= OnDate).OrderByDescending(o => o.OnDate).Select(kss => kss.Saldo).Take(1).FirstOrDefault()
                }).ToList().Where(w => w.Saldo > 0);

                if (!kagents.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("Kagent", kagents.ToList());

                Print(data_for_report, TemlateList.rep_5);
            }

            if (idx == 6)
            {
                var kagents = db.Kagent.Where(w => w.Deleted == 0 && (w.Archived ?? 0) == 0).Select(s => new
                {
                    s.Name,
                    Saldo = db.KAgentSaldo.Where(ks => ks.KAId == s.KaId && ks.OnDate <= OnDate).OrderByDescending(o => o.OnDate).Select(kss => kss.Saldo).Take(1).FirstOrDefault()
                }).ToList().Where(w => w.Saldo < 0);

                if (!kagents.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("Kagent", kagents.ToList());

                Print(data_for_report, TemlateList.rep_6);
            }

            if (idx == 7)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                int wid = Warehouse.WId == "*" ? 0 : Convert.ToInt32(Warehouse.WId);

                var mat = db.WhMatGet(grp, wid, 0, OnDate, 0, "*", 0, "", 0, 0).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == grp || grp == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "OutGrpId",
                    master_table = "MatGroup",
                    child_table = "MatList"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.OutGrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatList", mat);
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_7);


                /*
                SP_WMAT_GET->DataSource = MatGroupDS;
                MatGroup->ParamByName("grp")->Value = GRP;

                SP_WMAT_GET->ParamByName("ONDATE")->Value = OnDate;
                SP_WMAT_GET->ParamByName("GRPID")->Value = GRP;
                if (WH == "*") SP_WMAT_GET->ParamByName("WID")->Value = 0;
                else SP_WMAT_GET->ParamByName("WID")->Value = WH;
                SP_WMAT_GET->ParamByName("MINREST")->Value = 0;

                xlReport_7->Params->Items[0]->Value = SkladData->WhComboBoxNAME->Value;
                xlReport_7->Params->Items[1]->Value = OnDate;
                xlReport_7->Params->Items[2]->Value = SkladData->MatGroupComboBoxNAME->Value;
                xlReport_7->Report();
                SP_WMAT_GET->DataSource = NULL;
                 */
            }

            /*      if(idx == 18)
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


        private void Print(Dictionary<string, IList> data_for_report, string temlate)
        {

            String result_file = Path.Combine(rep_path, temlate);
            String template_file = Path.Combine(template_path, temlate);
            if (File.Exists(template_file))
            {
                ReportBuilder.GenerateReport(data_for_report, template_file, result_file, false);
            }

            if (File.Exists(result_file))
            {
                Process.Start(result_file);
            }
        }

    }
}
