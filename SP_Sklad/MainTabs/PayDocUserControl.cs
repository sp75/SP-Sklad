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
                ExecPayCheckBox.EditValue = _pd.Checked;
                SetValue();
            }
            else
            {
                ExecPayCheckBox.EditValue = 0;
            }
        }

        public void SetValue()
        {
            if (_pd == null)
            {
                return;
            }

            NumEdit.EditValue = _pd.DocNum;
            PTypeComboBox.EditValue = _pd.PTypeId;
            CashEdit.EditValue = _pd.CashId;
            PersonEdit.EditValue = _pd.MPersonId;
            SumEdit.EditValue = _pd.Total;
            CurrEdit.EditValue = _pd.CurrId;

            PTypeComboBox.Properties.DataSource = DBHelper.PayTypes;
            CashEdit.Properties.DataSource = DBHelper.CashDesks;
            PersonEdit.Properties.DataSource = DBHelper.Persons;
        }

        private void ExecPayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ExecPayCheckBox.Checked && _pd == null)
            {
                int wtype = _wb.WType;

                _pd = new PayDoc()
                {
                    DocNum = new BaseEntities().GetCounter("pay_doc").FirstOrDefault(), //Номер документа
                    Total = _wb.SummAll.Value,
                    Checked = 0,
                    OnDate = DateTime.Now,
                    CTypeId = 1,  // За товар
                    WithNDS = 1,  // З НДС
                    PTypeId = 1,  // Наличкой
                    CashId = _db.CashDesks.FirstOrDefault(w => w.Def == 1).CashId,  // Каса по умолчанию
                    CurrId = 2,  //Валюта по умолчанию
                    OnValue = 1,  //Курс валюти
                    MPersonId = 0 // Виконавець
                };

                if (new int[] { 1, 6, 16 }.Contains(wtype)) _pd.DocType = -1;   // Вихідний платіж
                if (new int[] { -1, -6, 2, -16 }.Contains(wtype)) _pd.DocType = 1;  // Вхідний платіж

                SetValue();

            }
        }

        public void Execute(WaybillList wb)
        {
            if (_pd == null) return;

            _pd.KaId = wb.KaId;
            _pd.OnDate = wb.OnDate;
            _pd.Checked = Convert.ToInt32(ExecPayCheckBox.EditValue);
            _db.PayDoc.Add(_pd);
            _db.Entry<PayDoc>(_pd).State = System.Data.Entity.EntityState.Modified;
            _db.Entry<PayDoc>(_pd).Property(p => p.DocId).IsModified = false;

            _db.SaveChanges();

            /*
            if (PayDoc->State == dsInsert || (PayDocCHECKED->Value == 0 && !PayDocCHECKED->IsNull))
            {
                PayDoc->Edit();
                PayDocONDATE->Value = wayBillList->FieldByName("ONDATE")->Value;
                PayDocKAID->Value = wayBillList->FieldByName("KAID")->Value;
            }

            if (cxCheckBox1->Checked && cxCheckBox1->Enabled)
            {
                PayDoc->Edit();
                PayDocCHECKED->Value = 1;
            }
            else if (!cxCheckBox1->Checked && PayDoc->RecordCount > 0)
            {
                //		 PayDoc->Delete();
            }

            if (PayDoc->State == dsInsert || PayDoc->State == dsEdit) PayDoc->Post();

            if (PayDocCHECKED->Value == 1 && cxCheckBox1->Enabled)
            {
                SET_DOCREL->ParamByName("DOCID")->Value = wayBillList->FieldByName("DOCID")->Value;
                SET_DOCREL->ParamByName("RDOCID")->Value = PayDocDOCID->Value;
                SET_DOCREL->ExecProc();
            }*/
        }
    }
}
