using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP_Sklad.Common;
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
        public dynamic Material { get; set; }
        public String DocStr { get; set; }
        public object DocType { get; set; }
        public dynamic ChType { get; set; }

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
                    KAID = Kagent != null ? Kagent.Name : "",
                    MatId = Material != null ? Material.Name : "",
                    CType = ChType!= null ? ChType.Name :""
                });
                return obj;
            }
        }

        public PrintReport()
        {

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

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatInDet", mat);
                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_1);
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

                IHelper.Print(data_for_report, TemlateList.rep_3);
            }

            if (idx == 14)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wh = Convert.ToString(Warehouse.WId);
                int kid = Convert.ToInt32(Kagent.KaId);
                var mat = db.REP_3_14(StartDate, EndDate, grp, kid, wh, "-1,").ToList();

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

                IHelper.Print(data_for_report, TemlateList.rep_3);
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

                IHelper.Print(data_for_report, TemlateList.rep_4);
            }

            if (idx == 25)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                int kid = Convert.ToInt32(Kagent.KaId);
                string wh = Convert.ToString(Warehouse.WId);
                var mat = db.REP_4_25(StartDate, EndDate, grp, kid, wh, "1,").ToList();

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
                var kagents = db.Kagent.Where(w => w.Deleted == 0 && (w.Archived ?? 0) == 0).Select(s => new
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

                IHelper.Print(data_for_report, TemlateList.rep_5);
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

                IHelper.Print(data_for_report, TemlateList.rep_6);
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

                IHelper.Print(data_for_report, TemlateList.rep_7);
            }

            if (idx == 8)
            {
                var list = db.GetDocList(StartDate, EndDate, (int)Kagent.KaId, 0).OrderBy(o => o.OnDate).Select(s => new
                {
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

                var list = db.GetMatMove((int)this.Material.MatId, StartDate, EndDate, wid, (int)Kagent.KaId, (int)DocType, "*").ToList();

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

                var mat = db.REP_10(StartDate, EndDate, grp, wid, 0).ToList();

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
                    child_table = "MatList"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
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
            }

            if (idx == 13)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wid = Convert.ToString(Warehouse.WId);

                var mat = db.REP_13(StartDate, EndDate, grp, (int)Kagent.KaId, (String)Warehouse.WId, 0).ToList();

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
                    Income = s.Sum(r => r.Income)
                }).ToList());

                IHelper.Print(data_for_report, TemlateList.rep_13);
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
                var list = db.GetMatMove((int)this.Material.MatId, StartDate, EndDate, wid, (int)Kagent.KaId, (int)DocType, "*").ToList();

                if (!list.Any())
                {
                    return;
                }

                var satrt_remais = db.MatRemainByWh(mat_id, wid, (int)Kagent.KaId, StartDate, "*").Sum(s => s.Remain);
                var sart_avg_price = db.v_MatRemains.Where(w => w.MatId == mat_id && w.OnDate <= StartDate).OrderByDescending(o => o.OnDate).Select(s => s.AvgPrice).FirstOrDefault();
                var end_remais = db.MatRemainByWh(mat_id, wid, (int)Kagent.KaId, EndDate, "*").Sum(s => s.Remain);
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
                var mat = db.OrderedList(StartDate, EndDate, 0, (int)Kagent.KaId, -16, 0).GroupBy(g => new
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

                var mat = db.WhMatGet(grp, wid, 0, OnDate, 0, "*", 0, "", 0, 0).Where(w => w.Remain < w.MinReserv && w.MinReserv != null).ToList();

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
                var mat = db.REP_27(StartDate, EndDate, (int)Kagent.KaId, (int)MatGroup.GrpId, (int)this.Material.MatId).ToList();

                if (!mat.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatList", mat.ToList());

                IHelper.Print(data_for_report, TemlateList.rep_27);

            }

            if (idx == 31)
            {
                var mat = db.REP_31(StartDate, EndDate, (int)MatGroup.GrpId, (int)this.Material.MatId).OrderBy(o => o.MatName).ToList();

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
                data_for_report.Add("XLRPARAMS", XLRPARAMS);

                var mat = db.REP_13(StartDate, EndDate, 0, 0, "*", 0).ToList();
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


                var mat2 = db.REP_13(StartDate, EndDate, 0, 0, "*", 1).ToList();
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

                var paydoc = db.REP_16(StartDate, EndDate, 0, 0, 1).ToList();
                data_for_report.Add("DocList", paydoc);


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

                data_for_report.Add("_realation_", rel);

                IHelper.Print(data_for_report, TemlateList.rep_17);
            }

            if (idx == 23)
            {
                data_for_report.Add("XLRPARAMS", XLRPARAMS);

                data_for_report.Add("DocList1", db.GetPayDocList(1, StartDate, EndDate, 0, 1, -1).ToList());
                data_for_report.Add("DocList2", db.GetPayDocList(-1, StartDate, EndDate, 0, 1, -1).ToList());
                data_for_report.Add("DocList3", db.GetPayDocList(-2, StartDate, EndDate, 0, 1, -1).ToList());
                data_for_report.Add("DocList4", db.GetPayDocList(6, StartDate, EndDate, 0, 1, -1).ToList());

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
}
