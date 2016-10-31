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
using SP_Sklad.WBForm;

namespace SP_Sklad
{
    public partial class mainForm : DevExpress.XtraEditors.XtraForm
    {
        public static int user_id { get; set; }
        public static int enterprise_id { get; set; }
        public static mainForm main_form { get; set; }


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

            History.AddEntry(new entity { FunId = 0, MainTabs = xtraTabControl1.SelectedTabPageIndex });
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            new frmLockApp().ShowDialog();
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

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var wb_in = new frmWayBillOut(-1, null))
            {
                wb_in.ShowDialog();
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var wb_in = new frmWayBillOut(1, null))
            {
                wb_in.ShowDialog();
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var wb_make = new frmWBManufacture(null))
            {
                wb_make.ShowDialog();
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var wb_make = new frmWBDeboning(null))
            {
                wb_make.ShowDialog();
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            History.AddEntry(new entity { FunId = 0, MainTabs = xtraTabControl1.SelectedTabPageIndex });
        }

        private void xtraTabControl1_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
           
        }

        private void PrevBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var p = History.Previous();
            if(p!= null)
            {
                History.is_enable = false;
                xtraTabControl1.SelectedTabPageIndex = p.MainTabs;
                History.is_enable = true;
            }
        }

        private void NextBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var n = History.Next();

            if (n != null)
            {
                History.is_enable = false;
                xtraTabControl1.SelectedTabPageIndex = n.MainTabs;
                History.is_enable = true;
            }

        }

    }
}
