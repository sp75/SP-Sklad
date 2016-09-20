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
using SP_Sklad.Reports;
using SP_Sklad.SkladData;

namespace SP_Sklad.FinanseForm
{
    public partial class frmPayDoc : Form
    {
        private int? _DocType { get; set; }
        BaseEntities _db { get; set; }
        private int? _PayDocId { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private PayDoc _pd { get; set; }

        public frmPayDoc(int? DocType, int? PayDocId)
        {
            _DocType = DocType;
            _PayDocId = PayDocId;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction(IsolationLevel.RepeatableRead);

            InitializeComponent();
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void frmPayDoc_Load(object sender, EventArgs e)
        {
            PTypeComboBox.Properties.DataSource = DBHelper.PayTypes;
            CashEditComboBox.Properties.DataSource = DBHelper.CashDesks;
            KagentComboBox.Properties.DataSource = DBHelper.Kagents;
            PersonEdit.Properties.DataSource = DBHelper.Persons;
            CurrEdit.Properties.DataSource = DBHelper.Currency;
            ChargeTypesEdit.Properties.DataSource = DBHelper.ChargeTypes;

            var ent_id = DBHelper.Enterprise.KaId;
            AccountEdit.Properties.DataSource = _db.EnterpriseAccount.Where(w => w.KaId == ent_id).Select(s => new { s.AccId, s.AccNum, s.BankName }).ToList();

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
                    DocType = Convert.ToInt32( _DocType),
                    UpdatedBy = DBHelper.CurrentUser.UserId
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
                TurnDocCheckBox.DataBindings.Add(new Binding("EditValue", _pd, "Checked"));
                NumEdit.DataBindings.Add(new Binding("EditValue", _pd, "DocNum"));
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", _pd, "OnDate"));
                PTypeComboBox.DataBindings.Add(new Binding("EditValue", _pd, "PTypeId"));
                CashEditComboBox.DataBindings.Add(new Binding("EditValue", _pd, "CashId", true, DataSourceUpdateMode.OnValidation));
                ChargeTypesEdit.DataBindings.Add(new Binding("EditValue", _pd, "CTypeId"));
                KagentComboBox.DataBindings.Add(new Binding("EditValue", _pd, "KaId", true, DataSourceUpdateMode.OnValidation));
                PersonEdit.DataBindings.Add(new Binding("EditValue", _pd, "MPersonId", true, DataSourceUpdateMode.OnValidation));
                SumEdit.DataBindings.Add(new Binding("EditValue", _pd, "Total"));
                CurrEdit.DataBindings.Add(new Binding("EditValue", _pd, "CurrId"));
                CurrValueEdit.DataBindings.Add(new Binding("EditValue", _pd, "OnValue"));
                SchetEdit.DataBindings.Add(new Binding("EditValue", _pd, "Schet"));
                WithNDScheckEdit.DataBindings.Add(new Binding("EditValue", _pd, "WithNDS"));
                ReasonEdit.DataBindings.Add(new Binding("EditValue", _pd, "Reason"));
                NotesEdit.DataBindings.Add(new Binding("EditValue", _pd, "Notes"));
                AccountEdit.DataBindings.Add(new Binding("EditValue", _pd, "AccId", true, DataSourceUpdateMode.OnValidation));
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

                //  DOCTYP->Filter = "ID = 1 or ID = 6 or ID = 16";
            }
            else
            {
                Text = "Властивості вхідного платежу";
                labelControl8.Text = "Платник:";
                //  DOCTYP->Filter = "ID < 0 or ID = 2";
            }

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var state = _db.Entry<PayDoc>(_pd).State;

            _db.SaveChanges();
            current_transaction.Commit();
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

            PrintDoc.Show(_PayDocId.Value, _pd.DocType == -2 ? _pd.DocType : _pd.DocType * 3, _db);
        }

        private void PTypeComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (PTypeComboBox.EditValue == DBNull.Value)
            {
                return;
            }

            labelControl7.Visible = false;
            CashEditComboBox.Visible = false;
            simpleButton4.Visible = false;

            labelControl18.Visible = false;
            AccountEdit.Visible = false;
            simpleButton9.Visible = false;

            if ((int)PTypeComboBox.EditValue == 1)
            {
                labelControl7.Visible = true;
                CashEditComboBox.Visible = true;
                simpleButton4.Visible = true;
                _pd.AccId = null;

            }

            if ((int)PTypeComboBox.EditValue == 2)
            {
                labelControl18.Visible = true;
                AccountEdit.Visible = true;
                simpleButton9.Visible = true;
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

        private void frmPayDoc_Shown(object sender, EventArgs e)
        {
            GetOk();
        }
    }
}
