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
    public partial class ucOpenStorePaymentsSummary : DevExpress.XtraEditors.XtraUserControl
    {
      
        public ucOpenStorePaymentsSummary()
        {
            InitializeComponent();
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

            Tranzit_OSEntities objectContext = new Tranzit_OSEntities();

            var sql = @"SELECT 
       [SAREAID]
      ,[SAREANAME]
      ,sum([Price]) Total
	  ,sum(case when [FiscalReceipt] = 0 then [Price] else 0 end) TotalNotFiscalReceipt
      ,sum(case when [FiscalReceipt] = 1 then [Price] else 0 end) TotalFiscalReceipt
	  ,sum(case when SALESTYPE = 0 then [Price] else 0 end) TotalCash
	  ,sum(case when SALESTYPE = 1 then [Price] else 0 end) TotalCashless
	  ,sum(case when SALESTYPE = 4 then [Price] else 0 end) TotalCashNotFiscalReceipt
  FROM [v_Payment]
  where [SALESTIME]  between '{0}' and '{1}' and SALESTYPE in (0,1,4)
  group by [SAREAID], [SAREANAME] ";

            var list = objectContext.Database.SqlQuery<PaymentsSummaryView>(string.Format(sql, start_date, end_date)).ToList();
            PaymentGridControl.DataSource = list;
        }


        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetData();
        }

        private void barButtonItem11_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(PaymentGridControl);
        }



        public void FocusRow(GridView view, int rowHandle)
        {
    /*        view.TopRowIndex = prev_top_row_index == -1 ? rowHandle : prev_top_row_index;
            view.FocusedRowHandle = rowHandle;
            view.SelectRow(rowHandle);*/
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
            if (PaymentGridView.OptionsBehavior.AlignGroupSummaryInGroupRow == DevExpress.Utils.DefaultBoolean.Default)
            {
                PaymentGridView.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                PaymentGridView.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.Default;
            }
        }
    }
}
