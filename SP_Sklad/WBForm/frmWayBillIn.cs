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

namespace SP_Sklad.WBForm
{
    public partial class frmWayBillIn : Form
    {
        private int _wtype { get; set; }
        BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private WaybillList wb { get; set; }

        public frmWayBillIn(int wtype, int? wbill_id)
        {
            InitializeComponent();

            _wtype = wtype;
            _wbill_id = wbill_id;
            _db = new BaseEntities();
      //      _db.Database.CommandTimeout = 1;
            current_transaction = _db.Database.BeginTransaction(IsolationLevel.RepeatableRead);

            WaybillDetInGridControl.DataSource = _db.GetWaybillDetIn(wbill_id);
        }

        private void frmWayBillIn_Load(object sender, EventArgs e)
        {
            if (_wbill_id == null)
            {
                wb = _db.WaybillList.Add(new WaybillList() { WType = _wtype, OnDate = DBHelper.ServerDateTime(), Num = _db.GetCounter("wb_in").FirstOrDefault(), CurrId = 2, OnValue = 1 });
                try
                {
                    _db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }

            }
            else
            {
             //   wb = _db.WaybillList.Find(_wbill_id);
                try
                {
                    wb = _db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK) where WbillId = {0}", _wbill_id).FirstOrDefault();
                }
                catch (SqlException exception)
                {
                    if (!exception.Errors.Cast<SqlError>().Any(error =>
                        (error.Number == DeadlockErrorNumber) ||
                        (error.Number == LockingErrorNumber) ||
                        (error.Number == UpdateConflictErrorNumber)))
                    {
                        Close();
                    }
                    else 
                    {
                        Close();
                    }
                }
               
            }

            if (wb != null)
            {
                GetDocValue(wb);
            }

            KagentComboBox.Properties.DataSource = _db.KAGENT.Select(s => new { s.KAID, s.NAME }).ToList();

            var wh_list = DBHelper.WhList();
            WHComboBox.Properties.DataSource = wh_list;
            WHComboBox.EditValue = wh_list.Where(w => w.DEF == 1).Select(s => s.WID).FirstOrDefault();

            PersonComboBox.Properties.DataSource = _db.KAGENT.Where(w => w.KTYPE == 2).Select(s => new { s.KAID, s.NAME }).ToList();
        }

        private void GetDocValue(WaybillList wb)
        {
            _wbill_id = wb.WbillId;
            NUMDBTextEdit.Text = wb.Num;
            OnDateDBEdit.DateTime = wb.OnDate;
            KagentComboBox.EditValue = wb.KaId;
            PersonComboBox.EditValue = wb.PersonId;
            TurnDocCheckBox.Checked = Convert.ToBoolean(wb.Checked);
            ReasonEdit.Text = wb.Reason;
            NotesEdit.Text = wb.Notes;
        }

        private void frmWayBillIn_Shown(object sender, EventArgs e)
        {
            if (_wtype == 1) this.Text = "Властивості прибуткової накладної";
            if (_wtype == 16) this.Text = "Замовлення постачальникові";

            TurnDocCheckBox.Visible = (_wtype != 16);
            checkEdit2.Visible = (_wtype == 16);
            dateEdit2.Visible = (_wtype == 16);

         /*   WhTemp->Edit();
            WhTempWID->Value = SkladData->Warehouse->Lookup("DEF", 1, "WID");

            OnDateDBEdit->Enabled = (SkladData->CurentUserENABLEEDITDATE->Value == 1);
            NowDateBtn->Enabled = OnDateDBEdit->Enabled;*/
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            wb.Num = NUMDBTextEdit.Text;
            wb.OnDate = OnDateDBEdit.DateTime;
            wb.KaId = (int?)KagentComboBox.EditValue;
            wb.PersonId = (int?)PersonComboBox.EditValue;
            wb.Checked = (int)TurnDocCheckBox.EditValue;
            wb.Reason = ReasonEdit.Text;
            wb.Notes = NotesEdit.Text;
            wb.UpdatedAt = DateTime.Now;
            wb.Checked = Convert.ToInt32(TurnDocCheckBox.Checked);
            if (!CheckDate())
            {
                return;
            }

            _db.Entry<WaybillList>(  wb).State  = EntityState.Modified;
            _db.SaveChanges();
            current_transaction.Commit();

            if (TurnDocCheckBox.Checked)
            {
                using (var db = new BaseEntities())
                {
                    db.ExecuteWayBill(wb.WbillId, null);
                }
            }

            Close();
        }

        private bool CheckDate()
        {
            var q = _db.WMatTurn.Where(w => w.WaybillDet.WbillId == _wbill_id).Select(s => new { s.OnDate, s.WaybillDet.MATERIALS.NAME }).Distinct().FirstOrDefault();
            /*  select first 1 distinct wmt.ondate, m.name
   from WMATTURN wmt, waybilldet wbd , materials m
   where wbd.wbillid=:WBILLID and m.matid = wbd.matid and wbd.posid=wmt.posid
     and wmt.turntype = 2
  order by wmt.ondate */

            if (q != null && OnDateDBEdit.DateTime > q.OnDate)
            {
                String msg = "Дата документа не може бути більшою за дату видаткової партії! \nПозиція: " + q.NAME + " \nДата: " + q.OnDate + " \nЗмінити дату докомента на " + q.OnDate + "?";
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
           if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

           _db.Dispose();
           current_transaction.Dispose();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            OnDateDBEdit.DateTime = DateTime.Now;
        }

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var df = new frmWayBillDetIn(_db, null,  wb);
            if (df.ShowDialog() == DialogResult.OK)
            {
                WaybillDetInGridControl.DataSource = _db.GetWaybillDetIn(_wbill_id); 
            }
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetInGridView.GetRow(WaybillDetInGridView.FocusedRowHandle) as GetWaybillDetIn_Result;

            if (dr != null)
            {
                var df = new frmWayBillDetIn(_db, dr.PosId, wb);
                if (df.ShowDialog() == DialogResult.OK)
                {
                    WaybillDetInGridControl.DataSource = _db.GetWaybillDetIn(_wbill_id);
                }
            }
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WaybillDetInGridView.GetRow(WaybillDetInGridView.FocusedRowHandle) as GetWaybillDetIn_Result;

            if (dr != null)
            {
                _db.WaybillDet.Remove(_db.WaybillDet.Find(dr.PosId));
                _db.SaveChanges();

                WaybillDetInGridControl.DataSource = _db.GetWaybillDetIn(_wbill_id);
            }
        }

        private const int DefaultRetryCount = 6;

        private const int DeadlockErrorNumber = 1205;
        private const int LockingErrorNumber = 1222;
        private const int UpdateConflictErrorNumber = 3960;

    /*    private void RetryOnDeadlock(
            Action<DataContext> action,
            int retryCount = DefaultRetryCount)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            var attemptNumber = 1;
            while (true)
            {
                var dataContext = CreateDataContext();
                try
                {
                    action(dataContext);
                    break;
                }
                catch (SqlException exception)
                {
                    if (!exception.Errors.Cast<SqlError>().Any(error =>
                        (error.Number == DeadlockErrorNumber) ||
                        (error.Number == LockingErrorNumber) ||
                        (error.Number == UpdateConflictErrorNumber)))
                    {
                        throw;
                    }
                    else if (attemptNumber == retryCount + 1)
                    {
                        throw;
                    }
                }
                finally
                {
                    dataContext.Dispose();
                }

                attemptNumber++;
            }
        }*/

    }
}
