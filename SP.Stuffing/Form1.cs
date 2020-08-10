using SP.Base;
using SP.Base.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP.Stuffing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var satrt_date = DateTime.Now.AddDays(-1000);
            var end_date = DateTime.Now.AddYears(100);

            int top_row = WbGridView.TopRowIndex;
            using (SPBaseModel _db = Database.SPBase())
            {
                WBListMakeBS.DataSource = _db.WBListMake(satrt_date, end_date, 2,"*", 0, -20).ToList();
                //WBGridControl.DataSource = _db.WBListMake(satrt_date, end_date, -1, "*", 0, -20).ToList();
            }
            WbGridView.TopRowIndex = top_row;
        }

        private void WbGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var focused_row = e.Row as WBListMake_Result;

            TechProcDetBS.DataSource = null;
            using (SPBaseModel _db = Database.SPBase())
            {
                TechProcDetBS.DataSource = _db.v_TechProcDet.Where(w => w.WbillId == focused_row.WbillId).OrderBy(o => o.Num).ToList();
            }


        }
    }
}
