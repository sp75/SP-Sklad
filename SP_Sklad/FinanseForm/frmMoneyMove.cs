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
using SP_Sklad.SkladData;

namespace SP_Sklad.FinanseForm
{
    public partial class frmMoneyMove : Form
    {
        private int? _DocType { get; set; }
        private BaseEntities _db { get; set; }
        private int? _PayDocId { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private PayDoc _pd { get; set; }

        public frmMoneyMove(int? DocType, int? PayDocId = null)
        {
            _DocType = DocType;
            _PayDocId = PayDocId;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction(IsolationLevel.RepeatableRead);

            InitializeComponent();
        }

        private void frmMoneyMove_Load(object sender, EventArgs e)
        {
            PTypeComboBox.Properties.DataSource = DBHelper.PayTypes;
            CashEditComboBox.Properties.DataSource = DBHelper.CashDesks;
            PersonEdit.Properties.DataSource = DBHelper.Persons;
            PayDocTypeEdit.Properties.DataSource = _db.PayDocType.Where(w=> w.Id == 6).ToList();

            if (_PayDocId == null)
            {
                _pd = _db.PayDoc.Add(new PayDoc
                {
                    Checked = 1,
                    DocNum = new BaseEntities().GetCounter("pay_doc").FirstOrDefault(),
                    OnDate = DBHelper.ServerDateTime(),
                    Total = 0,
                    CTypeId = 1,// За товар
                    WithNDS = 1,// З НДС
                    PTypeId = 1,// Наличкой
                    CashId = DBHelper.CashDesks.Where(w => w.Def == 1).Select(s => s.CashId).FirstOrDefault(),// Каса по умолчанию
                    CurrId = DBHelper.Currency.Where(w => w.Def == 1).Select(s => s.CurrId).FirstOrDefault(), //Валюта по умолчанию
                    OnValue = 1,//Курс валюти
                    MPersonId = DBHelper.CurrentUser.KaId,
                    DocType = Convert.ToInt32(_DocType)
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
                if (_pd.Total < 0)
                {
                    SumEdit.Value = _pd.Total * -1;
                    SumTypeEdit.SelectedIndex = 1;
                }
                else
                {
                    SumEdit.Value = _pd.Total;
                }

                PayDocBS.DataSource = _pd;
            }

            
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (SumTypeEdit.SelectedIndex == 0) _pd.Total = SumEdit.Value;
            else _pd.Total = SumEdit.Value * -1;

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
            _pd.OnDate = DBHelper.ServerDateTime();
            OnDateDBEdit.DateTime = _pd.OnDate;
        }

        private void PTypeComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (PTypeComboBox.EditValue == DBNull.Value )
            {
                return;
            }

            labelControl7.Visible = false;
            CashEditComboBox.Visible = false;
            simpleButton4.Visible = false;
            labelControl4.Visible = false;
            AccountEdit.Visible = false;
            simpleButton1.Visible = false;

            if ((int)PTypeComboBox.EditValue == 1)
            {
                labelControl7.Visible = true;
                CashEditComboBox.Visible = true;
                simpleButton4.Visible = true;
                _pd.AccId = null;
                
            }

            if ((int)PTypeComboBox.EditValue == 2)
            {
                labelControl4.Visible = true;
                AccountEdit.Visible = true;
                simpleButton1.Visible = true;
                _pd.CashId = null;
              
            }
            GetOk();
        }

        bool GetOk()
        {
            bool recult = (NumEdit.Text.Any() && PTypeComboBox.EditValue != null && (CashEditComboBox.EditValue != null || AccountEdit.Text.Any()) && SumEdit.Value > 0);

            OkButton.Enabled = recult;

            return recult;
        }

        private void NumEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }
    }
}
