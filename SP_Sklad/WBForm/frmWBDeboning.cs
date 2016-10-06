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
    public partial class frmWBDeboning : Form
    {
        BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private WaybillList wb { get; set; }
        private GetWayBillDetOut_Result wbd_row { get; set; }
        private IQueryable<GetWayBillDetOut_Result> wbd_list { get; set; }
        public int? rec_id { get; set; }
        public int? source_wid { get; set; }

        public frmWBDeboning(int? wbill_id = null)
        {
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction();

            InitializeComponent();
        }

        private void frmWBDeboning_Load(object sender, EventArgs e)
        {
            KagentComboBox.Properties.DataSource = DBHelper.Persons;
            PersonMakeComboBox.Properties.DataSource = DBHelper.Persons;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            WhComboBox.Properties.DataSource = DBHelper.WhList();
            repositoryItemWhEdit.DataSource = DBHelper.WhList();
            RecipeComboBox.Properties.DataSource = DB.SkladBase().MatRecipe.Where(w => w.RType == 2).Select(s => new
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
                    WType = -22,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = new BaseEntities().GetCounter("wb_deboning").FirstOrDefault(),
                    EntId = DBHelper.Enterprise.KaId,
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    KaId = DBHelper.CurrentUser.KaId,
                    WayBillMake = new WayBillMake
                    {
                        SourceWId = source_wid ?? DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId,
                        RecId = rec_id
                    },
                    Nds = 0
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
                //   AmountMakeEdit.DataBindings.Add(new Binding("EditValue", wb.WayBillMake, "Amount"));

                PersonMakeComboBox.DataBindings.Add(new Binding("EditValue", wb.WayBillMake, "PersonId", true, DataSourceUpdateMode.OnPropertyChanged));
                KagentComboBox.DataBindings.Add(new Binding("EditValue", wb, "KaId", true, DataSourceUpdateMode.OnPropertyChanged));
                PersonComboBox.DataBindings.Add(new Binding("EditValue", wb, "PersonId", true, DataSourceUpdateMode.OnPropertyChanged));

                NumEdit.DataBindings.Add(new Binding("EditValue", wb, "Num"));
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", wb, "OnDate", true, DataSourceUpdateMode.OnPropertyChanged));
                ToDateEdit.DataBindings.Add(new Binding("EditValue", wb, "ToDate"));

                NotesEdit.DataBindings.Add(new Binding("EditValue", wb, "Notes"));
                ReasonEdit.DataBindings.Add(new Binding("EditValue", wb, "Reason"));

                checkEdit2.Checked = wb.ToDate != null;
            }

            RefreshDet();
            RefreshDeboningDet();
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
            var dr = WaybillDetOutGridView.GetRow(WaybillDetOutGridView.FocusedRowHandle) as GetWayBillDetOut_Result;

            WaybillDetOutGridControl.DataSource = null;
            WaybillDetOutGridControl.DataSource = wbd_list;

            WaybillDetOutGridView.FocusedRowHandle = FindRowHandleByRowObject(WaybillDetOutGridView, dr);

            GetOk();
        }

        private int FindRowHandleByRowObject(GridView view, GetWayBillDetOut_Result dr)
        {
            if (dr != null)
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    if (dr.PosId == (view.GetRow(i) as GetWayBillDetOut_Result).PosId)
                    {
                        return i;
                    }
                }
            }
            return GridControl.InvalidRowHandle;
        }

        bool GetOk()
        {
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && RecipeComboBox.EditValue != null && (int)WhComboBox.EditValue > 0 && OnDateDBEdit.EditValue != null && WaybillDetOutGridView.DataRowCount > 0);

            if (recult && TurnDocCheckBox.Checked)
            {
                recult = (!wbd_list.Any(w => w.Rsv == 0 && w.PosType == 0) && DeboningDetGridView.DataRowCount > 0);
            }

            RecipeComboBox.Enabled = WaybillDetOutGridView.DataRowCount == 0;
            ReceptBtn.Enabled = RecipeComboBox.Enabled;
            WhComboBox.Enabled = RecipeComboBox.Enabled;
            WhInBtn.Enabled = RecipeComboBox.Enabled;

            AddMaterialBtn.Enabled = (WhComboBox.EditValue != DBNull.Value && RecipeComboBox.EditValue != DBNull.Value);

            EditMaterialBtn.Enabled = WaybillDetOutGridView.DataRowCount > 0;
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;
            ByRecipeBtn.Enabled = EditMaterialBtn.Enabled;

            OkButton.Enabled = recult;
            return recult;
        }

        private void frmWBDeboning_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void frmWBDeboning_Shown(object sender, EventArgs e)
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
            dynamic row = RecipeComboBox.GetSelectedDataRow();
            var frm = new frmWriteOffDet(_db, null, wb);

            frm.mat_id = row.MatId;
            frm.amount = row.Amount;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                current_transaction = current_transaction.CommitRetaining(_db);
                UpdLockWB();
                _db.GetDeboningDet(_wbill_id);
                RefreshDeboningDet();
                RefreshDet();
            }
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row != null)
            {
                new frmWriteOffDet(_db, wbd_row.PosId, wb).ShowDialog();

                current_transaction = current_transaction.CommitRetaining(_db);
                UpdLockWB();
                _db.GetDeboningDet(_wbill_id);
                RefreshDeboningDet();
                RefreshDet();
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row != null)
            {
                DelRsvBarBtn.PerformClick();

                _db.DeleteWhere<WaybillDet>(w => w.PosId == wbd_row.PosId);
                _db.SaveChanges();
                _db.GetDeboningDet(_wbill_id);
                RefreshDeboningDet();
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
        }

        private void WaybillDetOutGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.WbDetPopupMenu.ShowPopup(p2);
            }
        }

        private void WaybillDetOutGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            wbd_row = WaybillDetOutGridView.GetRow(WaybillDetOutGridView.FocusedRowHandle) as GetWayBillDetOut_Result;
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

                _db.GetDeboningDet(_wbill_id);

                RefreshDeboningDet();

                xtraTabControl1.SelectedTabPageIndex = 1;
            }
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
        }

        private void RefreshDeboningDet()
        {
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

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkEdit2.ContainsFocus) return;

            if (checkEdit2.Checked) wb.ToDate = OnDateDBEdit.DateTime.AddDays(3);
            else wb.ToDate = null;

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

            DeboningDetGridView.RefreshRow(e.RowHandle);
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
    }
}
