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
         /*   if ((int)e.Node.Tag == 0)
            {
                mainContentTab.SelectedTabPageIndex = 1;
                ucOpenStoreSales.GetData();
            }*/
        }
    }
}
