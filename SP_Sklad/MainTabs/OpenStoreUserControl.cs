using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.XtraReports.UI;
using OpenStore.Tranzit.Base;
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
            DirTreeList.ExpandAll();

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
                       }
            if (e.Node.Id == 2 )
            {
                mainContentTab.SelectedTabPageIndex = 3;
                
            }

            if (e.Node.Id == 3)
            {
                mainContentTab.SelectedTabPageIndex = 4;
                
            }
            if (e.Node.Id == 6)
            {
                mainContentTab.SelectedTabPageIndex = 5;
                
            }

            if (e.Node.Id == 5)
            {
                mainContentTab.SelectedTabPageIndex = 6;
                
            }

            if (e.Node.Id == 8)
            {
                mainContentTab.SelectedTabPageIndex = 7;
                using (var db = new Tranzit_OSEntities())
                {
                    MatGridControl.DataSource = db.ART.AsNoTracking().Where(w => w.DELFLAG == 0).Select(s => new
                    {
                        s.ARTID,
                        s.ARTCODE,
                        s.ARTNAME,
                        s.ARTSNAME,
                        s.GRP.GRPNAME
                    }).ToList();

                    MatGridView.ExpandAllGroups();
                }
            }

            if (e.Node.Id == 9)
            {
                mainContentTab.SelectedTabPageIndex = 8;
                using (var db = new Tranzit_OSEntities())
                {
                    gridControl1.DataSource = db.SAREA.AsNoTracking().Where(w => w.DELFLAG == 0).ToList();
                }
            }
        }
    }
}
