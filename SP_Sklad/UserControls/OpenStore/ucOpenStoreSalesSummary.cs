using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.EditForm;
using SP_Sklad.Common;
using DevExpress.XtraTreeList;
using System.IO;
using System.Diagnostics;
using SP_Sklad.Properties;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using SkladEngine.DBFunction;
using SP_Sklad.WBForm;
using DevExpress.XtraGrid;
using OpenStore.Tranzit.Base;

namespace SP_Sklad.UserControls.Warehouse
{
    public partial class ucOpenStoreSalesSummary : DevExpress.XtraEditors.XtraUserControl
    {
      
        public ucOpenStoreSalesSummary()
        {
            InitializeComponent();
        }

        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                var user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());
                
                var tt = new BaseEntities().EmployeeKagent.Where(w => w.EmployeeId == DBHelper.CurrentUser.KaId).Select(s => s.Kagent1.OpenStoreAreaId).ToList();
                if (tt.Any())
                {
                    KagentList.Properties.DataSource = (new List<SAREA>() { new SAREA { SAREAID = -1, SAREANAME = "Усі" } }.Concat(new Tranzit_OSEntities().SAREA.Where(w=> tt.Contains(w.SAREAID)).ToList())).ToList();
                }
                else
                {
                    KagentList.Properties.DataSource = (new List<SAREA>() { new SAREA { SAREAID = -1, SAREANAME = "Усі" } }.Concat(new Tranzit_OSEntities().SAREA.ToList())).ToList();
                }
                KagentList.EditValue = -1;

                SalesGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

                PeriodComboBoxEdit.SelectedIndex = 1;
            }
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


        public void GetData(bool restore = true)
        {
            var start_date = wbStartDate.DateTime.ToString("yyyyMMddHHmmss");
            var end_date = wbEndDate.DateTime.ToString("yyyyMMddHHmmss");

            var area = KagentList.GetSelectedDataRow() as SAREA;

            if (area == null)
            {
                return;
            }

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

            var list = objectContext.Database.SqlQuery<SalesSummaryView>(string.Format(sql, start_date, end_date, area.SAREAID, tt.Any() ? $"and SAREAID in ({string.Join(", ", tt)})"  : ""  )).ToList();
            SalesGridControl.DataSource = list;
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetData();
        }

        private void barButtonItem11_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(SalesGridControl);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
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

            GetData();
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbStartDate.ContainsFocus)
            {
                GetData();
            }
        }

        private void wbEndDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbEndDate.ContainsFocus)
            {
                GetData();
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SalesGridView.OptionsBehavior.AlignGroupSummaryInGroupRow == DevExpress.Utils.DefaultBoolean.Default)
            {
                SalesGridView.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                SalesGridView.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.Default;
            }
        }

        private void KagentList_EditValueChanged(object sender, EventArgs e)
        {
            if (KagentList.ContainsFocus)
            {
                GetData();
            }
        }
    }
}
