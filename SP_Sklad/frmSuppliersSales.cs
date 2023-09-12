using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkladEngine.ModelViews;
using SP_Sklad.Common;
using SP_Sklad.Common.WayBills;
using SP_Sklad.SkladData;
using SP_Sklad.SkladData.ViewModels;

namespace SP_Sklad
{
    public partial class frmSuppliersSales : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private DateTime _startDate { get; set; }
        private DateTime _endDate { get; set; }
        private int _matId { get; set; }
        private int _SupplierId { get; set; }
        private int _w_type { get; set; }
        public List<GetSalesOfSuppliersView> pos_out_list { get; set; }

        public frmSuppliersSales(BaseEntities db, DateTime startDate, DateTime endDate, int SupplierId)
        {
            InitializeComponent();
            _startDate = startDate;
            _endDate = endDate;
            _SupplierId = SupplierId;
            _db = db;
        }

        private void frmOutMatList_Load(object sender, EventArgs e)
        {
            whKagentList.Properties.DataSource = DBHelper.KagentsList;
            whKagentList.EditValue = _SupplierId;

            StartDate.DateTime = _startDate;
            EndDate.DateTime = _endDate;

            GetData();
        }

        private void GetData()
        {
            OkButton.Enabled = false;

            if (StartDate.DateTime.Date <= DateTime.MinValue || EndDate.DateTime <= DateTime.MinValue)
            {
                return;
            }

            pos_out_list = _db.GetSalesOfSuppliers(StartDate.DateTime.Date, EndDate.DateTime, (int)whKagentList.EditValue).ToList();
            
            GetPosOutBS.DataSource = pos_out_list;

            OkButton.Enabled = pos_out_list.Count > 0;
        }

        private void StartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (StartDate.ContainsFocus || EndDate.ContainsFocus || whKagentList.ContainsFocus)
            {
                GetData();
            }
        }

        private void bandedGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) OkButton.PerformClick();
        }
    }
}
