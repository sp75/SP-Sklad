﻿using System;
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
    public partial class frmMoneyIn : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private int? _PayDocId { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private PayDoc _pd { get; set; }
        private PayDoc _pd_additional_costs { get; set; }

        public frmMoneyIn( int? PayDocId = null)
        {
            _PayDocId = PayDocId;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction(/*IsolationLevel.RepeatableRead*/);

            InitializeComponent();
        }

        private void frmMoneyMove_Load(object sender, EventArgs e)
        {
            PTypeComboBox.Properties.DataSource = DBHelper.PayTypes;
            CashEditComboBox.Properties.DataSource = DBHelper.CashDesks;
            PersonEdit.Properties.DataSource = DBHelper.Persons;
            RecipientAccEdit.Properties.DataSource = _db.v_KAgentAccount.Where(w => w.KType != 3).ToList();
            ChargeTypesEdit.Properties.DataSource = DBHelper.ChargeTypes;

            var ent_id = DBHelper.Enterprise.KaId ;
            AccountEdit.Properties.DataSource = _db.EnterpriseAccount.Where(w => w.KaId == ent_id).Select(s=> new {s.AccId, s.AccNum, s.BankName }).ToList();

            if (_PayDocId == null)
            {
                _pd = _db.PayDoc.Add(new PayDoc
                {
                    Id = Guid.NewGuid(),
                    Checked = 1,
                    DocNum = new BaseEntities().GetDocNum("pay_doc").FirstOrDefault(),
                    OnDate = DBHelper.ServerDateTime(),
                    Total = 0,
                    CTypeId = 1,// За товар
                    WithNDS = 1,// З НДС
                    PTypeId = 1,// Наличкой
                    CashId = DBHelper.CashDesks.Where(w => w.Def == 1).Select(s => s.CashId).FirstOrDefault(),// Каса по умолчанию
                    CurrId = DBHelper.Currency.Where(w => w.Def == 1).Select(s => s.CurrId).FirstOrDefault(), //Валюта по умолчанию
                    OnValue = 1,//Курс валюти
                    MPersonId = DBHelper.CurrentUser.KaId,
                    DocType = 11,//Коригування залишку
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = UserSession.EnterpriseId,
                    OperId = Guid.NewGuid()
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

            if (_pd != null)
            {
                 _pd.UpdatedBy = DBHelper.CurrentUser.UserId;
                PayDocBS.DataSource = _pd;
            }

            _pd_additional_costs = _db.PayDoc.FirstOrDefault(w => w.OperId == _pd.OperId && w.DocType == -2);

            if(_pd_additional_costs == null)
            {
                _pd_additional_costs = _pd_additional_costs = new PayDoc
                {
                    Id = Guid.NewGuid(),
                    Checked = 1,
                    DocNum = new BaseEntities().GetDocNum("pay_doc").FirstOrDefault(),
                    OnDate = DBHelper.ServerDateTime(),
                    Total = 0,
                    CTypeId = DBHelper.ChargeTypes.Any(a => a.Def == 1) ? DBHelper.ChargeTypes.FirstOrDefault(w => w.Def == 1).CTypeId : DBHelper.ChargeTypes.FirstOrDefault().CTypeId,// За товар
                    WithNDS = 1,// З НДС
                    PTypeId = 1,// Наличкой
                    CashId = DBHelper.CashDesks.Where(w => w.Def == 1).Select(s => s.CashId).FirstOrDefault(),// Каса по умолчанию
                    CurrId = DBHelper.Currency.Where(w => w.Def == 1).Select(s => s.CurrId).FirstOrDefault(), //Валюта по умолчанию
                    OnValue = 1,//Курс валюти
                    MPersonId = DBHelper.CurrentUser.KaId,
                    DocType = -2, //Додаткові витрати
                    UpdatedBy = UserSession.UserId,
                    //  KaId = _ka_id,
                    EntId = UserSession.EnterpriseId,
                    ReportingDate = DBHelper.ServerDateTime(),
                    OperId = _pd.OperId
                };
            }

            AdditionalCostsBS.DataSource = _pd_additional_costs;

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _pd_additional_costs.Checked = _pd.Checked;
            _pd_additional_costs.AccId = _pd.AccId;
            _pd_additional_costs.CashId = _pd.CashId;
            _pd_additional_costs.PTypeId = _pd.PTypeId;

            if (_pd_additional_costs.Total > 0)
            {
                if (_db.Entry<PayDoc>(_pd_additional_costs).State == System.Data.Entity.EntityState.Detached)
                {
                    _db.Entry<PayDoc>(_pd_additional_costs).State = System.Data.Entity.EntityState.Added;
                }
            }
/*            else if (_db.Entry<PayDoc>(_pd_additional_costs).State == System.Data.Entity.EntityState.Modified)
            {
                _db.Entry<PayDoc>(_pd_additional_costs).State = System.Data.Entity.EntityState.Deleted;
            }*/


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

 
        private void PTypeComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (PTypeComboBox.EditValue == DBNull.Value )
            {
                return;
            }

            labelControl7.Visible = false;
            CashEditComboBox.Visible = false;
            labelControl4.Visible = false;
            AccountEdit.Visible = false;

            if ((int)PTypeComboBox.EditValue == 1)
            {
                labelControl7.Visible = true;
                CashEditComboBox.Visible = true;
                _pd.AccId = null;
            }

            if ((int)PTypeComboBox.EditValue == 2)
            {
                labelControl4.Visible = true;
                AccountEdit.Visible = true;
                _pd.CashId = null;
            }

            GetOk();
        }

        bool GetOk()
        {
            bool source_from = PTypeComboBox.EditValue != null && PTypeComboBox.EditValue != DBNull.Value && (((int)PTypeComboBox.EditValue == 1 && CashEditComboBox.EditValue != DBNull.Value) || ((int)PTypeComboBox.EditValue == 2 && AccountEdit.EditValue != DBNull.Value));

            bool recult = (NumEdit.Text.Any() && source_from && SumEdit.Value > 0);

            OkButton.Enabled = recult;

            return recult;
        }

        private void NumEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
          
        }

        private void CashEditComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                CashEditComboBox.EditValue = IHelper.ShowDirectList(CashEditComboBox.EditValue, 4);
            }
        }

        private void PayDocTypeEdit_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                OnDateDBEdit.DateTime = DBHelper.ServerDateTime();
            }
        }

        private void PersonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                PersonEdit.EditValue = IHelper.ShowDirectList(PersonEdit.EditValue, 3);
            }
        }

        private void ChargeTypesEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                ChargeTypesEdit.EditValue = IHelper.ShowDirectList(ChargeTypesEdit.EditValue, 6);
            }
        }
    }
}
