using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.Common.WayBills;
using SP_Sklad.SkladData;

namespace SP_Sklad
{
    public partial class frmOutMatList : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private DateTime _startDate { get; set; }
        private DateTime _endDate { get; set; }
        private int _matId { get; set; }
        private int _kaId { get; set; }
        public List<GetPosOut_Result> pos_out_list { get; set; }

        public frmOutMatList(BaseEntities db, DateTime startDate, DateTime endDate, int matId, int kaId)
        {
            InitializeComponent();
            _matId = matId;
            _startDate = startDate;
            _endDate = endDate;
            _kaId = kaId;
            _db = db;
        }

        private void frmOutMatList_Load(object sender, EventArgs e)
        {
            MatComboBox.Properties.DataSource = new List<object>() { new { MatId = 0, Name = "Усі" } }.Concat(_db.Materials.Where(w => w.Deleted == 0).Select(s => new { s.MatId, s.Name }));
            MatComboBox.EditValue = _matId;
            StartDate.DateTime = _startDate;
            EndDate.DateTime = _endDate;

            GetData();
        }

        private void GetData()
        {
            if (StartDate.DateTime.Date <= DateTime.MinValue || EndDate.DateTime <= DateTime.MinValue)
            {
                return;
            }

            pos_out_list = pos_out_list = _db.GetPosOut(StartDate.DateTime.Date, EndDate.DateTime, (int)MatComboBox.EditValue, _kaId, -1).ToList();
           
            GetPosOutBS.DataSource = pos_out_list;
        }

        private void StartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (StartDate.ContainsFocus || EndDate.ContainsFocus || MatComboBox.ContainsFocus)
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
