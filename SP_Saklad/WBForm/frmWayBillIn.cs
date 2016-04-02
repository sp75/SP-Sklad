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
using SP_Saklad.SpData;
using SP_Saklad.WBDetForm;

namespace SP_Saklad.WBForm
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
            current_transaction = _db.Database.BeginTransaction(IsolationLevel.RepeatableRead);

            WaybillDetInGridControl.DataSource = _db.GetWaybillDetIn(wbill_id);
        }

        private void frmWayBillIn_Load(object sender, EventArgs e)
        {
            if (_wbill_id == null)
            {
                wb = _db.WaybillList.Add(new WaybillList() { WType = _wtype, OnDate = DateTime.Now, Num = _db.GetCounter("wb_in").FirstOrDefault() });
                try
                {
                    _db.SaveChanges();
                }
                catch ( Exception )
                {
                    throw;
                }
                
            }
            else
            {
                wb = _db.WaybillList.Find(_wbill_id);
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

            _db.SaveChanges();
            current_transaction.Commit();

            Close();
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
    }
}
