using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.Common;
using SP_Sklad.ViewsForm;
using CheckboxIntegration.Models;
using CheckboxIntegration.Client;

namespace SP_Sklad.MainTabs
{
    public partial class PayDocUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        BaseEntities _db { get; set; }
        private WaybillList _wb { get; set; }
        private PayDoc _pd { get; set; }
        private GetUserAccessTree_Result _user_Access { get; set; }

        public PayDocUserControl()
        {
            InitializeComponent();
        }

        public bool IsPayDoc()
        {
            return _pd != null;
        }

        private class user_acc
        {
            public int AccId { get; set; }
            public string AccNum { get; set; }
            public string Name { get; set; }
            public int ExtDocType { get; set; }
        }
        private List<user_acc> user_acc_list { get; set; }

        public void OnLoad(BaseEntities db, WaybillList wb)
        {
            _db = db;
            _wb = wb;

            if (new int[] { -1, -6, 2, -16 }.Contains(_wb.WType))   // Вхідний платіж
            {
                _user_Access = _db.GetUserAccessTree(DBHelper.CurrentUser.UserId).ToList().FirstOrDefault(w => w.FunId == 26);
            }
            else
            {
                _user_Access = _db.GetUserAccessTree(DBHelper.CurrentUser.UserId).ToList().FirstOrDefault(w => w.FunId == 25); //Вихідні платежі
            }

            panelControl1.Visible = _user_Access.CanView == 1;

            var rel = db.GetRelDocList(wb.Id).FirstOrDefault(w => w.DocType == -3 || w.DocType == 3);

            if (rel != null)
            {
                try
                {
                    var PayDocId = db.PayDoc.AsNoTracking().FirstOrDefault(f => f.Id == rel.Id).PayDocId;
                    _pd = db.Database.SqlQuery<PayDoc>("select * from  PayDoc WITH (UPDLOCK, NOWAIT) where PayDocId = {0}", PayDocId).FirstOrDefault();
                }
                catch
                {
                    panelControl1.Enabled = false;
                }

                if (_pd != null)
                {
                    _db.Entry(_pd).State = System.Data.Entity.EntityState.Modified;

                    ExecPayCheckBox.EditValue = _pd.Checked;
                    NumEdit.EditValue = _pd.DocNum;
                    PTypeComboBox.EditValue = _pd.PTypeId;
                    CashEditComboBox.EditValue = _pd.CashId;
                    ChargeTypesEdit.EditValue = _pd.CTypeId;
                    SumEdit.EditValue = _pd.Total;
                    CurrencyLookUpEdit.EditValue = _pd.CurrId;
                    AccountEdit.EditValue = _pd.AccId;
                }
            }
            else
            {
                ExecPayCheckBox.EditValue = 0;
                PTypeComboBox.EditValue = 1;
                CurrencyLookUpEdit.EditValue = DBHelper.Currency.Where(w => w.Def == 1).Select(s => s.CurrId).FirstOrDefault(); //Валюта по умолчанию
                if (DBHelper.CashDesks.Any(w => w.Def == 1))
                {
                    CashEditComboBox.EditValue = DBHelper.CashDesks.FirstOrDefault(w => w.Def == 1).CashId;
                }
                else if (DBHelper.CashDesks.Any())
                {
                    CashEditComboBox.EditValue = DBHelper.CashDesks.FirstOrDefault().CashId;
                }
            }

            panelControl1.Enabled = (_user_Access.CanModify == 1 || (_user_Access.CanInsert == 1 && _pd == null)) && DBHelper.CashDesks.Any() && DBHelper.CashDesks.Any(a => a.CashId == Convert.ToInt32(CashEditComboBox.EditValue));
            ExecPayCheckBox.Enabled = _user_Access.CanPost == 1 || (_user_Access.CanInsert == 1 && _pd == null);

            PTypeComboBox.Properties.DataSource = DBHelper.PayTypes;
            CashEditComboBox.Properties.DataSource = panelControl1.Enabled ? DBHelper.CashDesks : DBHelper.AllCashDesks;

            ChargeTypesEdit.Properties.DataSource = DBHelper.ChargeTypes;
            if (DBHelper.ChargeTypes.Any(a => a.Def == 1))
            {
                ChargeTypesEdit.EditValue = DBHelper.ChargeTypes.FirstOrDefault(f => f.Def == 1).CTypeId;
            }

            CurrencyLookUpEdit.Properties.DataSource = _db.Currency.ToList();


            var ent_id = DBHelper.Enterprise.KaId;
            user_acc_list = _db.EnterpriseAccount.Where(w => w.KaId == ent_id).Select(s => new user_acc
            {
                AccId = s.AccId,
                AccNum = s.AccNum,
                Name = s.BankName,
                ExtDocType = 1
            }).ToList()/*.Concat(_db.KAgentAccount.Where(w => w.KAId == _wb.KaId).Select(s => new user_acc
            {
                AccId = s.AccId,
                AccNum = s.AccNum,
                Name = s.Kagent.Name,
                ExtDocType = -1
            }).ToList()).ToList()*/;

            AccountEdit.Properties.DataSource = user_acc_list;

        }


        private void ExecPayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _db.SaveChanges();

            if (_pd != null || _wb.KaId == null)
            {
                return;
            }

            SumEdit.EditValue = _db.WaybillDet.Where(w => w.WbillId == _wb.WbillId).Sum(s => s.Total * s.OnValue);
            if (NumEdit.EditValue == null)
            {
                NumEdit.EditValue = new BaseEntities().GetDocNum("pay_doc").FirstOrDefault();
            }

            if (_wb.KaId != null)
            {
                var ka = _db.Kagent.Find(_wb.KaId);
                if (ka.PayTypeId != null)
                {
                    PTypeComboBox.EditValue = ka.PayTypeId;
                    if (ka.PayTypeId == 1)
                    {
                        CashEditComboBox.EditValue = DBHelper.CashDesks.Any(a => a.CashId == ka.CashDeskId) ? ka.CashDeskId : DBHelper.CashDesks.FirstOrDefault().CashId;
                    }

                }
            }
            else
            {
                PTypeComboBox.EditValue = 1;  // Наличкой
            }

        }

        public void Execute(int wbill_id, bool fiscalization_check = false)
        {
            _wb = _db.WaybillList.FirstOrDefault(s => s.WbillId == wbill_id);
            if (_wb == null && !panelControl1.Enabled)
            {
                return;
            }


            if (_pd == null && ExecPayCheckBox.Checked)
            {
                if (_user_Access.CanInsert == 1)
                {
                    _pd = _db.PayDoc.Add(new PayDoc()
                    {
                        Id = Guid.NewGuid(),
                        DocNum = NumEdit.EditValue.ToString(), //Номер документа
                        Total = Convert.ToDecimal(SumEdit.EditValue),
                        Checked = _user_Access.CanPost == 1 ? 1 : 0,
                        OnDate = _wb.OnDate,
                        WithNDS = 1,  // З НДС
                        PTypeId = Convert.ToInt32(PTypeComboBox.EditValue),  // Вид оплати
                        CashId = (int)PTypeComboBox.EditValue == 1 ? (int?)CashEditComboBox.EditValue : null,  // Каса 
                        AccId = (int)PTypeComboBox.EditValue == 2 ? (int?)AccountEdit.EditValue : null, // Acount
                        CTypeId = (int)ChargeTypesEdit.EditValue,
                        CurrId = 2,  //Валюта по умолчанию
                        OnValue = 1,  //Курс валюти
                        MPersonId = _wb.PersonId,
                        KaId = _wb.KaId,
                        UpdatedBy = DBHelper.CurrentUser.UserId,
                        EntId = DBHelper.Enterprise.KaId,
                        ReportingDate = _wb.OnDate
                    });

                    if (new int[] { 1, 6, 16, 25 }.Contains(_wb.WType)) _pd.DocType = -1;   // Вихідний платіж
                    if (new int[] { -1, -6, 2, -16, -25 }.Contains(_wb.WType)) _pd.DocType = 1;  // Вхідний платіж

                    if (fiscalization_check)
                    {
                        var receipt = CreateReceiptSell(_pd.DocType == -1);
                        _pd.ReceiptId = receipt.id;
                        _wb.ReceiptId = receipt.id;
                    }

                    /*   if ((int)PTypeComboBox.EditValue == 2)
                       {
                           var acc = AccountEdit.GetSelectedDataRow() as user_acc;
                           _pd.DocType = _pd.DocType * acc.ExtDocType;
                       }*/

                    _db.SaveChanges();

                    var new_pd = _db.PayDoc.AsNoTracking().FirstOrDefault(w => w.PayDocId == _pd.PayDocId);

                    if (new_pd != null)
                    {
                        _db.SetDocRel(_wb.Id, new_pd.Id);
                    }
                }
            }
            else if (_pd != null)
            {
                if (_pd.Checked == 1/* && !ExecPayCheckBox.Checked*/)
                {
                    _pd.Checked = 0;
                    _db.SaveChanges();
                }

                _pd.KaId = _wb.KaId;
                _pd.Checked = _user_Access.CanPost == 1 ? Convert.ToInt32(ExecPayCheckBox.EditValue) : _pd.Checked;

                if (_user_Access.CanModify == 1)
                {
                    _pd.DocNum = NumEdit.EditValue.ToString();
                    _pd.Total = Convert.ToDecimal(SumEdit.EditValue);
                    _pd.PTypeId = Convert.ToInt32(PTypeComboBox.EditValue);
                    _pd.MPersonId = _wb.PersonId;
                    _pd.CashId = (int)PTypeComboBox.EditValue == 1 ? (int?)CashEditComboBox.EditValue : null;  // Каса 
                    _pd.AccId = (int)PTypeComboBox.EditValue == 2 ? (int?)AccountEdit.EditValue : null; // Acount
                }
            }

            _db.SaveChanges();
        }

        private void PTypeComboBox_EditValueChanged(object sender, EventArgs e)
        {

            labelControl3.Visible = false;
            CashEditComboBox.Visible = false;

            labelControl18.Visible = false;
            AccountEdit.Visible = false;

            if (PTypeComboBox.EditValue == DBNull.Value)
            {
                return;
            }
            int PType = (int)PTypeComboBox.EditValue;

            if (PType == 1)
            {
                labelControl3.Visible = true;
                CashEditComboBox.Visible = true;
                AccountEdit.EditValue = null;
                if (DBHelper.CashDesks.Any(w => w.Def == 1))
                {
                    CashEditComboBox.EditValue = DBHelper.CashDesks.FirstOrDefault(w => w.Def == 1).CashId;
                }
            }

            if (PType == 2)
            {
                labelControl18.Visible = true;
                AccountEdit.Visible = true;
                CashEditComboBox.EditValue = null;

                if (user_acc_list != null && user_acc_list.Any())
                {
                    AccountEdit.EditValue = user_acc_list.FirstOrDefault().AccId;
                }
            }
        }

        private void CashEditComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                CashEditComboBox.EditValue = IHelper.ShowDirectList(CashEditComboBox.EditValue, 4);
            }
        }

        private void PTypeComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                using (var frm = new frmFinancesView(null, 117))
                {
                    frm.fin_uc.isDirectList = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        PTypeComboBox.EditValue = frm.PayTypeId;
                        if(frm.PayTypeId == 1)
                        {
                            CashEditComboBox.EditValue = frm.Id;
                        }

                        if(frm.PayTypeId == 2)
                        {
                            AccountEdit.EditValue = frm.Id;
                        }
                    }
                }

            }
        }

        private ReceiptsSellRespond CreateReceiptSell(bool return_receipt)
        {
            List<Payment> payments = new List<Payment>();
       //     var total = _db.WaybillDet.Where(w => w.WbillId == _wb.WbillId).Sum(s => s.Total * s.OnValue);
            var wb_det = _db.GetWaybillDetIn(_wb.WbillId).ToList();
            var total = wb_det.Sum(s => s.TotalInCurrency);

            payments.Add(new Payment
            {
                type = PaymentType.CASH.ToString(),
                value = Convert.ToInt32(total * 100),
                label = "Готівка"
            });

            var req = new ReceiptSellPayload
            {
                id = Guid.NewGuid(),
                cashier_name = DBHelper.CurrentUser.Name,
                departament = DBHelper.Kagents.FirstOrDefault(w => w.KaId == _wb.KaId).Name,
                goods = wb_det.Select(s => new Good
                {
                    quantity = Convert.ToInt32(s.Amount * 1000),
                    good = new Good2
                    {
                        code = s.MatId.ToString(),
                        //   barcode = s.BarCode,
                        name = s.MatName,
                        price = Convert.ToInt32(s.Price * 100)
                    },
                    discounts = new List<object>(),
                    is_return = return_receipt

                }).ToList(),
                payments = payments,
                discounts = new List<object>(),
                technical_return = return_receipt,
                rounding = false,
              //  barcode = _wb.WbillId.ToString()
            };

            var cashier_id = _db.Kagent.FirstOrDefault(w => w.KaId == _wb.PersonId).UserId;
            var user_settings = new UserSettingsRepository(/*DBHelper.CurrentUser.UserId*/cashier_id.Value, _db);
            var login = new CheckboxClient().CashierSignin(new CashierSigninRequest { login = user_settings.CashierLoginCheckbox, password = user_settings.CashierPasswordCheckbox });

            string _access_token = login.access_token;

            var return_receipts = new CheckboxClient(_access_token).CreateReceipt(req);

            if (return_receipts.id != Guid.Empty)
            {
                _db.Receipt.Add(new Receipt
                {
                    Id = return_receipts.id,
                    CeatedAt = return_receipts.created_at,
                    TotalPayment = return_receipts.total_payment,
                    TotalSum = return_receipts.total_sum,
                    Status = return_receipts.status,
                    ShiftId = return_receipts.shift.id,
                    BarCode = return_receipts.barcode,
                    FiscalCode = return_receipts.fiscal_code,
                    FiscalDate = return_receipts.fiscal_date
                });
                _db.SaveChanges();
            }

            return return_receipts;
        }



    }
}
