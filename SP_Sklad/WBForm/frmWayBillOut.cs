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

namespace SP_Sklad.WBForm
{
    public partial class frmWayBillOut : Form
    {
        private int _wtype { get; set; }
        BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private WaybillList wb { get; set; }

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
                KagentComboBox.DataBindings.Add(new Binding("EditValue", wb, "KaId", true, DataSourceUpdateMode.OnValidation));
                PersonComboBox.DataBindings.Add(new Binding("EditValue", wb, "PersonId", true, DataSourceUpdateMode.OnValidation));
                TurnDocCheckBox.DataBindings.Add(new Binding("EditValue", wb, "Checked"));
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
            wb.UpdatedAt = DateTime.Now; wb.UpdatedAt = DateTime.Now;
            _db.SaveChanges();

            if (!DBHelper.CheckInDate(wb, _db, OnDateDBEdit.DateTime))
            {
                return;
            }

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
            WaybillDetInGridControl.DataSource = _db.GetWayBillDetOut(_wbill_id);
            GetOk();
        }

        bool GetOk()
        {
            bool recult = (NumEdit.EditValue != null && KagentComboBox.EditValue != null && OnDateDBEdit.EditValue != null && WaybillDetInGridView.DataRowCount > 0);
            barSubItem1.Enabled = KagentComboBox.EditValue != null;

          //  OkButton.Enabled = recult;
            EditMaterialBtn.Enabled = WaybillDetInGridView.DataRowCount > 0;
            DelMaterialBtn.Enabled = WaybillDetInGridView.DataRowCount > 0;

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
            var dr = WaybillDetInGridView.GetRow(WaybillDetInGridView.FocusedRowHandle) as GetWayBillDetOut_Result;

            if (dr != null)
            {
           //     DeleteRsv(dr.PosId);
                _db.WaybillDet.Remove(_db.WaybillDet.Find(dr.PosId));
                _db.SaveChanges();

                RefreshDet();
            }
        }

        private void DeleteRsv(int? pos_id) 
        {
            _db.WMatTurn.RemoveRange(_db.WMatTurn.Where(w => w.SourceId == pos_id));
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
            var dr = WaybillDetInGridView.GetRow(WaybillDetInGridView.FocusedRowHandle) as GetWayBillDetOut_Result;

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

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetInGridView.GetRow(WaybillDetInGridView.FocusedRowHandle) as GetWayBillDetOut_Result;

            if (dr.Rsv == 1 && dr.PosId > 0)
            {
                _db.WMatTurn.Remove(_db.WMatTurn.Single(w => w.SourceId == dr.PosId));

                _db.SaveChanges();
                current_transaction.Commit();
                current_transaction = _db.Database.BeginTransaction(IsolationLevel.RepeatableRead);

                RefreshDet();
            }
        }

    }
}
