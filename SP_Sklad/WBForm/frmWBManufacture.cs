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

namespace SP_Sklad.WBForm
{
    public partial class frmWBManufacture : DevExpress.XtraEditors.XtraForm
    {
        private const int _wtype = -20;

        BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private WaybillList wb { get; set; }
        private GetWayBillDetOut_Result wbd_row
        {
            get { return WaybillDetOutGridView.GetFocusedRow() as GetWayBillDetOut_Result; }
        }
        private IQueryable<GetWayBillDetOut_Result> wbd_list { get; set; }

        public frmWBManufacture(int? wbill_id = null)
        {
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction();

            InitializeComponent();
        }

        private void frmWBManufacture_Load(object sender, EventArgs e)
        {
            KagentComboBox.Properties.DataSource = DBHelper.Persons;
            PersonMakeComboBox.Properties.DataSource = DBHelper.Persons;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            WhComboBox.Properties.DataSource = DBHelper.WhList();
            RecipeComboBox.Properties.DataSource = DB.SkladBase().MatRecipe.Where(w => w.RType == 1).Select(s => new
            {
                s.RecId,
                s.Name,
                s.Amount,
                MatName = s.Materials.Name,
                s.MatId
            }).ToList();

            if (_wbill_id == null)
            {
                wb = _db.WaybillList.Add(new WaybillList()
                {
                    WType = _wtype,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = new BaseEntities().GetDocNum("wb_make").FirstOrDefault(),
                    EntId = DBHelper.Enterprise.KaId,
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    KaId = DBHelper.CurrentUser.KaId,
                    WayBillMake = new WayBillMake { SourceWId = DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId },
                    Docs = new Docs { DocType = _wtype },
                    UpdatedBy = DBHelper.CurrentUser.UserId
                });
                _db.SaveChanges();

                _wbill_id = wb.WbillId;
            }
            else
            {
                try
                {
                    UpdLockWB();
                }
                catch
                {
                    Close();
                }

            }

            if (wb != null && wb.WayBillMake != null)
            {
                wb.UpdatedBy = DBHelper.CurrentUser.UserId;

                TurnDocCheckBox.EditValue = wb.Checked;
                RecipeComboBox.DataBindings.Add(new Binding("EditValue", wb.WayBillMake, "RecId", true, DataSourceUpdateMode.OnPropertyChanged));
                WhComboBox.DataBindings.Add(new Binding("EditValue", wb.WayBillMake, "SourceWId", true, DataSourceUpdateMode.OnPropertyChanged));
                AmountMakeEdit.DataBindings.Add(new Binding("EditValue", wb.WayBillMake, "Amount"));

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
        }

        private void UpdLockWB()
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
        }

        private void RefreshDet()
        {
            wbd_list = _db.GetWayBillDetOut(_wbill_id);

            int top_row = WaybillDetOutGridView.TopRowIndex;
            WaybillDetOutBS.DataSource = wbd_list;
            WaybillDetOutGridView.TopRowIndex = top_row;

            TechProcGridControl.DataSource = _db.v_TechProcDet.Where(w => w.WbillId == _wbill_id).OrderBy(o => o.Num).ToList();
            GetOk();
        }


        bool GetOk()
        {
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && RecipeComboBox.EditValue != null && WhComboBox.EditValue != null && OnDateDBEdit.EditValue != null && WaybillDetOutBS.Count > 0);

            if (recult && TurnDocCheckBox.Checked)
            {
                recult = !wbd_list.Any(w => w.Rsv == 0 && w.PosType == 0);
            }

            RecipeComboBox.Enabled = WaybillDetOutBS.Count == 0;
            ReceptBtn.Enabled = RecipeComboBox.Enabled;
            WhComboBox.Enabled = RecipeComboBox.Enabled;
            WhInBtn.Enabled = RecipeComboBox.Enabled;
            AmountMakeEdit.Enabled = RecipeComboBox.Enabled;

            barSubItem1.Enabled = (WhComboBox.EditValue != null && RecipeComboBox.EditValue != null && AmountMakeEdit.Value > 0);

            EditMaterialBtn.Enabled = WaybillDetOutBS.Count > 0;
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;

            OkButton.Enabled = recult;
            return recult;
        }

        private void frmWBManufacture_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void frmWBManufacture_Shown(object sender, EventArgs e)
        {
            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            NowDateBtn.Enabled = OnDateDBEdit.Enabled;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DBHelper.CheckInDate(wb, _db, OnDateDBEdit.DateTime))
            {
                return;
            }
       
            wb.WayBillMake.Amount = _db.WaybillDet.Where(w => w.WbillId == _wbill_id && w.Materials.MId == w.WaybillList.WayBillMake.MatRecipe.Materials.MId).Sum(s => s.Amount);

            wb.UpdatedAt = DateTime.Now;
            _db.SaveChanges();

            if (TurnDocCheckBox.Checked)
            {
                var ew = _db.ExecuteWayBill(wb.WbillId, null).ToList();
            }
            current_transaction.Commit();

            Close();
        }

        private void WaybillDetOutGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
        }

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new frmWriteOffDet(_db, null, wb).ShowDialog() == DialogResult.OK)
            {
                current_transaction = current_transaction.CommitRetaining(_db);
                UpdLockWB();
                RefreshDet();
            }
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetOutGridView.GetRow(WaybillDetOutGridView.FocusedRowHandle) as GetWayBillDetOut_Result;

            if (dr != null)
            {
                new frmWriteOffDet(_db, dr.PosId, wb).ShowDialog();

                current_transaction = current_transaction.CommitRetaining(_db);
                UpdLockWB();
                RefreshDet();

                var dd = _db.Entry<WaybillDet>(_db.WaybillDet.FirstOrDefault(w => w.PosId == dr.PosId)).State;
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetOutGridView.GetRow(WaybillDetOutGridView.FocusedRowHandle) as GetWayBillDetOut_Result;

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

            _db.ReservedPosition(wbd_row.PosId, r);
            current_transaction = current_transaction.CommitRetaining(_db);
            UpdLockWB();

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
                current_transaction = current_transaction.CommitRetaining(_db);
                UpdLockWB();
                wbd_row.Rsv = 0;
                WaybillDetOutGridView.RefreshRow(WaybillDetOutGridView.FocusedRowHandle);
            }

            GetOk();
        }

        private void RsvAllBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var res = _db.ReservedAllPosition(wb.WbillId);

            if (res.Any())
            {
                MessageBox.Show("Не вдалося зарезервувати деякі товари!");
            }

            current_transaction = current_transaction.CommitRetaining(_db);
            UpdLockWB();

            RefreshDet();
        }

        private void DelAllRsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.DeleteAllReservePosition(wb.WbillId);
            current_transaction = current_transaction.CommitRetaining(_db);
            UpdLockWB();

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
            OnDateDBEdit.EditValue = DBHelper.ServerDateTime();
            wb.OnDate = OnDateDBEdit.DateTime;
            _db.SaveChanges();
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
            dynamic row = RecipeComboBox.GetSelectedDataRow();

            if (RecipeComboBox.ContainsFocus && row != null)
            {
                wb.WayBillMake.Amount = row.Amount;
                AmountMakeEdit.EditValue = row.Amount;
                wb.WayBillMake.RecId = row.RecId;

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

            PrintDoc.Show(wb.DocId.Value, wb.WType, _db);
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

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            IHelper.ShowMatListByWH3(_db, wb, WhComboBox.EditValue.ToString());
            RefreshDet();
        }

        private void ReceptBtn_Click(object sender, EventArgs e)
        {
            RecipeComboBox.EditValue = IHelper.ShowDirectList(RecipeComboBox.EditValue, 13);
        }
    }
}
