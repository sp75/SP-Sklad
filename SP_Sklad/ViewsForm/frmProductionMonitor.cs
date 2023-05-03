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
using DevExpress.Data;

namespace SP_Sklad.ViewsForm
{
    public partial class frmProductionMonitor : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }

        public frmProductionMonitor()
        {
            InitializeComponent();
            _db = DB.SkladBase();


            if (!ShellHelper.IsApplicationShortcutExist("SP-Sklad"))
            {
                ShellHelper.TryCreateShortcut(
                    applicationId: toastNotificationsManager1.ApplicationId,
                    name: "SP-Sklad");
            }
        }

        private void frmKaGroup_Load(object sender, EventArgs e)
        {
            GetData();
            ShowNotification();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            var list = _db.v_ProductionMonitor.AsNoTracking().OrderByDescending(o => o.Pct).ToList();
            ProductionMonitorBS.DataSource = list;
        }

        private void ShowNotification()
        {
            if (ProductionMonitorBS.Count > 0)
            {
                var list = ProductionMonitorBS.DataSource as List<v_ProductionMonitor>;

                foreach (var item in list)
                {
                    if (item.Pct > 80 && item.Pct < 100)
                    {
                        toastNotificationsManager1.Notifications[0].Header = item.TechProcessName + " " + item.Pct?.ToString("0") + "%";
                        toastNotificationsManager1.Notifications[0].Body = "Рецепт: " + item.MatName;
                        toastNotificationsManager1.Notifications[0].Body2 = "Рама: " + item.RamaName;
                        toastNotificationsManager1.ShowNotification("1a7e37b8-6aba-4e9c-9a03-28d08312b44e");
                    }
                }
            }
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


            if (pm_row != null && pm_row.Pct > 80 )
            {
                e.Appearance.ForeColor = Color.Orange;
              //e.Appearance.BackColor = Color.Orange;
            }

            if (pm_row != null && pm_row.Pct > 99)
            {
                   e.Appearance.ForeColor = Color.Green;
             //   e.Appearance.BackColor = Color.Green;
            }
        }

        private void frmProductionMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            UserSession.production_monitor_frm = null;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ShowNotification();
        }
    }
}