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
using CheckboxIntegration.Models;
using CheckboxIntegration.Client;
using SP_Sklad.TradeForm;

namespace SP_Sklad.WBForm
{
    public partial class frmCashboxWBOut : DevExpress.XtraEditors.XtraForm
    {
        private List<GetWayBillDetOut_Result> wbd_list { get; set; }
        private BaseEntities _db { get; set; }
        public WaybillList wb { get; set; }
        public bool is_new_record { get; set; }
        public int? _wbill_id { get; set; }
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

        private string _access_token { get; set; }
        public bool is_authorization { get; set; }
        private string CashDesksName { get; set; }
        private EnterpriseWorker Enterprise { get; set; }
        private v_Kagent TradingPoint { get; set; }

        public frmCashboxWBOut(string access_token, int? wbill_id = null)
        {
            _access_token = access_token;
            is_authorization = !string.IsNullOrEmpty(_access_token);
            _wbill_id = wbill_id;

            InitializeComponent();

            _rawinput = new RawInput(Handle, true);

            //     _rawinput.AddMessageFilter();   // Adding a message filter will cause keypresses to be handled
            //     Win32.DeviceAudit();            // Writes a file DeviceAudit.txt to the current directory

            _rawinput.KeyPressed += OnKeyPressed;

            _db = new BaseEntities();
        }

        private void frmCashboxWBOut_Load(object sender, EventArgs e)
        {
            user_settings = new UserSettingsRepository(UserSession.UserId, _db);
            Enterprise = _db.EnterpriseWorker.Where(w => w.WorkerId == user_settings.DefaultBuyer).FirstOrDefault();
            TradingPoint = DBHelper.TradingPoints.FirstOrDefault(w => w.KaId == user_settings.DefaultBuyer);
            SetPriceBtn.Enabled = user_settings.AccessEditPrice;

            if (Enterprise == null)
            {
                MessageBox.Show("Не визначено підприемство для торгової точки " + TradingPoint.Name);
                Close();
            }

            CashDesksName = _db.CashDesks.FirstOrDefault(w => w.CashId == user_settings.CashDesksDefaultRMK).Name;

            if (_wbill_id == null)
            {
                NewWaybill();
            }
            else
            {
                LoadWaybill(_wbill_id.Value);
            }
        }

        private bool SaveWbill()
        {
            if( WaybillDetOutBS.Count == 0)
            {
                return false;
            }

            if (!DBHelper.CheckOrderedInSuppliers(wb.WbillId, _db))
            {
                return false;
            }

            if (!DBHelper.CheckInDate(wb, _db, wb.OnDate))
            {
                return false;
            }

            var list = ResivedItems();
            if (list.Any())
            {
               // MessageBox.Show("Не вдалося зарезервувати: " + String.Join(",", list));

                return false;
            }


            wb.UpdatedAt = DateTime.Now;
            _db.Save(wb.WbillId);

            wbd_list = _db.GetWayBillDetOut(_wbill_id).OrderBy(o => o.Num).ToList();

            if (!wbd_list.Any(w => w.Rsv == 0 && w.PosType == 0 && w.Total > 0))
            {
                var ex_wb = _db.ExecuteWayBill(wb.WbillId, null, DBHelper.CurrentUser.KaId).ToList();
            }

            is_new_record = false;

            return true;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (WaybillDetOutBS.Count == 0)
            {
                return;
            }

            var list = ResivedItems();
            if (list.Any())
            {
                using (var frm = new frmMessageBox("Інформація", $"Не вдалося зарезервувати деякі позиціїї: {String.Join(", ", list)}, продовжити оплату ?", false))
                {
                    if (frm.ShowDialog() != DialogResult.Yes)
                    {
                        return;
                    }
                }
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


            using (var pay = new frmCashboxCheckout(_db, wb, _access_token))
            {
                if (pay.ShowDialog() == DialogResult.OK)
                {
                    SaveWbill();
                    DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

                    NewWaybill();
                }
            };
        }



        private void LoadWaybill(int wbill_id)
        {
            wb = _db.WaybillList.Find(wbill_id);

            if (wb != null)
            {
                is_new_record = false;
                _wbill_id = wb.WbillId;
                DBHelper.UpdateSessionWaybill(wb.WbillId);

                WaybillListBS.DataSource = wb;
                RefreshDet();
            }
        }

        private void NewWaybill()
        {
            is_new_record = true;

            wb = _db.WaybillList.Add(new WaybillList()
            {
                Id = Guid.NewGuid(),
                WType = -25,
                OnDate = DBHelper.ServerDateTime(),
                Num = new BaseEntities().GetDocNum("wb_sales_out").FirstOrDefault(),
                CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                OnValue = 1,
                PersonId = DBHelper.CurrentUser.KaId,
                EntId = Enterprise.EnterpriseId,
                UpdatedBy = DBHelper.CurrentUser.UserId,
                KaId = user_settings.DefaultBuyer,
                Nds = 0
            });

            _db.SaveChanges();
            disc_card = null;

            _wbill_id = wb.WbillId;
            wb.Kontragent = _db.Kagent.Find(wb.KaId);

            DBHelper.UpdateSessionWaybill(wb.WbillId);

            WaybillListBS.DataSource = wb;
            RefreshDet();
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

            if (keyData == (Keys.Control | Keys.F12))
            {
                simpleButton21.PerformClick();
                return true;
            }

            if (keyData == Keys.F3)
            {
                simpleButton22.PerformClick();
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
                //    PayDocBtn.PerformClick();
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


        private void AddMat(int mat_id)
        {
            var mat = _db.WaybillDet.FirstOrDefault(w => w.MatId == mat_id && w.WbillId == wb.WbillId);
            if (mat == null)
            {
                var amount = 1;
                var p_type = (wb.Kontragent != null ? (wb.Kontragent.PTypeId ?? DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId) : DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId);
                var mat_price = DB.SkladBase().GetListMatPrices(mat_id, wb.CurrId, p_type).FirstOrDefault();
                var base_price = mat_price != null ? Math.Round((mat_price.Price ?? 0), 2) : 0;

                var discount_value = disc_card != null ? disc_card.OnValue : (_db.GetDiscount(wb.KaId, mat_id).FirstOrDefault() ?? 0.00m);

                var total = Math.Round(base_price * amount, 2);
                var discount = Math.Round((total * discount_value / 100), 2);
                var total_discount = total - discount;

                var num = wb.WaybillDet.Count();
                var wbd = new WaybillDet
                {
                    WbillId = wb.WbillId,
                    Num = ++num,
                    OnDate = wb.OnDate,
                    MatId = mat_id,
                    WId = wb.Kontragent.WId,//remain_in_wh.Any() ? remain_in_wh.First().WId : (DBHelper.WhList.Any(w => w.Def == 1) ? DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId : DBHelper.WhList.FirstOrDefault().WId),
                    Amount = amount,
                    Price = discount_value > 0 ? (total_discount / amount) : base_price,
                    PtypeId = mat_price != null ? mat_price.PType : null,
                    Discount = discount_value,
                    Nds = wb.Nds,
                    CurrId = wb.CurrId,
                    OnValue = wb.OnValue,
                    BasePrice = base_price ,
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


        private void DisCartButton_Click(object sender, EventArgs e)
        {
            _rawinput.KeyPressed -= OnKeyPressed;

            using (var frm = new frmSetDiscountCard(_db, wb))
            {
                if (frm.ShowDialog() == DialogResult.OK && frm.cart != null)
                {
                    disc_card = frm.cart;
                    wb.CustomerId = disc_card.KaId;

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
                    wb.CustomerId = null;

                    foreach (var item in _db.WaybillDet.Where(w => w.WbillId == wb.WbillId))
                    {
                        if (item.DiscountKind == 2)
                        {
                            item.Price = item.BasePrice;
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

        private void SetPrice(decimal base_price)
        {
            base_price = Math.Round(base_price, 2);

            var wbd = _db.WaybillDet.FirstOrDefault(w => w.PosId == wbd_row.PosId);

            if (wbd_row.Rsv == 0 && wbd_row.PosType == 0)
            {
                wbd.BasePrice = base_price;
                wbd.PtypeId = null;

                var total = Math.Round(base_price * wbd.Amount, 2);
                var discount = Math.Round((total * (wbd.Discount ?? 0) / 100), 2);
                var total_discount = total - discount;

                wbd.Price = wbd.Discount > 0 && wbd.Amount > 0 ? (total_discount / wbd.Amount) : base_price;
            }
            _db.SaveChanges();

            IHelper.MapProp(_db.GetWayBillDetOut(_wbill_id).FirstOrDefault(w => w.PosId == wbd_row.PosId), wbd_row);

            RefreshDet();
        }


        private void WaybillDetOutGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            if (wbd_row == null)
            {
                textBox1.Text = "";
                label1.Text = "";
                return;
            }

            if (wbd_row.PosType == 0)
            {
                textBox1.Text = wbd_row.MatName;
                label1.Text = string.Format("{0} Х {1} {2} = {3} грн.", Math.Round(wbd_row.Price.Value, 2), wbd_row.Amount, wbd_row.MsrName, wbd_row.Total);
            }
        }

        private List<string> ResivedItems()
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

            return list;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (!panel9.Visible)
            {
                splitterControl1.Visible = true;
                panel9.Visible = true;
                WhMatGridControl.DataSource = DB.SkladBase().WhMatGet(0, wb.Kontragent.WId, 0, DateTime.Now, 0, "*", 0, "", DBHelper.CurrentUser.UserId, 0).ToList();
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
            SetFormTitle();
            label1.Focus();

            if (string.IsNullOrEmpty(Settings.Default.barcode_scanner_name))
            {
                textBox1.Text = "Налаштуйте сканер штрихкодів, Сервіс->Налаштування->Торгове обладнання";
            }

            error_autch_label.Visible = !is_authorization;
            RefundCheckBtn.Enabled = is_authorization;
            simpleButton19.Enabled = is_authorization;
            simpleButton20.Enabled = is_authorization;
        }

        private void SetFormTitle()
        {
            Text = $"РМК [Каса: {CashDesksName}, Касир: {DBHelper.CurrentUser.Name}, Продавець: {Enterprise.Kagent.Name}, Торгова точка: {TradingPoint.Name} ]";
        }

        private void frmCashboxWBOut_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((is_new_record || _db.IsAnyChanges()) && OkButton.Enabled)
            {
                var m_recult = MessageBox.Show(Resources.save_wb, "Реалізація товарів №" + wb.Num, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

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
            if (!AmountEdit.Text.Any(a => a == ','))
            {
                AmountEdit.Text += ",";
            }
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            if (NumPadPanel.Visible)
            {
                NumPadPanel.Visible = false;
            }
            else
            {
                NumPadPanel.Visible = true;
            }
        }

        private void KAgentBtn_Click(object sender, EventArgs e)
        {
            _rawinput.KeyPressed -= OnKeyPressed;

            using (var frm = new frmKagents(1,""))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    wb.CustomerId = frm.focused_row.KaId;

                    SetFormTitle();
                }
            }

            _rawinput.KeyPressed += OnKeyPressed;
        }

        private void DiscountBtn_Click(object sender, EventArgs e)
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

        private void WhListBtn_Click(object sender, EventArgs e)
        {
            _rawinput.KeyPressed -= OnKeyPressed;

            IHelper.ShowMatListByWH(_db, wb, disc_card, wb.Kontragent.WId);

            _db.SaveChanges();

            _rawinput.KeyPressed += OnKeyPressed;

            RefreshDet();

            WaybillDetOutGridView.MoveLastVisible();
        }

        private void BarCodeBtn_Click(object sender, EventArgs e)
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

        private void simpleButton19_Click_1(object sender, EventArgs e)
        {
            using (var frm = new frmNumericKeypad())
            {
                frm.Text = "Внесення коштів";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    decimal val = Convert.ToDecimal(frm.AmountEdit.Text);

                    var receipt = CreateServiceReceipt(Convert.ToInt32(val * 100));

                    if (receipt.error == null)
                    {
                        CreateFinDoc(val, receipt.id);

                        GetReceiptPdf(receipt.id);
                    }
                    else
                    {
                        MessageBox.Show(receipt.error.message);
                    }
                }
            }
        }

        private ReceiptsSellRespond CreateServiceReceipt(int value)
        {
            var req = new ReceiptServicePayload
            {
                id = Guid.NewGuid(),
                payment = new Payment
                {
                    type = PaymentType.CASH.ToString(),
                    value = value,
                    label = "Готівка"
                }
            };

            var new_receipts = new CheckboxClient(_access_token).CreateServiceReceipt(req);

            return new_receipts;
        }

        private void GetReceiptPdf(Guid receipt_id)
        {
            if (receipt_id != Guid.Empty)
            {
                var pdf = new CheckboxClient(_access_token).GetReceiptPdf(receipt_id, ReceiptExportFormat.pdf);
                using (var frm = new frmPdfView(pdf))
                {
                    frm.ShowDialog();
                }
            }
        }

        private void CreateFinDoc(decimal value, Guid receipt_id)
        {
            using (var new_db = new BaseEntities())
            {
                var _pd = new_db.PayDoc.Add(new PayDoc
                {
                    Id = Guid.NewGuid(),
                    Checked = 1,
                    DocNum = new BaseEntities().GetDocNum("pay_doc").FirstOrDefault(),
                    OnDate = DBHelper.ServerDateTime(),
                    Total = value,
                    CTypeId = 1,// За товар
                    WithNDS = 1,// З НДС
                    PTypeId = 1,// Наличкой
                    CashId = user_settings.CashDesksDefaultRMK,
                    CurrId = 2,  //Валюта по умолчанию
                    OnValue = 1, //Курс валюти
                    MPersonId = DBHelper.CurrentUser.KaId,
                    DocType = 6,//Коригування залишку
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId,
                    OperId = Guid.NewGuid(),
                    ReceiptId = receipt_id
                });

                new_db.SaveChanges();
            }
        }

        private void simpleButton20_Click(object sender, EventArgs e)
        {
            using (var frm = new frmNumericKeypad())
            {
                var cashier_shift = new CheckboxClient(_access_token).GetCashierShift();
                frm.AmountEdit.Text = Convert.ToString(cashier_shift.balance.balance / 100.00);

                frm.Text = "Вилучення коштів";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    decimal val = Convert.ToDecimal(frm.AmountEdit.Text) * -1;

                    var receipt = CreateServiceReceipt(Convert.ToInt32(val * 100));

                    if (receipt.error == null)
                    {
                        CreateFinDoc(val, receipt.id);

                        GetReceiptPdf(receipt.id);
                    }
                    else
                    {
                        MessageBox.Show(receipt.error.message);
                    }
                }
            }
        }


        private void simpleButton21_Click(object sender, EventArgs e)
        {
            var m_recult = MessageBox.Show("Відкласти поточний чек?", "Реалізація товарів №" + wb.Num, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

            if (m_recult == DialogResult.Yes)
            {
                wb.UpdatedAt = DateTime.Now;
                _db.Save(wb.WbillId);
                DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

                NewWaybill();
            }

            if (m_recult == DialogResult.No)
            {
                if (is_new_record)
                {
                    _db.DeleteWhere<WaybillList>(w => w.WbillId == _wbill_id);
                }

                NewWaybill();
            }

            disc_card = null;
        }

        private void simpleButton22_Click(object sender, EventArgs e)
        {
            using (var frm = new frmSalesList(user_settings.DefaultBuyer.Value, 0, DateTime.Now.Date, DateTime.Now.Date.AddDays(1)))
            {
                frm.Text = "Відкладені чеки";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var m_recult = MessageBox.Show("Відкласти поточний чек?", "Реалізація товарів №" + wb.Num, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                    if (m_recult == DialogResult.Yes)
                    {
                        wb.UpdatedAt = DateTime.Now;
                        _db.Save(wb.WbillId);
                        DBHelper.UpdateSessionWaybill(_wbill_id.Value, true);

                        if (frm.wb_focused_row != null)
                        {
                            LoadWaybill(frm.wb_focused_row.WbillId);
                        }
                    }

                    if (m_recult == DialogResult.No)
                    {
                        if (is_new_record)
                        {
                            _db.DeleteWhere<WaybillList>(w => w.WbillId == _wbill_id);
                        }
                        if (frm.wb_focused_row != null)
                        {
                            LoadWaybill(frm.wb_focused_row.WbillId);
                        }
                        else
                        {
                            NewWaybill();
                        }
                    }
                }
            }
        }

        private void simpleButton23_Click(object sender, EventArgs e)
        {
            using (var frm = new frmSalesList(user_settings.DefaultBuyer.Value, 1, DateTime.Now.Date.AddDays(-14), DateTime.Now.Date.AddDays(1)))
            {
                frm.Text = "Роздрібні продажі (Чеки)";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var list = _db.GetPosOut(frm.wb_focused_row.OnDate.Date, frm.wb_focused_row.OnDate.Date.AddDays(1), 0, user_settings.DefaultBuyer, -25).Where(w => w.WbillId == frm.wb_focused_row.WbillId && w.Remain > 0).ToList();

                    if (list.Any())
                    {
                        var return_wb = _db.WaybillList.Add(new WaybillList()
                        {
                            Id = Guid.NewGuid(),
                            WType = 25,
                            OnDate = DBHelper.ServerDateTime(),
                            Num = DB.SkladBase().GetDocNum("wb_sales_in").FirstOrDefault(),
                            CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                            OnValue = 1,
                            PersonId = DBHelper.CurrentUser.KaId,
                            UpdatedBy = DBHelper.CurrentUser.UserId,
                            EntId = DBHelper.Enterprise.KaId,
                            KaId = user_settings.DefaultBuyer
                        });
                        _db.SaveChanges();

                        var wid = _db.Kagent.FirstOrDefault(w => w.KaId == user_settings.DefaultBuyer).WId;

                        foreach (var pos_out_row in list)
                        {
                            int num = 0;

                            var ordered_in_list = _db.GetShippedPosIn(pos_out_row.PosId).ToList();
                            foreach (var item in ordered_in_list.Where(w => w.Remain > 0))
                            {
                                var t_wbd = _db.WaybillDet.Add(new WaybillDet
                                {
                                    WbillId = return_wb.WbillId,
                                    Amount = item.Remain.Value,
                                    Price = pos_out_row.Price,
                                    BasePrice = pos_out_row.BasePrice,
                                    Nds = pos_out_row.Nds,
                                    Discount = pos_out_row.Discount,
                                    CurrId = pos_out_row.CurrId,
                                    OnValue = pos_out_row.OnValue,
                                    OnDate = pos_out_row.OnDate,
                                    WId = wid,
                                    MatId = pos_out_row.MatId,
                                    Num = ++num,
                                    Checked = 1
                                });
                                
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

                        //DocEdit.WBEdit(return_wb.WbillId, 25);
                        new frmCashboxRefund(_db, return_wb, _access_token).ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Всі товари по чеку вже повернуто!");
                    }
                }
            }
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            using (var frm = new frmCustomInfo())
            {
                frm.Text = $"Інформація про активну зміну касира: {DBHelper.CurrentUser.Name}";

                if (is_authorization)
                {
                    var new_receipts = new CheckboxClient(_access_token).GetCashierShift();
                    if (new_receipts.error == null)
                    {
                        frm.AddItem("Зміна відкрита", new_receipts.opened_at);
                        frm.AddItem("Продаж за готівку", new_receipts.balance.cash_sales / 100.00);
                        frm.AddItem("Продаж по картці", new_receipts.balance.card_sales / 100.00);
                        frm.AddItem("Повернення готівкою", new_receipts.balance.cash_returns / 100.00);
                        frm.AddItem("Залишок в касі (checkbox)", new_receipts.balance.balance / 100.00);
                    }
                }

                var money = _db.MoneyOnDate(DateTime.Now);
                var cur_user_cash = money.Where(w => w.CashId == user_settings.CashDesksDefaultRMK).Sum(s => s.SaldoDef);
                var cur_user_acc = money.Where(w => w.AccId == user_settings.AccountDefaultRMK).Sum(s => s.SaldoDef);
                frm.AddItem($"Залишок в касі ({CashDesksName})", cur_user_cash);
                frm.AddItem($"Залишок на рахунку ({new BaseEntities().KAgentAccount.FirstOrDefault(w=> w.AccId == user_settings.AccountDefaultRMK).AccNum})", cur_user_acc);

                frm.ShowDialog();
            }
        }

        private void SetPriceBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AmountEdit.Text))
            {
                SetPrice(Convert.ToDecimal(AmountEdit.Text));
            }

            AmountEdit.Text = "";
        }
    }
}
