using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using SP_Sklad.Properties;
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
                _wbd = _db.WaybillDet.Add(new WaybillDet()
                 {
                     WbillId = _wb.WbillId,
                     MatId = Convert.ToInt32(MatComboBox.EditValue),
                     WId = Convert.ToInt32(WHComboBox.EditValue),
                     Amount = Convert.ToDecimal(AmountEdit.EditValue),
                     Price = Convert.ToDecimal(PriceEdit.EditValue),
                     Discount = 0,
                     Nds = Convert.ToDecimal(NdsEdit.EditValue),
                     CurrId = _wb.CurrId,
                     OnDate = _wb.OnDate,
                     Num = _wb.WaybillDet.Count() + 1,
                     OnValue = _wb.OnValue,
                     BasePrice = Convert.ToDecimal(BasePriceEdit.EditValue),
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

            if (_wb.WType == 16)
            {
                _db.DeleteWhere<WMatTurn>(w => w.PosId == _wbd.PosId);
                _db.WMatTurn.Add(new WMatTurn()
                {
                    SourceId = _wbd.PosId,
                    PosId = _wbd.PosId,
                    WId = _wbd.WId.Value,
                    MatId = _wbd.MatId,
                    OnDate = _wbd.OnDate.Value,
                    TurnType = 3,
                    Amount = _wbd.Amount
                });
            }

         //   if (Serials->State == dsInsert || Serials->State == dsEdit) Serials->Post();
      //      if (WayBillDetAddProps->State == dsInsert || WayBillDetAddProps->State == dsEdit) WayBillDetAddProps->Post();


            _db.SaveChanges();

            if (_wb.WType == 1)
            {
                _db.UpdWaybillDetPrice(_wb.WbillId);
            }
        }

        private void MatComboBox_Properties_EditValueChanged(object sender, EventArgs e)
        {
            var row = (MaterialsList)MatComboBox.GetSelectedDataRow();
            
            NdsEdit.EditValue =  row.Nds;
            WHComboBox.EditValue = row.Wid;
            labelControl24.Text = row.MeasuresName;
            labelControl27.Text = row.MeasuresName;

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
            BotBasePriceEdit.EditValue = PriceEdit.EditValue;

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
            BotAmountEdit.Text = AmountEdit.Text;
            TotalSumEdit.EditValue = Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(PriceEdit.EditValue);
            SummAllEdit.EditValue = Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(BasePriceEdit.EditValue);
            TotalNdsEdit.EditValue = Convert.ToDecimal(SummAllEdit.EditValue) - Convert.ToDecimal(TotalSumEdit.EditValue); 

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
            panel3.Visible = barCheckItem1.Checked;
            if (!barCheckItem1.Checked) Height -= panel3.Height;

            panel4.Visible = barCheckItem2.Checked;
            if (!barCheckItem2.Checked) Height -= panel4.Height;

            panel5.Visible = barCheckItem3.Checked;
            if (!barCheckItem3.Checked) Height -= panel5.Height;


            if (_wbd != null)
            {
                MatComboBox.EditValue = _wbd.MatId;
                WHComboBox.EditValue = _wbd.WId;
                AmountEdit.EditValue = _wbd.Amount;
                PriceEdit.EditValue = _wbd.Price;
                NdsEdit.EditValue = _wbd.Nds;
                BasePriceEdit.EditValue = _wbd.BasePrice;
            }
            else
            {
                AmountEdit.EditValue = 1;
                PriceEdit.EditValue = 0;
            }

            GetOk();
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var item = e.Item as BarCheckItem;

            bool ch = item.Checked;
            int ItemIndex = Convert.ToInt32(e.Item.Tag);
            if (ItemIndex == 0)
            {
                panel3.Visible = ch;
                if (ch) Height += panel3.Height;
                else Height -= panel3.Height;
                Settings.Default.ch_view1 = ch;
            }
            if (ItemIndex == 1)
            {
                panel4.Visible = ch;
                if (ch) Height += panel4.Height;
                else Height -= panel4.Height;
                Settings.Default.ch_view2 = ch;
            }

            if (ItemIndex == 2)
            {
                panel5.Visible = ch;
                if (ch) Height += panel5.Height;
                else Height -= panel5.Height;
                Settings.Default.ch_view3 = ch;
            }
        }

        private void frmWayBillDetIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_wbd != null)
            {
                _db.Entry<WaybillDet>(_wbd).Reload();
            }
            Settings.Default.Save();
        }
    }
}
