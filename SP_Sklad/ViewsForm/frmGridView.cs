using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using SP_Sklad.SkladData;

namespace SP_Sklad.ViewsForm
{
    public partial class frmGridView : DevExpress.XtraEditors.XtraForm
    {
       

        public frmGridView(string report_name, object data)
        {
            InitializeComponent();

            gridControl1.DataSource = data;
            Text = report_name;
        }

        private void frmRemainOnWh_Load(object sender, EventArgs e)
        {
          
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.OptionsBehavior.AlignGroupSummaryInGroupRow == DevExpress.Utils.DefaultBoolean.Default)
            {
                gridView1.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                gridView1.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.Default;
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (MemoryStream ms_xlsx = new MemoryStream())
            {
                gridView1.ExportToXlsx(ms_xlsx);
                new frmSpreadsheed(ms_xlsx.ToArray()).Show();
            }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (MemoryStream ms_pdf = new MemoryStream())
            {
                gridView1.ExportToPdf(ms_pdf);
                new frmPdfView(ms_pdf.ToArray()).Show();
            }
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.PrintDialog();
        }

        private void gridView1_CustomSummaryExists(object sender, DevExpress.Data.CustomSummaryExistEventArgs e)
        {
            GridSummaryItem item = e.Item as GridSummaryItem;
            if (item == null) return;
            GridColumn col = ((ColumnView)sender).Columns[item.FieldName];
            if (col == null) return;
            item.DisplayFormat = "{0:" + col.DisplayFormat.FormatString + "}";
        }
    }
}
