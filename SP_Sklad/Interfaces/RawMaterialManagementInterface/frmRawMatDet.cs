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

namespace SP_Sklad.RawMaterialManagementInterface
{
    public partial class frmRawMatDet : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private readonly RawInput _rawinput;

        private RawMaterialManagement rmm { get; set; }
        private v_RawMaterialManagementDet focused_row => tileView1.GetFocusedRow() as v_RawMaterialManagementDet;
        private Guid _id { get; set; }
        private Tara tara { get; set; }

        public frmRawMatDet(Guid id)
        {
            InitializeComponent();
            _rawinput = new RawInput(Handle, true);
            _rawinput.KeyPressed += OnKeyPressed;
            _id = id;
     
            GetDetail(id);
        }
      


        private void FluentDesignForm1_Load(object sender, EventArgs e)
        {
            using (var s = new UserSettingsRepository())
            {
              
            }

            using (var _db = new BaseEntities())
            {
                rmm = _db.RawMaterialManagement.Find(_id);

                tara = _db.Tara.FirstOrDefault(w => w.TypeId == 7);
            }

        }

        private void GetDetail(Guid id)
        {
            using (var _db = new BaseEntities())
            {
                var list = _db.v_RawMaterialManagementDet.Where(w => w.RawMaterialManagementId == id).ToList();
                bindingSource1.DataSource = list;

                IntermediateWeighingEdit.EditValue = list.Sum(s => s.Amount);
            }

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
        Color colorCaptionSold =  Color.FromArgb(219, 219, 219);

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
            using (var _db = new BaseEntities())
            {
                rmm = _db.RawMaterialManagement.Find(_id);
                if (!rmm.RawMaterialManagementDet.Any())
                {
                    _db.DeleteWhere<RawMaterialManagement>(w => w.Id == _id);
                }
                else
                {
                    rmm.UpdatedAt = DateTime.Now;

                    _db.SaveChanges();
                }
            }

            Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if(focused_row == null)
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

            GetDetail(rmm.Id);
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
                    var mat = _db.MatBarCode.FirstOrDefault(w => w.BarCode == BarCodeBox.Text);

                    if (mat == null)
                    {
                        MessageBox.Show("Штрих-код не знайдено!");
                        BarCodeBox.Text = "";
                        return;
                    }

                    var last_doc_type = _db.RawMaterialManagementDet.Where(w => w.BarCode == BarCodeBox.Text ).OrderByDescending(o => o.OnDate).Select(s => new
                    {
                        s.RawMaterialManagement.DocType,
                        s.Amount,
                        s.PosId,
                        s.RawMaterialManagement.WId
                    }).FirstOrDefault();

                    if (rmm.DocType == last_doc_type?.DocType)
                    {
                        MessageBox.Show("Штрих-код вже використовується!");
                        BarCodeBox.Text = "";
                        return;
                    }

                    if (rmm.DocType == -1 && (last_doc_type?.PosId == null || last_doc_type?.WId != rmm.WId))
                    {
                        MessageBox.Show("Штрих-код не використовується!");
                        BarCodeBox.Text = "";
                        return;
                    }

                    using (var frm2 = new frmWeightEdit(mat.Materials.Name))
                    {
                        if (frm2.ShowDialog() == DialogResult.OK)
                        {
                            var tara_weight = tara != null ? tara.Weight ?? 0 : 0;

                            _db.RawMaterialManagementDet.Add(new RawMaterialManagementDet
                            {
                                Id = Guid.NewGuid(),
                                BarCode = mat.BarCode,
                                Amount = frm2.AmountEdit.Value - tara_weight,
                                MatId = mat.MatId,
                                OnDate = DBHelper.ServerDateTime(),
                                RawMaterialManagementId = rmm.Id,
                                LastAmount = rmm.DocType == -1 ? last_doc_type.Amount : null,
                                PosId = rmm.DocType == -1 ? last_doc_type.PosId : null,
                            });

                            _db.SaveChanges();
                        }
                    }
                }

                BarCodeBox.Text = "";

                labelControl1.Focus();

                GetDetail(rmm.Id);
            }
        }
    }
}
