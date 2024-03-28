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
using System.Data.Entity.Infrastructure;
using System.Collections;
using SP_Sklad.ViewsForm;
using DevExpress.XtraEditors;
using SP_Sklad.EditForm;
using System.Globalization;

namespace SP_Sklad
{
    public partial class mainForm : DevExpress.XtraEditors.XtraForm
    {
        private int user_id { get; set; }
        public static mainForm main_form { get; set; }

        private int? _wid => (mainTabControl.SelectedTabPageIndex == 2 && whUserControl.ByWhBtn.Down) ? whUserControl.focused_tree_node?.Num : null;


        public mainForm() : this(UserSession.UserId) { }

        public mainForm(int UserId)
        {
            InitializeComponent();
            user_id = UserId;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            CurDateEditBarItem.EditValue = DateTime.Now;
            repositoryItemLookUpEdit1.DataSource = DBHelper.EnterpriseList;

            if (barEditItem3.EditValue == null || barEditItem3.EditValue == DBNull.Value || (barEditItem3.EditValue != null && barEditItem3.EditValue != DBNull.Value && !DBHelper.EnterpriseList.Any(a => a.KaId == Convert.ToInt32(barEditItem3.EditValue))))
            {
                barEditItem3.EditValue = DBHelper.EnterpriseList.Select(s => s.KaId).FirstOrDefault();
            }

            History.AddEntry(new HistoryEntity { FunId = 0, MainTabs = mainTabControl.SelectedTabPageIndex });

            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Rep")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Rep"));
            }

            var sta = new AppSettingRepository().ShowTradeApp;
            TradeTabPage.PageVisible = sta;
            CashierWorkplaceBtn.Visibility = sta ? CashierWorkplaceBtn.Visibility : DevExpress.XtraBars.BarItemVisibility.Never;

            CashierWorkplaceBtn.Enabled = DBHelper.is_main_cacher || DBHelper.is_cacher;
            WbCorrBtn.Enabled = DBHelper.is_buh;

            WbMatTemplateBtn.Visibility = IHelper.GetUserTreeView(143)?.Visible == 1 ? WbMatTemplateBtn.Visibility : DevExpress.XtraBars.BarItemVisibility.Never;
            WbMatTemplateBtn.Enabled = IHelper.GetUserAccess(96)?.CanView == 1;
            NewCustomerOrder.Enabled = IHelper.GetUserAccess(64)?.CanInsert == 1;
            NewWBWriteOnItem.Enabled = IHelper.GetUserAccess(44)?.CanInsert == 1;
            AddWBInBtn.Enabled = IHelper.GetUserAccess(21)?.CanInsert == 1;
            AddWBOutBtn.Enabled = IHelper.GetUserAccess(23)?.CanInsert == 1;
            NewWayBillMoveBtn.Enabled = IHelper.GetUserAccess(36)?.CanInsert == 1;
            NewWBWriteOffBtn.Enabled = IHelper.GetUserAccess(41)?.CanInsert == 1;
            AddManufacturingBtn.Enabled = IHelper.GetUserAccess(68)?.CanInsert == 1;
            AddDeboningBtn.Enabled = IHelper.GetUserAccess(72)?.CanInsert == 1;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            new frmLockApp().ShowDialog();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var want_to_exit = XtraMessageBox.Show(@"Ви дійсно хочете вийти з програми?", @"Закрити програму", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

            if (want_to_exit)
            {
                DBHelper.ClearSessionWaybill();
            }

            e.Cancel = !want_to_exit;
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            docsUserControl1.wayBillInUserControl.SaveGridLayouts();
            docsUserControl1.ucWBOrdersOut.SaveGridLayouts();
            docsUserControl1.ucWaybillOut.SaveGridLayouts();
            docsUserControl1.ucWBOrdersIn.SaveGridLayouts();
            docsUserControl1.ucInvoices.SaveGridLayouts();
            docsUserControl1.ucServicesIn.SaveGridLayouts();
            docsUserControl1.ucWayBillReturnСustomers.SaveGridLayouts();
            docsUserControl1.ucWaybillReturnSuppliers.SaveGridLayouts();
            docsUserControl1.ucKAgentAdjustmentIn.SaveGridLayouts();
            docsUserControl1.ucKAgentAdjustmentOut.SaveGridLayouts();
            docsUserControl1.ucPayDocIn.SaveGridLayouts();
            docsUserControl1.ucPayDocOut.SaveGridLayouts();
            docsUserControl1.ucPayDocExtOut.SaveGridLayouts();
            docsUserControl1.ucPayDoc.SaveGridLayouts();
            docsUserControl1.ucProjectManagement.SaveGridLayouts();

            whUserControl.ucWaybillMove.SaveGridLayouts();
            whUserControl.ucWaybillWriteOn.SaveGridLayouts();
            whUserControl.ucWaybillWriteOff.SaveGridLayouts();
            whUserControl.ucWhMat.SaveGridLayouts();
            whUserControl.ucWaybillInventory.SaveGridLayouts();

            manufacturingUserControl1.ucManufacturingProducts.SaveGridLayouts();
            manufacturingUserControl1.ucDeboningProducts.SaveGridLayouts();

            tradeUserControl1.SaveGridLayouts();
            financesUserControl1.SaveGridLayouts();

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
                    UserSession.EnterpriseId = enterprise.KaId;
                    barEditItem3.EditValue = enterprise.KaId;
                }
            }
            else if ((int)barEditItem3.EditValue == 0 && enterprise != null)
            {
                UserSession.EnterpriseId = enterprise.KaId;
                barEditItem3.EditValue = enterprise.KaId;
            }
            else
            {
                UserSession.EnterpriseId = (int)barEditItem3.EditValue;
            }

            GetMainHeder();

            var user_acc = DB.SkladBase().UserAccess.Where(w => w.UserId == DBHelper.CurrentUser.UserId).ToList();
            AddDeboningBtn.Enabled = user_acc.Any(w => w.FunId == 72 && w.CanInsert == 1);
            AddManufacturingBtn.Enabled = user_acc.Any(w => w.FunId == 68 && w.CanInsert == 1);
            AddWBOutBtn.Enabled = user_acc.Any(w => w.FunId == 23 && w.CanInsert == 1);
            AddWBInBtn.Enabled = user_acc.Any(w => w.FunId == 21 && w.CanInsert == 1);

            mainTabControl.SelectedTabPageIndex = Properties.Settings.Default.LastTabPage;
            SetNode(new HistoryEntity
            {
                FunId = Properties.Settings.Default.LastFunId,
                MainTabs = Properties.Settings.Default.LastTabPage
            });
        }

        private void barEditItem3_EditValueChanged(object sender, EventArgs e)
        {
            if (barEditItem3.EditValue == DBNull.Value)
            {
                barEditItem3.EditValue = UserSession.EnterpriseId;
            }
            else
            {
                UserSession.EnterpriseId = (int)barEditItem3.EditValue;
                DBHelper.Enterprise = null;
            }

            GetMainHeder();
        }

        private void GetMainHeder()
        {
            var date = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
            Text = $"{DBHelper.CommonParam.ProgramName} [Користувач: {DBHelper.CurrentUser.Name}, Підприємство: {(DBHelper.Enterprise != null ? DBHelper.Enterprise.Name : "")}] [v.{ date }]";
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();

            //     Application.Exit();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            History.AddEntry(new HistoryEntity { FunId = 0, MainTabs = mainTabControl.SelectedTabPageIndex });

            Properties.Settings.Default.LastTabPage = mainTabControl.SelectedTabPageIndex;
        }

        private void PrevBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var p = History.Previous();
            if (p != null)
            {
                History.is_enable = false;
                mainTabControl.SelectedTabPageIndex = p.MainTabs;
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
                mainTabControl.SelectedTabPageIndex = n.MainTabs;
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
                    tradeUserControl1.DocsTreeList.FocusedNode = tradeUserControl1.DocsTreeList.FindNodeByFieldValue("FunId", e.FunId);
                    break;

                case 4:
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
                    // Application.Exit();
                    Close();
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
            mainForm.main_form.Dispose();
            UserSession.login_form.Show();
            //Close();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmAboutAs().ShowDialog();
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //    Application.Exit();
            Close();

            var update = Path.Combine(Application.StartupPath, "update.exe");

            if (File.Exists(update))
            {
                Process.Start(update, "true");
            }
        }

        private void AddWBOutBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var wb_in = new frmWayBillOut(-1, null))
            {
                wb_in.ShowDialog();
            }
        }

        private void WbMatTemplateBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var f = new frmCatalog(null, 143))
            {
                f.uc.isDirectList = false;
                f.uc.splitContainerControl1.Collapsed = true;
                f.Text = "Шаблони";
                f.ShowDialog();
            }
        }

        private void AddWBInBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var wb_in = new frmWayBillIn(1, wid: _wid))
            {
                wb_in.ShowDialog();
            }
        }

        private void AddManufacturingBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var wb_make = new frmWBManufacture(null))
            {
                wb_make.ShowDialog();
            }
        }

        private void AddDeboningBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var wb_make = new frmWBDeboning(null))
            {
                wb_make.ShowDialog();
            }
        }

        private void CashierWorkplaceBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmCashierWorkplace())
            {
                frm.ShowDialog();

                docsUserControl1.RefrechItemBtn.PerformClick();
            }
        }

        private void NewCustomerOrder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var wb_in = new frmWayBillCustomerOrder(-16, null))
            {
                wb_in.ShowDialog();
            }
        }

        private void NewWBWriteOnItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var wb_in = new frmWBWriteOn(wid: _wid))
            {
                wb_in.ShowDialog();
            }
        }

        private void NewWayBillMoveBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var wb_move = new frmWayBillMove(wid: _wid))
            {
                wb_move.ShowDialog();
            }
        }

        private void NewWBWriteOffBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var wb_on = new frmWBWriteOff(wid: _wid))
            {
                wb_on.ShowDialog();
            }
        }

        private void WbCorrBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmWaybillCorrection())
            {
                frm.ShowDialog();
            }
        }

        private void WbCorrListBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmWaybillCorrectionsView().ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (UserSession.production_monitor_frm == null)
            {
                UserSession.production_monitor_frm = new frmProductionMonitor();
                UserSession.production_monitor_frm.Show();
            }
            else
            {
                UserSession.production_monitor_frm.WindowState = FormWindowState.Normal;
                UserSession.production_monitor_frm.Activate();
            }
        }
    }
}
