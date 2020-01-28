using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid;
using SP_Sklad.Common;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBForm
{
    public partial class frmKAgentAdjustment : DevExpress.XtraEditors.XtraForm
    {
        public BaseEntities _db { get; set; }
        public Guid? _doc_id { get; set; }
        private KAgentAdjustment pp { get; set; }
        public bool is_new_record { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private v_KAgentAdjustmentDet pp_det_row
        {
            get
            {
                return KAgentAdjustmentDetGridView.GetFocusedRow() as v_KAgentAdjustmentDet;
            }
        }

        public frmKAgentAdjustment(Guid? doc_id = null)
        {
            is_new_record = false;
            _doc_id = doc_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();
        }

        private void frmProductionPlans_Load(object sender, EventArgs e)
        {
            PersonComboBox.Properties.DataSource = DBHelper.Persons;

            OperationTypeEdit.Properties.DataSource = _db.OperationTypes.Select(s => new { s.Id, s.Name }).ToList();
            WriteOffTypesEdit.Properties.DataSource = _db.WriteOffTypes.Select(s => new { s.Id, s.Name }).ToList();


            if (_doc_id == null)
            {
                is_new_record = true;

                pp = _db.KAgentAdjustment.Add(new KAgentAdjustment
                {
                    Id = Guid.NewGuid(),
                    Checked = 0,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = "",
                    PersonId = DBHelper.CurrentUser.KaId,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    OperationType = _db.OperationTypes.FirstOrDefault().Id,
                    WriteOffType = _db.WriteOffTypes.FirstOrDefault().Id,
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    Deleted = 0
                });

                _db.SaveChanges();
            }
            else
            {
                pp = _db.KAgentAdjustment.FirstOrDefault(f => f.Id == _doc_id);
            }

            if (pp != null)
            {
                _doc_id = pp.Id;

                pp.SessionId = (Guid?)UserSession.SessionId;
                pp.UpdatedBy = UserSession.UserId;
                pp.UpdatedAt = DateTime.Now;
                _db.SaveChanges();

                if (is_new_record)
                {
                    pp.Num = new BaseEntities().GetDocNum("writing_off_debt").FirstOrDefault();
                }
                KAgentAdjustmentBS.DataSource = pp;

            }

            RefreshDet();
        }

        private void GetOk()
        {
            OkButton.Enabled = WriteOffTypesEdit.EditValue != DBNull.Value && OperationTypeEdit.EditValue != DBNull.Value && KAgentAdjustmentDetBS.Count > 0;
            //     barSubItem1.Enabled = WriteOffTypesEdit.EditValue != DBNull.Value && OperationTypeEdit.EditValue != DBNull.Value;
            EditMaterialBtn.Enabled = KAgentAdjustmentDetBS.Count > 0;
            DelMaterialBtn.Enabled = KAgentAdjustmentDetBS.Count > 0;

            OperationTypeEdit.Enabled = KAgentAdjustmentDetBS.Count == 0;
            WriteOffTypesEdit.Enabled = KAgentAdjustmentDetBS.Count == 0;
            KagentComboBox.Enabled = KAgentAdjustmentDetBS.Count == 0;
        }

        private void RefreshDet()
        {
            var list = _db.v_KAgentAdjustmentDet.AsNoTracking().Where(w => w.KAgentAdjustmentId == _doc_id).OrderBy(o => o.OnDate).ToList();

            int top_row = KAgentAdjustmentDetGridView.TopRowIndex;
            KAgentAdjustmentDetBS.DataSource = list;
            KAgentAdjustmentDetGridView.TopRowIndex = top_row;

            GetOk();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //  new frmProductionPlanDet(_db, Guid.NewGuid(), pp).ShowDialog();

            RefreshDet();
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = KAgentAdjustmentDetGridView.GetFocusedRow() as v_KAgentAdjustmentDet;
            //    new frmProductionPlanDet(_db, row.Id, pp).ShowDialog();

            RefreshDet();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            pp.UpdatedAt = DateTime.Now;

            var ProductionPlan = _db.ProductionPlans.Find(_doc_id);
            if (ProductionPlan != null && ProductionPlan.SessionId != UserSession.SessionId)
            {
                throw new Exception("Не можливо зберегти документ, тільки перегляд.");
            }

            if (TurnDocCheckBox.Checked)
            {
                pp.SummAll = pp.SummInCurr;
            }

            _db.SaveChanges();

            is_new_record = false;

            Close();
        }


        private void frmProductionPlans_FormClosed(object sender, FormClosedEventArgs e)
        {
            _db.UndoAllChanges();

            pp.SessionId = (pp.SessionId == UserSession.SessionId ? null : pp.SessionId);
            pp.UpdatedBy = UserSession.UserId;
            pp.UpdatedAt = DateTime.Now;
            _db.SaveChanges();

            if (is_new_record)
            {
                _db.DeleteWhere<KAgentAdjustment>(w => w.Id == _doc_id);
            }

            _db.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.KAgentAdjustmentDet.RemoveRange(_db.KAgentAdjustmentDet.Where(w=> w.KAgentAdjustmentId == pp.Id).ToList()); //DeleteWhere<KAgentAdjustmentDet>(w => w.KAgentAdjustmentId == pp.Id);
            _db.SaveChanges();

            RefreshDet();
        }


        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (OperationTypeEdit.EditValue != null && OperationTypeEdit.EditValue != DBNull.Value && WriteOffTypesEdit.EditValue != null && WriteOffTypesEdit.EditValue != DBNull.Value)
            {
                _db.DeleteWhere<KAgentAdjustmentDet>(w => w.KAgentAdjustmentId == pp.Id);

                var doc_list = _db.GetDocList(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(100), pp.DebtKaId, 0).OrderBy(o => o.OnDate).ToList();
                var result = new List<GetDocList_Result>();
                foreach (var item in doc_list)
                {
                    result.Add(item);

                    if (item.Saldo == 0)
                    {
                        result.Clear();
                    }
                }

                decimal saldo = 0;
                int idx = 0;
                var saldo_types = new List<int>() { 1, -1, -6, 6, -3, 3, -23, 23, 100, -100 };

                foreach (var item in result.Where(w => saldo_types.Contains(w.WType.Value) && w.SummInCurr != 0))
                {
                    saldo += (item.SummInCurr ?? 0) * item.WType.Value / Math.Abs(item.WType.Value);

                    _db.KAgentAdjustmentDet.Add(new KAgentAdjustmentDet
                    {
                        Id = Guid.NewGuid(),
                        Idx = ++idx,
                        DocId = item.Id,
                        KAgentAdjustmentId = pp.Id,
                        Saldo = saldo
                    });
                 

                    pp.SummAll = Math.Abs(saldo);
                    pp.SummInCurr = Math.Abs(saldo) ;
                }

                _db.SaveChanges();

                RefreshDet();
            }
        }

        private void WaybillDetInGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
        }

        private void WHComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                OperationTypeEdit.EditValue = IHelper.ShowDirectList(OperationTypeEdit.EditValue, 2);
            }
        }

        private void ManufactoryEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                WriteOffTypesEdit.EditValue = IHelper.ShowDirectList(WriteOffTypesEdit.EditValue, 2);
            }
        }


        private void frmProductionPlans_Shown(object sender, EventArgs e)
        {
            KAgentAdjustmentDetGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
        }

        private void WriteOffTypesEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (WriteOffTypesEdit.EditValue != null && WriteOffTypesEdit.EditValue != DBNull.Value)
            {
                int type = Convert.ToInt32(WriteOffTypesEdit.EditValue);
                if (type == 1)
                {
                    KagentComboBox.Properties.DataSource = _db.KagentList.Where(w => w.Saldo < 0).Select(s => new { s.KaId, s.Name, s.Saldo }).ToList();
                    pp.WType = 23;

                    labelControl6.Text = "Дебіторська заборгованість";
                }

                if (type == 2)
                {
                    KagentComboBox.Properties.DataSource = _db.KagentList.Where(w => w.Saldo > 0).Select(s => new { s.KaId, s.Name, s.Saldo }).ToList();
                    pp.WType = -23;

                    labelControl6.Text = "Кредиторська заборгованість";
                }
            }
        }

        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                pp.OnDate = DBHelper.ServerDateTime();
                OnDateDBEdit.DateTime = pp.OnDate;
            }
        }

        private void KAgentAdjustmentDetGridView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                GridSummaryItem item = e.Item as GridSummaryItem;

                if (item.FieldName == "Saldo")
                {
                    e.TotalValue = e.FieldValue;
                }
            }
        }
    }
}
