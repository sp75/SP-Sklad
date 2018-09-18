using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP_Sklad.Common;
using SP_Sklad.Reports.XtraRep;
using SP_Sklad.SkladData;
using SpreadsheetReportBuilder;
using DevExpress.XtraReports.UI;

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
        public dynamic Material { get; set; }
        public String DocStr { get; set; }
        public object DocType { get; set; }
        public dynamic ChType { get; set; }
        public object Status { get; set; }
        public dynamic KontragentGroup { get; set; }
        private int? _person_id { get; set; }
        private int _user_id  { get; set; }
        public String GrpStr { get; set; }
        public dynamic Person { get; set; }

        private List<XLRPARAM> XLRPARAMS
        {
            get
            {
                var obj = new List<XLRPARAM>();
                obj.Add(new XLRPARAM
                {
                    OnDate = OnDate.ToShortDateString(),
                    StartDate = StartDate.ToShortDateString(),
                    EndDate = EndDate.ToShortDateString(),
                    GRP = MatGroup != null ? MatGroup.Name : "",
                    WH = Warehouse != null ? Warehouse.Name : "",
                    KAID = Kagent != null ? Kagent.Name : "",
                    MatId = Material != null ? Material.Name : "",
                    CType = ChType != null ? ChType.Name : "",
                    KontragentGroupName = KontragentGroup != null ? KontragentGroup.Name : "",
                });
                return obj;
            }
        }

        public class XLRPARAM
        {
            public string OnDate { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string GRP { get; set; }
            public string WH { get; set; }
            public string KAID { get; set; }
            public string MatId { get; set; }
            public string CType { get; set; }
            public string KontragentGroupName { get; set; }
        }


        public PrintReport()
        {
            _person_id = DBHelper.CurrentUser.KaId;
            _user_id = DBHelper.CurrentUser.UserId;
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
                var mat = db.REP_1(StartDate, EndDate, grp, (int)Kagent.KaId, wh, DocStr, _user_id).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = mat.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
                {
                    s.Key.GrpId,
                    Name = s.Key.GrpName,
                    Summ = s.Sum(xs => xs.SumPrice)
                }).OrderBy(o => o.Name).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "MatInDet"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp);
                data_for_report.Add("MatInDet", mat);
                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_1);
            }

            if (idx == 2)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wh = Convert.ToString(Warehouse.WId);
                int status = Convert.ToInt32(Status);
                var mat = db.REP_2(StartDate, EndDate, grp, (int)Kagent.KaId, wh, DocStr, status, _user_id).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = mat.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
                {
                    s.Key.GrpId,
                    Name = s.Key.GrpName,
                    Summ = s.Sum(xs => xs.SumPrice)
                }).OrderBy(o => o.Name).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "MatOutDet"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp);
                data_for_report.Add("MatOutDet", mat);
                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_2);
            }

            if (idx == 3)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wh = Convert.ToString(Warehouse.WId);
                int kid = Convert.ToInt32(Kagent.KaId);
                var mat = db.REP_3_14(StartDate, EndDate, grp, kid, wh, "-1,", _user_id).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var kagents = DBHelper.Kagents.Where(w =>  w.KaId == kid || kid == 0).Select(s => new { s.KaId, s.Name }).ToList();

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

                IHelper.Print(data_for_report, TemlateList.rep_3);
            }

            if (idx == 14)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wh = Convert.ToString(Warehouse.WId);
                int kid = Convert.ToInt32(Kagent.KaId);
                var mat = db.REP_3_14(StartDate, EndDate, grp, kid, wh, DocStr, _user_id).ToList();

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
                data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
                {
                    Amount = s.Sum(a => a.Amount),
                    Summ = s.Sum(ss => ss.Summ),
                    ReturnAmountIn = s.Sum(r => r.ReturnAmountIn),
                    ReturnSummIn = s.Sum(r => r.ReturnSummIn)
                }).ToList());
                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_3/*TemlateList.rep_14*/);
            }


            if (idx == 4)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                int kid = Convert.ToInt32(Kagent.KaId);
                string wh = Convert.ToString(Warehouse.WId);
                var mat = db.REP_4_25(StartDate, EndDate, grp, kid, wh, "1,", _user_id).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var kagents = DBHelper.Kagents.Where(w => w.KaId == kid || kid == 0).Select(s => new { s.KaId, s.Name }).ToList();

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

                IHelper.Print(data_for_report, TemlateList.rep_4);
            }

            if (idx == 25)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                int kid = Convert.ToInt32(Kagent.KaId);
                string wh = Convert.ToString(Warehouse.WId);
                var mat = db.REP_4_25(StartDate, EndDate, grp, kid, wh, DocStr, _user_id).ToList();

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

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatInDet", mat);
                data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
                {
                    SummPrice = s.Sum(r => r.SummPrice),
                    ReturnSummPriceOut = s.Sum(r => r.ReturnSummPriceOut)
                }).ToList());
                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_4);
            }


            if (idx == 5)
            {
                var kagents = db.REP_4_5(OnDate).Where(w => w.Saldo > 0).ToList().Select((s, index) => new
                {
                    N = index + 1,
                    s.Name,
                    s.Saldo
                }); 

                if (!kagents.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("Kagent", kagents.ToList());

                IHelper.Print(data_for_report, TemlateList.rep_5);
            }

            if (idx == 6)
            {
                var kagents = db.REP_4_5(OnDate).Where(w => w.Saldo < 0).ToList().Where(w => w.Saldo < 0).Select((s, index) => new
                {
                    N = index + 1,
                    s.Name,
                    s.Saldo
                }); 

                if (!kagents.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("Kagent", kagents.ToList());

                IHelper.Print(data_for_report, TemlateList.rep_6);
            }

            if (idx == 7)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                int wid = Warehouse.WId == "*" ? 0 : Convert.ToInt32(Warehouse.WId);

                var mat = db.WhMatGet(grp, wid, 0, OnDate, 0, "*", 0, GrpStr, DBHelper.CurrentUser.UserId, 0).Select(s => new
                {
                    s.BarCode,
                    s.MatName,
                    s.MsrName,
                    s.Remain,
                    s.Rsv,
                    s.AvgPrice,
                    s.OutGrpId,
                    s.GrpName,
                }).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = mat.GroupBy(g => new { g.GrpName, g.OutGrpId }).Select(s => new
                {
                    s.Key.OutGrpId,
                    Name = s.Key.GrpName,
                    Total = s.Sum(xs => (xs.AvgPrice * xs.Remain)),
                }).OrderBy(o => o.Name).ToList();

                rel.Add(new
                {
                    pk = "OutGrpId",
                    fk = "OutGrpId",
                    master_table = "MatGroup",
                    child_table = "MatList"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp);
                data_for_report.Add("MatList", mat);
                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_7);
            }

            if (idx == 8)
            {
                var list = db.GetDocList(StartDate, EndDate, (int)Kagent.KaId, 0).OrderBy(o => o.OnDate).Select(s => new
                {
                    s.Num,
                    s.TypeName,
                    s.OnDate,
                    s.SummAll,
                    s.Saldo,
                    DocName = s.TypeName + " №" + s.Num

                }).ToList();

                if (!list.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("KADocList", list.ToList());

                IHelper.Print(data_for_report, TemlateList.rep_8);
            }

            if (idx == 9)
            {
                int wid = Warehouse.WId == "*" ? 0 : Convert.ToInt32(Warehouse.WId);
                Guid kg = KontragentGroup.Id;

                var list = db.GetMatMove((int)this.Material.MatId, StartDate, EndDate, wid, (int)Kagent.KaId, (int)DocType, "*", kg, _user_id).ToList();

                if (!list.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatList", list.ToList());

                IHelper.Print(data_for_report, TemlateList.rep_9);
            }

            if (idx == 10)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wid = Convert.ToString(Warehouse.WId);

                var mat = db.REP_10(StartDate, EndDate, grp, wid, 0, _user_id).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = mat.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
                {
                    s.Key.GrpId,
                    Name = s.Key.GrpName,
                    SummIn = s.Sum(xs => xs.SummIn),
                    SummOut = s.Sum(xs => xs.SummOut),
                    SummStart = s.Sum(xs => xs.SummStart),
                    SummEnd = s.Sum(xs => xs.SummEnd)
                }).OrderBy(o => o.Name).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "MatList"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp);
                data_for_report.Add("MatList", mat);
                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_10);
            }

            if (idx == 11)
            {
                var list = db.GetDocList(StartDate, EndDate, 0, (int)DocType).OrderBy(o => o.OnDate).Select(s => new
                {
                    s.OnDate,
                    s.KaName,
                    s.SummAll,
                    s.Saldo,
                    DocName = s.TypeName + " №" + s.Num
                }).ToList();

                if (!list.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("DocList", list.ToList());

                IHelper.Print(data_for_report, TemlateList.rep_11);
             //   var rep = new DocListReport();
             //   rep.ShowPreviewDialog();
            }

            if (idx == 13)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wid = Convert.ToString(Warehouse.WId);

                var mat = db.REP_13(StartDate, EndDate, grp, (int)Kagent.KaId, (String)Warehouse.WId, 0, GrpStr).ToList();

                if (!mat.Any())
                {
                    return;
                }
                var gs =  !String.IsNullOrEmpty( GrpStr) ? GrpStr.Split(',').Select(s=> Convert.ToInt32(s)).ToList() : new List<int>();
                var mat_grp = db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == grp || grp == 0 || gs.Contains(w.GrpId))).Select(s => new { s.GrpId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "MatList"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatList", mat);
                data_for_report.Add("_realation_", rel);
                data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
                {
                    SummIn = s.Sum(r => r.SummIn),
                    SummOut = s.Sum(r => r.SummOut),
                    Income = s.Sum(r => r.Income),
                    Rentabelnist = s.Average(a => (a.SummOut - a.SummIn - a.Income) > 0 && a.Income > 0 ? a.Income / (a.SummOut - a.SummIn - a.Income) : 0)
                }).ToList());

                IHelper.Print(data_for_report, TemlateList.rep_13);
            }

            if (idx == 15)
            {
                var wb_list = db.REP_15(StartDate, EndDate, (int)Kagent.KaId, (int)this.Material.MatId).ToList();

                if (!wb_list.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("WbList", wb_list);

                IHelper.Print(data_for_report, TemlateList.rep_15);
            }

            if (idx == 16)
            {
                var paydoc = db.REP_16(StartDate, EndDate, (int)Kagent.KaId, (int)ChType.CTypeId, 1).ToList();

                if (!paydoc.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("DocList", paydoc);

                IHelper.Print(data_for_report, TemlateList.rep_16);
            }

            if (idx == 19)
            {
                int wid = Warehouse.WId == "*" ? 0 : Convert.ToInt32(Warehouse.WId);
                int mat_id = (int)this.Material.MatId;
                Guid grp_kg = KontragentGroup.Id;
                var list = db.GetMatMove((int)this.Material.MatId, StartDate, EndDate, wid, (int)Kagent.KaId, (int)DocType, "*", grp_kg, _user_id).ToList();

                if (!list.Any())
                {
                    return;
                }

                var satrt_remais = db.MatRemainByWh(mat_id, wid, (int)Kagent.KaId, StartDate, "*", DBHelper.CurrentUser.UserId).Sum(s => s.Remain);
                var sart_avg_price = db.v_MatRemains.Where(w => w.MatId == mat_id && w.OnDate <= StartDate).OrderByDescending(o => o.OnDate).Select(s => s.AvgPrice).FirstOrDefault();
                var end_remais = db.MatRemainByWh(mat_id, wid, (int)Kagent.KaId, EndDate, "*", DBHelper.CurrentUser.UserId).Sum(s => s.Remain);
                var end_avg_price = db.v_MatRemains.Where(w => w.MatId == mat_id && w.OnDate <= EndDate).OrderByDescending(o => o.OnDate).Select(s => s.AvgPrice).FirstOrDefault();

                var balances = new List<object>();
                balances.Add(new
                {
                    SARTREMAIN = satrt_remais,
                    SARTAVGPRICE = sart_avg_price,
                    ENDREMAIN = end_remais,
                    ENDAVGPRICE = end_avg_price

                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("Balances", balances);
                data_for_report.Add("MatList", list.ToList());

                IHelper.Print(data_for_report, TemlateList.rep_19);
            }

            if (idx == 20)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);

                var svc = db.REP_20(StartDate, EndDate, grp, (int)Kagent.KaId).ToList();

                if (!svc.Any())
                {
                    return;
                }

                var svc_grp = db.SvcGroup.Where(w => w.Deleted == 0 && (w.GrpId == grp || grp == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "SvcGroup",
                    child_table = "SvcOutDet"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("SvcGroup", svc_grp.Where(w => svc.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("SvcOutDet", svc);
                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_20);
            }

            if (idx == 28)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                Guid grp_kg = KontragentGroup.Id;
                var mat = db.OrderedList(StartDate, EndDate, 0, (int)Kagent.KaId, -16, 0, grp_kg).Where(w => w.GrpId == grp || grp == 0).GroupBy(g => new
                {
                    g.BarCode,
                    g.GrpId,
                    g.MatId,
                    g.MatName,
                    g.CurrencyName,
                    g.MsrName

                }).Select(s => new
                {
                    s.Key.BarCode,
                    s.Key.GrpId,
                    s.Key.MatName,
                    s.Key.MsrName,
                    Amount = s.Sum(a => a.Amount),
                    OnSum = s.Sum(sum => sum.Price * sum.Amount)

                }).ToList();

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


                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatInDet", mat);
                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_28);
            }


            if (idx == 18)
            {

                int grp = Convert.ToInt32(MatGroup.GrpId);
                int wid = Warehouse.WId == "*" ? 0 : Convert.ToInt32(Warehouse.WId);

                var mat = db.WhMatGet(grp, wid, 0, OnDate, 0, "*", 0, "", DBHelper.CurrentUser.UserId, 0).Where(w => w.Remain < w.MinReserv && w.MinReserv != null).ToList();

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

                IHelper.Print(data_for_report, TemlateList.rep_18);
            }

            if (idx == 26)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wid = Convert.ToString(Warehouse.WId);
                var make = db.WBListMake(StartDate, EndDate, 1, wid, grp, -20).ToList().Concat(db.WBListMake(StartDate, EndDate, 1, wid, grp, -22).ToList());

                if (!make.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MakedProduct", make.ToList());

                IHelper.Print(data_for_report, TemlateList.rep_26);
            }

            if (idx == 27) 
            {
                Guid grp_kg = KontragentGroup.Id;
                var mat = db.REP_27(StartDate, EndDate, (int)Kagent.KaId, (int)MatGroup.GrpId, (int)this.Material.MatId, grp_kg, (int)Person.KaId).ToList();

                if (!mat.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatList", mat.GroupBy(g => new { g.KaName,  g.MatName, g.MsrName, g.BarCode }).Select(s => new
                {
                    BarCode = s.Key.BarCode,
                    MatName = s.Key.MatName,
                    MsrName = s.Key.MsrName,
                    KaName = s.Key.KaName,
                    AmountOrd = s.Sum(su => su.AmountOrd),
                    TotalOrd = s.Sum(su => su.TotalOrd),
                    AmountOut = s.Sum(su => su.AmountOut),
                    TotalOut = s.Sum(su => su.TotalOut),
                    PersonName = String.Join(", ", s.Select(su=> su.PersonName).Distinct() )
                }).ToList());

                IHelper.Print2(data_for_report, TemlateList.rep_27);
            }


            if (idx == 31)
            {
                var sql = @"  select 
	    wbd.MatId, 
		mat.Name MatName,  
		mat.GrpId, 
		mg.Name GrpName, 
		sum(wbd.AMOUNT) AmountOrd, 
		sum(wbd.TOTAL) TotalOrd, 
        msr.shortname MsrName,  
		mat.BarCode, 
		sum(x.Amount) AmountOut, 
		sum(x.Total) TotalOut
      from WAYBILLDET wbd 
	  join  WAYBILLLIST wbl on wbl.wbillid = wbd.wbillid
      join MATERIALS mat on mat.matid = wbd.MatId
	  join MatGroup mg on mat.GrpId = mg.GrpId
      join measures msr on msr.mid=mat.mid
      join kagent kaemp on wbl.kaid = kaemp.kaid
	  outer apply ( select Amount, Total
                    from WAYBILLDET wbdo , WAYBILLLIST wblo
                    join DOCRELS dl on wblo.id = dl.OriginatorId
                    left outer join kagent kaemp on wblo.personid = kaemp.kaid
                    where wblo.wbillid = wbdo.wbillid  and  wblo.checked = 1 and wblo.wtype = -1
                          and wblo.id = dl.OriginatorId and dl.RelOriginatorId = wbl.id  and  wbdo.matid =wbd.MATID) x
      where wbl.wbillid = wbd.wbillid and wbl.wtype = -16
            and wbl.ondate between {0} and {1}
            and ( mat.grpid = {2} or {2} = 0 )
            and ( mat.matid = {3} or {3} = 0 )
      group by  mat.GrpId, mg.Name ,wbd.MATID, mat.name,   msr.shortname,  mat.barcode";

           //       var mat = db.Database.SqlQuery<REP_31_Result>(sql, StartDate, EndDate, (int)MatGroup.GrpId, (int)this.Material.MatId).ToList().OrderBy(o => o.MatName).ToList();

                var mat = db.REP_31(StartDate, EndDate, (int)MatGroup.GrpId, (int)this.Material.MatId).ToList().OrderBy(o => o.MatName).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = mat.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
                {
                    s.Key.GrpId,
                    Name = s.Key.GrpName,
                    TotalOrd = s.Sum(xs => xs.TotalOrd),
                    TotalOut = s.Sum(xs => xs.TotalOut)
                }).OrderBy(o => o.Name).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "MatList"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp);
                data_for_report.Add("MatList", mat);
                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_31);
            }

            if (idx == 29)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wh = Convert.ToString(Warehouse.WId);
                var mat = db.REP_29(StartDate, EndDate, (int)Kagent.KaId, grp, wh).ToList();

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

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatInDet", mat);
                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_29);
            }


            if (idx == 17)
            {
                decimal? total = 0;
                data_for_report.Add("XLRPARAMS", XLRPARAMS);

                var mat = db.REP_13(StartDate, EndDate, 0, 0, "*", 0, GrpStr).ToList();
                var mat_grp = mat.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
                {
                    s.Key.GrpId,
                    Name = s.Key.GrpName,
                    Income = s.Sum(xs => xs.SummOut - (xs.AmountOut * xs.AvgPrice))
                }).OrderBy(o => o.Name).ToList();
                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "MatList"
                });
                data_for_report.Add("MatGroup", mat_grp);
                data_for_report.Add("MatList", mat);
                total += mat_grp.Sum(s => s.Income);


                var mat2 = db.REP_13(StartDate, EndDate, 0, 0, "*", 1, GrpStr).ToList();
                var mat_grp2 = mat2.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
                {
                    s.Key.GrpId,
                    Name = s.Key.GrpName,
                    Income = s.Sum(xs => xs.SummIn - (xs.AmountIn * xs.AvgPrice))
                }).OrderBy(o => o.Name).ToList();
                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup2",
                    child_table = "MatSelPr"
                });
                data_for_report.Add("MatGroup2", mat_grp2);
                data_for_report.Add("MatSelPr", mat2);
                total -= mat_grp2.Sum(s => s.Income);


                var svc = db.REP_20(StartDate, EndDate, 0, 0).ToList();
                var svc_grp = svc.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
                {
                    s.Key.GrpId,
                    Name = s.Key.GrpName,
                    Total = s.Sum(xs => xs.Summ)
                }).OrderBy(o => o.Name).ToList();
                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "SvcGroup",
                    child_table = "SvcOutDet"
                });
                data_for_report.Add("SvcGroup", svc_grp.Where(w => svc.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("SvcOutDet", svc);
                total += svc_grp.Sum(s => s.Total);

                var paydoc = db.REP_16(StartDate, EndDate, 0, 0, 0).ToList();
                data_for_report.Add("DocList", paydoc);
                total -= paydoc.Sum(s => s.Total);


                var mat3 = db.REP_17(StartDate, EndDate, 0, 0).ToList();
                var mat_grp3 = mat3.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
                {
                    s.Key.GrpId,
                    Name = s.Key.GrpName,
                    Summ = s.Sum(xs => xs.Summ)
                }).OrderBy(o => o.Name).ToList();
                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup3",
                    child_table = "WBWriteOff"
                });
                data_for_report.Add("MatGroup3", mat_grp3);
                data_for_report.Add("WBWriteOff", mat3);
                total -= mat_grp3.Sum(s => s.Summ);

                data_for_report.Add("_realation_", rel);


                var obj = new List<object>();
                obj.Add(new
                {
                    Total = total  //=K12-K22+K32-K39-K49
                });
                data_for_report.Add("Summary", obj);

                IHelper.Print(data_for_report, TemlateList.rep_17);
            }

            if (idx == 23)
            {
                data_for_report.Add("XLRPARAMS", XLRPARAMS);

                data_for_report.Add("DocList1", db.GetPayDocList(1, StartDate, EndDate, 0, 1, -1, _person_id).ToList());
                data_for_report.Add("DocList2", db.GetPayDocList(-1, StartDate, EndDate, 0, 1, -1, _person_id).ToList());
                data_for_report.Add("DocList3", db.GetPayDocList(-2, StartDate, EndDate, 0, 1, -1, _person_id).ToList());
                data_for_report.Add("DocList4", db.GetPayDocList(6, StartDate, EndDate, 0, 1, -1, _person_id).ToList());

                var m = db.MoneyOnDate(EndDate).GroupBy(g => new { g.SaldoType, g.Currency }).Select(s => new { s.Key.SaldoType, s.Key.Currency, Saldo = s.Sum(sum => sum.Saldo) }).ToList();
                data_for_report.Add("MONEY1", m.Where(w => w.SaldoType == 0).ToList());
                data_for_report.Add("MONEY2", m.Where(w => w.SaldoType == 1).ToList());

                IHelper.Print(data_for_report, TemlateList.rep_23);
            }

            if (idx == 30)
            {
                var list = db.GetDocList(StartDate, EndDate, (int)Kagent.KaId, 0).OrderBy(o => o.OnDate).ToList().Where(w => new int[] { 1, -1, 3, -3, -6, 6 }.Any(a => a == w.WType)).Select((s, index) => new
                {
                    idx = index + 1,
                    s.OnDate,
                    s.SummAll,
                    s.Saldo,
                    DocName = s.TypeName + " №" + s.Num,
                    PN = s.WType == 1 ? s.SummAll : null,
                    VN = s.WType == -1 ? s.SummAll : null,
                    PKO = s.WType == 3 ? s.SummAll : null,
                    VKO = s.WType == -3 ? s.SummAll : null,
                    PDP = s.WType == -6 ? s.SummAll : null,
                    PVK = s.WType == 6 ? s.SummAll : null,
                }).OrderBy(o=> o.OnDate);

                if (!list.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("KADocList", list.ToList());

                IHelper.Print(data_for_report, TemlateList.rep_30);
            }

            if (idx == 32)
            {
           //     int grp = Convert.ToInt32(MatGroup.GrpId);
           //     string wh = Convert.ToString(Warehouse.WId);
                var mat = db.REP_32(StartDate, EndDate).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var drivers = mat.Select(s => new { s.DriverId, s.DriverName }).Distinct().ToList();
                var routes = mat.Select(s => new { s.DriverId, s.RouteName }).Distinct().ToList();
                var mat_out = mat.GroupBy(g => new { g.DriverId, g.DriverName, g.MatName }).Select(s => new
                {
                    s.Key.DriverId,
                    s.Key.MatName,
                    Amount = s.Sum(sum => sum.Amount)
                }).ToList();

                rel.Add(new
                {
                    pk = "DriverId",
                    fk = "DriverId",
                    master_table = "Drivers",
                    child_table = "Routes"
                });

                rel.Add(new
                {
                    pk = "DriverId",
                    fk = "DriverId",
                    master_table = "Drivers",
                    child_table = "MatList"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("Drivers", drivers);
                data_for_report.Add("Routes", routes);
                data_for_report.Add("MatList", mat_out);
                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_32);
            }

            if (idx == 33)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                int mat_id = (int)this.Material.MatId;
                var make = db.REP_33(StartDate, EndDate, grp, mat_id).OrderBy(o=> o.OnDate).ToList();

                if (!make.Any())
                {
                    return;
                }

                rel.Add(new
                {
                    pk = "MatId",
                    fk = "MatId",
                    master_table = "MatList",
                    child_table = "WBList"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatList", make.GroupBy(o => new { o.MatId, o.MatName }).Select(s => new { s.Key.MatId, s.Key.MatName }).ToList());
                data_for_report.Add("WBList", make.ToList());
                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_33);
            }

            if (idx == 34)
            {
                var r = db.WaybillList.Where(w => w.WType == -20 && (w.Checked == 0 || w.Checked == 2) && w.OnDate <= OnDate).SelectMany(s => s.TechProcDet).Where(w => w.MatId != null).Select(s => new
                {
                    s.MatId,
                    s.WaybillList.WayBillMake.MatRecipe.Materials.Name
                }).ToList();

                var list = db.Materials.Where(w => w.TypeId == 1).ToList().Select(s => new
                {
                    s.Name,
                    s.Artikul,
                    s.Weight,
                    Status = r.Any(a=> a.MatId == s.MatId) ? r.FirstOrDefault(f=> f.MatId == s.MatId).Name : "Вільна"
                });


                if (!list.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("DocList", list.ToList());

                IHelper.Print(data_for_report, TemlateList.rep_34);
            }

            if (idx == 35)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                int mat_id = (int)this.Material.MatId;
                var make = db.REP_35(StartDate, EndDate, grp, mat_id).OrderBy(o => o.OnDate).ToList();

                if (!make.Any())
                {
                    return;
                }

                rel.Add(new
                {
                    pk = "MatId",
                    fk = "MatId",
                    master_table = "MatList",
                    child_table = "WBList"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatList", make.GroupBy(o => new { o.MatId, o.MatName }).Select(s => new { s.Key.MatId, s.Key.MatName }).ToList());
                data_for_report.Add("WBList", make.ToList());
                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_35);
            }

            if (idx == 36)
            {
                var disc = db.DiscCards.Select(s => new
                {
                    s.Num,
                    s.OnValue,
                    KaName = s.Kagent != null ? s.Kagent.Name : "",
                    Total = db.WayBillDetAddProps.Where(w => w.CardId == s.CardId && w.WaybillDet.OnDate <= OnDate).Sum(t => t.WaybillDet.Total) 
                });

                if (!disc.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("DiscCards", disc.ToList());

                IHelper.Print(data_for_report, TemlateList.rep_36);
            }

            if (idx == 37)
            {
                int wh_id = Convert.ToInt32(Warehouse.WId);
                var make = db.REP_37(wh_id, StartDate, EndDate).OrderBy(o => o.Num).ToList();

                if (!make.Any())
                {
                    return;
                }

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "WayBillItems"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", make.GroupBy(o => new { o.GrpId, o.GrpName }).Select(s => new { s.Key.GrpId, s.Key.GrpName }).OrderBy(o => o.GrpName).ToList());
                data_for_report.Add("WayBillItems", make.ToList());
                data_for_report.Add("_realation_", rel);
                data_for_report.Add("SummaryField", make.GroupBy(g => 1).Select(s => new
                {
                    SummAll = s.Sum(a => a.SumAll),
                }).ToList());

                IHelper.Print(data_for_report, TemlateList.rep_37);
            }

            if (idx == 38)
            {
                var sql_1 = @"
   select [WayBillMake].WbillId  , [WaybillList].Num, [WaybillList].OnDate, rec.Name as RecipeName, [WayBillMake].Amount
  from  [sp_base].[dbo].[WayBillMake] 
  inner join [sp_base].[dbo].[WaybillList] on [WaybillList].WbillId = [WayBillMake].WbillId
  join [sp_base].[dbo].[MatRecipe] on [MatRecipe].RecId = [WayBillMake].RecId 
  join [sp_base].[dbo].[Materials] rec on rec.MatId = [MatRecipe].MatId
  where [WaybillList].OnDate between  {0} and {1} and [WaybillList].WType = -20
  order by rec.Name , [WaybillList].OnDate
     ";

                var waybill_list = db.Database.SqlQuery<make_wb>(sql_1, StartDate, EndDate).ToList();


                if (!waybill_list.Any())
                {
                    return;
                }

                var sql_2 = @"
  select [WayBillMake].WbillId , s_mat.Name, [WaybillDet].Amount, ( ROUND( ([WayBillMake].Amount / [MatRecipe].Amount), 0) * [MatRecDet].AMOUNT )  as  RecAmount
  from  [WayBillMake] 
  join [sp_base].[dbo].[WaybillList] on [WaybillList].WbillId = [WayBillMake].WbillId
  join [sp_base].[dbo].[MatRecipe] on [MatRecipe].RecId = [WayBillMake].RecId 
  join [sp_base].[dbo].[WaybillDet] on [WayBillMake].[WbillId] = [WaybillDet].[WbillId]
  join [sp_base].[dbo].[Materials] s_mat on s_mat.MatId = [WaybillDet].MatId
  left outer join [sp_base].[dbo].[MatRecDet] on [MatRecDet].RecId = [WayBillMake].RecId and [WaybillDet].MatId = [MatRecDet].MatId
  where [WaybillList].OnDate between {0} and {1} and [WaybillList].WType = -20 and [MatRecipe].Amount > 0";

                var use_rec_mat = db.Database.SqlQuery<use_rec_mat>(sql_2, StartDate, EndDate).ToList().OrderBy(o => o.Name).ToList();

                rel.Add(new
                {
                    pk = "WbillId",
                    fk = "WbillId",
                    master_table = "WaybillList",
                    child_table = "UseMatRecipe"
                });

                var sql_3 = @"
  select [WayBillMake].WbillId , rec_mat.Name, [MatRecDet].Amount as RecAmount 
  from  [WayBillMake] 
  inner join [WaybillList] on [WaybillList].WbillId = [WayBillMake].WbillId
  join [MatRecDet] on [MatRecDet].RecId = [WayBillMake].RecId 
  join [Materials] rec_mat on rec_mat.MatId = [MatRecDet].MatId
  where [WaybillList].OnDate between {0} and {1} and [WaybillList].WType = -20 
  and [MatRecDet].MatId not in (select MatId from [sp_base].[dbo].[WaybillDet] where WbillId = [WayBillMake].WbillId)";

                var not_use_rec_mat = db.Database.SqlQuery<not_use_rec_mat>(sql_3, StartDate, EndDate).ToList().OrderBy(o => o.Name).ToList();

                rel.Add(new
                {
                    pk = "WbillId",
                    fk = "WbillId",
                    master_table = "WaybillList",
                    child_table = "NotUseMatRecipe"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("WaybillList", waybill_list);
                data_for_report.Add("UseMatRecipe", use_rec_mat);
                data_for_report.Add("NotUseMatRecipe", not_use_rec_mat);
                data_for_report.Add("_realation_", rel);
               
                IHelper.Print(data_for_report, TemlateList.rep_38);
            }

            if (idx == 22)
            {
                int person = (int)Person.KaId;

                var sql_1 = @"
   	            select m.GrpId, m.name Name, wbd.amount Amount, wbd.total Summ, ms.ShortName, wbl.WbillId, person.Name PersonName , person.KaId PersonId , wbl.KaId , ka.Name KontragentName

                from waybilldet wbd
                join waybilllist wbl on wbl.wbillid = wbd.wbillid
                join materials m on m.matid = wbd.matid
                join measures ms on ms.mid = m.mid
			    join kagent person on person.kaid = wbl.PersonId
                join kagent ka on ka.kaid = wbl.KaId

                where  wbl.checked = 1 and wbl.WType = -1
                       and wbl.ondate between {0} and {1}
                       and person.KaId = {2}
   
			    order by  m.name ";

                var waybill_list = db.Database.SqlQuery<rep_22>(sql_1, StartDate, EndDate, person).ToList();

                if (!waybill_list.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("WbList", waybill_list.GroupBy(g => new { g.Name, g.ShortName, g.PersonName }).Select(s => new
                {
                    Name = s.Key.Name,
                    ShortName = s.Key.ShortName,
                    PersonName = s.Key.PersonName,
                    Amount = s.Sum(a => a.Amount),
                    Summ = s.Sum(su => su.Summ)
                }).ToList());

                data_for_report.Add("MeasuresList", waybill_list.GroupBy(g => new { g.ShortName }).Select(s => new
                {
                    ShortName = s.Key.ShortName,
                    Amount = s.Sum(a => a.Amount),
                    Summ = s.Sum(su => su.Summ)
                }).ToList());


                data_for_report.Add("KagentList", waybill_list.GroupBy(g => g.KontragentName).Select(s => new
                {
                    Name = s.Key,
                    Amount = s.Select(d => d.WbillId).Distinct().Count(),
                    Summ = s.Sum(su => su.Summ)
                }).ToList());

                IHelper.Print2(data_for_report, TemlateList.rep_22);
            }

            if (idx == 39)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wh = Convert.ToString(Warehouse.WId);
                int kid = Convert.ToInt32(Kagent.KaId);
                Guid grp_kg = KontragentGroup.Id;
                var mat = db.REP_39(StartDate, EndDate, grp, kid, wh, "-1,", _user_id, grp_kg).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var kagents = DBHelper.Kagents.Where(w => w.KaId == kid || kid == 0).Select(s => new { s.KaId, s.Name }).ToList();

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

                IHelper.Print(data_for_report, TemlateList.rep_39);
            }

            if (idx == 40)
            {
                var list = DB.SkladBase().GetUsedMaterials((int)this.Material.MatId, OnDate.Date.AddDays(1)).OrderBy(o=> o.KaName).ToList();

                var k = (int)Kagent.KaId;
                if (k > 0)
                {
                    list = list.Where(w => w.KaId == k).ToList();
                }

                if (!list.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("DiscCards", list.ToList());

                IHelper.Print(data_for_report, TemlateList.rep_40);
            }

            db.PrintLog.Add(new PrintLog
            {
                PrintType = 1,
                RepId = idx,
                UserId = DBHelper.CurrentUser.UserId,
                OnDate = DateTime.Now
            });

            db.SaveChanges();
        }

    }


    public class make_wb
    {
        public int WbillId { get; set; }
        public string Num { get; set; }
        public DateTime OnDate { get; set; }
        public string RecipeName { get; set; }
        public decimal Amount { get; set; }
    }

    public class use_rec_mat
    {
        public int WbillId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal? RecAmount { get; set; }
    }

    public class not_use_rec_mat
    {
        public int WbillId { get; set; }
        public string Name { get; set; }
        public decimal? RecAmount { get; set; }
    }

    public class rep_22
    {
        public int GrpId { get; set; }
        public string Name { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Summ { get; set; }
        public string ShortName { get; set; }
        public int WbillId { get; set; }
        public string PersonName { get; set; }
        public int? PersonId { get; set; }
        public int KaId { get; set; }
        public string KontragentName { get; set; }
    }
}
