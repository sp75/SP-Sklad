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
    public partial class frmWBReturnDetIn : Form
    {
        BaseEntities _db { get; set; }
        private int? _PosId { get; set; }
        private WaybillList _wb { get; set; }
        private WaybillDet _wbd { get; set; }
        private bool modified_dataset { get; set; }
        private List<GetPosOut_Result> pos_out_list { get; set; }
        private ReturnRel _temp_return_rel { get; set; }
        private List<GetShippedPosIn_Result> ordered_in_list { get; set; }
        
        public frmWBReturnDetIn(BaseEntities db, int? PosId, WaybillList wb)
        {
            InitializeComponent();

              _db = db;
            _PosId = PosId;
            _wb = wb;

            WHComboBox.Properties.DataSource = DBHelper.WhList();
            pos_out_list = _db.GetPosOut(DateTime.Now.AddDays(-30).Date, _wb.OnDate, 0, _wb.KaId, 0).ToList();
            MatComboBox.Properties.DataSource = pos_out_list;
        }



        private void frmWBReturnDetIn_Load(object sender, EventArgs e)
        {
            _wbd = _wbd = _db.WaybillDet.Find(_PosId);

            if (_wbd == null)
            {
                _wbd = new WaybillDet()
                {
                    WbillId = _wb.WbillId,
                    Amount = 0,
                    Num = _wb.WaybillDet.Count() + 1,
                };
                modified_dataset = false;
            }
            else
            {
                _temp_return_rel = _db.ReturnRel.FirstOrDefault(w => w.PosId == _wbd.PosId);
                if (_temp_return_rel != null)
                {
                    MatComboBox.EditValue = _temp_return_rel.OutPosId;

                    _db.ReturnRel.Remove(_temp_return_rel);
                    _db.SaveChanges();
                }

                modified_dataset = (_wbd != null);
            }

            WHComboBox.DataBindings.Add(new Binding("EditValue", _wbd, "WId", true, DataSourceUpdateMode.OnValidation));
            AmountEdit.DataBindings.Add(new Binding("EditValue", _wbd, "Amount"));
            BasePriceEdit.DataBindings.Add(new Binding("EditValue", _wbd, "BasePrice", true, DataSourceUpdateMode.OnValidation));
        }

        private void MatComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = (GetPosOut_Result)MatComboBox.GetSelectedDataRow();

            _wbd.Price = row.Price;
            _wbd.BasePrice = row.Price + row.Price * row.Nds / 100;
            _wbd.Nds = row.Nds;
            _wbd.CurrId = row.CurrId;
            _wbd.OnValue = row.OnValue;
            _wbd.OnDate = row.OnDate;
            _wbd.WId = row.WID;
            _wbd.MatId = row.MatId;

            ordered_in_list = _db.GetShippedPosIn(row.PosId).ToList();

            PosOutAmountEdit.EditValue = row.Amount;
            ReturnAmountEdit.EditValue = row.ReturnAmount;
            RemainEdit.EditValue = row.Remain;

            GetOk();
        }

        public class Temp_ReturnRel
        {
            public int PosId { get; set; }
            public int OutPosId { get; set; }
            public int PPosId { get; set; }
            public int DPosId { get; set; }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var pos_out_row = (GetPosOut_Result)MatComboBox.GetSelectedDataRow();
            bool stop = false;
            int num = _wbd.Num;

            foreach (var item in ordered_in_list.Where(w => w.Remain > 0))
            {

                if (!stop)
                {
                    var t_wbd = _db.WaybillDet.Add(new WaybillDet
                    {

                        WbillId = _wb.WbillId,
                        Price = pos_out_row.Price,
                        BasePrice = pos_out_row.Price + pos_out_row.Price * pos_out_row.Nds / 100,
                        Nds = pos_out_row.Nds,
                        CurrId = pos_out_row.CurrId,
                        OnValue = pos_out_row.OnValue,
                        OnDate = pos_out_row.OnDate,
                        WId = _wbd.WId,
                        MatId = item.MatId,
                        Num = ++num
                    });

                    if (item.Remain >= _wbd.Amount)
                    {
                        t_wbd.Amount = _wbd.Amount;
                        stop = true;
                    }
                    else
                    {
                        _wbd.Amount = item.Remain.Value;

                        _wbd.Amount -= item.Remain.Value;
                    }
                    _db.SaveChanges();

                    _db.ReturnRel.Add(new ReturnRel
                    {
                        PosId = t_wbd.PosId,
                        OutPosId = pos_out_row.PosId,
                        PPosId = item.PosId
                    });

                }
            }
            _db.SaveChanges();
        }

        bool GetOk()
        {
            var pos_out_row = (GetPosOut_Result)MatComboBox.GetSelectedDataRow();

            bool recult = (pos_out_row != null && AmountEdit.Value <= pos_out_row.Remain && pos_out_list != null && MatComboBox.EditValue != DBNull.Value && WHComboBox.EditValue != DBNull.Value && BasePriceEdit.EditValue != DBNull.Value && AmountEdit.EditValue != DBNull.Value);

            OkButton.Enabled = recult;

            BotAmountEdit.Text = AmountEdit.Text;
      /*      TotalSumEdit.EditValue = Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(PriceEdit.EditValue);
            SummAllEdit.EditValue = Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(BasePriceEdit.EditValue);
            TotalNdsEdit.EditValue = Convert.ToDecimal(SummAllEdit.EditValue) - Convert.ToDecimal(TotalSumEdit.EditValue);*/

            return recult;
        }

        private void AmountEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                ;
            }
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void frmWBReturnDetIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            ;
        }

        private void frmWBReturnDetIn_Shown(object sender, EventArgs e)
        {
            GetOk();
            this.Text = this.Text + " " + MatComboBox.Text;
            AmountEdit.Focus();
            /*
                try
                {
                    ComPort->Open();
                    ComPort->ClearBuffer(true, true);
                    Timer1->Enabled = true;
                }
                catch(...)  {}

                if(showOutList) cxButton3->Click();
            */
        }
    }
}
