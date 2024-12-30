using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using SP_Sklad.Common;
using SP_Sklad.Interfaces.Tablet.UI;
using SP_Sklad.SkladData;
using SP_Sklad.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SP_Sklad.Interfaces.Tablet
{
    public partial class frmMainTablet : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private int _user_id { get; set; }
        public static frmMainTablet main_form { get; set; }
        public BaseEntities _db { get; set; }
        public frmMainTablet()
             : this(UserSession.UserId)
        {

        }

        public frmMainTablet(int user_id)
        {
            InitializeComponent();
            _user_id = user_id;
            _db = new BaseEntities();
        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement10_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement16_Click(object sender, EventArgs e)
        {

        }

        private void frmMainTablet_FormClosed(object sender, FormClosedEventArgs e)
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

        private void accordionControlElement19_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement24_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement33_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement41_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement21_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            if (!mainContainer.Controls.Contains(mainContainer.Controls["ucWayBillIn"]))
            {
                mainContainer.Controls.Add(new ucWayBillIn() {  Dock = DockStyle.Fill});
            }
        }

        private void accordionControlElement45_Click(object sender, EventArgs e)
        {
            accordionControl1.SelectElement((AccordionControlElement)sender);

            var control = mainContainer.Controls["ucTabletOpenStoreSales"];

            if (!mainContainer.Controls.Contains(control))
            {
                control = new ucTabletOpenStoreSales() { Dock = DockStyle.Fill };
                mainContainer.Controls.Add(control);
            }

            control.BringToFront();
        }

        private void frmMainTablet_Load(object sender, EventArgs e)
        {
            var date = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
            Text = $"Кабінет регіонального менеджера [Користувач: {DBHelper.CurrentUser.Name}, Підприємство: {(DBHelper.CurrentEnterprise != null ? DBHelper.CurrentEnterprise.Name : "")}] [v.{ date }]";
        }

        private void accordionControlElement47_Click(object sender, EventArgs e)
        {
            new frmUserSettings().ShowDialog();
        }

        private void accordionControlElement15_Click(object sender, EventArgs e)
        {
            accordionControl1.SelectElement((AccordionControlElement)sender);

            var control = mainContainer.Controls["ucTabletWayBillCustomerOrder"];
            if (!mainContainer.Controls.Contains(control))
            {
                control = new ucTabletWayBillCustomerOrder() { Dock = DockStyle.Fill };
                mainContainer.Controls.Add(control);
            }

            control.BringToFront();
        }

        private void accordionControl1_ElementClick(object sender, ElementClickEventArgs e)
        {
            AccordionControl accordionControl = sender as AccordionControl;
            if (e.MouseButton == MouseButtons.Left && accordionControl.IsPopupFormShown)
            {
                accordionControl.ClosePopupForm();
                e.Handled = true;
            }
        }

        private void accordionControlElement48_Click(object sender, EventArgs e)
        {
            accordionControl1.SelectElement((AccordionControlElement)sender);

            var control = mainContainer.Controls["ucTabletOpenStorePaymentsSummary"];

            if (!mainContainer.Controls.Contains(control))
            {
                control = new ucTabletOpenStorePaymentsSummary() { Dock = DockStyle.Fill };
                mainContainer.Controls.Add(control);
            }

            control.BringToFront();
        }

        private void accordionControlElement23_Click(object sender, EventArgs e)
        {
            accordionControl1.SelectElement((AccordionControlElement)sender);

            var control = mainContainer.Controls["ucTabletWarehouse"];

            if (!mainContainer.Controls.Contains(control))
            {
                control = new ucTabletWarehouse() { Dock = DockStyle.Fill };
                mainContainer.Controls.Add(control);
            }

            control.BringToFront();
        }

        private void accordionControlElement49_Click(object sender, EventArgs e)
        {
            accordionControl1.SelectElement((AccordionControlElement)sender);

            var control = mainContainer.Controls["ucTabletReport27"];

            if (!mainContainer.Controls.Contains(control))
            {
                control = new ucTabletReport27() { Dock = DockStyle.Fill };
                mainContainer.Controls.Add(control);
            }

            control.BringToFront();
        }
    }
}
