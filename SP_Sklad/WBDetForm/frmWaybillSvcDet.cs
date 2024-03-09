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
            if (!DiscountCheckBox.Checked)
            {
                _wbs.Discount = 0;
            }
            _wbs.Price = DiscountPriceEdit.Value;

            if (_db.Entry<WayBillSvc>(_wbs).State == EntityState.Detached)
            {
                _db.WayBillSvc.Add(_wbs);
            }

            _db.SaveChanges();
        }

        private void frmWaybillSvcDet_Load(object sender, EventArgs e)
        {
            SvcComboBox.Properties.DataSource = DB.SkladBase().v_Services.AsNoTracking().Select(s => new
            {
                s.SvcId,
                s.Name,
                s.MeasuresName,
                s.Price,
                IsNormed = s.IsNormed == 1 ? true : false,
                s.Norm,
                s.MsrShortName
            }).ToList();

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

                BasePriceEdit.Value = _wbs.BasePrice ?? 0;

                GetOk();
            }

            checkEdit1.Enabled = (_wb.WType == 1);
        }

        bool GetOk()
        {
            //bool recult = (!cxDBLookupComboBox4->Text.IsEmpty() && !NDSDBEdit->Text.IsEmpty() && !quantityEdit->Text.IsEmpty() && !BasePriceEdit->Text.IsEmpty());
            bool recult = (SvcComboBox.EditValue != null && SvcComboBox.EditValue != DBNull.Value && (int)SvcComboBox.EditValue > 0 && AmountEdit.EditValue != null && AmountEdit.EditValue != DBNull.Value && (decimal)AmountEdit.EditValue > 0);

            OkButton.Enabled = recult;
            /*
            BotAmountEdit.Text = AmountEdit.Text;

            if (DiscountCheckBox.Checked) DiscountPriceEdit.EditValue = _wbs.BasePrice - (_wbs.BasePrice * _wbs.Discount / 100);
            else DiscountPriceEdit.EditValue = _wbs.BasePrice;

            decimal price;
            if (NormCheckBox.Checked) price = DiscountPriceEdit.Value * NormEdit.Value;
            else price = DiscountPriceEdit.Value;


            PriceNotNDSEdit.EditValue = Convert.ToDecimal(DiscountPriceEdit.EditValue) * 100 / (100 + _wbs.Nds);
            TotalSumEdit.EditValue = AmountEdit.EditValue == DBNull.Value ? 0 : (Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(PriceNotNDSEdit.EditValue));
            SummAllEdit.EditValue = AmountEdit.EditValue == DBNull.Value ? 0 : Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(DiscountPriceEdit.EditValue);
            TotalNdsEdit.EditValue = Convert.ToDecimal(SummAllEdit.EditValue) - Convert.ToDecimal(TotalSumEdit.EditValue);*/

            BotAmountEdit.Text = AmountEdit.Text;
            var discount_value = DiscountCheckBox.Checked ? DiscountEdit.Value : 0;

            var total = Math.Round(BasePriceEdit.Value * AmountEdit.Value, 2);
            var discount = Math.Round((total * discount_value / 100), 2);
            var total_discount = total - discount;

            if (DiscountCheckBox.Checked && AmountEdit.Value > 0)
            {
                DiscountPriceEdit.EditValue = total_discount / AmountEdit.Value;
            }
            else
            {
                DiscountPriceEdit.EditValue = BasePriceEdit.Value;
            }

            decimal price;
            if (NormCheckBox.Checked) price = DiscountPriceEdit.Value * NormEdit.Value;
            else price = DiscountPriceEdit.Value;

            PriceNotNDSEdit.EditValue = price * 100 / (100 + _wbs.Nds);
            TotalSumEdit.EditValue = total_discount * 100 / (100 + _wbs.Nds);
            TotalNdsEdit.EditValue = total_discount - (decimal)TotalSumEdit.EditValue;
            SummAllEdit.EditValue = total_discount;


            return recult;

        }

        private void SvcComboBox_EditValueChanged(object sender, EventArgs e)
        {
            dynamic row = SvcComboBox.GetSelectedDataRow();
            if (row != null)
            {
                _wbs.SvcId = row.SvcId;

               labelControl5.Visible = row.IsNormed;
                NormEdit.Visible = row.IsNormed;

                BasePriceEdit.EditValue = row.Price;
                _wbs.Price = row.Price;
                _wbs.BasePrice =row.Price ;

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

                labelControl24.Text = row.MsrShortName;
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
            GetOk();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
         
        }

        private void PersonEditBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void SvcComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                SvcComboBox.EditValue = IHelper.ShowDirectList(SvcComboBox.EditValue, 11);
            }
        }

        private void PersonComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                PersonComboBox.EditValue = IHelper.ShowDirectList(PersonComboBox.EditValue, 3);
            }
        }
    }
}
