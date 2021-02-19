using SP_Sklad.Common;
using SP_Sklad.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad.ViewsForm
{
    public partial class frmMessageBox : DevExpress.XtraEditors.XtraForm
    {
        public UserSettingsRepository user_settings { get; set; }

        public frmMessageBox(string caption, string Text, bool show_check_message = true)
        {
            InitializeComponent();

            this.Text = caption;
            MessageText.Text = Text;
            user_settings = new UserSettingsRepository();

            checkEdit5.Visible = show_check_message;
        }

        private void frmMessageBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Resources.wb_copy == MessageText.Text)
            {
                user_settings.NotShowMessageCopyDocuments = checkEdit5.Checked;
            }
        }
    }
}
