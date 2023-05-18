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
    public partial class frmWBDeboning : DevExpress.XtraEditors.XtraForm
    {
        private const int _wtype = -22;

        BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        private WaybillList wb { get; set; }
        public bool is_new_record { get; set; }
        private GetWayBillDetOut_Result wbd_row
        {
            get { return WaybillDetOutGridView.GetFocusedRow() as GetWayBillDetOut_Result; }
        }
        private UserSettingsRepository user_settings { get; set; }
        private List<GetWayBillDetOut_Result> wbd_list { get; set; }
        public int? rec_id { get; set; }
        public int? source_wid { get; set; }

        public frmWBDeboning(int? wbill_id = null)
        {
            is_new_record = false;
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);
            InitializeComponent();
        }

        private void frmWBDeboning_Load(object sender, EventArgs e)
        {
            KagentComboBox.Properties.DataSource = DBHelper.Persons;
            PersonMakeComboBox.Properties.DataSource = DBHelper.Persons;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            WhComboBox.Properties.DataSource = DBHelper.WhList;
            repositoryItemWhEdit.DataSource = DBHelper.WhList;
            RecipeComboBox.Properties.DataSource = DB.SkladBase().MatRecipe.Where(w => w.RType == 2 && !w.Archived).Select(s => new
            {
                s.RecId,
                s.Name,
                s.Amount,
                MatName = s.Materials.Name,
                s.MatId
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
                    WayBillMake = new WayBillMake
                    {
                        SourceWId = source_wid ?? DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId,
                        RecId = rec_id
                    },
                    Nds = 0,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                });
                _db.SaveChanges();

                _wbill_id = wb.WbillId;
            }
            else
            {
                wb = _db.WaybillList.FirstOrDefault(f => f.WbillId == _wbill_id);
            }

            if (wb != null && wb.WayBillMake != null)
            {
                DBHelper.UpdateSessionWaybill(wb.WbillId);

                if (is_new_record)
                {
                    wb.Num = new BaseEntities().GetDocNum("wb_deboning").FirstOrDefault();
                }

                TurnDocCheckBox.EditValue = wb.Checked;
                RecipeComboBox.DataBindings.Add(new Binding("EditValue", wb.WayBillMake, "RecId", true, DataSourceUpdateMode.OnPropertyChanged));
                WhComboBox.DataBindings.Add(new Binding("EditValue", wb.WayBillMake, "SourceWId", true, DataSourceUpdateMode.OnPropertyChanged));
                //   AmountMakeEdit.DataBindings.Add(new Binding("EditValue", wb.WayBillMake, "Amount"));

                PersonMakeComboBox.DataBindings.Add(new Binding("EditValue", wb.WayBillMake, "PersonId", true, DataSourceUpdateMode.OnPropertyChanged));
                KagentComboBox.DataBindings.Add(new Binding("EditValue", wb, "KaId", true, DataSourceUpdateMode.OnPropertyChanged));
                PersonComboBox.DataBindings.Add(new Binding("EditValue", wb, "PersonId", true, DataSourceUpdateMode.OnPropertyChanged));

                NumEdit.DataBindings.Add(new Binding("EditValue", wb, "Num"));
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", wb, "OnDate", true, DataSourceUpdateMode.OnPropertyChanged));
                ToDateEdit.DataBindings.Add(new Binding("EditValue", wb, "ToDate", true, DataSourceUpdateMode.OnPropertyChanged));

                NotesEdit.DataBindings.Add(new Binding("EditValue", wb, "Notes"));
                ReasonEdit.DataBindings.Add(new Binding("EditValue", wb, "Reason"));

                checkEdit2.Checked = wb.ToDate != null;
            }

            RefreshDet();
            RefreshDeboningDet();
        }

      /*  private void UpdLockWB()
        {
            if (wb != null)
            {
                _db.Entry<WaybillList>(wb).State = EntityState.Detached;
            }

            wb = _db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK, NOWAIT) where WbillId = {0}", _wbill_id).FirstOrDefault();
            if (wb != null)
            {
                wb.WayBillMake = _db.WayBillMake.Find(_wbill_id);
            }

            _db.Entry<WaybillList>(wb).State = EntityState.Modified;
        }*/

        private void RefreshDet()
        {
            wbd_list = _db.GetWayBillDetOut(_wbill_id).OrderBy(o => o.Num).ToList();

            int top_row = WaybillDetOutGridView.TopRowIndex;
            WaybillDetOutBS.DataSource = wbd_list;
            WaybillDetOutGridView.TopRowIndex = top_row;

            GetOk();
        }

        bool GetOk()
        {
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && RecipeComboBox.EditValue != null && (int)WhComboBox.EditValue > 0 && OnDateDBEdit.EditValue != null && WaybillDetOutBS.Count > 0);

            if (recult && TurnDocCheckBox.Checked)
            {
                recult = (!wbd_list.Any(w => w.Rsv == 0 && w.PosType == 0) && WaybillDetOutBS.Count > 0);
            }

            RecipeComboBox.Enabled = WaybillDetOutBS.Count == 0;
            ReceptBtn.Enabled = RecipeComboBox.Enabled;
            WhComboBox.Enabled = RecipeComboBox.Enabled;
            WhInBtn.Enabled = RecipeComboBox.Enabled;

            AddMaterialBtn.Enabled = (WhComboBox.EditValue != DBNull.Value && RecipeComboBox.EditValue != DBNull.Value);

            EditMaterialBtn.Enabled = WaybillDetOutBS.Count > 0;
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;
            ByRecipeBtn.Enabled = EditMaterialBtn.Enabled;

            OkButton.Enabled = recult;
            return recult;
        }

        private void frmWBDeboning_FormClosed(object sender, FormClosedEventArgs e)
        {
            DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

            if (is_new_record)
            {
                _db.DeleteWhere<WaybillList>(w => w.WbillId == _wbill_id);
            }

            _db.Dispose();
        }

        private void frmWBDeboning_Shown(object sender, EventArgs e)
        {
            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            NowDateBtn.Enabled = OnDateDBEdit.Enabled;

            WaybillDetOutGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

            NumEdit.Enabled = user_settings.AccessEditDocNum;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DBHelper.CheckInDate(wb, _db, OnDateDBEdit.DateTime))
            {
                return;
            }

            wb.UpdatedAt = DateTime.Now;
            _db.SaveChanges();

            if (TurnDocCheckBox.Checked)
            {
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
            dynamic row = RecipeComboBox.GetSelectedDataRow();
            using (var frm = new frmWriteOffDet(_db, null, wb))
            {
                frm.mat_id = row.MatId;
                frm.amount = row.Amount;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //  current_transaction = current_transaction.CommitRetaining(_db);
                    //   UpdLockWB();
                    RefreshDeboningDet(true);
                    RefreshDet();
                }
            }
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row != null)
            {
               using( var frm = new frmWriteOffDet(_db, wbd_row.PosId, wb))
               {
                   frm.ShowDialog();
               }

            //    current_transaction = current_transaction.CommitRetaining(_db);
            //    UpdLockWB();
                RefreshDeboningDet(true);
                RefreshDet();
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch(xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:
                    if (wbd_row != null)
                    {
                        DelRsvBarBtn.PerformClick();

                        _db.DeleteWhere<WaybillDet>(w => w.PosId == wbd_row.PosId);
                        _db.SaveChanges();
                        RefreshDeboningDet(true);
                        WaybillDetOutGridView.DeleteSelectedRows();

                        GetOk();
                    }

                    break;

                case 1:
                    {
                        var dr = DeboningDetGridView.GetFocusedRow() as v_DeboningDet;

                        if (MessageBox.Show($"Ви дійсно бажаєте видалити {dr.MatName} з документа?", "Обвалка сировини №" + wb.Num, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            _db.DeleteWhere<DeboningDet>(w => w.DebId == dr.DebId);

                            RefreshDeboningDet();
                        }

                        break;
                    }

            }

        }

        private void RsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var r = new ObjectParameter("RSV", typeof(Int32));

            _db.ReservedPosition(wbd_row.PosId, r, DBHelper.CurrentUser.UserId);
         //   current_transaction = current_transaction.CommitRetaining(_db);
         //   UpdLockWB();

            if (r.Value != null)
            {
                wbd_row.Rsv = (int)r.Value;
                WaybillDetOutGridView.RefreshRow(WaybillDetOutGridView.FocusedRowHandle);
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
            wb.OnDate = DBHelper.ServerDateTime();
            OnDateDBEdit.DateTime = wb.OnDate;

            _db.SaveChanges();
        }

        private void NumEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void ByRecipeBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row.Rsv == 1)
            {
                _db.SaveChanges();
                RefreshDeboningDet(true);
                xtraTabControl1.SelectedTabPageIndex = 1;
            }
        }
/*
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
        }*/

        private void RefreshDeboningDet(bool reset = false)
        {
            if (reset)
            {
                _db.GetDeboningDet(_wbill_id);
            }

            DeboningDetGridControl.DataSource = _db.v_DeboningDet.AsNoTracking().Where(w => w.WBillId == _wbill_id).ToList();
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
            }

            GetOk();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WhComboBox_EditValueChanged(object sender, EventArgs e)
        {
            wb.WayBillMake.SourceWId = (int)WhComboBox.EditValue;

            GetOk();
        }

        private void DeboningGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var dr = DeboningDetGridView.GetRow(e.RowHandle) as v_DeboningDet;

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

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            PrintDoc.Show(wb.Id, wb.WType, _db);
        }

        private void WhInBtn_Click(object sender, EventArgs e)
        {
            WhComboBox.EditValue = IHelper.ShowDirectList(WhComboBox.EditValue, 2);
         }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            PersonMakeComboBox.EditValue = IHelper.ShowDirectList(PersonMakeComboBox.EditValue, 3);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            KagentComboBox.EditValue = IHelper.ShowDirectList(KagentComboBox.EditValue, 3);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            PersonComboBox.EditValue = IHelper.ShowDirectList(PersonComboBox.EditValue, 3);
        }

        private void ReceptBtn_Click(object sender, EventArgs e)
        {
            RecipeComboBox.EditValue = IHelper.ShowDirectList(RecipeComboBox.EditValue, 15);
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

        private void frmWBDeboning_FormClosing(object sender, FormClosingEventArgs e)
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.DeleteWhere<DeboningDet>(w => w.WBillId == _wbill_id && w.Amount == 0);
            _db.SaveChanges();
            RefreshDeboningDet();

        }

        private void DeboningDetGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                popupMenu1.ShowPopup(p2);
            }
        }
    }
}
