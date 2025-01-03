﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using System.Management;
using System.Text.RegularExpressions;
using System.Globalization;
using SP_Sklad.Common;
using System.Security.Principal;
using DevExpress.LookAndFeel;
using SP_Sklad.IntermediateWeighingInterface;
using DevExpress.XtraEditors;
using SP_Sklad.WBForm;
using SP_Sklad.Properties;
using System.Reflection;
using System.IO;
using SP_Sklad.RawMaterialManagementInterface;
using System.Configuration;
using SP_Sklad.DeboningWeighingInterface;
using SP_Sklad.Interfaces.ExpeditionInterface;
using SP_Sklad.Interfaces.Tablet;

namespace SP_Sklad
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        private bool is_registered { get; set; }
        private int? user_id => (int?)UserIDEdit.EditValue;
        private string ip_address => UniqueID.GetPhysicalIPAdress();
        private string user_name => string.IsNullOrEmpty(Environment.UserDomainName) ? Environment.UserName : Environment.UserDomainName + "\\" + Environment.UserName;
        private string _kay_id { get; set; }

        public frmLogin()
        {
            InitializeComponent();

            UserLookAndFeel.Default.SkinName = Properties.Settings.Default["ApplicationSkinName"].ToString();

            try
            {
                UserIDEdit.Properties.DataSource = new BaseEntities().Users.Where(w => w.IsWorking).Select(s => new
                {
                    s.UserId,
                    s.Name,
                    s.Pass
                }).ToList();
            }
            catch
            {
                MessageBox.Show("Не вдалось підключитись до сервера, зверніться до Админістратора");
            }
         //   var interfaces = new BaseEntities().GetUserInterfaces();
        //    InterfaceLookUpEdit.Properties.DataSource = new BaseEntities().Interfaces.Where(w=> w.Visible == true).ToList();

            CheckTrial();

            var ver = new BaseEntities().CommonParams.FirstOrDefault().Ver;
            if (ver != Application.ProductVersion)
            {
                label1.Visible = true;
                label1.Text = "З'явилася нова версія , загрузіть оновлення!";
            }

            _kay_id = UniqueID.getMotherBoardID();
            if (String.IsNullOrEmpty(_kay_id)) //якщо невдалось отримати ID bother board
            {
                _kay_id = UniqueID.GetMacAddress();
            }

            if (String.IsNullOrEmpty(_kay_id)) //якщо невдалось отримати MacAddress
            {
                try
                {
                    _kay_id = UniqueID.getUniqueID("C");
                }
                catch { }
            }

            if (String.IsNullOrEmpty(_kay_id))
            {
                _kay_id = "123456789";
            }
            //            var ddd = DeCoding(Coding("77419"));  //test

            var identity = WindowsIdentity.GetCurrent();
            if (SystemInformation.TerminalServerSession && identity.User.IsAccountSid())
            {
                var sid = identity.User.Value;
                long sum = 0;
                foreach (var item in sid.Split('-'))
                {
                    long s;
                    if (long.TryParse(item, out s))
                    {
                        sum += s;
                    }
                }
                _kay_id = sum.ToString();
            }

            using (var db = new BaseEntities())
            {
                var lic = db.Licenses.FirstOrDefault(w => w.MacAddress == _kay_id);
                if (lic == null)
                {
                    db.Licenses.Add(new Licenses
                    {
                        MacAddress = _kay_id,
                        LicencesKay = "",
                        IpAddress = ip_address,
                        MachineName = Environment.MachineName,
                        UserName = user_name
                    });
                    is_registered = false;
                }
                else
                {
                    lic.IpAddress = ip_address;
                    lic.MachineName = Environment.MachineName;
                    lic.UseDate = DateTime.Now;

                    is_registered = DeCoding(lic.LicencesKay) == lic.MacAddress;
                }

                if (!is_registered)
                {
                    label1.Text = "Програма не зареєстрована !";
                    label1.Visible = true;
                    RegBtn.Visible = true;
                }
                db.SaveChanges();
            }

        }

        private void CheckTrial()
        {
            using (var db = new BaseEntities())
            {
                var cp = db.CommonParams.First();
                if (cp.TrialPeriod != null && cp.TrialPeriod < DBHelper.ServerDateTime())
                {
                    cp.TrialPeriod = null;
                    db.Licenses.RemoveRange(db.Licenses.ToList());
                }

                db.SaveChanges();
            }

        }

        public string DeCoding(String val)
        {
            string rezult = "-1";
            if (val == null)
            {
                return rezult;
            }

            //                     --Метод №2--

            val = val.Replace("-", ""); // Убираем "-"

            var arr = val.ToCharArray().Reverse().ToArray(); // Переворачиваем строку
            val = new string(arr);


            var rez = "";
            for (int a = 1; val.Length > a; a++)  // В каждой паре символом меняем их местами1
            {
                rez = rez + val[a] + val[a - 1];
                a++;
            }

            if ((val.Length - rez.Length) == 1) rez = rez + val[val.Length-1];  // Коректировка нечетности

            int b = 0;
            val = "";
            for (int a = 0; a < (rez.Length / 3); ++a) // Декодируем
            {
                String block = rez.Substring(b, 3);
                b += 3;

                if (block == "VER") val = val + '0';
                if (block == "G4F") val = val + '1';
                if (block == "BAL") val = val + '2';
                if (block == "PVS") val = val + '3';
                if (block == "ZHD") val = val + '4';
                if (block == "NSP") val = val + '5';
                if (block == "LEY") val = val + '6';
                if (block == "8RT") val = val + '7';
                if (block == "MF3") val = val + '8';
                if (block == "RUK") val = val + '9';
            }

            return val;
        }

        private string Coding(String val)
        {
            if (val.Length < 4) return "Error code !";

            String conv = "", rezult = "";

            for (int a = 0; a < val.Length; a++)   // цифри в групи символів
            {
                switch (val[a])
                {
                    case '0': conv += "VER"; break;
                    case '1': conv += "G4F"; break;
                    case '2': conv += "BAL"; break;
                    case '3': conv += "PVS"; break;
                    case '4': conv += "ZHD"; break;
                    case '5': conv += "NSP"; break;
                    case '6': conv += "LEY"; break;
                    case '7': conv += "8RT"; break;
                    case '8': conv += "MF3"; break;
                    case '9': conv += "RUK"; break;
                }
            }

            for (int a = 1; conv.Length > a; a++)    // в кожній парі міняем символи місцями
            {
                rezult = rezult + conv[a] + conv[a - 1];
                a++;
            }

            if ((conv.Length - rezult.Length) == 1) rezult = rezult + conv[conv.Length-1];  // корегування нечетности


            var arr = rezult.ToCharArray().Reverse().ToArray(); // Переворачиваем строку
            rezult = new string(arr);

 
            for (int a = rezult.Length / 3 - 1; a > 0; a--) // добавляем "-" через кожні три символи
            {
                rezult = rezult.Insert((a * 3), "-");
            }

            return rezult;
        }



        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!is_registered)
            {
                Application.Exit();

                return;
            }

            DBHelper.ClearDBHelper();

            UserSession.UserId = user_id.Value;
            UserSession.SessionId = Guid.NewGuid();
            UserSession.EnterpriseId = Settings.Default.ent_id;
            UserSession.login_form = this;

            using (var db = new BaseEntities())
            {
                var user = db.Users.FirstOrDefault(w => w.Name == UserIDEdit.Text && w.Pass == passtextEdit.Text);
                if (user != null)
                {
                    user.LastLogin = DBHelper.ServerDateTime();
                    user.IsOnline = true;

                    db.LoginHistory.Add(new LoginHistory
                    {
                        IpAddress = ip_address,
                        MachineName = Environment.MachineName,
                        UserName = user_name,
                        UserId = user_id,
                        LoginDate = DBHelper.ServerDateTime(),
                        Ver = $"v.{ new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime }"
                    });

                    db.SaveChanges();

                    this.Hide();

                    switch (Settings.Default.interfaces_id)
                    {
                        case 1:
                            mainForm.main_form = new mainForm();
                            mainForm.main_form.Show();
                            break;

                        case 2:
                            WindowsFormsSettings.ForceDirectXPaint();
                            WindowsFormsSettings.TouchUIMode = TouchUIMode.True;
                            WindowsFormsSettings.TouchScaleFactor = 2;
                            frmMainIntermediateWeighing.main_form = new frmMainIntermediateWeighing();
                            frmMainIntermediateWeighing.main_form.Show();
                            break;

                        case 3:
                            try
                            {
                                frmCashierWorkplace.main_form = new frmCashierWorkplace();
                                frmCashierWorkplace.main_form.Show();
                            }
                            catch (Exception _ex)
                            {
                                MessageBox.Show(_ex.Message);
                                Application.Exit();
                            }
                            break;

                        case 4:
                            WindowsFormsSettings.TouchUIMode = TouchUIMode.True;
                            WindowsFormsSettings.TouchScaleFactor = 2;
                            frmMainRawMaterialManagement.main_form = new frmMainRawMaterialManagement();
                            frmMainRawMaterialManagement.main_form.Show();

                            break;


                        case 5:
                            WindowsFormsSettings.ForceDirectXPaint();
                            WindowsFormsSettings.TouchUIMode = TouchUIMode.True;
                            WindowsFormsSettings.TouchScaleFactor = 2;
                            frmDeboningWeighing.main_form = new frmDeboningWeighing();
                            frmDeboningWeighing.main_form.Show();
                            break;

                        case 6:
                            WindowsFormsSettings.ForceDirectXPaint();
                            WindowsFormsSettings.TouchUIMode = TouchUIMode.True;
                            WindowsFormsSettings.TouchScaleFactor = 2;
                            frmMainExpeditionInterface.main_form = new frmMainExpeditionInterface();
                            frmMainExpeditionInterface.main_form.Show();
                            break;

                        case 7:

                            WindowsFormsSettings.ForceDirectXPaint();
                            WindowsFormsSettings.TouchUIMode = TouchUIMode.True;
                            WindowsFormsSettings.TouchScaleFactor = 2;
                            frmMainTablet.main_form = new frmMainTablet();
                            frmMainTablet.main_form.Show();
                            break;

                        default:
                            Application.Exit();
                            break;
                    }
                }
                else
                {
                    label1.Visible = true;
                }

                passtextEdit.Text = "";
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ApplicationSkinName = UserLookAndFeel.Default.SkinName;
            Properties.Settings.Default.Save();
        }

        private void passtextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !String.IsNullOrEmpty(passtextEdit.Text))
            {
                OkButton.PerformClick();
            }

        }
        private void GetOk()
        {
            OkButton.Enabled = !string.IsNullOrEmpty(UserIDEdit.Text) && !string.IsNullOrEmpty(InterfaceLookUpEdit.Text);
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            if (Settings.Default.interfaces_id > 0)
            {
                panelControl3.Visible = false;
                this.Height = this.Height - panelControl3.Height;
            }

            Text = $"Авторизація користувача [{InterfaceLookUpEdit.Text}]";

            passtextEdit.Focus();

            GetOk();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (!panelControl3.Visible)
            {
                panelControl3.Visible = true;
                this.Height = this.Height + panelControl3.Height;
            }
            else
            {
                panelControl3.Visible = false;
                this.Height = this.Height - panelControl3.Height;
            }
        }

        private void UserIDEdit_EditValueChanged(object sender, EventArgs e)
        {
            var interfaces = new BaseEntities().GetUserInterfaces(user_id).ToList();
            InterfaceLookUpEdit.Properties.DataSource = interfaces.Any(a => a.Allow == 1) ? interfaces.Where(w => w.Allow == 1).ToList() : interfaces;  //new BaseEntities().Interfaces.Where(w => w.Visible == true).ToList();

            GetOk();
        }

        private void InterfaceLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            Text = $"Авторизація користувача [{InterfaceLookUpEdit.Text}]";

            GetOk();
        }

        private void RegBtn_Click(object sender, EventArgs e)
        {
            using (var frm = new frmReg(_kay_id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    using (var db = new BaseEntities())
                    {
                        var lic = db.Licenses.FirstOrDefault(w => w.MacAddress == _kay_id);

                        is_registered = DeCoding(lic.LicencesKay) == lic.MacAddress;
                        if (!is_registered)
                        {
                            label1.Text = "Програма не зареєстрована!";
                            label1.Visible = true;
                        }
                        else
                        {
                            RegBtn.Visible = false;
                            label1.Visible = false;
                            lic.UseDate = DateTime.Now;
                        }

                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
