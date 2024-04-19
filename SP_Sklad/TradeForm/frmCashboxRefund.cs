using CheckboxIntegration.Client;
using CheckboxIntegration.Models;
using DevExpress.XtraEditors;
using SP_Sklad.Common;
using SP_Sklad.Properties;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad.WBForm
{
    public partial class frmCashboxRefund : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private WaybillList _wb { get; set; }
        //   private GetUserAccessTree_Result _user_Access { get; set; }
        private UserSettingsRepository user_settings { get; set; }
        private string _access_token { get; set; }
        public bool is_authorization { get; set; }
        public bool is_new_record { get; set; }
        private GetWaybillDetIn_Result focused_dr
        {
            get { return WBDetReInGridView.GetFocusedRow() as GetWaybillDetIn_Result; }
        }

        private class user_acc
        {
            public int AccId { get; set; }
            public string AccNum { get; set; }
            public string Name { get; set; }
            public int ExtDocType { get; set; }
        }
        private List<user_acc> user_acc_list { get; set; }

        public frmCashboxRefund(BaseEntities db, WaybillList wb, string access_token)
        {
            InitializeComponent();

            _db = db;
            _wb = wb;
            _access_token = access_token;
            is_authorization = !string.IsNullOrEmpty(_access_token);
        }

        private void frmCashboxCheckoutcs_Load(object sender, EventArgs e)
        {
            user_settings = new UserSettingsRepository(UserSession.UserId, _db);
            var list = _db.GetWaybillDetIn(_wb.WbillId).ToList();

            SumAllEdit.EditValue = list.Sum(s => s.Total);
            PutCashSumEdit.Value = SumAllEdit.Value;
            WaybillDetInBS.DataSource = list;
            is_new_record = true;

        }

        private void PayDoc(int PType, decimal total, ReceiptsSellRespond receipt = null)
        {
            _wb = _db.WaybillList.FirstOrDefault(s => s.WbillId == _wb.WbillId);

            var _pd = _db.PayDoc.Add(new PayDoc()
            {
                Id = Guid.NewGuid(),
                DocNum = new BaseEntities().GetDocNum("pay_doc").FirstOrDefault(), //Номер документа
                Total = total,
                Checked = 1,
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
                EntId = DBHelper.CurrentEnterprise.KaId,
                ReportingDate = _wb.OnDate,
                ReceiptId = receipt?.id
            });

            if (new int[] { 1, 6, 16, 25 }.Contains(_wb.WType)) _pd.DocType = -1;   // Вихідний платіж
            if (new int[] { -1, -6, 2, -16, -25 }.Contains(_wb.WType)) _pd.DocType = 1;  // Вхідний платіж

            _db.SaveChanges();

            var new_pd = _db.PayDoc.AsNoTracking().FirstOrDefault(w => w.PayDocId == _pd.PayDocId);

            if (new_pd != null)
            {
                _db.SetDocRel(_wb.Id, new_pd.Id);
            }

            _db.SaveChanges();
        }

        private void PutSumEdit_EditValueChanged(object sender, EventArgs e)
        {
            var put_cash_sum = string.IsNullOrEmpty(PutCashSumEdit.Text) ? 0 : Convert.ToDecimal(PutCashSumEdit.Text);
            PutCashlessSumEdit.Value = (SumAllEdit.Value - put_cash_sum) > 0 ? (SumAllEdit.Value - put_cash_sum) : 0;
            var put_sum = put_cash_sum + PutCashlessSumEdit.Value;

            PayBtn.Enabled = (put_sum == SumAllEdit.Value) && (SumAllEdit.Value > 0);
        }

        private void frmCashboxCheckout_Shown(object sender, EventArgs e)
        {
            PutCashSumEdit.Enabled = user_settings.CashDesksDefaultRMK != 0;
            PutCashlessSumEdit.Enabled = user_settings.AccountDefaultRMK != 0;

            PutCashSumEdit.Focus();
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

        private void PayBtn_Click(object sender, EventArgs e)
        {
            if (is_authorization)
            {
                _db.DeleteWhere<WaybillDet>(w => w.Checked != 1 && w.WbillId == _wb.WbillId);

                var SummAll = _db.WaybillDet.Where(w => w.WbillId == _wb.WbillId).Sum(s => s.Total) ?? 0;
                _wb.UpdatedAt = DateTime.Now;
                _wb.SummAll = SummAll;
                _wb.SummInCurr = SummAll * _wb.OnValue;
                _db.SaveChanges();

                List<Payment> payments = new List<Payment>();
                if (PutCashSumEdit.Value > 0)
                {
                    payments.Add(new Payment
                    {
                        type = PaymentType.CASH.ToString(),
                        value = Convert.ToInt32(PutCashSumEdit.Value * 100),
                        label = "Готівка"
                    });
                }

                if (PutCashlessSumEdit.Value > 0)
                {
                    payments.Add(new Payment
                    {
                        type = PaymentType.CASHLESS.ToString(),
                        value = Convert.ToInt32(PutCashlessSumEdit.Value * 100),
                        label = "Картка"
                    });
                }

                var receipt = CreateReceiptSell(payments);
                if (receipt.is_error)
                {
                    MessageBox.Show($@"Помилка при отриманні фіксального номера! {(receipt.is_error ? receipt.error.message : "")}");
                    return;
                }
                else
                {
                    _wb.ReceiptId = receipt.id;

                    var ex_wb = _db.ExecuteWayBill(_wb.WbillId, null, DBHelper.CurrentUser.KaId).FirstOrDefault();

                    if (ex_wb.ErrorMessage != "False")
                    {
                        MessageBox.Show(ex_wb.ErrorMessage);
                    }

                    if ((SumAllEdit.Value - PutCashlessSumEdit.Value) > 0)
                    {
                        PayDoc(1, SumAllEdit.Value - PutCashlessSumEdit.Value, receipt);
                    }

                    if (PutCashlessSumEdit.Value > 0)
                    {
                        PayDoc(2, PutCashlessSumEdit.Value, receipt);
                    }

                    _db.Receipt.Add(new Receipt
                    {
                        Id = receipt.id,
                        CeatedAt = receipt.created_at,
                        TotalPayment = receipt.total_payment,
                        TotalSum = receipt.total_sum,
                        Status = receipt.status,
                        ShiftId = receipt.shift != null ? (Guid?)receipt.shift.id : null,
                        BarCode = receipt.barcode,
                        FiscalCode = receipt.fiscal_code,
                        FiscalDate = receipt.fiscal_date,
                        ErrorMessage = receipt.is_error ? receipt.error.message : ""
                    });

                    _db.SaveChanges();

                    IHelper.PrintReceiptPng(_access_token, receipt.id);
                }

                is_new_record = false;
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private ReceiptsSellRespond CreateReceiptSell(List<Payment> payments)
        {
            var wb_det = _db.GetWayBillDetOut(_wb.WbillId).ToList();

            var req = new ReceiptSellPayload
            {
                id = Guid.NewGuid(),
                cashier_name = DBHelper.CurrentUser.Name,
                departament = DBHelper.KagentsWorkerList.FirstOrDefault(w => w.KaId == _wb.KaId).Name,

                goods = wb_det.Select(s => new Goods
                {
                    quantity = Convert.ToInt32(s.Amount * 1000),
                    good = new Good
                    {
                        code = s.MatId.ToString(),
                        barcode = s.BarCode,
                        name = s.MatName,
                        price = Convert.ToInt32(s.BasePrice * 100)
                    },
                    discounts = s.Discount > 0 ? new List<DiscountPayload> { new DiscountPayload { mode = DiscountMode.VALUE, type = DiscountType.DISCOUNT, value = Math.Round((s.DiscountTotal ?? 0), 2) * 100 } } : new List<DiscountPayload>(),
                    is_return = true
                }).ToList(),
                payments = payments,
                discounts = new List<DiscountPayload>(),
                technical_return = true,
                rounding = user_settings.RoundingCheckboxReceipt,
            };

            var new_receipts = new CheckboxClient(_access_token).CreateReceipt(req);

            return new_receipts;
        }

        private void frmCashboxRefund_FormClosed(object sender, FormClosedEventArgs e)
        {
          //  DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

            if (is_new_record)
            {
                _db.DeleteWhere<WaybillList>(w => w.WbillId == _wb.WbillId);
            }
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit edit = sender as CheckEdit;
            var wbd = _db.WaybillDet.FirstOrDefault(w => w.PosId == focused_dr.PosId);
            wbd.Checked = edit.Checked ? 1 : 0;
            _db.SaveChanges();

            var list = _db.GetWaybillDetIn(_wb.WbillId).ToList();

            SumAllEdit.EditValue = _db.WaybillDet.Where(w=> w.WbillId == _wb.WbillId && w.Checked == 1).Sum(s => s.Total);
            PutCashSumEdit.Value = SumAllEdit.Value;
        }

        private void PutCashlessSumEdit_EditValueChanged(object sender, EventArgs e)
        {
            PutCashSumEdit.Value = (SumAllEdit.Value - PutCashlessSumEdit.Value) > 0 ? (SumAllEdit.Value - PutCashlessSumEdit.Value) : 0;
            var put_sum = PutCashSumEdit.Value + PutCashlessSumEdit.Value;

            PayBtn.Enabled = (put_sum == SumAllEdit.Value) && (SumAllEdit.Value > 0);
        }
    }
}
