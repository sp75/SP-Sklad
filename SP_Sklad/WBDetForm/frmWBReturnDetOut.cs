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
        private List<WhMatGet_Result> wh_mat_get { get; set; }
        private GetMatRemain_Result mat_remain { get; set; }
        public int _ka_id { get; set; }

        public frmWBReturnDetOut(BaseEntities db, int? PosId, WaybillList wb, int ka_id)
        {
            InitializeComponent();

            _db = db;
            _PosId = PosId;
            _wb = wb;
            _ka_id = ka_id;

            WHComboBox.Properties.DataSource = DBHelper.WhList();
            MatComboBox.Properties.DataSource = db.MaterialsList.ToList();
        }

        private void frmWBReturnDetOut_Load(object sender, EventArgs e)
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
            }
            else
            {
                _wbd = _db.WaybillDet.Find(_PosId);
            }

            if (_wbd != null)
            {
                if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Modified)
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
                BasePriceEdit.DataBindings.Add(new Binding("EditValue", _wbd, "BasePrice", true, DataSourceUpdateMode.OnValidation));
                
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


      //      PriceNotNDSEdit.EditValue = BasePriceEdit.Value;
     //       TotalSumEdit.EditValue = Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(PriceNotNDSEdit.EditValue);
     //       SummAllEdit.EditValue = Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(DiscountPriceEdit.EditValue);
     //       TotalNdsEdit.EditValue = Convert.ToDecimal(SummAllEdit.EditValue) - Convert.ToDecimal(TotalSumEdit.EditValue);

            return recult;
        }

        private void MatComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = (MaterialsList)MatComboBox.GetSelectedDataRow();
            if (row == null)
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

            GetContent();
        }

        private void GetContent()
        {
            if (_wbd.WId == null || _wbd.MatId == 0)
            {
                return;
            }
            wh_mat_get = _db.WhMatGet(0, _wb.WaybillMove.SourceWid, _ka_id, DBHelper.ServerDateTime(), 0, "*", 0, "", DBHelper.CurrentUser.UserId, 0).ToList();
      

            mat_remain = _db.GetMatRemain(_wbd.WId, _wbd.MatId).FirstOrDefault();

            if (mat_remain != null)
            {
                RemainWHEdit.EditValue = mat_remain.RemainInWh;
                RsvEdit.EditValue = mat_remain.Rsv;
                CurRemainEdit.EditValue = mat_remain.Remain;
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

        private void OkButton_Click(object sender, EventArgs e)
        {
            _wbd.Price = Convert.ToDecimal(PriceNotNDSEdit.EditValue);

            if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Detached)
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
            catch (System.Data.Entity.Infrastructure.DbUpdateException exp)
            {
                MessageBox.Show(exp.InnerException.InnerException.Message);
            }
        }

    }
}
