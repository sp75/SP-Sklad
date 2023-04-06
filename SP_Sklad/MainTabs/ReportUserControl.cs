﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                    //      repositoryItemLookUpEdit1.DataSource = DBHelper.WhList;

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
          /*  dynamic row = RepGridView.GetFocusedRow();
            if (row == null)
            {
                return;
            }

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
                case 15:
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
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 22:
                case 39:
                case 40:
                case 41:
                case 42:
                    RepBtn.Enabled = true;
                    break;
            }*/
        }

        private void RepBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dynamic row = RepGridView.GetFocusedRow();

            if(row == null)
            {
                return;
            }

            var frm = new frmReport((int)row.RepId);
/*
            switch ((int)row.RepId)
            {
                case 1:
                    frm.OutDocGroupBox.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.DocTypeGroupBox2.Visible = true;
                    break;

                case 2:
                    frm.InDocGroupBox.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.StatusPanel.Visible = true;
                    frm.DocTypeGroupBox2.Visible = true;
                    break;

                case 39:
                case 3:
                    frm.InDocGroupBox.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.GroupKontragentPanel.Visible = true;
                    break;

                case 14:
                    frm.DocTypeGroupBox2.Visible = true;
                    frm.InDocGroupBox.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.GroupKontragentPanel.Visible = true;
                    break;

                case 4:
                    frm.OutDocGroupBox.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 25:
                    frm.DocTypeGroupBox2.Visible = true;
                    frm.OutDocGroupBox.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 26: 
                    frm.KontragentPanel.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 30:
                case 8: 
                    frm.WHGroupBox.Visible = false;
                    frm.GRPGroupBox.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 5:
                case 6:
                    frm.OnDateGroupBox.Visible = true;
                    frm.KontragentPanel.Visible = false;
                    frm.PeriodGroupBox.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    frm.GRPGroupBox.Visible = false;
                    frm.KontragentPanel.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 18:
                case 7:
                    frm.OnDateGroupBox.Visible = true;
                    frm.PeriodGroupBox.Visible = false;
                    frm.KontragentPanel.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 9:
                case 19:
                    frm.GRPGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.GroupKontragentPanel.Visible = true;
                    break;

                case 10:
                    frm.MatGroupBox.Visible = false;
                    frm.KontragentPanel.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 11: 
                    frm.KontragentPanel.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    frm.GRPGroupBox.Visible = false;
                    frm.KontragentPanel.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 13: 
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 15: 
                    frm.GRPGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    break;

                case 16:
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    frm.GRPGroupBox.Visible = false;
                    break;

                case 17:
                case 23: 
              //      frm.KontragentPanel.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    frm.GRPGroupBox.Visible = false;
                    frm.MatGroupBox.Visible = false;
                 //   frm.ChargeGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    break;

                case 20:
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    frm.GrpComboBox.Properties.DataSource = new List<object>() { new { GrpId = 0, Name = "Усі" } }.Concat(new BaseEntities().SvcGroup.Where(w => w.Deleted == 0).Select(s => new { s.GrpId, s.Name }).ToList());
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 28: 
                    frm.WHGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.GroupKontragentPanel.Visible = true;
                    break;

                case 27: 
                    frm.WHGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.GroupKontragentPanel.Visible = true;
                    frm.PersonPanel.Visible = true;
                    break;

                case 29:
                    frm.OutDocGroupBox.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 31: 
                    frm.KontragentPanel.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 32: 
                    frm.GRPGroupBox.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.KontragentPanel.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 33:
                    frm.KontragentPanel.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 34:
                    frm.OnDateGroupBox.Visible = true;
                    frm.PeriodGroupBox.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    frm.GRPGroupBox.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.KontragentPanel.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 35: 
                    frm.KontragentPanel.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 36:
                    frm.OnDateGroupBox.Visible = true;
                    frm.KontragentPanel.Visible = false;
                    frm.PeriodGroupBox.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    frm.GRPGroupBox.Visible = false;
                    frm.KontragentPanel.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 37:
                    frm.KontragentPanel.Visible = false;
                    frm.GRPGroupBox.Visible = false;
                    frm.KontragentPanel.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    break;

                case 38:
                    frm.KontragentPanel.Visible = false;
                    frm.GRPGroupBox.Visible = false;
                    frm.KontragentPanel.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    break;

                case 22:
                    frm.PersonPanel.Visible = true;
                    frm.KontragentPanel.Visible = false;
                    frm.GRPGroupBox.Visible = false;
                    frm.KontragentPanel.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    break;

                case 40:
                    frm.OnDateGroupBox.Visible = true;
                    frm.PersonPanel.Visible = false;
                    frm.GRPGroupBox.Visible = false;
                 //   frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    frm.PeriodGroupBox.Visible = false;
                    break;

                case 41:
                    frm.GroupKontragentPanel.Visible = true;
                    frm.PersonPanel.Visible = false;
                    frm.GRPGroupBox.Visible = false;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    frm.KontragentPanel.Visible = false;
                    break;

                case 42:
                    frm.InDocGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    frm.KontragentPanel.Visible = false;
                    frm.StatusPanel.Visible = true;
                    frm.wmatturnStatusPanel.Visible = true;
                    break;


                case 43:
                    frm.PersonPanel.Visible = false;
                    frm.GRPGroupBox.Visible = false;
                    //   frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.WHGroupBox.Visible = false;
                    frm.GroupKontragentPanel.Visible = true;
                    frm.KontragentPanel.Visible = false;
                    break;

                case 44:
                    frm.DocTypeGroupBox2.Visible = true;
                    frm.MatGroupBox.Visible = false;
                    frm.DocTypeGroupBox.Visible = false;
                    frm.ChargeGroupBox.Visible = false;
                    frm.StatusPanel.Visible = true;
                    frm.GRPGroupBox.Visible = false;
                    break;
            }*/

            frm.Text = row.Name;
            frm.ShowDialog();

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
