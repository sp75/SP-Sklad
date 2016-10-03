using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraRichEdit.Model;
using SP_Sklad.SkladData;
using SpreadsheetReportBuilder;

namespace SP_Sklad.Reports
{
    class PrintDoc
    {
        public static string template_path
        {
            get
            {

                return DBHelper.CommonParam.TemplatePatch;
                /*
#if DEBUG
                return Path.Combine(@"c:\WinVSProjects\SP-Sklad\SP_Sklad\", "TempLate");
#else
               return Path.Combine(Application.StartupPath, "TempLate" );
#endif*/
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

        public static void Show(int doc_id, int doc_type, BaseEntities db)
        {
            db.SaveChanges();

            switch (doc_type)
            {
                case 1:
                    WayBillInReport(doc_id, db, TemlateList.wb_in);
                    break;

                case -1:
                    WayBillOutReport(doc_id, db, TemlateList.wb_out);
                    break;

                case 5:
                    WayBillReport(doc_id, db, TemlateList.write_on);
                    break;

                case -5:
                    WayBillReport(doc_id, db, TemlateList.write_off);
                    break;

                case -6:
                    WayBillReport(doc_id, db, TemlateList.re_supp);
                    break;

                case 6:
                    WayBillReport(doc_id, db, TemlateList.re_cust);
                    break;

                case -16:
                    WayBillReport(doc_id, db, TemlateList.ord_out);
                    break;

                case 16:
                    WayBillReport(doc_id, db, TemlateList.ord_in);
                    break;

                case 4:
                    WayBillReport(doc_id, db, TemlateList.wb_move);
                    break;

                case 7:
                    WayBillReport(doc_id, db, TemlateList.wb_inv);
                    break;

                case -22:
                    DeboningReport(doc_id, db);
                    break;

                case -20:
                    MakedReport(doc_id, db);
                    break;

                case 10:
                    PriseListReport(doc_id, db);
                    break;

                case 3:
                    PayDocReport(doc_id, db, TemlateList.pay_doc_in);
                    break;

                case -3:
                    PayDocReport(doc_id, db, TemlateList.pay_doc_out);
                    break;

                case -2:
                    PayDocReport(doc_id, db, TemlateList.pay_doc_out);
                    break;

            }
        }

        public static void WayBillInReport(int doc_id, BaseEntities db, string template_name)
        {
            var dataForReport = new Dictionary<string, IList>();

            var wb = db.v_WaybillList.Where(w => w.DocId == doc_id).AsNoTracking().ToList();

            dataForReport.Add("WayBillList", wb);
            dataForReport.Add("range1", db.GetWaybillDetIn(wb.First().WbillId).ToList());

            Print(dataForReport, template_name);
        }

        public static void WayBillReport(int doc_id, BaseEntities db, string template_name)
        {
            var dataForReport = new Dictionary<string, IList>();

            var wb = db.v_WaybillList.Where(w => w.DocId == doc_id).AsNoTracking().ToList();
            int wbill_id = wb.First().WbillId;

            dataForReport.Add("WayBillList", wb);
            dataForReport.Add("WayBillItems", db.GetWaybillDetIn(wbill_id).ToList());
            dataForReport.Add("Commission", db.Commission.Where(w => w.WbillId == wbill_id).Select(s => new
            {
                MainName = s.Kagent.Name,
                FirstName = s.Kagent1.Name,
                SecondName = s.Kagent2.Name,
                ThirdName = s.Kagent3.Name
            }).ToList());

            Print(dataForReport, template_name);
        }

        public static void WayBillOutReport(int doc_id, BaseEntities db, string template_name)
        {
            var data_report = new Dictionary<string, IList>();

            var wb = db.v_WaybillList.Where(w => w.DocId == doc_id).AsNoTracking().ToList();

            if (wb != null)
            {
                var m = new MoneyToStr("UAH", "UKR", "TEXT");
                wb.First().www = m.convertValue(wb.First().SummAll.Value);
            }

       //     db.Entry(wb).Property(p=> p.).IsModified = false =

            var ent_id = wb.First().EntId;
            data_report.Add("EntAccount", db.EnterpriseAccount.Where(w => w.KaId == ent_id && w.Def == 1).ToList());
            data_report.Add("WayBillList", wb);
            data_report.Add("range1", db.GetWayBillDetOut(wb.First().WbillId).ToList());

            var w_id = wb.First().WbillId;
            var p = db.WaybillDet.Where(w => w.WbillId == w_id).Select(s => new
            {
                s.Num,
                s.Amount,
                s.Price,
                s.Materials.Name,
                s.Materials.Measures.ShortName                ,
                s.Materials.Artikul,
                s.Materials.CF1,
                s.Materials.CF2,
                s.Materials.CF3,
                s.Materials.CF4,
                s.Materials.CF5,
                OnDate = DbFunctions.AddDays( s.OnDate , -1)
            }).ToList();
            data_report.Add("Posvitcheny", p);

            Print(data_report, template_name);
        }

        public static void DeboningReport(int doc_id, BaseEntities db)
        {
            var dataForReport = new Dictionary<string, IList>();
            var date = db.WaybillList.Where(w=> w.DocId == doc_id).Select(s=> s.OnDate).First();

            var wb = db.WBListMake(date,date,-1,"*",0,-22).Where(w => w.DocId == doc_id).ToList();
            int wbill_id = wb.First().WbillId;

            var item = db.DeboningDet.Where(w => w.WBillId == wbill_id).AsNoTracking().ToList().Select((s, index) => new
            {
                Num = index + 1,
                s.Amount,
                s.Price,
                s.Materials.Name,
                WhName = s.Warehouse.Name,
                MsrName = s.Materials.Measures.ShortName,
                s.Materials.GrpId,
                GrpName = s.Materials.MatGroup.Name
            }).ToList();

            var grp = item.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                s.Key.GrpName,
                Amount = s.Sum(sa => sa.Amount),
                Summ = s.Sum(sum => sum.Amount * sum.Price)
            }).ToList();

            var rel = new List<object>();
            rel.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MGRPD",
                child_table = "DeboningItems"
            });

            dataForReport.Add("WayBillList", wb);
            dataForReport.Add("MGRPD", grp);
            dataForReport.Add("DeboningItems", item);
            dataForReport.Add("_realation_", rel);

            Print(dataForReport, TemlateList.wb_deb);
        }

        public static void MakedReport(int wbill_id, BaseEntities db)
        {
            var dataForReport = new Dictionary<string, IList>();
            var date = db.WaybillList.Where(w => w.WbillId == wbill_id).Select(s => s.OnDate).First();
            var wb = db.WBListMake(date, date, -1, "*", 0, -20).ToList();
            var item = db.GetWayBillDetOut(wbill_id).ToList().Select((s, index) => new
            {
                Num = index + 1,
                s.Amount,
                s.Price,
                s.MatName,
                s.WhName,
                s.MsrName,
                s.GrpId,
                GrpName = s.GroupName
            }).ToList();

            var grp = item.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                s.Key.GrpName,
                Amount = s.Sum(sa => sa.Amount),
                Summ = s.Sum(sum => sum.Amount * sum.Price)
            }).ToList();

            var tp = db.TechProcDet.Where(w => w.WbillId == wbill_id).ToList().Select((s, index) => new
            {
                Num = index + 1,
                s.Out,
                s.Notes,
                PersonName = s.Kagent.Name,
                s.OnDate,
                s.TechProcess.Name
            }).ToList();

            var rel = new List<object>();
            rel.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MGRP",
                child_table = "WayBillItems"
            });

            dataForReport.Add("WayBillList", wb);
            dataForReport.Add("MGRP", grp);
            dataForReport.Add("WayBillItems", item);
            dataForReport.Add("TechProc",  tp);
            dataForReport.Add("_realation_", rel);

            Print(dataForReport, TemlateList.wb_maked);
        }

        public static void PayDocReport(int doc_id, BaseEntities db, string template_name)
        {
            var dataForReport = new Dictionary<string, IList>();
            var pd = db.v_PayDoc.Where(w => w.DocId == doc_id).AsNoTracking().ToList();
            if (pd != null)
            {
                var m = new MoneyToStr("UAH", "UKR", "TEXT");
                 pd.First().CurrName = m.convertValue(pd.First().Total);
            }
            
            dataForReport.Add("PayDoc", pd);
            dataForReport.Add("Enterprise", db.KagentList.Where(w =>  w.KType == 3).Take( 1 ).ToList());

            Print(dataForReport, template_name);

        }

        public static void PriseListReport(int pl_id, BaseEntities db)
        {
            var dataForReport = new Dictionary<string, IList>();

            var pl = db.PriceList.Where(w => w.PlId == pl_id).AsNoTracking().ToList();
            var pl_d = db.GetPriceListDet(pl_id).ToList().Select(s => new
            {
                s.BarCode,
                s.GrpName,
                s.Name,
                s.Price,
                BarCode_Price = Getlable(s.Price, s.BarCode)
            }).ToList();

            dataForReport.Add("PriceList", pl);
            dataForReport.Add("PriceListDet", pl_d);

            Print(dataForReport, TemlateList.p_list);
        }

        private static string Getlable(Decimal? price , String code)
        {
            var split_price = Math.Round(price.Value, 2).ToString(CultureInfo.CreateSpecificCulture("en-GB")).Split('.');

            return "*" + code + "+" + split_price[0] + "+" + split_price[1] + "*";
        }

        private static void Print(Dictionary<string, IList> data_for_report, string temlate)
        {

            String result_file = Path.Combine(rep_path, Path.GetFileName(temlate) + "_" + DateTime.Now.Ticks.ToString() + Path.GetExtension(temlate));
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
