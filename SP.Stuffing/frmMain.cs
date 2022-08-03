using SP.Base;
using SP.Base.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace SP.Stuffing
{
    public partial class Form1 : Form
    {

        private v_IntermediateWeighing intermediate_weighing_focused_row => IntermediateWeighingGridView.GetFocusedRow() as v_IntermediateWeighing;

        public Form1()
        {
            InitializeComponent();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetIntermediateWeighing();
        }

        void GetIntermediateWeighing()
        {

            int top_row = IntermediateWeighingGridView.TopRowIndex;
            var satrt_date = IntermediateWeighingStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : IntermediateWeighingStartDate.DateTime;
            var end_date = IntermediateWeighingEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : IntermediateWeighingEndDate.DateTime;

            using (SPBaseModel _db = Database.SPBase())
            {
                IntermediateWeighingBS.DataSource = _db.v_IntermediateWeighing.Where(w => w.OnDate > satrt_date && w.OnDate <= end_date && w.Checked == 0).OrderBy(o => o.OnDate).ToList();
            }

            IntermediateWeighingGridView.TopRowIndex = top_row;

        }

        private void xtraTabControl6_PaddingChanged(object sender, EventArgs e)
        {
            if (intermediate_weighing_focused_row == null)
            {

                IntermediateWeighingDetBS.DataSource = null;
                gridControl16.DataSource = null;
                return;
            }

            using (SPBaseModel _db = Database.SPBase())
            {
                switch (xtraTabControl6.SelectedTabPageIndex)
                {
                    case 0:
                        var list = _db.v_IntermediateWeighingDet.AsNoTracking().Where(w => w.IntermediateWeighingId == intermediate_weighing_focused_row.Id).OrderBy(o => o.CreatedDate).ToList();

                        int top_row = WaybillDetInGridView.TopRowIndex;
                        IntermediateWeighingDetBS.DataSource = list;
                        WaybillDetInGridView.TopRowIndex = top_row;
                        break;


                    case 1:
                    //    gridControl16.DataSource = _db.GetRelDocList(intermediate_weighing_focused_row.Id).OrderBy(o => o.OnDate).ToList();
                        break;
                }
            }
        }

        private void IntermediateWeighingGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            xtraTabControl6_PaddingChanged(sender, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SPBaseModel _db = Database.SPBase())
            {

                var total = _db.IntermediateWeighing.Where(w => w.WbillId == intermediate_weighing_focused_row.WbillId).Sum(s => s.Amount);
                var amount_by_recipe = _db.WayBillMake.FirstOrDefault(w => w.WbillId == intermediate_weighing_focused_row.WbillId).AmountByRecipe;

                if ((total ?? 0) > (amount_by_recipe ?? 0))
                {
                    MessageBox.Show("Вага закладок по виробництву більша запланованої");

                    return;
                }

                var iw = _db.IntermediateWeighing.Find(intermediate_weighing_focused_row.Id);
                using (var f = new frmIntermediateWeighingDet(_db, Guid.NewGuid(), iw))
                {
                    f.ShowDialog();
                }
            }


         //   RefreshDet();
        }
    }
}
