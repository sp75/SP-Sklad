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
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.TableLayout;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraGrid.Views.Tile.ViewInfo;
using SP_Sklad.Common;
using SP_Sklad.IntermediateWeighingInterface.Views;
using SP_Sklad.RawMaterialManagementInterface;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;

namespace SP_Sklad.Interfaces.ExpeditionInterface
{
    public partial class frmMainExpeditionInterface : DevExpress.XtraEditors.XtraForm
    {
        private int _user_id { get; set; }
        public static frmMainExpeditionInterface main_form { get; set; }

        public BaseEntities _db { get; set; }

        private v_Expedition rmm_focused_row => tileView1.GetFocusedRow() is NotLoadedObject ? null : tileView1.GetFocusedRow() as v_Expedition;

        public frmMainExpeditionInterface()
            :this (UserSession.UserId)
        {
        }

        public frmMainExpeditionInterface(int user_id)
        {
            InitializeComponent();
            _user_id = user_id;
            _db = new BaseEntities();
        }
  
        private void Form1_Load(object sender, EventArgs e)
        {

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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            using (var frm = new frmExpeditionInterface())
            {
                frm.ShowDialog();
            }

            ExpeditionSource.Refresh();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void IsDoneToggleSwitch_EditValueChanged(object sender, EventArgs e)
        {
            ExpeditionSource.Refresh();
        }

        private void RawMaterialManagementSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var _db = DB.SkladBase();

            var rmm = _db.v_Expedition;

            e.QueryableSource = rmm;

            e.Tag = _db;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if(rmm_focused_row == null)
            {
                return;
            }

            using (var frm = new frmExpeditionInterface(rmm_focused_row.Id))
            {
               // frm.labelControl1.Text = rmm_focused_row.DocTypeName + " №" + rmm_focused_row.Num;
                frm.ShowDialog();
            }

            ExpeditionSource.Refresh();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            ExpeditionSource.Refresh();
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
 
            var Checked = tileView1.GetRowCellValue(e.RowHandle, tileView1.Columns["Checked"]);

            if (Checked is NotLoadedObject)
            {
                return;
            }

            if (Checked == null)
            {
                return;
            }

   
                var CheckedCaption = e.Item.GetElementByName("CheckedElement");
                if ((int)Checked == 0)
                {
                    CheckedCaption.Text = "Новий";
                }
                else if ((int)Checked == 1)
                {
                    CheckedCaption.Text = "Завершено погрузку";
                 //   CheckedCaption.Appearance.Normal.ForeColor = Color.Green;
                }
         

            e.Item.AppearanceItem.Normal.BackColor = (int)Checked == 1 ? colorPanelSold : colorPanelReady;

            //RecipeCaption.Appearance.Normal.ForeColor = is_done ? colorCaptionSold : colorCaptionReady;
            //WBDateCaption.Appearance.Normal.ForeColor = is_done ? colorCaptionSold : colorCaptionReady;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            PrintDoc.ExpeditionReport(rmm_focused_row.Id, _db, print: true);
        }

        private void frmMainExpeditionInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ApplicationSkinName = UserLookAndFeel.Default.SkinName;
            Properties.Settings.Default.Save();
        }
    }
}
