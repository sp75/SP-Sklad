using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static void Show(int doc_id, int doc_type, BaseEntities db)
        {
        //    db.SaveChanges();

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
            }
        }

        public static void WayBillInReport(int doc_id, BaseEntities db, string template_name)
        {
            var dataForReport = new Dictionary<string, IList>();

            var wb = db.v_WaybillList.Where(w => w.DocId == doc_id).ToList();

            dataForReport.Add("WayBillList", wb);
            dataForReport.Add("range1", db.GetWaybillDetIn(wb.First().WbillId).ToList());

            Print(dataForReport, template_name);
        }

        public static void WayBillReport(int doc_id, BaseEntities db, string template_name)
        {
            var dataForReport = new Dictionary<string, IList>();

            var wb = db.v_WaybillList.Where(w => w.DocId == doc_id).ToList();
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

            var wb = db.v_WaybillList.Where(w => w.DocId == doc_id).ToList();
           
            if (wb != null)
            {
                var m = new MoneyToStr("UAH", "UKR", "TEXT");
                wb.First().www = m.convertValue(wb.First().SummAll.Value);
            }

            var ent_id = wb.First().EntId;
            data_report.Add("EntAccount", db.EnterpriseAccount.Where(w => w.KaId == ent_id).ToList());
            data_report.Add("WayBillList", wb);
            data_report.Add("range1", db.GetWayBillDetOut(wb.First().WbillId).ToList());

            Print(data_report, template_name);
        }

        private static void Print(Dictionary<string, IList> data_for_report, string temlate)
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
