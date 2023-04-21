using System;
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
using SP_Sklad.Common;

namespace SP_Sklad.ViewsForm
{
    public partial class frmProductionMonitor : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }

        public frmProductionMonitor()
        {
            InitializeComponent();
            _db = DB.SkladBase();
        }

        private void frmKaGroup_Load(object sender, EventArgs e)
        {
            ProductionMonitorBS.DataSource = _db.v_ProductionMonitor.AsNoTracking().OrderBy(o=> o.TechProcessStartDate).ToList();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            ProductionMonitorBS.DataSource = _db.v_ProductionMonitor.AsNoTracking().OrderBy(o => o.TechProcessStartDate).ToList();
        }

        private void ProductionMonitorGridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            
            if( e.Column.FieldName == "Duration")
            {
                var tick = (long)e.CellValue;

                e.DisplayText = new TimeSpan(tick).ToString();
            }
        }

        private void ProductionMonitorGridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            var pm_row = ProductionMonitorGridView.GetRow(e.RowHandle) as v_ProductionMonitor;


            if (pm_row != null && pm_row.Pct > 50 )
            {
                e.Appearance.ForeColor = Color.Red;
            }

            if (pm_row != null && pm_row.Pct > 99)
            {
                e.Appearance.ForeColor = Color.Green;
            }
        }

        private void frmProductionMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            UserSession.production_monitor_frm = null;
        }
    }
}