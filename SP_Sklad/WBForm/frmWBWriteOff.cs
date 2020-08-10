using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;
using EntityState = System.Data.Entity.EntityState;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Data.Entity.Core.Objects;
using SP_Sklad.Reports;
using SP_Sklad.Common;
using DevExpress.XtraEditors;
using SP_Sklad.Properties;

namespace SP_Sklad.WBForm
{
    public partial class frmWBWriteOff : DevExpress.XtraEditors.XtraForm
    {
        private const int _wtype = -5;

        public BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        public Guid? doc_id { get; set; }
        private WaybillList wb { get; set; }
        public bool is_new_record { get; set; }

        private List<GetWayBillDetOut_Result> wbd_list { get; set; }
        private GetWayBillDetOut_Result focused_dr
        {
            get { return WaybillDetOutGridView.GetFocusedRow() as GetWayBillDetOut_Result; }
        }
        private UserSettingsRepository user_settings { get; set; }

        public frmWBWriteOff(int? wbill_id=null)
        {
            is_new_record = false;
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();
        }

        private void frmWBWriteOff_Load(object sender, EventArgs e)
        {
            WhOutComboBox.Properties.DataSource = DBHelper.WhList;
            lookUpEdit1.Properties.DataSource = DBHelper.Persons;
            lookUpEdit2.Properties.DataSource = DBHelper.Persons;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            lookUpEdit3.Properties.DataSource = DBHelper.Persons;

            if (_wbill_id == null && doc_id == null)
            {
                is_new_record = true;

                wb = _db.WaybillList.Add(new WaybillList()
                {
                    Id = Guid.NewGuid(),
                    WType = _wtype,
                    DefNum = 1,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = "",
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    WaybillMove = new WaybillMove { SourceWid = DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId },
                    Nds = 0,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId
                });

                _db.SaveChanges();

                _wbill_id = wb.WbillId;
                CommissionBS.DataSource = _db.Commission.Add(new Commission { WbillId = _wbill_id.Value, KaId = DBHelper.CurrentUser.KaId });
                _db.SaveChanges();

            }
            else
            {
                wb = _db.WaybillList.FirstOrDefault(f => f.Id == doc_id || f.WbillId == _wbill_id);
                _wbill_id = wb.WbillId;
            }

            if (wb != null && wb.WaybillMove != null)
            {
                DBHelper.UpdateSessionWaybill(wb.WbillId);

                if (is_new_record)
                {
                    wb.Num = new BaseEntities().GetDocNum("wb_write_off").FirstOrDefault();
                }

                wb.WaybillMove = _db.WaybillMove.Find(_wbill_id);

                var com = _db.Commission.FirstOrDefault(w => w.WbillId == _wbill_id);
                if (com == null)
                {
                    com = _db.Commission.Add(new Commission { WbillId = _wbill_id.Value, KaId = DBHelper.CurrentUser.KaId });
                }

                CommissionBS.DataSource = com;

                TurnDocCheckBox.EditValue = wb.Checked;

                WhOutComboBox.DataBindings.Add(new Binding("EditValue", wb.WaybillMove, "SourceWid", false, DataSourceUpdateMode.OnPropertyChanged));

                NumEdit.DataBindings.Add(new Binding("EditValue", wb, "Num"));
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", wb, "OnDate" , false, DataSourceUpdateMode.OnPropertyChanged));

                NotesEdit.DataBindings.Add(new Binding("EditValue", wb, "Notes"));
                ReasonEdit.DataBindings.Add(new Binding("EditValue", wb, "Reason"));
                DefNumCheckBox.DataBindings.Add(new Binding("EditValue", wb, "DefNum"));
            }

            RefreshDet();
        }

    /*    private void UpdLockWB()
        {
            if (wb != null)
            {
                _db.Entry<WaybillList>(wb).State = EntityState.Detached;
            }

            if (_wbill_id == null && doc_id != null)
            {
                _wbill_id = _db.WaybillList.AsNoTracking().FirstOrDefault(f => f.Id == doc_id).WbillId;
            }

            wb = _db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK, NOWAIT) where WbillId = {0} ", _wbill_id).FirstOrDefault();

            if (wb != null)
            {
                _db.Entry<WaybillList>(wb).State = EntityState.Modified;

                wb.WaybillMove = _db.WaybillMove.Find(_wbill_id);

                var com = _db.Commission.FirstOrDefault(w => w.WbillId == _wbill_id); 
                if(com == null)
                {
                    com = _db.Commission.Add(new Commission { WbillId = _wbill_id.Value, KaId = DBHelper.CurrentUser.KaId });
                }

                CommissionBS.DataSource = com;
            }
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
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && OnDateDBEdit.EditValue != null && WaybillDetOutBS.Count > 0);

            if (recult && TurnDocCheckBox.Checked)
            {
                recult = !wbd_list.Any(w => w.Rsv == 0 && w.PosType == 0);
            }

            WhOutComboBox.Enabled = (WaybillDetOutBS.Count == 0);
            WhBtn.Enabled = WhOutComboBox.Enabled;

            EditMaterialBtn.Enabled = WaybillDetOutBS.Count > 0;
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;

            OkButton.Enabled = recult;
            return recult;
        }

        private void frmWBWriteOff_Shown(object sender, EventArgs e)
        {
            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            NowDateBtn.Enabled = OnDateDBEdit.Enabled;

            WaybillDetOutGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

            /*     PersonComboBox.Enabled = !String.IsNullOrEmpty(user_settings.AccessEditPersonId) && Convert.ToInt32(user_settings.AccessEditPersonId) == 1;
                 PersonEditBtn.Enabled = PersonComboBox.Enabled;*/
        }

        private void frmWBWriteOff_FormClosed(object sender, FormClosedEventArgs e)
        {
            DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

            if (is_new_record)
            {
                _db.DeleteWhere<WaybillList>(w => w.WbillId == _wbill_id);
            }

            _db.Dispose();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            wb.UpdatedAt = DateTime.Now;
            _db.SaveChanges();

            if (!DBHelper.CheckInDate(wb, _db, OnDateDBEdit.DateTime))
            {
                return;
            }

     //       current_transaction.Commit();

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
                using (var frm = new frmWriteOffDet(_db, dr.PosId, wb))
                {
                    frm.ShowDialog();
                }
                RefreshDet();

            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmWriteOffDet(_db, null, wb))
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

                RefreshDet();
            }
        }

        private void RsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var r = new ObjectParameter("RSV", typeof(Int32));

            _db.ReservedPosition(focused_dr.PosId, r, DBHelper.CurrentUser.UserId);

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
           //     current_transaction = current_transaction.CommitRetaining(_db);
          //      UpdLockWB();
                focused_dr.Rsv = 0;
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
   //         current_transaction = current_transaction.CommitRetaining(_db);
    //        UpdLockWB();

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

        private void NowDateBtn_Click(object sender, EventArgs e)
        {
            OnDateDBEdit.EditValue = DBHelper.ServerDateTime();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WhOutComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (wb != null && wb.WaybillMove != null && WhOutComboBox.EditValue != null && WhOutComboBox.EditValue != DBNull.Value)
            {
                wb.WaybillMove.SourceWid = (int)WhOutComboBox.EditValue;
            }
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

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(focused_dr.MatId, _db);
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            IHelper.ShowMatListByWH3(_db, wb, WhOutComboBox.EditValue.ToString());
            RefreshDet();
        }

        private void WhBtn_Click(object sender, EventArgs e)
        {
            WhOutComboBox.EditValue = IHelper.ShowDirectList(WhOutComboBox.EditValue, 2);
        }

        private void lookUpEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                ((LookUpEdit)sender).EditValue = IHelper.ShowDirectList(((LookUpEdit)sender).EditValue, 3);
            }
        }

        private void frmWBWriteOff_FormClosing(object sender, FormClosingEventArgs e)
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
