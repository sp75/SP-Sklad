using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.EditForm;
using SP_Sklad.Common;

namespace SP_Sklad.MainTabs
{
    public partial class ServiceUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        GetServiceTree_Result focused_tree_node { get; set; }

        public ServiceUserControl()
        {
            InitializeComponent();
        }

        private void ServiceUserControl_Load(object sender, EventArgs e)
        {
            mainContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            if (!DesignMode)
            {
                using (var db = new BaseEntities())
                {
                    //      repositoryItemLookUpEdit1.DataSource = DBHelper.WhList();

                    DirTreeList.DataSource = db.GetServiceTree(DBHelper.CurrentUser.UserId).ToList();
                    DirTreeList.ExpandToLevel(1);


                    wbStartDate.DateTime = DateTimeDayOfMonthExtensions.FirstDayOfMonth(DateTime.Now);
                    wbEndDate.DateTime = DateTime.Now.AddDays(1);

                    UserComboBox.Properties.DataSource = new List<object>() { new { UserId = -1, Name = "Усі" } }.Concat(new BaseEntities().Users.Select(s => new { s.UserId, s.Name })).ToList();
                    UserComboBox.EditValue = -1;

                    wTypeList.Properties.DataSource = new List<object>() { new { FunId = (int?)-1, Name = "Усі" } }
                        .Concat(new BaseEntities().ViewLng.Where(w => w.LangId == 2 && (w.UserTreeView.Functions.TabId == 24 || w.UserTreeView.Functions.TabId == 27 || w.UserTreeView.Functions.TabId == 51)).Select(s => new { s.UserTreeView.FunId, s.Name })).ToList();
                    wTypeList.EditValue = -1;

                }

           /*     using (var s = new UserSettingsRepository(UserSession.UserId))
                {
                    ComPortNameEdit.Text = s.ComPortName;
                    ComPortSpeedEdit.Text = s.ComPortSpeed;
                }*/
            }
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as GetServiceTree_Result;

            RefrechItemBtn.PerformClick();
            mainContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;

            if (focused_tree_node.FunId != null)
            {
                History.AddEntry(new HistoryEntity { FunId = focused_tree_node.FunId.Value, MainTabs = 6 });
            }
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
                    var u = UsersGridView.GetFocusedRow() as Users;
                    new frmUserEdit(u.UserId).ShowDialog();

                    RefrechItemBtn.PerformClick();
                    break;

           /*     case 3: frmDBEdit = new TfrmDBEdit(Application);
                    SkladData->DBList->Open();
                    SkladData->DBList->Edit();
                    if (frmDBEdit->ShowModal() == mrOk)
                    {
                        SkladData->DBList->Post();
                        if (SkladData->DBListdef->Value == 1)
                        {
                            int id = SkladData->DBListDBID->Value;
                            for (SkladData->DBList->First(); !SkladData->DBList->Eof; SkladData->DBList->Next())
                            {
                                if (id != SkladData->DBListDBID->Value)
                                {
                                    SkladData->DBList->Edit();
                                    SkladData->DBListdef->Value = 0;
                                    SkladData->DBList->Post();
                                }
                            }
                            SkladData->DBList->Locate("DBID", id, TLocateOptions());
                        }

                    }
                    else SkladData->DBList->Cancel();
                    delete frmDBEdit;
                    break;*/

                case 5:
                    var f = new frmOperLogDet();
                    f.OperLogDetBS.DataSource = OprLogGridView.GetFocusedRow();
                    f.ShowDialog();
                    break;
            }
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            switch (focused_tree_node.GType)
            {
                case 1:
                    UsersDS.DataSource = DB.SkladBase().Users.ToList();
                    break;

                case 2:
                    UsersOnlineBS.DataSource = DB.SkladBase().Users.AsNoTracking().Where(w => w.IsOnline == true).ToList();
                    break;

                /*        case 3: DelBarButton->Enabled = (cxGridDBTableView2->DataController->DataSource->DataSet->FieldByName("def")->Value != 1);
                            break;*/

                case 5:
                    GetOperLogBS.DataSource = DB.SkladBase().GetOperLog(wbStartDate.DateTime, wbEndDate.DateTime, (int)wTypeList.EditValue, (int)UserComboBox.EditValue).OrderByDescending(o => o.OnDate).ToList().Select(s => new GetOperLog_Result
                    {
                        OpCode = s.OpCode,
                        OnDate = s.OnDate,
                        FunName = s.FunName,
                        Id = s.Id,
                        DocNum = s.DocNum,
                        UserName = s.UserName,
                        TabId = s.TabId,
                        DataBefore = IHelper.ConvertLogData(s.DataBefore),
                        DataAfter = IHelper.ConvertLogData(s.DataAfter),
                        ClassName = s.ClassName,
                        DocType = s.DocType,
                        FunId = s.FunId,
                        OpId = s.OpId,
                        UserId = s.UserId
                    }).ToList();
                    PrintLogGridControl.DataSource = DB.SkladBase().GetPrintLog(wbStartDate.DateTime, wbEndDate.DateTime, (int)UserComboBox.EditValue);
                    break;

                case 6:
                    CommonParamsBS.DataSource = DBHelper.CommonParam;
                    break;
            }
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
                    new frmUserEdit().ShowDialog();

                    RefrechItemBtn.PerformClick();
                    break;

                /*	case 3:		frmDBEdit = new  TfrmDBEdit(Application);
                                SkladData->DBList->Open();
                                SkladData->DBList->Append();
                                if(frmDBEdit->ShowModal()== mrOk)
                                 {
                                    SkladData->DBList->Post();
                                 }  else SkladData->DBList->Cancel();
                                delete frmDBEdit;
                                break;*/
            }
            RefrechItemBtn.PerformClick();
        }

        private void UsersGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
                    var u = UsersGridView.GetFocusedRow() as Users;
                    using (var db = DB.SkladBase())
                    {
                        db.DeleteWhere<Users>(w => w.UserId == u.UserId);
                        db.SaveChanges();
                    }
                    break;

                /*case 3: SkladData->DBList->Delete(); break ;
                case 5: if(cxGrid4->ActiveLevel->Index == 0 ) OperLog->Delete();
                        if(cxGrid4->ActiveLevel->Index == 1) PrintLog->Delete();
                        break ;*/
            }

            RefrechItemBtn.PerformClick();
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }

        private void OprLogGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                OperLogPopupMenu.ShowPopup(p2);
            }
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            RefrechItemBtn.PerformClick();
        }

        private void OprLogGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = OprLogGridView.GetFocusedRow() as GetOperLog_Result;
            new frmLogHistory(dr.TabId, dr.Id).ShowDialog();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
         //   gridControl1.DataSource = DB.SkladBase().Database.SqlQuery, memoEdit1.Text);
        }

        private void ComPortNameEdit_EditValueChanged(object sender, EventArgs e)
        {
           /* using (var s = new UserSettingsRepository(UserSession.UserId ))
            {
                s.ComPortName = ComPortNameEdit.Text;
            }*/
        }

        private void ComPortSpeedEdit_EditValueChanged(object sender, EventArgs e)
        {
          /*  using (var s = new UserSettingsRepository(UserSession.UserId))
            {
                s.ComPortSpeed = ComPortSpeedEdit.Text;
            }*/
        }
    }
}
