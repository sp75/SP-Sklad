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
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBDetForm
{
    public partial class frmWBReturnDetOut : Form
    {
        BaseEntities _db { get; set; }
        private int? _PosId { get; set; }
        private WaybillList _wb { get; set; }
        private WaybillDet _wbd { get; set; }
        private List<GetPosIn_Result> pos_in { get; set; }
        private GetActualRemainByWh_Result mat_remain { get; set; }
        public int _ka_id { get; set; }

        public frmWBReturnDetOut(BaseEntities db, int? PosId, WaybillList wb, int ka_id)
        {
            InitializeComponent();

            _db = db;
            _PosId = PosId;
            _wb = wb;
            _ka_id = ka_id;
        }

        private void frmWBReturnDetOut_Load(object sender, EventArgs e)
        {
            WHComboBox.Properties.DataSource = DBHelper.WhList();
            MatComboBox.Properties.DataSource = _db.WhMatGet(0, _wb.WaybillMove.SourceWid, _ka_id, DBHelper.ServerDateTime(), 0, "*", 0, "", DBHelper.CurrentUser.UserId, 0).ToList();

            if (_wb.WType == 4)
            {
                WHComboBox.Enabled = false;
                WhEditBtn.Enabled = false;
            }

            if (_PosId == null)
            {
                _wbd = new WaybillDet()
                {
                    WbillId = _wb.WbillId,
                    Amount = 0,
                    OnValue = _wb.OnValue,
                    WId = _wb.WaybillMove.SourceWid
                };
            }
            else
            {
                _wbd = _db.WaybillDet.Find(_PosId);
            }

            if (_wbd != null)
            {
                MatComboBox.DataBindings.Add(new Binding("EditValue", _wbd, "MatId"));
                WHComboBox.DataBindings.Add(new Binding("EditValue", _wbd, "WId", true, DataSourceUpdateMode.OnValidation));
                AmountEdit.DataBindings.Add(new Binding("EditValue", _wbd, "Amount"));
                BasePriceEdit.DataBindings.Add(new Binding("EditValue", _wbd, "BasePrice", true, DataSourceUpdateMode.OnValidation));

                if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Unchanged)
                {
                    var w_mat_turn = _db.WMatTurn.Where(w => w.SourceId == _wbd.PosId).ToList();
                    if (w_mat_turn.Count > 0)
                    {
                        _db.WMatTurn.RemoveRange(w_mat_turn);
                        _db.SaveChanges();
                    }

                    GetContent();

                    foreach (var item in w_mat_turn)
                    {
                        if (pos_in.Any(a => a.PosId == item.PosId))
                        {
                            pos_in.FirstOrDefault(a => a.PosId == item.PosId).Amount = item.Amount;
                        }
                    }
                }
            }
            GetOk();
        }

        bool GetOk()
        {
            bool recult = (MatComboBox.EditValue != DBNull.Value && WHComboBox.EditValue != DBNull.Value && BasePriceEdit.EditValue != DBNull.Value && AmountEdit.EditValue != DBNull.Value);

            OkButton.Enabled = recult;

            RSVCheckBox.Checked = (OkButton.Enabled && pos_in != null && mat_remain != null && pos_in.Count > 0 && AmountEdit.Value <= mat_remain.CurRemainInWh && pos_in.Sum(s => s.FullRemain) >= AmountEdit.Value);
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


      //      PriceNotNDSEdit.EditValue = BasePriceEdit.Value;
     //       TotalSumEdit.EditValue = Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(PriceNotNDSEdit.EditValue);
     //       SummAllEdit.EditValue = Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(DiscountPriceEdit.EditValue);
     //       TotalNdsEdit.EditValue = Convert.ToDecimal(SummAllEdit.EditValue) - Convert.ToDecimal(TotalSumEdit.EditValue);

            return recult;
        }

        private void MatComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = (WhMatGet_Result)MatComboBox.GetSelectedDataRow();
            if (row == null)
            {
                return;
            }

            if (MatComboBox.ContainsFocus)
            {
                _wbd.MatId = row.MatId.Value;
                GetContent();
            }

            labelControl24.Text = row.MsrName;
            labelControl27.Text = row.MsrName;
        }

        private void GetContent()
        {
            if (_wbd.WId == null || _wbd.MatId == 0)
            {
                return;
            }

            mat_remain = _db.GetActualRemainByWh(_wbd.WId, _wbd.MatId).FirstOrDefault();

            if (mat_remain != null)
            {
                RemainWHEdit.EditValue = mat_remain.CurRemainInWh;
                RsvEdit.EditValue = mat_remain.Rsv;
                CurRemainEdit.EditValue = mat_remain.Remain;
            }

            pos_in = _db.GetPosIn(_wb.OnDate, _wbd.MatId, _wbd.WId, 0).OrderByDescending(o => o.OnDate).ToList();

            if (pos_in.Any())
            {
                _wbd.Price = pos_in.First().Price;
                _wbd.BasePrice = pos_in.First().BasePrice;
                _wbd.Nds = pos_in.First().Nds;
            }

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

            if (pos_in.Count > 0 && AmountEdit.Value <= mat_remain.CurRemainInWh && sum_amount != AmountEdit.Value)
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

        private void OkButton_Click(object sender, EventArgs e)
        {
            int num = _wb.WaybillDet.Count();
            try
            {
                if (RSVCheckBox.Checked && !_db.WMatTurn.Any(w => w.SourceId == _wbd.PosId))
                {
                    var sate = _db.Entry<WaybillDet>(_wbd).State;
                    if (sate == EntityState.Modified || sate == EntityState.Unchanged)
                    {
                        _db.WaybillDet.Remove(_wbd);
                    }

                    foreach (var item in pos_in.Where(w => w.Amount > 0))
                    {
                        var wbd = _db.WaybillDet.Add(new WaybillDet()
                        {
                            WbillId = _wb.WbillId,
                            Price = item.Price,
                            BasePrice = item.BasePrice,
                            Nds = item.Nds,
                            CurrId = item.CurrId,
                            OnDate = _wb.OnDate,
                            WId = item.WId,
                            Num = ++num,
                            Amount = item.Amount.Value,
                            MatId = item.MatId,
                        });
                        _db.SaveChanges();

                        _db.WMatTurn.Add(new WMatTurn
                        {
                            PosId = item.PosId,
                            WId = item.WId,
                            MatId = item.MatId,
                            OnDate = _wb.OnDate,
                            TurnType = 2,
                            Amount = Convert.ToDecimal(item.Amount),
                            SourceId = wbd.PosId
                        });
                        _db.SaveChanges();
                    }
                }

                Close();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException exp)
            {
                MessageBox.Show(exp.InnerException.InnerException.Message);
            }
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            SetAmount();
        }

        private void RSVCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RSVCheckBox.Checked && RSVCheckBox.ContainsFocus)
            {
                GetOk();
            }
        }

        private void frmWBReturnDetOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Modified)
            {
                _db.Entry<WaybillDet>(_wbd).Reload();
            }
        }

    }
}
