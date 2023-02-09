using System;
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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SP_Sklad.Reports;
using SP_Sklad.Common;
using SP_Sklad.Properties;
using SP_Sklad.EditForm;

namespace SP_Sklad.WBForm
{
    public partial class frmActServicesProvided : DevExpress.XtraEditors.XtraForm
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

        public frmActServicesProvided(int wtype, int? wbill_id = null)
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
      //      WaybillDetInGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + "frmActServicesProvided\\WaybillDetInGridView");

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
                    Nds = DBHelper.Enterprise.NdsPayer == 1 ? DBHelper.CommonParam.Nds : 0,
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
                    wb.Num = new BaseEntities().GetDocNum("wb_act_service").FirstOrDefault();
                }

                WaybillListBS.DataSource = wb;

                _wbill_id = wb.WbillId;

                payDocUserControl1.OnLoad(_db, wb);
                KagentComboBox.Enabled = !payDocUserControl1.IsPayDoc();
            }

            KagentComboBox.Properties.DataSource = DBHelper.KagentsWorkerList;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            CurrencyLookUpEdit.Properties.DataSource = _db.Currency.ToList();
            //PTypeComboBox.Properties.DataSource = DBHelper.PayTypes;

            RefreshDet();
        }

        private void frmWayBillIn_Shown(object sender, EventArgs e)
        {
            if (_wtype == 29) this.Text = "Властивості акту наданих послуг";

        
          //  PTypeComboBox.Visible = (_wtype == 1);

            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);

            PersonComboBox.Enabled = !String.IsNullOrEmpty(user_settings.AccessEditPersonId) && Convert.ToInt32(user_settings.AccessEditPersonId) == 1;
            WaybillDetInGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
            NumEdit.Enabled = user_settings.AccessEditDocNum;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            wb.UpdatedAt = DateTime.Now;
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



        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmWayBillIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            WaybillDetInGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + "frmActServicesProvided\\WaybillDetInGridView");

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

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetInGridView.GetRow(WaybillDetInGridView.FocusedRowHandle) as GetWaybillDetIn_Result;

            if (dr != null)
            {

                using (var svc_det = new frmWaybillSvcDet(_db, dr.PosId * -1, wb))
                {
                    svc_det.ShowDialog();
                }

                RefreshDet();
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (wbd_row != null)
            {
                _db.DeleteWhere<WayBillSvc>(w => w.PosId == wbd_row.PosId * -1);

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

            CurrencyLookUpEdit.Enabled = WaybillDetInBS.List.Count == 0;
            CurRateEdit.Enabled = WaybillDetInBS.List.Count == 0;

            return recult;
        }

        private void NUMDBTextEdit_EditValueChanged(object sender, EventArgs e)
        {
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

        private void WaybillDetInGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var dr = WaybillDetInGridView.GetRow(e.RowHandle) as GetWaybillDetIn_Result;
            var wbs = _db.WayBillSvc.Find(dr.PosId);
            wbs.Amount = Convert.ToDecimal(e.Value);
            
            _db.SaveChanges();

            IHelper.MapProp(_db.GetWaybillDetIn(_wbill_id).AsNoTracking().FirstOrDefault(w => w.PosId == wbd_row.PosId), wbd_row);
        }

        private void PrintBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.Save(wb.WbillId);
            PrintDoc.Show(wb.Id, wb.WType, _db);
        }


        private void KagentComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (!KagentComboBox.ContainsFocus || KagentComboBox.EditValue == null || KagentComboBox.EditValue == DBNull.Value) return;

            wb.KaId = (int?)KagentComboBox.EditValue;
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


        private void KagBalBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KagentComboBox.EditValue == DBNull.Value) return;
            IHelper.ShowKABalans((int)KagentComboBox.EditValue);
        }

        private void frmWayBillIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((is_new_record || _db.IsAnyChanges()) && OkButton.Enabled)
            {
                var m_recult = MessageBox.Show(Resources.save_wb, "Акт наданих послуг №" + wb.Num, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

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
                KagentComboBox.EditValue = IHelper.ShowDirectList(KagentComboBox.EditValue, 1);
                if (KagentComboBox.EditValue != null && KagentComboBox.EditValue != DBNull.Value)
                {
                    wb.KaId = Convert.ToInt32(KagentComboBox.EditValue);
                }

                GetOk();
            }
        }


        private void PersonComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                PersonComboBox.EditValue = IHelper.ShowDirectList(PersonComboBox.EditValue, 3);
            }
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
