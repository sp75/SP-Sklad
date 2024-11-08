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
    public partial class frmOpenStoreCashRegisterSyncMonitor : DevExpress.XtraEditors.XtraForm
    {
        public frmOpenStoreCashRegisterSyncMonitor()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ucOpenStoreCashRegisterSyncMonitor1.GetData();
        }
    }
}