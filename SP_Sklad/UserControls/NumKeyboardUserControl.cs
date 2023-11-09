using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Globalization;

namespace SP_Sklad.UserControls
{
    public partial class NumKeyboardUserControl : UserControl
    {
        public decimal Value { get; set; }
        public CalcEdit _amount_edit { get; set; }

        public NumKeyboardUserControl()
        {
            InitializeComponent();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            _amount_edit.Text += ((SimpleButton)sender).Text;
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            if (!_amount_edit.Text.Any(a => a == ','))
            {
                _amount_edit.Text += ",";
            }
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            _amount_edit.Text = string.IsNullOrEmpty(_amount_edit.Text) ? "" : _amount_edit.Text.Remove(_amount_edit.Text.Length - 1);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
        /*    if (_amount_edit != null)
            {
                Value = _amount_edit.Value;
            }
            else
            {
                Value = string.IsNullOrEmpty(_amount_edit.Text) ? 0 : Convert.ToDecimal(_amount_edit.Text.Replace(" ", "").Replace(',', '.'), new CultureInfo("en-US"));
            }*/

            Value = _amount_edit.Value;

            ((XtraForm)this.Parent).Close();
        }
    }
}
