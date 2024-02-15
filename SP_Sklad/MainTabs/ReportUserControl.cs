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
    public partial class ReportUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        GetReportTree_Result focused_tree_node { get; set; }
        public RepView report_row => RepGridView.GetFocusedRow() as RepView;

        public ReportUserControl()
        {
            InitializeComponent();
        }

        private void ReportUserControl_Load(object sender, EventArgs e)
        {
            mainContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            if (!DesignMode)
            {

                using (var db = new BaseEntities())
                {
                    DirTreeList.DataSource = db.GetReportTree(DBHelper.CurrentUser.UserId).ToList();
                    DirTreeList.ExpandToLevel(1);
                }
            }
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as GetReportTree_Result;
            if (focused_tree_node == null)
            {
                return;
            }

            using (var db = DB.SkladBase())
            {
                RepGridControl.DataSource = null;
                RepGridControl.DataSource = db.Reports.Where(w => w.GrpId == focused_tree_node.Id && w.Fil == 1)
                .Join(db.RepLng.Where(r => r.LangId == 2), rep => rep.RepId, lng => lng.RepId, (rep, lng) => new RepView
                {
                    ImgIndex = 23,
                    RepId = rep.RepId,
                    Name = lng.Name,
                    Notes = lng.Notes,
                    Num = rep.Num
                }).OrderBy(o => o.Num).ToList();
            }
        }
        public class RepView
        {
            public int ImgIndex { get; set; }
            public int RepId { get; set; }
            public string Name { get; set; }
            public string Notes { get; set; }
            public int? Num { get; set; }
        }

        private void RepGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void RepBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dynamic row = RepGridView.GetFocusedRow() as RepView;
            string report_name = DB.SkladBase().RepLng.Where(w => w.LangId == 2 && w.RepId == report_row.RepId).Select(s => s.Name).FirstOrDefault();

            if(row == null)
            {
                return;
            }

            if(row.RepId == 51)
            {
                new frmReport51().ShowDialog();
            }
            else if (row.RepId == 53)
            {
                new frmReport53().ShowDialog();
            }
            else if (row.RepId == 54)
            {
                new frmReport54().ShowDialog();
            }
            else if (row.RepId == 55)
            {
                XtraReport55 report = new XtraReport55();

                var tool = new ReportPrintTool(report);
                tool.ShowPreview();
            }
            else
            {

                using (var frm = new frmReport((int)row.RepId))
                {
                    frm.Text = row.Name;
                    frm.ShowDialog();
                }
            }

        }

        private void RepGridView_DoubleClick(object sender, EventArgs e)
        {
            RepBtn.PerformClick();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
         
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }
    }
}
