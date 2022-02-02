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
using System.Data.Entity.Core.Objects;
using SP_Sklad.Properties;
using RawInput_dll;

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

        public WhMatGet_Result focused_wh_mat
        {
            get { return WhMatGridView.GetFocusedRow() as WhMatGet_Result; }
        }

        private UserSettingsRepository user_settings { get; set; }
        private long KeyDownTicks { get; set; }
        private readonly RawInput _rawinput;
        public frmCashboxWBOut()
        {
            InitializeComponent();

            _rawinput = new RawInput(Handle, true);

            //     _rawinput.AddMessageFilter();   // Adding a message filter will cause keypresses to be handled
            //     Win32.DeviceAudit();            // Writes a file DeviceAudit.txt to the current directory

            _rawinput.KeyPressed += OnKeyPressed;


            _db = new BaseEntities();

            current_wid = 34;
        }


        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DBHelper.CheckOrderedInSuppliers(wb.WbillId, _db)) return;

            if (!DBHelper.CheckInDate(wb, _db, wb.OnDate))
            {
                return;
            }

            //     payDocUserControl1.Execute(wb.WbillId);

            wb.UpdatedAt = DateTime.Now;
            _db.Save(wb.WbillId);

            wbd_list = _db.GetWayBillDetOut(_wbill_id).OrderBy(o => o.Num).ToList();

            if (!wbd_list.Any(w => w.Rsv == 0 && w.PosType == 0 && w.Total > 0))
            {
                var ex_wb = _db.ExecuteWayBill(wb.WbillId, null, DBHelper.CurrentUser.KaId).ToList();
            }

            is_new_record = false;

            Close();
        }

        private void frmCashboxWBOut_Load(object sender, EventArgs e)
        {
            user_settings = new UserSettingsRepository(UserSession.UserId, _db);

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
                KaId = user_settings.DefaultBuyer,
                Nds = 0
            });

            _db.SaveChanges();

            _wbill_id = wb.WbillId;

            DBHelper.UpdateSessionWaybill(wb.WbillId);

            WaybillListBS.DataSource = wb;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            _rawinput.KeyPressed -= OnKeyPressed;

            wb.Kontragent = _db.Kagent.Find(wb.KaId);

            IHelper.ShowMatListByWH(_db, wb, disc_card);

            _db.SaveChanges();

            _rawinput.KeyPressed += OnKeyPressed;

            RefreshDet();

            WaybillDetOutGridView.MoveLastVisible();
        }

        private void RefreshDet()
        {
            wbd_list = _db.GetWayBillDetOut(_wbill_id).OrderBy(o => o.Num).ToList();

            if (disc_card != null)
            {
                wbd_list.Add(new GetWayBillDetOut_Result
                {
                    Discount = disc_card.OnValue,
                    MatName = "Дисконтна картка" + (disc_card.KaId != null ? " [" + disc_card.Kagent.Name + "]" : ""),
                    Num = wbd_list.Count() + 1,
                    CardNum = disc_card.Num,
                    PosType = 3
                });
            }

            int top_row = WaybillDetOutGridView.TopRowIndex;
            WaybillDetOutBS.DataSource = wbd_list;
            WaybillDetOutGridView.TopRowIndex = top_row;

            WaybillDetOutGridView.RefreshData();

            textEdit5.Text = Math.Round(Convert.ToDouble(wbd_list.Sum(s => s.Amount * s.BasePrice)), 2).ToString();
            textEdit1.Text = Math.Round(Convert.ToDouble(wbd_list.Sum(s => (s.Amount * s.BasePrice) - s.Total)), 2).ToString();
            textEdit2.Text = wbd_list.Sum(s => s.Total).ToString();

            KAgentBtn.Enabled = disc_card == null || disc_card.KaId == null;

            //  GetOk();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCashboxWBOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_rawinput != null) _rawinput.KeyPressed -= OnKeyPressed;

            DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

            if (is_new_record)
            {
                _db.DeleteWhere<WaybillList>(w => w.WbillId == _wbill_id);
            }


            _db.Dispose();

        }

        private string BarCodeStr { get; set; }
        private void frmCashboxWBOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*   int min_interval = 500000;

               var interval = DateTime.Now.Ticks - KeyDownTicks;

               if (interval < min_interval)
               {
                   label1.Focus();
               }

               if (interval > min_interval)
               {
                   BarCodeStr = string.Empty;
               }

               if (e.KeyChar == 13 && interval < min_interval)
               {

                   var mat = _db.Materials.FirstOrDefault(w => w.BarCode == BarCodeStr);

                   if (mat != null)
                   {
                       AddMat(mat.MatId);
                   }

                   BarCodeStr = "";
               }
               else
               {
                   BarCodeStr += e.KeyChar;
               }

               KeyDownTicks = DateTime.Now.Ticks;*/
        }


        private void OnKeyPressed(object sender, RawInputEventArg e)
        {
      /*      if (!this.IsActive)
            {
                return;
            }*/

            if (e.KeyPressEvent.DeviceName == Settings.Default.barcode_scanner_name && e.KeyPressEvent.Message == Win32.WM_KEYDOWN)
            {
                label1.Focus();
            }

            if (e.KeyPressEvent.DeviceName == Settings.Default.barcode_scanner_name && e.KeyPressEvent.Message == Win32.WM_KEYUP)
            {
                if (e.KeyPressEvent.VKey != 13)
                {
                    BarCodeStr += (char)e.KeyPressEvent.VKey;

                }

                if (e.KeyPressEvent.VKey == 13 && !string.IsNullOrEmpty(BarCodeStr))
                {
                    var mat = _db.Materials.FirstOrDefault(w => w.BarCode == BarCodeStr);

                    if (mat != null)
                    {
                        AddMat(mat.MatId);
                    }
                    else
                    {
                        var bc = _db.v_BarCodes.FirstOrDefault(w => w.BarCode == BarCodeStr);
                        if (bc != null)
                        {
                            AddMat(bc.MatId);
                        }
                        else
                        {
                            textBox1.Text = "Товар не знайдено!";
                        }
                    }


                    BarCodeStr = "";
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2)
            {
                KAgentBtn.PerformClick();
                return true;
            }

            if (keyData == Keys.F3)
            {
                PrintDocBtn.PerformClick();
                return true;
            }

            if (keyData == Keys.F5)
            {
                WhListBtn.PerformClick();
                return true;
            }

            if (keyData == Keys.F7)
            {
                BarCodeBtn.PerformClick();
                return true;
            }

            if (keyData == Keys.F8)
            {
                DiscountBtn.PerformClick();
                return true;
            }

            if (keyData == Keys.F9)
            {
                PayDocBtn.PerformClick();
                return true;
            }

            if (keyData == Keys.F4)
            {
                DisCartButton.PerformClick();
                return true;
            }

            if (keyData == Keys.F10)
            {
                OkButton.PerformClick();
                return true;
            }

            if (keyData == Keys.F11)
            {
                Close();
                return true;
            }

            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            AmountEdit.Text += ((SimpleButton)sender).Text;
        }

        private void BarCodeTextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*  if (e.KeyChar == 13 && !String.IsNullOrEmpty(BarCodeTextEdit.Text))
              {
                  var mat = _db.Materials.FirstOrDefault(w => w.BarCode == BarCodeTextEdit.Text);

                  if (mat != null)
                  {
                      AddMat(mat.MatId);
                  }

                  BarCodeTextEdit.Text = "";
              }*/
        }


        private void AddMat(int mat_id)
        {
            var mat = _db.WaybillDet.FirstOrDefault(w => w.MatId == mat_id && w.WbillId == wb.WbillId);
            if (mat == null)
            {

                var p_type = (wb.Kontragent != null ? (wb.Kontragent.PTypeId ?? DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId) : DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId);
                var mat_price = DB.SkladBase().GetListMatPrices(mat_id, wb.CurrId, p_type).FirstOrDefault();

                var discount = _db.GetDiscount(wb.KaId, mat_id).FirstOrDefault();
                var remain_in_wh = _db.MatRemainByWh(mat_id, 0, 0, DateTime.Now, "*", DBHelper.CurrentUser.UserId).ToList();
                var price = mat_price != null ? (mat_price.Price ?? 0) : 0;

                var num = wb.WaybillDet.Count();
                var wbd = new WaybillDet
                {
                    WbillId = wb.WbillId,
                    Num = ++num,
                    OnDate = wb.OnDate,
                    MatId = mat_id,
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
            }
            else
            {
                mat.Amount += 1;
            }


            _db.SaveChanges();



            RefreshDet();
            WaybillDetOutGridView.MoveLast();
        }

        private void GetSammary()
        {

        }

        private void DisCartButton_Click(object sender, EventArgs e)
        {
            _rawinput.KeyPressed -= OnKeyPressed;

            using (var frm = new frmSetDiscountCard(_db, wb))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    disc_card = frm.cart;

                    RefreshDet();

                    SetFormTitle();
                }
            }

            _rawinput.KeyPressed += OnKeyPressed;
        }

        private void simpleButton12_Click_1(object sender, EventArgs e)
        {
            AmountEdit.Text = string.IsNullOrEmpty(AmountEdit.Text) ? "" : AmountEdit.Text.Remove(AmountEdit.Text.Length - 1);
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

            BarCodeStr = "";
        }

        private void simpleButton5_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AmountEdit.Text))
            {
                SetAmount(Convert.ToDecimal(AmountEdit.Text));
            }

            AmountEdit.Text = "";
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
            if(wbd_row == null)
            {
                return;
            }

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
            _rawinput.KeyPressed -= OnKeyPressed;

            using (var frm = new frmSetCustomDiscount(_db, wb))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    RefreshDet();
                }
            }
            _rawinput.KeyPressed += OnKeyPressed;
        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            if (!ResivedItems() || WaybillDetOutBS.Count == 0)
            {
                return;
            }

            if (!DBHelper.CheckOrderedInSuppliers(wb.WbillId, _db))
            {
                return;
            }

            if (!DBHelper.CheckInDate(wb, _db, wb.OnDate))
            {
                return;
            }

            wb.UpdatedAt = DateTime.Now;

            _db.Save(wb.WbillId);

            using (var pay = new frmCashboxCheckout(_db, wb))
            {
                if (pay.ShowDialog() == DialogResult.OK)
                {
                    OkButton.PerformClick();
                }
            }
        }


        private bool ResivedItems()
        {
            _db.SaveChanges();
            var list = new List<string>();

            var r = new ObjectParameter("RSV", typeof(Int32));

            var wb_list = _db.GetWayBillDetOut(_wbill_id).ToList().Where(w => w.Rsv != 1).ToList();

            foreach (var i in wb_list)
            {
                _db.ReservedPosition(i.PosId, r, DBHelper.CurrentUser.UserId);

                if (r.Value != null && (int)r.Value == 0)
                {
                    list.Add(i.MatName);
                }

            }

            if (list.Any())
            {
                MessageBox.Show("Не вдалося зарезервувати: " + String.Join(",", list));

                return false;
            }

            return true;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (!panel9.Visible)
            {
                splitterControl1.Visible = true;
                panel9.Visible = true;
                WhMatGridControl.DataSource = DB.SkladBase().WhMatGet(0, 0, 0, DateTime.Now, 0, "*", 0, "", DBHelper.CurrentUser.UserId, 0).ToList();
                WhMatGridView.ExpandAllGroups();
            }
            else
            {
                splitterControl1.Visible = false;
                panel9.Visible = false;
            }
        }

        private void WhMatGridView_DoubleClick(object sender, EventArgs e)
        {
            if (focused_wh_mat != null)
            {
                AddMat(focused_wh_mat.MatId);
            }
        }

        private void frmCashboxWBOut_Shown(object sender, EventArgs e)
        {
            Text = string.Format("РМК [Касир: {0}, Продавець: {1}, Покупець: {2} ]", DBHelper.CurrentUser.Name, (DBHelper.Enterprise != null ? DBHelper.Enterprise.Name : ""), DBHelper.Kagents.FirstOrDefault(w => w.KaId == wb.KaId).Name);
            label1.Focus();

            if(string.IsNullOrEmpty(Settings.Default.barcode_scanner_name))
            {
                textBox1.Text = "Налаштуйте сканер штрихкодів, Сервіс->Налаштування->Торгове обладнання";
            }
        }

        private void simpleButton25_Click(object sender, EventArgs e)
        {
            _rawinput.KeyPressed -= OnKeyPressed;

            using (var frm = new frmKagents())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    wb.KaId = frm.focused_row.KaId;

                    SetFormTitle();
                }
            }

            _rawinput.KeyPressed += OnKeyPressed;
        }

        private void SetFormTitle()
        {
            Text = string.Format("РМК [Касир: {0}, Продавець: {1}, Покупець: {2} ]", DBHelper.CurrentUser.Name, (DBHelper.Enterprise != null ? DBHelper.Enterprise.Name : ""), DBHelper.Kagents.FirstOrDefault(w => w.KaId == wb.KaId).Name);
        }

        private void simpleButton11_Click_1(object sender, EventArgs e)
        {
            _rawinput.KeyPressed -= OnKeyPressed;

            using (var frm = new frmBarCode())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var mat = _db.Materials.FirstOrDefault(w => w.BarCode == frm.BarCodeEdit.Text);

                    if (mat != null)
                    {
                        AddMat(mat.MatId);
                    }
                    else
                    {
                        var bc = _db.v_BarCodes.FirstOrDefault(w => w.BarCode == frm.BarCodeEdit.Text);
                        if (bc != null)
                        {
                            AddMat(bc.MatId);
                        }
                    }
                }
            }

            _rawinput.KeyPressed += OnKeyPressed;
        }

        private void frmCashboxWBOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((is_new_record || _db.IsAnyChanges()) && OkButton.Enabled)
            {
                var m_recult = MessageBox.Show(Resources.save_wb, "Видаткова накладна №" + wb.Num, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (m_recult == DialogResult.Yes)
                {
                    OkButton.PerformClick();
                }

                if (m_recult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

            }
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            if(!AmountEdit.Text.Any(a=> a == ','))
            {
                AmountEdit.Text += ",";
            }
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            if(NumPadPanel.Visible)
            {
                NumPadPanel.Visible = false;
            }
            else
            {
                NumPadPanel.Visible = true;
            }
        }

    }
}
