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
using SP_Sklad.ViewsForm;
using SP_Sklad.WBDetForm;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBForm
{
    public partial class frmProjectManagement : DevExpress.XtraEditors.XtraForm
    {
        public BaseEntities _db { get; set; }
        public Guid? _doc_id { get; set; }
        private ProjectManagement pm { get; set; }
        public bool is_new_record { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private v_ProjectManagementDet pm_det_row
        {
            get
            {
                return ProjectManagementDetGridView.GetFocusedRow() as v_ProjectManagementDet;
            }
        }

        public frmProjectManagement(Guid? doc_id = null)
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
            KagentComboBox.Properties.DataSource = DBHelper.KagentsWorkerList;

            if (_doc_id == null)
            {
                is_new_record = true;

                pm = _db.ProjectManagement.Add(new ProjectManagement
                {
                    Id = Guid.NewGuid(),
                    Checked = 0,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = "",
                    PersonId = DBHelper.CurrentUser.KaId,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    Deleted = 0,
                    EntId = DBHelper.Enterprise.KaId
                });

                _db.SaveChanges();
            }
            else
            {
                pm = _db.ProjectManagement.FirstOrDefault(f => f.Id == _doc_id);
            }

            if (pm != null)
            {
                _doc_id = pm.Id;

                pm.SessionId = (Guid?)UserSession.SessionId;
                pm.UpdatedBy = UserSession.UserId;
                pm.UpdatedAt = DateTime.Now;
                _db.SaveChanges();

                if (is_new_record)
                {
                    pm.Num = new BaseEntities().GetDocNum("project_management").FirstOrDefault();
                }
                ProjectManagementBS.DataSource = pm;

            }

            RefreshDet();
        }

        private void GetOk()
        {
            OkButton.Enabled =  ProjectManagementDetBS.Count > 0;
            
            EditMaterialBtn.Enabled = ProjectManagementDetBS.Count > 0;
            DelMaterialBtn.Enabled = ProjectManagementDetBS.Count > 0;

            KagentComboBox.Enabled = ProjectManagementDetBS.Count == 0;
        }

        private void RefreshDet()
        {
            var list = _db.v_ProjectManagementDet.AsNoTracking().Where(w => w.ProjectManagementId == _doc_id).OrderBy(o => o.OnDate).ToList();

            int top_row = ProjectManagementDetGridView.TopRowIndex;
            ProjectManagementDetBS.DataSource = list;
            ProjectManagementDetGridView.TopRowIndex = top_row;

            GetOk();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //  new frmProductionPlanDet(_db, Guid.NewGuid(), pp).ShowDialog();

            RefreshDet();
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = ProjectManagementDetGridView.GetFocusedRow() as v_KAgentAdjustmentDet;

            RefreshDet();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            pm.UpdatedAt = DateTime.Now;

            var ProductionPlan = _db.ProductionPlans.Find(_doc_id);
            if (ProductionPlan != null && ProductionPlan.SessionId != UserSession.SessionId)
            {
                throw new Exception("Не можливо зберегти документ, тільки перегляд.");
            }

            if (TurnDocCheckBox.Checked)
            {
                pm.SummAll = pm.SummInCurr;
            }

            _db.SaveChanges();

            is_new_record = false;

            Close();
        }


        private void frmProductionPlans_FormClosed(object sender, FormClosedEventArgs e)
        {
            _db.UndoAllChanges();

            pm.SessionId = (pm.SessionId == UserSession.SessionId ? null : pm.SessionId);
            pm.UpdatedBy = UserSession.UserId;
            pm.UpdatedAt = DateTime.Now;
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
            _db.KAgentAdjustmentDet.RemoveRange(_db.KAgentAdjustmentDet.Where(w=> w.KAgentAdjustmentId == pm.Id).ToList()); //DeleteWhere<KAgentAdjustmentDet>(w => w.KAgentAdjustmentId == pp.Id);
            _db.SaveChanges();

            RefreshDet();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmDocumentViews();

            frm.ShowDialog();
        }

        private void WaybillDetInGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
        }

        private void frmProductionPlans_Shown(object sender, EventArgs e)
        {
            ProjectManagementDetGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
        }

      
        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                pm.OnDate = DBHelper.ServerDateTime();
                OnDateDBEdit.DateTime = pm.OnDate;
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
