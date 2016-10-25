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
    public partial class frmLockApp : DevExpress.XtraEditors.XtraForm
    {
        public static Users _users { get; set; }
        private bool is_close { get; set; }

        public frmLockApp()
        {
            InitializeComponent();
            _users = DB.SkladBase().Users.FirstOrDefault(w => w.UserId == mainForm.user_id);
            is_close = false;
        }

        private void frmLockApp_Load(object sender, EventArgs e)
        {
            UserIDEdit.Text = _users.Name;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (passtextEdit.Text == _users.Pass)
            {
                is_close = true;
                Close();
                mainForm.main_form.Show();
            }
            else
            {
                passtextEdit.Focus();
            }
        }

        private void frmLockApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !is_close;
        }

        private void passtextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !String.IsNullOrEmpty(passtextEdit.Text))
            {
                OkButton.PerformClick();
            }
        }

        private void frmLockApp_Shown(object sender, EventArgs e)
        {
            passtextEdit.Focus();
        }
    }
}
