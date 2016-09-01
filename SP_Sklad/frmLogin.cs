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
    public partial class frmLogin : Form
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
                    this.Hide();
                    var frm = new mainForm();
                    mainForm.user_id = (int)UserIDEdit.EditValue;
                    //   frm.password = passtextEdit.Text;
                    //   frm.uid = user.uid.ToString();
                    frm.Show();
                    //   if (!RememberMeCheck.Checked) passtextEdit.Text = "";
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
    }
}
