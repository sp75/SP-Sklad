using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.Skins;
using DevExpress.UserSkins;

namespace SP_Sklad
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            Application.Run(new frmLogin());
        }
    }
}
