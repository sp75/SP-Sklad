using SP_Sklad.Common;
using SP_Sklad.Properties;
using SP_Sklad.SkladData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad.TradeForm
{
    public partial class frmCashboxSettings : DevExpress.XtraEditors.XtraForm
    {

        private BaseEntities _db { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        public frmCashboxSettings()
        {
            InitializeComponent();

            _db = DB.SkladBase();
            current_transaction = _db.Database.BeginTransaction();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);
        }

        private void frmCashboxSettings_Load(object sender, EventArgs e)
        {
            ChargeTypesEdit.Properties.DataSource = DBHelper.ChargeTypes;
            ChargeTypesEdit.EditValue = user_settings.DefaultChargeTypeByRMK;

            CashEditComboBox.Properties.DataSource = DBHelper.AllCashDesks.Where(w => !string.IsNullOrEmpty(w.LicenseKey)).ToList();
            CashEditComboBox.EditValue = user_settings.CashDesksDefaultRMK;

            AccountEdit.Properties.DataSource = _db.EnterpriseAccount.Select(s => new
            {
                AccId = s.AccId,
                AccNum = s.AccNum,
                Name = s.BankName,
                s.KaName
            }).ToList();

            AccountEdit.EditValue = user_settings.AccountDefaultRMK;


            String pkInstalledPrinters;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                comboInstalledPrinters.Properties.Items.Add(pkInstalledPrinters);
            }

            comboInstalledPrinters.Text = Settings.Default.receipt_printer;
            RroundingCheckBox.Checked = user_settings.RoundingCheckboxReceipt;
        }

        private void ChargeTypesEdit_EditValueChanged(object sender, EventArgs e)
        {
            user_settings.DefaultChargeTypeByRMK = (int)ChargeTypesEdit.EditValue;
        }

        private void CashEditComboBox_EditValueChanged(object sender, EventArgs e)
        {
            user_settings.CashDesksDefaultRMK = (int)CashEditComboBox.EditValue;
        }

        private void AccountEdit_EditValueChanged(object sender, EventArgs e)
        {
            user_settings.AccountDefaultRMK = (int)AccountEdit.EditValue;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
            current_transaction.Commit();
        }

        private void frmCashboxSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            user_settings.RoundingCheckboxReceipt = RroundingCheckBox.Checked;
        }
    }
}
