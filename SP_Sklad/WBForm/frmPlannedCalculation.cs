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
using SP_Sklad.Common;
using SP_Sklad.WBDetForm;
using SP_Sklad.Reports;

namespace SP_Sklad.WBForm
{
    public partial class frmPlannedCalculation : DevExpress.XtraEditors.XtraForm
    {
        public BaseEntities _db { get; set; }

        public Guid? _doc_id { get; set; }

        private PlannedCalculation pc { get; set; }

        public bool is_new_record { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private v_PlannedCalculationDetDet pc_det_row
        {
            get
            {
                return gridView8.GetFocusedRow() as v_PlannedCalculationDetDet;
            }
        }

        public frmPlannedCalculation(Guid? doc_id = null)
        {
            is_new_record = false;
            _doc_id = doc_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            pc.UpdatedAt = DateTime.Now;

            var planned = _db.PlannedCalculation.Find(_doc_id);
            if (planned != null && planned.SessionId != UserSession.SessionId)
            {
                throw new Exception("Не можливо зберегти документ, тільки перегляд.");
            }

            _db.SaveChanges();

            is_new_record = false;

            Close();
        }

        private void frmPlannedCalculation_Load(object sender, EventArgs e)
        {
            PersonComboBox.Properties.DataSource = DBHelper.Persons;

            if (_doc_id == null)
            {
                is_new_record = true;

                pc = _db.PlannedCalculation.Add(new PlannedCalculation
                {
                    Id = Guid.NewGuid(),
                    OnDate = DBHelper.ServerDateTime(),
                    Num = "",
                    PersonId = DBHelper.CurrentUser.KaId,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId,
                });

                _db.SaveChanges();
            }
            else
            {
                pc = _db.PlannedCalculation.FirstOrDefault(f => f.Id == _doc_id);
            }

            if (pc != null)
            {
                _doc_id = pc.Id;

                pc.SessionId = (Guid?)UserSession.SessionId;
                pc.UpdatedBy = UserSession.UserId;
                pc.UpdatedAt = DateTime.Now;
                _db.SaveChanges();

                if (is_new_record)
                {
                    pc.Num = new BaseEntities().GetDocNum("planned_calc").FirstOrDefault();
                }
                PlannedCalculationBS.DataSource = pc;
            }

            lookUpEdit1.Properties.DataSource = _db.PriceList.Select(s => new { s.PlId, s.Name }).ToList();

            RefreshDet();
        }

        private void RefreshDet()
        {
            var list = _db.v_PlannedCalculationDetDet.AsNoTracking().Where(w => w.PlannedCalculationId == _doc_id).OrderBy(o => o.RecipeName).ToList();

            int top_row = gridView8.TopRowIndex;
            PlannedCalculationDetBS.DataSource = list;
            gridView8.TopRowIndex = top_row;

            GetOk();
        }

        private void GetOk()
        {
            //OkButton.Enabled = ProductionPlanDetBS.Count > 0;

            EditMaterialBtn.Enabled = PlannedCalculationDetBS.Count > 0;
            DelMaterialBtn.Enabled = PlannedCalculationDetBS.Count > 0;

            barButtonItem3.Enabled = lookUpEdit1.EditValue != null && lookUpEdit1.EditValue != DBNull.Value;
        }

        private void frmPlannedCalculation_FormClosed(object sender, FormClosedEventArgs e)
        {
            _db.UndoAllChanges();

            pc.SessionId = (pc.SessionId == UserSession.SessionId ? null : pc.SessionId);
            pc.UpdatedBy = UserSession.UserId;
            pc.UpdatedAt = DateTime.Now;
            _db.SaveChanges();

            if (is_new_record)
            {
                _db.DeleteWhere<PlannedCalculation>(w => w.Id == _doc_id);
            }

            _db.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WaybillDetInGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
        }

        private void OnDateDBEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                pc.OnDate = DBHelper.ServerDateTime();
                OnDateDBEdit.DateTime = pc.OnDate;
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmPlannedCalculationDet(_db, Guid.NewGuid(), pc).ShowDialog();

            RefreshDet();
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = gridView8.GetFocusedRow() as v_PlannedCalculationDetDet;
            new frmPlannedCalculationDet(_db, row.Id, pc).ShowDialog();

            RefreshDet();
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = gridView8.GetFocusedRow() as v_PlannedCalculationDetDet;
            if (row != null)
            {
                var det = _db.PlannedCalculationDet.Find(row.Id);
                if (det != null)
                {
                    _db.PlannedCalculationDet.Remove(det);
                }
                _db.SaveChanges();
                gridView8.DeleteSelectedRows();
            }
            GetOk();
        }

        private void gridView8_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pc_det_row != null)
            {
                IHelper.ShowMatRSV(pc_det_row.MatId, _db);
            }
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pc_det_row != null)
            {
                IHelper.ShowMatInfo(pc_det_row.MatId);
            }
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintDoc.Show(pc.Id, 22, _db);
        }

        private void lookUpEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                foreach (var item in _db.PlannedCalculationDet.Where(w => w.PlannedCalculationId == pc.Id).ToList())
                {
                    item.RecipePrice = _db.GetRecipePrice(item.RecId, pc.PlId).FirstOrDefault();
                    item.SalesPrice = _db.PriceListDet.Where(w => w.PlId == pc.PlId && w.MatId == item.MatRecipe.MatId).Select(s => s.Price).FirstOrDefault();
                }
                _db.SaveChanges();
                RefreshDet();
            }
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void gridView8_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var wbd = _db.PlannedCalculationDet.FirstOrDefault(w => w.Id == pc_det_row.Id);

            if (e.Column.FieldName == "PlannedProfitability")
            {
                wbd.PlannedProfitability = Convert.ToDecimal(e.Value);


            }
            else if (e.Column.FieldName == "ProductionPlan")
            {
                wbd.ProductionPlan = Convert.ToDecimal(e.Value);
            }

            _db.SaveChanges();
            RefreshDet();
        }
    }
}