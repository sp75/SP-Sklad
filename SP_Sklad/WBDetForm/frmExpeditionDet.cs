using System;
using System.Data;
using System.Linq;
using SP_Sklad.SkladData;
using EntityState = System.Data.Entity.EntityState;
using SP_Sklad.Common;
using DevExpress.XtraEditors;
using SP_Sklad.EditForm;
using SP_Sklad.ViewsForm;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SP_Sklad.WBDetForm
{
    public partial class frmExpeditionDet : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private Guid? _id { get; set; }
        private ExpeditionDet det { get; set; }
        private Expedition _ex { get; set; }
        private int _wb_id { get; set; }

        private bool isNewRecord { get; set; }

        public frmExpeditionDet(BaseEntities db, Guid? id, Expedition ex, int wb_id)
        {
            _id = id;
            _db = db;
            _wb_id = wb_id;
            _ex = ex;

            InitializeComponent();

            /*    using (var s = new UserSettingsRepository(UserSession.UserId, _db))
                {
                    AmountEdit.ReadOnly = !(s.AccessEditWeight == "1");
                }*/
        }

        private void frmIntermediateWeighingDet_Load(object sender, EventArgs e)
        {
            MsrComboBox.Properties.DataSource = DBHelper.MeasuresList;

            if (!_id.HasValue)
            {
                _id = Guid.NewGuid();

                det = new ExpeditionDet
                {
                    Id = _id.Value,
                    Amount = 0,
                    ExpeditionId = _ex.Id,
                    CreatedAt = DBHelper.ServerDateTime(),
                    WbillId = _wb_id,
                    MId = 2
                };

                isNewRecord = true;
            }
            else
            {
                det = _db.ExpeditionDet.Find(_id);
            }

            ExpeditionDetBS.DataSource = det;

            GetOk();
        }

        private void GetOk()
        {
            /*   var row = MatComboBox.GetSelectedDataRow() as make_det;

               if (row != null)
               {
                   var plan_weighing = Math.Round(Convert.ToDecimal((row.AmountByRecipe * _iw.Amount) / row.TotalWeightByRecipe), 2);

                   ByRecipeEdit.EditValue = row.AmountByRecipe;
                   IntermediateWeighingEdit.EditValue = row.AmountIntermediateWeighing;
                   TotalEdit.EditValue = row.AmountByRecipe - (row.AmountIntermediateWeighing ?? 0);

                   var deviation = _db.MatRecDet.FirstOrDefault(w => w.MatId == row.MatId && w.RecId == row.RecId)?.Deviation ?? 1000000;
                   var vizok = (dynamic)TareMatEdit.GetSelectedDataRow();
                   var netto_amount = AmountEdit.Value - TaraCalcEdit.Value - (vizok != null ? vizok.Weight : 0);

                   CalcAmount.EditValue = plan_weighing + TaraCalcEdit.Value + (vizok != null ? vizok.Weight : 0);


                  OkButton.Enabled = !String.IsNullOrEmpty(MatComboBox.Text)
                       && ((plan_weighing + deviation) >= netto_amount && (plan_weighing - deviation) <= netto_amount);
                   //    && (TotalEdit.Value + deviation) >= netto_amount;
               }
               else
               {
                   OkButton.Enabled = false;
               }*/
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            //      det.Total = det.Amount - det.TaraAmount - (vizok != null ? (decimal)vizok.Weight : 0);
            //      det.TaraTotal = det.TaraAmount + (vizok != null ? (decimal)vizok.Weight : 0);

            if (_db.Entry<ExpeditionDet>(det).State == EntityState.Detached)
            {
                _db.ExpeditionDet.Add(det);
            }
            _db.SaveChanges();
        }

        private void RecipeComboBox_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            /*  if (e.Button.Index == 1)
              {
                  MatComboBox.EditValue = IHelper.ShowDirectList(MatComboBox.EditValue, 13);
              }*/
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void AmountEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            /*  if (e.Button.Index == 1)
              {
                  using (var frm = new frmWeightEdit(MatComboBox.Text, 1))
                  {

                      if (frm.ShowDialog() == DialogResult.OK)
                      {
                          AmountEdit.EditValue = frm.AmountEdit.Value;

                          GetOk();
                      }
                  }
              }*/

        }

        private void AmountEdit_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((CalcEdit)sender).SelectAll();
        }


        private void frmIntermediateWeighingDet_Shown(object sender, EventArgs e)
        {
            AmountEdit.Properties.Buttons[1].Visible = DBHelper.WeighingScales_1 != null;
            AmountEdit.Properties.Buttons[2].Visible = DBHelper.WeighingScales_2 != null;

            AmountEdit.Focus();
        }

        private void AmountEdit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void MsrComboBox_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
    }
}