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

namespace SP_Sklad.WBDetForm
{
    public partial class frmWayBillDetOut : DevExpress.XtraEditors.XtraForm
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
            ProducerTextEdit.Properties.Items.AddRange(_db.WayBillDetAddProps.Where(w => w.Producer != null).Select(s => s.Producer).Distinct().ToList());

            panel3.Visible = barCheckItem1.Checked;
            if (!barCheckItem1.Checked) Height -= panel3.Height;

            panel4.Visible = barCheckItem2.Checked;
            if (!barCheckItem2.Checked) Height -= panel4.Height;

            panel5.Visible = barCheckItem3.Checked;
            if (!barCheckItem3.Checked) Height -= panel5.Height;
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
                    DiscountKind = 0,
                    PtypeId = _db.Kagent.Find(_wb.KaId).PTypeId,
                    WayBillDetAddProps = new WayBillDetAddProps()
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

                if (modified_dataset)
                {
                    var w_mat_turn = _db.WMatTurn.Where(w => w.SourceId == _wbd.PosId).ToList();
                    if (w_mat_turn.Count > 0)
                    {
                        _db.WMatTurn.RemoveRange(w_mat_turn);
                        _db.SaveChanges();

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
                }

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

            PriceNotNDSEdit.EditValue = DiscountPriceEdit.Value * 100 / (100 + _wbd.Nds);
            TotalSumEdit.EditValue = AmountEdit.Value * Convert.ToDecimal(PriceNotNDSEdit.EditValue);
            SummAllEdit.EditValue = AmountEdit.Value * DiscountPriceEdit.Value;
            TotalNdsEdit.EditValue = Convert.ToDecimal(SummAllEdit.EditValue) - Convert.ToDecimal(TotalSumEdit.EditValue);

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
                _wbd.WId = row.WId;
                _wbd.Nds = row.NDS == 1 ? DBHelper.CommonParam.Nds : 0;
                _wbd.MatId = row.MatId;
                ProducerTextEdit.EditValue = row.Produced;
            }

            labelControl24.Text = row.MeasuresName;
            labelControl27.Text = row.MeasuresName;

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

            mat_remain = _db.GetMatRemain(WId, MatId).FirstOrDefault();
            GetMatRemainBS.DataSource = mat_remain != null ? mat_remain : new GetMatRemain_Result();

            pos_in = _db.GetPosIn(_wb.OnDate, MatId, WId, 0).Where(w => w.FullRemain > 0).OrderBy(o => o.OnDate).ToList();
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
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DiscountCheckBox.Checked) _wbd.Discount = 0;

            _wbd.Price = Convert.ToDecimal(PriceNotNDSEdit.EditValue);

            try
            {
                try
                {

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
                        /*    _wbd.WMatTurn1.Add(new WMatTurn
                            {
                                PosId = item.PosId,
                                WId = _wbd.WId.Value,
                                MatId = _wbd.MatId,
                                OnDate = _wb.OnDate,
                                TurnType = _wb.WType == -16 ? -16 : 2,
                                Amount = Convert.ToDecimal(item.Amount),
                                SourceId = _wbd.PosId
                            });*/
                        }
                    }

                    _db.SaveChanges();
                    Close();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException exp)
                {
                    UndoAll(_db);

                    MessageBox.Show(exp.InnerException.InnerException.Message);
                }
            }
            catch { }
        }

        public void UndoAll(BaseEntities context)
        {
            //detect all changes (probably not required if AutoDetectChanges is set to true)
            context.ChangeTracker.DetectChanges();

            //get all entries that are changed
            var entries = context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList();

            //somehow try to discard changes on every entry
            foreach (var dbEntityEntry in entries)
            {
                var entity = dbEntityEntry.Entity;

                if (entity == null) continue;

                if (dbEntityEntry.State == EntityState.Added)
                {
                    //if entity is in Added state, remove it. (there will be problems with Set methods if entity is of proxy type, in that case you need entity base type
                    var set = context.Set(entity.GetType());
                    set.Remove(entity);
                }
                else if (dbEntityEntry.State == EntityState.Modified)
                {
                    //entity is modified... you can set it to Unchanged or Reload it form Db??
                    dbEntityEntry.Reload();
                }
                else if (dbEntityEntry.State == EntityState.Deleted)
                    //entity is deleted... not sure what would be the right thing to do with it... set it to Modifed or Unchanged
                    dbEntityEntry.State = EntityState.Modified;
            }
        }

        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (WHComboBox.EditValue == DBNull.Value || !WHComboBox.ContainsFocus) return;

         //   _wbd.WId = (int)WHComboBox.EditValue;
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
            if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Modified)
            {
                _db.Entry<WaybillDet>(_wbd).Reload();
            }

            Settings.Default.Save();
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (_wb.WType == -16)
            {
                MatComboBox.EditValue = IHelper.ShowDirectList(MatComboBox.EditValue, 5);
                _wbd.MatId = MatComboBox.EditValue != null && MatComboBox.EditValue != DBNull.Value ? (int)MatComboBox.EditValue : _wbd.MatId;
            }
            else
            {
                var result = IHelper.ShowRemainByWH(MatComboBox.EditValue, WHComboBox.EditValue, 1);
            //    _wbd.WId = result.wid;
                WHComboBox.EditValue = result.wid;
            //    _wbd.MatId = result.mat_id;
                MatComboBox.EditValue = result.mat_id;
            }
        }

        private void btnShowRemainByWH_Click(object sender, EventArgs e)
        {
            var result = IHelper.ShowRemainByWH(MatComboBox.EditValue, WHComboBox.EditValue, 2);
            WHComboBox.EditValue = result.wid;
            GetContent(result.wid, (int?)MatComboBox.EditValue);
            SetAmount();
            GetOk();

         /*   Variant savePoint = MatComboBox->EditValue;// 06.12.12

            WayBIllDetWID->Value = frmMain->ShowRemainByWH(MatComboBox->EditValue, WHComboBox->EditValue, 2, MatComboBox->EditValue)->WID;

            if (!savePoint.IsEmpty() && !savePoint.IsNull()) WayBIllDetMATID->Value = savePoint;  // 06.12.12*/

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
            PriceTypesEdit.EditValue = IHelper.ShowDirectList(PriceTypesEdit.EditValue, 8);
            GetMatPrice();
            GetOk();
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
                panel4.Visible = ch;
                if (ch) Height += panel4.Height;
                else Height -= panel4.Height;
                Settings.Default.wb_out_certificat = ch;
            }

            if (ItemIndex == 2)
            {
                panel5.Visible = ch;
                if (ch) Height += panel5.Height;
                else Height -= panel5.Height;
                Settings.Default.wb_out_result = ch;
            }
        }

    }
}
