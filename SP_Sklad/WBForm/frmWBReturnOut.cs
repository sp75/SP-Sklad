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
using SP_Sklad.Common;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;
using EntityState = System.Data.Entity.EntityState;
using System.Data.Entity.Core.Objects;
using SP_Sklad.Reports;

namespace SP_Sklad.WBForm
{
    public partial class frmWBReturnOut : DevExpress.XtraEditors.XtraForm
    {
        private const int _wtype = -6;

        public BaseEntities _db { get; set; }
        public int? _wbill_id { get; set; }
        public int? doc_id { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private WaybillList wb { get; set; }
        private List<GetWayBillDetOut_Result> wbd_list { get; set; }
        private GetWayBillDetOut_Result focused_dr
        {
            get { return WaybillDetOutGridView.GetFocusedRow() as GetWayBillDetOut_Result; }
        } 

        public frmWBReturnOut(int? wbill_id)
        {
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction(/*IsolationLevel.RepeatableRead*/);

            InitializeComponent();
        }

        private void frmWBReturnOut_Load(object sender, EventArgs e)
        {
            KagentComboBox.Properties.DataSource = DBHelper.Kagents;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;

            if (_wbill_id == null && doc_id == null)
            {
                wb = _db.WaybillList.Add(new WaybillList()
                {
                    Id = Guid.NewGuid(),
                    WType = _wtype,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = new BaseEntities().GetDocNum("wb_return_out").FirstOrDefault(),
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    EntId = DBHelper.Enterprise.KaId,
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
                catch (System.Data.Entity.Infrastructure.DbUpdateException exp)
                {
                    MessageBox.Show(exp.InnerException.InnerException.Message);
                    Close();
                }

            }

            if (wb != null)
            {
                wb.UpdatedBy = DBHelper.CurrentUser.UserId;

                TurnDocCheckBox.EditValue = wb.Checked;

                KagentComboBox.DataBindings.Add(new Binding("EditValue", wb, "KaId", true, DataSourceUpdateMode.OnValidation));
                PersonComboBox.DataBindings.Add(new Binding("EditValue", wb, "PersonId", true, DataSourceUpdateMode.OnValidation));
                NumEdit.DataBindings.Add(new Binding("EditValue", wb, "Num"));
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", wb, "OnDate"));
                NotesEdit.DataBindings.Add(new Binding("EditValue", wb, "Notes"));
                ReasonEdit.DataBindings.Add(new Binding("EditValue", wb, "Reason"));

                payDocUserControl1.OnLoad(_db, wb);
            }

            RefreshDet();
        }

        private void UpdLockWB()
        {
            if (wb != null)
            {
                _db.Entry<WaybillList>(wb).State = EntityState.Detached;
            }

            if (_wbill_id == null && doc_id != null)
            {
                _wbill_id = _db.WaybillList.AsNoTracking().FirstOrDefault(f => f.DocId == doc_id).WbillId;
            }

            wb = _db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK, NOWAIT) where WbillId = {0} ", _wbill_id).FirstOrDefault();

            _db.Entry<WaybillList>(wb).State = EntityState.Modified;
            _db.Entry<WaybillList>(wb).Property(f => f.SummPay).IsModified = false;

        }

        private void RefreshDet()
        {
            wbd_list = _db.GetWayBillDetOut(_wbill_id).ToList();

            int top_row = WaybillDetOutGridView.TopRowIndex;
            WaybillDetOutBS.DataSource = wbd_list;
            WaybillDetOutGridView.TopRowIndex = top_row;

            GetOk();
        }

        bool GetOk()
        {
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && KagentComboBox.EditValue != null && OnDateDBEdit.EditValue != null && wbd_list != null && wbd_list.Any());

            if (recult  && TurnDocCheckBox.Checked)
            {
                recult = !wbd_list.Any(w => w.Rsv == 0 && w.PosType == 0);
            }

            KagentComboBox.Enabled = (wbd_list == null || !wbd_list.Any());
            barSubItem1.Enabled = KagentComboBox.EditValue != null && KagentComboBox.EditValue != DBNull.Value;

            EditMaterialBtn.Enabled = (wbd_list != null && wbd_list.Any());
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;

            OkButton.Enabled = recult;
            return recult;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (TurnDocCheckBox.Checked && !DBHelper.CheckOrderedInSuppliers(wb.WbillId, _db)) return;

            if (!DBHelper.CheckInDate(wb, _db, OnDateDBEdit.DateTime))
            {
                return;
            }

            wb.UpdatedAt = DateTime.Now;
            _db.SaveChanges();

            payDocUserControl1.Execute(wb.WbillId);

            current_transaction.Commit();

            if (TurnDocCheckBox.Checked)
            {
                _db.ExecuteWayBill(wb.WbillId, null);
            }

            Close();
        }

        private void frmWBReturnOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void frmWBReturnOut_Shown(object sender, EventArgs e)
        {
            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            NowDateBtn.Enabled = OnDateDBEdit.Enabled;

            PersonComboBox.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            PersonEditBtn.Enabled = PersonComboBox.Enabled;
        }

        private void WaybillDetOutGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
        }

        private void KagentComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = (KagentList)KagentComboBox.GetSelectedDataRow();
            if (row == null)
            {
                return;
            }
            wb.KaId = row.KaId;

            wb.Nds = row.NdsPayer == 1 ? DBHelper.CommonParam.Nds : 0;

            GetOk();
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetOutGridView.GetRow(WaybillDetOutGridView.FocusedRowHandle) as GetWayBillDetOut_Result;
            if (dr != null)
            {
                new frmWBReturnDetOut(_db, dr.PosId, wb, (int)KagentComboBox.EditValue).ShowDialog();

                current_transaction = current_transaction.CommitRetaining(_db);
                UpdLockWB();
                RefreshDet();
            }
        }

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new frmWBReturnDetOut(_db, null, wb, (int)KagentComboBox.EditValue).ShowDialog() == DialogResult.OK)
            {
                current_transaction = current_transaction.CommitRetaining(_db);
                UpdLockWB();
                RefreshDet();
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_dr != null)
            {
                _db.DeleteWhere<WaybillDet>(w => w.PosId == focused_dr.PosId);
                _db.SaveChanges();

                RefreshDet();
            }
        }

        private void RsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var r = new ObjectParameter("RSV", typeof(Int32));

            _db.ReservedPosition(focused_dr.PosId, r);
            current_transaction = current_transaction.CommitRetaining(_db);
            UpdLockWB();

            if (r.Value != null)
            {
                focused_dr.Rsv = (int)r.Value;
                WaybillDetOutGridView.RefreshRow(WaybillDetOutGridView.FocusedRowHandle);
            }

            GetOk();
        }

        private void DelRsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_dr.Rsv == 1 && focused_dr.PosId > 0)
            {
                _db.DeleteWhere<WMatTurn>(w => w.SourceId == focused_dr.PosId);
                current_transaction = current_transaction.CommitRetaining(_db);
                UpdLockWB();
                focused_dr.Rsv = 0;
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

        private void WaybillDetOutGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.WbDetPopupMenu.ShowPopup(p2);
            }
        }

        private void NumEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WbDetPopupMenu_Popup(object sender, EventArgs e)
        {
            RsvBarBtn.Enabled = (focused_dr.Rsv == 0 && focused_dr.PosId > 0);
            DelRsvBarBtn.Enabled = (focused_dr.Rsv == 1 && focused_dr.PosId > 0);
            RsvAllBarBtn.Enabled = (WaybillDetOutGridView.FocusedRowHandle >= 0);
            DelAllRsvBarBtn.Enabled = (WaybillDetOutGridView.FocusedRowHandle >= 0);
        }

        private void MarkBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wbd = _db.WaybillDet.Find(focused_dr.PosId);
            if (wbd.Checked == 1)
            {
                wbd.Checked = 0;
                focused_dr.Checked = 0;
            }
            else
            {
                wbd.Checked = 1;
                focused_dr.Checked = 1;
            }

            WaybillDetOutGridView.RefreshRow(WaybillDetOutGridView.FocusedRowHandle);
        }

        private void NowDateBtn_Click(object sender, EventArgs e)
        {
            wb.OnDate = DBHelper.ServerDateTime();
            OnDateDBEdit.DateTime = wb.OnDate;
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*_db.SaveChanges(); //Не доделано

            IHelper.ShowMatListByWH2(_db, wb, (int)KagentComboBox.EditValue);
            RefreshDet();*/
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(focused_dr.MatId, _db);

        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            PrintDoc.Show(wb.Id, wb.WType, _db);
        }

        private void KagBalBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowKABalans((int)KagentComboBox.EditValue);
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(focused_dr.MatId);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            wb.KaId = (int)IHelper.ShowDirectList(KagentComboBox.EditValue, 1);
            KagentComboBox.EditValue = wb.KaId;
        }

        private void PersonEditBtn_Click(object sender, EventArgs e)
        {
            PersonComboBox.EditValue = IHelper.ShowDirectList(PersonComboBox.EditValue, 3);
        }
        
    }
}
