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
using System.IO;
using System.Diagnostics;
using System.Reflection;

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

            UserSession.UserId = UserId;
            UserSession.SessionId = Guid.NewGuid();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            CurDateEditBarItem.EditValue = DateTime.Now;
            repositoryItemLookUpEdit1.DataSource = DBHelper.EnterpriseList;

            if (barEditItem3.EditValue == null || barEditItem3.EditValue == DBNull.Value)
            {
                barEditItem3.EditValue = DBHelper.EnterpriseList.Select(s => s.KaId).FirstOrDefault();
            }

            History.AddEntry(new HistoryEntity { FunId = 0, MainTabs = xtraTabControl1.SelectedTabPageIndex });
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            new frmLockApp().ShowDialog();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var want_to_exit = MessageBox.Show(@"Ви дійсно хочете вийти з програми?", @"Закрити програму", MessageBoxButtons.YesNo) == DialogResult.Yes;
            if (want_to_exit)
            {
                DBHelper.ClearSessionWaybill();
            }

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
                }

                var wb_list = db.WaybillList.Where(w => w.SessionId == UserSession.SessionId).ToList();
                foreach (var item in wb_list)
                {
                    item.SessionId = null;
                }

                db.SaveChanges();
            }
            Application.Exit();
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            var enterprise = DBHelper.EnterpriseList.FirstOrDefault();// new BaseEntities().Kagent.Where(w => w.KType == 3).Select(s => new { s.KaId }).FirstOrDefault();
            if ((barEditItem3.EditValue == null || barEditItem3.EditValue == DBNull.Value))
            {
                if (enterprise != null)
                {
                    enterprise_id = enterprise.KaId;
                    barEditItem3.EditValue = enterprise.KaId;
                }
            }
            else if ((int)barEditItem3.EditValue == 0 && enterprise != null)
            {
                enterprise_id = enterprise.KaId;
                barEditItem3.EditValue = enterprise.KaId;
            }
            else
            {
                enterprise_id = (int)barEditItem3.EditValue;
            }

            GetMainHeder();
        }

        private void barEditItem3_EditValueChanged(object sender, EventArgs e)
        {
            if (barEditItem3.EditValue == DBNull.Value)
            {
                barEditItem3.EditValue = enterprise_id;
            }
            else
            {
                enterprise_id = (int)barEditItem3.EditValue;
                DBHelper.Enterprise = null;
            }

            GetMainHeder();
        }

        private void GetMainHeder()
        {
            var date = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
            Text = "SP-Склад [Користувач: " + DBHelper.CurrentUser.Name + ", Підприємство: " + (DBHelper.Enterprise != null ? DBHelper.Enterprise.Name : "") + "] [v." + date + "]";
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
            History.AddEntry(new HistoryEntity { FunId = 0, MainTabs = xtraTabControl1.SelectedTabPageIndex });
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
                SetNode(p);
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
                SetNode(n);
                History.is_enable = true;
            }

        }

        private void SetNode(HistoryEntity e)
        {
            switch (e.MainTabs)
            {
                case 0:
                    docsUserControl1.DocsTreeList.FocusedNode = docsUserControl1.DocsTreeList.FindNodeByFieldValue("FunId", e.FunId);
                    break;

                case 1:
                    manufacturingUserControl1.DocsTreeList.FocusedNode = manufacturingUserControl1.DocsTreeList.FindNodeByFieldValue("FunId", e.FunId);
                    break;

                case 2:
                    whUserControl.WHTreeList.FocusedNode = whUserControl.WHTreeList.FindNodeByFieldValue("FunId", e.FunId);
                    break;

                case 3:
                    financesUserControl1.FinancesTreeList.FocusedNode = financesUserControl1.FinancesTreeList.FindNodeByFieldValue("FunId", e.FunId);
                    break;

                case 5:
                    DirUserControl.DirTreeList.FocusedNode = DirUserControl.DirTreeList.FindNodeByFieldValue("FunId", e.FunId);
                    break;

                case 6:
                    serviceUserControl1.DirTreeList.FocusedNode = serviceUserControl1.DirTreeList.FindNodeByFieldValue("FunId", e.FunId);
                    break;
            }
 
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ver = new BaseEntities().CommonParams.FirstOrDefault().Ver;
            if (ver != Application.ProductVersion)
            {
                var update = Path.Combine(Application.StartupPath, "update.exe");
                if (File.Exists(update))
                {
                    Application.Exit();
                    Process.Start(update);
                }
            }
            else
            {
                MessageBox.Show("У Вас остання версія");
            }
        }

        private void barButtonItem10_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Restart();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmCashboxWBOut().ShowDialog();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmAboutAs().ShowDialog();
        }

    }
}
