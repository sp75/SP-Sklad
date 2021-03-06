﻿using System;
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
        private ComPortHelper com_port { get; set; }
        private int? _def_wid { get; set; }
        private DateTime _start_date { get; set; }

        public frmWBReturnDetIn(BaseEntities db, int? PosId, WaybillList wb, int? wid, DateTime start_date)
        {
            InitializeComponent();

            _db = db;
            _PosId = PosId;
            _wb = wb;
            _def_wid = wid;
            _start_date = start_date;

            WHComboBox.Properties.DataSource = DBHelper.WhList;

            com_port = new ComPortHelper(Settings.Default.com_port_name, Convert.ToInt32(Settings.Default.com_port_speed));
        }

        private void frmWBReturnDetIn_Load(object sender, EventArgs e)
        {
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
                pos_out_list = _db.GetPosOut(_start_date, _wb.OnDate, 0, _wb.KaId, -1);
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

                var wid = _def_wid != null ? _def_wid.Value : row.WID;

                _wbd.Price = row.Price;
                BotPriceEdit.EditValue = row.Price;
                _wbd.BasePrice = row.Price + row.Price * row.Nds / 100;
                _wbd.Nds = row.Nds;
                _wbd.CurrId = row.CurrId;
                _wbd.OnValue = row.OnValue;
                _wbd.OnDate = row.OnDate;
                _wbd.WId = wid;
                _wbd.MatId = row.MatId;

                ordered_in_list = _db.GetShippedPosIn(row.PosId).ToList();

                WHComboBox.EditValue = wid;
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
                        BasePrice = pos_out_row.Price + pos_out_row.Price * pos_out_row.Nds / 100,
                        Nds = pos_out_row.Nds,
                        CurrId = pos_out_row.CurrId,
                        OnValue = pos_out_row.OnValue,
                        OnDate = pos_out_row.OnDate,
                        WId = _wbd.WId,
                        MatId = item.MatId,
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

            var fff = _db.Entry<WaybillDet>(_wbd).State;
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

            TotalSumEdit.EditValue = (AmountEdit.EditValue != DBNull.Value ? Convert.ToDecimal(AmountEdit.EditValue) : 0) * _wbd.Price;
            SummAllEdit.EditValue = (AmountEdit.EditValue != DBNull.Value ? Convert.ToDecimal(AmountEdit.EditValue) : 0) * _wbd.BasePrice;
            TotalNdsEdit.EditValue = Convert.ToDecimal(SummAllEdit.EditValue) - Convert.ToDecimal(TotalSumEdit.EditValue);

            return recult;
        }

        private void AmountEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var f = new frmOutParty();
                f.Text = "Вхідні партії: " +  MatComboBox.Text + ", Контрагент: " ; //OrderedOutListKANAME->AsString
              //  frmOutParty->ToDateEdit->Date = frmWBReturnIn->OnDateDBEdit->EditValue;
                f.OutPartyGridControl.DataSource = ordered_in_list;
                f.ShowDialog();
            }
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void frmWBReturnDetIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
            com_port.Close();
            com_port.Dispose();

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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var row = (GetPosOutView)MatComboBox.GetSelectedDataRow();
            int matId = row != null ? row.MatId : 0;

            using (var frm = new frmOutMatList(_db, _start_date, _wb.OnDate, matId, _wb.KaId.Value))
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

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            WHComboBox.EditValue = IHelper.ShowDirectList(WHComboBox.EditValue, 2);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (com_port.weight > 0)
            {
                timer1.Stop();
                AmountEdit.EditValue = com_port.weight;
                com_port.Close();
            }
            /* if (com_port.received != null && com_port.received.IndexOf('<') != -1 && com_port.received.IndexOf('>') != -1)
            {
                timer1.Enabled = false;
                com_port.Close();
                var val = Convert.ToDecimal(Regex.Replace(com_port.received, "[^0-9 ]", ""));

                AmountEdit.EditValue = (val / 100);
            }*/
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            try
            {
                com_port.Open();
            }
            catch { }
        }

        private void AmountEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && OkButton.Enabled)
            {
                OkButton.PerformClick();
            }
        }

    }
}
