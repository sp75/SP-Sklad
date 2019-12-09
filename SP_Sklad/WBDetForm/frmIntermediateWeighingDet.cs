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

namespace SP_Sklad.WBDetForm
{
    public partial class frmIntermediateWeighingDet : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private Guid? _id { get; set; }
        private IntermediateWeighingDet det { get; set; }
        private IntermediateWeighing _iw { get; set; }
        private List<GetWayBillMakeDet_Result> mat_list { get; set; }

        public frmIntermediateWeighingDet(BaseEntities db, Guid? id, IntermediateWeighing iw)
        {
            _id = id;
            _db = db;
            _iw = iw;

            InitializeComponent();

            var wh_list = DB.SkladBase().UserAccessWh.Where(w => w.UserId == DBHelper.CurrentUser.UserId).Select(s => s.WId).ToList();
            mat_list = DB.SkladBase().GetWayBillMakeDet(_iw.WbillId).Where(w => wh_list.Contains(w.wid.Value) && w.Rsv == 0).OrderBy(o => o.Num).ToList();

            MatComboBox.Properties.DataSource = mat_list;
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


                using (var f = new frmIntermediateWeighingList(mat_list))
                {
                    if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        det.MatId = f.focused_row.MatId;
                    }
                }
            }

            IntermediateWeighingDetBS.DataSource = det;
            GetOk();
        }

        private void GetOk()
        {
            OkButton.Enabled = !String.IsNullOrEmpty(MatComboBox.Text) ;

            var row = MatComboBox.GetSelectedDataRow() as GetWayBillMakeDet_Result;

            if (row != null)
            {
                ByRecipeEdit.EditValue = row.AmountByRecipe;
                IntermediateWeighingEdit.EditValue = row.AmountIntermediateWeighing ;
                TotalEdit.EditValue = row.AmountByRecipe - (row.AmountIntermediateWeighing ?? 0);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            det.Total = det.Amount - det.TaraAmount;

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

            var row = MatComboBox.GetSelectedDataRow() as GetWayBillMakeDet_Result;

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
                var frm = new frmWeightEdit(MatComboBox.Text);

                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    AmountEdit.EditValue = frm.AmountEdit.Value;
                }
            }
        }

        private void AmountEdit_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ((CalcEdit)sender).SelectAll();
        }

        private void MatComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index ==1 )
            {
                using (var f = new frmIntermediateWeighingList(mat_list))
                {
                    if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        MatComboBox.EditValue = f.focused_row.MatId;
                        AmountEdit.Focus();
                    }
                }
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
    }
}