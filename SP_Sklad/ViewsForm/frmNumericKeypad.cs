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

namespace SP_Sklad.ViewsForm
{
    public partial class frmNumericKeypad : DevExpress.XtraEditors.XtraForm
    {
        public frmNumericKeypad()
        {
            InitializeComponent();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            AmountEdit.Text += ((SimpleButton)sender).Text;
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            if (!AmountEdit.Text.Any(a => a == ','))
            {
                AmountEdit.Text += ",";
            }
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            AmountEdit.Text = string.IsNullOrEmpty(AmountEdit.Text) ? "" : AmountEdit.Text.Remove(AmountEdit.Text.Length - 1);
        }
    }
}
