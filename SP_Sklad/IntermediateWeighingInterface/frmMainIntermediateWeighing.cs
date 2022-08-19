using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.TableLayout;
using DevExpress.XtraGrid.Views.Tile;
using SP_Sklad.IntermediateWeighingInterface.Views;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;

namespace SP_Sklad.IntermediateWeighingInterface
{
    public partial class frmMainIntermediateWeighing : DevExpress.XtraEditors.XtraForm
    {
        public static int _user_id { get; set; }
        public static frmMainIntermediateWeighing main_form { get; set; }

        public BaseEntities _db { get; set; }

        private IntermediateWeighingView intermediate_weighing_focused_row => tileView1.GetFocusedRow() as IntermediateWeighingView;

        public frmMainIntermediateWeighing(int user_id)
        {
            InitializeComponent();
            _user_id = user_id;
            _db = new BaseEntities();
            GetIntermediateWeighing();
        }
  
        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //GetIntermediateWeighing();
        }

        private void RgViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            tileView1.OptionsTiles.Orientation = (Orientation)rgViewType.SelectedIndex;
        }
        private void ToggleSwitch1_EditValueChanged(object sender, EventArgs e)
        {
            if (object.Equals(toggleSwitch1.EditValue, true))
                tileView1.ColumnSet.GroupColumn = tileViewColumn2;
            else
                tileView1.ColumnSet.GroupColumn = null;
        }
        void repositoryItemZoomTrackBar1_EditValueChanged(object sender, EventArgs e)
        {
            int h = (int)(sender as BaseEdit).EditValue;
            int w = (int)(h * 1.78);
            tileView1.OptionsTiles.ItemSize = new Size(w, h);
        }


        void GetIntermediateWeighing()
        {

            //int top_row = tileView1.ind TopRowIndex;
            //    var satrt_date = IntermediateWeighingStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : IntermediateWeighingStartDate.DateTime;
            //    var end_date = IntermediateWeighingEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : IntermediateWeighingEndDate.DateTime;
            var satrt_date = DateTime.Now.AddDays(-10);

            /*     gridControl1.DataSource = _db.v_IntermediateWeighing.Where(w => w.OnDate > satrt_date && w.Checked == 0)
                       .GroupBy(g => new { g.WbillId, g.WbNum, g.RecipeName, g.WbOnDate, g.RecipeCount, g.MsrName, g.MatId })
                       .Select(s => new IntermediateWeighingView
                       {
                           WbillId = s.Key.WbillId,
                           WbNum = s.Key.WbNum,
                           WbOnDate = s.Key.WbOnDate,
                           RecipeName = s.Key.RecipeName,
                           RecipeCount = s.Key.RecipeCount,
                           Amount = SqlFunctions.StringConvert( s.Sum(su => su.Amount)) + s.Key.MsrName,
                           BMP = _db.Materials.FirstOrDefault(f=> f.MatId ==s.Key.MatId).BMP,
                           IsDone = !_db.v_IntermediateWeighingSummary.Where(w => w.WbillId == s.Key.WbillId && w.IntermediateWeighingDetId == null && w.UserId == _user_id).Select(ss => ss.WbillId).Any()
                       })
                       .OrderBy(o => o.WbOnDate).ToList();*/

            gridControl1.DataSource = _db.v_IntermediateWeighingSummary.Where(w => w.OnDate > satrt_date && w.Checked == 0 && w.UserId == _user_id )
                .GroupBy(g => new
                {
                    g.WbillId,
                    g.WbNum,
                    g.RecipeName,
                    g.WbOnDate,
                    g.RecipeCount,
                    g.ReceipeMsrName,
                    g.RecipeMatId
                })
            .Select(s => new IntermediateWeighingView
            {
                WbillId = s.Key.WbillId,
                WbNum = s.Key.WbNum,
                WbOnDate = s.Key.WbOnDate,
                RecipeName = s.Key.RecipeName,
                RecipeCount = s.Key.RecipeCount,
                Amount = SqlFunctions.StringConvert(s.Sum(su => su.IntermediateWeighingAmount)) + s.Key.ReceipeMsrName,
                BMP = _db.Materials.Where(ww=> ww.MatId == s.Key.RecipeMatId).Select(s2=> s2.BMP).FirstOrDefault(),
                IsDone = !s.Any(w => w.IntermediateWeighingDetId == null)
            }).OrderBy(o => o.WbOnDate).ToList();



            //       IntermediateWeighingGridView.TopRowIndex = top_row;
        }


        private void IntermediateWeighingGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
                var total = _db.IntermediateWeighing.Where(w => w.WbillId == intermediate_weighing_focused_row.WbillId).Sum(s => s.Amount);
                var amount_by_recipe = _db.WayBillMake.FirstOrDefault(w => w.WbillId == intermediate_weighing_focused_row.WbillId).AmountByRecipe;

                if ((total ?? 0) > (amount_by_recipe ?? 0))
                {
                    MessageBox.Show("Вага закладок по виробництву більша запланованої");

                    return;
                }

                var iw = _db.IntermediateWeighing.Find(intermediate_weighing_focused_row.Id);
                using (var f = new WBDetForm.frmIntermediateWeighingDet(_db, Guid.NewGuid(), iw))
                {
                    f.ShowDialog();
                }

         //   RefreshDet();
        }

        private void frmMainIntermediateWeighing_FormClosed(object sender, FormClosedEventArgs e)
        {
        /*    docsUserControl1.SaveGridLayouts();
            whUserControl.SaveGridLayouts();
            manufacturingUserControl1.SaveGridLayouts();
            tradeUserControl1.SaveGridLayouts();*/

            using (var db = new BaseEntities())
            {
                var user = db.Users.Find(_user_id);
                if (user != null)
                {
                    user.IsOnline = false;
                }

                db.SaveChanges();
            }

            Application.Exit();
        }

        Color colorPanelReady = Color.FromArgb(58, 166, 101);
        Color colorPanelSold = Color.FromArgb(158, 158, 158);
        Color colorCaptionReady = Color.FromArgb(193, 222, 204);
        Color colorCaptionSold = Color.FromArgb(219, 219, 219);

        private void tileView1_ItemCustomize(object sender, TileViewItemCustomizeEventArgs e)
        {
            if (e.Item == null || e.Item.Elements.Count == 0)
                return;
            bool sold =  (bool)tileView1.GetRowCellValue(e.RowHandle, tileView1.Columns["IsDone"]) ;

            var RecipeCaption = e.Item.GetElementByName("RecipeCaption");
            var WBDateCaption = e.Item.GetElementByName("WBDateCaption");
       //     var price = e.Item.GetElementByName("Price");

            e.Item.AppearanceItem.Normal.BackColor = sold ? colorPanelSold : colorPanelReady;

            RecipeCaption.Appearance.Normal.ForeColor = sold ? colorCaptionSold : colorCaptionReady;
            WBDateCaption.Appearance.Normal.ForeColor = sold ? colorCaptionSold : colorCaptionReady;
         //   if (sold) price.Text = "Sold";
        }

        private void tileView1_ItemClick(object sender, TileViewItemClickEventArgs e)
        {
            using (var frm = new FluentDesignForm1(intermediate_weighing_focused_row.WbillId))
            {
                frm.Text = "Список сировини для зважування, Рецепт: " + intermediate_weighing_focused_row.RecipeName;
                frm.ShowDialog();

                GetIntermediateWeighing();
            }


                
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GetIntermediateWeighing();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
