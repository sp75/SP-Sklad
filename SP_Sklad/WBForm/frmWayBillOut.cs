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

namespace SP_Sklad.WBForm
{
    public partial class frmWayBillOut : Form
    {
        private int _wtype { get; set; }
        BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private WaybillList wb { get; set; }
        private GetWayBillDetOut_Result wbd_row { get; set; }
        private IQueryable<GetWayBillDetOut_Result> wbd_list { get; set; }

        public frmWayBillOut(int wtype, int? wbill_id)
        {
            _wtype = wtype;
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction(IsolationLevel.RepeatableRead);

            InitializeComponent();
        }

        private void frmWayBillOut_Load(object sender, EventArgs e)
        {
            KagentComboBox.Properties.DataSource = DBHelper.Kagents;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;

            if (_wbill_id == null)
            {
                wb = _db.WaybillList.Add(new WaybillList()
                {
                    WType = _wtype,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = new BaseEntities().GetCounter("wb_in").FirstOrDefault(),
                    CurrId = DBHelper.Currency.FirstOrDefault(w=> w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    EntId = DBHelper.Enterprise.KaId
                });

                _db.SaveChanges();
            }
            else
            {
                try
                {
                    wb = _db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK, NOWAIT) where WbillId = {0}", _wbill_id).FirstOrDefault();
                    _db.Entry<WaybillList>(wb).State = EntityState.Modified;
                    _db.Entry<WaybillList>(wb).Property(f => f.SummPay).IsModified = false;
                }
                catch
                {

                    Close();
                }

            }

            if (wb != null)
            {
                TurnDocCheckBox.EditValue = wb.Checked;

                KagentComboBox.DataBindings.Add(new Binding("EditValue", wb, "KaId", true, DataSourceUpdateMode.OnValidation));
                PersonComboBox.DataBindings.Add(new Binding("EditValue", wb, "PersonId", true, DataSourceUpdateMode.OnValidation));
                NumEdit.DataBindings.Add(new Binding("EditValue", wb, "Num"));
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", wb, "OnDate"));
                ToDateEdit.DataBindings.Add(new Binding("EditValue", wb, "ToDate"));
                NotesEdit.DataBindings.Add(new Binding("EditValue", wb, "Notes"));
                ReasonEdit.DataBindings.Add(new Binding("EditValue", wb, "Reason"));

                payDocUserControl1.OnLoad(_db, wb);
            }

            RefreshDet();
        }

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var df = new frmWayBillDetOut(_db, null, wb);
            if (df.ShowDialog() == DialogResult.OK)
            {
                RefreshDet();
            }
        }

        private void frmWayBillOut_Shown(object sender, EventArgs e)
        {
            if (_wtype == -1) Text = "Властивості видаткової накладної, Продавець: " + DBHelper.Enterprise.Name;
            if (_wtype == 2) Text = "Властивості рахунка, Продавець: " + DBHelper.Enterprise.Name;
            if (_wtype == -16) Text = "Замовлення від клієнтів, Продавець: " + DBHelper.Enterprise.Name;
            checkEdit2.Visible = (_wtype == 2 || _wtype == -16);
            TurnDocCheckBox.Visible = !checkEdit2.Visible;


            AttLabel.Visible = (_wtype == -1);
            AttEdit.Visible = AttLabel.Visible;

            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            NowDateBtn.Enabled = OnDateDBEdit.Enabled;

            PersonComboBox.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            PersonEditBtn.Enabled = PersonComboBox.Enabled;

            if (TurnDocCheckBox.Checked) Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
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

        private void RefreshDet()
        {
            wbd_list = _db.GetWayBillDetOut(_wbill_id);

            WaybillDetOutGridControl.DataSource = null;
            WaybillDetOutGridControl.DataSource = wbd_list;

            GetOk();
        }

        bool GetOk()
        {
            bool recult = ( !String.IsNullOrEmpty(NumEdit.Text) && KagentComboBox.EditValue != null && OnDateDBEdit.EditValue != null && WaybillDetOutGridView.DataRowCount > 0);

            if (recult && wb.WType == -1 && TurnDocCheckBox.Checked)
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

        private void frmWayBillOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
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

        private void DeleteRsv(int? pos_id) 
        {
            _db.DeleteWhere<WMatTurn>(w => w.SourceId == pos_id);
        }

        private void KagentComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = (KagentList)KagentComboBox.GetSelectedDataRow();
            if (row == null)
            {
                return;
            }

            if (row.NdsPayer == 1)
            {
                wb.Nds = DBHelper.CommonParam.Nds;
            }
            else
            {
                wb.Nds = 0;
            }

            GetOk();
        }

        private void WaybillDetInGridView_DoubleClick(object sender, EventArgs e)
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
                if (dr.PosId > 0)
                {
                    var df = new frmWayBillDetOut(_db, dr.PosId, wb);
                    if (df.ShowDialog() == DialogResult.OK)
                    {
                        RefreshDet();
                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WaybillDetInGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.WbDetPopupMenu.ShowPopup(p2);
            }
        }

        private void RsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var r = new ObjectParameter("RSV", typeof(Int32));

            _db.ReservedPosition(wbd_row.PosId, r);
            current_transaction = current_transaction.CommitRetaining(_db);

            if (r.Value != null)
            {
                wbd_row.Rsv = (int)r.Value;
                WaybillDetOutGridView.RefreshRow(WaybillDetOutGridView.FocusedRowHandle);
            }
        }

        private void WaybillDetInGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            wbd_row = WaybillDetOutGridView.GetRow(WaybillDetOutGridView.FocusedRowHandle) as GetWayBillDetOut_Result;
        }

        private void WbDetPopupMenu_Popup(object sender, EventArgs e)
        {
            RsvBarBtn.Enabled = (wbd_row.Rsv == 0 && wbd_row.PosId > 0);
            DelRsvBarBtn.Enabled = (wbd_row.Rsv == 1 && wbd_row.PosId > 0);
            RsvAllBarBtn.Enabled = (WaybillDetOutGridView.FocusedRowHandle >= 0);
            DelAllRsvBarBtn.Enabled = (WaybillDetOutGridView.FocusedRowHandle >= 0);
        }

        private void RsvAllBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var res = DB.SkladBase().ReservedAllPosition(wb.WbillId);

            if (res.Any())
            {
                MessageBox.Show("Не вдалося зарезервувати деякі товари!");
            }
           
            RefreshDet();
        }

        private void DelRsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row.Rsv == 1 && wbd_row.PosId > 0)
            {
                _db.DeleteWhere<WMatTurn>(w => w.SourceId == wbd_row.PosId);
                current_transaction = current_transaction.CommitRetaining(_db);
                wbd_row.Rsv = 0;
                WaybillDetOutGridView.RefreshRow(WaybillDetOutGridView.FocusedRowHandle);
            }
        }

        private void DelAllRsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
         //   _db.DeleteWhere<WMatTurn>(w => w.SourceId == wbd_row.PosId);

            _db.DeleteAllReservePosition(wb.WbillId);
            current_transaction = current_transaction.CommitRetaining(_db);
         
            RefreshDet();
        }

        private void MarkBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wbd = _db.WaybillDet.Find(wbd_row.PosId);
            if (wbd.Checked == 1)
            {
                wbd.Checked = 0;
                wbd_row.Checked = 0;
            }
            else
            {
                wbd.Checked = 1;
                wbd_row.Checked = 1;
            }

            WaybillDetOutGridView.RefreshRow(WaybillDetOutGridView.FocusedRowHandle);
        }

        private void NumEdit_Validated(object sender, EventArgs e)
        {
            GetOk();
        }

        private void NowDateBtn_Click(object sender, EventArgs e)
        {
            OnDateDBEdit.DateTime = DBHelper.ServerDateTime();
        }



    }
}
