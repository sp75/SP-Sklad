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
using System.Data.Entity;
using SP_Sklad.Common;

namespace SP_Sklad
{
    public partial class mainForm : Form
    {
        public static int user_id { get; set; }

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            CurDateEditBarItem.EditValue = DateTime.Now;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var want_to_exit = MessageBox.Show(
               @"Do you really want close the program?",
               @"Close application",
               MessageBoxButtons.YesNo) == DialogResult.Yes;

            e.Cancel = !want_to_exit;
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }

    public enum EditWBOptions
    {
        New = 1,
        Edit = 2
    }
}
