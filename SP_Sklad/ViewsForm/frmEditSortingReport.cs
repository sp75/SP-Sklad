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
    public partial class frmEditSortingReport : DevExpress.XtraEditors.XtraForm
    {
        private ReportSortedFields focused_wh
        {
            get { return WhRemainGridView.GetFocusedRow() as ReportSortedFields; }
        }
        private BaseEntities _db = new BaseEntities();

        public frmEditSortingReport(int rep_id)
        {
            InitializeComponent();

            RemainOnWhGrid.DataSource = _db.ReportSortedFields.Where(w => w.RepId == rep_id).OrderBy(o => o.Idx).ToList();
        }

        private void frmRemainOnWh_Load(object sender, EventArgs e)
        {
         
        }

        private void WhRemainGridView_DoubleClick(object sender, EventArgs e)
        {
            //OkButton.PerformClick();
        }

        private void WhRemainGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "OrderDirection")
            {
                var i = _db.ReportSortedFields.Find(focused_wh.Id);
                i.OrderDirection = Convert.ToInt32(e.Value);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
        }
    }
}
