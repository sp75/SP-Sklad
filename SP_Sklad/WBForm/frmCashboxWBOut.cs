using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.Common;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;
using SP_Sklad.Reports;

namespace SP_Sklad.WBForm
{
    public partial class frmCashboxWBOut : DevExpress.XtraEditors.XtraForm
    {
        private List<GetWayBillDetOut_Result> wbd_list { get; set; }
        private BaseEntities _db { get; set; }
        public WaybillList wb { get; set; }
        public bool is_new_record { get; set; }
        public int? _wbill_id { get; set; }
        private int current_wid { get; set; }
           private DiscCards disc_card { get; set; }
        private GetWayBillDetOut_Result wbd_row
        {
            get
            {
                return WaybillDetOutGridView.GetFocusedRow() as GetWayBillDetOut_Result;
            }
        }

        public frmCashboxWBOut()
        {
            InitializeComponent();

            _db = new BaseEntities();

            current_wid = 34;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if ( !DBHelper.CheckOrderedInSuppliers(wb.WbillId, _db)) return;

            if (!DBHelper.CheckInDate(wb, _db, wb.OnDate))
            {
                return;
            }

       //     payDocUserControl1.Execute(wb.WbillId);

            wb.UpdatedAt = DateTime.Now;
            _db.Save(wb.WbillId);

            if (!wbd_list.Any(w => w.Rsv == 0 && w.PosType == 0 && w.Total > 0))
            {
                var ex_wb = _db.ExecuteWayBill(wb.WbillId, null, DBHelper.CurrentUser.KaId).ToList();
            }

            is_new_record = false;

            Close();
        }

        private void frmCashboxWBOut_Load(object sender, EventArgs e)
        {
          //  KagentComboBox.Properties.DataSource = DBHelper.Kagents;
         //   PersonComboBox.Properties.DataSource = DBHelper.Persons;

            is_new_record = true;

            wb = _db.WaybillList.Add(new WaybillList()
            {
                Id = Guid.NewGuid(),
                WType = -1,
                OnDate = DBHelper.ServerDateTime(),
                Num = new BaseEntities().GetDocNum("wb_out").FirstOrDefault(),
                CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                OnValue = 1,
                PersonId = DBHelper.CurrentUser.KaId,
                EntId = DBHelper.Enterprise.KaId,
                UpdatedBy = DBHelper.CurrentUser.UserId,
                KaId = 53,
                Nds = 0
            });

            _db.SaveChanges();

            _wbill_id = wb.WbillId;

            DBHelper.UpdateSessionWaybill(wb.WbillId);

            WaybillListBS.DataSource = wb;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            wb.Kontragent = _db.Kagent.Find(wb.KaId);

            IHelper.ShowMatListByWH(_db, wb);

            _db.SaveChanges();

            _db.ReservedAllPosition(wb.WbillId, DBHelper.CurrentUser.UserId).ToList();

            RefreshDet();

            WaybillDetOutGridView.MoveLastVisible();
        }

        private void RefreshDet()
        {
            wbd_list = _db.GetWayBillDetOut(_wbill_id).OrderBy(o => o.Num).ToList();

            if (disc_card != null)
            {
                wbd_list.Add(new GetWayBillDetOut_Result { Discount = disc_card.OnValue, MatName = "Дисконтна картка", Num = wbd_list.Count() + 1, CardNum = disc_card.Num, PosType = 3 });
            }

            int top_row = WaybillDetOutGridView.TopRowIndex;
            WaybillDetOutBS.DataSource = wbd_list;
            WaybillDetOutGridView.TopRowIndex = top_row;

            WaybillDetOutGridView.RefreshData();

            textEdit5.Text = Math.Round(Convert.ToDouble( wbd_list.Sum(s => s.Amount * s.BasePrice)) ,2 ).ToString();
            textEdit1.Text = Math.Round(Convert.ToDouble(wbd_list.Sum(s => (s.Amount * s.BasePrice) - s.Total ) ),2 ).ToString();
            textEdit2.Text = wbd_list.Sum(s => s.Total).ToString();

            // GetOk();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCashboxWBOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

            if (is_new_record)
            {
                _db.DeleteWhere<WaybillList>(w => w.WbillId == _wbill_id);
            }


            _db.Dispose();

        }

        private void frmCashboxWBOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            ;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            AmountEdit.Text += ((SimpleButton)sender).Text;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void BarCodeTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !String.IsNullOrEmpty(BarCodeTextEdit.Text))
            {
                var mat = _db.Materials.FirstOrDefault(w => w.BarCode == BarCodeTextEdit.Text);

                if (mat != null)
                {
                    AddMat(mat);
                }

                BarCodeTextEdit.Text = "";
            }
        }


        private void AddMat(Materials mat)
        {
            var p_type = (wb.Kontragent != null ? (wb.Kontragent.PTypeId ?? DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId) : DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId);
            var mat_price = DB.SkladBase().GetListMatPrices(mat.MatId, wb.CurrId, p_type).FirstOrDefault();

            var discount = _db.GetDiscount(wb.KaId, mat.MatId).FirstOrDefault();
            var remain_in_wh = _db.MatRemainByWh(mat.MatId, 0, wb.KaId, DateTime.Now, "*", DBHelper.CurrentUser.UserId).ToList();
            var price = mat_price != null ? (mat_price.Price ?? 0) : 0;

            var num = wb.WaybillDet.Count();
            var wbd = new WaybillDet
            {
                WbillId = wb.WbillId,
                Num = ++num,
                OnDate = wb.OnDate,
                MatId = mat.MatId,
                WId = remain_in_wh.Any() ? remain_in_wh.First().WId : (DBHelper.WhList.Any(w => w.Def == 1) ? DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId : DBHelper.WhList.FirstOrDefault().WId),
                Amount = 1,
                Price = price - (price * (discount ?? 0.00m) / 100),
                PtypeId = mat_price != null ? mat_price.PType : null,
                Discount = disc_card != null ? disc_card.OnValue : (discount ?? 0.00m),
                Nds = wb.Nds,
                CurrId = wb.CurrId,
                OnValue = wb.OnValue,
                BasePrice = price + Math.Round(price * wb.Nds.Value / 100, 2),
                PosKind = 0,
                PosParent = 0,
                DiscountKind = disc_card != null ? 2 : 0,

            };
            _db.WaybillDet.Add(wbd);
            _db.SaveChanges();

         

            RefreshDet();
            WaybillDetOutGridView.MoveLast();
        }

        private void GetSammary()
        {

        }

        private void DisCartButton_Click(object sender, EventArgs e)
        {
            using (var frm = new frmSetDiscountCard(_db, wb))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    disc_card = frm.cart;
                  //  KagentComboBox.EditValue = wb.KaId;

                    RefreshDet();
                }
            }
        }

        private void simpleButton12_Click_1(object sender, EventArgs e)
        {
            AmountEdit.Text = "";
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            if (wbd_row != null)
            {
                if (wbd_row.PosType == 0)
                {
                    _db.DeleteWhere<WaybillDet>(w => w.PosId == wbd_row.PosId);
                }

                if (wbd_row.PosType == 1)
                {
                    _db.DeleteWhere<WayBillSvc>(w => w.PosId == wbd_row.PosId * -1);
                }

                if (wbd_row.PosType == 3)
                {
                    disc_card = null;

                    foreach (var item in _db.WaybillDet.Where(w => w.WbillId == wb.WbillId))
                    {
                        if (item.DiscountKind == 2)
                        {
                            var DiscountPrice = item.BasePrice;
                            item.Price = DiscountPrice * 100 / (100 + item.Nds.Value);
                            item.Discount = 0;
                            item.DiscountKind = 0;
                            if (item.WayBillDetAddProps != null)
                            {
                                item.WayBillDetAddProps.CardId = null;
                            }
                            else
                            {
                                _db.WayBillDetAddProps.Add(new WayBillDetAddProps
                                {
                                    CardId = null,
                                    PosId = item.PosId
                                });
                            }
                        }
                    }
                }

                _db.SaveChanges();

                RefreshDet();
            }
        }

        private void WaybillDetOutGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Amount")
            {
                SetAmount(Convert.ToDecimal(e.Value));
            }
        }

        private void simpleButton5_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AmountEdit.Text))
            {
                SetAmount(Convert.ToDecimal(AmountEdit.Text));
            }
        }

        private void SetAmount(decimal amount)
        {
            var wbd = _db.WaybillDet.FirstOrDefault(w => w.PosId == wbd_row.PosId);

            if (wbd_row.Rsv == 0 && wbd_row.PosType == 0)
            {
                wbd.Amount = amount;
                wbd.Checked = 1;
            }
            _db.SaveChanges();

            IHelper.MapProp(_db.GetWayBillDetOut(_wbill_id).FirstOrDefault(w => w.PosId == wbd_row.PosId), wbd_row);

            RefreshDet();
        }

        private void WaybillDetOutGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            if (wbd_row.PosType == 0)
            {
                textBox1.Text = wbd_row.MatName;
                label1.Text = string.Format("{0} Х {1} {2} = {3} грн.", Math.Round(wbd_row.BasePrice.Value, 2), wbd_row.Amount, wbd_row.MsrName, wbd_row.Total);
            }
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            wb.UpdatedAt = DateTime.Now;
            PrintDoc.Show(wb.Id, wb.WType, _db);
        }

        private void simpleButton24_Click(object sender, EventArgs e)
        {
            using (var frm = new frmSetCustomDiscount(_db, wb))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    RefreshDet();
                }
            }
        }
    }
}
