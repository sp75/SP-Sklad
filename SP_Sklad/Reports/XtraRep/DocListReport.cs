using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SP_Sklad.SkladData;
using System.Linq;

namespace SP_Sklad.Reports.XtraRep
{
    public partial class DocListReport : DevExpress.XtraReports.UI.XtraReport
    {
        public DocListReport()
        {
            InitializeComponent();
        }

        private void DocListReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            bindingSource1.DataSource = DB.SkladBase().Banks.ToList();
            bindingSource2.DataSource = DB.SkladBase().Kagent.ToList();
        }

    }
}
