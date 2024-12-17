using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using SP_Sklad.SkladData;
using SP_Sklad.EditForm;
using SP_Sklad.Common;
using OpenStore.Tranzit.Base;

namespace SP_Sklad.Interfaces.Tablet.UI
{
    public partial class ucTabletOpenStorePaymentsSummary : DevExpress.XtraEditors.XtraUserControl
    {
      
        public ucTabletOpenStorePaymentsSummary()
        {
            InitializeComponent();
            windowsUIButtonPanel.BackColor = new Color();
        }

        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {

                var user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());

                PaymentGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

                PeriodComboBoxEdit.SelectedIndex = 1;
            }
        }

        public class PaymentsSummaryView
        {
            public string SAREANAME { get; set; }
            public int SAREAID { get; set; }
            public Nullable<decimal> Total { get; set; }
            public Nullable<decimal> TotalNotFiscalReceipt { get; set; }
            public Nullable<decimal> TotalFiscalReceipt { get; set; }
            public Nullable<decimal> TotalCash { get; set; }
            public Nullable<decimal> TotalCashless { get; set; }
            public Nullable<decimal> TotalCashNotFiscalReceipt { get; set; }
        }


        public void GetData(bool restore = true)
        {
            var start_date = wbStartDate.DateTime.ToString("yyyyMMddHHmmss");
            var end_date = wbEndDate.DateTime.ToString("yyyyMMddHHmmss");
            var tt = new BaseEntities().EmployeeKagent.Where(w => w.EmployeeId == DBHelper.CurrentUser.KaId && w.Kagent1.OpenStoreAreaId != null).Select(s => s.Kagent1.OpenStoreAreaId).ToList();

            Tranzit_OSEntities objectContext = new Tranzit_OSEntities();

            var sql = @"SELECT 
       [SAREAID]
      ,[SAREANAME]
      ,sum([Price]) Total
	  ,sum(case when [FiscalReceipt] = 0 then [Total] else 0 end) TotalNotFiscalReceipt
      ,sum(case when [FiscalReceipt] = 1 then [Total] else 0 end) TotalFiscalReceipt
	  ,sum(case when SALESTYPE = 0 then [Total] else 0 end) TotalCash
	  ,sum(case when SALESTYPE = 1 then [Total] else 0 end) TotalCashless
	  ,sum(case when SALESTYPE = 4 then [Total] else 0 end) TotalCashNotFiscalReceipt
  FROM [v_Payment]
  where [SALESTIME]  between '{0}' and '{1}' and SALESTYPE in (0,1,4) {2}
  group by [SAREAID], [SAREANAME] ";

            var list = objectContext.Database.SqlQuery<PaymentsSummaryView>(string.Format(sql, start_date, end_date, tt.Any() ? $"and SAREAID in ({string.Join(", ", tt)})" : "")).ToList();
            PaymentGridControl.DataSource = list;
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


        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            if (e.Button.Properties.VisibleIndex == 0)
            {
                GetData();
            }
            else if (e.Button.Properties.VisibleIndex == 1)
            {
                IHelper.ExportToXlsx(PaymentGridControl);
            }
        }
    }
}
