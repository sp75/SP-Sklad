using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using System.Data.Entity;
using SP_Sklad.Common;

namespace SP_Sklad
{
    public partial class mainForm : Form
    {
        public static int user_id { get; set; }
        public static int enterprise_id { get; set; }

        public mainForm(int UserId)
        {
            InitializeComponent();
            user_id = UserId;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            CurDateEditBarItem.EditValue = DateTime.Now;
            repositoryItemLookUpEdit1.DataSource = DBHelper.EnterpriseList;

            if (barEditItem3.EditValue == null || barEditItem3.EditValue == DBNull.Value)
            {
                barEditItem3.EditValue = DBHelper.EnterpriseList.Select(s => s.KaId).FirstOrDefault();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*
            //	MainPageControl->Visible = false ;
            frmMain->WindowState = wsMinimized;
            frmLockSoft->ShowModal();
            //	MainPageControl->Visible = true ;
            frmMain->WindowState = wsMaximized;
             */
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var want_to_exit = MessageBox.Show(@"Ви дійсно хочете вийти з програми?", @"Закрити програму", MessageBoxButtons.YesNo) == DialogResult.Yes;
            e.Cancel = !want_to_exit;
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            using (var db = new BaseEntities())
            {
                var user = db.Users.Find(user_id);
                if (user != null)
                {
                    user.IsOnline = false;
                    db.SaveChanges();
                }
            }

            Application.Exit();
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            enterprise_id = (int)barEditItem3.EditValue;

            GetMainHeder();
        }

        private void barEditItem3_EditValueChanged(object sender, EventArgs e)
        {
            enterprise_id = (int)barEditItem3.EditValue;
            DBHelper.Enterprise = null;
            GetMainHeder();
        }

        private void GetMainHeder()
        {
            Text = "SP-Склад [Користувач: " + DBHelper.CurrentUser.Name + ", Підприємство: " + (DBHelper.Enterprise != null ? DBHelper.Enterprise.Name : "") + "]";
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

    }
}
