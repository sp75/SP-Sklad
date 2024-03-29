﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.SkladData;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;

namespace SP_Sklad.ViewsForm
{
    public partial class frmKagentMaterilPrices : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        public KontragentGroup focused_row
        {
            get
            {
                return (KagentMaterilPricesGridView.GetFocusedRow() as KontragentGroup);
            }
        }

        public frmKagentMaterilPrices()
        {
            InitializeComponent();
            _db = DB.SkladBase();
        }

         private void KagentMaterilPricesSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            e.QueryableSource =  _db.v_KagentMaterilPrices.AsQueryable();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            using (MemoryStream ms_xlsx = new MemoryStream())
            {
                KagentMaterilPricesGridView.ExportToXlsx(ms_xlsx);
                new frmSpreadsheed(ms_xlsx.ToArray()).Show();
            }
        }

        private void KagentMaterilPricesGridView_AsyncCompleted(object sender, EventArgs e)
        {
            KagentMaterilPricesGridView.ExpandAllGroups();
        }
    }
}