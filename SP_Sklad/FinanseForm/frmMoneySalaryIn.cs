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
    public partial class frmMoneySalaryIn : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private int? _PayDocId { get; set; }
        private DbContextTransaction current_transaction { get; set; }
   //     private PayDoc _pd_from { get; set; }
        private PayDoc _pd_to { get; set; }

        public frmMoneySalaryIn(int? PayDocId = null)
        {
            _PayDocId = PayDocId;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction(/*IsolationLevel.RepeatableRead*/);

            InitializeComponent();
        }

        private void frmMoneyMove_Load(object sender, EventArgs e)
        {
            PTypeFromEdit.Properties.DataSource = DBHelper.PayTypes;
            PersonToEdit.Properties.DataSource = DBHelper.Persons;

            CashFromEdit.Properties.DataSource = DBHelper.CashDesks;

            ChargeTypesEdit.Properties.DataSource = DBHelper.ChargeTypes;

            var ent_id = DBHelper.CurrentEnterprise.KaId ;
            AccountFromEdit.Properties.DataSource = _db.EnterpriseAccount.Where(w => w.KaId == ent_id).Select(s => new { s.AccId, s.AccNum, s.BankName }).ToList();

            PersonEdit.Properties.DataSource = DBHelper.Persons;

            if (_PayDocId == null)
            {
                var on_date =  DBHelper.ServerDateTime();
         /*       var oper_id = Guid.NewGuid();

                _pd_from = _db.PayDoc.Add(new PayDoc
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
                    DocType = -3, //Списання коштів з каси
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId,
                    OperId = oper_id
                });*/

                _pd_to = _db.PayDoc.Add(new PayDoc
                {
                    Id = Guid.NewGuid(),
                    Checked = 1,
                    DocNum = new BaseEntities().GetDocNum("salary_in").FirstOrDefault(),
                    OnDate = on_date,
                    Total = 0,
                    CTypeId = 1,// За товар
                    WithNDS = 1,// З НДС
                    PTypeId = 2,// Безготівковий
                    CurrId = DBHelper.Currency.Where(w => w.Def == 1).Select(s => s.CurrId).FirstOrDefault(), //Валюта по умолчанию
                    OnValue = 1,//Курс валюти
                    MPersonId = DBHelper.CurrentUser.KaId,
                    DocType = -10, // Зарахування зарплати на карточку працівника
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.CurrentEnterprise.KaId,
               //     OperId = oper_id
                });
            }
            else
            {
                try
                {
                    _pd_to = _db.Database.SqlQuery<PayDoc>("SELECT * from PayDoc WITH (UPDLOCK, NOWAIT) where PayDocId = {0}", _PayDocId).FirstOrDefault();
                    _db.Entry<PayDoc>(_pd_to).State = System.Data.Entity.EntityState.Modified;

                /*    _pd_from = _db.Database.SqlQuery<PayDoc>("SELECT * from PayDoc WITH (UPDLOCK, NOWAIT) where OperId = {0} and DocType = -3 ", _pd_to.OperId).FirstOrDefault();
                    _db.Entry<PayDoc>(_pd_from).State = System.Data.Entity.EntityState.Modified;*/

                }
                catch
                {
                    Close();
                }

            }

       /*     if (_pd_from != null)
            {
                _pd_from.UpdatedBy = DBHelper.CurrentUser.UserId;

                SumEdit.Value = _pd_from.Total;

                PayDocFromBS.DataSource = _pd_from;
            }*/

            if (_pd_to != null)
            {
                _pd_to.UpdatedBy = DBHelper.CurrentUser.UserId;
                PayDocToBS.DataSource = _pd_to;
            }


        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            //_pd_from.Total = SumEdit.Value;
            _pd_to.Total = SumEdit.Value;

    /*        _pd_to.Checked = _pd_from.Checked;
          //  _pd_to.DocNum = _pd_from.DocNum;
            _pd_to.OnDate = _pd_from.OnDate;
            _pd_to.MPersonId = _pd_from.MPersonId;
            _pd_to.Reason = _pd_from.Reason;
            _pd_to.Notes = _pd_from.Notes;
            _pd_to.CTypeId = _pd_from.CTypeId;*/

            _db.SaveChanges();

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
            bool source_from = PTypeFromEdit.EditValue != null && PTypeFromEdit.EditValue != DBNull.Value && (((int)PTypeFromEdit.EditValue == 1 && CashFromEdit.EditValue != DBNull.Value) || ((int)PTypeFromEdit.EditValue == 2 && AccountFromEdit.EditValue != DBNull.Value));
            bool source_to = PersonToEdit.EditValue != null && PersonToEdit.EditValue != DBNull.Value ;

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
                CashFromEdit.EditValue = IHelper.ShowDirectList(CashFromEdit.EditValue, 4);
            }
        }

        private void PTypeFromEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (PTypeFromEdit.EditValue == DBNull.Value)
            {
                return;
            }

            labelControl13.Visible = false;
            CashFromEdit.Visible = false;
            labelControl12.Visible = false;
            AccountFromEdit.Visible = false;

            if ((int)PTypeFromEdit.EditValue == 1)
            {
                labelControl13.Visible = true;
                CashFromEdit.Visible = true;
                _pd_to.AccId = null;
            }

            if ((int)PTypeFromEdit.EditValue == 2)
            {
                labelControl12.Visible = true;
                AccountFromEdit.Visible = true;
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
       /*     if (PersonToEdit.EditValue == DBNull.Value)
            {
                return;
            }
            int ka_id = (int)PersonToEdit.EditValue;

            AccountToEdit.Properties.DataSource = _db.KAgentAccount.Where(w => w.Kagent.KType == 2 && w.KAId == ka_id).Select(s => new { s.AccId, s.AccNum, BankName = s.Kagent.Name }).ToList();*/

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
    }
}
