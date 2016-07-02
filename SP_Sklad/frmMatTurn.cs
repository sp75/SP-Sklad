﻿using System;
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
using SP_Sklad.Common;
using SP_Sklad.SkladData;

namespace SP_Sklad
{
    public partial class frmMatTurn : Form
    {
        public int _mat_id { get; set; }

        public frmMatTurn(int mat_id)
        {
            InitializeComponent();
            _mat_id = mat_id;
        }

        private void frmMatTurn_Load(object sender, EventArgs e)
        {
            wTypeList.Properties.DataSource = new List<object>() { new { Id = 0, Name = "Усі" } }.Concat(new BaseEntities().DocType.Select(s => new { s.Id, s.Name })).ToList();
            wTypeList.EditValue = 0;
            KAgentEdit.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().Kagent.Select(s => new { s.KaId, s.Name }));
            KAgentEdit.EditValue = 0;

            wbStartDate.DateTime = DateTimeDayOfMonthExtensions.FirstDayOfMonth(DateTime.Now);
            wbEndDate.DateTime = DateTime.Now.AddDays(1);

            this.Text = "Рух товару: " + new BaseEntities().Materials.Find(_mat_id).Name;
        }

        private void GetTurns()
        {
            DocListBindingSource.DataSource = DB.SkladBase().GetMatMove(_mat_id, wbStartDate.DateTime, wbEndDate.DateTime, 0, (int)KAgentEdit.EditValue, (int)wTypeList.EditValue, "*").ToList();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var path = Path.Combine(Application.StartupPath, "expotr.pdf");
            gridControl1.ExportToPdf(path);

            Process.Start(path);
        }

        private void bandedGridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            popupMenu1.ShowPopup(Control.MousePosition);
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wTypeList.ContainsFocus || wbStartDate.ContainsFocus || wbEndDate.ContainsFocus || KAgentEdit.ContainsFocus)
            {
                GetTurns();
            }
        }
    }
}