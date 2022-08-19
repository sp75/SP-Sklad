using DevExpress.XtraBars;
using SP_Sklad.EditForm;
using SP_Sklad.IntermediateWeighingInterface.Views;
using SP_Sklad.SkladData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.IntermediateWeighingInterface
{
    public partial class FluentDesignForm1 : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public BaseEntities _db { get; set; }
        private IntermediateWeighingDet det { get; set; }
        private make_det focused_row => tileView1.GetFocusedRow() as make_det;
        private int _wbill_id { get; set; }

        public FluentDesignForm1(int wbill_id)
        {
            InitializeComponent();
            _wbill_id = wbill_id;

            _db = new BaseEntities();

            GetDetail(wbill_id);
        }


        private void FluentDesignForm1_Load(object sender, EventArgs e)
        {
            sidePanel1.Hide();
        }

        private List<make_det> GetDetail(int wbill_id)
        {
            /*       var group_list = _db.UserAccessMatGroup.Where(w => w.UserId == frmMainIntermediateWeighing._user_id).Select(s => s.GrpId).ToList();
                   var wbm = _db.WayBillMake.FirstOrDefault(w => w.WbillId == wbill_id);
                   var intermediate_weighing = _db.IntermediateWeighing.Where(w => w.WbillId == wbill_id).OrderBy(o=> o.OnDate).ToList();
                   var intermediate_det_list = _db.v_IntermediateWeighingDet.Where(w => w.WbillId == wbill_id).ToList();

                   var wb_make_det = _db.GetWayBillMakeDet(wbill_id).Where(w => group_list.Contains(w.GrpId.Value) && w.Rsv == 0).OrderBy(o => o.Num).ToList();

                   var result = new List<make_det>();
                   int rn = 0;
                   foreach (var item in intermediate_weighing)
                   {
                       ++rn;
                       foreach (var wb_make_item in wb_make_det)
                       {
                           var intermediate_det_item = intermediate_det_list.FirstOrDefault(w => w.MatId == wb_make_item.MatId && w.IntermediateWeighingId == item.Id);

                           result.Add(new make_det
                           {
                               IntermediateWeighingId = item.Id,
                               IntermediateWeighingNum = item.Num,
                               IntermediateWeighingAmount = item.Amount,
                               MsrName = wb_make_item.MsrName,
                               MatName = wb_make_item.MatName,
                               MatId = wb_make_item.MatId,
                               WbillId = wbill_id,
                               IntermediateWeighingCount = intermediate_det_list.Count(co => co.MatId == wb_make_item.MatId),
                               RecipeCount = intermediate_weighing.Count(),//wbm.RecipeCount,
                               Rn = rn,
                               img = _db.Materials.FirstOrDefault(w=>  w.MatId == wb_make_item.MatId).BMP,
                               AmountByRecipe = wb_make_item.AmountByRecipe,
                               TotalWeightByRecipe = wbm.AmountByRecipe,
                               TotalWeighted = wb_make_item.AmountIntermediateWeighing,
                               IntermediateWeighingDetId = intermediate_det_item?.Id,
                               IntermediateWeighingDetTotal =   intermediate_det_item != null ? string.Format("{0}{1}" , intermediate_det_item.Total.Value.ToString("0.000"), intermediate_det_item?.MsrName) :"",
                               RecId = wbm.RecId
                           });
                       }
                   }

                   if (intermediate_weighing.Count() > 1)
                   {
                       tileView1.ColumnSet.GroupColumn = tileViewColumn1;
                   }*/

            var result = _db.v_IntermediateWeighingSummary.Where(w => w.WbillId == wbill_id && w.Checked == 0 && w.UserId == frmMainIntermediateWeighing._user_id)
                .Select(s => new make_det
                {
                    IntermediateWeighingId = s.IntermediateWeighingId,
                    IntermediateWeighingNum = s.IntermediateWeighingNum,
                    IntermediateWeighingAmount = s.IntermediateWeighingAmount,
                    MsrName = s.MsrName,
                    MatName = s.MatName,
                    MatId = s.MatId,
                    WbillId = wbill_id,
                    //       IntermediateWeighingCount = intermediate_det_list.Count(co => co.MatId == wb_make_item.MatId),
                    RecipeCount = s.RecipeCount, // intermediate_weighing.Count(),
                                                 //   Rn = rn,
                    img = s.BMP,
                    AmountByRecipe = s.AmountByRecipe,
                    TotalWeightByRecipe = s.TotalWeightByRecipe,
                    TotalWeighted = s.AmountIntermediateWeighing,
                    IntermediateWeighingDetId = s.IntermediateWeighingDetId,
                    IntermediateWeighingDetTotal = s.Total != null ? (SqlFunctions.StringConvert(s.Total,10,3) + s.MsrName): "",
                    RecId = s.RecId
                }).ToList();

            bindingSource1.DataSource = result;

            if (result.Select(s => s.IntermediateWeighingId).Distinct().Count() > 1)
            {
                tileView1.ColumnSet.GroupColumn = tileViewColumn1;
            }



            return result;

        }
        private void tileView1_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            sidePanel1.Show();

            layoutControlGroup2.Text = focused_row.MatName;

            if (focused_row.IntermediateWeighingDetId != null)
            {
                det = _db.IntermediateWeighingDet.Find(focused_row.IntermediateWeighingDetId);
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

            IntermediateWeighingDetBS.DataSource = det;

            GetOk();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            det.Total = det.Amount - det.TaraAmount ;
            det.TaraTotal = det.TaraAmount ;


            if (_db.Entry<IntermediateWeighingDet>(det).State == EntityState.Detached)
            {
                _db.IntermediateWeighingDet.Add(det);
            }
            _db.SaveChanges();

            GetDetail(_wbill_id);
        }

        private void GetOk()
        {
            var row = focused_row;

            if (row != null)
            {
                var plan_weighing = Math.Round(Convert.ToDecimal((row.AmountByRecipe * row.IntermediateWeighingAmount) / row.TotalWeightByRecipe), 2);

            //    ByRecipeEdit.EditValue = row.AmountByRecipe;
                IntermediateWeighingEdit.EditValue = row.TotalWeighted;
                TotalEdit.EditValue = row.AmountByRecipe - (row.TotalWeighted ?? 0);

                var deviation = _db.MatRecDet.FirstOrDefault(w => w.MatId == row.MatId && w.RecId == row.RecId)?.Deviation ?? 1000000;

                var netto_amount = AmountEdit.Value - TaraCalcEdit.Value ;

                CalcAmount.EditValue = plan_weighing + TaraCalcEdit.Value ;


                OkButton.Enabled =  ((plan_weighing + deviation) >= netto_amount && (plan_weighing - deviation) <= netto_amount);
                
            }
            else
            {
                OkButton.Enabled = false;
            }
        }

        private void TaraCalcEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void AmountEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                using (var frm = new frmWeightEdit(focused_row.MatName, 1))
                {

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        AmountEdit.EditValue = frm.AmountEdit.Value;

                        GetOk();
                    }
                }
            }

            if (e.Button.Index == 2)
            {
                using (var frm = new frmWeightEdit(focused_row.MatName, 2))
                {

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        AmountEdit.EditValue = frm.AmountEdit.Value;

                        GetOk();
                    }
                }
            }
        }


        Color colorPanelReady = Color.MistyRose;//Color.FromArgb(58, 166, 101);
        Color colorPanelSold = Color.FromArgb(158, 158, 158);
        Color colorCaptionReady = Color.Black;//Color.FromArgb(193, 222, 204);
        Color colorCaptionSold = Color.FromArgb(219, 219, 219);

        private void tileView1_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
        {
            if (e.Item == null || e.Item.Elements.Count == 0)
                return;
            var is_done = !string.IsNullOrEmpty((string)tileView1.GetRowCellValue(e.RowHandle, tileView1.Columns["IntermediateWeighingDetTotal"]));

            var RecipeCaption = e.Item.GetElementByName("RecipeCaption");
            var WBDateCaption = e.Item.GetElementByName("WBDateCaption");
            //     var price = e.Item.GetElementByName("Price");

            e.Item.AppearanceItem.Normal.BackColor = is_done ? colorPanelSold : colorPanelReady;

            RecipeCaption.Appearance.Normal.ForeColor = is_done ? colorCaptionSold : colorCaptionReady;
            WBDateCaption.Appearance.Normal.ForeColor = is_done ? colorCaptionSold : colorCaptionReady;
            //   if (sold) price.Text = "Sold";
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            using (var frm = new frmWeightEdit(focused_row.MatName, 1))
            {

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    AmountEdit.EditValue = frm.AmountEdit.Value;

                    GetOk();
                }
            }
        }
    }
}
