using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.TableLayout;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraGrid.Views.Tile.ViewInfo;
using SP_Sklad.Common;
using SP_Sklad.IntermediateWeighingInterface.Views;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;

namespace SP_Sklad.IntermediateWeighingInterface
{
    public partial class frmMainIntermediateWeighing : DevExpress.XtraEditors.XtraForm
    {
        private int _user_id { get; set; }
        public static frmMainIntermediateWeighing main_form { get; set; }

        public BaseEntities _db { get; set; }

        private IntermediateWeighingView intermediate_weighing_focused_row => tileView1.GetFocusedRow() is NotLoadedObject ? null : tileView1.GetFocusedRow() as IntermediateWeighingView;

        public frmMainIntermediateWeighing()
            :this (UserSession.UserId)
        {
        }

        public frmMainIntermediateWeighing(int user_id)
        {
            InitializeComponent();
            _user_id = user_id;
            _db = new BaseEntities();
        }
  
        private void Form1_Load(object sender, EventArgs e)
        {
   
        }

        private void RgViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            tileView1.OptionsTiles.Orientation = (Orientation)rgViewType.SelectedIndex;
        }

        private void ToggleSwitch1_EditValueChanged(object sender, EventArgs e)
        {
            if (object.Equals(toggleSwitch1.EditValue, true))
                tileView1.ColumnSet.GroupColumn =  tileViewColumn2;
            else
                tileView1.ColumnSet.GroupColumn = null;
        }

        void repositoryItemZoomTrackBar1_EditValueChanged(object sender, EventArgs e)
        {
            int h = (int)(sender as BaseEdit).EditValue;
            int w = (int)(h * 2.74);
            tileView1.OptionsTiles.ItemSize = new Size(w, h);
        }

        private void frmMainIntermediateWeighing_FormClosed(object sender, FormClosedEventArgs e)
        {
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
            {
                return;
            }
            var IsDoneValue = tileView1.GetRowCellValue(e.RowHandle, tileView1.Columns["IsDone"]);
            if (IsDoneValue is NotLoadedObject)
            {
                return;
            }

            if(IsDoneValue == null)
            {
                return;
            }

            bool is_done = (bool)IsDoneValue;

            var RecipeCaption = e.Item.GetElementByName("RecipeCaption");
            var WBDateCaption = e.Item.GetElementByName("WBDateCaption");
            var receipeValue = e.Item.GetElementByName("receipeValue");
            //     var price = e.Item.GetElementByName("Price");

            e.Item.AppearanceItem.Normal.BackColor = is_done ? colorPanelSold : colorPanelReady;


            var font_receipeValue = receipeValue.Appearance.Normal.GetFont();
            int h = (int)zoomTrackBarControl1.EditValue;
            if (h > 150 && h < 200)
            {
                receipeValue.Appearance.Normal.Font = new Font(font_receipeValue.FontFamily, 12, font_receipeValue.Style);
            }
            else if (h >= 200 && h < 245)
            {
                receipeValue.Appearance.Normal.Font = new Font(font_receipeValue.FontFamily, 16, font_receipeValue.Style);
            }
            else if (h >= 245)
            {
                receipeValue.Appearance.Normal.Font = new Font(font_receipeValue.FontFamily, 20, font_receipeValue.Style);
            }
            else
            {
                receipeValue.Appearance.Normal.Font = new Font(font_receipeValue.FontFamily, 10, font_receipeValue.Style);
            }

            RecipeCaption.Appearance.Normal.ForeColor = is_done ? colorCaptionSold : colorCaptionReady;
            WBDateCaption.Appearance.Normal.ForeColor = is_done ? colorCaptionSold : colorCaptionReady;
            //   if (sold) price.Text = "Sold";
        }

        private void tileView1_ItemClick(object sender, TileViewItemClickEventArgs e)
        {
            if (intermediate_weighing_focused_row == null)
            {
                return;
            }


            var total = _db.IntermediateWeighing.Where(w => w.WbillId == intermediate_weighing_focused_row.WbillId).Sum(s => s.Amount);
            var amount_by_recipe = _db.WayBillMake.FirstOrDefault(w => w.WbillId == intermediate_weighing_focused_row.WbillId)?.AmountByRecipe;

            if ((total ?? 0) > (amount_by_recipe ?? 0))
            {
                MessageBox.Show("Вага закладок по виробництву більша запланованої");

                return;
            }


            using (var frm = new MainIntermediateWeighingDet(intermediate_weighing_focused_row.WbillId, _user_id))
            {
                frm.Text = "Список сировини для зважування, Рецепт: " + intermediate_weighing_focused_row.RecipeName;
                try
                {
                    if (intermediate_weighing_focused_row.BMP != null)
                    {
                        frm.pictureBox1.Image = (Bitmap)((new ImageConverter()).ConvertFrom(intermediate_weighing_focused_row.BMP));
                    }
                }
                catch { }

                frm.labelControl1.Text = intermediate_weighing_focused_row.RecipeName;
                frm.ShowDialog();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            RecipeListSource.Refresh();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RecipeListSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var satrt_date = DateTime.Now.AddDays(-2);

            e.QueryableSource = _db.v_IntermediateWeighingInterface.Where(w => w.UserId == _user_id && ( IsDoneToggleSwitch.IsOn || (!IsDoneToggleSwitch.IsOn && !w.IsDone.Value) ))
                  .Select(s => new IntermediateWeighingView
                  {
                      WbillId = s.WbillId,
                      WbNum = s.WbNum,
                      WbOnDate = s.WbOnDate,
                      RecipeName = s.RecipeName,
                      RecipeCount = s.RecipeCount,
                      Amount = SqlFunctions.StringConvert(s.Amount) + s.ReceipeMsrName,
                      BMP = s.BMP,
                      IsDone = s.IsDone
                  });

        }

        private void IsDoneToggleSwitch_EditValueChanged(object sender, EventArgs e)
        {
            RecipeListSource.Refresh();
        }
    }
}
