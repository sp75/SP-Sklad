using System;
using System.Data;
using System.Linq;
//using EntityState = System.Data.Entity.EntityState;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using System.Windows.Forms;
using SP.Base.Models;
using System.Data.Entity;
using SP.IntermediateWeighing;

namespace SP.Stuffing
{
    public partial class frmIntermediateWeighingDet : DevExpress.XtraEditors.XtraForm
    {
        private SPBaseModel _db { get; set; }
        private Guid? _id { get; set; }
        private IntermediateWeighingDet det { get; set; }
        private SP.Base.Models.IntermediateWeighing _iw { get; set; }
        private List<make_det> mat_list { get; set; }
        
        private bool isNewRecord { get; set; }

        public frmIntermediateWeighingDet(SPBaseModel db, Guid? id, SP.Base.Models.IntermediateWeighing iw)
        {
            _id = id;
            _db = db;
            _iw = iw;

            InitializeComponent();

        }

        private void frmPlannedCalculationDetDet_Load(object sender, EventArgs e)
        {
            det = _db.IntermediateWeighingDet.Find(_id);

            if (det == null)
            {
                det = new IntermediateWeighingDet
                {
                    Id = Guid.NewGuid(),
                    Amount = 0,
                    IntermediateWeighingId = _iw.Id,
                    CreatedDate = DBHelper.ServerDateTime(),
                    TaraAmount = 0
                };

                isNewRecord = true;
            }


        //    var wh_list = DB.SkladBase().UserAccessWh.Where(w => w.UserId == DBHelper.CurrentUser.UserId).Select(s => s.WId).ToList();
            var group_list = _db.UserAccessMatGroup.Where(w => w.UserId == DBHelper.CurrentUser.UserId).Select(s => s.GrpId).ToList();
            var wbm = _db.WayBillMake.FirstOrDefault(w => w.WbillId == _iw.WbillId);

            mat_list = _db.GetWayBillMakeDet(_iw.WbillId).Where(w =>/* wh_list.Contains(w.MatDefWId.Value)*/ group_list.Contains(w.GrpId.Value) && w.Rsv == 0).OrderBy(o => o.Num).ToList().Select(s => new make_det
            {
                MatName = s.MatName,
                MsrName = s.MsrName,
                AmountByRecipe = s.AmountByRecipe,
                AmountIntermediateWeighing = _db.v_IntermediateWeighingDet.Where(w => w.WbillId == _iw.WbillId && w.MatId == s.MatId && w.Id != det.Id).Sum(st=> st.Total),
                MatId = s.MatId,
                WbillId = _iw.WbillId,
                RecipeCount = wbm.RecipeCount,
                IntermediateWeighingCount = _db.v_IntermediateWeighingDet.Where(w => w.WbillId == _iw.WbillId && w.MatId == s.MatId && w.Id != det.Id).Count(),
                TotalWeightByRecipe = wbm.AmountByRecipe,
                RecId = wbm.RecId
            }).ToList();

            if (isNewRecord)
            {
                if (mat_list.Any())
                {
                    using (var f = new frmIntermediateWeighingList(mat_list))
                    {
                        if (f.ShowDialog() == DialogResult.OK)
                        {
                            det.MatId = f.focused_row != null ? f.focused_row.MatId : det.MatId;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Сировина для зважування відсутня");
                }
            }


            MatComboBox.Properties.DataSource = mat_list;

            IntermediateWeighingDetBS.DataSource = det;

            TareMatEdit.Properties.DataSource = _db.Tara.Where(w => w.TypeId == 5).Select(s => new { MatId = s.TaraId, s.Name, s.Artikul, s.Weight }).ToList();


            GetOk();
        }

        private void GetOk()
        {
            var row = MatComboBox.GetSelectedDataRow() as make_det;

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
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var vizok = (dynamic)TareMatEdit.GetSelectedDataRow();

            det.Total = det.Amount - det.TaraAmount - (vizok != null ? (decimal)vizok.Weight : 0);
            det.TaraTotal = det.TaraAmount + (vizok != null ? (decimal)vizok.Weight : 0);

            if (_db.Entry<IntermediateWeighingDet>(det).State == EntityState.Detached)
            {
                _db.IntermediateWeighingDet.Add(det);
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

        private void RecipeComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (!MatComboBox.ContainsFocus)
            {
                return;
            }

            var row = MatComboBox.GetSelectedDataRow() as make_det;

            if (row == null)
            {
                return;
            }

            det.MatId = row.MatId;

         
            GetOk();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void AmountEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                using (var frm = new frmWeightEdit(MatComboBox.Text, 1))
                {

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        AmountEdit.EditValue = frm.AmountEdit.Value;

                        GetOk();
                    }
                }
            }

            if (e.Button.Index == 2)
            {
                using (var frm = new frmWeightEdit(MatComboBox.Text, 2))
                {

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        AmountEdit.EditValue = frm.AmountEdit.Value;

                        GetOk();
                    }
                }
            }
        }

        private void AmountEdit_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((CalcEdit)sender).SelectAll();
        }

        private void MatComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1 && mat_list.Any())
            {
                using (var f = new frmIntermediateWeighingList(mat_list))
                {
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        MatComboBox.EditValue = f.focused_row != null ? f.focused_row.MatId : MatComboBox.EditValue;
                        AmountEdit.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Сировина для зважування відсутня");
            }
        }

        private void frmIntermediateWeighingDet_Shown(object sender, EventArgs e)
        {
            AmountEdit.Focus();
        }

        private void AmountEdit_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && MatComboBox.EditValue != null)
            {
                OkButton.PerformClick();
            }
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void TaraMatEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1 )
            {
                TareMatEdit.EditValue = null;
            }
        }
    }
}