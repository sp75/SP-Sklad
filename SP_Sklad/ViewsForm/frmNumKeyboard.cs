using SP_Sklad.Common;
using SP_Sklad.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad.ViewsForm
{
    public partial class frmNumKeyboard : DevExpress.XtraEditors.XtraForm
    {

        public frmNumKeyboard()
        {
            InitializeComponent();
        }


        private void frmTestComPort_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void frmNumKeyboard_Load(object sender, EventArgs e)
        {

        }

        private void AmountEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 )
            {
                numKeyboardUserControl2.Value = AmountEdit.Value;

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void frmNumKeyboard_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = AmountEdit;
        }
    }
}
