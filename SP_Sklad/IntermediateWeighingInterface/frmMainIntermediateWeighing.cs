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

            //    int top_row = IntermediateWeighingGridView.TopRowIndex;
            //    var satrt_date = IntermediateWeighingStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : IntermediateWeighingStartDate.DateTime;
            //    var end_date = IntermediateWeighingEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : IntermediateWeighingEndDate.DateTime;
            var satrt_date = DateTime.Now.AddDays(-20);

            gridControl1.DataSource = _db.v_IntermediateWeighing.Where(w => w.OnDate > satrt_date/*&& w.OnDate <= end_date*/ && w.Checked == 0).ToList()
                .GroupBy(g => new { g.WbillId, g.WbNum, g.RecipeName, g.WbOnDate, g.RecipeCount, g.Amount, g.BMP })
                .Select(s=> new IntermediateWeighingView
                {
                    WbillId = s.Key.WbillId,
                    WbNum = s.Key.WbNum,
                    WbOnDate = s.Key.WbOnDate,
                    RecipeName = s.Key.RecipeName,
                    RecipeCount = s.Key.RecipeCount,
                    Amount = String.Format("{0}Kg", s.Key.Amount.Value.ToString("0.0")) ,
                    BMP = s.Key.BMP
                })
                .OrderBy(o => o.WbOnDate).ToList();


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
                using (var f = new frmIntermediateWeighingDet(_db, Guid.NewGuid(), iw))
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
            bool sold = false;// (int)tileView1.GetRowCellValue(e.RowHandle, tileView1.Columns["Status"]) == 1;

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
          


            new FluentDesignForm1(intermediate_weighing_focused_row.WbillId).ShowDialog();
        }
    }
}
