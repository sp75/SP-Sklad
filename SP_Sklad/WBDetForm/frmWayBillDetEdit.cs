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
using SkladEngine.DBFunction;
using SP_Sklad.Common;
using SP_Sklad.Properties;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBDetForm
{
    public partial class frmWayBillDetEdit : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
         private WaybillDet _wbd { get; set; }
        private WayBillDetAddProps wbdp { get; set; }
        private Serials serials { get; set; }

        private int _PosId { get; set; }

        public frmWayBillDetEdit(int PosId)
        {
            InitializeComponent();
            _db = new BaseEntities();
            _PosId = PosId;

            WHComboBox.Properties.DataSource = DBHelper.WhList;
            MatComboBox.Properties.DataSource = _db.v_Materials.AsNoTracking().Where(w=> w.Archived == 0).ToList();
            ProducerTextEdit.Properties.Items.AddRange(_db.WayBillDetAddProps.Where(w => w.Producer != null).Select(s => s.Producer).Distinct().ToList());
        }

        private void frmWayBillDetIn_Load(object sender, EventArgs e)
        {
            panel3.Visible = barCheckItem1.Checked;
            if (!barCheckItem1.Checked) Height -= panel3.Height;

            panel4.Visible = barCheckItem2.Checked;
            if (!barCheckItem2.Checked) Height -= panel4.Height;

            panel5.Visible = barCheckItem3.Checked;
            if (!barCheckItem3.Checked) Height -= panel5.Height;

            if (DBHelper.CurrentUser.ShowPrice == 0)
            {
                if (barCheckItem2.Checked) barCheckItem2.PerformClick();
                barCheckItem2.Visibility = BarItemVisibility.Never;
            }

            _wbd = _db.WaybillDet.Find(_PosId);

            WaybillDetBS.DataSource = _wbd;

            wbdp = _db.WayBillDetAddProps.FirstOrDefault(w => w.PosId == _wbd.PosId);
            if (wbdp == null)
            {
                wbdp = new WayBillDetAddProps();
            }

            WayBillDetAddPropsBS.DataSource = wbdp;

            serials = _db.Serials.FirstOrDefault(w => w.PosId == _wbd.PosId);
            if (serials == null)
            {
                serials = new Serials();
            }

            SerialsBS.DataSource = serials;


            CurrencyBS.DataSource = _db.Currency.Where(w => w.CurrId == _wbd.WaybillList.CurrId).FirstOrDefault();
            GetOk();

            groupControl1.Text = $"{groupControl1.Text}, {DBHelper.NationalCurrency.ShortName}";
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _wbd.Price = _wbd.BasePrice;

            if (wbdp != null && _db.Entry<WayBillDetAddProps>(wbdp).State == EntityState.Detached)
            {
                wbdp.PosId = _wbd.PosId;
                _db.WayBillDetAddProps.Add(wbdp);
            }
            wbdp.Notes = DateTime.Now.ToString();

            if (serials != null && serials.SerialNo != null && _db.Entry<Serials>(serials).State == EntityState.Detached)
            {
                serials.PosId = _wbd.PosId;
                _db.Serials.Add(serials);
            }

            foreach (var item in _db.PosRemains.Where(w=> w.PosId == _wbd.PosId))
            {
                item.AvgPrice = _wbd.Price;
            }

          

            _wbd.WaybillList.UpdatedAt = DBHelper.ServerDateTime();
            _wbd.WaybillList.UpdatedBy = DBHelper.CurrentUser.UserId;

            _db.SaveChanges();


            foreach (var wmt_out in _db.WMatTurn.Where(w => w.PosId == _wbd.PosId && w.TurnType == -1).ToList())
            {
                var wbd_out = _db.WaybillDet.Find(wmt_out.SourceId);
                if (wbd_out != null)
                {
                    wbd_out.AvgInPrice = _db.GetAvgPrice(wbd_out.PosId).FirstOrDefault();
                }
            }
            _db.SaveChanges();
        }

        private void GetRemains()
        {
            var r = _db.SP_MAT_REMAIN_GET_SIMPLE((int)MatComboBox.EditValue, _wbd.OnDate).FirstOrDefault();

            if (r != null)
            {
                RemainEdit.EditValue = r.Remain;
                RsvEdit.EditValue = r.Rsv;
                CurRemainEdit.EditValue = r.Remain - r.Rsv;
                MinPriceEdit.EditValue = r.MinPrice;
                AvgPriceEdit.EditValue = r.AvgPrice;
                MaxPriceEdit.EditValue = r.MaxPrice;
            }
        }

        private void BasePriceEdit_EditValueChanged(object sender, EventArgs e)
        {
            PriceEdit.Value = _wbd.Nds > 0 ? Math.Round((BasePriceEdit.Value * 100 / (100 + Convert.ToDecimal(_wbd.Nds))), 2) : BasePriceEdit.Value;

            GetOk();
        }

        private void PriceEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!PriceEdit.ContainsFocus)
            {
                return;
            }

            _wbd.BasePrice = _wbd.Nds > 0 ? Math.Round(PriceEdit.Value + (PriceEdit.Value * Convert.ToDecimal(_wbd.Nds) / 100), 2) : PriceEdit.Value;
            BasePriceEdit.EditValue = _wbd.BasePrice;

            GetOk();
        }

        bool GetOk()
        {
            bool recult = (MatComboBox.EditValue != DBNull.Value && !String.IsNullOrEmpty(MatComboBox.Text) && WHComboBox.EditValue != DBNull.Value && BasePriceEdit.Value > 0 && PriceEdit.Value > 0 && AmountEdit.Value > 0);

            OkButton.Enabled = recult;

            BotBasePriceEdit.EditValue = PriceEdit.Value;
            BotAmountEdit.EditValue = AmountEdit.Value;
            TotalSumEdit.EditValue = AmountEdit.Value * PriceEdit.Value;
            SummAllEdit.EditValue = AmountEdit.Value * BasePriceEdit.Value;
            TotalNdsEdit.EditValue = (decimal)SummAllEdit.EditValue - (decimal)TotalSumEdit.EditValue;

            return recult;
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {

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
            if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Modified)
            {
                _db.Entry<WaybillDet>(_wbd).Reload();
            }

            Settings.Default.Save();
        }


        private void MatComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = (v_Materials)MatComboBox.GetSelectedDataRow();
            if (row != null)
            {
                labelControl24.Text = row.ShortName;
                labelControl27.Text = row.ShortName;

                GetRemains();

                if (MatComboBox.ContainsFocus)
                {
                    ProducerTextEdit.EditValue = row.Producer;
                
                }
            }

            GetOk();
        }


        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(_wbd.MatId);
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(_wbd.MatId, _db);
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.ShowTurnMaterial(_wbd.MatId);
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.ShowOrdered(_wbd.WaybillList.KaId.Value, 16, _wbd.MatId);
        }

        private void WHComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           
        }
    }
}
