using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.SkladData;

namespace SP_Sklad
{
    public partial class frmUserSettings : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        public frmUserSettings()
        {
            InitializeComponent();
            _db = DB.SkladBase();
        }

        private void frmUserSettings_Load(object sender, EventArgs e)
        {
            user_settings = new UserSettingsRepository(UserSession.UserId, _db);

            UserBS.DataSource = _db.Users.Find(UserSession.UserId);

            comboBoxEdit2.Text = user_settings.GridFontName;
            comboBoxEdit1.Value = user_settings.GridFontSize;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DBHelper.CurrentUser = null;
            _db.SaveChanges();
        }

        private void comboBoxEdit2_EditValueChanged(object sender, EventArgs e)
        {
            user_settings.GridFontName = comboBoxEdit2.Text;
        }

        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            user_settings.GridFontSize = comboBoxEdit1.Value;
        }
    }
}
