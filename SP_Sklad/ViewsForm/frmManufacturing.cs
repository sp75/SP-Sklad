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

namespace SP_Sklad.ViewsForm
{
    public partial class frmManufacturing : Form
    {

        public WBListMake_Result wb_focused_row
        {
            get
            {
                return WbGridView.GetFocusedRow() as WBListMake_Result;
            }
        }

        public frmManufacturing(BaseEntities db)
        {
            InitializeComponent();

            var satrt_date = DateTime.Now.AddYears(-100);
            var end_date = DateTime.Now.AddYears(100);

            WBGridControl.DataSource = db.WBListMake(satrt_date.Date, end_date.Date.AddDays(1), 2, "*", 0, -20).ToList();

        }

        private void frmManufacturing_Load(object sender, EventArgs e)
        {

        }

        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            OkButton.PerformClick();
        }
    }
}
