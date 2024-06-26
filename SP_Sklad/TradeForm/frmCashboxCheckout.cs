﻿using CheckboxIntegration.Client;
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
    public partial class frmCashboxCheckout : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private WaybillList _wb { get; set; }
     //   private GetUserAccessTree_Result _user_Access { get; set; }
        private UserSettingsRepository user_settings { get; set; }
        private string _access_token { get; set; }
        public bool is_authorization { get; set; }

        private class user_acc
        {
            public int AccId { get; set; }
            public string AccNum { get; set; }
            public string Name { get; set; }
            public int ExtDocType { get; set; }
        }
        private List<user_acc> user_acc_list { get; set; }

        public frmCashboxCheckout(BaseEntities db, WaybillList wb, string access_token)
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

        /*    var user_access_tree = _db.GetUserAccessTree(DBHelper.CurrentUser.UserId).ToList();

          if (_wb.WType == -25) // Чек  
            {
                _user_Access = user_access_tree.FirstOrDefault(w => w.FunId == 88); // Прибуткові касові ордера
            }
            else if (_wb.WType == 25) // Повернення
            {
                _user_Access = user_access_tree.FirstOrDefault(w => w.FunId == 89); // Видаткові касові ордера
            }*/

            SumAllEdit.Value = _db.WaybillDet.Where(w => w.WbillId == _wb.WbillId).Sum(s => s.Total * s.OnValue).Value;
            PutCashSumEdit.Value = user_settings.RoundingCheckboxReceipt ? Math.Round(SumAllEdit.Value, 1) : SumAllEdit.Value;
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
            var put_cashless_sum = PutCashlessSumEdit.Value;
            var put_sum = put_cash_sum + put_cashless_sum;

            if (put_sum - SumAllEdit.Value >= 0)
            {
                RemainderEdit.Value = put_sum - SumAllEdit.Value;
            }

            PayBtn.Enabled = put_sum >= SumAllEdit.Value && PutCashlessSumEdit.Value <= SumAllEdit.Value;
        }

        private void frmCashboxCheckout_Shown(object sender, EventArgs e)
        {
            PutCashSumEdit.Enabled = user_settings.CashDesksDefaultRMK != 0;
            PutCashlessSumEdit.Enabled = user_settings.AccountDefaultRMK != 0;

            PutCashSumEdit.Focus();
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
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    PrintDoc.Show(_wb.Id, _wb.WType, _db);
                }
            }
        }

        private void PayBtn_Click(object sender, EventArgs e)
        {
            if (is_authorization)
            {
                var c_sign = new CheckboxClient(_access_token).CheckSignature();
                if(c_sign == null || c_sign.error != null)
                {
                    MessageBox.Show($@"Помилка оплати, checkbox сервіс тимчасово не доступний!");
                    return;
                }

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
                    receipt.created_at = DateTime.Now;
                    receipt.total_payment = 0;
                    receipt.total_sum = 0;

                    MessageBox.Show($@"Помилка при отриманні фіксального номера! {(receipt.is_error ? receipt.error.message : "")}");

                    DialogResult = DialogResult.OK;
                    return;
                }
                else
                {
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

                    _wb.ReceiptId = receipt.id;

                    _db.SaveChanges();

                    try
                    {
                        IHelper.PrintReceiptPng(_access_token, receipt.id);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show($"Помилка в налаштуваннях прінтера, {err.Message}!"); 
                    }
                }
            }
            else
            {
                PayWithoutCheckBtn.PerformClick();
            }

            DialogResult = DialogResult.OK;
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
                    is_return = false
                }).ToList(),
                payments = payments,
                discounts = new List<DiscountPayload>(),
                technical_return = false,
                rounding = user_settings.RoundingCheckboxReceipt,
                //  barcode = _wb.WbillId.ToString()
            };

            var new_receipts = new CheckboxClient(_access_token).CreateReceipt(req);

            return new_receipts;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if ((SumAllEdit.Value - PutCashlessSumEdit.Value) > 0)
            {
                PayDoc(1, SumAllEdit.Value - PutCashlessSumEdit.Value);
            }

            if (PutCashlessSumEdit.Value > 0)
            {
                PayDoc(2, PutCashlessSumEdit.Value);
            }

            if (MessageBox.Show("Відкрити документ ?", "Видаткова накладна №" + _wb.Num, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                PrintDoc.Show(_wb.Id, _wb.WType, _db);
            }
        }
    }
}
