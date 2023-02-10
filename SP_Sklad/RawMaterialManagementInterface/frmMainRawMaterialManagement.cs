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

namespace SP_Sklad.RawMaterialManagementInterface
{
    public partial class frmMainRawMaterialManagement : DevExpress.XtraEditors.XtraForm
    {
        private int _user_id { get; set; }
        public static frmMainRawMaterialManagement main_form { get; set; }

        public BaseEntities _db { get; set; }

        private v_RawMaterialManagement rmm_focused_row => tileView1.GetFocusedRow() is NotLoadedObject ? null : tileView1.GetFocusedRow() as v_RawMaterialManagement;

        public frmMainRawMaterialManagement()
            :this (UserSession.UserId)
        {
        }

        public frmMainRawMaterialManagement(int user_id)
        {
            InitializeComponent();
            _user_id = user_id;
            _db = new BaseEntities();
        }
  
        private void Form1_Load(object sender, EventArgs e)
        {
            WhComboBoxEdit.Properties.DataSource = DBHelper.WhList;
            WhComboBoxEdit.EditValue = DBHelper.WhList.FirstOrDefault().WId;

            //   gridControl1.DataSource = _db.v_RawMaterialManagement.Where(w => w.Checked == 0).ToList();
        }

        private void RgViewType_SelectedIndexChanged(object sender, EventArgs e)
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
            var rmm = _db.RawMaterialManagement.Add(new RawMaterialManagement
            {
                Id = Guid.NewGuid(),
                OnDate = DBHelper.ServerDateTime(),
                Num = new BaseEntities().GetDocNum("raw_material_management").FirstOrDefault(),
                PersonId = DBHelper.CurrentUser.KaId.Value,
                UpdatedBy = DBHelper.CurrentUser.UserId,
                Checked = 0,
                DocType = (int)rgViewType.EditValue,
                WId = (int)WhComboBoxEdit.EditValue
            });

            _db.SaveChanges();
            
            using (var frm = new frmRawMatDet(rmm.Id))
            {
                frm.labelControl1.Text = rgViewType.Properties.Items[rgViewType.SelectedIndex].Description + " №"+rmm.Num;
                frm.ShowDialog();
            }

            RawMaterialManagementSource.Refresh();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void IsDoneToggleSwitch_EditValueChanged(object sender, EventArgs e)
        {
            RawMaterialManagementSource.Refresh();
        }

        private void RawMaterialManagementSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var _db = DB.SkladBase();

            var rmm = _db.v_RawMaterialManagement.Where(w => w.Checked == 0);

            e.QueryableSource = rmm;

            e.Tag = _db;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if(rmm_focused_row == null)
            {
                return;
            }

            using (var frm = new frmRawMatDet(rmm_focused_row.Id))
            {
                frm.labelControl1.Text = rmm_focused_row.DocTypeName + " №" + rmm_focused_row.Num;
                frm.ShowDialog();
            }

            RawMaterialManagementSource.Refresh();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            RawMaterialManagementSource.Refresh();
        }
    }
}
