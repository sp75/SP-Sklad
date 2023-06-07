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
using DevExpress.XtraEditors;
using SP_Sklad.Properties;

namespace SP_Sklad.WBForm
{
    public partial class frmWayBillMove : DevExpress.XtraEditors.XtraForm
    {
        private const int _wtype = 4;
        private BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        private WaybillList wb { get; set; }
        private List<GetWayBillDetOut_Result> wbd_list { get; set; }
        public bool is_new_record { get; set; }
        private int? _wid { get; set; }
        private GetWayBillDetOut_Result focused_dr
        {
            get { return WaybillDetOutGridView.GetFocusedRow() as GetWayBillDetOut_Result; }
        }

        private UserSettingsRepository user_settings { get; set; }

        public frmWayBillMove(int? wbill_id = null, int? wid = null)
        {
            is_new_record = false;
            _wbill_id = wbill_id;
            _wid = wid;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();
        }

        private void frmWayBillMove_Load(object sender, EventArgs e)
        {
            KagentComboBox.Properties.DataSource = DBHelper.Persons;
            PersonOutComboBox.Properties.DataSource = DBHelper.Persons;
            PersonInComboBox.Properties.DataSource = DBHelper.Persons;
            WhOutComboBox.Properties.DataSource = DBHelper.WhList;
             WhInComboBox.Properties.DataSource = DBHelper.WhList;

            if (_wbill_id == null)
            {
                is_new_record = true;

                wb = _db.WaybillList.Add(new WaybillList()
                {
                    Id = Guid.NewGuid(),
                    WType = _wtype,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = "",
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    WaybillMove = new WaybillMove { SourceWid = _wid.HasValue ? _wid.Value : DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId },
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId
                });
                
                _db.SaveChanges();

                _wbill_id = wb.WbillId;
            }
            else
            {
                wb = _db.WaybillList.FirstOrDefault(f => f.WbillId == _wbill_id);
            }

            if (wb != null && wb.WaybillMove != null)
            {
                DBHelper.UpdateSessionWaybill(wb.WbillId);

                if (is_new_record)
                {
                    wb.Num = new BaseEntities().GetDocNum("wb_move").FirstOrDefault();
                }

                TurnDocCheckBox.EditValue = wb.Checked;

                WhOutComboBox.DataBindings.Add(new Binding("EditValue", wb.WaybillMove, "SourceWid", true, DataSourceUpdateMode.OnPropertyChanged));
                WhInComboBox.DataBindings.Add(new Binding("EditValue", wb.WaybillMove, "DestWId", true, DataSourceUpdateMode.OnPropertyChanged));

                PersonOutComboBox.DataBindings.Add(new Binding("EditValue", wb.WaybillMove, "PersonId", true, DataSourceUpdateMode.OnPropertyChanged));
                PersonInComboBox.DataBindings.Add(new Binding("EditValue", wb, "PersonId", true, DataSourceUpdateMode.OnPropertyChanged));
                KagentComboBox.DataBindings.Add(new Binding("EditValue", wb, "KaId", true, DataSourceUpdateMode.OnPropertyChanged));

                NumEdit.DataBindings.Add(new Binding("EditValue", wb, "Num"));
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", wb, "OnDate", true, DataSourceUpdateMode.OnPropertyChanged));

                NotesEdit.DataBindings.Add(new Binding("EditValue", wb, "Notes"));
                ReasonEdit.DataBindings.Add(new Binding("EditValue", wb, "Reason"));
            }

            RefreshDet();
        }

    /*    private void UpdLockWB()
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
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && KagentComboBox.EditValue != null && OnDateDBEdit.EditValue != null && WaybillDetOutBS.Count > 0 && WhInComboBox.EditValue != DBNull.Value );

            if (recult && TurnDocCheckBox.Checked)
            {
                recult = !wbd_list.Any(w => w.Rsv == 0 && w.PosType == 0);
            }

            WhOutComboBox.Enabled = WaybillDetOutBS.Count == 0;
            barSubItem1.Enabled = KagentComboBox.EditValue != null;

            EditMaterialBtn.Enabled = WaybillDetOutBS.Count > 0;
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;

            OkButton.Enabled = recult;
            return recult;

        }

        private void frmWayBillMove_FormClosed(object sender, FormClosedEventArgs e)
        {
            DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

            if (is_new_record)
            {
                _db.DeleteWhere<WaybillList>(w => w.WbillId == _wbill_id);
            }

            _db.Dispose();
        }

        private void frmWayBillMove_Shown(object sender, EventArgs e)
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

            wb.UpdatedAt = DateTime.Now;

            _db.Save(wb.WbillId);

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

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetOutGridView.GetRow(WaybillDetOutGridView.FocusedRowHandle) as GetWayBillDetOut_Result;

            if (dr != null)
            {
                using (var frm = new frmWBMoveDet(_db, dr.PosId, wb))
                {
                    frm.ShowDialog();
                }

                RefreshDet();
            }
        }

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmWBMoveDet(_db, null, wb))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    RefreshDet();
                }
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_dr != null)
            {
                _db.DeleteWhere<WaybillDet>(w => w.PosId == focused_dr.PosId);
                _db.SaveChanges();

                WaybillDetOutGridView.DeleteSelectedRows();

                GetOk();
            }
        }

        public class MaterialsByWh
        {
            public int MatId { get; set; }
            public int WId { get; set; }
            public string Name { get; set; }
            public string MsrName { get; set; }
            public decimal Remain { get; set; }
            public decimal Rsv { get; set; }
        }

        private void RsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();
            var r = new ObjectParameter("RSV", typeof(Int32));

            _db.ReservedPosition(focused_dr.PosId, r, DBHelper.CurrentUser.UserId);


            if (r.Value != null)
            {
                var focused_wbd = _db.WaybillDet.Find(focused_dr.PosId);
                focused_dr.Rsv = (int)r.Value;
                focused_dr.Price = focused_wbd.Price;
                focused_dr.Total = focused_wbd.Total;

                WaybillDetOutGridView.RefreshRow(WaybillDetOutGridView.FocusedRowHandle);
            }

            /*
            if (!DBHelper.RsvItem(focused_dr.PosId.Value, _db))
            {
                MessageBox.Show("Не вдалося зарезервувати товар!");
            }
            
            RefreshDet();*/

            GetOk();
        }

        private void DelRsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_dr.Rsv == 1 && focused_dr.PosId > 0)
            {
                _db.DeleteWhere<WMatTurn>(w => w.SourceId == focused_dr.PosId);
            //    current_transaction = current_transaction.CommitRetaining(_db);
             //   UpdLockWB();
                focused_dr.Rsv = 0;
                WaybillDetOutGridView.RefreshRow(WaybillDetOutGridView.FocusedRowHandle);
            }

            GetOk();
        }

        private void RsvAllBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*     var res = _db.ReservedAllPosition(wb.WbillId, DBHelper.CurrentUser.UserId);

                 if (res.Any())
                 {
                     MessageBox.Show("Не вдалося зарезервувати деякі товари!");
                 }*/

        /*    foreach (var item in _db.GetWayBillDetOut(_wbill_id).OrderBy(o => o.Num).ToList())
            {
                if (item.Rsv != 1)
                {
                    DBHelper.RsvItem(item.PosId.Value, _db);
                }
            }*/

            _db.SaveChanges();
            var list = new List<string>();

            var r = new ObjectParameter("RSV", typeof(Int32));

            var wb_list = _db.GetWayBillDetOut(_wbill_id).ToList().Where(w => w.Rsv != 1).ToList();
            progressBarControl1.Visible = true;
            progressBarControl1.Properties.Maximum = wb_list.Count;
            foreach (var i in wb_list)
            {
                _db.ReservedPosition(i.PosId, r, DBHelper.CurrentUser.UserId);

                if (r.Value != null && (int)r.Value == 0)
                {
                    list.Add(i.MatName);
                }

                progressBarControl1.PerformStep();
                progressBarControl1.Update();
            }
            progressBarControl1.Visible = false;

            if (list.Any())
            {
                MessageBox.Show("Не вдалося зарезервувати: " + String.Join(",", list));
            }


            RefreshDet();

        }

        private void DelAllRsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.DeleteAllReservePosition(wb.WbillId);
        //    current_transaction = current_transaction.CommitRetaining(_db);
       //     UpdLockWB();

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

        private void NumEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
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

            PrintDoc.Show(wb.Id, wb.WType, _db);
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(focused_dr.MatId);
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            IHelper.ShowMatListByWH3(_db, wb, WhOutComboBox.EditValue.ToString());
            RefreshDet();
        }

        private void WhOutComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                WhOutComboBox.EditValue = IHelper.ShowDirectList(WhOutComboBox.EditValue, 2);
            }
        }

        private void WhInComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                WhInComboBox.EditValue = IHelper.ShowDirectList(WhOutComboBox.EditValue, 2);
            }
        }

        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                OnDateDBEdit.EditValue = DBHelper.ServerDateTime();
            }
        }

        private void PersonOutComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                ((LookUpEdit)sender).EditValue = IHelper.ShowDirectList(((LookUpEdit)sender).EditValue, 3);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WhInComboBox_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void frmWayBillMove_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
