using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SP_Sklad.SkladData;
using System.Linq;

namespace SP_Sklad.Reports.XtraRep
{
    public partial class XtraReport55 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport55()
        {
            InitializeComponent();
            objectDataSource1.DataSource = DB.SkladBase().v_Report_55.OrderBy(o=> o.OnDate).ToList();
        }

    }
}
