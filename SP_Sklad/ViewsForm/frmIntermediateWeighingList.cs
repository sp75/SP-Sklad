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
using SP_Sklad.SkladData;
using static SP_Sklad.WBDetForm.frmIntermediateWeighingDet;
using System.Collections.ObjectModel;

namespace SP_Sklad.ViewsForm
{
    public partial class frmIntermediateWeighingList : DevExpress.XtraEditors.XtraForm
    {
        public make_det focused_row { get; set; }

        private List<make_det> _list { get; set; }
        public int wb_id { get; set; }

        public frmIntermediateWeighingList(List<make_det> list)
        {
            InitializeComponent();
            _list = list;
            GetWayBillMakeDetBS.DataSource = list;
            wb_id = list.First().WbillId;
        }

        private void frmUserGroup_Load(object sender, EventArgs e)
        {
            var wb_maked = DB.SkladBase().WayBillMake.Where(w => w.WbillId == wb_id).Select(s => new { s.RecipeCount, s.MatRecipe.Materials.Name }).FirstOrDefault();

           Text = "Список сировини для зважування, Рецепт: "+ wb_maked.Name;

            var det_list = DB.SkladBase().v_IntermediateWeighingDet.Where(w => w.WbillId == wb_id).ToList();

            var empty_list = _list.Where(w => !det_list.Any(a => a.MatId == w.MatId)).Select(ss => new make_det
            {
                MsrName = ss.MsrName,
                MatName = ss.MatName,
                AmountIntermediateWeighing = 0,
                Rn = 1,
                MatId = ss.MatId,
                WbillId = ss.WbillId,
                RecipeCount = ss.RecipeCount,
                IntermediateWeighingCount = ss.IntermediateWeighingCount
            }).ToList();

            var list = det_list.GroupBy(g => g.MatName)  // PARTITION BY ^^^^
           .Select(c => c.OrderBy(o => o.CreatedDate).Select((v, i) => new { i, v }).ToList()) //  ORDER BY ^^
           .SelectMany(c => c)
           .Select(c => new make_det
           {
               MsrName = c.v.MsrName,
               MatName = c.v.MatName,
               AmountIntermediateWeighing = c.v.Total,
               MatId = c.v.MatId,
               WbillId = c.v.WbillId,
               IntermediateWeighingCount = det_list.Count(co => co.MatId == c.v.MatId),
               RecipeCount = wb_maked.RecipeCount,
               Rn = c.i + 1
           }).ToList();

            list.AddRange(empty_list);

            bindingSource1.DataSource = list;

            WaybillDetInGridControl.DataSource = det_list.OrderBy(o => o.CreatedDate).ToList();
        }

        private void frmUserGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }

        private void UsersGroupGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
           var list = DB.SkladBase().v_IntermediateWeighingDet.Where(w => w.WbillId == focused_row.WbillId && w.MatId == focused_row.MatId).ToList().Select(s => new make_det
            {
                MsrName = s.MsrName,
                AmountIntermediateWeighing = s.Amount
            }).ToList();

           OkButton.Enabled = list.Count() < focused_row.RecipeCount;
        }

        private void RecipeComboBox_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void UsersGroupGridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
          /*  if (e.RowHandle < 0)
            {
                return;
            }

            var wh_row = UsersGroupGridView.GetRow(e.RowHandle) as make_det;

            if (wh_row != null && wh_row.IntermediateWeighingCount >= wh_row.RecipeCount)
            {
                e.Appearance.ForeColor = Color.Blue;
            }*/
        }

        private void pivotGridControl1_FocusedCellChanged(object sender, EventArgs e)
        {
        
        }

        private void pivotGridControl1_CellClick(object sender, DevExpress.XtraPivotGrid.PivotCellEventArgs e)
        {
            var dataSource = pivotGridControl1.CreateDrillDownDataSource(0, e.RowIndex);
            if (dataSource.RowCount == 1)
            {
                var row = dataSource[0];
                focused_row = (bindingSource1.DataSource as List<make_det>)?[row.ListSourceRowIndex];

                OkButton.Enabled = focused_row.IntermediateWeighingCount < focused_row.RecipeCount;
            }
        }

        private void pivotGridControl1_CellDoubleClick(object sender, DevExpress.XtraPivotGrid.PivotCellEventArgs e)
        {
            OkButton.PerformClick();
        }
    }
}