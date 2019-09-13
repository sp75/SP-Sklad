using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;

namespace SP_Sklad.ViewsForm
{
    public partial class frmSpreadsheed : DevExpress.XtraEditors.XtraForm
    {
        private byte[] _xlsx { get; set; }
        public frmSpreadsheed(byte[] xlsx)
        {
            InitializeComponent();
            _xlsx = xlsx;
        }

        private void frmSpreadsheed_Load(object sender, EventArgs e)
        {
            spreadsheetControl1.LoadDocument(_xlsx, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
        }

        public void Print()
        {
            spreadsheetControl1.LoadDocument(_xlsx, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
            spreadsheetControl1.Print();
        }
    }
}