using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using DocumentFormat.OpenXml.ReportBuilder;
using SP.Reports.Models;
using SP.Base;
using SP.Reports.Comon;
using SP.Base.Models;
using SP.Reports.Models.Views;

namespace SP.Reports
{
    public class PrintReportv2: BaseReport
    {
        public DateTime OnDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public GrpComboBoxItem MatGroup { get; set; }
        public KagentComboBoxItem Kagent { get; set; }
        public WhComboBoxItem Warehouse { get; set; }
        public MatComboBoxItem Material { get; set; }
        public String DocStr { get; set; }
        public object DocType { get; set; }
        public dynamic ChType { get; set; }
        public object Status { get; set; }
        public GrpKagentComboBoxItem KontragentGroup { get; set; }
        public String GrpStr { get; set; }
        public dynamic Person { get; set; }


        private int? _person_id { get; set; }
        private int _user_id { get; set; }


        private List<XLRPARAM> XLR_PARAMS
        {
            get
            {
                var obj = new List<XLRPARAM>
                {
                    new XLRPARAM
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
                        Year = StartDate.Year.ToString()
                    }
                };

                return obj;
            }
        }

        private Dictionary<string, IList> data_for_report { get; set; }
        private List<object> realation { get; set; }
        private int  _rep_id { get; set; }

        public PrintReportv2(int rep_id,  int? person_id, int user_id)
        {
            _person_id = person_id; 
            _user_id = user_id;
            _rep_id = rep_id;

            data_for_report = new Dictionary<string, IList>();
            realation = new List<object>();
        }

        public byte[] CreateReport( string template_file, string file_format)
        {
            int report_mode = 0;

            switch (_rep_id)
            {
                case 1:
                    REP_1();
                    break;
                case 2:
                    REP_2();
                    break;
                case 3:
                    REP_3();
                    break;
                case 4:
                    REP_4();
                    break;
                case 5:
                    REP_5();
                    report_mode = 1;
                    break;
                case 6:
                    REP_6();
                    report_mode = 1;
                    break;
                case 7:
                    REP_7();
                    break;
                case 8:
                    REP_8();
                    report_mode = 1;
                    break;
                case 9:
                    REP_9();
                    report_mode = 1;
                    break;
                case 10:
                    REP_10();
                    break;
                case 11:
                    REP_11();
                    report_mode = 1;
                    break;
                case 13:
                    REP_13();
                    break;
                case 14:
                    REP_14();
                    break;

                default:
                    break;
            }

            return report_mode == 0 ? ReportBuilder.GenerateReport(data_for_report, template_file, false, file_format) : ReportBuilderXLS.GenerateReport(data_for_report, template_file).ToArray();
        }

        private void REP_1()
        {
            var mat = _db.REP_1(StartDate, EndDate, MatGroup.GrpId, Kagent.KaId, Warehouse.WId, DocStr, _user_id).ToList();

            var mat_grp = mat.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                Name = s.Key.GrpName,
                Summ = s.Sum(xs => xs.SumPrice)
            }).OrderBy(o => o.Name).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatInDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp);
            data_for_report.Add("MatInDet", mat);
            data_for_report.Add("_realation_", realation);
        }
        private void REP_2()
        {
            int grp = Convert.ToInt32(MatGroup.GrpId);
            string wh = Convert.ToString(Warehouse.WId);
            int status = Convert.ToInt32(Status);

            var mat = _db.REP_2(StartDate, EndDate, grp, Kagent.KaId, wh, DocStr, status, _user_id).AsQueryable().OrderBy(GetSortedList(_rep_id)).ToList();

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

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatOutDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp);
            data_for_report.Add("MatOutDet", mat);
            data_for_report.Add("_realation_", realation);
        }
        private void REP_3()
        {
            var mat = _db.REP_3_14(StartDate, EndDate, MatGroup.GrpId, Kagent.KaId, Warehouse.WId, "-1,", _user_id, KontragentGroup.Id).ToList();

            var enterprise_list = EnterpriseList(_person_id).Select(s => (int?)s.KaId);

            var kagents = (from k in _db.KagentList
                           join ew in _db.EnterpriseWorker on k.KaId equals ew.WorkerId into gj
                           from subfg in gj.DefaultIfEmpty()
                           where (subfg.EnterpriseId == null || enterprise_list.Contains(subfg.EnterpriseId)) 
                           select k
                      ).Distinct().ToList().Where(w => w.KaId == Kagent.KaId || Kagent.KaId == 0).Select(s => new { s.KaId, s.Name }).ToList();

            realation.Add(new
            {
                pk = "KaId",
                fk = "KaId",
                master_table = "MatGroup",
                child_table = "MatOutDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", kagents.Where(w => mat.Select(s => s.KaId).Contains(w.KaId)).ToList());
            data_for_report.Add("MatOutDet", mat);
            data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
            {
                Amount = s.Sum(a => a.Amount),
                Summ = s.Sum(ss => ss.Summ),
                ReturnAmountIn = s.Sum(r => r.ReturnAmountIn),
                ReturnSummIn = s.Sum(r => r.ReturnSummIn)
            }).ToList());
            data_for_report.Add("_realation_", realation);
        }
        private void REP_4()
        {
            var mat = _db.REP_4_25(StartDate, EndDate, MatGroup.GrpId, Kagent.KaId, Warehouse.WId, "1,", _user_id).ToList();
            var enterprise_list = EnterpriseList(_person_id).Select(s => (int?)s.KaId);
            var kagents = (from k in _db.KagentList
                           join ew in _db.EnterpriseWorker on k.KaId equals ew.WorkerId into gj
                           from subfg in gj.DefaultIfEmpty()
                           where (subfg.EnterpriseId == null || enterprise_list.Contains(subfg.EnterpriseId))
                           select k
                      ).Distinct().ToList().Where(w => w.KaId == Kagent.KaId || Kagent.KaId == 0).Select(s => new { s.KaId, s.Name }).ToList();

            realation.Add(new
            {
                pk = "KaId",
                fk = "KaId",
                master_table = "MatGroup",
                child_table = "MatInDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", kagents.Where(w => mat.Select(s => s.KaId).Contains(w.KaId)).ToList());
            data_for_report.Add("MatInDet", mat);
            data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
            {
                SummPrice = s.Sum(r => r.SummPrice),
                ReturnSummPriceOut = s.Sum(r => r.ReturnSummPriceOut)
            }).ToList());

            data_for_report.Add("_realation_", realation);
        }
        private void REP_5()
        {
            var kagents = _db.REP_4_5(OnDate).Where(w => w.Saldo > 0).ToList().Select((s, index) => new
            {
                N = index + 1,
                s.Name,
                s.Saldo
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("Kagent", kagents.ToList());
        }
        private void REP_6()
        {
            var kagents = _db.REP_4_5(OnDate).Where(w => w.Saldo < 0).ToList().Select((s, index) => new
            {
                N = index + 1,
                s.Name,
                s.Saldo
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("Kagent", kagents.ToList());
        }
        private void REP_7()
        {
            int grp = Convert.ToInt32(MatGroup.GrpId);
            int wid = Warehouse.WId == "*" ? 0 : Convert.ToInt32(Warehouse.WId);

            var mat = _db.WhMatGet(grp, wid, 0, OnDate, 0, "*", 0, GrpStr, _user_id, 0).Select(s => new
            {
                s.BarCode,
                s.MatName,
                s.MsrName,
                s.Remain,
                s.Rsv,
                s.AvgPrice,
                s.OutGrpId,
                s.GrpName,
            }).OrderBy(o => o.MatName).ToList();

            var mat_grp = mat.GroupBy(g => new { g.GrpName, g.OutGrpId }).Select(s => new
            {
                s.Key.OutGrpId,
                Name = s.Key.GrpName,
                Total = s.Sum(xs => (xs.AvgPrice * xs.Remain)),
            }).OrderBy(o => o.Name).ToList();

            realation.Add(new
            {
                pk = "OutGrpId",
                fk = "OutGrpId",
                master_table = "MatGroup",
                child_table = "MatList"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp);
            data_for_report.Add("MatList", mat);
            data_for_report.Add("_realation_", realation);
        }
        private void REP_8()
        {
            var list = _db.GetDocList(StartDate, EndDate, Kagent.KaId, 0).OrderBy(o => o.OnDate).Select(s => new
            {
                s.Num,
                s.TypeName,
                s.OnDate,
                SummAll = s.SummInCurr,
                s.Saldo,
                DocName = s.TypeName + " №" + s.Num

            }).ToList();

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("KADocList", list.ToList());
        }
        private void REP_9()
        {
            int wid = Warehouse.WId == "*" ? 0 : Convert.ToInt32(Warehouse.WId);

            var list = _db.GetMatMove(Material.MatId, StartDate, EndDate, wid, Kagent.KaId, (int)DocType, "*", KontragentGroup.Id, _user_id).ToList();

            if (!list.Any())
            {
                return;
            }

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatList", list.ToList());
        }
        private void REP_10()
        {
            var mat = _db.REP_10(StartDate, EndDate, MatGroup.GrpId, Warehouse.WId, 0, _user_id).OrderBy(o => o.MatId).ToList();

            var mat_grp = mat.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                Name = s.Key.GrpName,
                SummIn = s.Sum(xs => xs.SummIn),
                SummOut = s.Sum(xs => xs.SummOut),
                SummStart = s.Sum(xs => xs.SummStart),
                SummEnd = s.Sum(xs => xs.SummEnd)
            }).OrderBy(o => o.Name).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatList"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp);
            data_for_report.Add("MatList", mat);
            data_for_report.Add("_realation_", realation);
        }
        private void REP_11()
        {
            var list = _db.GetDocList(StartDate, EndDate, 0, (int)DocType).OrderBy(o => o.OnDate).Select(s => new
            {
                s.OnDate,
                s.KaName,
                SummAll = s.SummInCurr,
                s.Saldo,
                DocName = s.TypeName + " №" + s.Num
            }).ToList();

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("DocList", list.ToList());
        }
        private void REP_13()
        {
            var mat = _db.REP_13(StartDate, EndDate, MatGroup.GrpId, Kagent.KaId, Warehouse.WId, 0, GrpStr).ToList();

            var gs = !String.IsNullOrEmpty(GrpStr) ? GrpStr.Split(',').Select(s => Convert.ToInt32(s)).ToList() : new List<int>();
            var mat_grp = _db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == MatGroup.GrpId || MatGroup.GrpId == 0 || gs.Contains(w.GrpId))).Select(s => new { s.GrpId, s.Name }).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatList"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
            data_for_report.Add("MatList", mat);
            data_for_report.Add("_realation_", realation);
            data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
            {
                SummIn = s.Sum(r => r.SummIn),
                SummOut = s.Sum(r => r.SummOut),
                Income = s.Sum(r => r.Income),
                Rentabelnist = s.Average(a => (a.SummOut - a.SummIn - a.Income) > 0 && a.Income > 0 ? a.Income / (a.SummOut - a.SummIn - a.Income) : 0)
            }).ToList());
        }
        private void REP_14()
        {
            var mat = _db.REP_3_14(StartDate, EndDate, MatGroup.GrpId, Kagent.KaId, Warehouse.WId, DocStr, _user_id, KontragentGroup.Id).ToList();
            var mat_grp = _db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == MatGroup.GrpId || MatGroup.GrpId == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatOutDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
            data_for_report.Add("MatOutDet", mat);
            data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
            {
                Amount = s.Sum(a => a.Amount),
                Summ = s.Sum(ss => ss.Summ),
                ReturnAmountIn = s.Sum(r => r.ReturnAmountIn),
                ReturnSummIn = s.Sum(r => r.ReturnSummIn)
            }).ToList());

            data_for_report.Add("_realation_", realation);
        }

        private string GetSortedList(int rep_id)
        {

            string result = "";
            var list = _db.ReportSortedFields.Where(w => w.RepId == rep_id && w.OrderDirection != 0).OrderBy(o => o.Idx).ToList();

            foreach (var i in list)
            {
                result += $"{i.FieldName} {(i.OrderDirection == 2 ? "desc" : "asc")},";
            }

            return result.Trim(',');

        }
        private List<Enterprise> EnterpriseList(int? currentuser_kaid)
        {

            using (var db = Database.SPBase())
            {
                return db.Kagent.Where(w => w.KType == 3 && w.Deleted == 0 && (w.Archived == null || w.Archived == 0))
                    .Join(db.EnterpriseWorker.Where(ew => ew.WorkerId == currentuser_kaid), w => w.KaId, ew => ew.EnterpriseId, (w, ew) => new Enterprise
                    {
                        KaId = w.KaId,
                        Name = w.Name,
                        NdsPayer = w.NdsPayer
                    }).ToList();
            }

        }

    }


   


   

    
}
