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
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBForm
{
    public partial class frmProductionPlans : DevExpress.XtraEditors.XtraForm
    {
        public BaseEntities _db { get; set; }
        public Guid? _doc_id { get; set; }
        private ProductionPlans pp { get; set; }
        public bool is_new_record { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private v_ProductionPlanDet pp_det_row
        {
            get
            {
                return WaybillDetInGridView.GetFocusedRow() as v_ProductionPlanDet;
            }
        }

        public frmProductionPlans(Guid? doc_id)
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
            var wh_list = DBHelper.WhList;
            WHComboBox.Properties.DataSource = wh_list;
            WHComboBox.EditValue = wh_list.Where(w => w.Def == 1).Select(s => s.WId).FirstOrDefault();
            ManufactoryEdit.Properties.DataSource = wh_list;
            ManufactoryEdit.EditValue = wh_list.Where(w => w.Def == 1).Select(s => s.WId).FirstOrDefault();

            if (_doc_id == null)
            {
                is_new_record = true;

                pp = _db.ProductionPlans.Add(new ProductionPlans
                {
                    Id = Guid.NewGuid(),
                    OnDate = DBHelper.ServerDateTime(),
                    Num = "",
                    PersonId = DBHelper.CurrentUser.KaId,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId
                });

                _db.SaveChanges();
            }
            else
            {
                pp = _db.ProductionPlans.FirstOrDefault(f => f.Id == _doc_id );
            }

            if (pp != null)
            {
                _doc_id = pp.Id;

                pp.SessionId =  (Guid?)UserSession.SessionId;
                pp.UpdatedBy = UserSession.UserId;
                pp.UpdatedAt = DateTime.Now;
                _db.SaveChanges();

                if (is_new_record)
                {
                    pp.Num = new BaseEntities().GetDocNum("prod_plan").FirstOrDefault();
                }
                ProductionPlansBS.DataSource = pp;

              //  TurnDocCheckBox.EditValue =


              //  rdl = _db.GetRelDocList(wb.Id).Where(w => w.DocType == 7 || w.DocType == -22).ToList();
            
             //   AddBarSubItem.Enabled = !rdl.Any();
            //    EditMaterialBtn.Enabled = !rdl.Any(a => a.DocType == 7);
            //    DelMaterialBtn.Enabled = AddBarSubItem.Enabled;
            }

            RefreshDet();
        }

        private void GetOk()
        {
            OkButton.Enabled = ManufactoryEdit.EditValue != DBNull.Value && WHComboBox.EditValue != DBNull.Value && ProductionPlanDetBS.Count > 0;
            barSubItem1.Enabled = ManufactoryEdit.EditValue != DBNull.Value && WHComboBox.EditValue != DBNull.Value;
            EditMaterialBtn.Enabled = ProductionPlanDetBS.Count > 0;
            DelMaterialBtn.Enabled = ProductionPlanDetBS.Count > 0;
        }

        private void RefreshDet()
        {
            var list = _db.v_ProductionPlanDet.AsNoTracking().Where(w => w.ProductionPlanId == _doc_id).OrderBy(o => o.Num).ToList();

           int top_row = WaybillDetInGridView.TopRowIndex;
            ProductionPlanDetBS.DataSource = list;
            WaybillDetInGridView.TopRowIndex = top_row;

            GetOk();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmProductionPlanDet(_db, Guid.NewGuid(), pp).ShowDialog();

            RefreshDet();
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = WaybillDetInGridView.GetFocusedRow() as v_ProductionPlanDet;
            new frmProductionPlanDet(_db, row.Id, pp).ShowDialog();

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

            _db.SaveChanges();


            if (TurnDocCheckBox.Checked)
            {
                ExecuteDocument();
            }

            is_new_record = false;

            Close();
        }

        private void ExecuteDocument()
        {
            var list = _db.v_ProductionPlanDet.AsNoTracking().Where(w => w.ProductionPlanId == _doc_id && w.Total > 0).OrderBy(o => o.Num).ToList();
            foreach (var i in list)
            {
                var wb = _db.WaybillList.Add(new WaybillList()
                {
                    Id = Guid.NewGuid(),
                    WType = -20,
                    OnDate = pp.OnDate,
                    Num = new BaseEntities().GetDocNum("wb_make").FirstOrDefault() + "_" + pp.Num,
                    EntId = DBHelper.Enterprise.KaId,
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    KaId = DBHelper.CurrentUser.KaId,
                    WayBillMake = new WayBillMake { SourceWId = DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId },
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                });
                _db.SaveChanges();

                _db.SetDocRel(pp.Id, wb.Id);

                wb.WayBillMake.Amount = i.Total;
                wb.WayBillMake.SourceWId = i.WhId;
                wb.WayBillMake.RecId = i.RecId;

                
                wb.WayBillMake.AmountByRecipe = i.Total;
                wb.WayBillMake.RecipeCount = Convert.ToInt32(Math.Ceiling(wb.WayBillMake.AmountByRecipe.Value / _db.MatRecipe.FirstOrDefault(w => w.RecId == i.RecId).Amount));

                _db.SaveChanges();

                var r = _db.GetRecipe(wb.WbillId).ToList();

                if (ReservedAllСheck.Checked)
                {
                    var rez = _db.ReservedAllPosition(wb.WbillId, DBHelper.CurrentUser.UserId).ToList();
                }
            }
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
                _db.DeleteWhere<ProductionPlans>(w => w.Id == _doc_id);
            }

            _db.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = WaybillDetInGridView.GetFocusedRow() as v_ProductionPlanDet;
            if (row != null)
            {
                var det = _db.ProductionPlanDet.Find(row.Id);
                if (det != null)
                {
                    _db.ProductionPlanDet.Remove(det);
                }
                _db.SaveChanges();
                WaybillDetInGridView.DeleteSelectedRows();
            }
            GetOk();
        }

        private void NowDateBtn_Click(object sender, EventArgs e)
        {
          
        }

        private void WaybillDetInGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var row = WaybillDetInGridView.GetFocusedRow() as v_ProductionPlanDet;
            var wbd = _db.ProductionPlanDet.Find(row.Id);
            if (e.Column.FieldName == "Amount")
            {

                wbd.Amount = Convert.ToDecimal(e.Value);
                var real_amount = wbd.Amount.Value - wbd.Remain.Value;
                var tmp_amount = (real_amount / (row.ResipeOut == 0 ? 100.00m : row.ResipeOut)) * 100; // real_amount + (real_amount - (real_amount * row.ResipeOut / 100));
                wbd.Total = Math.Ceiling(tmp_amount / row.RecipeAmount) * row.RecipeAmount;
            }

            if (e.Column.FieldName == "Total")
            {
                wbd.Total = Convert.ToDecimal(e.Value);
            }

            _db.SaveChanges();
            RefreshDet();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Ви дійсно бажаєте заповнити по залишками зі складу ?", "Планування виробництва", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (WHComboBox.EditValue != null && WHComboBox.EditValue != DBNull.Value && ManufactoryEdit.EditValue != null && ManufactoryEdit.EditValue != DBNull.Value)
                {
                    _db.DeleteWhere<ProductionPlanDet>(w => w.ProductionPlanId == pp.Id);
                    var wh_remain = _db.WhMatGet(0, (int)WHComboBox.EditValue, 0, DateTime.Now, 0, "*", 0, "", DBHelper.CurrentUser.UserId, 0).ToList();
                    var maked = _db.WBListMake(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(100), 2, "*", 0, -20, UserSession.UserId).ToList();
                    int num = 0;
                    foreach (var item in wh_remain)
                    {
                        var rec = _db.MatRecipe.FirstOrDefault(w => w.MatId == item.MatId);
                        if (rec != null)
                        {
                            var sc_amount = _db.SchedulingOrders.Where(w => w.RecId == rec.RecId).Select(s => s.Amount).FirstOrDefault();
                            var in_process = maked.Where(w => w.MatId == item.MatId).Sum(s => s.AmountOut - (s.ShippedAmount ?? 0));
                            var reamin = item.Remain + in_process;

                            var real_amount = sc_amount - reamin;
                            var tmp_amount = (real_amount / (rec.Out == 0 ? 100m : rec.Out)) * 100;// real_amount + (real_amount - (real_amount * rec.Out / 100));

                            _db.ProductionPlanDet.Add(new ProductionPlanDet
                            {
                                Id = Guid.NewGuid(),
                                Num = ++num,
                                Amount = sc_amount,
                                ProductionPlanId = pp.Id,
                                RecId = rec.RecId,
                                Remain = reamin,
                                WhId = (int)ManufactoryEdit.EditValue,
                                Total = Math.Ceiling(Convert.ToDecimal(tmp_amount / rec.Amount)) * rec.Amount
                            });
                        }
                    }
                    _db.SaveChanges();

                    RefreshDet();
                }
            }
        }

        private void WaybillDetInGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          /*  if (wbd_row != null)
            {
                IHelper.ShowMatRSV(wbd_row.MatId, _db);
            }*/
        }

        private void WHComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                WHComboBox.EditValue = IHelper.ShowDirectList(WHComboBox.EditValue, 2);
            }
        }

        private void ManufactoryEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                ManufactoryEdit.EditValue = IHelper.ShowDirectList(ManufactoryEdit.EditValue, 2);
            }
        }

        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void frmProductionPlans_Shown(object sender, EventArgs e)
        {
            WaybillDetInGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                pp.OnDate = DBHelper.ServerDateTime();
                OnDateDBEdit.DateTime = pp.OnDate;
            }
        }
    }
}
