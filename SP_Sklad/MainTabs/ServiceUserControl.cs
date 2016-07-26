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

namespace SP_Sklad.MainTabs
{
    public partial class ServiceUserControl : UserControl
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
                }
            }
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as GetServiceTree_Result;

            RefrechItemBtn.PerformClick();
            mainContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;
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
                    break;

                case 5: frmOperLog = new TfrmOperLog(Application);
                    frmOperLog->ShowModal();
                    delete frmOperLog;
                    break;*/
            }
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            switch (focused_tree_node.GType)
            {
                case 1:
                    UsersDS.DataSource = DB.SkladBase().Users.ToList();
                    break;

                /*      case 2: MonAttach->Open();
                          MonAttach->FullRefresh();
                          break;

                      case 3: DelBarButton->Enabled = (cxGridDBTableView2->DataController->DataSource->DataSet->FieldByName("def")->Value != 1);
                          break;

                      case 5: OperLog->Open();
                          OperLog->Refresh();
                          OperLog->FullRefresh();
                          PrintLog->Open();
                          PrintLog->Refresh();
                          PrintLog->FullRefresh();
                          break;*/
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
    }
}
