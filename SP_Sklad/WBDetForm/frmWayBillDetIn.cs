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
using SP_Sklad.Common;
using SP_Sklad.Properties;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBDetForm
{
    public partial class frmWayBillDetIn : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private WaybillList _wb { get; set; }
        private WaybillDet _wbd { get; set; }
        private WayBillDetAddProps wbdp { get; set; }
        private Serials serials { get; set; }
        private int? _PosId { get; set; }
        private bool modified_dataset { get; set; }

        public frmWayBillDetIn(BaseEntities db, int? PosId, WaybillList wb)
        {
            InitializeComponent();
            _db = db;
            _wb = wb;
            _PosId = PosId;

            WHComboBox.Properties.DataSource = DBHelper.WhList;
            MatComboBox.Properties.DataSource = db.MaterialsList.ToList();
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

            if (_PosId == null)
            {
                _wbd = new WaybillDet()
                {
                    WbillId = _wb.WbillId,
                    OnDate = _wb.OnDate,
                    Discount = 0,
                    Nds = _wb.Nds,
                    CurrId = _wb.CurrId,
                    OnValue = _wb.OnValue,
                    Num = _wb.WaybillDet.Count() + 1,
                    PosKind = 0,
                    PosParent = 0,
                    DiscountKind = 0,
                    Amount = 1

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
                var wid = _wbd.WId;
                WaybillDetBS.DataSource = _wbd;
                WHComboBox.EditValue = wid;

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

                MatComboBox.Enabled = (wbdp == null || wbdp.WbMaked == null);
                MatEditBtn.Enabled = MatComboBox.Enabled;
             //   AmountEdit.Enabled = (MatComboBox.Enabled  );
                ManufEditBtn.Visible = ((wbdp == null || wbdp.WaybillList == null || wbdp.WaybillList.WType == -20) && _wb.WType == 5);

                CurrencyBS.DataSource = _db.Currency.Where(w => w.CurrId == _wb.CurrId).FirstOrDefault();

              
            }
            else
            {
                AmountEdit.EditValue = 1;
                PriceEdit.EditValue = 0;
                ManufEditBtn.Visible = (_wb.WType == 5);
            }

            GetOk();

            groupControl1.Text = $"{groupControl1.Text}, {DBHelper.NationalCurrency.ShortName}";
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!modified_dataset)
                {
                    _db.WaybillDet.Add(_wbd);
                }
                _db.Save(_wb.WbillId);

                /*   if (_wb.WType == 16)
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
                   }*/

                //   if (Serials->State == dsInsert || Serials->State == dsEdit) Serials->Post();
                //      if (WayBillDetAddProps->State == dsInsert || WayBillDetAddProps->State == dsEdit) WayBillDetAddProps->Post();

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

                if (_wb.WType == 1)
                {
                    _db.UpdWaybillDetPrice(_wb.WbillId);
                }

                //якщо позиція є замовлення в постачальника
                var wmt = _db.WMatTurn.FirstOrDefault(w => w.SourceId == _wbd.PosId && w.TurnType == 3);
                if (wmt != null || _wb.WType == 16)
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

                _db.Save(_wb.WbillId);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException exp)
            {
                _db.UndoAllChanges();

                throw exp;
            }

        }

        private void MatComboBox_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void GetRemains()
        {
            var r = _db.SP_MAT_REMAIN_GET_SIMPLE((int)MatComboBox.EditValue, _wb.OnDate).FirstOrDefault();

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
            GetOk();

            if (!BasePriceEdit.ContainsFocus || PriceEdit.ContainsFocus)
            {
                return;
            }

            _wbd.Price = _wbd.Nds > 0 ? Math.Round((BasePriceEdit.Value * 100 / (100 + Convert.ToDecimal(_wbd.Nds))), 2) : BasePriceEdit.Value;
        }

        private void PriceEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();

            if (!PriceEdit.ContainsFocus || BasePriceEdit.ContainsFocus)
            {
                return;
            }

            _wbd.BasePrice = _wbd.Nds > 0 ? Math.Round(PriceEdit.Value + (PriceEdit.Value * Convert.ToDecimal(_wbd.Nds) / 100), 2) : PriceEdit.Value;
        }

        bool GetOk()
        {
            bool recult = (MatComboBox.EditValue != DBNull.Value && !String.IsNullOrEmpty(MatComboBox.Text) && WHComboBox.EditValue != DBNull.Value && BasePriceEdit.Value > 0 && PriceEdit.Value > 0 && AmountEdit.Value > 0 );

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
            if (!WHComboBox.ContainsFocus)
            {
                return;
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
            if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Modified)
            {
                _db.Entry<WaybillDet>(_wbd).Reload();
            }

            Settings.Default.Save();
        }

        private void MatEditBtn_Click(object sender, EventArgs e)
        {
            MatComboBox.EditValue = IHelper.ShowDirectList(MatComboBox.EditValue, 5);
            if( MatComboBox.EditValue != DBNull.Value)
            {
                SetPrice((int)MatComboBox.EditValue);
            }

            if (_wbd != null)
            {
                var p = _db.WayBillDetAddProps.FirstOrDefault(w => w.PosId == _wbd.PosId);
                if (p != null && p.WbMaked != null)
                {
                    p.WbMaked = null;
                    MatComboBox.Enabled = true;
                    //    MatEditBtn.Enabled = true;
                    AmountEdit.Enabled = true;
                }
            }
        }

        private void ManufEditBtn_Click(object sender, EventArgs e)
        {
            using (var frm = new frmManufacturing(_db,2))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    wbdp.WbMaked = frm.wb_focused_row.WbillId;
                    serials.SerialNo = frm.wb_focused_row.Num;

                    MatComboBox.EditValue = frm.wb_focused_row.MatId;
                    AmountEdit.EditValue = (frm.wb_focused_row.AmountOut ?? 0) - (frm.wb_focused_row.ShippedAmount ?? 0);
                    BasePriceEdit.EditValue = Math.Round((frm.wb_focused_row.SummAll / frm.wb_focused_row.AmountOut) ?? 0, 4);
                    PriceEdit.EditValue = _wbd.BasePrice;

                    MatComboBox.Enabled = false;
                    MatEditBtn.Enabled = false;
                 //   AmountEdit.Enabled = false;

                    GetOk();
                }
            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
         
        }

        private void MatComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = (MaterialsList)MatComboBox.GetSelectedDataRow();
            if (row != null)
            {
                //   _wbd.Nds = row.NDS;  треба подумати як правильно
                _wbd.WId = row.WId;
                labelControl24.Text = row.MeasuresName;
                labelControl27.Text = row.MeasuresName;

                GetRemains();

                if (MatComboBox.ContainsFocus)
                {
                    ProducerTextEdit.EditValue = row.Produced;
                    SetPrice(row.MatId);
                }
            }

            GetOk();
        }

        private void SetPrice(int mat_id)
        {
            var get_last_price_result = _db.GetLastPrice(mat_id, _wb.KaId, 1, _wb.OnDate).FirstOrDefault();
            if (get_last_price_result != null)
            {
                _wbd.Price = (get_last_price_result.Price ?? 0) / _wb.OnValue;
                _wbd.BasePrice = _wbd.Nds > 0 ? Math.Round(_wbd.Price.Value + (PriceEdit.Value * Convert.ToDecimal(_wbd.Nds) / 100), 2) : _wbd.Price.Value;
            }
            else
            {
                _wbd.Price = 0;
                _wbd.BasePrice = 0;
            }
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
            IHelper.ShowOrdered(_wb.KaId.Value, 16, _wbd.MatId);
        }

        private void WHComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                WHComboBox.EditValue = IHelper.ShowDirectList(WHComboBox.EditValue, 2);
            }
        }
    }
}
