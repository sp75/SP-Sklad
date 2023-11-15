using DevExpress.XtraBars;
using SP_Sklad.Common;
using SP_Sklad.Interfaces.Tablet.UI;
using SP_Sklad.SkladData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            if (!mainContainer.Controls.Contains(mainContainer.Controls["ucWaybillIn"]))
            {
                mainContainer.Controls.Add(new ucWaybillIn());
            }
        }
    }
}
