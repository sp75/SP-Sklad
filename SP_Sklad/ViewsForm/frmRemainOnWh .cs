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
    public partial class frmRemainOnWh : DevExpress.XtraEditors.XtraForm
    {
        public MatRemainByWh_Result focused_wh
        {
            get { return WhRemainGridView.GetFocusedRow() as MatRemainByWh_Result; }
        }

        public frmRemainOnWh(BaseEntities db, int mat_id)
        {
            InitializeComponent();
            RemainOnWhGrid.DataSource = db.MatRemainByWh(mat_id, 0, 0, DateTime.Now, "*", DBHelper.CurrentUser.UserId).ToList();
        }

        private void frmRemainOnWh_Load(object sender, EventArgs e)
        {
            OkButton.Enabled = (WhRemainGridView.RowCount > 0);
        }

        private void WhRemainGridView_DoubleClick(object sender, EventArgs e)
        {
            OkButton.PerformClick();
        }
    }
}
