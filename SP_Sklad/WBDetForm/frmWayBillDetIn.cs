using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.SkladData;

namespace SP_Sklad.WBDetForm
{
    public partial class frmWayBillDetIn : Form
    {
        BaseEntities _db { get; set; }
        private int? _PosId { get; set; }
        private WaybillList _wb { get; set; }
        private WaybillDet _wbd { get; set; }

        public frmWayBillDetIn(BaseEntities db, int? PosId, WaybillList wb)
        {
            InitializeComponent();
            _db = db;
            _PosId = PosId;
            _wb = wb;

            WHComboBox.Properties.DataSource = DBHelper.WhList();
            MatComboBox.Properties.DataSource = db.MaterialsList.ToList();
            _wbd = db.WaybillDet.Find(_PosId);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (_wbd == null)
            {
                _db.WaybillDet.Add(new WaybillDet()
                {
                    WbillId = _wb.WbillId,
                    MatId = (int)MatComboBox.EditValue,
                    WId = (int)WHComboBox.EditValue,
                    Amount = (decimal)AmountEdit.EditValue,
                    Price = (decimal)PriceEdit.EditValue,
                    Discount = 0,
                    Nds = (decimal)NdsEdit.EditValue,
                    CurrId = _wb.CurrId,
                    OnDate = _wb.OnDate,
                    Num = _wb.WaybillDet.Count() + 1,
                    OnValue = _wb.OnValue,
                    BasePrice = (decimal)BasePriceEdit.EditValue,
                    PosKind = 0,
                    PosParent = 0,
                    DiscountKind = 0
                });
            }
            else
            {
                _wbd.MatId = (int)MatComboBox.EditValue;
                _wbd.WId = (int)WHComboBox.EditValue;
                _wbd.Amount = (decimal)AmountEdit.EditValue;
                _wbd.Price = (decimal)PriceEdit.EditValue;
                _wbd.Nds = (decimal)NdsEdit.EditValue;
                _wbd.BasePrice = (decimal)BasePriceEdit.EditValue;
              
            }

            _db.SaveChanges();

            if (_wb.WType == 1)
            {
                //_db.upd;
            }
        }

        private void MatComboBox_Properties_EditValueChanged(object sender, EventArgs e)
        {
            var row = (MaterialsList)MatComboBox.GetSelectedDataRow();
            
            NdsEdit.EditValue =  row.Nds;
            WHComboBox.EditValue = row.Wid;
            
            GetRemains();
            
            GetOk();
        }

        private void GetRemains()
        {
             var r =_db.SP_MAT_REMAIN_GET_SIMPLE((int)MatComboBox.EditValue, _wb.OnDate).FirstOrDefault();

             if (r != null)
             {
                 RemainEdit.EditValue = r.Remain;
                 RsvEdit.EditValue = r.Rsv;
                 CurRemainEdit.EditValue = r.Remain-r.Rsv;
                 MinPriceEdit.EditValue = r.MinPrice;
                 AvgPriceEdit.EditValue = r.AvgPrice;
                 MaxPriceEdit.EditValue = r.MaxPrice;
             }
        }

        private void BasePriceEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(NdsEdit.EditValue) > 0)
            {
                PriceEdit.EditValue = Math.Round((Convert.ToDecimal(BasePriceEdit.EditValue) * 100 / (100 + Convert.ToDecimal(NdsEdit.EditValue))), 4);
            }
            else PriceEdit.EditValue = BasePriceEdit.EditValue;
            
            GetOk();
        }

        private void PriceEdit_EditValueChanged(object sender, EventArgs e)
        {
            if ( Convert.ToDecimal(NdsEdit.EditValue) > 0)
            {
                var p = Convert.ToDecimal(PriceEdit.EditValue);
                BasePriceEdit.EditValue = Math.Round(p + (p * Convert.ToDecimal(NdsEdit.EditValue) / 100), 4);
            }
            else BasePriceEdit.EditValue = PriceEdit.EditValue;

            GetOk();
        }

        bool GetOk()
        {
            bool recult = (MatComboBox.EditValue != null && WHComboBox.EditValue != null && BasePriceEdit.EditValue != null && PriceEdit.EditValue != null && AmountEdit.EditValue != null);

            OkButton.Enabled = recult;
            return recult;
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void frmWayBillDetIn_Load(object sender, EventArgs e)
        {
            if (_wbd != null)
            {
                MatComboBox.EditValue = _wbd.MatId;
                WHComboBox.EditValue = _wbd.WId;
                AmountEdit.EditValue = _wbd.Amount;
                PriceEdit.EditValue = _wbd.Price;
                NdsEdit.EditValue = _wbd.Nds;
                BasePriceEdit.EditValue = _wbd.BasePrice;
            }

            GetOk();
        }
    }
}
