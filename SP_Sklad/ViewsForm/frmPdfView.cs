using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SP_Sklad.ViewsForm
{
    public partial class frmPdfView : DevExpress.XtraEditors.XtraForm
    {
        private byte[] _pdf { get; set; }
        public frmPdfView(byte[] pdf)
        {
            InitializeComponent();
            _pdf = pdf;
        }

        private void frmPdfView_Load(object sender, EventArgs e)
        {
            Stream stream = new MemoryStream(_pdf);
            pdfViewer1.LoadDocument(stream);
        }
    }
}