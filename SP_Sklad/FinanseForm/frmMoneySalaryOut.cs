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
using SP_Sklad.Common;
using SP_Sklad.SkladData;

namespace SP_Sklad.FinanseForm
{
    public partial class frmMoneySalaryOut : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private int? _PayDocId { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private PayDoc _pd_from { get; set; }
        private PayDoc _pd_to { get; set; }
        private decimal? _summ_pay { get; set; }
        public int? _ka_id { get; internal set; }

        public frmMoneySalaryOut(int? PayDocId = null, decimal? SummPay = 0)
        {
            _PayDocId = PayDocId;
            _summ_pay = SummPay;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction(/*IsolationLevel.RepeatableRead*/);

            InitializeComponent();
        }

        private void frmMoneyMove_Load(object sender, EventArgs e)
        {
            PTypeToEdit.Properties.DataSource = DBHelper.PayTypes;
            PersonFromEdit.Properties.DataSource = DBHelper.Persons;

            CashToEdit.Properties.DataSource = DBHelper.CashDesks;

            ChargeTypesEdit.Properties.DataSource = DBHelper.ChargeTypes;

            var ent_id = DBHelper.Enterprise.KaId;
            AccountToEdit.Properties.DataSource = _db.EnterpriseAccount.Where(w => w.KaId == ent_id).Select(s => new { s.AccId, s.AccNum, s.BankName }).ToList();

            PersonEdit.Properties.DataSource = DBHelper.Persons;

            if (_PayDocId == null)
            {
                var on_date = DBHelper.ServerDateTime();
                var oper_id = Guid.NewGuid();

                _pd_from = _db.PayDoc.Add(new PayDoc
                {
                    Id = Guid.NewGuid(),
                    Checked = 1,
                    DocNum = new BaseEntities().GetDocNum("salary_out").FirstOrDefault(),
                    OnDate = on_date,
                    Total = 0,
                    CTypeId = 1,// За товар
                    WithNDS = 1,// З НДС
                    PTypeId = 2,// Безготівковий
                    CurrId = DBHelper.Currency.Where(w => w.Def == 1).Select(s => s.CurrId).FirstOrDefault(), //Валюта по умолчанию
                    OnValue = 1,//Курс валюти
                    MPersonId = DBHelper.CurrentUser.KaId,
                    DocType = -10, // Списання зарплати з карточки працівника
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId,
                    OperId = oper_id,
                    KaId = _ka_id
                });

                _pd_to = _db.PayDoc.Add(new PayDoc
                {
                    Id = Guid.NewGuid(),
                    Checked = 1,
                    DocNum = new BaseEntities().GetDocNum("pay_doc").FirstOrDefault(),
                    OnDate = on_date,
                    Total = 0,
                    CTypeId = 1,// За товар
                    WithNDS = 1,// З НДС
                    PTypeId = 1,// Наличкой
                    CashId = DBHelper.CashDesks.Where(w => w.Def == 1).Select(s => s.CashId).FirstOrDefault(),// Каса по умолчанию
                    CurrId = DBHelper.Currency.Where(w => w.Def == 1).Select(s => s.CurrId).FirstOrDefault(), //Валюта по умолчанию
                    OnValue = 1,//Курс валюти
                    MPersonId = DBHelper.CurrentUser.KaId,
                    DocType = 3, //Зарахування коштів в касу
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId,
                    OperId = oper_id,
                    KaId = _ka_id
                });

                SumEdit.EditValue = _summ_pay == null ? 0 : _summ_pay.Value;
            }
            else
            {
                var pd = _db.PayDoc.AsNoTracking().FirstOrDefault(w => w.PayDocId == _PayDocId);
                if (pd != null)
                {
                    _pd_from = _db.Database.SqlQuery<PayDoc>("SELECT * from PayDoc WITH (UPDLOCK, NOWAIT) where OperId = {0} and DocType = -10 ", pd.OperId).FirstOrDefault();
                    _db.Entry<PayDoc>(_pd_from).State = System.Data.Entity.EntityState.Modified;

                    _pd_to = _db.Database.SqlQuery<PayDoc>("SELECT * from PayDoc WITH (UPDLOCK, NOWAIT) where OperId = {0} and DocType = 3", pd.OperId).FirstOrDefault();
                    _db.Entry<PayDoc>(_pd_to).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    new Exception("Документ не знайдено");
                }

                SumEdit.Value = pd.Total;
            }

            if (_pd_from != null)
            {
                _pd_from.UpdatedBy = DBHelper.CurrentUser.UserId;
                PayDocFromBS.DataSource = _pd_from;
            }

            if (_pd_to != null)
            {
                _pd_to.UpdatedBy = DBHelper.CurrentUser.UserId;
                PayDocToBS.DataSource = _pd_to;
            }

            TypDocsEdit.Properties.DataSource = DBHelper.DocTypeList.Where(w => new int[] { -1, -6, 2, -16, -8, }.Any(a => a.Equals(w.Id))).ToList();

            if (TypDocsEdit.EditValue == null) TypDocsEdit.EditValue = -1;

            var rl = _db.GetRelDocList(_pd_to.Id).FirstOrDefault();
            if (rl != null)
            {
                PayDocCheckEdit.Checked = true;
                TypDocsEdit.EditValue = rl.DocType;
                GetDocList();
                DocListEdit.EditValue = rl.Id;

                var row = DocListEdit.GetSelectedDataRow() as GetWayBillList_Result;

                textEdit4.EditValue = (row == null ? 0 : row.Balans) - SumEdit.Value;
            }

        }

        public void GetDocList()
        {
            if (TypDocsEdit.EditValue == null)
            {
                return;
            }
            var ka_id = PersonFromEdit.EditValue == null || PersonFromEdit.EditValue == DBNull.Value ? 0 : (int)PersonFromEdit.EditValue;

            DocListEdit.Properties.DataSource = DB.SkladBase().GetWayBillList(DateTime.Now.AddYears(-100), DateTime.Now, Convert.ToString(TypDocsEdit.EditValue), -1, ka_id, 0, "*", DBHelper.CurrentUser.KaId)
                .OrderByDescending(o => o.OnDate).Where(w => (w.SummInCurr - w.SummPay) > 0);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _pd_from.Total = SumEdit.Value;
            _pd_to.Total = SumEdit.Value;

            _pd_to.Checked = _pd_from.Checked;
          //  _pd_to.DocNum = _pd_from.DocNum;
            _pd_to.OnDate = _pd_from.OnDate;
            _pd_to.MPersonId = _pd_from.MPersonId;
            _pd_to.Reason = _pd_from.Reason;
            _pd_to.Notes = _pd_from.Notes;
            _pd_to.CTypeId = _pd_from.CTypeId;
            _pd_to.KaId = _pd_from.KaId;

           var rl = _db.GetRelDocList(_pd_to.Id).ToList();
            foreach (var item in rl)
            {
                _db.DeleteWhere<DocRels>(w => w.OriginatorId == item.Id && w.RelOriginatorId == _pd_to.Id);
            }
            _db.SaveChanges();

            if (PayDocCheckEdit.Checked && DocListEdit.EditValue != null)
            {
                var row = DocListEdit.GetSelectedDataRow() as GetWayBillList_Result;

       //         _pd = _db.PayDoc.AsNoTracking().FirstOrDefault(w => w.PayDocId == _pd_to.PayDocId);
                _db.SetDocRel(row.Id, _pd_to.Id);

                _db.SaveChanges();
            }

            current_transaction.Commit();
        }

        private void frmMoneyMove_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

 
        bool GetOk()
        {
            bool source_from = PTypeToEdit.EditValue != null && PTypeToEdit.EditValue != DBNull.Value && (((int)PTypeToEdit.EditValue == 1 && CashToEdit.EditValue != DBNull.Value) || ((int)PTypeToEdit.EditValue == 2 && AccountToEdit.EditValue != DBNull.Value));
            bool source_to = PersonFromEdit.EditValue != null && PersonFromEdit.EditValue != DBNull.Value && AccountFromEdit.EditValue != DBNull.Value;

            bool recult = (NumEdit.Text.Any() &&  source_from && source_to && SumEdit.Value > 0);

            OkButton.Enabled = recult;

            return recult;
        }

        private void NumEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void CashFromEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                CashToEdit.EditValue = IHelper.ShowDirectList(CashToEdit.EditValue, 4);
            }
        }

        private void PTypeFromEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (PTypeToEdit.EditValue == DBNull.Value)
            {
                return;
            }

            labelControl13.Visible = false;
            CashToEdit.Visible = false;
            labelControl12.Visible = false;
            AccountToEdit.Visible = false;

            if ((int)PTypeToEdit.EditValue == 1)
            {
                labelControl13.Visible = true;
                CashToEdit.Visible = true;
                _pd_to.AccId = null;
            }

            if ((int)PTypeToEdit.EditValue == 2)
            {
                labelControl12.Visible = true;
                AccountToEdit.Visible = true;
                _pd_to.CashId = null;
            }

            GetOk();
        }

 
        private void OnDateDBEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                OnDateDBEdit.DateTime = DBHelper.ServerDateTime();
            }
        }

        private void PersonToEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (PersonFromEdit.EditValue == DBNull.Value)
            {
                return;
            }

            int ka_id = (int)PersonFromEdit.EditValue;

            AccountFromEdit.Properties.DataSource = _db.KAgentAccount.Where(w => w.Kagent.KType == 2 && w.KAId == ka_id).Select(s => new { s.AccId, s.AccNum, BankName = s.Kagent.Name }).ToList();


            if (PersonFromEdit.ContainsFocus)
            {
                GetDocList();
                DocListEdit.EditValue = null;
            }

            GetOk();
        }

        private void ChargeTypesEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                ChargeTypesEdit.EditValue = IHelper.ShowDirectList(ChargeTypesEdit.EditValue, 6);
            }
        }

        private void PersonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                PersonEdit.EditValue = IHelper.ShowDirectList(PersonEdit.EditValue, 3);
            }
        }

        private void TypDocsEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!TypDocsEdit.ContainsFocus)
            {
                return;
            }

            GetDocList();
        }

        private void PayDocCheckEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (PayDocCheckEdit.ContainsFocus)
            {
                GetDocList();
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
                textEdit4.EditValue = row.Balans - SumEdit.Value;
            }

            GetOk();
        }
    }
}
