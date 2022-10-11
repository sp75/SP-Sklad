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
    public partial class frmMoneyMove : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private int? _PayDocId { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private PayDoc _pd_from { get; set; }
        private PayDoc _pd_to { get; set; }

        public frmMoneyMove(int? PayDocId = null)
        {
            _PayDocId = PayDocId;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction(/*IsolationLevel.RepeatableRead*/);

            InitializeComponent();
        }

        private void frmMoneyMove_Load(object sender, EventArgs e)
        {
            PTypeFromEdit.Properties.DataSource = DBHelper.PayTypes;
            PTypeToEdit.Properties.DataSource = DBHelper.PayTypes;

            CashFromEdit.Properties.DataSource = DBHelper.CashDesks;
            CashToEdit.Properties.DataSource = DBHelper.CashDesks;

            var ent_id = DBHelper.Enterprise.KaId ;
            AccountFromEdit.Properties.DataSource = _db.EnterpriseAccount./*Where(w => w.KaId == ent_id).*/Select(s => new { s.AccId, s.AccNum, s.BankName, s.KaName }).ToList();
            AccountToEdit.Properties.DataSource = AccountFromEdit.Properties.DataSource;

            PersonEdit.Properties.DataSource = DBHelper.Persons;

            if (_PayDocId == null)
            {
                var doc_num = new BaseEntities().GetDocNum("pay_doc").FirstOrDefault();
                var on_date =  DBHelper.ServerDateTime();
                var oper_id = Guid.NewGuid();

                _pd_from = _db.PayDoc.Add(new PayDoc
                {
                    Id = Guid.NewGuid(),
                    Checked = 1,
                    DocNum = doc_num,
                    OnDate = on_date,
                    Total = 0,
                    CTypeId = 1,// За товар
                    WithNDS = 1,// З НДС
                    PTypeId = 1,// Наличкой
                    CashId = DBHelper.CashDesks.Where(w => w.Def == 1).Select(s => s.CashId).FirstOrDefault(),// Каса по умолчанию
                    CurrId = DBHelper.Currency.Where(w => w.Def == 1).Select(s => s.CurrId).FirstOrDefault(), //Валюта по умолчанию
                    OnValue = 1,//Курс валюти
                    MPersonId = DBHelper.CurrentUser.KaId,
                    DocType = -3,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId,
                    OperId = oper_id
                });

                _pd_to = _db.PayDoc.Add(new PayDoc
                {
                    Id = Guid.NewGuid(),
                    Checked = 1,
                    DocNum = doc_num,
                    OnDate = on_date,
                    Total = 0,
                    CTypeId = 1,// За товар
                    WithNDS = 1,// З НДС
                    PTypeId = 1,// Наличкой
                    CashId = DBHelper.CashDesks.Where(w => w.Def == 1).Select(s => s.CashId).FirstOrDefault(),// Каса по умолчанию
                    CurrId = DBHelper.Currency.Where(w => w.Def == 1).Select(s => s.CurrId).FirstOrDefault(), //Валюта по умолчанию
                    OnValue = 1,//Курс валюти
                    MPersonId = DBHelper.CurrentUser.KaId,
                    DocType = 3,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId,
                    OperId = oper_id
                });
            }
            else
            {
                try
                {
                    _pd_to = _db.Database.SqlQuery<PayDoc>("SELECT * from PayDoc WITH (UPDLOCK, NOWAIT) where PayDocId = {0}", _PayDocId).FirstOrDefault();
                    _db.Entry<PayDoc>(_pd_to).State = System.Data.Entity.EntityState.Modified;

                    _pd_from = _db.Database.SqlQuery<PayDoc>("SELECT * from PayDoc WITH (UPDLOCK, NOWAIT) where OperId = {0} and DocType = -3 ", _pd_to.OperId).FirstOrDefault();
                    _db.Entry<PayDoc>(_pd_from).State = System.Data.Entity.EntityState.Modified;

                }
                catch
                {
                    Close();
                }

            }

            if (_pd_from != null)
            {
                _pd_from.UpdatedBy = DBHelper.CurrentUser.UserId;

                SumEdit.Value = _pd_from.Total;

                PayDocFromBS.DataSource = _pd_from;
            }

            if (_pd_to != null)
            {
                _pd_to.UpdatedBy = DBHelper.CurrentUser.UserId;
                PayDocToBS.DataSource = _pd_to;
            }


        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _pd_from.Total = SumEdit.Value;
            _pd_to.Total = SumEdit.Value;

            _pd_to.Checked = _pd_from.Checked;
            _pd_to.DocNum = _pd_from.DocNum;
            _pd_to.OnDate = _pd_from.OnDate;
            _pd_to.MPersonId = _pd_from.MPersonId;
            _pd_to.Reason = _pd_from.Reason;
            _pd_to.Notes = _pd_from.Notes;

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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
           
        }

 
        bool GetOk()
        {
            bool source_from = PTypeFromEdit.EditValue != null && PTypeFromEdit.EditValue != DBNull.Value && (((int)PTypeFromEdit.EditValue == 1 && CashFromEdit.EditValue != DBNull.Value) || ((int)PTypeFromEdit.EditValue == 2 && AccountFromEdit.EditValue != DBNull.Value));
            bool source_to = PTypeToEdit.EditValue != null && PTypeToEdit.EditValue != DBNull.Value && (((int)PTypeToEdit.EditValue == 1 && CashToEdit.EditValue != DBNull.Value) || ((int)PTypeToEdit.EditValue == 2 && AccountToEdit.EditValue != DBNull.Value));

            bool recult = (NumEdit.Text.Any() &&  source_from && source_to && SumEdit.Value > 0);

            OkButton.Enabled = recult;

            return recult;
        }

        private void NumEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            PersonEdit.EditValue = IHelper.ShowDirectList(PersonEdit.EditValue, 3);
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
                _pd_from.AccId = null;
            }

            if ((int)PTypeFromEdit.EditValue == 2)
            {
                labelControl12.Visible = true;
                AccountFromEdit.Visible = true;
                _pd_from.CashId = null;
            }

            GetOk();
        }

        private void PTypeToEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (PTypeToEdit.EditValue == DBNull.Value)
            {
                return;
            }

            labelControl14.Visible = false;
            CashToEdit.Visible = false;
            labelControl15.Visible = false;
            AccountToEdit.Visible = false;

            if ((int)PTypeToEdit.EditValue == 1)
            {
                labelControl14.Visible = true;
                CashToEdit.Visible = true;
                _pd_to.AccId = null;
            }

            if ((int)PTypeToEdit.EditValue == 2)
            {
                labelControl15.Visible = true;
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

        private void CashToEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                CashToEdit.EditValue = IHelper.ShowDirectList(CashToEdit.EditValue, 4);
            }
        }
    }
}
