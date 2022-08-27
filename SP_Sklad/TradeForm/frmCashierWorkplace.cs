using CheckboxIntegration.Client;
using CheckboxIntegration.Models;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using SP_Sklad.Common;
using SP_Sklad.SkladData;
using SP_Sklad.TradeForm;
using SP_Sklad.ViewsForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad.WBForm
{
    public partial class frmCashierWorkplace : DevExpress.XtraEditors.XtraForm
    {
        private string _access_token { get; set; }
        private string _license_key { get; set; }
        private UserSettingsRepository user_settings { get; set; }
        public static frmCashierWorkplace main_form { get; set; }

        public frmCashierWorkplace()
        {
            InitializeComponent();

            using (var _db = new BaseEntities())
            {
                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);
                try
                {
                    var login = new CheckboxClient().CashierSignin(new CashierSigninRequest { login = user_settings.CashierLoginCheckbox, password = user_settings.CashierPasswordCheckbox });

                    _access_token = login.access_token;
                }
                catch { }

                _license_key = _db.CashDesks.FirstOrDefault(w => w.CashId == user_settings.CashDesksDefaultRMK).LicenseKey;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            using (var frm = new frmCashboxWBOut(_access_token))
            {
                frm.ShowDialog();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var new_shift = new CheckboxClient(_access_token).CreateShift(_license_key);
            if (new_shift.error == null)
            {
                using (var db = new BaseEntities())
                {
                    db.Shift.Add(new SkladData.Shift
                    {
                        Id = new_shift.id,
                        CreatedAt = new_shift.created_at,
                        UserId = DBHelper.CurrentUser.UserId,
                        OpenedAt = WaitingOpeningShift(new_shift.id),
                        CashId = user_settings.CashDesksDefaultRMK
                    });

                    db.SaveChanges();
                }
                simpleButton5.Enabled = true;
                simpleButton5.PerformClick();
            }
            else
            {
                MessageBox.Show(new_shift.error.message);
            }
        }

        public DateTime WaitingOpeningShift(Guid shift_id)
        {
            DateTime? open_at = null;

            while(!open_at.HasValue)
            {
                var new_shift = new CheckboxClient(_access_token).GetShift(shift_id);
                open_at = new_shift.opened_at;
            }

            return open_at.Value;
        }

        public DateTime WaitingClosingShift(Guid shift_id)
        {
            DateTime? closed_at = null;

            while (!closed_at.HasValue)
            {
                var new_shift = new CheckboxClient(_access_token).GetShift(shift_id);
                closed_at = new_shift.closed_at;
            }

            return closed_at.Value;
        }

        private void btnCloseShift_Click(object sender, EventArgs e)
        {
            var close_shift = new CheckboxClient(_access_token).CloseShift();
            if (close_shift.error == null)
            {
                using (var db = new BaseEntities())
                {
                    var shift = db.Shift.FirstOrDefault(w => w.Id == close_shift.id);

                    if (shift != null)
                    {
                        shift.ClosedAt = WaitingClosingShift(close_shift.id);
                        shift.UpdatedAt = close_shift.updated_at;
                    }

                    db.SaveChanges();
                }

                simpleButton5.Enabled = false;


                var z_report = IHelper.PrintReportText(_access_token, new Guid(close_shift.z_report.id));

                var result_file = Path.Combine(Application.StartupPath, "Rep", close_shift.z_report.id + ".txt");
                File.WriteAllBytes(result_file, z_report);

                if (File.Exists(result_file))
                {
                    Process.Start(result_file);
                }

            }
            else
            {
                MessageBox.Show(close_shift.error.message);
            }
        }

        private void frmCashierWorkplace_Load(object sender, EventArgs e)
        {
            using (var db = new BaseEntities())
            {
                try
                {
                    var cashier_shift = new CheckboxClient(_access_token).GetCashierShift();
                    //        var active_shift = db.Shift.OrderByDescending(o => o.CreatedAt).FirstOrDefault(w => w.CashId == user_settings.CashDesksDefaultRMK);
                    simpleButton5.Enabled = /*(active_shift != null && !active_shift.ClosedAt.HasValue)*/(cashier_shift != null && !cashier_shift.is_error && cashier_shift.status == ShiftStatus.OPENED && (DateTime.Now - cashier_shift.opened_at.Value).TotalHours < 24) || string.IsNullOrEmpty(_access_token);
                }
                catch
                {

                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            IHelper.KssBook(DateTime.Now.Date, user_settings.CashDesksDefaultRMK);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var new_x_report = new CheckboxClient(_access_token).CreateXReport();
            if (new_x_report.error == null)
            {
                var x_report = IHelper.PrintReportText(_access_token, new_x_report.id);

                var result_file = Path.Combine(Application.StartupPath, "Rep", new_x_report.id.ToString() + ".txt");
                File.WriteAllBytes(result_file, x_report);

                if (File.Exists(result_file))
                {
                    Process.Start(result_file);
                }

            }
            else
            {
                MessageBox.Show(new_x_report.error.message);
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            using (var frm = new frmCashboxSettings())
            {
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    using (var db = new BaseEntities())
                    {
                        user_settings = new UserSettingsRepository();
                        _license_key = db.CashDesks.FirstOrDefault(w => w.CashId == user_settings.CashDesksDefaultRMK).LicenseKey;
                    }
                }
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            using (var frm = new frmCustomInfo())
            {
                frm.Text = $"Інформація про активну зміну касира: {DBHelper.CurrentUser.Name}";

                if (!string.IsNullOrEmpty(_access_token))
                {
                    var new_receipts = new CheckboxClient(_access_token).GetCashierShift();
                    if (new_receipts.error == null)
                    {
                        frm.AddItem("Зміна відкрита", new_receipts.opened_at);
                        frm.AddItem("Продаж за готівку", new_receipts.balance.cash_sales / 100.00);
                        frm.AddItem("Продаж по картці", new_receipts.balance.card_sales / 100.00);
                        frm.AddItem("Повернення готівкою", new_receipts.balance.cash_returns / 100.00);
                        frm.AddItem("Залишок в касі (checkbox)", new_receipts.balance.balance / 100.00);
                    }
                }
                frm.ShowDialog();
            }
        }
    }
}
