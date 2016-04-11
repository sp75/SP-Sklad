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
                _pd = db.Database.SqlQuery<PayDoc>("select * from  PayDoc WITH (UPDLOCK) where DocId = {0}", rel.DocId).FirstOrDefault();

                if ( _pd != null )
                {
                    _db.Entry<PayDoc>(_pd).State = System.Data.Entity.EntityState.Modified;

                    ExecPayCheckBox.EditValue = _pd.Checked;
                    NumEdit.EditValue = _pd.DocNum;
                    PTypeComboBox.EditValue = _pd.PTypeId;
                    CashEdit.EditValue = _pd.CashId;
                    PersonEdit.EditValue = _pd.MPersonId;
                    SumEdit.EditValue = _pd.Total;
                    CurrEdit.EditValue = _pd.CurrId;
                }
            }
            else
            {
                ExecPayCheckBox.EditValue = 0;
            }

            PTypeComboBox.Properties.DataSource = DBHelper.PayTypes;
            CashEdit.Properties.DataSource = DBHelper.CashDesks;
            PersonEdit.Properties.DataSource = DBHelper.Persons;
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
                CashEdit.EditValue = cd.CashId; // За товар
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
                    CashId = Convert.ToInt32(CashEdit.EditValue),  // Каса по умолчанию
                    CTypeId = 1, // За товар
                    CurrId = 2,  //Валюта по умолчанию
                    OnValue = 1,  //Курс валюти
                    MPersonId = Convert.ToInt32(PersonEdit.EditValue)
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
                _pd.CashId = Convert.ToInt32(CashEdit.EditValue);
                _pd.MPersonId = Convert.ToInt32(PersonEdit.EditValue);
            }

            _db.SaveChanges();
        }
    }
}
