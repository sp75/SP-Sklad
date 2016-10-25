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

namespace SP_Sklad
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        public frmLogin()
        {
            InitializeComponent();

            UserIDEdit.Properties.DataSource = new BaseEntities().Users.Select(s => new { s.UserId, s.Name, s.Pass }).ToList();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            using (var db = new BaseEntities())
            {
                var user = db.Users.FirstOrDefault(w => w.Name == UserIDEdit.Text && w.Pass == passtextEdit.Text);
                if (user != null)
                {
                    user.LastLogin = DBHelper.ServerDateTime();
                    user.IsOnline = true;
                    db.SaveChanges();

                    this.Hide();
                    mainForm.main_form = new mainForm((int)UserIDEdit.EditValue);
                    mainForm.main_form.Show();
                }
                else
                {
                    label1.Visible = true;
                }
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void passtextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !String.IsNullOrEmpty(passtextEdit.Text))
            {
                OkButton.PerformClick();
            }

        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            passtextEdit.Focus();
        }
    }
}
