using CheckboxIntegration.Client;
using CheckboxIntegration.Models;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using SP_Sklad.Common;
using SP_Sklad.SkladData;
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

        public frmCashierWorkplace()
        {
            InitializeComponent();

            using (var _db = new BaseEntities())
            {
                 user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

                var login = new CheckboxClient().CashierSignin(new CashierSigninRequest { login = user_settings.CashierLoginCheckbox, password = user_settings.CashierPasswordCheckbox });

                _access_token = login.access_token;

                _license_key = _db.CashDesks.FirstOrDefault(w => w.CashId == user_settings.CashDesksDefaultRMK).LicenseKey;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            using (var frm = new frmCashboxWBOut())
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
                    db.Shift.Add(new SkladData.Shift { Id = new Guid(new_shift.id),  CreatedAt = Convert.ToDateTime(new_shift.created_at), UserId = DBHelper.CurrentUser.UserId });

                    db.SaveChanges();
                }

                simpleButton5.PerformClick();
            }
            else
            {
                MessageBox.Show(new_shift.error.message);
            }
        }

        private void btnCloseShift_Click(object sender, EventArgs e)
        {
            var new_shift = new CheckboxClient(_access_token).CloseShift();
            if (new_shift.error == null)
            {
                using (var db = new BaseEntities())
                {
                    var shift = db.Shift.FirstOrDefault(w => w.Id == new Guid(new_shift.id));

                    if (shift != null)
                    {
                        shift.ClosedAt = Convert.ToDateTime(new_shift.closed_at);
                    }
                }


                var z_report = new CheckboxClient(_access_token).GetReportText(new Guid(new_shift.z_report.id), ReceiptExportFormat.text);

                var result_file = Path.Combine(Application.StartupPath, "Rep", new_shift.z_report.id + ".txt");
                File.WriteAllBytes(result_file, z_report);

                if (File.Exists(result_file))
                {
                    Process.Start(result_file);
                }

                /*     var z = Encoding.UTF8.GetString(z_report);


                    RichTextBox rtb = new RichTextBox();
                    rtb.Text = File.ReadAllText(result_file, System.Text.Encoding.UTF8);
                    RichTextBoxLink rtbl = new RichTextBoxLink(new PrintingSystem());
                    rtbl.RichTextBox = rtb;
                    rtbl.ShowPreviewDialog();*/

                /*    PrintDocument p = new PrintDocument();
                      p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
                      {
                          e1.Graphics.DrawString(z, new Font("Times New Roman", 12), new SolidBrush(Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));

                      };
                      try
                      {
                          p.Print();
                      }
                      catch (Exception ex)
                      {
                          throw new Exception("Exception Occured While Printing", ex);
                      }

                      MessageBox.Show("Зміна закрита, надрукувати Z-звіт ?");*/
            }
            else
            {
                MessageBox.Show(new_shift.error.message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = new RichTextBox();
            rtb.Text = File.ReadAllText(@"c:\WinVSProjects\SP-Sklad\SP_Sklad\bin\Debug\Rep\b810cf5a-deb1-4bf1-bf24-9fa34f469ed9.txt", System.Text.Encoding.UTF8);
            //rtb.LoadFile(@"c:\WinVSProjects\SP-Sklad\SP_Sklad\bin\Debug\Rep\b810cf5a-deb1-4bf1-bf24-9fa34f469ed9.txt", RichTextBoxStreamType.PlainText);
            RichTextBoxLink rtbl = new RichTextBoxLink(new PrintingSystem());
            rtbl.RichTextBox = rtb;
            rtbl.ShowPreviewDialog();
        }
    }
}
