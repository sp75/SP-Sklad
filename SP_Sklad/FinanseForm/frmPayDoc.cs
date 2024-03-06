using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.EditForm;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;

namespace SP_Sklad.FinanseForm
{
    public partial class frmPayDoc : DevExpress.XtraEditors.XtraForm
    {
        private int? _DocType { get; set; }
        private BaseEntities _db { get; set; }
        private int? _PayDocId { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        public PayDoc _pd { get; set; }
        public int? _ka_id { get; set; }
        private decimal? _summ_pay { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private class user_acc
        {
            public int AccId { get; set; }
            public string AccNum { get; set; }
            public string Name { get; set; }
            public int ExtDocType { get; set; }
            public int KaId { get; set; }
        }
        private List<user_acc> user_acc_list { get; set; }

        public frmPayDoc(int? DocType, int? PayDocId, decimal? SummPay = 0)
        {
            _DocType = DocType;
            _PayDocId = PayDocId;
            _summ_pay = SummPay;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction(/*IsolationLevel.RepeatableRead*/);

            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();
        }

        private void frmPayDoc_Load(object sender, EventArgs e)
        {
            PTypeComboBox.Properties.DataSource = DBHelper.PayTypes;
            CashEditComboBox.Properties.DataSource = DBHelper.CashDesks;
            KagentComboBox.Properties.DataSource = DBHelper.KagentsWorkerList;
            PersonEdit.Properties.DataSource = DBHelper.Persons;
            CurrEdit.Properties.DataSource = DBHelper.Currency;
            ChargeTypesEdit.Properties.DataSource = DBHelper.ChargeTypes;

      //      RecipientAccEdit.Properties.DataSource = _db.v_KAgentAccount.Where(w => w.KType != 3).ToList();

            /*
                        user_acc_list = _db.EnterpriseAccount.Where(w => w.KaId == UserSession.EnterpriseId).Select(s => new user_acc
                        {
                            AccId = s.AccId,
                            AccNum = s.AccNum,
                            Name = s.BankName,
                            ExtDocType = 1
                        }).ToList();*/


            if (_PayDocId == null)
            {
                int w_type = _DocType.Value != -2 ? _DocType.Value * 3 : _DocType.Value;
                string doc_setting_name = w_type == -3 ? "pay_doc_out" : (w_type == 3 ? "pay_doc_in" : "pay_doc");
                _pd = _db.PayDoc.Add(new PayDoc
                {
                    Id = Guid.NewGuid(),
                    Checked = 1,
                    DocNum = new BaseEntities().GetDocNum(doc_setting_name).FirstOrDefault(),
                    OnDate = DBHelper.ServerDateTime(),
                    Total = _summ_pay == null ? 0 : _summ_pay.Value,
                    CTypeId = DBHelper.ChargeTypes.Any(a => a.Def == 1) ? DBHelper.ChargeTypes.FirstOrDefault(w => w.Def == 1).CTypeId : DBHelper.ChargeTypes.FirstOrDefault().CTypeId,// За товар
                    WithNDS = 1,// З НДС
                    PTypeId = 1,// Наличкой
                    CashId = DBHelper.CashDesks.Where(w => w.Def == 1).Select(s => s.CashId).FirstOrDefault(),// Каса по умолчанию
                    CurrId = DBHelper.Currency.Where(w => w.Def == 1).Select(s => s.CurrId).FirstOrDefault(), //Валюта по умолчанию
                    OnValue = 1,//Курс валюти
                    MPersonId = DBHelper.CurrentUser.KaId,
                    DocType = _DocType.Value,
                    UpdatedBy = UserSession.UserId,
                    KaId = _ka_id,
                    EntId = UserSession.EnterpriseId,
                    ReportingDate = DBHelper.ServerDateTime()
                });
            }
            else
            {
                try
                {
                    _pd = _db.Database.SqlQuery<PayDoc>("SELECT * from PayDoc WITH (UPDLOCK, NOWAIT) where PayDocId = {0}", _PayDocId).FirstOrDefault();
                    _db.Entry<PayDoc>(_pd).State = System.Data.Entity.EntityState.Modified;
                }
                catch
                {
                    Close();
                }

            }

            /*    AccountEdit.Properties.DataSource = user_acc_list.Concat(_db.KAgentAccount.Where(w => w.KAId == _pd.KaId).Select(s => new user_acc
                {
                    AccId = s.AccId,
                    AccNum = s.AccNum,
                    Name = s.Kagent.Name,
                    ExtDocType = -1,
                    KaId = s.KAId
                }).ToList()).ToList();*/


            AccountEdit.Properties.DataSource = _db.EnterpriseAccount.Where(w => w.KaId == _pd.EntId).Select(s => new user_acc
            {
                AccId = s.AccId,
                AccNum = s.AccNum,
                Name = s.BankName,
                ExtDocType = 1
            }).ToList();


            if (_pd != null)
            {
                TurnDocCheckBox.DataBindings.Add(new Binding("EditValue", _pd, "Checked"));
                NumEdit.DataBindings.Add(new Binding("EditValue", _pd, "DocNum"));
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", _pd, "OnDate", true, DataSourceUpdateMode.OnPropertyChanged));
                PTypeComboBox.DataBindings.Add(new Binding("EditValue", _pd, "PTypeId", true, DataSourceUpdateMode.OnPropertyChanged));
                CashEditComboBox.DataBindings.Add(new Binding("EditValue", _pd, "CashId", true, DataSourceUpdateMode.OnPropertyChanged));
                ChargeTypesEdit.DataBindings.Add(new Binding("EditValue", _pd, "CTypeId", true, DataSourceUpdateMode.OnPropertyChanged));
                KagentComboBox.DataBindings.Add(new Binding("EditValue", _pd, "KaId", true, DataSourceUpdateMode.OnPropertyChanged));
                PersonEdit.DataBindings.Add(new Binding("EditValue", _pd, "MPersonId", true, DataSourceUpdateMode.OnPropertyChanged));
                SumEdit.DataBindings.Add(new Binding("EditValue", _pd, "Total"));
                CurrEdit.DataBindings.Add(new Binding("EditValue", _pd, "CurrId"));
                CurrValueEdit.DataBindings.Add(new Binding("EditValue", _pd, "OnValue", true, DataSourceUpdateMode.OnPropertyChanged));
                SchetEdit.DataBindings.Add(new Binding("EditValue", _pd, "Schet"));
                WithNDScheckEdit.DataBindings.Add(new Binding("EditValue", _pd, "WithNDS"));
                ReasonEdit.DataBindings.Add(new Binding("EditValue", _pd, "Reason"));
                NotesEdit.DataBindings.Add(new Binding("EditValue", _pd, "Notes"));
                AccountEdit.DataBindings.Add(new Binding("EditValue", _pd, "AccId", true, DataSourceUpdateMode.OnPropertyChanged));
                ReportingDateEdit.DataBindings.Add(new Binding("EditValue", _pd, "ReportingDate", true, DataSourceUpdateMode.OnPropertyChanged));
                RecipientAccEdit.DataBindings.Add(new Binding("EditValue", _pd, "KaAccId", true, DataSourceUpdateMode.OnPropertyChanged));
            }

            if (_DocType < 0)
            {
                if (_DocType == -2)
                {
                    Text = "Властивості платежу (дод. витрати)";
                }
                if (_DocType == -1)
                {
                    Text = "Властивості вихідного платежу";
                }

                TypDocsEdit.Properties.DataSource = DBHelper.DocTypeList.Where(w => w.Id == 1 || w.Id == 6 || w.Id == 16 || w.Id == 25 || w.Id == 27 || w.Id == 29).ToList();
                if (TypDocsEdit.EditValue == null)
                {
                    TypDocsEdit.EditValue = 1;
                }
            }
            else
            {
                Text = "Властивості вхідного платежу";
                labelControl13.Text = "Платник:";
                TypDocsEdit.Properties.DataSource = DBHelper.DocTypeList.Where(w => new int[] { -1, -6, 2, -16, -8, -25, 27 }.Any(a => a.Equals(w.Id))).ToList();
                if (TypDocsEdit.EditValue == null)
                {
                    TypDocsEdit.EditValue = -1;
                }
            }

            var rl = _db.GetRelDocList(_pd.Id).FirstOrDefault();
            if (rl != null)
            {
                PayDocCheckEdit.Checked = true;
                TypDocsEdit.EditValue = rl.DocType;
                GetDocList();
                DocListEdit.EditValue = rl.Id;

                var row = DocListEdit.GetSelectedDataRow() as GetWayBillList_Result;

                textEdit4.EditValue = (row == null ? 0 : row.Balans) - (SumEdit.Value * CurrValueEdit.Value);

                KagentComboBox.Enabled = false;
            }

            Text += $" [{DBHelper.Enterprise.Name}]";
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _pd.UpdatedBy = UserSession.UserId;

            if (_pd.OnDate.Date <= _db.CommonParams.First().EndCalcPeriod)
            {
                MessageBox.Show("Період вже закритий. Змініть дату документа!", "Відміна/Проведення платіжного документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = DocListEdit.GetSelectedDataRow() as GetWayBillList_Result;
            var rl = _db.GetRelDocList(_pd.Id).ToList();


            if (!PayDocCheckEdit.Checked)
            {
                foreach (var item in rl)
                {
                    _db.DeleteWhere<DocRels>(w => w.OriginatorId == item.Id && w.RelOriginatorId == _pd.Id);
                }
                _db.SaveChanges();
            }

            _db.SaveChanges();


            if (PayDocCheckEdit.Checked && DocListEdit.EditValue != null && row != null)
            {
                foreach (var item in rl)
                {
                    _db.DeleteWhere<DocRels>(w => w.OriginatorId == item.Id && w.RelOriginatorId == _pd.Id);
                }

                _db.SetDocRel(row.Id, _pd.Id);

                _db.SaveChanges();
            }

            current_transaction.Commit();

            Close();
        }

        private void frmPayDoc_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.SaveChanges();

            PrintDoc.Show(_pd.Id, _pd.DocType == -2 ? _pd.DocType : _pd.DocType * 3, _db);
        }

        private void PTypeComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (PTypeComboBox.EditValue == DBNull.Value)
            {
                return;
            }

            labelControl7.Visible = false;
            CashEditComboBox.Visible = false;

            labelControl18.Visible = false;
            AccountEdit.Visible = false;

            if ((int)PTypeComboBox.EditValue == 1)
            {
                labelControl7.Visible = true;
                CashEditComboBox.Visible = true;
                _pd.AccId = null;
            }

            if ((int)PTypeComboBox.EditValue == 2)
            {
                labelControl18.Visible = true;
                AccountEdit.Visible = true;
                _pd.CashId = null;
            }

            GetOk();
        }

        bool GetOk()
        {
            bool kg = (_DocType.Value == -2) || (KagentComboBox.EditValue != null && KagentComboBox.EditValue != DBNull.Value && (_DocType.Value != -2));

            bool source = PTypeComboBox.EditValue != null && PTypeComboBox.EditValue != DBNull.Value
                && (((int)PTypeComboBox.EditValue == 1 && CashEditComboBox.EditValue != DBNull.Value && Convert.ToInt32(CashEditComboBox.EditValue) > 0) || ((int)PTypeComboBox.EditValue == 2 && AccountEdit.EditValue != DBNull.Value));

            bool recult = (NumEdit.Text.Any() && SumEdit.Value > 0 && kg && source);

            OkButton.Enabled = recult;

            return recult;
        }

        private void NumEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void frmPayDoc_Shown(object sender, EventArgs e)
        {
            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            NumEdit.Enabled = user_settings.AccessEditDocNum;

            GetOk();
        }

        private void TypDocsEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!TypDocsEdit.ContainsFocus)
            {
                return;
            }

            GetDocList();
        }

        private void DocListEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!DocListEdit.ContainsFocus)
            {
                return;
            }

            var row = DocListEdit.GetSelectedDataRow() as GetWayBillList_Result;
            if (row != null)
            {
                SumEdit.EditValue = row.Balans;
                _pd.Total = row.Balans.Value;
                KagentComboBox.EditValue = row.KaId;
                _pd.KaId = row.KaId;

                _pd.Reason = TypDocsEdit.Text + " №" + DocListEdit.Text;
                ReasonEdit.EditValue = _pd.Reason;
            }
        }

        private void SumEdit_EditValueChanged(object sender, EventArgs e)
        {
             var row = DocListEdit.GetSelectedDataRow() as GetWayBillList_Result;
             if (row == null)
             {
                 textEdit4.EditValue = SumEdit.EditValue;
             }
             else
             {
                textEdit4.EditValue = row.Balans - (SumEdit.Value * CurrValueEdit.Value);
             }

             GetOk();
        }

        public void GetDocList()
        {
            if (TypDocsEdit.EditValue == null)
            {
                return;
            }
            var ka_id = KagentComboBox.EditValue == null || KagentComboBox.EditValue == DBNull.Value ? 0 : (int)KagentComboBox.EditValue;

            var doc_type = Convert.ToInt32(TypDocsEdit.EditValue);

            //    DocListEdit.Properties.DataSource = _db.v_WaybillList.Where(w => w.WType == doc_type && w.KaId == ka_id && (w.SummInCurr - w.SummPay) > 0).OrderByDescending(o => o.OnDate).ToList();

            DocListEdit.Properties.DataSource = _db.GetWayBillList(DateTime.Now.AddYears(-100), DateTime.Now, Convert.ToString(TypDocsEdit.EditValue), -1, ka_id, 0, "*", DBHelper.CurrentUser.KaId)
                  .OrderByDescending(o => o.OnDate).Where(w => (w.SummInCurr - w.SummPay) > 0);
        }

        private void CashEditComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                CashEditComboBox.EditValue = IHelper.ShowDirectList(CashEditComboBox.EditValue, 4);
            }
        }

        private void AccountEdit_EditValueChanged(object sender, EventArgs e)
        {
          /*  if (AccountEdit.ContainsFocus)
            {
                var row = AccountEdit.GetSelectedDataRow() as user_acc;
                if (row != null && row.ExtDocType == -1)
                {
                    KagentComboBox.EditValue = row.KaId;
                    KagentComboBox.Enabled = false;
                }
                else
                {
                    KagentComboBox.Enabled = true;
                }
            }*/

            GetOk();
        }

        private void CashEditComboBox_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void KagentComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                KagentComboBox.EditValue = IHelper.ShowDirectList(KagentComboBox.EditValue, 1);
            }
        }

        private void CurrEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (CurrEdit.Focused)
            {
                var cur_id = Convert.ToInt32(CurrEdit.EditValue);

                var last_curr = _db.CurrencyRate.Where(w => w.OnDate <= OnDateDBEdit.DateTime.Date && w.CurrId == cur_id).OrderByDescending(o => o.OnDate).FirstOrDefault();
                if (last_curr != null)
                {
                    _pd.OnValue = last_curr.OnValue;
                }
                else
                {
                    _pd.OnValue = 1;
                }

                var curr_on_date = _db.CurrencyRate.FirstOrDefault(w => w.OnDate == OnDateDBEdit.DateTime.Date && w.CurrId == cur_id);

                if (curr_on_date == null && cur_id != 2)
                {
                    using (var frm = new frmCurrencyRate(cur_id, OnDateDBEdit.DateTime.Date))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            _pd.OnValue = frm.SumEdit.Value;
                        }
                    }
                }


            }
        }

        private void KagentComboBox_Properties_EditValueChanged(object sender, EventArgs e)
        {
            if (KagentComboBox.ContainsFocus)
            {
                if (PayDocCheckEdit.Checked)
                {
                    PayDocCheckEdit.Checked = false;

                    DocListEdit.Properties.DataSource = null;
                    DocListEdit.EditValue = null;
                }
            }

            GetOk();
        }

  
        private void PayDocCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            if(PayDocCheckEdit.Checked && PayDocCheckEdit.ContainsFocus)
            {
                GetDocList();

                DocListEdit.EditValue = null;
            }
        }

        private void PersonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1 )
            {
                PersonEdit.EditValue = IHelper.ShowDirectList(PersonEdit.EditValue, 3);
            }
        }

        private void KagentComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (KagentComboBox.EditValue != null && KagentComboBox.EditValue != DBNull.Value)
            {
                var kaid = (int)KagentComboBox.EditValue;

                RecipientAccEdit.Properties.DataSource = _db.v_KAgentAccount.AsNoTracking().Where(w => w.KAId == kaid).ToList();
            }

        }

        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                OnDateDBEdit.EditValue = DBHelper.ServerDateTime();
            }
        }

        private void ChargeTypesEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                ChargeTypesEdit.EditValue = IHelper.ShowDirectList(ChargeTypesEdit.EditValue, 6);
            }
        }

        private void AccountEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                AccountEdit.EditValue = IHelper.ShowDirectList(AccountEdit.EditValue, 17);
            }
        }

        private void RecipientAccEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                RecipientAccEdit.EditValue = IHelper.ShowDirectList(RecipientAccEdit.EditValue, 17);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ;
        }

        private void KagBalBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KagentComboBox.EditValue == DBNull.Value)
            {
                return;
            }

            IHelper.ShowKABalans((int)KagentComboBox.EditValue);
        }
    }
}
