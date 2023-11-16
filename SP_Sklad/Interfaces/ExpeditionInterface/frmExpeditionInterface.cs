using DevExpress.XtraBars;
using RawInput_dll;
using SP_Sklad.Common;
using SP_Sklad.EditForm;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SP_Sklad.Properties;
using System.Collections.Generic;
using System.IO;
using SP_Sklad.WBDetForm;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;

namespace SP_Sklad.Interfaces.ExpeditionInterface
{
    public partial class frmExpeditionInterface : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private readonly RawInput _rawinput;

        private Expedition exp { get; set; }
        private v_ExpeditionDet focused_row => ExpeditionDetGridView.GetFocusedRow() as v_ExpeditionDet;
        private Guid? _exp_id { get; set; }
        public bool is_new_record { get; set; }
        public BaseEntities _db { get; set; }
    //    private ExpeditionDet det { get; set; }

        public frmExpeditionInterface(Guid? exp_id = null)
        {
            InitializeComponent();
            _rawinput = new RawInput(Handle, true);
            _rawinput.KeyPressed += OnKeyPressed;
            _exp_id = exp_id;

            _db = new BaseEntities();
        }

        private void FluentDesignForm1_Load(object sender, EventArgs e)
        {
            using (var s = new UserSettingsRepository())
            {

            }

            CarsLookUpEdit.Properties.DataSource = _db.Cars.ToList();
            DriverEdit.Properties.DataSource = _db.Kagent.Where(w => w.JobType == 3).Select(s => new Kontragent { KaId = s.KaId, Name = s.Name }).ToList();
            RouteLookUpEdit.Properties.DataSource = DB.SkladBase().Routes.AsNoTracking().ToList();

            if (_exp_id == null)
            {
                is_new_record = true;

                exp = _db.Expedition.Add(new Expedition
                {
                    Id = Guid.NewGuid(),
                    DocType = 32,
                    Checked = 1,
                    OnDate = DBHelper.ServerDateTime(),
                    PersonId = DBHelper.CurrentUser.KaId,
                    Num = new BaseEntities().GetDocNum("expedition").FirstOrDefault()
                 });

                _db.SaveChanges();
                _exp_id = exp.Id;
            }
            else
            {
                exp = _db.Expedition.Find(_exp_id);
            }

            if (exp != null)
            {
                exp.Checked = 0;

                ExpeditionBS.DataSource = exp;
            }
          

            GetDetail();
        }

        private void GetDetail()
        {
            int top_row = ExpeditionDetGridView.TopRowIndex;
            ExpeditionDetListBS.DataSource = _db.v_ExpeditionDet.AsNoTracking().Where(w => w.ExpeditionId == _exp_id).OrderBy(o => o.CreatedAt).ToList();
            ExpeditionDetGridView.TopRowIndex = top_row;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (focused_row == null)
            {
                return;
            }
            if (XtraMessageBox.Show(@"Ви дійсно хочете видалити документ?", @"Видалення запису", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _db.ExpeditionDet.Remove(_db.ExpeditionDet.FirstOrDefault(w => w.Id == focused_row.Id));
                _db.SaveChanges();
                GetDetail();
            }
        }

        private void OnKeyPressed(object sender, RawInputEventArg e)
        {
            if (e.KeyPressEvent.DeviceName == Settings.Default.barcode_scanner_name && e.KeyPressEvent.Message == Win32.WM_KEYDOWN)
            {
                BarCodeBox.Focus();
            }
        }

        private class WbIfo
        {
            public Guid? CarId { get; set; }
            public List<Measures> Measures { get; set; }
            public decimal WbAmount { get; set; }
            public decimal TareWeight { get; set; }
            public int? DriverId { get; set; }
            public int? RouteId { get; set; }
            public Guid Id { get; set; }
        }

        private WbIfo GetWbInfo(int wbill_id)
        {
            var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wbill_id && w.WType == -1 );
            if (wb != null)
            {
                var msr_list = _db.WaybillDet.Where(w => w.WbillId == wbill_id).Select(s => s.Materials.Measures).Distinct().ToList();
                var mid = msr_list.FirstOrDefault()?.MId;

                return new WbIfo()
                {
                    CarId = wb.CarId,
                    DriverId = wb.DriverId,
                    RouteId = wb.RouteId,
                    Measures = msr_list,
                    WbAmount = _db.WaybillDet.Where(w => w.WbillId == wbill_id && w.Materials.MId == mid).Select(s => s.Amount).ToList().Sum(),
                    TareWeight = _db.WayBillTmc.Where(w => w.WbillId == wbill_id).Select(s => s.Amount * s.Materials.Weight ?? 0).ToList().Sum(),
                    Id = wb.Id
                };
            }
            else
            {
                return null;
            }
        }

        private void Add(int wbill_id)
        {
            if (CarsLookUpEdit.EditValue == DBNull.Value || DriverEdit.EditValue == DBNull.Value)
            {
                MessageBox.Show("Виберіть машину та водія!");
                return;
            }

            _db.SaveChanges();

            if (_db.ExpeditionDet.Any(a => a.WbillId == wbill_id && a.Checked == 1))
            {
                MessageBox.Show("Документ вже добавлено в одну з експедицій!");
                return ;
            }

            var wb_info = GetWbInfo(wbill_id);
            if (wb_info != null)
            {
                using (var nf = new frmNumKeyboard())
                {
                    nf.Text = "К-сть товару з тарою";
                    if (nf.ShowDialog() == DialogResult.OK)
                    {
                   //     exp.CarId = wb_info.CarId.HasValue ? wb_info.CarId.Value : exp.CarId;
                    //    CarsLookUpEdit.EditValue = exp.CarId;

                   //     exp.DriverId = wb_info.DriverId.HasValue ? wb_info.DriverId.Value : exp.DriverId;
                 //       DriverEdit.EditValue = exp.DriverId;

                        exp.RouteId = wb_info.RouteId.HasValue ? wb_info.RouteId : exp.RouteId;

                        var mid = wb_info.Measures.FirstOrDefault()?.MId;

                        var verified_weight_without_tare = mid == 2 ? nf.numKeyboardUserControl2.Value - wb_info.TareWeight : nf.numKeyboardUserControl2.Value;
                        var new_id = Guid.NewGuid();

                        var new_det = _db.ExpeditionDet.Add(new ExpeditionDet
                        {
                            Id = new_id,
                            Amount = nf.numKeyboardUserControl2.Value,
                            ExpeditionId = exp.Id,
                            CreatedAt = DBHelper.ServerDateTime(),
                            WbillId = wbill_id,
                            MId = mid,
                            Checked = 1,
                            WbAmount = wb_info.WbAmount,
                            TareWeight = wb_info.TareWeight,
                            TareQuantity = _db.WayBillTmc.Where(w => w.WbillId == wbill_id).Select(s => s.Amount).ToList().Sum(),
                            TotalWeight = verified_weight_without_tare - wb_info.WbAmount
                        });
                        _db.SetDocRel(exp.Id, wb_info.Id);

                        _db.SaveChanges();

                        GetDetail();

                        int rowHandle = ExpeditionDetGridView.LocateByValue("Id", new_id);
                        if (rowHandle != GridControl.InvalidRowHandle)
                        {
                            ExpeditionDetGridView.FocusedRowHandle = rowHandle;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Документ не знайдено!");
            }
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(BarCodeBox.Text))
            {
                if (int.TryParse(BarCodeBox.Text, out int wbill_id))
                {
                    Add(wbill_id);
                }

                BarCodeBox.Text = "";
                labelControl1.Focus();
            }
        }

        private void frmExpeditionInterface_FormClosed(object sender, FormClosedEventArgs e)
        {
            /*  if (is_new_record)
              {
                  _db.DeleteWhere<Expedition>(w => w.Id == _exp_id);
              }*/

            exp.UpdatedAt = DateTime.Now;
            exp.UpdatedBy = DBHelper.CurrentUser.UserId;

            _db.SaveChanges();

            _db.Dispose();
        }

        private void ExpeditionDetGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            if (focused_row == null)
            {
            //    det = null;
             //   ExpeditionDetBS.DataSource = new ExpeditionDet();
                return;
            }

            PacksQuantityEdit.EditValue = focused_row.PacksQuantity;
            KilogramsQuantityEdit.EditValue = focused_row.KilogramsQuantity;

            var msr_list = _db.WaybillDet.Where(w => w.WbillId == focused_row.WbillId).Select(s => s.Materials.Measures).Distinct().ToList();
            layoutControlItem3.Text = "Товарів по документу, " + msr_list.FirstOrDefault(w=> w.MId == focused_row.MId)?.ShortName;

            MsrComboBox.Properties.DataSource = msr_list;

            ExpeditionDetBS.DataSource = _db.ExpeditionDet.Where(w => w.Id == focused_row.Id).FirstOrDefault(); 
        }

        private void GetTotalWeight()
        {
            if (focused_row == null)
            {
                return;
            }

            var det = _db.ExpeditionDet.Where(w => w.Id == focused_row.Id).FirstOrDefault();
            if (det == null)
            {
                return;
            }

            var msr = MsrComboBox.GetSelectedDataRow() as Measures;
            det.Amount = AmountEdit.Value;
            det.TareWeight = TareWeightEdit.Value;

            var verified_weight_without_tare = msr?.MId == 2 ? AmountEdit.Value - TareWeightEdit.Value : AmountEdit.Value;

            det.TotalWeight = verified_weight_without_tare - det.WbAmount??0;
            TotalDoc.EditValue = det.TotalWeight;

            _db.SaveChanges();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            exp.Checked = 1;
            foreach (var item in _db.v_ExpeditionDet.Where(w => w.ExpeditionId == exp.Id))
            {
                if (item.RouteId.HasValue && item.Checked == 1)
                {
                    var exp_wb = _db.WaybillList.Find(item.WbillId);
                    exp_wb.ShipmentDate = exp.OnDate.AddTicks(item.RouteDuration ?? 0);
                }
            }

            simpleButton2.PerformClick();
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (AmountEdit.ContainsFocus)
            {
                GetTotalWeight();
                GetDetail();
            }
        }

        private void MsrComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if(!MsrComboBox.ContainsFocus)
            {
                return;
            }

            var r = MsrComboBox.GetSelectedDataRow() as Measures;
            if (r == null)
            {
                return;
            }
            var det = _db.ExpeditionDet.Where(w => w.Id == focused_row.Id).FirstOrDefault();

            det.MId = r.MId;

            det.WbAmount = _db.WaybillDet.Where(w => w.WbillId == det.WbillId && w.Materials.MId == r.MId).Select(s => s.Amount).ToList().Sum();
            textEdit1.EditValue = det.WbAmount;

            det.TareWeight = _db.WayBillTmc.Where(w => w.WbillId == det.WbillId).Select(s => s.Amount * s.Materials.Weight ?? 0).ToList().Sum();
            TareWeightEdit.EditValue = det.TareWeight;

            det.TareQuantity = _db.WayBillTmc.Where(w => w.WbillId == det.WbillId).Select(s => s.Amount).ToList().Sum();
            TareQuantityEdit.EditValue = det.TareQuantity;

            _db.SaveChanges();

            GetTotalWeight();
        }

        private void TareWeightEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (focused_row == null)
            {
                return;
            }

            if (TareWeightEdit.ContainsFocus)
            {
                GetTotalWeight();
                GetDetail();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            using (var frm = new frmBarCode())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(frm.BarCodeEdit.Text))
                    {
                        if (int.TryParse(frm.BarCodeEdit.Text, out int wbill_id))
                        {
                            Add(wbill_id);
                        }
                        else
                        {
                            MessageBox.Show("Не вірний штрих код!");
                        }
                    }
                }
            }
        }

        private void ExpeditionDetGridView_DoubleClick(object sender, EventArgs e)
        {
            if(focused_row == null)
            {
                return;
            }

            var det = _db.ExpeditionDet.Where(w => w.Id == focused_row.Id).FirstOrDefault();
            if (det != null)
            {
                using (var frm = new frmWBMaterialsDet(det))
                {
                    frm.ShowDialog();
                }
            }
        }

        private void AmountEdit_Properties_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            AmountEdit.Focus();
        }

        private void TareWeightEdit_Properties_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            TareWeightEdit.Focus();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            is_new_record = false;

            Close();
        }

        private void checkEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if(focused_row == null)
            {
                return;
            }

            _db.SaveChanges();

            focused_row.Checked = Convert.ToInt32( checkEdit1.Checked);

            ExpeditionDetGridView.RefreshData();

        }

        private void NumPadBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void AmountEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1 )
            {
                if (focused_row == null)
                {
                    return;
                }

                using (var nf = new frmNumKeyboard())
                {
                    nf.Text = "К-сть товару з тарою";
                    if (nf.ShowDialog() == DialogResult.OK)
                    {
                        AmountEdit.Value = nf.numKeyboardUserControl2.Value;

                        GetTotalWeight();
                        GetDetail();
                    }
                }
            }
        }

        private void RouteLookUpEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
    }
}
