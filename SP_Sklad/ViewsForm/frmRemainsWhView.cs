using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;

namespace SP_Sklad.EditForm
{
    public partial class frmRemainsWhView : DevExpress.XtraEditors.XtraForm
    {
        private DiscCards _cart { get; set; }
        public string WhName { get; set; }
        public frmRemainsWhView( DiscCards cart =null)
        {
            InitializeComponent();
            _cart = cart;
        }

        private void frmWhCatalog_Load(object sender, EventArgs e)
        {
            ucWhMat.disc_card = _cart;

            if (!string.IsNullOrEmpty( WhName))
            {
                Text = "Залишки на складі: " + WhName;
            }
        }

        private void frmWhCatalog_Shown(object sender, EventArgs e)
        {
            ucWhMat.BarCodeEdit.Focus();
            var result = ucWhMat.GetMatOnWh();
        }

        private void ucWhMat_WhMatGridViewDoubleClick(object sender, EventArgs e)
        {
            OkButton.PerformClick();
        }
    }
}
