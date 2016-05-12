using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;

namespace SP_Sklad.WBDetForm
{
    public partial class frmWayBillDetOut : Form
    {
        BaseEntities _db { get; set; }
        private int? _PosId { get; set; }
        private WaybillList _wb { get; set; }
        private WaybillDet _wbd { get; set; }
        private List<GetPosIn_Result> pos_in { get; set; }
        private GetMatRemain_Result mat_remain { get; set; }
        private bool modified_dataset { get; set; }

        public frmWayBillDetOut(BaseEntities db, int? PosId, WaybillList wb)
        {
            InitializeComponent();

            _db = db;
            _PosId = PosId;
            _wb = wb;

            WHComboBox.Properties.DataSource = DBHelper.WhList();
            MatComboBox.Properties.DataSource = db.MaterialsList.ToList();
            PriceTypesEdit.Properties.DataSource = DB.SkladBase().PriceTypes.ToList();
         //   _wbd = db.WaybillDet.Find(_PosId);
        }

        private void frmWayBillDetOut_Load(object sender, EventArgs e)
        {
            if (_PosId == null)
            {
                _wbd = new WaybillDet()
                {
                    WbillId = _wb.WbillId,
                    Amount = 0,
                    Discount = 0,
                    Nds = _wb.Nds,
                    CurrId = _wb.CurrId,
                    OnDate = _wb.OnDate,
                    Num = _wb.WaybillDet.Count() + 1,
                    OnValue = _wb.OnValue,
                    PosKind = 0,
                    PosParent = 0,
                    DiscountKind = 0
                };
                modified_dataset = false;
            }
            else
            {
                _wbd = _db.WaybillDet.Find(_PosId);

                modified_dataset = (_wbd != null);
            }

            if (_wbd != null)
            {
                if (modified_dataset)
                {
                    var w_mat_turn = _db.WMatTurn.Where(w => w.SourceId == _wbd.PosId).ToList();
                    if (w_mat_turn.Count > 0)
                    {
                        pos_in = _db.GetPosIn(_wb.OnDate, _wbd.MatId, _wbd.WId, 0).OrderByDescending(o => o.OnDate).ToList();

                        foreach (var item in w_mat_turn)
                        {
                            if (pos_in.Any(a => a.PosId == item.PosId))
                            {
                                pos_in.FirstOrDefault(a => a.PosId == item.PosId).Amount = item.Amount;
                            }
                        }
                        _db.WMatTurn.RemoveRange(w_mat_turn);
                        _db.SaveChanges();
                    }
                }

                MatComboBox.DataBindings.Add(new Binding("EditValue", _wbd, "MatId"));
                WHComboBox.DataBindings.Add(new Binding("EditValue", _wbd, "WId", true, DataSourceUpdateMode.OnValidation));
                AmountEdit.DataBindings.Add(new Binding("EditValue", _wbd, "Amount"));
                PriceTypesEdit.DataBindings.Add(new Binding("EditValue", _wbd, "PtypeId", true, DataSourceUpdateMode.OnValidation));
                BasePriceEdit.DataBindings.Add(new Binding("EditValue", _wbd, "BasePrice", true, DataSourceUpdateMode.OnValidation));
                DiscountEdit.DataBindings.Add(new Binding("EditValue", _wbd, "Discount", true, DataSourceUpdateMode.OnValidation));
                CheckCustomEdit.DataBindings.Add(new Binding("EditValue", _wbd, "DiscountKind", true, DataSourceUpdateMode.OnValidation));

                GetOk();
            }
        }

        bool GetOk()
        {
            bool recult = (MatComboBox.EditValue != DBNull.Value && WHComboBox.EditValue != DBNull.Value && BasePriceEdit.EditValue != DBNull.Value && AmountEdit.EditValue != DBNull.Value);

            OkButton.Enabled = recult;

            RSVCheckBox.Checked = (OkButton.Enabled && pos_in != null && mat_remain != null && pos_in.Count > 0 && AmountEdit.Value <= mat_remain.RemainInWh && pos_in.Sum(s => s.FullRemain) >= AmountEdit.Value);
            if (RSVCheckBox.Checked)
            {
                foreach (var item in pos_in)
                {
                    if (item.FullRemain < item.Amount)
                    {
                        RSVCheckBox.Checked = false;
                        break;
                    }
                }
            }

            btnShowRemainByWH.Enabled = (MatComboBox.EditValue != null);

            BotAmountEdit.Text = AmountEdit.Text;

            if (DiscountCheckBox.Checked) DiscountPriceEdit.EditValue = BasePriceEdit.Value - (BasePriceEdit.Value * DiscountEdit.Value / 100);
            else DiscountPriceEdit.EditValue = BasePriceEdit.Value;

            PriceNotNDSEdit.EditValue = Convert.ToDecimal(DiscountPriceEdit.EditValue) * 100 / (100 + _wbd.Nds);
            TotalSumEdit.EditValue = Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(PriceNotNDSEdit.EditValue);
            SummAllEdit.EditValue = Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(DiscountPriceEdit.EditValue);
            TotalNdsEdit.EditValue = Convert.ToDecimal(SummAllEdit.EditValue) - Convert.ToDecimal(TotalSumEdit.EditValue);

            return recult;
        }

        private void MatComboBox_EditValueChanged(object sender, EventArgs e)
        {
             var row = (MaterialsList)MatComboBox.GetSelectedDataRow();
             if (row == null )
             {
                 return;
             }

             if (MatComboBox.ContainsFocus)
             {
                 _wbd.WId = row.Wid;
                 WHComboBox.EditValue = row.Wid;
                 _wbd.Nds = row.Nds;
                 _wbd.MatId = row.MatId;
             }

             labelControl24.Text = row.MeasuresName;
             labelControl27.Text = row.MeasuresName;

             if (PriceTypesEdit.EditValue != null && PriceTypesEdit.EditValue != DBNull.Value)
             {
                 var list_price = _db.GetListMatPrices(row.MatId, _wb.CurrId).FirstOrDefault(w => w.PType == (int)PriceTypesEdit.EditValue);
                 if (list_price != null)
                 {
                     _wbd.BasePrice = Math.Round(list_price.Price.Value, 4);
                     BasePriceEdit.Value = _wbd.BasePrice.Value;
                 }
             }

             GetDiscount(row.MatId);
             GetContent();
        }

        private void GetDiscount(int? MatId)
        {
            var disc = DB.SkladBase().GetDiscount(_wb.KaId, MatId).FirstOrDefault();
            DiscountCheckBox.Checked = (disc > 0);

            if (_wbd.DiscountKind == 0)
            {
                DiscountEdit.EditValue = disc;
                _wbd.Discount = disc;
            }
        }

        private void GetContent()
        {
            if (_wbd.WId == null || _wbd.MatId == 0)
            {
                return;
            }

            mat_remain = _db.GetMatRemain(_wbd.WId, _wbd.MatId).FirstOrDefault();

            if (mat_remain != null)
            {
                RemainWHEdit.EditValue = mat_remain.RemainInWh;
                RsvEdit.EditValue = mat_remain.Rsv;
                CurRemainEdit.EditValue = mat_remain.Remain;
                MinPriceEdit.EditValue = mat_remain.MinPrice;
                AvgPriceEdit.EditValue = mat_remain.AvgPrice;
                MaxPriceEdit.EditValue = mat_remain.MaxPrice;
            }

            pos_in = _db.GetPosIn(_wb.OnDate, _wbd.MatId, _wbd.WId, 0).OrderByDescending(o => o.OnDate).ToList();
            SetAmount();
        }

        private void SetAmount()
        {
            if (pos_in == null || mat_remain == null)
            {
                return;
            }

            decimal? sum_amount = pos_in.Sum(s => s.Amount);
            decimal? sum_full_remain = pos_in.Sum(s => s.FullRemain);

            if (pos_in.Count > 0 && AmountEdit.Value <= mat_remain.RemainInWh && sum_amount != AmountEdit.Value)
            {
                sum_amount = AmountEdit.Value;
                bool stop = false;
                foreach (var item in pos_in)
                {
                    decimal? remain = item.FullRemain;
                    if (!stop)
                    {
                        if (remain >= sum_amount)
                        {
                            item.Amount = sum_amount;
                            sum_amount -= remain;
                            stop = true;
                        }
                        else
                        {
                            item.Amount = remain;
                            sum_amount -= remain;
                        }
                    }
                    else item.Amount = 0;
                }
                RSVCheckBox.Checked = (sum_amount <= 0);

            }
            else RSVCheckBox.Checked = false;

            if (AmountEdit.Value <= sum_full_remain) RSVCheckBox.Checked = false;

            GetOk();
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            SetAmount();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DiscountCheckBox.Checked) _wbd.Discount = 0;

            _wbd.Price = Convert.ToDecimal( PriceNotNDSEdit.EditValue);

            if (!modified_dataset)
            {
                _db.WaybillDet.Add(_wbd);
            }
            _db.SaveChanges();

            if (RSVCheckBox.Checked && !_db.WMatTurn.Any(w => w.SourceId == _wbd.PosId))
            {
                foreach (var item in pos_in.Where(w => w.Amount > 0))
                {
                    _db.WMatTurn.Add(new WMatTurn
                    {
                        PosId = item.PosId,
                        WId = _wbd.WId.Value,
                        MatId = _wbd.MatId,
                        OnDate = _wb.OnDate,
                        TurnType = _wb.WType == -16 ? -16 : 2,
                        Amount = Convert.ToDecimal(item.Amount),
                        SourceId = _wbd.PosId
                    });
                }
            }

         //   if (WayBillDetAddProps->State == dsInsert || WayBillDetAddProps->State == dsEdit) WayBillDetAddProps->Post();
            try
            {
                _db.SaveChanges();
                Close();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException  exp)
            {
                MessageBox.Show(exp.InnerException.InnerException.Message);
            }
        }

        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (WHComboBox.EditValue == DBNull.Value || !WHComboBox.ContainsFocus) return;

            _wbd.WId = (int)WHComboBox.EditValue;
            GetContent();
            GetOk();
        }

        private void PriceTypesEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!PriceTypesEdit.ContainsFocus || MatComboBox.EditValue == null || PriceTypesEdit.EditValue == null || PriceTypesEdit.EditValue == DBNull.Value)
            {
                return;
            }

            var list_price = _db.GetListMatPrices((int)MatComboBox.EditValue, _wb.CurrId).FirstOrDefault(w => w.PType == (int)PriceTypesEdit.EditValue);
            if (list_price != null)
            {
              _wbd.BasePrice   = Math.Round(list_price.Price.Value, 4);
              BasePriceEdit.Value = _wbd.BasePrice.Value;
            }

            GetOk();
        }

        private void BasePriceEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!BasePriceEdit.ContainsFocus)
            {
                return;
            }

            _wbd.PtypeId = null;
            PriceTypesEdit.EditValue = null;

            GetOk();
        }

        private void RSVCheckBox_Click(object sender, EventArgs e)
        {
            if (!RSVCheckBox.Checked) GetOk();
        }

        private void RSVCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RSVCheckBox.Checked && RSVCheckBox.ContainsFocus)
            {
                GetOk();
            }
        }

        private void DiscountCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void CheckCustomEdit_CheckedChanged(object sender, EventArgs e)
        {
            DiscountEdit.Enabled = CheckCustomEdit.Checked;

            if (!CheckCustomEdit.Checked && CheckCustomEdit.ContainsFocus)
            {
                GetDiscount(Convert.ToInt32( MatComboBox.EditValue));
            }
        }

        private void DiscountEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (DiscountEdit.ContainsFocus)
            {
                GetOk();
            }
        }

        private void frmWayBillDetOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            _db.Entry<WaybillDet>(_wbd).Reload();
        }

        private void PosInfoBtn_Click(object sender, EventArgs e)
        {
            var pos = new frmInParty(pos_in);
            pos.Text = "Прибуткові партії: " + MatComboBox.Text;
            pos.ShowDialog();
            _wbd.Amount = pos_in.Sum(s => s.Amount).Value;
            AmountEdit.Value = _wbd.Amount;

            GetOk();
        }

    }
}
