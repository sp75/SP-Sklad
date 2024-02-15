using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.SkladData;
using EntityState = System.Data.Entity.EntityState;
using SP_Sklad.Common.WayBills;
using SkladEngine.ModelViews;
using SP_Sklad.Properties;
using SP_Sklad.EditForm;
using DevExpress.XtraBars;

namespace SP_Sklad.WBDetForm
{
    public partial class frmWBReturnDetIn : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private int? _PosId { get; set; }
        private WaybillList _wb { get; set; }
        private WaybillDet _wbd { get; set; }
        public List<GetPosOutView> pos_out_list { get; set; }
        private ReturnRel _temp_return_rel { get; set; }
        private List<GetShippedPosIn_Result> ordered_in_list { get; set; }
        public int? outPosId { get; set; }
        private int? _def_wid { get; set; }
        private DateTime _start_date { get; set; }
        private int wtype_out { get; set; }
        private int? _defective { get; set; }

        public frmWBReturnDetIn(BaseEntities db, int? PosId, WaybillList wb, int? wid, DateTime start_date, int? Defective = null)
        {
            InitializeComponent();

            _db = db;
            _PosId = PosId;
            _wb = wb;
            _def_wid = wid;
            _start_date = start_date;
            _defective = Defective;


            if (_wb.WType == 6) // повернення від клієнта
            {
                wtype_out = -1;
            }
            if (_wb.WType == 25) // Реєстрація продаж
            {
                wtype_out = -25;
            }

         //   if(_defective == 1)
       //     {
       //         WHComboBox.Properties.DataSource = DBHelper.WhList.Where(w=> w.RecyclingWarehouse == 1 ).ToList();
      ////      }
       //     else
      //      {
                WHComboBox.Properties.DataSource = DBHelper.WhList;
       //     }
            
        }

        private void frmWBReturnDetIn_Load(object sender, EventArgs e)
        {
            panel5.Visible = barCheckItem3.Checked;
            if (!barCheckItem3.Checked) Height -= panel5.Height;

            _wbd = _db.WaybillDet.Find(_PosId);

            if (_wbd == null)
            {
                _wbd = new WaybillDet()
                {
                    WbillId = _wb.WbillId,
                    Amount = 0,
                    Num = _wb.WaybillDet.Count() + 1,
                };
            }
            else
            {
                _temp_return_rel = _db.ReturnRel.FirstOrDefault(w => w.PosId == _wbd.PosId);
                if (_temp_return_rel != null)
                {
                    _db.ReturnRel.Remove(_temp_return_rel);
                    _db.SaveChanges();

                    ordered_in_list = _db.GetShippedPosIn(_temp_return_rel.OutPosId).ToList();
                }
            }

            WaybillDetBS.DataSource = _wbd;

            if (pos_out_list == null)
            {
                pos_out_list = _db.GetPosOut(_start_date, _wb.OnDate, 0, _wb.KaId, wtype_out);
            }

            MatComboBox.Properties.DataSource = pos_out_list;
            if (_temp_return_rel != null)
            {
                MatComboBox.EditValue = _temp_return_rel.OutPosId;
            }

            if (outPosId != null)
            {
                MatComboBox.EditValue = outPosId.Value;
            }

            GetOk();
        }

        private void MatComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = (GetPosOutView)MatComboBox.GetSelectedDataRow();

            if (row != null)
            {
                GetPosOutBS.DataSource = row;

                int? wid = _def_wid != null ? _def_wid.Value : row.WID;

                PriceNotNDSEdit.EditValue = row.Price;

                _wbd.Price = row.Price;
                _wbd.BasePrice = row.BasePrice;
                _wbd.Nds = row.Nds;
                _wbd.CurrId = row.CurrId;
                _wbd.OnValue = row.OnValue;
                _wbd.OnDate = row.OnDate;
                _wbd.WId = wid;
                _wbd.MatId = row.MatId;
                _wbd.Discount = row.Discount;
               // _wbd.DiscountKind = row.DiscountKind;

                ordered_in_list = _db.GetShippedPosIn(row.PosId).ToList();
                WHComboBox.EditValue = wid;
                labelControl27.Text = row.Measure;
            }

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
            var pos_out_row = (GetPosOutView)MatComboBox.GetSelectedDataRow();
            bool stop = false;
            int num = _wbd.Num;
            decimal amount = _wbd.Amount;

            foreach (var item in ordered_in_list.Where(w => w.Remain > 0))
            {
                if (!stop)
                {
                    var t_wbd = _db.WaybillDet.Add(new WaybillDet
                    {
                        WbillId = _wb.WbillId,
                        Price = pos_out_row.Price,
                        BasePrice = pos_out_row.BasePrice,
                        Nds = pos_out_row.Nds,
                        CurrId = pos_out_row.CurrId,
                        OnValue = pos_out_row.OnValue,
                        OnDate = pos_out_row.OnDate,
                        WId = _wbd.WId,
                        MatId = item.MatId,
                        Discount = pos_out_row.Discount,
                        Defective = _defective,
                        Num = ++num
                    });

                    if (item.Remain >= amount)
                    {
                        t_wbd.Amount = amount;
                        stop = true;
                    }
                    else
                    {
                        t_wbd.Amount = item.Remain.Value;

                        amount -= item.Remain.Value;
                    }
                    _db.SaveChanges();

                    _db.ReturnRel.Add(new ReturnRel
                    {
                        PosId = t_wbd.PosId,
                        OutPosId = pos_out_row.PosId,
                        PPosId = item.PosId
                    });

                }

                _temp_return_rel = null;
            }

            if (_db.Entry<WaybillDet>(_wbd).State != EntityState.Detached)
            {
                _db.WaybillDet.Remove(_wbd);
            }

            _db.SaveChanges();
        }

        bool GetOk()
        {
            var pos_out_row = (GetPosOutView)MatComboBox.GetSelectedDataRow();

            bool recult = (pos_out_row != null && AmountEdit.Value <= pos_out_row.Remain && pos_out_list != null && MatComboBox.EditValue != DBNull.Value && WHComboBox.EditValue != DBNull.Value /*&& BasePriceEdit.EditValue != DBNull.Value*/ && AmountEdit.EditValue != DBNull.Value);

            OkButton.Enabled = recult;

            /*   TotalSumEdit.EditValue = (AmountEdit.EditValue != DBNull.Value ? Convert.ToDecimal(AmountEdit.EditValue) : 0) * _wbd.Price;
               SummAllEdit.EditValue = (AmountEdit.EditValue != DBNull.Value ? Convert.ToDecimal(AmountEdit.EditValue) : 0) * _wbd.BasePrice;
               TotalNdsEdit.EditValue = Convert.ToDecimal(SummAllEdit.EditValue) - Convert.ToDecimal(TotalSumEdit.EditValue);*/



            var discount_price = BasePriceEdit.Value - (BasePriceEdit.Value * (_wbd.Discount ?? 0) / 100);
            var total_discount = Math.Round(discount_price * AmountEdit.Value, 2);

            PriceNotNDSEdit.EditValue = discount_price * 100 / (100 + (_wbd.Nds ?? 0));
            TotalSumEdit.EditValue = total_discount * 100 / (100 + (_wbd.Nds ?? 0));
            TotalNdsEdit.EditValue = total_discount - (decimal)TotalSumEdit.EditValue;
            SummAllEdit.EditValue = total_discount;

            return recult;
        }

        private void AmountEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var f = new frmOutParty();
                f.Text = "Вхідні партії: " +  MatComboBox.Text + ", Контрагент: " ; 
             
                f.OutPartyGridControl.DataSource = ordered_in_list;
                f.ShowDialog();
            }
            else if(e.Button.Index == 2)
            {
                var frm = new frmWeightEdit(MatComboBox.Text, 1);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    AmountEdit.EditValue = frm.AmountEdit.Value;
                }
            }
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void frmWBReturnDetIn_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Modified)
            {
                _db.Entry<WaybillDet>(_wbd).Reload();
            }

            if (_temp_return_rel != null)
            {
                _db.ReturnRel.Add(_temp_return_rel);
                _db.SaveChanges();
            }
        }

        private void frmWBReturnDetIn_Shown(object sender, EventArgs e)
        {
            GetOk();
            this.Text = this.Text + " " + MatComboBox.Text;
            AmountEdit.Focus();

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
          
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
          

        }

        private void AmountEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && OkButton.Enabled)
            {
                OkButton.PerformClick();
            }
        }

        private void WHComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                WHComboBox.EditValue = IHelper.ShowDirectList(WHComboBox.EditValue, 2);
            }
        }

        private void MatComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                var row = (GetPosOutView)MatComboBox.GetSelectedDataRow();
                int matId = row != null ? row.MatId : 0;

                using (var frm = new frmOutMatList(_db, _start_date, _wb.OnDate, matId, _wb.KaId.Value, wtype_out))
                {
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        pos_out_list = frm.pos_out_list;
                        MatComboBox.Properties.DataSource = frm.pos_out_list;

                        if (pos_out_list != null)
                        {
                            var mat_row = frm.bandedGridView1.GetFocusedRow() as GetPosOutView;

                            MatComboBox.EditValue = mat_row.PosId;
                        }
                    }
                }
            }
        }

        private void barCheckItem3_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var item = e.Item as BarCheckItem;

            bool ch = item.Checked;
            int ItemIndex = Convert.ToInt32(e.Item.Tag);

            if (ItemIndex == 2)
            {
                panel5.Visible = ch;
                if (ch) Height += panel5.Height;
                else Height -= panel5.Height;
                Settings.Default.ch_view3 = ch;
            }
        }
    }
}
