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
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBDetForm
{
    public partial class frmProductionPlanDet : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private Guid? _id { get; set; }
        private ProductionPlanDet det { get; set; }

        public frmProductionPlanDet(BaseEntities db, Guid? id)
        {
            _id = id;
            _db = db;

            InitializeComponent();

            WHComboBox.Properties.DataSource = DBHelper.WhList();
            MatComboBox.Properties.DataSource = _db.MaterialsList.ToList();
        }

        private void frmProductionPlanDet_Load(object sender, EventArgs e)
        {
            det = _db.ProductionPlanDet.Find(_id);

            if (det == null)
            {
                det = new ProductionPlanDet()
                {
                    Id = Guid.NewGuid()
                };
            }

            ProductionPlanDetBS.DataSource = det;

          //  MatComboBox.Properties.DataSource = _db.WhMatGet(0, _wbd.WId, 0, DBHelper.ServerDateTime(), 0, "*", 0, "", DBHelper.CurrentUser.UserId, 0).ToList();

        //    GetOk();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (_db.Entry<ProductionPlanDet>(det).State == EntityState.Detached)
            {
                _db.ProductionPlanDet.Add(det);
            }
            _db.SaveChanges();

        }

        private void MatComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = (MaterialsList)MatComboBox.GetSelectedDataRow();
            if (row == null)
            {
                return;
            }

            var mat_remain = _db.v_MatRemains.Where(w => w.MatId == row.MatId).OrderByDescending(o => o.OnDate).FirstOrDefault();// GetMatRemain(det.WhId, row.MatId).FirstOrDefault();
            AmountEdit.EditValue = mat_remain.Remain;

            TotalEdit.Value = DiscountEdit.Value - AmountEdit.Value;
        }
    }
}
