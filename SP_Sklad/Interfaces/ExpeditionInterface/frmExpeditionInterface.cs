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
            ExpeditionDetBS.DataSource = _db.v_ExpeditionDet.AsNoTracking().Where(w => w.ExpeditionId == _exp_id).OrderBy(o => o.CreatedAt).ToList();
            ExpeditionDetGridView.TopRowIndex = top_row;
        }

        private void tileView1_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {

            /*    if (focused_row.IntermediateWeighingDetId != null)
                {
                    using (var _db = new BaseEntities())
                    {
                        det = _db.IntermediateWeighingDet.Find(focused_row.IntermediateWeighingDetId);
                    }
                }
                else
                {
                    det = new IntermediateWeighingDet
                    {
                        Id = Guid.NewGuid(),
                        Amount = 0,
                        IntermediateWeighingId = focused_row.IntermediateWeighingId,
                        CreatedDate = DBHelper.ServerDateTime(),
                        TaraAmount = 0,
                        MatId = focused_row.MatId

                    };
                }

                RawMaterialManagementDetBS.DataSource = det;*/

            GetOk();
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

        private void TaraCalcEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }


        Color colorPanelReady = Color.MistyRose;//Color.FromArgb(58, 166, 101);
        Color colorPanelSold = Color.FromArgb(158, 158, 158);
        Color colorCaptionReady = Color.Black;//Color.FromArgb(193, 222, 204);
        Color colorCaptionSold = Color.FromArgb(219, 219, 219);

        private void tileView1_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
        {
            /*      if (e.Item == null || e.Item.Elements.Count == 0)
                      return;
                  var is_done = !string.IsNullOrEmpty((string)tileView1.GetRowCellValue(e.RowHandle, tileView1.Columns["IntermediateWeighingDetTotal"]));

                  var RecipeCaption = e.Item.GetElementByName("RecipeCaption");
                  var WBDateCaption = e.Item.GetElementByName("WBDateCaption");
                  //     var price = e.Item.GetElementByName("Price");

                  e.Item.AppearanceItem.Normal.BackColor = is_done ? colorPanelSold : colorPanelReady;

                  RecipeCaption.Appearance.Normal.ForeColor = is_done ? colorCaptionSold : colorCaptionReady;
                  WBDateCaption.Appearance.Normal.ForeColor = is_done ? colorCaptionSold : colorCaptionReady;
                  //   if (sold) price.Text = "Sold";*/
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
            /*  if(focused_row == null)
              {
                  return;
              }

              using (var _db = new BaseEntities())
              {
                  var det = _db.RawMaterialManagementDet.Find(focused_row.Id);

                  using (var frm2 = new frmWeightEdit(det.Materials.Name))
                  {
                      if (frm2.ShowDialog() == DialogResult.OK)
                      {
                          det.Amount = frm2.AmountEdit.Value;

                          _db.SaveChanges();
                      }
                  }
              }

              GetDetail(rmm.Id);*/
        }

        private void OnKeyPressed(object sender, RawInputEventArg e)
        {
            if (e.KeyPressEvent.DeviceName == Settings.Default.barcode_scanner_name && e.KeyPressEvent.Message == Win32.WM_KEYDOWN)
            {
                BarCodeBox.Focus();
            }
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(BarCodeBox.Text))
            {
                using (var _db = new BaseEntities())
                {
                    var wbill_id = Convert.ToInt32(BarCodeBox.Text);
                    if (_db.WaybillList.Any(w => w.WbillId == wbill_id && w.WType == -1))
                    {
                        var msr_list = _db.WaybillDet.Where(w => w.WbillId == wbill_id).Select(s => s.Materials.Measures).Distinct().ToList();

                        var det = _db.ExpeditionDet.Add(new ExpeditionDet
                        {
                            Id = Guid.NewGuid(),
                            Amount = 0,
                            ExpeditionId = exp.Id,
                            CreatedAt = DBHelper.ServerDateTime(),
                            WbillId = wbill_id,
                            MId = msr_list.FirstOrDefault()?.MId
                        });

                        _db.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Документ не знайдено!");
                    }

                }
            }

            BarCodeBox.Text = "";

            labelControl1.Focus();

            GetDetail();

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
    }
}
