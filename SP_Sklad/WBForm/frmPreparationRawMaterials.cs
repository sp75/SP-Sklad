﻿using System;
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

namespace SP_Sklad.WBForm
{
    public partial class frmPreparationRawMaterials : DevExpress.XtraEditors.XtraForm
    {
        private const int _wtype = -24;

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

        public frmPreparationRawMaterials(int? wbill_id = null)
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
            repositoryItemWhEdit.DataSource = DBHelper.WhList;
            RecipeComboBox.Properties.DataSource = DB.SkladBase().MatRecipe.AsNoTracking().Where(w => w.RType == 3 && !w.Archived).Select(s => new
            {
                s.RecId,
                s.Name,
                s.Amount,
                s.MatId,
                MsrName = s.Measures.ShortName,
                s.Measures.AutoCalcRecipe
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
                    EntId = DBHelper.CurrentEnterprise.KaId,
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
                wb = _db.WaybillList.FirstOrDefault(f => f.WbillId == _wbill_id);
                MsrLabel.Text = wb.WayBillMake.MatRecipe.Materials.Measures.ShortName;
            }

            if (wb != null && wb.WayBillMake != null)
            {
                DBHelper.UpdateSessionWaybill(wb.WbillId);

                if (is_new_record) //Послі копіювання згенерувати новий номер
                {
                    wb.Num = new BaseEntities().GetDocNum("wb_preparation_rawmat").FirstOrDefault();
                }

                TurnDocCheckBox.EditValue = wb.Checked;

                checkEdit2.Checked = wb.ToDate != null;

                WaybillListBS.DataSource = wb;
                WayBillMakeBS.DataSource = wb.WayBillMake;
            }

            RefreshDet();
            RefreshDeboningDet();
        }

        private void RefreshDet()
        {
            wbd_list = _db.GetWayBillMakeDet(_wbill_id).AsNoTracking().OrderBy(o => o.Num).ToList();

            int top_row = WaybillDetOutGridView.TopRowIndex;
            GetWayBillMakeDetBS.DataSource = wbd_list;
            WaybillDetOutGridView.TopRowIndex = top_row;

            GetOk();
        }

        private void RefreshDeboningDet(bool reset = false)
        {
            if (reset)
            {
                _db.GetRecipePreparationRawMaterialsOut(_wbill_id);
            }

            DeboningDetGridControl.DataSource = _db.DeboningDet.Where(w => w.WBillId == _wbill_id).Select(s => new DeboningDetList
            {
                DebId = s.DebId,
                WBillId = s.WBillId,
                MatId = s.MatId,
                Amount = s.Amount,
                Price = s.Price,
                WId = s.WId,
                MatName = s.Materials.Name,
                Total = s.Amount * s.Price
            }).ToList();
        }

        public class DeboningDetList
        {
            public int DebId { get; set; }
            public int WBillId { get; set; }
            public int MatId { get; set; }
            public decimal Amount { get; set; }
            public decimal Price { get; set; }
            public int WId { get; set; }
            public string MatName { get; set; }
            public decimal Total { get; set; }
            public string WhName { get; set; }
        }

        bool GetOk()
        {
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && RecipeComboBox.EditValue != null && WhComboBox.EditValue != null && OnDateDBEdit.EditValue != null && GetWayBillMakeDetBS.Count > 0);

            if (recult && TurnDocCheckBox.Checked)
            {
                recult = !wbd_list.Any(w => w.Rsv == 0);
            }

            RecipeComboBox.Enabled = GetWayBillMakeDetBS.Count == 0;
            WhComboBox.Enabled = RecipeComboBox.Enabled;
            AmountMakeEdit.Enabled = RecipeComboBox.Enabled;
            barSubItem1.Enabled = (WhComboBox.EditValue != null && RecipeComboBox.EditValue != null && AmountMakeEdit.Value > 0);

            EditMaterialBtn.Enabled = GetWayBillMakeDetBS.Count > 0;
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;

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
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DBHelper.CheckInDate(wb, _db, OnDateDBEdit.DateTime))
            {
                return;
            }
            //  var measure_id = wb.WayBillMake.MatRecipe.Materials.MId;

            dynamic row = RecipeComboBox.GetSelectedDataRow();
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
                RefreshDet();
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
            using (var frm = new frmWriteOffDet(_db, null, wb))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    RefreshDet();
                    RefreshDeboningDet(true);
                }
            }
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetOutGridView.GetRow(WaybillDetOutGridView.FocusedRowHandle) as GetWayBillMakeDet_Result;

            if (dr != null)
            {
                using (var frm = new frmWriteOffDet(_db, dr.PosId, wb))
                {
                    frm.ShowDialog();

                    RefreshDet();
                    RefreshDeboningDet(true);
                }
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetOutGridView.GetRow(WaybillDetOutGridView.FocusedRowHandle) as GetWayBillMakeDet_Result;

            if (dr != null)
            {
                DelRsvBarBtn.PerformClick();

                _db.DeleteWhere<WaybillDet>(w => w.PosId == dr.PosId);
                _db.SaveChanges();

                RefreshDet();
            }
        }

        private void RsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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
            var res = _db.ReservedAllPosition(wb.WbillId, DBHelper.CurrentUser.UserId);

            if (res.Any())
            {
                MessageBox.Show("Не вдалося зарезервувати деякі товари!");
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
            var r = _db.GetRecipePreparationRawMaterials(_wbill_id).ToList();

            if (MessageBox.Show("Зарезервувати товар ? ", "Повідомлення.", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                RsvAllBarBtn.PerformClick();
            }

            RefreshDeboningDet(true);

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
            dynamic row = RecipeComboBox.GetSelectedDataRow();

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

        private void DeboningDetGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var dr = DeboningDetGridView.GetRow(e.RowHandle) as DeboningDetList;
            var dd = _db.DeboningDet.Find(dr.DebId);
            if (e.Column.FieldName == "Amount")
            {
                dd.Amount = Convert.ToDecimal(e.Value);
            }
            else if (e.Column.FieldName == "Price")
            {
                dd.Price = Convert.ToDecimal(e.Value);
            }
            else if (e.Column.FieldName == "WId")
            {
                dd.WId = Convert.ToInt32(e.Value);
            }
            dr.Total = dd.Amount * dd.Price;

            _db.SaveChanges();

            DeboningDetGridView.RefreshRow(e.RowHandle);
        }

        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
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
                RecipeComboBox.EditValue = IHelper.ShowDirectList(RecipeComboBox.EditValue, 16);
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
                PersonMakeComboBox.EditValue = IHelper.ShowDirectList(PersonMakeComboBox.EditValue, 3);
            }
        }

        private void KagentComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                KagentComboBox.EditValue = IHelper.ShowDirectList(KagentComboBox.EditValue, 3);
            }
        }

        private void PersonComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                PersonComboBox.EditValue = IHelper.ShowDirectList(PersonComboBox.EditValue, 3);
            }
        }
    }
}
