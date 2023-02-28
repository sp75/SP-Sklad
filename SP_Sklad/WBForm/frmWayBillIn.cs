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
using System.Data.SqlClient;
using System.Data.Linq;
using EntityState = System.Data.Entity.EntityState;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SP_Sklad.Reports;
using SP_Sklad.Common;
using SP_Sklad.Common.WayBills;
using SP_Sklad.Properties;
using SP_Sklad.EditForm;

namespace SP_Sklad.WBForm
{
    public partial class frmWayBillIn : DevExpress.XtraEditors.XtraForm
    {
        private int _wtype { get; set; }
        public BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        public Guid? doc_id { get; set; }
        private WaybillList wb { get; set; }
        private GetWaybillDetIn_Result wbd_row
        {
            get
            {
                return WaybillDetInGridView.GetFocusedRow() as GetWaybillDetIn_Result;
            }
        }
        public bool is_new_record { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        public frmWayBillIn(int wtype, int? wbill_id = null)
        {
            is_new_record = false;
            _wtype = wtype;
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();
        }

        private void frmWayBillIn_Load(object sender, EventArgs e)
        {
            WaybillDetInGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + "frmWayBillIn\\WaybillDetInGridView");

            if (_wbill_id == null && doc_id == null)
            {
                is_new_record = true;

                wb = _db.WaybillList.Add(new WaybillList()
                {
                    Id = Guid.NewGuid(),
                    WType = _wtype,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = "",
                    CurrId = 2,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId,
                    PTypeId = 1
                });

                _db.SaveChanges();
            }
            else
            {
                wb = _db.WaybillList.FirstOrDefault(f => f.Id == doc_id || f.WbillId == _wbill_id);
            }

            if (wb != null)
            {
                DBHelper.UpdateSessionWaybill(wb.WbillId);

                if (is_new_record) //Після копіювання згенерувати новий номер
                {
                    wb.Num = new BaseEntities().GetDocNum("wb_in").FirstOrDefault();
                }

                WaybillListBS.DataSource = wb;

                GetDocValue(wb);
            }

            KagentComboBox.Properties.DataSource = DBHelper.KagentsWorkerList;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            CurrencyLookUpEdit.Properties.DataSource = _db.Currency.ToList();
            PTypeComboBox.Properties.DataSource = DBHelper.PayTypes;

            var wh_list = DBHelper.WhList;
            WHComboBox.Properties.DataSource = wh_list;
            WHComboBox.EditValue = wh_list.Where(w => w.Def == 1).Select(s => s.WId).FirstOrDefault();

            RefreshDet();
        }

        private void GetDocValue(WaybillList wb)
        {
            _wbill_id = wb.WbillId;
            NumEdit.Text = wb.Num;
            OnDateDBEdit.DateTime = wb.OnDate;
            KagentComboBox.EditValue = wb.KaId;
            PersonComboBox.EditValue = wb.PersonId;
            TurnDocCheckBox.Checked = Convert.ToBoolean(wb.Checked);
            ReasonEdit.Text = wb.Reason;
            NotesEdit.Text = wb.Notes;
            PersonComboBox.EditValue = wb.PersonId;

            payDocUserControl1.OnLoad(_db, wb);
            KagentComboBox.Enabled = !payDocUserControl1.IsPayDoc();
        }

        private void frmWayBillIn_Shown(object sender, EventArgs e)
        {
            if (_wtype == 1) this.Text = "Властивості прибуткової накладної";
            if (_wtype == 16) this.Text = "Замовлення постачальникові";

            TurnDocCheckBox.Enabled = (_wtype != 16);
            checkEdit2.Visible = (_wtype == 16);
            ToDateEdit.Visible = (_wtype == 16);
            labelControl5.Visible = (_wtype == 1);
            PTypeComboBox.Visible = (_wtype == 1);

            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);

            PersonComboBox.Enabled = !String.IsNullOrEmpty(user_settings.AccessEditPersonId) && Convert.ToInt32(user_settings.AccessEditPersonId) == 1;
            WaybillDetInGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
            NumEdit.Enabled = user_settings.AccessEditDocNum;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            wb.Num = NumEdit.Text;
            wb.OnDate = OnDateDBEdit.DateTime;
            wb.KaId = (int?)KagentComboBox.EditValue;
            wb.PersonId = (int?)PersonComboBox.EditValue;
            wb.Reason = ReasonEdit.Text;
            wb.Notes = NotesEdit.Text;
            wb.UpdatedAt = DateTime.Now;


            if (!CheckDate())
            {
                return;
            }

            _db.Save(wb.WbillId);

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

        private bool CheckDate()
        {
            var q = _db.WMatTurn.Where(w => w.WaybillDet.WbillId == _wbill_id && w.TurnType == 2).Select(s => new
            {
                s.OnDate,
                s.WaybillDet.Materials.Name
            }).Distinct().FirstOrDefault();
            /*  select first 1 distinct wmt.ondate, m.name
   from WMATTURN wmt, waybilldet wbd , materials m
   where wbd.wbillid=:WBILLID and m.matid = wbd.matid and wbd.posid=wmt.posid
     and wmt.turntype = 2
  order by wmt.ondate */

            if (q != null && OnDateDBEdit.DateTime > q.OnDate)
            {
                String msg = "Дата документа не може бути більшою за дату видаткової партії! \nПозиція: " + q.Name + " \nДата: " + q.OnDate + " \nЗмінити дату докомента на " + q.OnDate + "?";
                if (MessageBox.Show(msg, "Інформація", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    wb.OnDate = q.OnDate;
                    return true;
                }
                else return false;
            }

            return true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmWayBillIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            WaybillDetInGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + "frmWayBillIn\\WaybillDetInGridView");

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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
          
        }

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var df = new frmWayBillDetIn(_db, null, wb))
            {
                if (df.ShowDialog() == DialogResult.OK)
                {
                    RefreshDet();
                }
            }
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetInGridView.GetRow(WaybillDetInGridView.FocusedRowHandle) as GetWaybillDetIn_Result;

            if (dr != null)
            {
                if (dr.PosId > 0)
                {
                    using (var wb_det = new frmWayBillDetIn(_db, dr.PosId, wb))
                    {
                        wb_det.ShowDialog();
                    }
                }
                else
                {
                    using (var svc_det = new frmWaybillSvcDet(_db, dr.PosId * -1, wb))
                    {
                        svc_det.ShowDialog();
                    }
                }

                RefreshDet();
            }
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
                _db.Save(wb.WbillId);

                WaybillDetInGridView.DeleteSelectedRows();
            }
        }

        private void RefreshDet()
        {
            _db.UpdWaybillDetPrice(_wbill_id);
            
            int top_row = WaybillDetInGridView.TopRowIndex;
            WaybillDetInBS.DataSource = _db.GetWaybillDetIn(_wbill_id).AsNoTracking().OrderBy(o=> o.Num).ToList();
            WaybillDetInGridView.TopRowIndex = top_row;

            GetOk();
        }

        bool GetOk()
        {
            bool recult = (NumEdit.EditValue != null  && KagentComboBox.EditValue != null && OnDateDBEdit.EditValue != null && WaybillDetInBS.List.Count > 0);
            barSubItem1.Enabled = KagentComboBox.EditValue != null;

            OkButton.Enabled = recult;
            EditMaterialBtn.Enabled = WaybillDetInBS.List.Count > 0;
            DelMaterialBtn.Enabled = WaybillDetInBS.List.Count > 0;
            barButtonItem5.Enabled = WaybillDetInBS.List.Count > 0;

            CurrencyLookUpEdit.Enabled = WaybillDetInBS.List.Count == 0;
            CurRateEdit.Enabled = WaybillDetInBS.List.Count == 0;

            return recult;
        }

        private void NUMDBTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (!WHComboBox.Focused)
            {
                return;
            }
            UpdateWh();
        }

        private void UpdateWh()
        {
            if (WaybillDetInBS.Count > 0)
            {
                if (MessageBox.Show("Оприходувати весь товар на склад <" + WHComboBox.Text + ">?", "Інформація", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    foreach (var item in _db.WaybillDet.Where(w => w.WbillId == _wbill_id))
                    {
                        item.WId = Convert.ToInt32(WHComboBox.EditValue);

                        foreach (var turn in _db.WMatTurn.Where(w => w.SourceId == item.PosId))
                        {
                            turn.WId = Convert.ToInt32(WHComboBox.EditValue);
                        }
                    }
                    _db.Save(wb.WbillId);
                    RefreshDet();
                }
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

        private void WaybillDetInGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var dr = WaybillDetInGridView.GetRow(e.RowHandle) as GetWaybillDetIn_Result;
            var wbd = _db.WaybillDet.Find(dr.PosId);

            wbd.Amount = Convert.ToDecimal(e.Value);
            wbd.Checked = 1;

            var wmt = _db.WMatTurn.FirstOrDefault(w => w.SourceId == wbd.PosId && w.TurnType == 3);
            if (wmt != null)
            {
              //  if (wb.WType == 16)
               // {
                    // удаляем резерв з видаткових документів
                    _db.DeleteWhere<WMatTurn>(w => w.PosId == wbd.PosId);
                    _db.WMatTurn.Add(new WMatTurn()
                    {
                        SourceId = wbd.PosId,
                        PosId = wbd.PosId,
                        WId = wbd.WId.Value,
                        MatId = wbd.MatId,
                        OnDate = wbd.OnDate.Value,
                        TurnType = 3,
                        Amount = wbd.Amount
                    });
               // }
            }
            _db.SaveChanges();

            IHelper.MapProp(_db.GetWaybillDetIn(_wbill_id).AsNoTracking().FirstOrDefault(w => w.PosId == wbd_row.PosId), wbd_row);

            //    var dd = WayBillsController.GetWaybillDetIn(_db, _wbill_id);
        }

        private void PrintBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.Save(wb.WbillId);
            PrintDoc.Show(wb.Id, wb.WType, _db);
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_wtype == 16)
            {
                IHelper.ShowMatListByWH(_db, wb);
            }
            else
            {
                IHelper.ShowMatList(_db, wb);
            }

            RefreshDet();
        }

        private void KagentComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = KagentComboBox.GetSelectedDataRow() as GetKagentList_Result;
            if (!KagentComboBox.ContainsFocus || row == null)
            {
                return;
            }

            wb.KaId = (int?)KagentComboBox.EditValue;
            wb.Nds = row.NdsPayer == 1 ? DBHelper.CommonParam.Nds : 0;
            GetOk();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var df = new frmWaybillSvcDet(_db, null, wb);
            if (df.ShowDialog() == DialogResult.OK)
            {
                RefreshDet();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void KagBalBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KagentComboBox.EditValue == DBNull.Value) return;
            IHelper.ShowKABalans((int)KagentComboBox.EditValue);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetInGridView.GetFocusedRow() as GetWaybillDetIn_Result;

            if (dr != null)
            {
                IHelper.ShowMatRSV(dr.MatId, _db);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowOrdered((int)KagentComboBox.EditValue, 16, 0);
        }


        private void frmWayBillIn_FormClosing(object sender, FormClosingEventArgs e)
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

        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                OnDateDBEdit.DateTime = DBHelper.ServerDateTime();
            }
        }

        private void KagentComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                //   wb.KaId = (int)IHelper.ShowDirectList(KagentComboBox.EditValue, 1);
                //    KagentComboBox.EditValue = wb.KaId;
                KagentComboBox.EditValue = IHelper.ShowDirectList(KagentComboBox.EditValue, 1);
                if (KagentComboBox.EditValue != null && KagentComboBox.EditValue != DBNull.Value)
                {
                    wb.KaId = Convert.ToInt32(KagentComboBox.EditValue);
                }

                GetOk();
            }
        }

        private void WHComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                WHComboBox.EditValue = IHelper.ShowDirectList(WHComboBox.EditValue, 2);

                UpdateWh();
            }
        }

        private void PersonComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                PersonComboBox.EditValue = IHelper.ShowDirectList(PersonComboBox.EditValue, 3);
            }
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

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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

            WaybillDetInGridView.RefreshRow(WaybillDetInGridView.FocusedRowHandle);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();
            _db.DeleteWhere<WaybillDet>(w => w.WbillId == _wbill_id && w.Checked != 1);
            RefreshDet();
        }

        private void WaybillDetInGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.WbDetPopupMenu.ShowPopup(p2);
            }
        }

        private void CurrencyLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (CurrencyLookUpEdit.Focused)
            {
                var cur_id = Convert.ToInt32(CurrencyLookUpEdit.EditValue);

                var last_curr = _db.CurrencyRate.Where(w => w.OnDate <= OnDateDBEdit.DateTime.Date && w.CurrId == cur_id).OrderByDescending(o => o.OnDate).FirstOrDefault();
                if (last_curr != null)
                {
                    wb.OnValue = last_curr.OnValue;
                }
                else
                {
                    wb.OnValue = 1;
                }

                var curr_on_date = _db.CurrencyRate.FirstOrDefault(w => w.OnDate == OnDateDBEdit.DateTime.Date && w.CurrId == cur_id);

                if (curr_on_date == null && cur_id != 2)
                {
                    using (var frm = new frmCurrencyRate(cur_id, OnDateDBEdit.DateTime.Date))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            wb.OnValue = frm.SumEdit.Value;
                        }
                    }
                }
                

            }
        }
    }
}
