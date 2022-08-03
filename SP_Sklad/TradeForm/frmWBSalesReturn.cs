using System;
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
using System.Collections.Generic;
using CheckboxIntegration.Models;
using CheckboxIntegration.Client;
using SP_Sklad.ViewsForm;

namespace SP_Sklad.WBForm
{
    public partial class frmWBSalesReturn : DevExpress.XtraEditors.XtraForm
    {
        private int _wtype { get; set; }
        BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        private WaybillList wb { get; set; }
        public bool is_new_record { get; set; }
        private GetWaybillDetIn_Result focused_dr
        {
            get { return WBDetReInGridView.GetFocusedRow() as GetWaybillDetIn_Result; }
        }
        private UserSettingsRepository user_settings { get; set; }

        public frmWBSalesReturn(int wtype, int? wbill_id)
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
            KagentComboBox.Properties.DataSource = DBHelper.TradingPoints;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            WHComboBox.Properties.DataSource = DBHelper.WhList;
            OutDateEdit.DateTime = DateTime.Now.Date.AddDays(-3);

            if (_wbill_id == null)
            {
                is_new_record = true;

                wb = _db.WaybillList.Add(new WaybillList()
                {
                    Id = Guid.NewGuid(),
                    WType = _wtype,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = DB.SkladBase().GetDocNum("wb_sales_in").FirstOrDefault(),
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId,
                    KaId = user_settings.DefaultBuyer
                });
                _db.SaveChanges();

                _wbill_id = wb.WbillId;
            }
            else
            {

                wb = _db.WaybillList.FirstOrDefault(f => f.WbillId == _wbill_id);

                /*  try
                  {
                      UpdLockWB();
                      _db.Entry<WaybillList>(wb).State = System.Data.Entity.EntityState.Modified;
                      _db.Entry<WaybillList>(wb).Property(f => f.SummPay).IsModified = false;
                  }
                  catch
                  {

                      Close();
                  }*/

            }

            if (wb != null)
            {
                DBHelper.UpdateSessionWaybill(wb.WbillId);

                //   wb.UpdatedBy = DBHelper.CurrentUser.UserId;

                TurnDocCheckBox.EditValue = wb.Checked;

                KagentComboBox.DataBindings.Add(new Binding("EditValue", wb, "KaId", true, DataSourceUpdateMode.OnPropertyChanged));
                PersonComboBox.DataBindings.Add(new Binding("EditValue", wb, "PersonId", true, DataSourceUpdateMode.OnPropertyChanged));
                NumEdit.DataBindings.Add(new Binding("EditValue", wb, "Num"));
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", wb, "OnDate"));
                NotesEdit.DataBindings.Add(new Binding("EditValue", wb, "Notes"));
                

                payDocUserControl1.OnLoad(_db, wb);
          //      KagentComboBox.Enabled = !payDocUserControl1.IsPayDoc();
            }

       //     KagentComboBox.Enabled = user_settings.DefaultBuyer == null;

            RefreshDet();
        }

   /*     private void UpdLockWB()
        {
            wb = _db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK, NOWAIT) where WbillId = {0}", _wbill_id).FirstOrDefault();
        }*/

        private void RefreshDet()
        {
            int top_row = WBDetReInGridView.TopRowIndex;
            WaybillDetInBS.DataSource = _db.GetWaybillDetIn(_wbill_id).ToList();
            WBDetReInGridView.TopRowIndex = top_row;

            GetOk();
        }

        bool GetOk()
        {
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && KagentComboBox.EditValue != null && OnDateDBEdit.EditValue != null && WaybillDetInBS.Count > 0);

            AddMaterialBtn.Enabled = KagentComboBox.EditValue != DBNull.Value;

            EditMaterialBtn.Enabled = WaybillDetInBS.Count > 0;
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;
            OrdInfoBtn.Enabled = EditMaterialBtn.Enabled;

            KagentComboBox.Enabled = WaybillDetInBS.Count == 0 && user_settings.DefaultBuyer == null && !payDocUserControl1.IsPayDoc();
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

            var SummAll = _db.WaybillDet.Where(w => w.WbillId == _wbill_id).Sum(s => s.Total);
            wb.UpdatedAt = DateTime.Now;
            wb.SummAll = SummAll ?? 0;
            wb.SummInCurr = (wb.SummAll ?? 0) * wb.OnValue;

            _db.SaveChanges();

            payDocUserControl1.Execute(wb.WbillId, fiscalization_checkEdit.Checked);

        //    current_transaction.Commit();

            if (TurnDocCheckBox.Checked)
            {
               var ex_wb =  _db.ExecuteWayBill(wb.WbillId, null, DBHelper.CurrentUser.KaId).FirstOrDefault();

                if (ex_wb.ErrorMessage != "False")
                {
                    MessageBox.Show(ex_wb.ErrorMessage);
                    return;
                }
            }

            is_new_record = false;

            if (MessageBox.Show("Розрукувати документ ?", "Накладна на повернення №" + wb.Num, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                PrintDoc.Show(wb.Id, wb.WType, _db);
            }

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
            var dr = WBDetReInGridView.GetRow(WBDetReInGridView.FocusedRowHandle) as GetWaybillDetIn_Result;

            if (dr != null)
            {
                if (dr.PosType == 0)
                {
                    using (var df = new frmWBReturnDetIn(_db, dr.PosId, wb, (int?)WHComboBox.EditValue, OutDateEdit.DateTime))
                    {
                        if (df.ShowDialog() == DialogResult.OK)
                        {
                            RefreshDet();
                        }
                    }
                }


                if (dr.PosType == 3)
                {
                    using (var df = new frmWayBillTMCDet(_db, dr.PosId, wb))
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
            var dr = WBDetReInGridView.GetRow(WBDetReInGridView.FocusedRowHandle) as GetWaybillDetIn_Result;

            if (dr != null)
            {
                if (dr.PosType == 0)
                {
                    _db.DeleteWhere<WaybillDet>(w => w.PosId == dr.PosId);
                }

                if (dr.PosType == 3)
                {
                    _db.DeleteWhere<WayBillTmc>(w => w.PosId == dr.PosId);
                }

                RefreshDet();
            }
        }

        private void WBDetReInGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && AddMaterialBtn.Enabled && !String.IsNullOrEmpty(BarCodeEdit.Text))
            {
                var BarCodeText = BarCodeEdit.Text.Split('+');
                string kod = BarCodeText[0];
                var item = _db.Materials.Where(w => w.BarCode == kod).Select(s => s.MatId).FirstOrDefault();

                using (var frm = new frmOutMatList(_db, OutDateEdit.DateTime, wb.OnDate, item, wb.KaId.Value, -25))
                {
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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

                                if (df.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {
                                    RefreshDet();
                                }
                            }
                        }
                    }
                }
   
                BarCodeEdit.Text = "";
            }
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

        private void frmWBReturnIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((is_new_record || _db.IsAnyChanges()) && OkButton.Enabled)
            {
                var m_recult = MessageBox.Show(Resources.save_wb, "Накладна на повернення №" + wb.Num, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

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
