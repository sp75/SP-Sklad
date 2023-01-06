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
using SP_Sklad.Common;
using SP_Sklad.EditForm;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;
using SP_Sklad.WBDetForm;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBForm
{
    public partial class frmRawMaterialManagement : DevExpress.XtraEditors.XtraForm
    {

        public BaseEntities _db { get; set; }
        
        public Guid? _doc_id { get; set; }
        private RawMaterialManagement rmm { get; set; }

        public bool is_new_record { get; set; }
        private UserSettingsRepository user_settings { get; set; }
        private int _wb_id { get; set; }

        private v_RawMaterialManagementDet rmm_det_row
        {
            get
            {
                return WaybillDetInGridView.GetFocusedRow() as v_RawMaterialManagementDet;
            }
        }

        public frmRawMaterialManagement(Guid? doc_id)
        {
            is_new_record = false;
            _doc_id = doc_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();
        }

        private void frmIntermediateWeighing_Load(object sender, EventArgs e)
        {
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            WhComboBox.Properties.DataSource = DBHelper.WhList;
            DocTypeEdit.Properties.DataSource = new List<object>() { new { Id = 1, Name = "Зважування сировини" }, new { Id = -1, Name = "Переміщення на обвалку" } };
            DocTypeEdit.EditValue = 0;

            if (_doc_id == null)
            {
                is_new_record = true;

                rmm = _db.RawMaterialManagement.Add(new RawMaterialManagement
                {
                    Id = Guid.NewGuid(),

                    OnDate = DBHelper.ServerDateTime(),
                    Num = "",
                    PersonId = DBHelper.CurrentUser.KaId.Value,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    Checked = 0,
                    DocType = 1
                });

                _db.SaveChanges();
            }
            else
            {
                rmm = _db.RawMaterialManagement.FirstOrDefault(f => f.Id == _doc_id );
            }

            if (rmm != null)
            {
                _doc_id = rmm.Id;

                rmm.SessionId =  (Guid?)UserSession.SessionId;
                rmm.UpdatedBy = UserSession.UserId;
                rmm.UpdatedAt = DateTime.Now;
                _db.SaveChanges();

                if (is_new_record)
                {
                    rmm.Num = new BaseEntities().GetDocNum("raw_material_management").FirstOrDefault();
                }

                RawMaterialManagementBS.DataSource = rmm;

            }

            RefreshDet();
        }

        private void GetOk()
        {
            WhComboBox.Enabled = RawMaterialManagementDetBS.Count == 0;
            DocTypeEdit.Enabled = RawMaterialManagementDetBS.Count == 0;
            barButtonItem2.Enabled = WhComboBox.EditValue != DBNull.Value;

            OkButton.Enabled = WhComboBox.EditValue != DBNull.Value && RawMaterialManagementDetBS.Count > 0;

            DelMaterialBtn.Enabled = RawMaterialManagementDetBS.Count > 0;
        }

        private void RefreshDet()
        {
            var list = _db.v_RawMaterialManagementDet.AsNoTracking().Where(w => w.RawMaterialManagementId == _doc_id).OrderBy(o=> o.OnDate).ToList();

            int top_row = WaybillDetInGridView.TopRowIndex;
            RawMaterialManagementDetBS.DataSource = list;
            WaybillDetInGridView.TopRowIndex = top_row;

            GetOk();
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            rmm.UpdatedAt = DateTime.Now;

            _db.SaveChanges();

            is_new_record = false;
        }

        private void frmIntermediateWeighing_FormClosed(object sender, FormClosedEventArgs e)
        {
            _db.UndoAllChanges();

            rmm.SessionId = (rmm.SessionId == UserSession.SessionId ? null : rmm.SessionId);
            rmm.UpdatedBy = UserSession.UserId;
            rmm.UpdatedAt = DateTime.Now;
            _db.SaveChanges();

            if (is_new_record)
            {
                _db.DeleteWhere<RawMaterialManagement>(w => w.Id == _doc_id);
            }

            _db.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.DeleteWhere<RawMaterialManagementDet>(w => w.Id == rmm_det_row.Id);

            RefreshDet();
        }

        private void WaybillDetInGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
         /*   var row = WaybillDetInGridView.GetFocusedRow() as v_IntermediateWeighingDet;
            var wbd = _db.IntermediateWeighingDet.Find(row.Id);
            if (e.Column.FieldName == "Amount")
            {

            }

            if (e.Column.FieldName == "Total")
            {
               
            }

            _db.SaveChanges();
            ;*/
        }

        private void WaybillDetInGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
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
                rmm.OnDate = DBHelper.ServerDateTime();
                OnDateDBEdit.DateTime = rmm.OnDate;
            }
        }

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmBarCode())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var mat = _db.MatBarCode.FirstOrDefault(w => w.BarCode == frm.BarCodeEdit.Text);

                    if (mat == null)
                    {
                        MessageBox.Show("Штрих-код не знайдено!");
                        return;
                    }

                    var last_doc_type = GetLastMoveBarCode(frm.BarCodeEdit.Text);

                    if (rmm.DocType == last_doc_type?.DocType)
                    {
                        MessageBox.Show("Штрих-код вже використовується!");
                        return;
                    }

                    if (rmm.DocType == -1 && (last_doc_type?.PosId == null || last_doc_type?.WId != rmm.WId) )
                    {
                        MessageBox.Show("Штрих-код не використовується!");
                        return;
                    }

                    using (var frm2 = new frmWeightEdit(mat.Materials.Name))
                    {
                        if (frm2.ShowDialog() == DialogResult.OK)
                        {
                            _db.RawMaterialManagementDet.Add(new RawMaterialManagementDet
                            {
                                Id = Guid.NewGuid(),
                                BarCode = mat.BarCode,
                                Amount = frm2.AmountEdit.Value,
                                MatId = mat.MatId,
                                OnDate = DBHelper.ServerDateTime(),
                                RawMaterialManagementId = rmm.Id,
                                LastAmount = rmm.DocType == -1 ? last_doc_type.Amount : null,
                                PosId = rmm.DocType == -1 ? last_doc_type.PosId : null,
                            });

                            _db.SaveChanges();
                        }
                    }

                }
            }

            RefreshDet();
        }

        private LastMoveBarCode GetLastMoveBarCode(string bar_code)
        {
            return _db.RawMaterialManagementDet.Where(w => w.BarCode == bar_code).OrderByDescending(o => o.OnDate)
                .Select(s => new LastMoveBarCode
                {
                    DocType = s.RawMaterialManagement.DocType,
                    Amount = s.Amount,
                    PosId = s.PosId,
                    WId = s.RawMaterialManagement.WId
                }).FirstOrDefault();
        }

        private class LastMoveBarCode
        {
            public int DocType { get; set; }
            public decimal? Amount { get; set; }
            public int? PosId { get; set; }
            public int? WId { get; set; }

        }

        private List<mat_move> GetMatMove(string bar_code)
        {
            var mat = from rmmd in _db.RawMaterialManagementDet
                      join pr in _db.v_PosRemains on rmmd.PosId equals pr.PosId
                      join m in _db.Materials on rmmd.MatId equals m.MatId
                      where rmmd.BarCode == bar_code
                      select new mat_move
                      {
                          MatId = rmmd.MatId,
                          ActualRemain = pr.ActualRemain,
                          AmountIn = rmmd.Amount,
                          MatName = m.Name
                      };

            return mat.ToList();
        }

        public class mat_move
        {
            public decimal? AmountIn { get; set; }
            public int MatId { get; set; }
            public decimal? ActualRemain { get; set; }
            public string MatName { get; set; }
        }


        private void WaybillDetInGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            EditMaterialBtn.Enabled = rmm_det_row != null ;

            DelMaterialBtn.Enabled = rmm_det_row != null;
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void WhComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                ;
            }

        }

        private void WhComboBox_EditValueChanged_1(object sender, EventArgs e)
        {
            GetOk();
        }
    }
}
