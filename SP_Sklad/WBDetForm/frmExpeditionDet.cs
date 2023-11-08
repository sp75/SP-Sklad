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
        private int? _wb_id { get; set; }

        private bool isNewRecord { get; set; }

        public frmExpeditionDet(BaseEntities db, Guid? id, Expedition ex, int? wb_id)
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
          var msr_list =   _db.WaybillDet.Where(w => w.WbillId == _wb_id).Select(s => s.Materials.Measures).Distinct().ToList();
        //    TareMatEdit.Properties.DataSource = _db.Tara.Where(w => w.TypeId == 8).ToList();
        //    TareMatEdit1.Properties.DataSource = TareMatEdit.Properties.DataSource;
        //    TareMatEdit2.Properties.DataSource = TareMatEdit.Properties.DataSource;
        //    repositoryItemLookUpEdit1.DataSource = DBHelper.


            if (!_id.HasValue && _wb_id.HasValue)
            {
                _id = Guid.NewGuid();

                det = _db.ExpeditionDet.Add( new ExpeditionDet
                {
                    Id = _id.Value,
                    Amount = 0,
                    ExpeditionId = _ex.Id,
                    CreatedAt = DBHelper.ServerDateTime(),
                    WbillId = _wb_id.Value,
                    MId = msr_list.FirstOrDefault()?.MId
                });

                isNewRecord = true;
            }
            else
            {
                det = _db.ExpeditionDet.Find(_id);

                _wb_id = det.WbillId;
            }

            _db.SaveChanges();

            MsrComboBox.Properties.DataSource = msr_list;
            ExpeditionDetBS.DataSource = det;

       //     GetMatDet();

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
            isNewRecord = false;

            _db.SetDocRel(_ex.Id,  _db.WaybillList.Find(_wb_id).Id);

            //    if (_db.Entry<ExpeditionDet>(det).State == EntityState.Detached)
            //     {
            //        _db.ExpeditionDet.Add(det);
            //     }
            _db.SaveChanges();
        }

        private void RecipeComboBox_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            /*  if (e.Button.Index == 1)
              {
                  MatComboBox.EditValue = IHelper.ShowDirectList(MatComboBox.EditValue, 13);
              }*/
        }

        private void AmountEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
             if (e.Button.Index == 1)
              {
                  using (var frm = new frmWeightEdit(Text, 1))
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

        private void frmIntermediateWeighingDet_Shown(object sender, EventArgs e)
        {
            AmountEdit.Properties.Buttons[1].Visible = DBHelper.WeighingScales_1 != null;
            AmountEdit.Focus();
        }
 
        private void MsrComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var r = MsrComboBox.GetSelectedDataRow() as Measures;
            if (r == null)
            {
                return;
            }
            det.MId = r.MId;

            det.WbAmount = _db.WaybillDet.Where(w => w.WbillId == det.WbillId && w.Materials.MId == r.MId).Select(s => s.Amount).ToList().Sum();
            textEdit1.EditValue = det.WbAmount;

            det.TareWeight = _db.WayBillTmc.Where(w => w.WbillId == det.WbillId).Select(s => s.Amount * s.Materials.Weight ?? 0).ToList().Sum();
            TareWeightEdit.EditValue = det.TareWeight;

            det.TareQuantity = _db.WayBillTmc.Where(w => w.WbillId == det.WbillId).Select(s => s.Amount).ToList().Sum();
            calcEdit1.EditValue = det.TareQuantity;

            _db.SaveChanges();

            ExpeditionWBMaterialsDetBS.DataSource = _db.v_ExpeditionWBMaterialsDet.AsNoTracking().Where(w => w.Id == det.Id && w.MId == r.MId).ToList();

            labelControl9.Text = MsrComboBox.Text;
            gridColumn1.Caption = "По документу, "+MsrComboBox.Text;

            GetTotalWeight();
        }

        private void GetTotalWeight()
        {
            var msr = MsrComboBox.GetSelectedDataRow() as Measures;

       //     var t = TareMatEdit.GetSelectedDataRow() as Tara;
     //       var t1 = TareMatEdit1.GetSelectedDataRow() as Tara;
      //      var t2 = TareMatEdit2.GetSelectedDataRow() as Tara;

      //      var total_tare_weight = ((t?.Weight ?? 0) * calcEdit1.Value) + ((t1?.Weight ?? 0) * calcEdit2.Value) + ((t2?.Weight ?? 0) * calcEdit3.Value);

            var verified_weight_without_tare = msr?.MId == 2 ? AmountEdit.Value - TareWeightEdit.Value : AmountEdit.Value;

            det.TotalWeight = verified_weight_without_tare - textEdit1.Value;
            TotalDoc.EditValue = det.TotalWeight;
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetTotalWeight();
        }


        private void MatChangeGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Amount")
            {
                var row = MatChangeGridView.GetFocusedRow() as v_ExpeditionWBMaterialsDet;

                if (row.ExpeditionMaterialsDetId.HasValue)
                {
                    var r = _db.ExpeditionMaterialsDet.Find(row.ExpeditionMaterialsDetId);
                    r.Amount = Convert.ToDecimal(e.Value);
                }
                else
                {
                    var new_r = _db.ExpeditionMaterialsDet.Add(new ExpeditionMaterialsDet()
                    {
                        Id = Guid.NewGuid(),
                        ExpeditionDetId = det.Id,
                        Amount = Convert.ToDecimal(e.Value),
                        MatId = row.MatId
                    });

                    row.ExpeditionMaterialsDetId = new_r.Id;
                }
            }
        }

        private void frmExpeditionDet_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(isNewRecord)
            {
                _db.ExpeditionDet.Remove(det);
                _db.SaveChanges();
            }
        }

        private void TareWeightEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetTotalWeight();
        }
    }
}