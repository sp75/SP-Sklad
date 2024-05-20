using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad.Reports
{
    public partial class frmDocumentDetails : DevExpress.XtraEditors.XtraForm
    {
        public frmDocumentDetails()
        {
            InitializeComponent();
        }

        private void frmDocumentDetails_Shown(object sender, EventArgs e)
        {
            ucDocumentDetails1.GetData();
        }

        private void frmDocumentDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            ucDocumentDetails1.SaveGridLayouts();
        }
    }
}