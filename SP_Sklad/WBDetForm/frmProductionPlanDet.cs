﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.SkladData;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBDetForm
{
    public partial class frmProductionPlanDet : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private Guid? _id { get; set; }
        private ProductionPlanDet det { get; set; }
        private ProductionPlans _pp { get; set; }

        public frmProductionPlanDet(BaseEntities db, Guid? id, ProductionPlans pp)
        {
            _id = id;
            _db = db;
            _pp = pp;

            InitializeComponent();

            WHComboBox.Properties.DataSource = DBHelper.WhList;

            RecipeComboBox.Properties.DataSource = DB.SkladBase().MatRecipe.Where(w => w.RType == 1 && !w.Archived).Select(s => new
            {
                RecId = s.RecId,
                Name = s.Name,
                Amount = s.Amount,
                MatName = s.Materials.Name,
                MatId = s.MatId,
                Out = s.Out
            }).ToList();

        }

        private void frmProductionPlanDet_Load(object sender, EventArgs e)
        {
            det = _db.ProductionPlanDet.Find(_id);

            if (det == null)
            {
                det = new ProductionPlanDet
                {
                    Id = Guid.NewGuid(),
                    Num = _db.ProductionPlanDet.Count(w => w.ProductionPlanId == _pp.Id) + 1,
                    ProductionPlanId = _pp.Id,
                    WhId = _pp.ManufId.Value,
                    Total = 0,
                    Amount = 0
                };
            }

            ProductionPlanDetBS.DataSource = det;

            GetOk();
        }

        private void  GetOk()
        {
            OkButton.Enabled = !String.IsNullOrEmpty(RecipeComboBox.Text) && !String.IsNullOrEmpty(WHComboBox.Text) && TotalEdit.Value > 0;
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
            dynamic row = RecipeComboBox.GetSelectedDataRow();

            if (row == null)
            {
                return;
            }

            det.RecId = Convert.ToInt32(RecipeComboBox.EditValue);

            int mat_id = row.MatId;
            var mat_remain = _db.v_MatRemains.Where(w => w.MatId == mat_id).OrderByDescending(o => o.OnDate).FirstOrDefault();

            det.Remain = mat_remain != null ? mat_remain.Remain : 0;
            OrderedEdit.EditValue = det.Remain + TotalEdit.Value;

            //     det.Total = CalcTotal();

            GetOk();
        }

        private decimal CalcTotal()
        {
            dynamic row = RecipeComboBox.GetSelectedDataRow();

            if (row == null)
            {
                return 0;
            }

            var real_amount = OrderedEdit.Value - RemainEdit.Value;
            var tmp_amount = (real_amount / (row.Out == 0 ? 100m : row.Out)) * 100;// real_amount + (real_amount - (real_amount * row.Out / 100));

            return Math.Ceiling(tmp_amount / row.Amount) * row.Amount;
        }

        private void RecipeComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                RecipeComboBox.EditValue = IHelper.ShowDirectList(RecipeComboBox.EditValue, 13);
            }
        }

        private void WHComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                WHComboBox.EditValue = IHelper.ShowDirectList(WHComboBox.EditValue, 2);
            }
        }

        private void TotalEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                OkButton.PerformClick();
            }
        }

        private void frmProductionPlanDet_Shown(object sender, EventArgs e)
        {
            RecipeComboBox.Focus();
        }

        private void TotalEdit_EditValueChanged(object sender, EventArgs e)
        {
            det.Amount = RemainEdit.Value + TotalEdit.Value;

            GetOk();
        }
    }
}
