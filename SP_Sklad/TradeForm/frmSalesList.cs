using SP_Sklad.SkladData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad.TradeForm
{
    public partial class frmSalesList : DevExpress.XtraEditors.XtraForm
    {
        public GetRetailWayBillList_Result wb_focused_row
        {
            get
            {
                return WbGridView.GetFocusedRow() as GetRetailWayBillList_Result;
            }
        }
        public frmSalesList(int kagent_id, int status, DateTime from_date, DateTime to_date)
        {
            InitializeComponent();


            GetTradeWayBillListBS.DataSource = new BaseEntities().GetRetailWayBillList(from_date, to_date, "-25", status, kagent_id, 1, DBHelper.CurrentUser.KaId).Where(w => w.SummAll > 0).OrderByDescending(o => o.OnDate).ToList();
        }

        private void frmDeferredCheck_Load(object sender, EventArgs e)
        {
            
        }

        private void OkButton_Click(object sender, EventArgs e)
        {

        }
    }
}
