using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using update.Properties;

namespace update
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();

            var sp_sklad = Path.Combine(Application.StartupPath, "SP_Sklad.exe");
            //    File.Copy(Path.Combine(Settings.Default.NewVersionPatch, "SP_Sklad.exe"), sp_sklad, true);
            CopyList();

            if (File.Exists(sp_sklad))
            {
                Process.Start(sp_sklad); 
            }
        }

        private void CopyList()
        {
            var list = File.ReadAllLines(Path.Combine(Settings.Default.NewVersionPatch, "update.txt"));
            int a = 0;
            foreach (var i in list)
            {
                var dest_file = Path.Combine(Application.StartupPath, i);
                var source_file = Path.Combine(Settings.Default.NewVersionPatch, i);
                if (File.Exists(source_file))
                {
                    File.Copy(source_file, dest_file, true);
                }
                progressBar1.Value = (++a * 100) / list.Count();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (AutoUpdateCheckEdit.Checked)
            {
                Close();
            }
        }
    }
}
