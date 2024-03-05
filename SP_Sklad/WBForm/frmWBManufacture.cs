using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;
using EntityState = System.Data.Entity.EntityState;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Data.Entity.Core.Objects;
using DevExpress.XtraGrid;
using SP_Sklad.Common;
using SP_Sklad.Reports;
using SP_Sklad.Properties;
using SP_Sklad.ViewsForm;
using DevExpress.XtraEditors;

namespace SP_Sklad.WBForm
{
    public partial class frmWBManufacture : DevExpress.XtraEditors.XtraForm
    {
        private const int _wtype = -20;

        BaseEntities _db { get; set; }
        public int? _wbill_id { get; set; }
        private WaybillList wb { get; set; }
        public bool is_new_record { get; set; }
        private GetWayBillMakeDet_Result wbd_row
        {
            get { return WaybillDetOutGridView.GetFocusedRow() as GetWayBillMakeDet_Result; }
        }
        private List<GetWayBillMakeDet_Result> wbd_list { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        public frmWBManufacture(int? wbill_id = null)
        {
            is_new_record = false;
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();
        }

        private void frmWBManufacture_Load(object sender, EventArgs e)
        {
            KagentComboBox.Properties.DataSource = DBHelper.Persons;
            PersonMakeComboBox.Properties.DataSource = DBHelper.Persons;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            WhComboBox.Properties.DataSource = DBHelper.WhList;
            RecipeComboBox.Properties.DataSource = DB.SkladBase().MatRecipe.AsNoTracking().Where(w => w.RType == 1 && !w.Archived && w.Materials.Archived != 1).Select(s => new RecipeList
            {
                RecId = s.RecId,
                Name = s.Name,
                Amount = s.Amount,
                MatName = s.Materials.Name,
                MatId = s.MatId,
                MsrName = s.Materials.Measures.ShortName,
                AutoCalcRecipe = s.Materials.Measures.AutoCalcRecipe,
                IndustrialProcessing = s.IndustrialProcessing
            }).ToList();

            if (_wbill_id == null)
            {
                is_new_record = true;

                wb = _db.WaybillList.Add(new WaybillList()
                {
                    Id = Guid.NewGuid(),
                    WType = _wtype,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = "",
                    EntId = DBHelper.Enterprise.KaId,
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    KaId = DBHelper.CurrentUser.KaId,
                    WayBillMake = new WayBillMake { SourceWId = DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId },
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                });
                _db.SaveChanges();

                _wbill_id = wb.WbillId;

                MsrLabel.Text = "";
            }
            else
            {
                wb = _db.WaybillList.FirstOrDefault(f =>  f.WbillId == _wbill_id);
                MsrLabel.Text = wb.WayBillMake.MatRecipe.Materials.Measures.ShortName;
            }

            if (wb != null && wb.WayBillMake != null)
            {
                DBHelper.UpdateSessionWaybill(wb.WbillId);

                if (is_new_record) //Послі копіювання згенерувати новий номер
                {
                    wb.Num = new BaseEntities().GetDocNum("wb_make").FirstOrDefault();
                }

                TurnDocCheckBox.EditValue = wb.Checked;

                checkEdit2.Checked = wb.ToDate != null;

                WaybillListBS.DataSource = wb;
                WayBillMakeBS.DataSource = wb.WayBillMake;

                if (!is_new_record && !DB.SkladBase().MatRecipe.AsNoTracking().Where(w => w.RType == 1 && !w.Archived && w.Materials.Archived != 1 && w.RecId == wb.WayBillMake.RecId).Any())
                {
                    MessageBox.Show("Рецепт або товар перенесоно до архіву!");
                }
            }

            RefreshDet();
        }
        public class RecipeList
        {
            public int RecId { get; set; }
            public string Name { get; set; }
            public decimal Amount { get; set; }
            public string MatName { get; set; }
            public int MatId { get; set; }
            public string MsrName { get; set; }
            public bool AutoCalcRecipe { get; set; }
            public bool? IndustrialProcessing { get; set; }
        }

        private void RefreshDet()
        {
            wbd_list = _db.GetWayBillMakeDet(_wbill_id).AsNoTracking().OrderBy(o => o.Num).ToList();

            int top_row = WaybillDetOutGridView.TopRowIndex;
            GetWayBillMakeDetBS.DataSource = wbd_list;
            WaybillDetOutGridView.TopRowIndex = top_row;

            TechProcGridControl.DataSource = _db.v_TechProcDet.AsNoTracking().Where(w => w.WbillId == _wbill_id).OrderBy(o => o.Num).ToList();

            MatRecipeAdditionalCostsGridControl.DataSource = _db.AdditionalCostsDet.Where(w => w.WbillId == _wbill_id).ToList();

            GetOk();
        }


        bool GetOk()
        {
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && RecipeComboBox.EditValue != null && WhComboBox.EditValue != null && OnDateDBEdit.EditValue != null && GetWayBillMakeDetBS.Count > 0);

            if (recult && TurnDocCheckBox.Checked)
            {
                recult = !wbd_list.Any(w => w.Rsv == 0);
            }

            RecipeComboBox.Enabled = GetWayBillMakeDetBS.Count == 0;
            ByRecipeBtn.Enabled = RecipeComboBox.Enabled;
            WhComboBox.Enabled = RecipeComboBox.Enabled;

            AmountMakeEdit.Enabled = RecipeComboBox.Enabled;

            barSubItem1.Enabled = (WhComboBox.EditValue != null && RecipeComboBox.EditValue != null && AmountMakeEdit.Value > 0);

            var row_recipe = RecipeComboBox.GetSelectedDataRow() as RecipeList;
            if (row_recipe != null)
            {
                DefectsClassifierColumn.Visible = Convert.ToBoolean(row_recipe.IndustrialProcessing);
            }

            OkButton.Enabled = recult;
            return recult;
        }

        private void frmWBManufacture_FormClosed(object sender, FormClosedEventArgs e)
        {
            DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

            if (is_new_record)
            {
                _db.DeleteWhere<WaybillList>(w => w.WbillId == _wbill_id);
            }

            _db.Dispose();
        }

        private void frmWBManufacture_Shown(object sender, EventArgs e)
        {
            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            WaybillDetOutGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

            NumEdit.Enabled = user_settings.AccessEditDocNum;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DBHelper.CheckInDate(wb, _db, OnDateDBEdit.DateTime))
            {
                return;
            }
            //  var measure_id = wb.WayBillMake.MatRecipe.Materials.MId;

            var row = RecipeComboBox.GetSelectedDataRow() as RecipeList;
            if (row.AutoCalcRecipe)
            {

                var main_sum = _db.WaybillDet.Where(w => w.WbillId == _wbill_id && w.Materials.MId == w.WaybillList.WayBillMake.MatRecipe.Materials.MId).ToList()
                    .Sum(s => s.Amount);

                var ext_sum = _db.WaybillDet.Where(w => w.WbillId == _wbill_id && w.Materials.MId != w.WaybillList.WayBillMake.MatRecipe.Materials.MId)
                      .Select(s => new
                      {
                          MaterialMeasures = s.Materials.MaterialMeasures.Where(f => f.MId == s.WaybillList.WayBillMake.MatRecipe.Materials.MId),
                          s.Amount
                      }).ToList()
                      .SelectMany(sm => sm.MaterialMeasures, (k, n) => new
                      {
                          k.Amount,
                          MeasureAmount = n.Amount
                      }).Sum(su => su.MeasureAmount * su.Amount);
                wb.WayBillMake.Amount = main_sum + ext_sum;

                var main_sum_rec = _db.WaybillDet.Where(w => w.WbillId == _wbill_id && w.Materials.MId == w.WaybillList.WayBillMake.MatRecipe.Materials.MId).ToList()
                    .Sum(s => s.Discount);

                var ext_sum_rec = _db.WaybillDet.Where(w => w.WbillId == _wbill_id && w.Materials.MId != w.WaybillList.WayBillMake.MatRecipe.Materials.MId)
                      .Select(s => new
                      {
                          MaterialMeasures = s.Materials.MaterialMeasures.Where(f => f.MId == s.WaybillList.WayBillMake.MatRecipe.Materials.MId),
                          s.Discount
                      }).ToList()
                      .SelectMany(sm => sm.MaterialMeasures, (k, n) => new
                      {
                          k.Discount,
                          MeasureAmount = n.Amount
                      }).Sum(su => su.MeasureAmount * su.Discount);

                var prod_plan = _db.GetRelDocList(wb.Id).FirstOrDefault(a => a.DocType == 20);
                if (prod_plan != null)
                {
                    var prod_plan_det = _db.ProductionPlanDet.FirstOrDefault(w => w.ProductionPlanId == prod_plan.Id && w.RecId == wb.WayBillMake.RecId);
                    if (prod_plan_det != null)
                    {
                        wb.WayBillMake.AmountByRecipe = prod_plan_det.Total;
                        wb.WayBillMake.RecipeCount = Convert.ToInt32(Math.Ceiling(wb.WayBillMake.AmountByRecipe.Value / row.Amount));
                    }

                }
                else
                {
                    wb.WayBillMake.AmountByRecipe = main_sum_rec/* + ext_sum_rec*/;
                    wb.WayBillMake.RecipeCount = Convert.ToInt32(Math.Ceiling(wb.WayBillMake.AmountByRecipe.Value / row.Amount));
                }


                if (wb.WayBillMake.Amount == 0)
                {
                    MessageBox.Show("Помилка в рецепті ,закладка = 0 " + MsrLabel.Text + " !");
                    return;
                }
            }

            wb.UpdatedAt = DateTime.Now;
            _db.SaveChanges();

            if (TurnDocCheckBox.Checked)
            {
                wbd_list = new BaseEntities().GetWayBillMakeDet(_wbill_id).AsNoTracking().OrderBy(o => o.Num).ToList();
                if (wbd_list.Any(w => w.Rsv == 0))
                {
                    MessageBox.Show("Не всі позиції зарезервовано");
                    return;
                }

                var ex_wb = _db.ExecuteWayBill(wb.WbillId, null, DBHelper.CurrentUser.KaId).FirstOrDefault();

                if (ex_wb.ErrorMessage != "False")
                {
                    MessageBox.Show(ex_wb.ErrorMessage);
                    return;
                }
            }

            is_new_record = false;
            Close();
        }

        private void WaybillDetOutGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
        }

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rec = RecipeComboBox.GetSelectedDataRow() as RecipeList;

            if(rec == null)
            {
                return;
            }

            using (var frm = new frmWBManufactureDet(_db, null, wb, rec.IndustrialProcessing))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    RefreshDet();
                }
            }
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetOutGridView.GetRow(WaybillDetOutGridView.FocusedRowHandle) as GetWayBillMakeDet_Result;
            var rec = RecipeComboBox.GetSelectedDataRow() as RecipeList;

            if (dr != null && rec !=null)
            {
                using (var frm = new frmWBManufactureDet(_db, dr.PosId, wb, rec.IndustrialProcessing))
                {
                    frm.ShowDialog();
                    RefreshDet();
                }
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetOutGridView.GetRow(WaybillDetOutGridView.FocusedRowHandle) as GetWayBillMakeDet_Result;

            if (dr != null)
            {
                using (var frm = new frmMessageBox($"Виготовлення продукції №{wb.Num}", $"Ви дійсно бажаєте видалити {dr.MatName} з документа?", false))
                {
                    if (frm.ShowDialog() == DialogResult.Yes)
                    {

                        DelRsvBarBtn.PerformClick();

                        _db.DeleteWhere<WaybillDet>(w => w.PosId == dr.PosId);
                        _db.SaveChanges();

                        RefreshDet();
                    }
                }

            }
        }

        private void RsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (!DBHelper.CheckIntermediateWeighing(_wbill_id.Value, _db))
            {
                return;
            }

            var r = new ObjectParameter("RSV", typeof(Int32));

            _db.ReservedPosition(wbd_row.PosId, r, DBHelper.CurrentUser.UserId);

            if (r.Value != null)
            {
                wbd_row.Rsv = (int)r.Value;
                RefreshDet();
            }

            GetOk();
        }

        private void DelRsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row.Rsv == 1 && wbd_row.PosId > 0)
            {
                _db.DeleteWhere<WMatTurn>(w => w.SourceId == wbd_row.PosId);
            //    current_transaction = current_transaction.CommitRetaining(_db);
           //     UpdLockWB();
                wbd_row.Rsv = 0;
                WaybillDetOutGridView.RefreshRow(WaybillDetOutGridView.FocusedRowHandle);
            }

            GetOk();
        }

        private void RsvAllBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!DBHelper.CheckIntermediateWeighing(_wbill_id.Value, _db))
            {
                return;
            }

            /*    var res = _db.ReservedAllPosition(wb.WbillId, DBHelper.CurrentUser.UserId);

                if (res.Any())
                {
                    MessageBox.Show("Не вдалося зарезервувати деякі товари!");
                }

                RefreshDet();*/

            _db.SaveChanges();
            var list = new List<string>();

            var r = new ObjectParameter("RSV", typeof(Int32));

            var wb_list = _db.GetWayBillMakeDet(_wbill_id).ToList().Where(w => w.Rsv != 1 ).ToList();


            int i = 0;
            barEditItem1.EditValue = i;
            repositoryItemProgressBar1.Maximum = wb_list.Count;
            barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            foreach (var item in wb_list)
            {
                _db.ReservedPosition(item.PosId, r, DBHelper.CurrentUser.UserId);

                if (r.Value != null && (int)r.Value == 0)
                {
                    list.Add(item.MatName);
                }

                barEditItem1.EditValue = ++i;
                barEditItem1.Refresh();
            }
            barEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            if (list.Any())
            {
                MessageBox.Show("Не вдалося зарезервувати: " + String.Join(",", list));
            }

            RefreshDet();
        }

        private void DelAllRsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.DeleteAllReservePosition(wb.WbillId);
            RefreshDet();
        }

        private void WaybillDetOutGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.WbDetPopupMenu.ShowPopup(p2);
            }
        }

        private void NowDateBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void NumEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void ByRecipeBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();
            var r = _db.GetRecipe(_wbill_id).ToList();

            if (MessageBox.Show("Зарезервувати товар ? ", "Повідомлення.", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                RsvAllBarBtn.PerformClick();
            }

            RefreshDet();
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkEdit2.ContainsFocus) return;

            if (checkEdit2.Checked) ToDateEdit.EditValue = OnDateDBEdit.DateTime.AddDays(3);
            else ToDateEdit.EditValue = null;

            ToDateEdit.Focus();
        }

        private void RecipeComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var  row = RecipeComboBox.GetSelectedDataRow() as RecipeList;

            if (RecipeComboBox.ContainsFocus && row != null)
            {
                wb.WayBillMake.Amount = row.Amount;
                MsrLabel.Text = row.MsrName;
                
                GetOk();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            PrintDoc.Show(wb.Id, wb.WType, _db);
        }

        private void WhInBtn_Click(object sender, EventArgs e)
        {
          
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
          
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
        
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            IHelper.ShowMatListByWH3(_db, wb, WhComboBox.EditValue.ToString());
            RefreshDet();
        }

        private void ReceptBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void frmWBManufacture_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((is_new_record || _db.IsAnyChanges()) && OkButton.Enabled)
            {
                var m_recult = MessageBox.Show(Resources.save_wb, "Видаткова накладна №" + wb.Num, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (m_recult == DialogResult.Yes)
                {
                    OkButton.PerformClick();
                }

                if (m_recult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

            }
        }

        private void WaybillDetOutGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            EditMaterialBtn.Enabled = GetWayBillMakeDetBS.Count > 0 /*&& wbd_row.IsIntermediateWeighing != 1*/;
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled /*&& wbd_row.IsIntermediateWeighing != 1*/;
            RsvInfoBtn.Enabled = GetWayBillMakeDetBS.Count > 0;
            MatInfoBtn.Enabled = GetWayBillMakeDetBS.Count > 0;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmAdditionalCosts())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var item = _db.AdditionalCostsDet.Add(new AdditionalCostsDet
                    {
                        Amount = 0,
                        AdditionalCosts = _db.AdditionalCosts.Find(frm.focused_row.Id),
                        WaybillList = wb
                    });

                    _db.SaveChanges();

                    MatRecipeAdditionalCostsGridControl.DataSource = _db.AdditionalCostsDet.Where(w => w.WbillId == _wbill_id).ToList();
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = MatRecipeAdditionalCostsView.GetFocusedRow() as AdditionalCostsDet;

            _db.DeleteWhere<AdditionalCostsDet>(w => w.Id == row.Id);

            MatRecipeAdditionalCostsGridControl.DataSource = _db.AdditionalCostsDet.Where(w => w.WbillId == _wbill_id).ToList();
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row != null)
            {
                IHelper.ShowMatInfo(wbd_row.MatId);
            }
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row != null)
            {
                IHelper.ShowMatRSV(wbd_row.MatId, _db);
            }
        }

        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                OnDateDBEdit.EditValue = DBHelper.ServerDateTime();
                wb.OnDate = OnDateDBEdit.DateTime;
                _db.SaveChanges();
            }
        }

        private void RecipeComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                RecipeComboBox.EditValue = IHelper.ShowDirectList(RecipeComboBox.EditValue, 13);
            }
        }

        private void WhComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                WhComboBox.EditValue = IHelper.ShowDirectList(WhComboBox.EditValue, 2);
            }
        }

        private void PersonMakeComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                ((LookUpEdit)sender).EditValue = IHelper.ShowDirectList(((LookUpEdit)sender).EditValue, 3);
            }
        }
    }
}
