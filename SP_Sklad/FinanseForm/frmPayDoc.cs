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

        public frmPayDoc(int? DocType, int? PayDocId, decimal? SummPay = 0)
        {
            _DocType = DocType;
            _PayDocId = PayDocId;
            _summ_pay = SummPay;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction(/*IsolationLevel.RepeatableRead*/);

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
                int w_type = _DocType.Value != -2 ? _DocType.Value * 3 : _DocType.Value;
                string doc_setting_name = w_type == -3 ? "pay_doc_out" : w_type == 3 ? "pay_doc_in" : "pay_doc";
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
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    KaId = _ka_id,
                    EntId = DBHelper.Enterprise.KaId
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
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", _pd, "OnDate", true, DataSourceUpdateMode.OnPropertyChanged));
                PTypeComboBox.DataBindings.Add(new Binding("EditValue", _pd, "PTypeId", true, DataSourceUpdateMode.OnPropertyChanged));
                CashEditComboBox.DataBindings.Add(new Binding("EditValue", _pd, "CashId", true, DataSourceUpdateMode.OnPropertyChanged));
                ChargeTypesEdit.DataBindings.Add(new Binding("EditValue", _pd, "CTypeId", true, DataSourceUpdateMode.OnPropertyChanged));
                KagentComboBox.DataBindings.Add(new Binding("EditValue", _pd, "KaId", true, DataSourceUpdateMode.OnPropertyChanged));
                PersonEdit.DataBindings.Add(new Binding("EditValue", _pd, "MPersonId", true, DataSourceUpdateMode.OnPropertyChanged));
                SumEdit.DataBindings.Add(new Binding("EditValue", _pd, "Total"));
                CurrEdit.DataBindings.Add(new Binding("EditValue", _pd, "CurrId"));
                CurrValueEdit.DataBindings.Add(new Binding("EditValue", _pd, "OnValue"));
                SchetEdit.DataBindings.Add(new Binding("EditValue", _pd, "Schet"));
                WithNDScheckEdit.DataBindings.Add(new Binding("EditValue", _pd, "WithNDS"));
                ReasonEdit.DataBindings.Add(new Binding("EditValue", _pd, "Reason"));
                NotesEdit.DataBindings.Add(new Binding("EditValue", _pd, "Notes"));
                AccountEdit.DataBindings.Add(new Binding("EditValue", _pd, "AccId", true, DataSourceUpdateMode.OnPropertyChanged));
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

                TypDocsEdit.Properties.DataSource = DBHelper.DocTypeList.Where(w => w.Id == 1 || w.Id == 6 || w.Id == 16).ToList();
                if (TypDocsEdit.EditValue == null) TypDocsEdit.EditValue = 1;
            }
            else
            {
                Text = "Властивості вхідного платежу";
                labelControl13.Text = "Платник:";
                TypDocsEdit.Properties.DataSource = DBHelper.DocTypeList.Where(w => new int[] { -1, -6, 2, -16, -8, }.Any(a => a.Equals(w.Id))).ToList();
                if (TypDocsEdit.EditValue == null) TypDocsEdit.EditValue = -1;
            }

            var rl = _db.GetRelDocList(_pd.Id).FirstOrDefault();
            if (rl != null)
            {
                PayDocCheckEdit.Checked = true;
                TypDocsEdit.EditValue = rl.DocType;
                GetDocList();
                DocListEdit.EditValue = rl.Id;

                var row = DocListEdit.GetSelectedDataRow() as GetWayBillList_Result;
                textEdit4.EditValue = row.Balans - SumEdit.Value;
            }

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (_pd.OnDate.Date <= _db.CommonParams.First().EndCalcPeriod)
            {
                MessageBox.Show("Період вже закритий. Змініть дату документа!", "Відміна/Проведення платіжного документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var rl = _db.GetRelDocList(_pd.Id).ToList();
            foreach (var item in rl)
            {
                _db.DeleteWhere<DocRels>(w => w.OriginatorId == item.Id && w.RelOriginatorId == _pd.Id);
            }
            _db.SaveChanges();

            if (PayDocCheckEdit.Checked && DocListEdit.EditValue != null)
            {
                var row = DocListEdit.GetSelectedDataRow() as GetWayBillList_Result;

                _pd = _db.PayDoc.AsNoTracking().FirstOrDefault(w => w.PayDocId == _pd.PayDocId);
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
            bool kg = (_DocType.Value == -2 ) || (KagentComboBox.EditValue != null && KagentComboBox.EditValue != DBNull.Value && (_DocType.Value != -2 ) ) ;

            bool recult = (NumEdit.Text.Any() && PTypeComboBox.EditValue != null && (CashEditComboBox.EditValue != null || AccountEdit.Text.Any()) && SumEdit.Value > 0 && kg);

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
                 textEdit4.EditValue = row.Balans - SumEdit.Value;
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

            DocListEdit.Properties.DataSource = DB.SkladBase().GetWayBillList(DateTime.Now.AddYears(-100), DateTime.Now, (int)TypDocsEdit.EditValue, -1, ka_id, 0, "*", DBHelper.CurrentUser.KaId)
                .OrderByDescending(o => o.OnDate).Where(w => (w.SummAll - w.SummPay) > 0);
        }

        private void KagentComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (KagentComboBox.ContainsFocus)
            {
                GetDocList();
                DocListEdit.EditValue = null;
            }

            GetOk();
        }

        private void CashEditComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                CashEditComboBox.EditValue = IHelper.ShowDirectList(CashEditComboBox.EditValue, 4);
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            ChargeTypesEdit.EditValue = IHelper.ShowDirectList(ChargeTypesEdit.EditValue, 6);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            PersonEdit.EditValue = IHelper.ShowDirectList(PersonEdit.EditValue, 3);
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            KagentComboBox.EditValue = IHelper.ShowDirectList(KagentComboBox.EditValue, 1);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            OnDateDBEdit.EditValue = DBHelper.ServerDateTime();
        }
    }
}
