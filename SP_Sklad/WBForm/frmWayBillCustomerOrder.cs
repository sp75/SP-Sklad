using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Entity.Core.Objects;
using DevExpress.XtraGrid;
using SP_Sklad.Common;
using SP_Sklad.EditForm;
using SP_Sklad.Reports;
using SP_Sklad.ViewsForm;
using SP_Sklad.Properties;
using DevExpress.Data;
using System.IO;

namespace SP_Sklad.WBForm
{

    public partial class frmWayBillCustomerOrder : DevExpress.XtraEditors.XtraForm
    {
        private int _wtype { get; set; }
        public BaseEntities _db { get; set; }
        public int? _wbill_id { get; set; }
        public Guid? doc_id { get; set; }
        private WaybillList wb { get; set; }
        public bool is_new_record { get; set; }
        private bool is_update_record { get; set; }
        private v_WayBillOrdersInDet wbd_row
        {
            get
            {
                return  WaybillDetOutGridView.GetFocusedRow() as v_WayBillOrdersInDet;
            }
        }
        private List<v_WayBillOrdersInDet> wbd_list { get; set; }
        private UserSettingsRepository user_settings { get; set; }
        private DiscCards disc_card { get; set; }
        private List<WaybillDet> tmp_WaybillDet { get; set; }
        private List<WaybillDet> tmp_rsv_WaybillDet { get; set; }
        private WaybillList tmp_wb { get; set; }

        public frmWayBillCustomerOrder(int wtype, int? wbill_id)
        {
            is_new_record = false;
            _wtype = wtype;
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);
            InitializeComponent();
        }

        Stream wb_det_layout_stream = new MemoryStream();

        private void frmWayBillOut_Load(object sender, EventArgs e)
        {
            WaybillDetOutGridView.SaveLayoutToStream(wb_det_layout_stream);
            WaybillDetOutGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + "frmWayBillCustomerOrder\\WaybillDetOutGridView");

            KagentComboBox.Properties.DataSource = DBHelper.KagentsWorkerList;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            repositoryItemComboBox1.Items.AddRange(DBHelper.Packaging.Select(s => s.Name).ToList());

            if (_wbill_id == null && doc_id == null)
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
                    EntId = DBHelper.Enterprise.KaId,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    ShipmentDate = DBHelper.ServerDateTime().Date.AddHours(8),
                    PTypeId = 1,
                    Nds = DBHelper.Enterprise.NdsPayer == 1 ? DBHelper.CommonParam.Nds : 0,
                });

                _db.SaveChanges();
            }
            else
            {
                wb = _db.WaybillList.FirstOrDefault(f => f.Id == doc_id || f.WbillId == _wbill_id);
            }

            if (wb != null)
            {
                _wbill_id = wb.WbillId;

                DBHelper.UpdateSessionWaybill(wb.WbillId);

                if (is_new_record)
                {
                    wb.Num = new BaseEntities().GetDocNum("wb(-16)").FirstOrDefault();
                }
                else
                {
                    tmp_wb = _db.WaybillList.Where(w => w.WbillId == wb.WbillId).AsNoTracking().FirstOrDefault();
                    tmp_WaybillDet = _db.WaybillDet.Where(w => w.WbillId == wb.WbillId).AsNoTracking().ToList();
                    tmp_rsv_WaybillDet = _db.WaybillDet.Where(w => w.WbillId == wb.WbillId && w.WMatTurn1.Any()).AsNoTracking().ToList();
                }

                WaybillListBS.DataSource = wb;
                checkEdit2.Checked = (wb.ToDate != null);

                payDocUserControl1.OnLoad(_db, wb);
                KagentComboBox.Enabled = !payDocUserControl1.IsPayDoc();
            }

            RefreshDet();

            is_update_record = false;
        }

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var df = new frmWayBillDetOut(_db, null, wb, disc_card))
            {
                if (df.ShowDialog() == DialogResult.OK)
                {
                    _db.SaveChanges();

                    RefreshDet();
                    WaybillDetOutGridView.MoveLastVisible();
                }
            }
        }

        private void frmWayBillOut_Shown(object sender, EventArgs e)
        {
            SetFormCaption();

            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            PersonComboBox.Enabled = !String.IsNullOrEmpty(user_settings.AccessEditPersonId) && Convert.ToInt32(user_settings.AccessEditPersonId) == 1;
            WaybillDetOutGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
            NumEdit.Enabled = user_settings.AccessEditDocNum;
        }

        private void SetFormCaption()
        {
            Text = $"Замовлення від клієнта, Продавець: {DBHelper.Enterprise.Name}";
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DBHelper.CheckInDate(wb, _db, OnDateDBEdit.DateTime))
            {
                return;
            }

            wb.UpdatedAt = DateTime.Now;

            _db.Save(wb.WbillId);

            payDocUserControl1.Execute(wb.WbillId);

            is_new_record = false;
            is_update_record = false;

            Close();
        }

        private void RefreshDet()
        {
             wbd_list = _db.v_WayBillOrdersInDet.Where(w=> w.WbillId ==_wbill_id).OrderBy(o=> o.Num).AsNoTracking().ToList();
            if(disc_card != null)
            {
                wbd_list.Add(new v_WayBillOrdersInDet { Discount = disc_card.OnValue, MatName = "Дисконтна картка", Num = wbd_list.Count() + 1, CardNum = disc_card.Num , PosType = 3});
            }

            int top_row = WaybillDetOutGridView.TopRowIndex;
            WaybillDetOutBS.DataSource = wbd_list;
            WaybillDetOutGridView.TopRowIndex = top_row;

            GetOk();
        }

        bool GetOk()
        {
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && KagentComboBox.EditValue != null && KagentComboBox.EditValue != DBNull.Value &&  OnDateDBEdit.EditValue != null && wbd_list != null && wbd_list.Any());

            barSubItem1.Enabled = KagentComboBox.EditValue != null && KagentComboBox.EditValue != DBNull.Value;

            EditMaterialBtn.Enabled = (wbd_list != null && wbd_list.Any());
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled ;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;

            payDocUserControl1.panelControl1.Enabled = recult;

            OkButton.Enabled = recult;
            return recult;
        }

        private void frmWayBillOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            WaybillDetOutGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + "frmWayBillCustomerOrder\\WaybillDetOutGridView");

            DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

            if (is_new_record)
            {
                _db.DeleteWhere<WaybillList>(w => w.WbillId == _wbill_id);
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
                if (wbd_row.PosType == 0)
                {
                    _db.DeleteWhere<WaybillDet>(w => w.PosId == wbd_row.PosId);
                }

                if (wbd_row.PosType == 1)
                {
                    _db.DeleteWhere<WayBillSvc>(w => w.PosId == wbd_row.PosId * -1);
                }

                if (wbd_row.PosType == 2)
                {
                    _db.DeleteWhere<WayBillTmc>(w => w.PosId == wbd_row.PosId);
                }

                if (wbd_row.PosType == 3)
                {
                    disc_card = null;

                    foreach (var item in _db.WaybillDet.Where(w => w.WbillId == wb.WbillId))
                    {
                        if (item.DiscountKind == 2)
                        {
                            var DiscountPrice = item.BasePrice;
                            item.Price = DiscountPrice;
                            item.Discount = 0;
                            item.DiscountKind = 0;
                            if (item.WayBillDetAddProps != null)
                            {
                                item.WayBillDetAddProps.CardId = null;
                            }
                            else 
                            {
                                _db.WayBillDetAddProps.Add(new WayBillDetAddProps
                                {
                                    CardId = null,
                                    PosId = item.PosId
                                });
                            }
                        }
                    }
                }

                _db.SaveChanges();

           //     WaybillDetOutGridView.DeleteSelectedRows();
                RefreshDet();
                GetOk();
            }
        }

        private void DeleteRsv(int? pos_id) 
        {
            _db.DeleteWhere<WMatTurn>(w => w.SourceId == pos_id);
        }

        private void KagentComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = KagentComboBox.GetSelectedDataRow() as GetKagentList_Result;
            if (row == null)
            {
                return;
            }
            wb.KaId = row.KaId;

            if (row.RouteId.HasValue)
            {
                var r = _db.Routes.FirstOrDefault(w => w.Id == row.RouteId.Value);

                wb.CarId = r.CarId;
                wb.RouteId = row.RouteId;
                wb.Received = r.Kagent1 != null ? r.Kagent1.Name : "";
                wb.DriverId = r.Kagent1 != null ? (int?)r.Kagent1.KaId : null;
            }

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
                if (wbd_row.PosType == 0)
                {
                    using (var df = new frmWayBillDetOut(_db, wbd_row.PosId, wb, disc_card))
                    {
                        df.ShowDialog();
                    }
                }

                if (wbd_row.PosType == 1)
                {
                    new frmWaybillSvcDet(_db, wbd_row.PosId * -1, wb).ShowDialog();
                }

                if (wbd_row.PosType == 2)
                {
                    using (var df = new frmWayBillTMCDet(_db, wbd_row.PosId, wb))
                    {
                        df.ShowDialog();
                    }
                }


                RefreshDet();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CancelUpdate()
        {
            if (is_new_record)
            {
                return;
            }

            wb.Num = tmp_wb.Num;
            wb.KaId = tmp_wb.KaId;
            wb.OnDate = tmp_wb.OnDate;
            wb.OnDate = tmp_wb.OnDate;
            wb.Notes = tmp_wb.Notes;
            wb.Reason = tmp_wb.Reason;
            wb.PersonId = tmp_wb.PersonId;
            wb.PTypeId = tmp_wb.PTypeId;
            wb.RouteId = tmp_wb.RouteId;
            wb.Received = tmp_wb.Received;
            wb.AttNum = tmp_wb.AttNum;
            wb.AttDate = tmp_wb.AttDate;
            wb.ShipmentDate = tmp_wb.ShipmentDate;

            if (tmp_WaybillDet.Any())
            {
                _db.DeleteWhere<WaybillDet>(w => w.WbillId == wb.WbillId);
                _db.WaybillDet.AddRange(tmp_WaybillDet);
            }

            _db.SaveChanges();

            foreach(var item in tmp_rsv_WaybillDet)
            {
                var r = new ObjectParameter("RSV", typeof(Int32));

                _db.ReservedPosition(item.PosId, r, DBHelper.CurrentUser.UserId);
            }

            is_update_record = false;
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
            _db.SaveChanges();

            var r = new ObjectParameter("RSV", typeof(Int32));

            _db.ReservedPosition(wbd_row.PosId, r, DBHelper.CurrentUser.UserId);

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
            WeighBtn.Enabled = (WaybillDetOutGridView.FocusedRowHandle >= 0) && DBHelper.WeighingScales_1 != null;
        }

        private void RsvAllBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();
            var list = new List<string>();

            var r = new ObjectParameter("RSV", typeof(Int32));

            var wb_list = _db.v_WayBillOrdersInDet.Where(w=> w.WbillId == _wbill_id).Where(w => w.Rsv != 1).ToList();
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

            /*       var res = _db.ReservedAllPosition(wb.WbillId, DBHelper.CurrentUser.UserId);

                   if (res.Any())
                   {
                       MessageBox.Show("Не вдалося зарезервувати деякі товари!");
                   }*/


            RefreshDet();
        }

        private void DelRsvBarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row.Rsv == 1 && wbd_row.PosId > 0)
            {
                _db.DeleteWhere<WMatTurn>(w => w.SourceId == wbd_row.PosId);
            
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
            if (wbd_row == null)
            {
                return;
            }

            if (wbd_row.PosType == 0)
            {
                var wbd = _db.WaybillDet.Find(wbd_row.PosId);
                if (wbd != null)
                {
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
                }
            }

            if (wbd_row.PosType == 2)
            {
                var wbt = _db.WayBillTmc.Find(wbd_row.PosId);
                if (wbt != null)
                {
                    if (wbt.Checked == 1)
                    {
                        wbt.Checked = 0;
                        wbd_row.Checked = 0;
                    }
                    else
                    {
                        wbt.Checked = 1;
                        wbd_row.Checked = 1;
                    }
                }
            }

            _db.SaveChanges();

            WaybillDetOutGridView.RefreshRow(WaybillDetOutGridView.FocusedRowHandle);
        }

        private void NumEdit_Validated(object sender, EventArgs e)
        {
            GetOk();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            wb.Kontragent = _db.Kagent.Find(wb.KaId);

            IHelper.ShowMatListByWH(_db, wb, disc_card);

            if (MessageBox.Show("Зарезервувати товар ? ", "Резервування", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                RsvAllBarBtn.PerformClick();
            }
            RefreshDet();

            WaybillDetOutGridView.MoveLastVisible();
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row != null && wbd_row.PosType  == 0)
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
            wb.UpdatedAt = DateTime.Now;
            PrintDoc.Show(wb.Id, wb.WType, _db);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(KagentComboBox.EditValue == DBNull.Value || KagentComboBox.EditValue == null)
            {
                return;
            }

            IHelper.ShowOrdered((int)KagentComboBox.EditValue, -16, 0);
        }

        private void WaybillDetOutGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var wbd = _db.WaybillDet.FirstOrDefault(w => w.PosId == wbd_row.PosId);
            if (e.Column.FieldName == "Amount")
            {
                if (wbd_row.Rsv == 0 && wbd_row.PosType == 0)
                {
                    wbd.Amount = Convert.ToDecimal(e.Value);
                    wbd.Checked = 1;
                }

                if (wbd_row.PosType == 2)
                {
                    var wbt = _db.WayBillTmc.FirstOrDefault(w => w.PosId == wbd_row.PosId);
                    wbt.Amount = Convert.ToDecimal(e.Value);
                    wbt.Checked = 1;
                }

            }
            else if (e.Column.FieldName == "Notes")
            {
                wbd.Notes = Convert.ToString(e.Value);
            }

            _db.SaveChanges();

           IHelper.MapProp(_db.v_WayBillOrdersInDet.Where(w=> w.WbillId ==  _wbill_id).AsNoTracking().FirstOrDefault(w => w.PosId == wbd_row.PosId), wbd_row);

            is_update_record = true;
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

         private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            using (var db = DB.SkladBase())
            {
                db.DeleteWhere<WaybillDet>(w => w.WbillId == _wbill_id && w.Checked != 1);
            }

            RefreshDet();
        }

        private void WeighBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (wbd_row == null || wbd_row.PosType != 0 || DBHelper.WeighingScales_1 == null)
            {
                return;
            }

            using (var frm = new frmWeightEdit(wbd_row.MatName))
            {
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

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmSetDiscountCard(_db, wb))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    disc_card = frm.cart;

                    if (disc_card?.KaId != null)
                    {
                        wb.KaId = disc_card.KaId;
                        KagentComboBox.EditValue = disc_card.KaId;
                    }

                    RefreshDet();
                }
            }
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row != null && wbd_row.PosType == 0)
            {
                IHelper.ShowMatInfo(wbd_row.MatId);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var df = new frmWayBillTMCDet(_db, null, wb);
            if (df.ShowDialog() == DialogResult.OK)
            {
                RefreshDet();
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; WaybillDetOutGridView.RowCount > i; i++)
            {
                var row = WaybillDetOutGridView.GetRow(i) as v_WayBillOrdersInDet;
                if (row.PosType == 0)
                {
                    var wbd = _db.WaybillDet.Find(row.PosId);
                    wbd.Num = i + 1;
                }
                if (row.PosType == 1)
                {
                    var wds = _db.WayBillSvc.Find(row.PosId);
                    wds.Num = i + 1;
                }

                if (row.PosType == 2)
                {
                    var wbt = _db.WayBillTmc.Find(row.PosId);
                    wbt.Num = i + 1;
                }
            }
            _db.SaveChanges();
            RefreshDet();
        }

        private void frmWayBillOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if ((is_new_record || _db.IsAnyChanges()) && OkButton.Enabled)
            if (is_new_record || is_update_record)
            {
                var m_recult = MessageBox.Show(Resources.save_wb, "Видаткова накладна №" + wb.Num, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (m_recult == DialogResult.Yes)
                {
                    if (OkButton.Enabled)
                    {
                        OkButton.PerformClick();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }

                if (m_recult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

                if (m_recult == DialogResult.No)
                {
                    CancelUpdate();
                }
            }
        }

        private void WaybillDetOutGridView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                var def_m = DBHelper.MeasuresList.FirstOrDefault(w=> w.Def == 1);

                GridSummaryItem item = e.Item as GridSummaryItem;

                if (item.FieldName == "MsrName")
                {
                    e.TotalValue = def_m.ShortName;
                }

                if (item.FieldName == "Amount")
                {
                    var amount_sum = _db.WaybillDet.Where(w => w.WbillId == _wbill_id && w.Materials.MId == def_m.MId).ToList().Sum(s => s.Amount);

                    var ext_sum = _db.WaybillDet.Where(w => w.WbillId == _wbill_id && w.Materials.MId != def_m.MId)
                        .Select(s => new { MaterialMeasures = s.Materials.MaterialMeasures.Where(f => f.MId == def_m.MId), s.Amount }).ToList()
                        .SelectMany(sm => sm.MaterialMeasures, (k, n) => new { k.Amount, MeasureAmount = n.Amount }).Sum(su => su.MeasureAmount * su.Amount);

                    e.TotalValue = Math.Round( amount_sum + ext_sum , 2);
                }
            }
        }

        private void KagentComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                KagentComboBox.ClosePopup();

                KagentComboBox.EditValue = IHelper.ShowDirectList(KagentComboBox.EditValue, 1);
            }
        }

        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                wb.OnDate = DBHelper.ServerDateTime();
                OnDateDBEdit.DateTime = wb.OnDate;

                _db.SaveChanges();
            }
        }

        private void PersonComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                PersonComboBox.EditValue = IHelper.ShowDirectList(PersonComboBox.EditValue, 3);
            }
        }

        private void WaybillDetOutGridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            var wh_row = WaybillDetOutGridView.GetRow(e.RowHandle) as v_WayBillOrdersInDet;

            if (wh_row != null && e.Column.FieldName == "Price")
            {
                if (wh_row.AvgInPrice > wh_row.Price)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*  using (var v_form = new frmWaybillTemplateView())
              {
                  if(v_form.ShowDialog() == DialogResult.OK)
                  {
                      _db.SaveChanges();
                      //  ExecuteDocument.ExecuteWaybillTemplate(v_form.waybillTemplateUserControl1.wbt_row.Id, wb, _db);
                      _db.CreateOrderByWBTemplate(wb.WbillId, v_form.waybillTemplateUserControl1.wbt_row.Id);

                      RefreshDet();
                  }
              }*/

            var result = IHelper.ShowDirectList(null, 18);
            if(result != null)
            {
                _db.SaveChanges();
                _db.CreateOrderByWBTemplate(wb.WbillId, (Guid)result);
                RefreshDet();
            }

        }


        private void WaybillListBS_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            is_update_record = true; 
        }

        private void WaybillDetOutBS_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            is_update_record = true;
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            wb_det_layout_stream.Seek(0, System.IO.SeekOrigin.Begin);

            WaybillDetOutGridView.RestoreLayoutFromStream(wb_det_layout_stream);
        }
    }
}
