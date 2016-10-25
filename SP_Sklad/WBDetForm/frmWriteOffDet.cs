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
using SP_Sklad.EditForm;
using SP_Sklad.SkladData;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBDetForm
{
    public partial class frmWriteOffDet : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private int? _PosId { get; set; }
        private WaybillList _wb { get; set; }
        private WaybillDet _wbd { get; set; }
        private List<GetPosIn_Result> pos_in { get; set; }
        private GetActualRemainByWh_Result mat_remain { get; set; }
        public decimal? amount { get; set; }
        public int? mat_id { get; set; }

        public frmWriteOffDet(BaseEntities db, int? PosId, WaybillList wb)
        {
            InitializeComponent();

            _db = db;
            _PosId = PosId;
            _wb = wb;
        }

        private void frmWriteOffDet_Load(object sender, EventArgs e)
        {
            WHComboBox.Properties.DataSource = DBHelper.WhList();
            MatComboBox.Properties.DataSource = _db.MaterialsList.ToList();

            if (_wb.WType == -5 || _wb.WType == -22)
            {
                WHComboBox.Enabled = false;
                WhEditBtn.Enabled = false;
            }
            if (_wb.WType == -22)
            {
                MatComboBox.Enabled = false;
                MatEditBtn.Enabled = false;
            }

            if (_PosId == null)
            {
                _wbd = new WaybillDet()
                {
                    WbillId = _wb.WbillId,
                    Num = _wb.WaybillDet.Count() + 1,
                    Amount = amount != null ? (decimal)amount : 0,
                    OnValue = _wb.OnValue,
                    WId = _wb.WaybillMove != null ? _wb.WaybillMove.SourceWid : _wb.WayBillMake != null ? _wb.WayBillMake.SourceWId : DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId,
                    Nds = _wb.Nds,
                    CurrId = _wb.CurrId,
                    OnDate = _wb.OnDate,
                    MatId = mat_id != null ? (int)mat_id : 0
                };
            }
            else
            {
                _wbd = _db.WaybillDet.Find(_PosId);
            }

            if (_wbd != null)
            {
                PriceEdit.DataBindings.Add(new Binding("EditValue", _wbd, "Price", true, DataSourceUpdateMode.OnValidation));
                MatComboBox.DataBindings.Add(new Binding("EditValue", _wbd, "MatId"));
                WHComboBox.DataBindings.Add(new Binding("EditValue", _wbd, "WId", true, DataSourceUpdateMode.OnPropertyChanged));
                AmountEdit.DataBindings.Add(new Binding("EditValue", _wbd, "Amount"));

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

            GetContent();
        }

        bool GetOk()
        {
            bool recult = ((int)MatComboBox.EditValue > 0 && WHComboBox.EditValue != DBNull.Value && AmountEdit.Value > 0);

            OkButton.Enabled = recult;

            RSVCheckBox.Checked = (OkButton.Enabled && pos_in != null && mat_remain != null && pos_in.Count > 0 && AmountEdit.Value <= mat_remain.CurRemainInWh);
            if (RSVCheckBox.Checked)
            {
                foreach (var item in pos_in)
                {
                    if (item.CurRemain < item.Amount)
                    {
                        RSVCheckBox.Checked = false;
                        break;
                    }
                }
            }

            BotAmountEdit.Text = AmountEdit.Text;

            decimal summ = Convert.ToDecimal(_wbd.Price) * Convert.ToDecimal(AmountEdit.EditValue);
            decimal summ_nds = (Convert.ToDecimal(_wbd.BasePrice) - (Convert.ToDecimal(_wbd.BasePrice) * 100 / (100 + Convert.ToDecimal(_wbd.Nds)))) * Convert.ToDecimal(AmountEdit.EditValue);
            SummAllEdit.EditValue = summ + summ_nds;

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
                _wbd.MatId = row.MatId;
                GetContent();
            }

            labelControl24.Text = row.MeasuresName;
            labelControl27.Text = row.MeasuresName;
        }

        public void GetContent()
        {
            if (_wbd.WId == null || _wbd.MatId == 0)
            {
                return;
            }

            mat_remain = _db.GetActualRemainByWh(_wbd.WId ,_wbd.MatId).FirstOrDefault();

            if (mat_remain != null)
            {
                RemainWHEdit.EditValue = mat_remain.CurRemainInWh;
                RsvEdit.EditValue = mat_remain.Rsv;
                CurRemainEdit.EditValue = mat_remain.CurRemain;
            }

            pos_in = _db.GetPosIn(_wb.OnDate, _wbd.MatId, _wbd.WId, 0).Where(w => w.CurRemain > 0).OrderByDescending(o => o.OnDate).ToList();

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
                GetOk();
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

          
            decimal? selamount = pos_in.Sum(s => s.Amount);
            decimal? sum = pos_in.Sum(s => s.Amount * s.Price);

            if (selamount > 0)
            {
                _wbd.Price = sum / selamount;
                _wbd.BasePrice = _wbd.Price;
            }
            else
            {
                _wbd.Price = 0;
                _wbd.BasePrice = 0;
            }
            
            if (AmountEdit.Value <= sum_full_remain) RSVCheckBox.Checked = false;

            GetOk();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
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
                        WId = item.WId,
                        MatId = item.MatId,
                        OnDate = _wbd.OnDate.Value,
                        TurnType =  2,
                        Amount = Convert.ToDecimal(item.Amount),
                        SourceId = _wbd.PosId
                    });
                }
            }
      
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

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (AmountEdit.ContainsFocus)
            {
                SetAmount();
            }
        }

        private void RSVCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RSVCheckBox.Checked && RSVCheckBox.ContainsFocus) GetOk();
        }

        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (WHComboBox.ContainsFocus)
            {
                _wbd.WId = (int)WHComboBox.EditValue;
                GetContent();
            }
        }

        private void frmWriteOffDet_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Modified)
            {
                _db.Entry<WaybillDet>(_wbd).Reload();
            }
        }

        private void PosInfoBtn_Click(object sender, EventArgs e)
        {
            using (var pos = new frmInParty(pos_in))
            {
                pos.Text = "Прибуткові партії: " + MatComboBox.Text;
                pos.ShowDialog();
                _wbd.Amount = pos_in.Sum(s => s.Amount).Value;
                AmountEdit.Value = _wbd.Amount;

                SetAmount();
                GetOk();
            }
        }

        private void MatEditBtn_Click(object sender, EventArgs e)
        {
            var f = new frmWhCatalog(1);

            f.uc.whKagentList.Enabled = false;
            f.uc.OnDateEdit.Enabled = false;
            f.uc.bar3.Visible = false;
            f.uc.ByWhBtn.Down = true;
            f.uc.splitContainerControl1.SplitterPosition = 0;
            f.uc.WHTreeList.DataSource = new BaseEntities().GetWhTree(DBHelper.CurrentUser.UserId, 2).Where(w => w.GType == 1 && w.Num == _wbd.WId).ToList();
            f.uc.GrpNameGridColumn.GroupIndex = 0;

            f.uc.isDirectList = true;
            if (f.ShowDialog() == DialogResult.OK)
            {
                _wbd.MatId = f.uc.focused_wh_mat.MatId.Value;
                MatComboBox.EditValue = _wbd.MatId;
                GetContent();
            }
        }

        private void WhEditBtn_Click(object sender, EventArgs e)
        {
            WHComboBox.EditValue = IHelper.ShowDirectList(WHComboBox.EditValue, 2);

            GetContent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(_wbd.MatId);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(_wbd.MatId, _db);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowTurnMaterial(_wbd.MatId);
        }

    }
}
