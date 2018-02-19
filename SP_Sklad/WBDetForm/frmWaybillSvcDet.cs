using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.SkladData;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBDetForm
{
    public partial class frmWaybillSvcDet : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private int? _PosId { get; set; }
        private WaybillList _wb { get; set; }
        private WayBillSvc _wbs { get; set; }

        public frmWaybillSvcDet(BaseEntities db, int? PosId, WaybillList wb)
        {
            InitializeComponent();

            _db = db;
            _PosId = PosId;
            _wb = wb;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DiscountCheckBox.Checked) _wbs.Discount = 0;
            _wbs.Price = (decimal)PriceNotNDSEdit.EditValue;

            if (_db.Entry<WayBillSvc>(_wbs).State == EntityState.Detached)
            {
                _db.WayBillSvc.Add(_wbs);
            }

            _db.SaveChanges();
        }

        private void frmWaybillSvcDet_Load(object sender, EventArgs e)
        {
            SvcComboBox.Properties.DataSource = DB.SkladBase().v_Services.Select(s => new { s.SvcId, s.Name, s.MeasuresName, s.Price, IsNormed = s.IsNormed == 1 ? true : false, s.Norm }).ToList();
            PersonComboBox.Properties.DataSource = DBHelper.Persons; ;

            if (_PosId == null)
            {
                _wbs = new WayBillSvc()
                {
                    WbillId = _wb.WbillId,
                    Norm = 1,
                    Amount = 0,
                    Discount = 0,
                    Nds = _wb.Nds,
                    CurrId = _wb.CurrId.Value,
                    Num = _db.GetWaybillDetIn(_wb.WbillId).Count() + 1,
                    SvcToPrice = 0
                };
            }
            else
            {
                _wbs = _db.WayBillSvc.Find(_PosId);
            }

            if (_wbs != null)
            {
                WayBillSvcBS.DataSource = _wbs;

                DiscountCheckBox.Checked = (_wbs.Discount > 0);

                //            cxDBCalcEdit3->Enabled = cxDBCheckBox2->Checked;

                PriceEdit.Value = Math.Round((decimal)_wbs.BasePrice * 100 / (100 + (decimal)_wb.Nds), 2);

                GetOk();
            }

            checkEdit1.Enabled = (_wb.WType == 1);
        }

        bool GetOk()
        {
            //bool recult = (!cxDBLookupComboBox4->Text.IsEmpty() && !NDSDBEdit->Text.IsEmpty() && !quantityEdit->Text.IsEmpty() && !BasePriceEdit->Text.IsEmpty());
            bool recult = (SvcComboBox.EditValue != DBNull.Value && AmountEdit.EditValue != DBNull.Value);

            OkButton.Enabled = recult;

            BotAmountEdit.Text = AmountEdit.Text;

            if (DiscountCheckBox.Checked) DiscountPriceEdit.EditValue = _wbs.BasePrice - (_wbs.BasePrice * _wbs.Discount / 100);
            else DiscountPriceEdit.EditValue = _wbs.BasePrice;

            decimal price;
            if (NormCheckBox.Checked) price = DiscountPriceEdit.Value * NormEdit.Value;
            else price = DiscountPriceEdit.Value;


            PriceNotNDSEdit.EditValue = Convert.ToDecimal(DiscountPriceEdit.EditValue) * 100 / (100 + _wbs.Nds);
            TotalSumEdit.EditValue = AmountEdit.EditValue == DBNull.Value ? 0 : (Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(PriceNotNDSEdit.EditValue));
            SummAllEdit.EditValue = AmountEdit.EditValue == DBNull.Value ? 0 : Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(DiscountPriceEdit.EditValue);
            TotalNdsEdit.EditValue = Convert.ToDecimal(SummAllEdit.EditValue) - Convert.ToDecimal(TotalSumEdit.EditValue);

            return recult;

        }

        private void SvcComboBox_EditValueChanged(object sender, EventArgs e)
        {
            dynamic row = SvcComboBox.GetSelectedDataRow();
            if (row != null)
            {
                labelControl5.Visible = row.IsNormed;
                NormEdit.Visible = row.IsNormed;

                PriceEdit.EditValue = row.Price;
                _wbs.Price = row.Price;
                _wbs.BasePrice = Math.Round(row.Price + (row.Price * _wb.Nds / 100), 2);

                NormEdit.Enabled = NormCheckBox.Checked;
                if (NormCheckBox.Checked)
                {
                    _wbs.Norm = row.Norm;
                }
                else
                {
                    _wbs.Norm = 1;
                }
                NormEdit.EditValue = _wbs.Norm;
            }
        }

        private void DiscountEdit_EditValueChanged(object sender, EventArgs e)
        {
            _wbs.Discount = DiscountEdit.Value;

            GetOk();
        }

        private void DiscountCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void PriceEdit_EditValueChanged(object sender, EventArgs e)
        {
           // if( PriceEdit.)
         //   {
                _wbs.BasePrice = ( PriceEdit.Value + (PriceEdit.Value * _wb.Nds /100));
          //  }


                GetOk();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SvcComboBox.EditValue = IHelper.ShowDirectList(SvcComboBox.EditValue, 11);
        }

        private void PersonEditBtn_Click(object sender, EventArgs e)
        {
            PersonComboBox.EditValue = IHelper.ShowDirectList(PersonComboBox.EditValue, 3);
        }
    }
}
