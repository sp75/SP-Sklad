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
using EntityState = System.Data.Entity.EntityState;
using SP_Sklad.Common;

namespace SP_Sklad.WBDetForm
{
    public partial class frmPlannedCalculationDet : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private Guid? _id { get; set; }
        private PlannedCalculationDet det { get; set; }
        private PlannedCalculation _pc { get; set; }

        public frmPlannedCalculationDet(BaseEntities db, Guid? id, PlannedCalculation pc)
        {
            _id = id;
            _db = db;
            _pc = pc;

            InitializeComponent();

            RecipeComboBox.Properties.DataSource = DB.SkladBase().MatRecipe.Where(w => w.RType == 1).Select(s => new
            {
                RecId = s.RecId,
                Name = s.Name,
                Amount = s.Amount,
                MatName = s.Materials.Name,
                MatId = s.MatId,
                Out = s.Out
            }).ToList();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void frmPlannedCalculationDetDet_Load(object sender, EventArgs e)
        {
            det = _db.PlannedCalculationDet.Find(_id);

            if (det == null)
            {
                det = new PlannedCalculationDet
                {
                    Id = Guid.NewGuid(),
                    PlannedCalculationId = _pc.Id,
                     
                  //  Num = _db.ProductionPlanDet.Count(w => w.ProductionPlanId == _pp.Id) + 1,
                 
                };
            }

            PlannedCalculationDetBS.DataSource = det;

            GetOk();
        }

        private void GetOk()
        {
            OkButton.Enabled = !String.IsNullOrEmpty(RecipeComboBox.Text) ;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (_db.Entry<PlannedCalculationDet>(det).State == EntityState.Detached)
            {
                _db.PlannedCalculationDet.Add(det);
            }
            _db.SaveChanges();
        }

        private void RecipeComboBox_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                RecipeComboBox.EditValue = IHelper.ShowDirectList(RecipeComboBox.EditValue, 13);
            }
        }

        private void RecipeComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (!RecipeComboBox.ContainsFocus)
            {
                return;
            }

            dynamic row = RecipeComboBox.GetSelectedDataRow();

            if (row == null)
            {
                return;
            }
            int RecId = row.RecId;
            decimal RecipeOut = row.Out == 0 ? 100 : row.Out;

            var main_sum = _db.MatRecDet.Where(w => w.RecId == RecId && w.Materials.MId == w.MatRecipe.Materials.MId).ToList().Sum(s => s.Amount);
            var ext_sum = _db.MatRecDet.Where(w => w.RecId == RecId && w.Materials.MId != w.MatRecipe.Materials.MId).ToList().Sum(s => (s.Materials.Weight ?? 0) * s.Amount);

            det.Amount = main_sum + ext_sum;
            det.RecipeOut = RecipeOut;
            det.RecipePrice = _db.GetRecipePrice(RecId).FirstOrDefault();


            GetOk();
        }

     /*   private Decimal? GetRecipePrice(int recipe_id)
        {
            var result = _db.MatRecDet.Where(w => w.RecId == recipe_id).ToList().Sum(s => s.Amount * _db.GetWMatTurnRemain(s.MatId, _pc.OnDate, 0).Select(ss => ss.AvgPrice).FirstOrDefault()) ;

            return result > 0 ? Math.Round(result.Value, 2) : (decimal?)null;
        }*/

        private void calcEdit2_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                dynamic row = RecipeComboBox.GetSelectedDataRow();

                if (row == null)
                {
                    return;
                }
                int RecId = row.RecId;

                calcEdit2.EditValue = _db.GetRecipePrice(RecId).FirstOrDefault();
            }
        }
    }
}