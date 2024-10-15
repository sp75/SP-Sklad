using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.XtraReports.UI;
using SP_Sklad.Reports;
using SP_Sklad.Reports.XtraRep;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;

namespace SP_Sklad.MainTabs
{
    public partial class OpenStoreUserControl : DevExpress.XtraEditors.XtraUserControl
    {

        public OpenStoreUserControl()
        {
            InitializeComponent();
        }

        private void ReportUserControl_Load(object sender, EventArgs e)
        {
            mainContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            if (!DesignMode)
            {

           /*     using (var db = new BaseEntities())
                {
                    DirTreeList.DataSource = db.GetReportTree(DBHelper.CurrentUser.UserId).ToList();
                    DirTreeList.ExpandToLevel(1);
                }*/
            }
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node.Id == 0)
            {
                mainContentTab.SelectedTabPageIndex = 1;
            //    ucOpenStoreSales.GetData();
            }

            if (e.Node.Id == 1)
            {
                mainContentTab.SelectedTabPageIndex = 2;
           //     ucOpenStorePayments.GetData();
            }
            if (e.Node.Id == 2 )
            {
                mainContentTab.SelectedTabPageIndex = 3;
                //     ucOpenStorePayments.GetData();
            }

            if (e.Node.Id == 3)
            {
                mainContentTab.SelectedTabPageIndex = 4;
                //     ucOpenStorePayments.GetData();
            }
            if (e.Node.Id == 4)
            {
                mainContentTab.SelectedTabPageIndex = 5;
                //     ucOpenStorePayments.GetData();
            }

        }
    }
}
