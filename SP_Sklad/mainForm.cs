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
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            CurDateEditBarItem.EditValue = DateTime.Now;
            
            using (var db  = new BaseEntities())
            {
               docsUserControl1.OnLoad();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }
    }

    public enum EditWBOptions
    {
        New = 1,
        Edit = 2
    }
}
