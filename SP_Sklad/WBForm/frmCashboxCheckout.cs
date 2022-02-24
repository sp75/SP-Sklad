using DevExpress.XtraEditors;
using SP_Sklad.Common;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad.WBForm
{
    public partial class frmCashboxCheckout : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private WaybillList _wb { get; set; }
        private GetUserAccessTree_Result _user_Access { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private class user_acc
        {
            public int AccId { get; set; }
            public string AccNum { get; set; }
            public string Name { get; set; }
            public int ExtDocType { get; set; }
        }
        private List<user_acc> user_acc_list { get; set; }

        public frmCashboxCheckout(BaseEntities db, WaybillList wb)
        {
            InitializeComponent();

            _db = db;
            _wb = wb;
        }

        private void frmCashboxCheckoutcs_Load(object sender, EventArgs e)
        {
            user_settings = new UserSettingsRepository(UserSession.UserId, _db);

            if (new int[] { -1, -6, 2, -16, -25 }.Contains(_wb.WType))   // Вхідний платіж
            {
                _user_Access = _db.GetUserAccessTree(DBHelper.CurrentUser.UserId).ToList().FirstOrDefault(w => w.FunId == 26);
            }
            else
            {
                _user_Access = _db.GetUserAccessTree(DBHelper.CurrentUser.UserId).ToList().FirstOrDefault(w => w.FunId == 25); //Вихідні платежі
            } 


            SumAllEdit.Value = _db.WaybillDet.Where(w => w.WbillId == _wb.WbillId).Sum(s => s.Total * s.OnValue).Value;
            PutCashSumEdit.Value = SumAllEdit.Value;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
           
        }

        private void PayDoc(int PType, decimal total)
        {
            _wb = _db.WaybillList.AsNoTracking().FirstOrDefault(s => s.WbillId == _wb.WbillId);

            if (_user_Access.CanInsert == 1)
            {
                var _pd = _db.PayDoc.Add(new PayDoc()
                {
                    Id = Guid.NewGuid(),
                    DocNum = new BaseEntities().GetDocNum("pay_doc").FirstOrDefault(), //Номер документа
                    Total = total,
                    Checked = _user_Access.CanPost == 1 ? 1 : 0,
                    OnDate = _wb.OnDate,
                    WithNDS = 1,  // З НДС
                    PTypeId = PType,  // Вид оплати
                    CashId = PType == 1 ? (int?)user_settings.CashDesksDefaultRMK : null,  // Каса 
                    AccId = PType == 2 ? (int?)user_settings.AccountDefaultRMK : null, // Acount
                    CTypeId = user_settings.DefaultChargeTypeByRMK,//(int)ChargeTypesEdit.EditValue,
                    CurrId = 2,  //Валюта по умолчанию
                    OnValue = 1,  //Курс валюти
                    MPersonId = _wb.PersonId,
                    KaId = _wb.KaId,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId,
                    ReportingDate = _wb.OnDate
                });

                if (new int[] { 1, 6, 16, 25 }.Contains(_wb.WType)) _pd.DocType = -1;   // Вихідний платіж
                if (new int[] { -1, -6, 2, -16 ,-25}.Contains(_wb.WType)) _pd.DocType = 1;  // Вхідний платіж


                _db.SaveChanges();

                var new_pd = _db.PayDoc.AsNoTracking().FirstOrDefault(w => w.PayDocId == _pd.PayDocId);

                if (new_pd != null)
                {
                    _db.SetDocRel(_wb.Id, new_pd.Id);
                }
            }

            _db.SaveChanges();
        }



        private void PutSumEdit_EditValueChanged(object sender, EventArgs e)
        {
            var put_cash_sum = string.IsNullOrEmpty(PutCashSumEdit.Text) ? 0 : Convert.ToDecimal(PutCashSumEdit.Text);
            var put_cashless_sum = PutCashlessSumEdit.Value;
            var put_sum = put_cash_sum + put_cashless_sum;

            calcEdit2.Value = put_sum - SumAllEdit.Value;

            PayBtn.Enabled = put_sum >= SumAllEdit.Value && PutCashlessSumEdit.Value <= SumAllEdit.Value;
        }

        private void PutSumEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void frmCashboxCheckout_Shown(object sender, EventArgs e)
        {
            PutCashSumEdit.Enabled = user_settings.CashDesksDefaultRMK != 0;
            PutCashlessSumEdit.Enabled = user_settings.AccountDefaultRMK != 0;

            PutCashSumEdit.Focus();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
         
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
 
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            PutCashSumEdit.Text += ((SimpleButton)sender).Text;
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            PutCashSumEdit.Text = "";
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            if (!PutCashSumEdit.Text.Any(a => a == ','))
            {
                PutCashSumEdit.Text += ",";
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                PayBtn.PerformClick();
                return true;
            }

            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            using (var frm = new frmSetWBNote(_wb))
            {
                frm.ShowDialog();
            }
        }

        private void PayBtn_Click(object sender, EventArgs e)
        {
            if (PutCashSumEdit.Value > 0)
            {
                PayDoc(1, Convert.ToDecimal(SumAllEdit.Value - PutCashlessSumEdit.Value));
            }

            if(PutCashlessSumEdit.Value > 0)
            {
                PayDoc(2, Convert.ToDecimal(PutCashlessSumEdit.Value));
            }
        }
    }
}
