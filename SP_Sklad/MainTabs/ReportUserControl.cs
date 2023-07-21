using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;

namespace SP_Sklad.MainTabs
{
    public partial class ReportUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        GetReportTree_Result focused_tree_node { get; set; }

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
                .Join(db.RepLng.Where(r => r.LangId == 2), rep => rep.RepId, lng => lng.RepId, (rep, lng) => new
            {
                ImgIndex = 23,
                rep.RepId,
                lng.Name,
                lng.Notes,
                rep.Num
            }).OrderBy(o => o.Num).ToList();
            }
        }

        private void RepGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        private void RepBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dynamic row = RepGridView.GetFocusedRow();

            if(row == null)
            {
                return;
            }

            if ((int)row.RepId == 53)
            {
                new frmReport53().ShowDialog();
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
            new frmReport51().ShowDialog();
        }
    }
}
