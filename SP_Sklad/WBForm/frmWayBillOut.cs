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
using SP_Sklad.EditForm;
using SP_Sklad.Reports;

namespace SP_Sklad.WBForm
{
    public partial class frmWayBillOut : DevExpress.XtraEditors.XtraForm
    {
        private int _wtype { get; set; }
        public BaseEntities _db { get; set; }
        public int? _wbill_id { get; set; }
        public int? doc_id { get; set; }
  //      private DbContextTransaction current_transaction { get; set; }
        private WaybillList wb { get; set; }
        public bool is_new_record { get; set; }

        private GetWayBillDetOut_Result wbd_row
        {
            get
            {
                return  WaybillDetOutGridView.GetFocusedRow() as GetWayBillDetOut_Result;
            }
        }

        private List<GetWayBillDetOut_Result> wbd_list { get; set; }

        public frmWayBillOut(int wtype, int? wbill_id)
        {
            is_new_record = false;
            _wtype = wtype;
            _wbill_id = wbill_id;
            _db = new BaseEntities();
        //    current_transaction = _db.Database.BeginTransaction(/*IsolationLevel.RepeatableRead*/);
            InitializeComponent();
        }

        private void frmWayBillOut_Load(object sender, EventArgs e)
        {
            KagentComboBox.Properties.DataSource = DBHelper.Kagents;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            repositoryItemComboBox1.Items.AddRange(_db.WaybillDet.Where(w=> w.Notes != null).Select(s => s.Notes).Distinct().ToList());

            if (_wbill_id == null && doc_id == null)
            {
                is_new_record = true;

                wb = _db.WaybillList.Add(new WaybillList()
                {
                    WType = _wtype,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = new BaseEntities().GetDocNum("wb_out").FirstOrDefault(),
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    EntId = DBHelper.Enterprise.KaId,
                    Docs = new Docs { DocType = _wtype },
                    UpdatedBy = DBHelper.CurrentUser.UserId
                });

                _db.SaveChanges();
            }
            else
            {
                wb = _db.WaybillList.FirstOrDefault(f => f.DocId == doc_id || f.WbillId == _wbill_id);
                //  UpdLockWB();
            }

            if (wb != null)
            {
                _wbill_id = wb.WbillId;

                DBHelper.UpdateSessionWaybill(wb.WbillId);

                WaybillListBS.DataSource = wb;
            //    wb.UpdatedBy = DBHelper.CurrentUser.UserId;
                checkEdit2.Checked = (wb.ToDate != null);

               payDocUserControl1.OnLoad(_db, wb);
            }

            RefreshDet();
        }

     /*   private void UpdLockWB()
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
          
            if (_db.Entry<WaybillList>(wb).State == EntityState.Detached)
            {
                _db.WaybillList.Attach(wb);
            }

            _db.Entry<WaybillList>(wb).State = EntityState.Modified;
            _db.Entry<WaybillList>(wb).Property(f => f.SummPay).IsModified = false;
        }*/

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //try
          //  {
                using (var df = new frmWayBillDetOut(_db, null, wb))
                {
                    if (df.ShowDialog() == DialogResult.OK)
                    {
                        _db.SaveChanges();
                  //      current_transaction = current_transaction.CommitRetaining(_db);
                  //      UpdLockWB();
                        RefreshDet();
                        WaybillDetOutGridView.MoveLastVisible();
                    }
                }
        //    }
        //    catch { }
        }

        private void frmWayBillOut_Shown(object sender, EventArgs e)
        {
            if (_wtype == -1) Text = "Властивості видаткової накладної, Продавець: " + DBHelper.Enterprise.Name;
            if (_wtype == 2) Text = "Властивості рахунка, Продавець: " + DBHelper.Enterprise.Name;
            if (_wtype == -16) Text = "Замовлення від клієнтів, Продавець: " + DBHelper.Enterprise.Name;
            checkEdit2.Visible = (_wtype == 2 || _wtype == -16);
            ToDateEdit.Visible = checkEdit2.Visible;
            TurnDocCheckBox.Enabled = !checkEdit2.Visible;

            ProcurationBtn.Enabled = (_wtype == -1);
          
            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            NowDateBtn.Enabled = OnDateDBEdit.Enabled;

            PersonComboBox.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            PersonEditBtn.Enabled = PersonComboBox.Enabled;

            if (TurnDocCheckBox.Checked) Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (TurnDocCheckBox.Checked && !DBHelper.CheckOrderedInSuppliers(wb.WbillId, _db)) return;

            if (!DBHelper.CheckInDate(wb, _db, OnDateDBEdit.DateTime))
            {
                return;
            }

            wb.UpdatedAt = DateTime.Now;
            _db.Save(wb.WbillId);

            payDocUserControl1.Execute(wb.WbillId);

            if (TurnDocCheckBox.Checked)
            {
                var ex_wb = _db.ExecuteWayBill(wb.WbillId, null).ToList();
            }

            is_new_record = false;

            Close();
        }

        private void RefreshDet()
        {
             wbd_list = _db.GetWayBillDetOut(_wbill_id).AsNoTracking().ToList();

            int top_row = WaybillDetOutGridView.TopRowIndex;
            WaybillDetOutBS.DataSource = wbd_list;
            WaybillDetOutGridView.TopRowIndex = top_row;

            GetOk();
        }

        bool GetOk()
        {
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && KagentComboBox.EditValue != null && OnDateDBEdit.EditValue != null && wbd_list != null && wbd_list.Any());

            if (recult && wb.WType == -1 && TurnDocCheckBox.Checked)
            {
                recult = !wbd_list.Any(w => w.Rsv == 0 && w.PosType == 0 && w.Total > 0);
            }

            barSubItem1.Enabled = KagentComboBox.EditValue != null && KagentComboBox.EditValue != DBNull.Value;

            EditMaterialBtn.Enabled = (wbd_list != null && wbd_list.Any());
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;

            OkButton.Enabled = recult;
            return recult;
        }

        private void frmWayBillOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

            if (is_new_record)
            {
                _db.DeleteWhere<WaybillList>(w => w.WbillId == _wbill_id);
                _db.SaveChanges();
                //   current_transaction.Commit();

            }

      /*      if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }*/

            _db.Dispose();
         //   current_transaction.Dispose();
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row != null)
            {
                if (wbd_row.PosId > 0)
                {
                    _db.DeleteWhere<WaybillDet>(w => w.PosId == wbd_row.PosId);
                }
                else
                {
                    _db.DeleteWhere<WayBillSvc>(w => w.PosId == wbd_row.PosId * -1);
                }
                _db.SaveChanges();

                WaybillDetOutGridView.DeleteSelectedRows();
                GetOk();
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
            wb.KaId = row.KaId;

            wb.Nds = row.NdsPayer == 1 ? DBHelper.CommonParam.Nds : 0;

            GetOk();
        }

        private void WaybillDetInGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row != null)
            {
                if (wbd_row.PosId > 0)
                {
                    using (var df = new frmWayBillDetOut(_db, wbd_row.PosId, wb))
                    {
                        df.ShowDialog();
                  //      current_transaction = current_transaction.CommitRetaining(_db);
            //            UpdLockWB();
                    }
                    
                }
                else
                {
                    new frmWaybillSvcDet(_db, wbd_row.PosId * -1, wb).ShowDialog();
                }

                RefreshDet();
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
      //      _db.SaveChanges();
        //    current_transaction = current_transaction.CommitRetaining(_db);
      //      UpdLockWB();

            if (r.Value != null)
            {
                wbd_row.Rsv = (int)r.Value;
                WaybillDetOutGridView.RefreshRow(WaybillDetOutGridView.FocusedRowHandle);
            }

            GetOk();
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
            var res = _db.ReservedAllPosition(wb.WbillId);

            if (res.Any())
            {
                MessageBox.Show("Не вдалося зарезервувати деякі товари!");
            }
       //     current_transaction = current_transaction.CommitRetaining(_db);
      //      UpdLockWB();
           
            RefreshDet();
        }

        private void DelRsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row.Rsv == 1 && wbd_row.PosId > 0)
            {
                _db.DeleteWhere<WMatTurn>(w => w.SourceId == wbd_row.PosId);
            //    _db.SaveChanges();
              //  current_transaction = current_transaction.CommitRetaining(_db);
       //         UpdLockWB();
                wbd_row.Rsv = 0;
                WaybillDetOutGridView.RefreshRow(WaybillDetOutGridView.FocusedRowHandle);
            }

            GetOk();
        }

        private void DelAllRsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.DeleteAllReservePosition(wb.WbillId);
       //     current_transaction = current_transaction.CommitRetaining(_db);
  //          UpdLockWB();
         
            RefreshDet();
        }

        private void MarkBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wbd = _db.WaybillDet.Find(wbd_row.PosId);
            if (wbd == null)
            {
                return;
            }

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
            _db.SaveChanges();
            WaybillDetOutGridView.RefreshRow(WaybillDetOutGridView.FocusedRowHandle);
        }

        private void NumEdit_Validated(object sender, EventArgs e)
        {
            GetOk();
        }

        private void NowDateBtn_Click(object sender, EventArgs e)
        {
            wb.OnDate = DBHelper.ServerDateTime();
            OnDateDBEdit.DateTime = wb.OnDate;
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            wb.Kontragent = _db.Kagent.Find(wb.KaId);

            if (wb.WType == -16)
            {
                IHelper.ShowMatList(_db, wb);
            }
            else
            {
                IHelper.ShowMatListByWH(_db, wb);
            }

         //   if (MessageDlg("Зарезервувати товар ? ", mtConfirmation, TMsgDlgButtons() << mbYes << mbNo, 0) == mrYes) RSVAllBarBtn->Click();
            RefreshDet();

            WaybillDetOutGridView.MoveLastVisible();
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row != null)
            {
                IHelper.ShowMatRSV(wbd_row.MatId, _db);
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var df = new frmWaybillSvcDet(_db, null, wb);
            if (df.ShowDialog() == DialogResult.OK)
            {
                RefreshDet();
            }
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintDoc.Show(wb.Docs.DocId, wb.WType, _db);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowOrdered((int)KagentComboBox.EditValue, -16, 0);
        }

        private void WaybillDetOutGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var wbd = _db.WaybillDet.Find(wbd_row.PosId);
            if (e.Column.FieldName == "Amount")
            {
                if (wbd_row.Rsv == 0)
                {
                    wbd.Amount = Convert.ToDecimal(e.Value);
                    wbd.Checked = 1;
                }
                else
                {
                    wbd_row.Amount = wbd.Amount;
                }
            }
            else if (e.Column.FieldName == "Notes")
            {
                wbd.Notes = Convert.ToString(e.Value);
            }

            _db.SaveChanges();
            RefreshDet();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
          //  wb.KaId = (int)IHelper.ShowDirectList(KagentComboBox.EditValue, 1);
            KagentComboBox.EditValue = IHelper.ShowDirectList(KagentComboBox.EditValue, 1);
        }

        private void KagBalBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KagentComboBox.EditValue == DBNull.Value) return;
            IHelper.ShowKABalans((int)KagentComboBox.EditValue);
        }

        private void ToDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!ToDateEdit.ContainsFocus)
            {
                return;
            }

            checkEdit2.Checked = (ToDateEdit.EditValue != DBNull.Value);
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkEdit2.ContainsFocus)
            {
                return;
            }

            if (checkEdit2.Checked)
            {
                ToDateEdit.EditValue = OnDateDBEdit.DateTime.AddDays(3);
                wb.ToDate = OnDateDBEdit.DateTime.AddDays(3);
            }
            else
            {
                wb.ToDate = null;
                ToDateEdit.EditValue = null;
            }

        }

        private void ProcurationBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var f = new frmAttEdit(wb))
            {
                f.ShowDialog();
            }
        }

        private void PersonEditBtn_Click(object sender, EventArgs e)
        {
            PersonComboBox.EditValue = IHelper.ShowDirectList(PersonComboBox.EditValue, 3);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wbd_list = _db.WaybillDet.Where(w => w.WbillId == _wbill_id && w.Checked != 1);
            if (wbd_list != null)
            {
                _db.WaybillDet.RemoveRange(wbd_list);
            }
          //  _db.SaveChanges();
            RefreshDet();
        }

        private void WeighBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
  
            var frm = new frmMatListEdit(wbd_row.MatName);
            frm.PriceEdit.EditValue = wbd_row.Price;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                var wbd = _db.WaybillDet.Find(wbd_row.PosId);

                if (wbd_row.Rsv == 0)
                {
                    wbd.Amount = frm.AmountEdit.Value;
                    wbd.Checked = 1;
                }
                _db.SaveChanges();

                RefreshDet();
            }
        }
    }
}
