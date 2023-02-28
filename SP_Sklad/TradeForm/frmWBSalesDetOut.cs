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
using SP_Sklad.Common;
using SP_Sklad.Properties;
using SP_Sklad.SkladData;
using EntityState = System.Data.Entity.EntityState;
using System.Data.Entity;
using SkladEngine.DBFunction;
using SkladEngine.DBFunction.Models;

namespace SP_Sklad.WBDetForm
{
    public partial class frmWBSalesDetOut : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private int? _PosId { get; set; }
        private WaybillList _wb { get; set; }
        private WaybillDet _wbd { get; set; }
        private List<GetPosIn_Result> pos_in { get; set; }

        private List<GetMatRemainByWh_Result> mat_remain { get; set; }
        private decimal? CurRemainInWh
        {
            get
            {
                return mat_remain.FirstOrDefault(w => w.WId == _wid)?.CurRemain;
            }
        }

        private bool modified_dataset { get; set; }
        private DiscCards _cart { get; set; }
        private int _wid { get; set; }

        public frmWBSalesDetOut(BaseEntities db, int? PosId, WaybillList wb, DiscCards cart, int wid)
        {
            InitializeComponent();

            _db = db;
            _PosId = PosId;
            _wb = wb;
            _cart = cart;
            _wid = wid;

            WHComboBox.Properties.DataSource = DBHelper.WhList;
            WHComboBox.EditValue = _wid;
            MatComboBox.Properties.DataSource = db.GetRemainByWh(_wid, _wb.OnDate).ToList();
            PriceTypesEdit.Properties.DataSource = DB.SkladBase().PriceTypes.ToList();

            panel3.Visible = barCheckItem1.Checked;
            if (!barCheckItem1.Checked) Height -= panel3.Height;

 
            panel5.Visible = barCheckItem3.Checked;
            if (!barCheckItem3.Checked) Height -= panel5.Height;

            if (DBHelper.CurrentUser.ShowPrice == 0)
            {
                if (barCheckItem2.Checked) barCheckItem2.PerformClick();
                barCheckItem2.Visibility = BarItemVisibility.Never;
            }

            BasePriceEdit.Enabled = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db).AccessEditPrice;
            PriceTypesEdit.Enabled = BasePriceEdit.Enabled;
            panelControl4.Enabled = BasePriceEdit.Enabled;
        }

        private void frmWayBillDetOut_Load(object sender, EventArgs e)
        {

            if (_PosId == null)
            {
                _wbd = new WaybillDet()
                {
                    WbillId = _wb.WbillId,
                    Amount = 0,
                    Discount = _cart != null ? _cart.OnValue : 0,
                    Nds = _wb.Nds,
                    CurrId = _wb.CurrId,
                    OnDate = _wb.OnDate,
                    Num = _db.GetWayBillDetOut(_wb.WbillId).Count() + 1,
                    OnValue = _wb.OnValue,
                    PosKind = 0,
                    PosParent = 0,
                    DiscountKind = _cart != null ? 2 : 0,
                    PtypeId = _db.Kagent.Find(_wb.KaId).PTypeId,
                    WayBillDetAddProps = new WayBillDetAddProps { CardId = _cart != null ? (int?)_cart.CardId : null },
                    WId = _wid
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
                WaybillDetBS.DataSource = _wbd;
                WHComboBox.EditValue = _wid;

                if (modified_dataset)
                {
                    var w_mat_turn = _db.WMatTurn.AsNoTracking().Where(w => w.SourceId == _wbd.PosId).ToList();
                    if (w_mat_turn.Count > 0)
                    {
                        //   _db.WMatTurn.RemoveRange(w_mat_turn);
                        //    _db.SaveChanges();
                        _db.DeleteWhere<WMatTurn>(w => w.SourceId == _wbd.PosId);

                        GetContent(_wbd.WId, _wbd.MatId);

                        foreach (var item in w_mat_turn)
                        {
                            var pos = pos_in.FirstOrDefault(a => a.PosId == item.PosId);
                            if (pos != null)
                            {
                                pos.Amount = item.Amount;

                                if (item.Amount == pos.FullRemain)
                                {
                                    pos.GetAll = 1;
                                }
                            }
                        }
                    }
                }

                if (_wbd.WayBillDetAddProps != null)
                {
                    WayBillDetAddPropsBS.DataSource = _wbd.WayBillDetAddProps;

                    if (_wbd.WayBillDetAddProps.CardId != null)
                    {
                        var disc_card = _db.DiscCards.Find(_wbd.WayBillDetAddProps.CardId);
                        DiscNumEdit.Text = disc_card.Num;
                        if (disc_card.Kagent != null)
                        {
                            textEdit6.Text = disc_card.Kagent.Name;
                        }
                    }
                }

                GetOk();
            }

        }

        bool GetOk()
        {
            bool recult = (MatComboBox.EditValue != DBNull.Value && WHComboBox.EditValue != DBNull.Value && BasePriceEdit.EditValue != DBNull.Value && AmountEdit.EditValue != DBNull.Value);

            OkButton.Enabled = recult;

            RSVCheckBox.Checked = (OkButton.Enabled && pos_in != null && mat_remain != null && pos_in.Count > 0 && AmountEdit.Value <= CurRemainInWh && pos_in.Sum(s => s.FullRemain) >= AmountEdit.Value);
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

            CalcPrice();

            return recult;
        }

        private void CalcPrice()
        {
            BotAmountEdit.Text = AmountEdit.Text;
            var total = Math.Round(BasePriceEdit.Value * AmountEdit.Value, 2);
            var discount = Math.Round((total * DiscountEdit.Value / 100), 2);
            var total_discount = total - discount;
            var nds = Math.Round(total_discount * (_wbd.Nds ?? 0) / 100, 2);

            if (DiscountCheckBox.Checked && AmountEdit.Value > 0)
            {
                DiscountPriceEdit.EditValue = total_discount / AmountEdit.Value;
            }
            else
            {
                DiscountPriceEdit.EditValue = BasePriceEdit.Value;
            }

            PriceNotNDSEdit.EditValue = DiscountPriceEdit.EditValue;
            TotalSumEdit.EditValue = total_discount;
            TotalNdsEdit.EditValue = nds;
            SummAllEdit.EditValue = total_discount + nds;
        }

        private void MatComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = (GetRemainByWh_Result)MatComboBox.GetSelectedDataRow();
            if (row == null)
            {
                return;
            }

            if (MatComboBox.ContainsFocus)
            {
                _wbd.Nds = _wb.Nds > 0 ? row.NDS : _wb.Nds;
                _wbd.MatId = row.MatId;
            }

            labelControl24.Text = row.MsrName;
            labelControl27.Text = row.MsrName;

            var sate = _db.Entry(_wbd).State;
            PriceTypesEdit.EditValue = PriceTypesEdit.EditValue == DBNull.Value ? (_db.Entry(_wbd).State == EntityState.Detached ? _db.PriceTypes.First(w => w.Def == 1).PTypeId : PriceTypesEdit.EditValue) : PriceTypesEdit.EditValue;

            if (PriceTypesEdit.EditValue != DBNull.Value)
            {
                var list_price = _db.GetListMatPrices(row.MatId, _wb.CurrId, (int)PriceTypesEdit.EditValue).FirstOrDefault();
                if (list_price != null)
                {
                    _wbd.BasePrice = list_price.Price != null ? Math.Round(list_price.Price.Value, 4) : 0;
                    BasePriceEdit.Value = _wbd.BasePrice.Value;
                }
            }

            GetDiscount(row.MatId);
            GetContent(_wbd.WId, row.MatId);
            SetAmount();
            GetOk();
        }

        private void GetDiscount(int? MatId)
        {
            var disc = DB.SkladBase().GetDiscount(_wb.KaId, MatId).FirstOrDefault();
            DiscountCheckBox.Checked = (disc > 0 || _wbd.Discount > 0);

            if (_wbd.DiscountKind == 0)
            {
                DiscountEdit.EditValue = disc;
                _wbd.Discount = disc;
            }
        }

        private void GetContent(int? WId, int? MatId)
        {
            if (WId == null || MatId == 0)
            {
                return;
            }


            mat_remain = new MaterialRemain(UserSession.UserId).GetMatRemainByWh(MatId.Value).ToList();

            RemainWHEdit.EditValue = CurRemainInWh;
            RsvEdit.EditValue = mat_remain.Sum(s => s.Rsv);
            CurRemainEdit.EditValue = mat_remain.Sum(s => s.CurRemain);

   //         mat_remain = new MaterialRemain(UserSession.UserId).GetRemainingMaterialInWh(WId.Value, MatId.Value);
    //        GetMatRemainBS.DataSource = mat_remain != null ? mat_remain : new GetMatRemain_Result();

            pos_in = _db.GetPosIn(_wb.OnDate, MatId, WId, 0, DBHelper.CurrentUser.UserId).AsNoTracking().Where(w => w.FullRemain > 0).OrderBy(o => o.OnDate).ToList();
        }

        private void SetAmount()
        {
            if (pos_in == null || mat_remain == null)
            {
                return;
            }

            decimal? sum_amount = pos_in.Sum(s => s.Amount);
            decimal? sum_full_remain = pos_in.Sum(s => s.FullRemain);

            if (pos_in.Count > 0 && AmountEdit.Value <= CurRemainInWh && sum_amount != AmountEdit.Value)
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

                        if (item.Amount == remain)
                        {
                            item.GetAll = 1;
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
            GetOk();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DiscountCheckBox.Checked)
            {
                _wbd.Discount = 0;
                _wbd.DiscountKind = 0;

                if (_wbd.WayBillDetAddProps != null)
                {
                    _wbd.WayBillDetAddProps.CardId = null;
                }
            }
            else if (CheckCustomEdit.Checked)
            {
                if (_wbd.WayBillDetAddProps != null)
                {
                    _wbd.WayBillDetAddProps.CardId = null;
                }
            }

            _wbd.Price = DiscountPriceEdit.Value;

            try
            {

                if (!modified_dataset)
                {
                    _db.WaybillDet.Add(_wbd);
                }
                _db.SaveChanges();

                if (RSVCheckBox.Checked && !_db.WMatTurn.Any(w => w.SourceId == _wbd.PosId) && _db.UserAccessWh.Any(a => a.UserId == DBHelper.CurrentUser.UserId && a.WId == _wbd.WId && a.UseReceived))
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

                _db.Save(_wb.WbillId);
                Close();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException exp)
            {
                _db.UndoAllChanges();

                throw exp;
            }
        }

        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (WHComboBox.EditValue == DBNull.Value || !WHComboBox.ContainsFocus) return;

            GetContent((int?)WHComboBox.EditValue, (int?)MatComboBox.EditValue);
            SetAmount();
            GetOk();
        }

        private void PriceTypesEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!PriceTypesEdit.ContainsFocus || MatComboBox.EditValue == null || PriceTypesEdit.EditValue == null || PriceTypesEdit.EditValue == DBNull.Value)
            {
                return;
            }

            GetMatPrice();
            GetOk();
        }

        private void GetMatPrice()
        {
            if (PriceTypesEdit.EditValue == DBNull.Value)
            {
                return;
            }

            var list_price = _db.GetListMatPrices((int)MatComboBox.EditValue, _wb.CurrId, (int?)PriceTypesEdit.EditValue).FirstOrDefault();
            if (list_price != null)
            {
                _wbd.BasePrice = list_price.Price != null ? Math.Round(list_price.Price.Value, 4) : 0;
                BasePriceEdit.EditValue = _wbd.BasePrice;
            }
        }

        private void BasePriceEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!BasePriceEdit.ContainsFocus)
            {
                return;
            }
            _wbd.BasePrice = BasePriceEdit.Value;
            //     _wbd.PtypeId = null;
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
            DiscountCheckBox.Checked = true;

            if (!CheckCustomEdit.Checked && CheckCustomEdit.ContainsFocus)
            {
                GetDiscount(Convert.ToInt32(MatComboBox.EditValue));
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
            if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Modified)
            {
                _db.Entry<WaybillDet>(_wbd).Reload();
            }

            Settings.Default.Save();
        }

        private void PosInfoBtn_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void btnShowRemainByWH_Click(object sender, EventArgs e)
        {
           

        }

        private void WHComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                WHComboBox.EditValue = IHelper.ShowDirectList(WHComboBox.EditValue, 2);
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
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

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowOrdered(_wb.KaId.Value, -16, _wbd.MatId);
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
                Settings.Default.wb_out_discount = ch;
            }

            if (ItemIndex == 1)
            {
                panel5.Visible = ch;
                if (ch) Height += panel5.Height;
                else Height -= panel5.Height;
                Settings.Default.wb_out_result = ch;
            }
        }

        private void CheckDiscontCartEdit_CheckedChanged(object sender, EventArgs e)
        {
            DiscountCheckBox.Checked = true;
        }

        private void PriceTypesEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                PriceTypesEdit.EditValue = IHelper.ShowDirectList(PriceTypesEdit.EditValue, 8);
                GetMatPrice();
                GetOk();
            }
        }

        private void AmountEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var pos = new frmInParty(pos_in);
                pos.Text = "Прибуткові партії: " + MatComboBox.Text;
                pos.ShowDialog();
                _wbd.Amount = pos_in.Sum(s => s.Amount).Value;
                AmountEdit.Value = _wbd.Amount;

                GetOk();
            }
        }

        private void MatComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var result = IHelper.ShowRemainByWH(MatComboBox.EditValue, WHComboBox.EditValue, 3);
                _wbd.MatId = result.mat_id;
                MatComboBox.EditValue = result.mat_id;
            }
        }
    }
}
