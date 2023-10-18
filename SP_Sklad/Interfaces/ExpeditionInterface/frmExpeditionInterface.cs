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
        private ExpeditionDet det { get; set; }

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
                    Num = new BaseEntities().GetDocNum("expedition").FirstOrDefault(),
                    CarId = _db.Cars.FirstOrDefault().Id
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

  
        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void GetOk()
        {
            /*    var row = focused_row;

                if (row != null)
                {
                    using (var _db = new BaseEntities())
                    {
                        var plan_weighing = Math.Round(Convert.ToDecimal((row.AmountByRecipe * row.IntermediateWeighingAmount) / row.TotalWeightByRecipe), 3);

                        //    ByRecipeEdit.EditValue = row.AmountByRecipe;
                        IntermediateWeighingEdit.EditValue = row.TotalWeighted;
                        TotalEdit.EditValue = row.AmountByRecipe - (row.TotalWeighted ?? 0);

                        var deviation = _db.MatRecDet.FirstOrDefault(w => w.MatId == row.MatId && w.RecId == row.RecId)?.Deviation ?? 1000000;

                        var netto_amount = AmountEdit.Value - TaraCalcEdit.Value;

                        CalcAmount.EditValue = plan_weighing + TaraCalcEdit.Value;

                        OkButton.Enabled = ((plan_weighing + deviation) >= netto_amount && (plan_weighing - deviation) <= netto_amount) && AmountEdit.Value > 0;
                    }

                }
                else
                {
                    OkButton.Enabled = false;
                }*/
        }

             

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            exp.UpdatedAt = DateTime.Now;
            exp.UpdatedBy = DBHelper.CurrentUser.UserId;

            _db.SaveChanges();

            is_new_record = false;

            Close();
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

        private void Add(int wbill_id)
        {
            using (var _db = new BaseEntities())
            {
                var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wbill_id && w.WType == -1);
                if (wb != null)
                {
                    exp.CarId = wb.CarId.HasValue ? wb.CarId.Value : exp.CarId;

                    var msr_list = _db.WaybillDet.Where(w => w.WbillId == wbill_id).Select(s => s.Materials.Measures).Distinct().ToList();
                    var new_id = Guid.NewGuid();

                    det = _db.ExpeditionDet.Add(new ExpeditionDet
                    {
                        Id = new_id,
                        Amount = 0,
                        ExpeditionId = exp.Id,
                        CreatedAt = DBHelper.ServerDateTime(),
                        WbillId = wbill_id,
                        MId = msr_list.FirstOrDefault()?.MId,
                        Checked = 0
                    });


                    det.WbAmount = _db.WaybillDet.Where(w => w.WbillId == det.WbillId && w.Materials.MId == det.MId).Select(s => s.Amount).ToList().Sum();
                    textEdit1.EditValue = det.WbAmount;

                    TareWeightEdit.EditValue = _db.WayBillTmc.Where(w => w.WbillId == det.WbillId).Select(s => s.Amount * s.Materials.Weight ?? 0).ToList().Sum();

                    det.TareQuantity = _db.WayBillTmc.Where(w => w.WbillId == det.WbillId).Select(s => s.Amount).ToList().Sum();
                    calcEdit1.EditValue = det.TareQuantity;

                    _db.SaveChanges();


                    PacksQuantityEdit.EditValue = _db.v_ExpeditionWBMaterialsDet.Where(w => w.Id == det.Id && w.MId == det.MId).Sum(ss => ss.PacksQuantity);
                    KilogramsQuantityEdit.EditValue = _db.v_ExpeditionWBMaterialsDet.Where(w => w.Id == det.Id && w.MId == det.MId).Sum(ss => ss.KilogramsQuantity);

                    layoutControlItem3.Text = "Товарів по документу, " + MsrComboBox.Text;

                    GetDetail();

                    int rowHandle = ExpeditionDetGridView.LocateByValue("Id", new_id);
                    if (rowHandle != GridControl.InvalidRowHandle)
                    {
                        ExpeditionDetGridView.FocusedRowHandle = rowHandle;
                    }

                    using (var nf = new frmNumKeyboard())
                    {
                        nf.Text = "К-сть товару з тарою";
                        if (nf.ShowDialog() == DialogResult.OK)
                        {
                            AmountEdit.Value = nf.numKeyboardUserControl2.Value;

                            SaveDetBtn.PerformClick();
                        }
                    }
               /*     using (var frm = new frmNumericKeypad())
                    {
                        frm.Text = "К-сть товару з тарою";
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            AmountEdit.Value = Convert.ToDecimal(frm.AmountEdit.Text);

                            SaveDetBtn.PerformClick();
                        }
                    }*/


                }
                else
                {
                    MessageBox.Show("Документ не знайдено!");
                }
            }
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(BarCodeBox.Text))
            {
                var wbill_id = Convert.ToInt32(BarCodeBox.Text);

                Add(wbill_id);

                BarCodeBox.Text = "";
                labelControl1.Focus();
              
            }
        }

        private void CarsLookUpEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void frmExpeditionInterface_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (is_new_record)
            {
                _db.DeleteWhere<Expedition>(w => w.Id == _exp_id);
            }

            _db.Dispose();
        }

        private void ExpeditionDetGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            if(focused_row == null)
            {
                det = null;
                ExpeditionDetBS.DataSource = new ExpeditionDet();
                return;
            }

            det = _db.ExpeditionDet.Where(w => w.Id == focused_row.Id).FirstOrDefault();
          

            var msr_list = _db.WaybillDet.Where(w => w.WbillId == det.WbillId).Select(s => s.Materials.Measures).Distinct().ToList();

            MsrComboBox.Properties.DataSource = msr_list;

            ExpeditionDetBS.DataSource = det;
        }

        private void GetTotalWeight()
        {
            if(det == null)
            {
                return;
            }

            var msr = MsrComboBox.GetSelectedDataRow() as Measures;

            var verified_weight_without_tare = msr?.MId == 2 ? AmountEdit.Value - TareWeightEdit.Value : AmountEdit.Value;

            det.TotalWeight = verified_weight_without_tare - textEdit1.Value;
            TotalDoc.EditValue = det.TotalWeight;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
            GetDetail();

        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetTotalWeight();
        }

        private void MsrComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var r = MsrComboBox.GetSelectedDataRow() as Measures;
            if (r == null)
            {
                return;
            }
            det.MId = r.MId;

            det.WbAmount = _db.WaybillDet.Where(w => w.WbillId == det.WbillId && w.Materials.MId == r.MId).Select(s => s.Amount).ToList().Sum();
            textEdit1.EditValue = det.WbAmount;

            TareWeightEdit.EditValue = _db.WayBillTmc.Where(w => w.WbillId == det.WbillId).Select(s => s.Amount * s.Materials.Weight ?? 0).ToList().Sum();

            det.TareQuantity = _db.WayBillTmc.Where(w => w.WbillId == det.WbillId).Select(s => s.Amount).ToList().Sum();
            calcEdit1.EditValue = det.TareQuantity;

            _db.SaveChanges();

            PacksQuantityEdit.EditValue = _db.v_ExpeditionWBMaterialsDet.Where(w => w.Id == det.Id && w.MId == det.MId).Sum(ss => ss.PacksQuantity);
            KilogramsQuantityEdit.EditValue = _db.v_ExpeditionWBMaterialsDet.Where(w => w.Id == det.Id && w.MId == det.MId).Sum(ss => ss.KilogramsQuantity);

            layoutControlItem3.Text = "Товарів по документу, "+ MsrComboBox.Text;


            GetTotalWeight();
        }

        private void TareWeightEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetTotalWeight();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            using (var frm = new frmBarCode())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(frm.BarCodeEdit.Text))
                    {
                        var wbill_id = Convert.ToInt32(frm.BarCodeEdit.Text);
                        Add(wbill_id);
                    }
                }
            }
        }
    }
}
