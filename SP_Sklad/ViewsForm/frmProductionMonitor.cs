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

namespace SP_Sklad.ViewsForm
{
    public partial class frmProductionMonitor : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        public KontragentGroup focused_row
        {
            get
            {
                return (ProductionMonitorGridView.GetFocusedRow() as KontragentGroup);
            }
        }

        public frmProductionMonitor()
        {
            InitializeComponent();
            _db = DB.SkladBase();
        }

        private void frmKaGroup_Load(object sender, EventArgs e)
        {
            ProductionMonitorBS.DataSource = _db.v_ProductionMonitor.AsNoTracking().ToList();
        }

        private void WhRemainGridView_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
           
        }

        private void frmKaGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
         
        }

        private void KontragentGroupBS_AddingNew(object sender, AddingNewEventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ProductionMonitorBS.DataSource = _db.v_ProductionMonitor.AsNoTracking().ToList();
        }

        private void ProductionMonitorGridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            
            if( e.Column.FieldName == "Duration")
            {
                var tick = (long)e.CellValue;

                e.DisplayText = new TimeSpan(tick).ToString();
            }
        }
    }
}