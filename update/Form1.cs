using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using update.Properties;
using System.IO.Compression;
using System.Threading;

namespace update
{
    public partial class Form1 : Form
    {
        private bool is_web_dowload { get; set; }
        public Form1(string[] args)
        {
            InitializeComponent();
            if (args.Any())
            {
                is_web_dowload = bool.Parse(args[0].ToString());
            }
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();

            var sp_sklad = Path.Combine(Application.StartupPath, "SP_Sklad.exe");

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

        private void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }



        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            button3.Enabled = true;
            button1.Enabled = true;

            string zipPath = Path.Combine(Application.StartupPath, "SP_Sklad_upd.zip");

            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
            {
                //    archive.ExtractToDirectory(Application.StartupPath);
                foreach (ZipArchiveEntry file in archive.Entries)
                {
                    string completeFileName = Path.Combine(Application.StartupPath, file.FullName);
                    if (file.Name != "")
                    {
                        try
                        {
                            file.ExtractToFile(completeFileName, true);
                        }
                        catch { }
                    }
                }
            }

            Close();
        }

        private void WebCopy()
        {
            is_web_dowload = true;

            button3.Enabled = false;
            button1.Enabled = false;
            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompleted);

                string zipPath = Path.Combine(Application.StartupPath, "SP_Sklad_upd.zip");

                //      client.DownloadFile(Settings.Default.NewVersionURL+ "update.txt", Path.Combine(Application.StartupPath, "update_web.txt"));
                Uri uri = new Uri(Settings.Default.NewVersionURL + "SP_Sklad_upd.zip");

                client.DownloadFileAsync(uri, zipPath);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            CopyList();
            Close();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (AutoUpdateCheckEdit.Checked && !is_web_dowload)
            {
                CopyList();
                Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WebCopy();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (is_web_dowload)
            {
                WebCopy();
            }
        }
    }
}
