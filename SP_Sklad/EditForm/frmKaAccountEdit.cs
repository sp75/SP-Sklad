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
    public partial class frmKaAccountEdit : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private int? _AccId { get; set; }
        private KAgentAccount ka_acc { get; set; }

        public frmKaAccountEdit(int? AccId = null)
        {
            _AccId = AccId;
            _db = DB.SkladBase();

            InitializeComponent();

            EntEdit.Properties.DataSource = _db.KagentList.Where(w => w.Archived == 0 || w.Archived == null).Select(s => new { s.KaId, s.Name }).ToList();
            lookUpEdit1.Properties.DataSource = _db.AccountType.Select(s => new { s.TypeId, s.Name }).ToList();
            lookUpEdit2.Properties.DataSource = _db.Banks.Select(s => new { s.BankId, s.Name }).ToList();
        }

        private void frmCashdesksEdit_Load(object sender, EventArgs e)
        {
            if (_AccId == null)
            {
                ka_acc = _db.KAgentAccount.Add(new KAgentAccount
                {
                  //  KAId = _ka.KaId,
                    AccNum = "",
                    TypeId = _db.AccountType.FirstOrDefault().TypeId,
                    BankId = _db.Banks.FirstOrDefault().BankId,
                 //   Def = _db.KAgentAccount.Any(a => a.KAId == _ka.KaId) ? 0 : 1
                });

                Text = "Додати новий рахунок:";
            }
            else
            {
                ka_acc = _db.KAgentAccount.Find(_AccId);

                Text = "Редагувати рахунок:";
            }

            KAgentAccountBS.DataSource = ka_acc;

            DefCheckBox.Enabled = !DefCheckBox.Checked;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
        }

        private void KTypeLookUpEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                EntEdit.EditValue = null;
            }
        }
    }
}
