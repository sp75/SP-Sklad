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
            }
        }

        public static void WayBillInReport(int doc_id, BaseEntities db, string template_name)
        {
            var dataForReport = new Dictionary<string, IList>();

            String result_file = Path.Combine(rep_path, template_name);
            String template_file = Path.Combine(template_path, template_name);

            var wb = db.v_WaybillList.Where(w => w.DocId == doc_id).ToList();

            dataForReport.Add("WayBillList", wb);
            dataForReport.Add("range1", db.GetWaybillDetIn(wb.First().WbillId).ToList());

            if (File.Exists(template_file))
            {
                ReportBuilder.GenerateReport(dataForReport, template_file, result_file, false);
            }

            if (File.Exists(result_file))
            {
                Process.Start(result_file);
            }
        }
        public static void WayBillOutReport(int doc_id, BaseEntities db, string template_name)
        {
            var data_report = new Dictionary<string, IList>();

            String result_file = Path.Combine(rep_path, template_name);
            String template_file = Path.Combine(template_path, template_name);

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

            if (File.Exists(template_file))
            {
                ReportBuilder.GenerateReport(data_report, template_file, result_file, false);
            }

            if (File.Exists(result_file))
            {
                Process.Start(result_file);
            }
        }
    }
}
