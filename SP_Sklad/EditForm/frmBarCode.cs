using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.SkladData;

namespace SP_Sklad.ViewsForm
{
    public partial class frmBarCode : DevExpress.XtraEditors.XtraForm
    {

        public frmBarCode()
        {
            InitializeComponent();

        }

        private void frmSetDiscountCard_Shown(object sender, EventArgs e)
        {
            BarCodeEdit.Focus();
        }

        private void BarCodeEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                OkButton.PerformClick();
            }
        }
    }
}