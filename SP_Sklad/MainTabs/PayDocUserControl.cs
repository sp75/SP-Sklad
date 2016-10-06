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

namespace SP_Sklad.MainTabs
{
    public partial class PayDocUserControl : UserControl
    {
        BaseEntities _db { get; set; }
        private WaybillList _wb { get; set; }
        private PayDoc _pd { get; set; }

        public PayDocUserControl()
        {
            InitializeComponent();
        }

        public void OnLoad(BaseEntities db, WaybillList wb)
        {
            _db = db;
            _wb = wb;

            var rel = db.GetRelDocList(wb.DocId).FirstOrDefault(w => w.DocType == -3 || w.DocType == 3);

            if (rel != null)
            {
                try
                {
                    _pd = db.Database.SqlQuery<PayDoc>("select * from  PayDoc WITH (UPDLOCK, NOWAIT) where DocId = {0}", rel.DocId).FirstOrDefault();
                }
                catch
                {
                    panelControl1.Enabled = false;
                }

                if ( _pd != null )
                {
                    _db.Entry<PayDoc>(_pd).State = System.Data.Entity.EntityState.Modified;

                    ExecPayCheckBox.EditValue = _pd.Checked;
                    NumEdit.EditValue = _pd.DocNum;
                    PTypeComboBox.EditValue = _pd.PTypeId;
                    CashEditComboBox.EditValue = _pd.CashId;
                    PersonEdit.EditValue = _pd.MPersonId;
                    SumEdit.EditValue = _pd.Total;
                    CurrEdit.EditValue = _pd.CurrId;
                }
            }
            else
            {
                ExecPayCheckBox.EditValue = 0;
                PTypeComboBox.EditValue = 1;
            }

            PTypeComboBox.Properties.DataSource = DBHelper.PayTypes;
            CashEditComboBox.Properties.DataSource = DBHelper.CashDesks;
            PersonEdit.Properties.DataSource = DBHelper.Persons;

            var ent_id = DBHelper.Enterprise.KaId;
            AccountEdit.Properties.DataSource = _db.EnterpriseAccount.Where(w => w.KaId == ent_id).Select(s => new { s.AccId, s.AccNum, s.BankName }).ToList();
        }

        private void ExecPayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ( _pd != null )
            {
                return;
            }

            SumEdit.EditValue = _db.WaybillDet.Where(w => w.WbillId == _wb.WbillId).Sum(s => s.Total);
            if (NumEdit.EditValue == null)
            {
                NumEdit.EditValue = new BaseEntities().GetCounter("pay_doc").FirstOrDefault();
            }
            var cd = _db.CashDesks.FirstOrDefault(w => w.Def == 1);
            if (cd != null)
            {
                CashEditComboBox.EditValue = cd.CashId; // За товар
            }
            PTypeComboBox.EditValue = 1;  // Наличкой
           /*добавить текущего пользователя*/ PersonEdit.EditValue = _wb.PersonId ?? _wb.KaId;// Виконавець
        }

        public void Execute(int wbill_id)
        {
            _wb = _db.WaybillList.AsNoTracking().FirstOrDefault(s => s.WbillId == wbill_id);
            if (_wb == null)
            {
                return;
            }

            if (_pd == null && ExecPayCheckBox.Checked)
            {
                _pd = _db.PayDoc.Add(new PayDoc()
                {
                    DocNum = NumEdit.EditValue.ToString(), //Номер документа
                    Total = Convert.ToDecimal(SumEdit.EditValue),
                    Checked = Convert.ToInt32(ExecPayCheckBox.EditValue),
                    OnDate = _wb.OnDate,
                    WithNDS = 1,  // З НДС
                    PTypeId = Convert.ToInt32(PTypeComboBox.EditValue),  // Вид оплати
                    CashId = (int)PTypeComboBox.EditValue == 1 ? (int?)CashEditComboBox.EditValue : null,  // Каса 
                    AccId = (int)PTypeComboBox.EditValue == 2 ?  (int?)AccountEdit.EditValue : null, // Acount
                    CTypeId = 1, // За товар
                    CurrId = 2,  //Валюта по умолчанию
                    OnValue = 1,  //Курс валюти
                    MPersonId = Convert.ToInt32(PersonEdit.EditValue),
                    KaId = _wb.KaId
                });

                if (new int[] { 1, 6, 16 }.Contains(_wb.WType)) _pd.DocType = -1;   // Вихідний платіж
                if (new int[] { -1, -6, 2, -16 }.Contains(_wb.WType)) _pd.DocType = 1;  // Вхідний платіж

                _db.SaveChanges();

                var new_pd = _db.PayDoc.AsNoTracking().FirstOrDefault(w => w.PayDocId == _pd.PayDocId);

                if (new_pd != null)
                {
                    _db.SP_SET_DOCREL(_wb.DocId, new_pd.DocId);
                }
            }
            else if (_pd != null)
            {
                if (_pd.Checked == 1)
                {
                    _pd.Checked = 0;
                    _db.SaveChanges();
                }

                _pd.DocNum = NumEdit.EditValue.ToString();
                _pd.Total = Convert.ToDecimal(SumEdit.EditValue);
                _pd.KaId = _wb.KaId;
                _pd.Checked = Convert.ToInt32(ExecPayCheckBox.EditValue);
                _pd.PTypeId = Convert.ToInt32(PTypeComboBox.EditValue);
                _pd.CashId = Convert.ToInt32(CashEditComboBox.EditValue);
                _pd.MPersonId = Convert.ToInt32(PersonEdit.EditValue);
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

            if ((int)PTypeComboBox.EditValue == 1)
            {
                labelControl3.Visible = true;
                CashEditComboBox.Visible = true;
                if (_pd!=null) _pd.AccId = null;
            }

            if ((int)PTypeComboBox.EditValue == 2)
            {
                labelControl18.Visible = true;
                AccountEdit.Visible = true;
                if (_pd != null) _pd.CashId = null;
            }
        }

        private void CashEditComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                CashEditComboBox.EditValue = IHelper.ShowDirectList(CashEditComboBox.EditValue, 4);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            PersonEdit.EditValue = IHelper.ShowDirectList(PersonEdit.EditValue, 3); 
        }
    }
}
