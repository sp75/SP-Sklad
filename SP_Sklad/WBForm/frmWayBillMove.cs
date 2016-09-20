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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;
using EntityState = System.Data.Entity.EntityState;
using System.Data.Entity.Core.Objects;
using SP_Sklad.Common;
using SP_Sklad.Reports;

namespace SP_Sklad.WBForm
{
    public partial class frmWayBillMove : Form
    {
        private BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private WaybillList wb { get; set; }
        private IQueryable<GetWayBillDetOut_Result> wbd_list { get; set; }
        private GetWayBillDetOut_Result focused_dr
        {
            get { return WaybillDetOutGridView.GetFocusedRow() as GetWayBillDetOut_Result; }
        } 

        public frmWayBillMove(int? wbill_id = null)
        {
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction();

            InitializeComponent();
        }

        private void frmWayBillMove_Load(object sender, EventArgs e)
        {
            KagentComboBox.Properties.DataSource = DBHelper.Persons;
            PersonOutComboBox.Properties.DataSource = DBHelper.Persons;
            PersonInComboBox.Properties.DataSource = DBHelper.Persons;
            WhOutComboBox.Properties.DataSource = DBHelper.WhList();
            WhInComboBox.Properties.DataSource = DBHelper.WhList();

            if (_wbill_id == null)
            {
                wb = _db.WaybillList.Add(new WaybillList()
                {
                    WType = 4,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = new BaseEntities().GetCounter("wb_move").FirstOrDefault(),
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    WaybillMove = new WaybillMove { SourceWid = DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId }
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

            if (wb != null && wb.WaybillMove != null)
            {
                wb.UpdatedBy = DBHelper.CurrentUser.UserId;

                TurnDocCheckBox.EditValue = wb.Checked;

                WhOutComboBox.DataBindings.Add(new Binding("EditValue", wb.WaybillMove, "SourceWid"));
                WhInComboBox.DataBindings.Add(new Binding("EditValue", wb.WaybillMove, "DestWId", true, DataSourceUpdateMode.OnValidation));
                PersonOutComboBox.DataBindings.Add(new Binding("EditValue", wb.WaybillMove, "PersonId", true, DataSourceUpdateMode.OnValidation));

                PersonInComboBox.DataBindings.Add(new Binding("EditValue", wb, "PersonId", true, DataSourceUpdateMode.OnValidation));

                KagentComboBox.DataBindings.Add(new Binding("EditValue", wb, "KaId", true, DataSourceUpdateMode.OnValidation));

                NumEdit.DataBindings.Add(new Binding("EditValue", wb, "Num"));
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", wb, "OnDate"));

                NotesEdit.DataBindings.Add(new Binding("EditValue", wb, "Notes"));
                ReasonEdit.DataBindings.Add(new Binding("EditValue", wb, "Reason"));
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
            if ( wb != null )
            {
                wb.WaybillMove = _db.WaybillMove.Find(_wbill_id);
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
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && KagentComboBox.EditValue != null && OnDateDBEdit.EditValue != null && WaybillDetOutGridView.DataRowCount > 0);

            if (recult && TurnDocCheckBox.Checked)
            {
                recult = !wbd_list.Any(w => w.Rsv == 0 && w.PosType == 0);
            }

            barSubItem1.Enabled = KagentComboBox.EditValue != null;

            EditMaterialBtn.Enabled = WaybillDetOutGridView.DataRowCount > 0;
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;

            OkButton.Enabled = recult;
            return recult;
        }

        private void frmWayBillMove_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void frmWayBillMove_Shown(object sender, EventArgs e)
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

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetOutGridView.GetRow(WaybillDetOutGridView.FocusedRowHandle) as GetWayBillDetOut_Result;

            if (dr != null)
            {
                new frmWBReturnDetOut(_db, dr.PosId, wb, 0).ShowDialog();

                current_transaction = current_transaction.CommitRetaining(_db);
                UpdLockWB();
                RefreshDet();
            }
        }

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new frmWBReturnDetOut(_db, null, wb,0).ShowDialog() == DialogResult.OK)
            {
                current_transaction = current_transaction.CommitRetaining(_db);
                UpdLockWB();
                RefreshDet();
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetOutGridView.GetRow(WaybillDetOutGridView.FocusedRowHandle) as GetWayBillDetOut_Result;

            if (dr != null)
            {
                _db.DeleteWhere<WaybillDet>(w => w.PosId == dr.PosId);
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

        private void WaybillDetOutGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
          
        }

        private void NumEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void NowDateBtn_Click(object sender, EventArgs e)
        {
            //
        }

        private void WbDetPopupMenu_Popup(object sender, EventArgs e)
        {
            RsvBarBtn.Enabled = (focused_dr.Rsv == 0 && focused_dr.PosId > 0);
            DelRsvBarBtn.Enabled = (focused_dr.Rsv == 1 && focused_dr.PosId > 0);
            RsvAllBarBtn.Enabled = (WaybillDetOutGridView.FocusedRowHandle >= 0);
            DelAllRsvBarBtn.Enabled = (WaybillDetOutGridView.FocusedRowHandle >= 0);
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(focused_dr.MatId, _db);
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            PrintDoc.Show(wb.DocId.Value, wb.WType, _db);
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(focused_dr.MatId);
        }
    }
}
