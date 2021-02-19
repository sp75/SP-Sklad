﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBForm
{
    public partial class frmIntermediateWeighing : DevExpress.XtraEditors.XtraForm
    {

        public BaseEntities _db { get; set; }
        
        public Guid? _doc_id { get; set; }
        private IntermediateWeighing iw { get; set; }

        public bool is_new_record { get; set; }
        private UserSettingsRepository user_settings { get; set; }
        private int _wb_id { get; set; }

        private v_IntermediateWeighingDet iw_det_row
        {
            get
            {
                return WaybillDetInGridView.GetFocusedRow() as v_IntermediateWeighingDet;
            }
        }

        public frmIntermediateWeighing(int wbill_id , Guid? doc_id)
        {
            is_new_record = false;
            _doc_id = doc_id;
            _wb_id = wbill_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();
        }

        private void frmIntermediateWeighing_Load(object sender, EventArgs e)
        {
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            var wh_list = DBHelper.WhList;

            if (_doc_id == null)
            {
                is_new_record = true;

                iw = _db.IntermediateWeighing.Add(new IntermediateWeighing
                {
                    Id = Guid.NewGuid(),
                    WbillId = _wb_id,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = "",
                    PersonId = DBHelper.CurrentUser.KaId.Value,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    Checked = 0
                });

                _db.SaveChanges();
            }
            else
            {
                iw = _db.IntermediateWeighing.FirstOrDefault(f => f.Id == _doc_id );
            }

            if (iw != null)
            {
                _doc_id = iw.Id;

                iw.SessionId =  (Guid?)UserSession.SessionId;
                iw.UpdatedBy = UserSession.UserId;
                iw.UpdatedAt = DateTime.Now;
                _db.SaveChanges();

                if (is_new_record)
                {
                    iw.Num = new BaseEntities().GetDocNum("intermediate_weighing").FirstOrDefault();
                }
                IntermediateWeighingBS.DataSource = iw;
            }

            RefreshDet();
        }

        private void GetOk()
        {

            barButtonItem2.Enabled = AmountEdit.Value > 0;

            AmountEdit.Enabled = IntermediateWeighingDetBS.Count == 0;

            /*     OkButton.Enabled = ManufactoryEdit.EditValue != DBNull.Value && WHComboBox.EditValue != DBNull.Value && ProductionPlanDetBS.Count > 0;
                 barSubItem1.Enabled = ManufactoryEdit.EditValue != DBNull.Value && WHComboBox.EditValue != DBNull.Value;
                 EditMaterialBtn.Enabled = ProductionPlanDetBS.Count > 0;
                 DelMaterialBtn.Enabled = ProductionPlanDetBS.Count > 0;*/
        }

        private void RefreshDet()
        {
            var list = _db.v_IntermediateWeighingDet.AsNoTracking().Where(w => w.IntermediateWeighingId == _doc_id).OrderBy(o=> o.CreatedDate).ToList();

            int top_row = WaybillDetInGridView.TopRowIndex;
            IntermediateWeighingDetBS.DataSource = list;
            WaybillDetInGridView.TopRowIndex = top_row;

            GetOk();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (iw_det_row != null && _db.GetWayBillMakeDet(iw_det_row.WbillId).Any(a => a.MatId == iw_det_row.MatId && a.Rsv == 0))
            {
                using (var f = new frmIntermediateWeighingDet(_db, iw_det_row.Id, iw))
                {
                    f.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Редагувати заборонено, сировина вже зарезервована");
            }

            RefreshDet();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            iw.UpdatedAt = DateTime.Now;

            var IntermediateWeighing = _db.IntermediateWeighing.Find(_doc_id);
            if (IntermediateWeighing != null && IntermediateWeighing.SessionId != UserSession.SessionId)
            {
                throw new Exception("Не можливо зберегти документ, тільки перегляд.");
            }

            _db.SaveChanges();

            _db.ExecuteIntermediateWeighing(iw.WbillId);

            is_new_record = false;

            Close();
        }


        private void frmIntermediateWeighing_FormClosed(object sender, FormClosedEventArgs e)
        {
            _db.UndoAllChanges();

            iw.SessionId = (iw.SessionId == UserSession.SessionId ? null : iw.SessionId);
            iw.UpdatedBy = UserSession.UserId;
            iw.UpdatedAt = DateTime.Now;
            _db.SaveChanges();

            if (is_new_record)
            {
                _db.DeleteWhere<IntermediateWeighing>(w => w.Id == _doc_id);
            }

            _db.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (iw_det_row != null &&   _db.GetWayBillMakeDet(iw_det_row.WbillId).Any(a => a.MatId == iw_det_row.MatId && a.Rsv == 0))
            {
                var det = _db.IntermediateWeighingDet.Find(iw_det_row.Id);
                if (det != null)
                {
                    _db.IntermediateWeighingDet.Remove(det);
                }
                _db.SaveChanges();

                WaybillDetInGridView.DeleteSelectedRows();
            }
            else
            {
                MessageBox.Show("Видаляти заборонено, сировина вже зарезервована");
            }

            GetOk();
        }

        private void NowDateBtn_Click(object sender, EventArgs e)
        {
            iw.OnDate = DBHelper.ServerDateTime();
            OnDateDBEdit.DateTime = iw.OnDate;
        }

        private void WaybillDetInGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var row = WaybillDetInGridView.GetFocusedRow() as v_IntermediateWeighingDet;
            var wbd = _db.IntermediateWeighingDet.Find(row.Id);
            if (e.Column.FieldName == "Amount")
            {

            }

            if (e.Column.FieldName == "Total")
            {
               
            }

            _db.SaveChanges();
            RefreshDet();
        }

        private void WaybillDetInGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void OnDateDBEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                iw.OnDate = DBHelper.ServerDateTime();
                OnDateDBEdit.DateTime = iw.OnDate;
            }
        }

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            var total = _db.IntermediateWeighing.Where(w => w.WbillId == iw.WbillId).Sum(s => s.Amount);
            var amount_by_recipe = _db.WayBillMake.FirstOrDefault(w => w.WbillId == iw.WbillId).AmountByRecipe;

            if ((total ?? 0) > (amount_by_recipe ?? 0))
            {
                MessageBox.Show("Вага закладок по виробництву більша запланованої");

                return;
            }


            using (var f = new frmIntermediateWeighingDet(_db, Guid.NewGuid(), iw))
            {
                f.ShowDialog();
            }

            RefreshDet();
        }

        private void WaybillDetInGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            EditMaterialBtn.Enabled = iw_det_row != null ;

            DelMaterialBtn.Enabled = iw_det_row != null;
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }
    }
}