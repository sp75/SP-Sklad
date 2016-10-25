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

namespace SP_Sklad.EditForm
{
    public partial class frmAccountTypeEdit : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private int? _TypeId { get; set; }
        private AccountType at { get; set; }

        public frmAccountTypeEdit(int? TypeId = null)
        {
            _TypeId = TypeId;
            _db = DB.SkladBase();

            InitializeComponent();
        }

        private void frmAccountTypeEdit_Load(object sender, EventArgs e)
        {
            if (_TypeId == null)
            {
                at = _db.AccountType.Add(new AccountType
                {
                    Deleted = 0,
                    Def = _db.AccountType.Any(a => a.Def == 1) ? 0 : 1,
                    Name = ""
                });

            }
            else
            {
                at = _db.AccountType.Find(_TypeId);
            }

            AccountTypeBS.DataSource = at;

            DefCheckBox.Enabled = !DefCheckBox.Checked;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
        }
    }
}
