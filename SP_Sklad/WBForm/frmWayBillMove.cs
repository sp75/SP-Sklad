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

namespace SP_Sklad.WBForm
{
    public partial class frmWayBillMove : Form
    {
        BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private WaybillList wb { get; set; }
        private WaybillMove wbm { get; set; }
        private GetWayBillDetOut_Result wbd_row { get; set; }
        private IQueryable<GetWayBillDetOut_Result> wbd_list { get; set; }

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
                    PersonId = DBHelper.CurrentUser.KaId
                });
                
                _db.SaveChanges();

                _wbill_id = wb.WbillId;

                wbm = new WaybillMove { WBillId = _wbill_id.Value };
            }
            else
            {
                try
                {
                    UpdLockWB();
                    _db.Entry<WaybillList>(wb).State = EntityState.Modified;
                    _db.Entry<WaybillList>(wb).Property(f => f.SummPay).IsModified = false;

                    wbm = _db.WaybillMove.Find(_wbill_id);
                }
                catch
                {
                    Close();
                }

            }

            if (wb != null && wbm != null)
            {
                TurnDocCheckBox.EditValue = wb.Checked;

                WhOutComboBox.DataBindings.Add(new Binding("EditValue", wbm, "SourceWid"));
                WhInComboBox.DataBindings.Add(new Binding("EditValue", wbm, "DestWId", true, DataSourceUpdateMode.OnValidation));
                PersonOutComboBox.DataBindings.Add(new Binding("EditValue", wbm, "PersonId", true, DataSourceUpdateMode.OnValidation));

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
            wb = _db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK, NOWAIT) where WbillId = {0}", _wbill_id).FirstOrDefault();
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

            if (recult && wb.WType == -1 && TurnDocCheckBox.Checked)
            {
                recult = !wbd_list.Any(w => w.Rsv == 0 && w.PosType == 0);
            }

            barSubItem1.Enabled = KagentComboBox.EditValue != null;

            EditMaterialBtn.Enabled = WaybillDetOutGridView.DataRowCount > 0;
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;

     //       OkButton.Enabled = recult;
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

            if (_db.Entry<WaybillMove>(wbm).State == EntityState.Detached)
            {
                _db.WaybillMove.Add(wbm);
            }

            wb.UpdatedAt = DateTime.Now;
            _db.SaveChanges();

            current_transaction.Commit();

            if (TurnDocCheckBox.Checked)
            {
                _db.ExecuteWayBill(wb.WbillId, null);
            }

            Close();
        }

        private void WaybillDetOutGridView_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = view.CalcHitInfo(pt);

            if (info.InRow || info.InRowCell)
            {
                EditMaterialBtn.PerformClick();
            }
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
    }
}
