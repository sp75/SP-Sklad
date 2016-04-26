using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP_Sklad.SkladData;
using SpreadsheetReportBuilder;

namespace SP_Sklad.Reports
{
    class PrintWB
    {
        void WayBillInReport(int wbill_id, BaseEntities db)
        {

     //       ReportBuilder.GenerateReport(dataForReport, @"C:\Temp\Temleyt.xlsx", @"C:\Temp\TemleytRez.xlsx", false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Object> ob = new List<object>();
            var per1 = new
            {
                Code = 10,
                Name = "Hello World",
                Date = DateTime.Now,
                num = 3.56
            };
            ob.Add(per1);

            var per2 = new
            {
                Code = 20,
                Name = "Hello World2",
                Date = DateTime.Now.AddDays(2),
                num = 7.56
            };
            ob.Add(per2);

            var per3 = new
            {
                Code = 30,
                Name = "Hello World3",
                Date = DateTime.Now.AddDays(5),
                num = 10.56
            };
            ob.Add(per3);

            List<Object> child = new List<object>();
            child.Add(new
            {
                id = 1,
                name = "Test1",
                parent_id = 10,

            });
            child.Add(new
            {
                id = 2,
                name = "Test2",
                parent_id = 10,

            });
            child.Add(new
            {
                id = 3,
                name = "Test3",
                parent_id = 20,

            });
            child.Add(new
            {
                id = 5,
                name = "Test4",
                parent_id = 20,

            });

            List<Object> child2 = new List<object>();
            child2.Add(new
            {
                id = 1,
                name = "WWWWWWWW",
                parent_id = 10,
            });


            List<Object> rel = new List<object>();
            rel.Add(new
            {
                pk = "Code",
                fk = "parent_id",
                master_table = "RListRange",
                child_table = "child_range"
            });

            rel.Add(new
            {
                pk = "Code",
                fk = "parent_id",
                master_table = "RListRange",
                child_table = "child_range_2"
            });

            var dataForReport = new Dictionary<string, IList>();
            //    dataForReport.Add("People", list);
            dataForReport.Add("List", ob);
            dataForReport.Add("www", ob);
            dataForReport.Add("RListRange", ob);
            dataForReport.Add("RListRange2", ob);
            dataForReport.Add("child_range", child);
            dataForReport.Add("child_range_2", child2);
            dataForReport.Add("_realation_", rel);



            //  FormulaExcel.CalculationlFormulaExcel.CalcSpreadsheetDocument(ReportBuilderXLS.GenerateReport(dataForReport, @".\Temleyt.xlsx", false), true, @".\TemleytRez.xlsx");
            //    ReportBuilderXLS.GenerateReport(dataForReport, @".\Temleyt.xlsx", @".\TemleytRez.xlsx", false);
            //    Process.Start(@".\TemleytRez.xlsx");

            ReportBuilder.GenerateReport(dataForReport, @"C:\Temp\Temleyt.xlsx", @"C:\Temp\TemleytRez.xlsx", false);



        }

    }
}
