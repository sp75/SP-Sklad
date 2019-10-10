using System;
using System.Data;
using System.Linq;
using SP_Sklad.SkladData;
using EntityState = System.Data.Entity.EntityState;
using SP_Sklad.Common;
using DevExpress.XtraEditors;
using SP_Sklad.EditForm;

namespace SP_Sklad.WBDetForm
{
    public partial class frmIntermediateWeighingDet : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private Guid? _id { get; set; }
        private IntermediateWeighingDet det { get; set; }
        private IntermediateWeighing _iw { get; set; }

        public frmIntermediateWeighingDet(BaseEntities db, Guid? id, IntermediateWeighing iw)
        {
            _id = id;
            _db = db;
            _iw = iw;

            InitializeComponent();

            MatComboBox.Properties.DataSource = DB.SkladBase().GetWayBillDetOut(_iw.WbillId).OrderBy(o => o.Num).ToList();
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
                    CreatedDate = DBHelper.ServerDateTime()
                };
            }

            IntermediateWeighingDetBS.DataSource = det;

            GetOk();
        }

        private void GetOk()
        {
            OkButton.Enabled = !String.IsNullOrEmpty(MatComboBox.Text) ;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
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

            dynamic row = MatComboBox.GetSelectedDataRow();

            if (row == null)
            {
                return;
            }

            int mat_id = row.MatId;
            det.MatId = mat_id;

            GetOk();
        }

         private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void AmountEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var frm = new frmMatListEdit(MatComboBox.Text);

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
    }
}