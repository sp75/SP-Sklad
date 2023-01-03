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
            lookUpEdit1.Properties.DataSource = new List<object>() { new { Id = 1, Name = "Зважування сировини" }, new { Id = -1, Name = "Переміщення на обвалку" } };
            lookUpEdit1.EditValue = 0;

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

      //      barButtonItem2.Enabled = AmountEdit.Value > 0;

       //     AmountEdit.Enabled = IntermediateWeighingDetBS.Count == 0;
     //       ManufLookUpEdit.Enabled = IntermediateWeighingDetBS.Count == 0;

            /*     OkButton.Enabled = ManufactoryEdit.EditValue != DBNull.Value && WHComboBox.EditValue != DBNull.Value && ProductionPlanDetBS.Count > 0;
                 barSubItem1.Enabled = ManufactoryEdit.EditValue != DBNull.Value && WHComboBox.EditValue != DBNull.Value;
                 EditMaterialBtn.Enabled = ProductionPlanDetBS.Count > 0;
                 DelMaterialBtn.Enabled = ProductionPlanDetBS.Count > 0;*/
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
          /*  if (iw_det_row != null && _db.GetWayBillMakeDet(iw_det_row.WbillId).Any(a => a.MatId == iw_det_row.MatId && a.Rsv == 0))
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

            RefreshDet();*/
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            rmm.UpdatedAt = DateTime.Now;

            _db.SaveChanges();

            is_new_record = false;
        }

        private Guid ExecuteRawMaterialManagement(Guid id)
        {
            var _rmm = _db.RawMaterialManagement.Find(id);

            var wb = _db.WaybillList.Add(new WaybillList()
            {
                Id = Guid.NewGuid(),
                WType = 1,
                OnDate = DBHelper.ServerDateTime(),
                Num = new BaseEntities().GetDocNum("wb_in").FirstOrDefault(),
                CurrId = 2,
                OnValue = 1,
                PersonId = DBHelper.CurrentUser.KaId,
                Nds = DBHelper.Enterprise.NdsPayer == 1 ? DBHelper.CommonParam.Nds : 0,
                UpdatedBy = DBHelper.CurrentUser.UserId,
                EntId = DBHelper.Enterprise.KaId,
                PTypeId = 1,
                Reason = $"Зважування напівтуш №{_rmm.Num}"
            });

            _db.SetDocRel(id, wb.Id);
            _db.SaveChanges();

            var list_det = _db.RawMaterialManagementDet.Where(w => w.RawMaterialManagementId == id && w.PosId == null)
                .GroupBy(g => new { g.MatId }).Select(s => new
                {
                    s.Key.MatId,
                    Amount = s.Sum(ss => ss.Amount)
                }).ToList();

            var num = 0;
            foreach (var item in list_det)
            {
                var wbd = _db.WaybillDet.Add(new WaybillDet
                {
                    WbillId = wb.WbillId,
                    MatId = item.MatId,
                    WId = _rmm.WId,
                    Amount = item.Amount.Value,
                    Price = 0,
                    Discount = 0,
                    Nds = wb.Nds,
                    CurrId = 2,
                    OnDate = wb.OnDate,
                    Num = ++num,
                    Checked = 0,
                    OnValue = 1,
                    Total = 0,
                    BasePrice = 0,
                });
                _db.SaveChanges();

                foreach (var rmm_det in _db.RawMaterialManagementDet.Where(w => w.RawMaterialManagementId == id && w.MatId == item.MatId))
                {
                    rmm_det.PosId = wbd.PosId;
                }
            }

            _rmm.Checked = 1;

            _db.SaveChanges();

            return wb.Id;
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
          /*  if (iw_det_row != null &&   _db.GetWayBillMakeDet(iw_det_row.WbillId).Any(a => a.MatId == iw_det_row.MatId && a.Rsv == 0))
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

            GetOk();*/
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
            RefreshDet();*/
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
                    if (rmm.DocType == 1)
                    {

                        var mat = _db.MatBarCode.FirstOrDefault(w => w.BarCode == frm.BarCodeEdit.Text);

                        if (mat != null)
                        {
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
                                        RawMaterialManagementId = rmm.Id
                                    });

                                    _db.SaveChanges();
                                }
                            }
                        }
                    }

                    if (rmm.DocType == -1)
                    {
                        var list = GetMatMove(frm.BarCodeEdit.Text);
                        if(list.Any())
                        {
                            using (var frm2 = new frmWeightEdit(list.FirstOrDefault().MatName))
                            {
                                if (frm2.ShowDialog() == DialogResult.OK)
                                {
                                    var pos = list.FirstOrDefault(w => w.ActualRemain >= frm2.AmountEdit.Value);
                                    if (pos != null)
                                    {
                                        _db.RawMaterialManagementDet.Add(new RawMaterialManagementDet
                                        {
                                            Id = Guid.NewGuid(),
                                            BarCode = frm.BarCodeEdit.Text,
                                            Amount = frm2.AmountEdit.Value,
                                            MatId = pos.MatId,
                                            OnDate = DBHelper.ServerDateTime(),
                                            RawMaterialManagementId = rmm.Id
                                        });

                                        _db.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            RefreshDet();
        }

        private List<mat_move> GetMatMove(string bar_code)
        {
            var mat = from rmmd in _db.RawMaterialManagementDet
                      join pr in _db.v_PosRemains on rmmd.PosId equals pr.PosId
                      join m in _db.Materials on rmmd.MatId equals m.MatId

                      where pr.ActualRemain > 0 && rmmd.BarCode == bar_code
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
    }
}
