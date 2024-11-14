using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.ComponentModel.DataAnnotations;
using SP.Reports;
using System.IO;
using System.Collections;
using SP_Sklad.SkladData;
using OpenStore.Tranzit.Base;
using SP_Sklad.Common;

namespace SP_Sklad.Interfaces.Tablet.UI
{
    public partial class ucTabletOpenStoreSales : DevExpress.XtraEditors.XtraUserControl
    {
        public ucTabletOpenStoreSales()
        {
            InitializeComponent();

            windowsUIButtonPanel.BackColor = new Color();
        }

        void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            if (e.Button.Properties.VisibleIndex == 0)
            {
                RefreshGrid();
            }
            else if(e.Button.Properties.VisibleIndex == 1)
            {
                IHelper.ExportToXlsx(gridControl1);
            }

        }

        private List<SalesSummaryView> GetDataSource()
        {
            var start_date = wbStartDate.DateTime.ToString("yyyyMMddHHmmss");
            var end_date = wbEndDate.DateTime.ToString("yyyyMMddHHmmss");

            var area = KagentList.GetSelectedDataRow() as SAREA;

           /* if (area == null)
            {
                return;
            }*/

            var tt = new BaseEntities().EmployeeKagent.Where(w => w.EmployeeId == DBHelper.CurrentUser.KaId).Select(s => s.Kagent1.OpenStoreAreaId).ToList();

            Tranzit_OSEntities objectContext = new Tranzit_OSEntities();

            var sql = @"SELECT [SAREANAME]
      ,[SAREAID]
      ,avg([PRICE]) Price
      ,sum([AMOUNT]) Amount
      ,sum([TOTAL] ) Total
      ,[UNITNAME]
      ,[ARTNAME]
      ,[ARTID]
      ,[ARTCODE]
      ,[GRPID]
      ,[GRPNAME]
  FROM [Tranzit_OS].[dbo].[v_Sales]
  where (SAREAID = {2} or {2} = -1) and [SALESTIME]  between '{0}' and '{1}' {3}
  group by [SAREANAME]
      ,[SAREAID],[UNITNAME]
      ,[ARTNAME]
      ,[ARTID]
      ,[ARTCODE]
      ,[GRPID]
      ,[GRPNAME]";

            var list = objectContext.Database.SqlQuery<SalesSummaryView>(string.Format(sql, start_date, end_date, area.SAREAID, tt.Any() ? $"and SAREAID in ({string.Join(", ", tt)})" : "")).ToList();

            return list;
        }

        public class SalesSummaryView
        {
            public string SAREANAME { get; set; }
            public int SAREAID { get; set; }
            public Nullable<decimal> Total { get; set; }
            public Nullable<decimal> Price { get; set; }
            public Nullable<decimal> Amount { get; set; }
            public string UNITNAME { get; set; }
            public string ARTNAME { get; set; }
            public int ARTCODE { get; set; }
            public int GRPID { get; set; }
            public string GRPNAME { get; set; }
        }


        public void RefreshGrid()
        {
            var h = gridView1.FocusedRowHandle;
            var top_r = gridView1.TopRowIndex;

            gridControl1.DataSource = GetDataSource();
            gridView1.ExpandAllGroups();

            gridView1.TopRowIndex = top_r;
            gridView1.FocusedRowHandle = h;
        }

        private void ucCurrentReturned_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                var tt = new BaseEntities().EmployeeKagent.Where(w => w.EmployeeId == DBHelper.CurrentUser.KaId).Select(s => s.Kagent1.OpenStoreAreaId).ToList();
                if (tt.Any())
                {
                    KagentList.Properties.DataSource = (new List<SAREA>() { new SAREA { SAREAID = -1, SAREANAME = "Усі" } }.Concat(new Tranzit_OSEntities().SAREA.Where(w => tt.Contains(w.SAREAID)).ToList())).ToList();
                }
                else
                {
                    KagentList.Properties.DataSource = (new List<SAREA>() { new SAREA { SAREAID = -1, SAREANAME = "Усі" } }.Concat(new Tranzit_OSEntities().SAREA.ToList())).ToList();
                }
                KagentList.EditValue = -1;

                PeriodComboBoxEdit.SelectedIndex = 1;

                var user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());
                gridView1.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
            }
        }
        private void PeriodComboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            wbEndDate.DateTime = DateTime.Now.Date.SetEndDay();
            switch (PeriodComboBoxEdit.SelectedIndex)
            {
                case 1:
                    wbStartDate.DateTime = DateTime.Now.Date;
                    break;

                case 2:
                    wbStartDate.DateTime = DateTime.Now.Date.StartOfWeek(DayOfWeek.Monday);
                    break;

                case 3:
                    wbStartDate.DateTime = DateTime.Now.Date.FirstDayOfMonth();
                    break;

                case 4:
                    wbStartDate.DateTime = new DateTime(DateTime.Now.Year, 1, 1);
                    break;
            }

            RefreshGrid();
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbStartDate.ContainsFocus)
            {
                RefreshGrid();
            }
        }

        private void wbEndDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbEndDate.ContainsFocus)
            {
                RefreshGrid();
            }
        }

        private void KagentList_EditValueChanged(object sender, EventArgs e)
        {
            if (KagentList.ContainsFocus)
            {
                RefreshGrid();
            }
        }
    }
}
