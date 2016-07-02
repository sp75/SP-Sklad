using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.SkladData;

namespace SP_Sklad.MainTabs
{
    public partial class ReportUserControl : UserControl
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
              //      repositoryItemLookUpEdit1.DataSource = DBHelper.WhList();

                    DirTreeList.DataSource = db.GetReportTree(DBHelper.CurrentUser.UserId).ToList();
                    DirTreeList.ExpandToLevel(1);
                }
            }
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as GetReportTree_Result;
            using (var db = DB.SkladBase())
            {
                RepGridControl.DataSource = db.Reports.Where(w => w.GrpId == focused_tree_node.Id)
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
            dynamic row = RepGridView.GetFocusedRow();
       
            RepBtn.Enabled = false;
            switch ((int)row.RepId)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 26:
                case 8:
                case 5:
                case 6:
                case 7:
                case 9:
                case 10:
                case 11:
                case 13:
                case 18:
                case 27:
                case 25:
                case 14:
                case 19:
                case 20:
                case 16:
                case 17:
                case 23:
                case 28:
                case 29:
                case 30:
                    RepBtn.Enabled = true;
                    break;
            }
        }

        private void RepBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dynamic row = RepGridView.GetFocusedRow();
            var frm = new frmReport((int)row.RepId);
         
            switch ((int)row.RepId)
            {
                case 1:
                    frm.OutDocGroupBox.Visible = false;
                    frm.OnDateGroupBox.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

             /*   case 2:
                    frmReport->InDocGroupBox->Visible = false;
                    frmReport->OnDateGroupBox->Visible = false;
                    frmReport->MatGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    break;

                case 3: frmReport->DocTypeGroupBox2->Visible = false;
                case 14:
                    frmReport->InDocGroupBox->Visible = false;
                    frmReport->OnDateGroupBox->Visible = false;
                    frmReport->MatGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    break;

                case 4: frmReport->DocTypeGroupBox2->Visible = false;
                case 25:
                    frmReport->OutDocGroupBox->Visible = false;
                    frmReport->OnDateGroupBox->Visible = false;
                    frmReport->MatGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    break;

                case 26: frmReport->OnDateGroupBox->Visible = false;
                    frmReport->KAGroupBox->Visible = false;
                    frmReport->MatGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox2->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    break;

                case 30:
                case 8: frmReport->OnDateGroupBox->Visible = false;
                    frmReport->WHGroupBox->Visible = false;
                    frmReport->GRPGroupBox->Visible = false;
                    frmReport->MatGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox2->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    break;

                case 5:
                case 6: frmReport->KAGroupBox->Visible = false;
                    frmReport->PeriodGroupBox->Visible = false;
                    frmReport->WHGroupBox->Visible = false;
                    frmReport->GRPGroupBox->Visible = false;
                    frmReport->KAGroupBox->Visible = false;
                    frmReport->MatGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox2->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    break;

                case 18:
                case 7: frmReport->PeriodGroupBox->Visible = false;
                    frmReport->KAGroupBox->Visible = false;
                    frmReport->MatGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox2->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    break;

                case 9:
                case 19: frmReport->OnDateGroupBox->Visible = false;
                    frmReport->GRPGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox2->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    break;

                case 10: frmReport->DocTypeGroupBox2->Visible = false;
                    frmReport->OnDateGroupBox->Visible = false;
                    frmReport->MatGroupBox->Visible = false;
                    frmReport->KAGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    break;

                case 11: frmReport->DocTypeGroupBox2->Visible = false;
                    frmReport->KAGroupBox->Visible = false;
                    frmReport->OnDateGroupBox->Visible = false;
                    frmReport->WHGroupBox->Visible = false;
                    frmReport->GRPGroupBox->Visible = false;
                    frmReport->KAGroupBox->Visible = false;
                    frmReport->MatGroupBox->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    break;

                case 13: frmReport->DocTypeGroupBox2->Visible = false;
                    frmReport->OnDateGroupBox->Visible = false;
                    frmReport->MatGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    break;

                case 27: frmReport->OnDateGroupBox->Visible = false;
                    frmReport->WHGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox2->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    break;

                case 20:
                    frmReport->DocTypeGroupBox2->Visible = false;
                    frmReport->OnDateGroupBox->Visible = false;
                    frmReport->MatGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    frmReport->WHGroupBox->Visible = false;
                    frmReport->GRPComboBox->Properties->ListSource = SkladData->SvcGroupComboBoxDS;
                    frmReport->CHARGEGroupBox->Visible = false;
                    break;

                case 16:
                    frmReport->DocTypeGroupBox2->Visible = false;
                    frmReport->OnDateGroupBox->Visible = false;
                    frmReport->MatGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    frmReport->WHGroupBox->Visible = false;
                    frmReport->GRPGroupBox->Visible = false;
                    break;

                case 17:
                case 23: frmReport->DocTypeGroupBox2->Visible = false;
                    frmReport->KAGroupBox->Visible = false;
                    frmReport->OnDateGroupBox->Visible = false;
                    frmReport->WHGroupBox->Visible = false;
                    frmReport->GRPGroupBox->Visible = false;
                    frmReport->KAGroupBox->Visible = false;
                    frmReport->MatGroupBox->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    break;

                case 28: frmReport->OnDateGroupBox->Visible = false;
                    frmReport->WHGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox2->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    break;

                case 29:
                    frmReport->OutDocGroupBox->Visible = false;
                    frmReport->OnDateGroupBox->Visible = false;
                    frmReport->MatGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox->Visible = false;
                    frmReport->CHARGEGroupBox->Visible = false;
                    frmReport->DocTypeGroupBox2->Visible = false;
                    break;*/
            }

            frm.Text = row.Name;
            frm.ShowDialog();

        }

        private void RepGridView_DoubleClick(object sender, EventArgs e)
        {
            RepBtn.PerformClick();
        }
    }
}
