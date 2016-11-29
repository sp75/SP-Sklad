using System;
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

namespace SP_Sklad
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        private bool is_registered { get; set; }
        public frmLogin()
        {
            InitializeComponent();

            UserIDEdit.Properties.DataSource = new BaseEntities().Users.Select(s => new { s.UserId, s.Name, s.Pass }).ToList();

        /*    var intetf  =  GetMacAddress();
            var mac_address = Regex.Replace(intetf.GetPhysicalAddress().ToString(), "[^0-9 ]", "") ;
            var ip = intetf.GetIPProperties().UnicastAddresses.Where(w => w.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            var ip_address = ip.Any() ? ip.FirstOrDefault().Address.ToString() : "";
            using (var db = new BaseEntities())
            {
                var lic = db.Llicenses.FirstOrDefault(w => w.MacAddress == mac_address);
                if (lic == null)
                {
                    db.Llicenses.Add(new Llicenses
                    {
                        MacAddress = mac_address,
                        LicencesKay = "",
                        IpAddress = ip_address,
                        MachineName = Environment.MachineName
                    });
                    is_registered = false;
                }
                else
                {
                    lic.IpAddress = ip_address;
                    lic.MachineName = Environment.MachineName;
                    is_registered = DeCoding(lic.LicencesKay) == Convert.ToInt32(lic.MacAddress);
                }

                if (!is_registered)
                {
                    label1.Text = "Програма не зареєстрована, зверніться до адміністратора!";
                    label1.Visible = true;
                }
                db.SaveChanges();
            }*/

        }
       
        public NetworkInterface GetMacAddress()
        {
            var myInterfaceAddress = NetworkInterface.GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .OrderByDescending(n => n.NetworkInterfaceType == NetworkInterfaceType.Ethernet).FirstOrDefault();
              //  .Select(n => n.GetPhysicalAddress())
                

            return myInterfaceAddress;
        }

        public long DeCoding(String val)
        {
            long rezult = -1;
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

            if (!String.IsNullOrEmpty(val))
            {
                rezult = Convert.ToInt32(val);
            }

            return rezult;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
        /*    if (!is_registered)
            {
                Application.Exit();

                return;
            }*/
            
            using (var db = new BaseEntities())
            {
                var user = db.Users.FirstOrDefault(w => w.Name == UserIDEdit.Text && w.Pass == passtextEdit.Text);
                if (user != null)
                {
                    user.LastLogin = DBHelper.ServerDateTime();
                    user.IsOnline = true;
                    db.SaveChanges();

                    this.Hide();
                    mainForm.main_form = new mainForm((int)UserIDEdit.EditValue);
                    mainForm.main_form.Show();
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

        private void passtextEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !String.IsNullOrEmpty(passtextEdit.Text))
            {
                OkButton.PerformClick();
            }

        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            passtextEdit.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
