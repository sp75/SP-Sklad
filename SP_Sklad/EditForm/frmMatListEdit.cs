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
using SP_Sklad.Common;

namespace SP_Sklad.EditForm
{
    public partial class frmMatListEdit : DevExpress.XtraEditors.XtraForm
    {
        private ComPortHelper com_port { get; set; }

        public frmMatListEdit(String MatName)
        {
            InitializeComponent();
            AmountEdit.EditValue = 0;
            PriceEdit.EditValue = 0;
            com_port = new ComPortHelper();
            Text = MatName;
        }

        private void AmountEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (com_port.weight > 0)
            {
                timer1.Stop();
                AmountEdit.EditValue = com_port.weight;
                com_port.Close();
            }
        }

        private void frmMatListEdit_Load(object sender, EventArgs e)
        {
            GetWeight();
        }

        void GetWeight()
        {
            timer1.Start();
            try
            {
                com_port.Open();
            }
            catch { }
        }

        private void frmMatListEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            com_port.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            GetWeight();
        }

        private void frmMatListEdit_Shown(object sender, EventArgs e)
        {
            AmountEdit.Focus();
        }

        private void AmountEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 )
            {
                OkButton.PerformClick();
            }
        }
    }
}