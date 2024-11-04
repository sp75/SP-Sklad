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
       //     mainLabelControl.ImageOptions.ImageIndex = 7;
        //    accordionControl1.SelectElement((AccordionControlElement)sender);

        //    mainLabelControl.Text = $"{accordionControlElement44.Text} / {accordionControlElement45.Text}";

            if (!mainContainer.Controls.Contains(mainContainer.Controls["ucTabletOpenStoreSales"]))
            {
                mainContainer.Controls.Add(new ucTabletOpenStoreSales() { Dock = DockStyle.Fill });
            }

            
        }

        private void frmMainTablet_Load(object sender, EventArgs e)
        {
            var date = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
            Text = $"Кабінет регіонального менеджера [Користувач: {DBHelper.CurrentUser.Name}, Підприємство: {(DBHelper.CurrentEnterprise != null ? DBHelper.CurrentEnterprise.Name : "")}] [v.{ date }]";

        }
    }
}
