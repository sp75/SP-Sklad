﻿using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;
using SP_Sklad.Common;
using SP_Sklad.Reports;
using SkladEngine.ModelViews;
using SP_Sklad.Properties;
using System.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace SP_Sklad.WBForm
{
    public partial class frmWBReturnIn : DevExpress.XtraEditors.XtraForm
    {
        private int _wtype { get; set; }
        BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        private WaybillList wb { get; set; }
        public bool is_new_record { get; set; }
        private v_WayBillReturnCustomerDet focused_dr => WBDetReInGridView.GetFocusedRow() as v_WayBillReturnCustomerDet;
        
        private UserSettingsRepository user_settings { get; set; }

        public frmWBReturnIn(int wtype, int? wbill_id)
        {
            is_new_record = false;
            _wtype = wtype;
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();
        }

        private void frmWBReturnIn_Load(object sender, EventArgs e)
        {
            KagentComboBox.Properties.DataSource = DBHelper.KagentsWorkerList;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            WHComboBox.Properties.DataSource = DBHelper.WhList;
           

            if (_wbill_id == null)
            {
                is_new_record = true;

                wb = _db.WaybillList.Add(new WaybillList()
                {
                    Id = Guid.NewGuid(),
                    WType = _wtype,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = DB.SkladBase().GetDocNum("wb(6)").FirstOrDefault(),
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.CurrentEnterprise.KaId,
                    ReportingDate = DBHelper.ServerDateTime(),
                    PTypeId = 1
                });
                _db.SaveChanges();

                _wbill_id = wb.WbillId;
            }
            else
            {
                wb = _db.WaybillList.FirstOrDefault(f => f.WbillId == _wbill_id);
            }

            if (wb != null)
            {
                if(wb.PersonId == null)
                {
                    wb.PersonId = DBHelper.CurrentUser.KaId;
                }

                DBHelper.UpdateSessionWaybill(wb.WbillId);

                OutDateEdit.DateTime = new[] { wb.OnDate.Date.AddDays(-3), DateTime.Now.Date.AddDays(-3) }.Min();

                TurnDocCheckBox.EditValue = wb.Checked;

                KagentComboBox.DataBindings.Add(new Binding("EditValue", wb, "KaId", true, DataSourceUpdateMode.OnPropertyChanged));
                PersonComboBox.DataBindings.Add(new Binding("EditValue", wb, "PersonId", true, DataSourceUpdateMode.OnPropertyChanged));
                NumEdit.DataBindings.Add(new Binding("EditValue", wb, "Num"));
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", wb, "OnDate"));
                NotesEdit.DataBindings.Add(new Binding("EditValue", wb, "Notes"));
                ReasonEdit.DataBindings.Add(new Binding("EditValue", wb, "Reason"));
                ReportingDateEdit.DataBindings.Add(new Binding("EditValue", wb, "ReportingDate", true, DataSourceUpdateMode.OnPropertyChanged));

                payDocUserControl1.OnLoad(_db, wb);
                KagentComboBox.Enabled = !payDocUserControl1.IsPayDoc();
            }

            RefreshDet();
        }

   /*     private void UpdLockWB()
        {
            wb = _db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK, NOWAIT) where WbillId = {0}", _wbill_id).FirstOrDefault();
        }*/

        private void RefreshDet()
        {
            int top_row = WBDetReInGridView.TopRowIndex;
            WaybillDetInBS.DataSource = _db.v_WayBillReturnCustomerDet.AsNoTracking().Where(w=> w.WbillId == _wbill_id).ToList();
            WBDetReInGridView.TopRowIndex = top_row;

            GetOk();
        }

        bool GetOk()
        {
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && KagentComboBox.EditValue != null && OnDateDBEdit.EditValue != null && WaybillDetInBS.Count > 0);

            AddMaterialBtn.Enabled = KagentComboBox.EditValue != DBNull.Value;
            TMCBtnItem.Enabled = KagentComboBox.EditValue != DBNull.Value;
            UsedProcuctBtn.Enabled = KagentComboBox.EditValue != DBNull.Value;


            EditMaterialBtn.Enabled = WaybillDetInBS.Count > 0;
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;
            OrdInfoBtn.Enabled = EditMaterialBtn.Enabled;

            KagentComboBox.Enabled = WaybillDetInBS.Count == 0;
            KagBalBtn.Enabled = KagentComboBox.EditValue != DBNull.Value;

            OkButton.Enabled = recult;
            return recult;
        }

        private void frmWBReturnIn_FormClosed(object sender, FormClosedEventArgs e)
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
            if (!DBHelper.CheckOutDate(wb, _db, OnDateDBEdit.DateTime))
            {
                return;
            }

            var ch = _db.Database.SqlQuery<String>(@"select materials.Name
		           from waybilldet wbd
                   inner join materials on wbd.MatId = materials.MatId 
         		   cross apply (select  sum(wbd_r.amount) ReturnAmount from waybilldet wbd_r ,RETURNREL rr  where wbd_r.posid = rr.posid and rr.outposid = wbd.posid  ) returnRel
                  where  wbd.PosId  in (select  rr.OutPosId from waybilldet wbd_r ,RETURNREL rr  where wbd_r.posid = rr.posid  and wbd_r.WbillId = @p0  ) and  (wbd.Amount -  returnRel.ReturnAmount)  < 0", wb.WbillId).ToList();
            if (ch.Any())
            {
                MessageBox.Show("Товар вже повернуто: " + String.Join(",", ch));
                return;
            }

          //  var SummAll = (_db.WaybillDet.Where(w => w.WbillId == _wbill_id).Sum(s => s.Total) ?? 0) + (_db.WayBillTmc.Where(w => w.WbillId == _wbill_id).Sum(s => s.Total) ?? 0);
      //      wb.UpdatedAt = DateTime.Now;
      //      wb.SummAll = SummAll;
        //    wb.SummInCurr = SummAll * wb.OnValue;

            _db.SaveChanges();

            payDocUserControl1.Execute(wb.WbillId);

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

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var df = new frmWBReturnDetIn(_db, null, wb, (int?)WHComboBox.EditValue, OutDateEdit.DateTime))
            {
                if (df.ShowDialog() == DialogResult.OK)
                {
                    RefreshDet();
                }
            }
        }

        private void OnDateDBEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!OnDateDBEdit.ContainsFocus) return;

            wb.OnDate = OnDateDBEdit.DateTime;

            GetOk();
        }

        private void KagentComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (!KagentComboBox.ContainsFocus || KagentComboBox.EditValue == null || KagentComboBox.EditValue == DBNull.Value) return;


            wb.KaId = (int)KagentComboBox.EditValue;

            GetOk();
        }

        private void frmWBReturnIn_Shown(object sender, EventArgs e)
        {
            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            PersonComboBox.Enabled = !String.IsNullOrEmpty(user_settings.AccessEditPersonId) && Convert.ToInt32(user_settings.AccessEditPersonId) == 1;
            WBDetReInGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

            GetOk();
        }

        private void NumEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!NumEdit.ContainsFocus) return;

            GetOk();
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        
            if (focused_dr != null)
            {
                if (focused_dr.PosType == 0)
                {
                    using (var df = new frmWBReturnDetIn(_db, focused_dr.PosId, wb, (int?)WHComboBox.EditValue, focused_dr.OnDate.Value))
                    {
                        if (df.ShowDialog() == DialogResult.OK)
                        {
                            RefreshDet();
                        }
                    }
                }

                if (focused_dr.PosType == 1) // Товар під утилізацію
                {
                    using (var df = new frmWBReturnDetIn(_db, focused_dr.PosId, wb, DBHelper.WhList.FirstOrDefault(w => w.RecyclingWarehouse == 1)?.WId, focused_dr.OnDate.Value,1))
                    {
                        if (df.ShowDialog() == DialogResult.OK)
                        {
                            RefreshDet();
                        }
                    }
                }


                if (focused_dr.PosType == 2) //ТМС
                {
                    using (var df = new frmWayBillTMCDet(_db, focused_dr.PosId, wb))
                    {
                        if (df.ShowDialog() == DialogResult.OK)
                        {
                            RefreshDet();
                        }
                    }
                }
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_dr != null)
            {
                if (focused_dr.PosType == 0 || focused_dr.PosType == 1)
                {
                    _db.DeleteWhere<WaybillDet>(w => w.PosId == focused_dr.PosId);
                }

                if (focused_dr.PosType == 2)
                {
                    _db.DeleteWhere<WayBillTmc>(w => w.PosId == focused_dr.PosId);
                }

                RefreshDet();
            }
        }

        private void WBDetReInGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            PrintDoc.Show(wb.Id, wb.WType, _db);
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(focused_dr.MatId, _db);
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(focused_dr.MatId);
        }

        private void KagBalBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowKABalans((int)KagentComboBox.EditValue);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowOrdered((int)KagentComboBox.EditValue, -16, 0);
        }


        private void PersonComboBox_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                PersonComboBox.EditValue = IHelper.ShowDirectList(PersonComboBox.EditValue, 3);
            }
        }

        private void WHComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                WHComboBox.EditValue = IHelper.ShowDirectList(WHComboBox.EditValue, 2);

                if (WHComboBox.EditValue != null && WHComboBox.EditValue != DBNull.Value)
                {
                    UpdateWh((int)WHComboBox.EditValue);
                }
            }
        }

        private void KagentComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                KagentComboBox.EditValue = IHelper.ShowDirectList(KagentComboBox.EditValue, 1);
            }
        }

        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                OnDateDBEdit.DateTime = DBHelper.ServerDateTime();
                wb.OnDate = OnDateDBEdit.DateTime;
            }
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var df = new frmWayBillTMCDet(_db, null, wb))
            {
                if (df.ShowDialog() == DialogResult.OK)
                {
                    RefreshDet();
                }
            }
        }

        private void frmWBReturnIn_FormClosing(object sender, FormClosingEventArgs e)
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

        private void BarCodeEdit_KeyDown(object sender, KeyEventArgs e)
        {
            var textEdit = sender as TextEdit;

            if (e.KeyCode == Keys.Enter && AddMaterialBtn.Enabled && !string.IsNullOrEmpty(textEdit.Text))
            {
                var BarCodeText = textEdit.Text.Split('+');
                string kod = BarCodeText[0];
               // var item = _db.Materials.Where(w => w.BarCode == kod).Select(s => s.MatId).FirstOrDefault();

                var bc = _db.v_BarCodes.FirstOrDefault(w => w.BarCode == kod);
                if (bc != null)
                {
                    using (var frm = new frmOutMatList(_db, OutDateEdit.DateTime, wb.OnDate, bc.MatId, wb.KaId.Value, -1))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            var mat_row = frm.bandedGridView1.GetFocusedRow() as GetPosOutView;
                            if (mat_row != null)
                            {
                                using (var df = new frmWBReturnDetIn(_db, null, wb, (int?)WHComboBox.EditValue, OutDateEdit.DateTime)
                                {
                                    pos_out_list = frm.pos_out_list,
                                    outPosId = mat_row.PosId
                                })
                                {
                                    if (df.ShowDialog() == DialogResult.OK)
                                    {
                                        RefreshDet();
                                    }
                                }
                            }
                        }
                    }
                }

                barEditItem1.EditValue = "";
                e.Handled = true;
                barEditItem1.Links[0].Focus();
            }
        }

        private void WBDetReInGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.WbDetPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem1_ItemClick_2(object sender, ItemClickEventArgs e)
        {
            using (var df = new frmWBReturnDetIn(_db, null, wb, DBHelper.WhList.FirstOrDefault(w => w.RecyclingWarehouse == 1)?.WId, OutDateEdit.DateTime, 1))
            {
                if (df.ShowDialog() == DialogResult.OK)
                {
                    RefreshDet();
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            var wbd = _db.WaybillDet.Find(focused_dr.PosId);

            wbd.Defective = 1;
            wbd.WId = DBHelper.WhList.FirstOrDefault(w => w.RecyclingWarehouse == 1)?.WId;

            _db.SaveChanges();

            RefreshDet();

        }

        private void WbDetPopupMenu_BeforePopup(object sender, System.ComponentModel.CancelEventArgs e)
        {
            barButtonItem2.Enabled = focused_dr.PosType == 0 && DBHelper.WhList.Any(w => w.RecyclingWarehouse == 1);
        }

        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (!WHComboBox.ContainsFocus)
            {
                return;
            }

            UpdateWh((int?)WHComboBox.EditValue);
        }

        private void UpdateWh(int? wid)
        {
            if (WaybillDetInBS.Count > 0 && wid.HasValue && wid > 0)
            {
                if (MessageBox.Show("Оприходувати весь товар на склад <" + WHComboBox.Text + ">?", "Інформація", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    foreach (var item in _db.WaybillDet.Where(w => w.WbillId == _wbill_id))
                    {
                        item.WId = wid;

                        foreach (var turn in _db.WMatTurn.Where(w => w.SourceId == item.PosId))
                        {
                            turn.WId = wid.Value;
                        }
                    }
                    _db.Save(wb.WbillId);
                    RefreshDet();
                }
            }
        }
    }
}
