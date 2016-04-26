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

namespace SP_Sklad.MainTabs
{
    public partial class WarehouseUserControl : UserControl
    {
        int wid = 0;
        string wh_list = "*";

        GetWhTree_Result focused_tree_node { get; set; }

        public WarehouseUserControl()
        {
            InitializeComponent();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void WarehouseUserControl_Load(object sender, EventArgs e)
        {
            whContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            OnDateEdit.DateTime = DateTime.Now;

            whKagentList.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().Kagent.Select(s => new { s.KaId, s.Name }).ToList());
            whKagentList.EditValue = 0;

            wbSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
            wbSatusList.EditValue = -1;

            wbStartDate.EditValue = DateTime.Now.AddDays(-30);
            wbEndDate.EditValue = DateTime.Now;

            GetTree(1);
        }

        void GetTree(int type)
        {
            WHTreeList.DataSource = new BaseEntities().GetWhTree(DBHelper.CurrentUser.UserId, type).ToList();
            WHTreeList.ExpandAll();
        }

        public void OnLoad(BaseEntities db)
        {

        }

        private void WHTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            CopyItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;
            focused_tree_node = WHTreeList.GetDataRecordByNode(e.Node) as GetWhTree_Result;

            switch (focused_tree_node.GType.Value)
            {
                case 1:
                    int grp_id = 0;
                   
                    string grp = "";

                    if (ByGrpBtn.Down) grp_id =focused_tree_node.Num;
                    else wid = focused_tree_node.Num;

                    if (ViewDetailTree.Down && ByGrpBtn.Down && focused_tree_node.Num != 0)
                    {
                        grp = focused_tree_node.Num.ToString();
                    }

                    WhMatGridControl.DataSource = null;
                    WhMatGridControl.DataSource = DB.SkladBase().WhMatGet(grp_id, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, 0, wh_list, 0, "", DBHelper.CurrentUser.UserId, 0).ToList();
                    /*
                        WhTopPanel->Edit();
                        WhTopPanelGRPID->Value = 0;
                        WhTopPanelWID->Value = 0;
                        if (ByGrpBtn->Down) WhTopPanelGRPID->Value = WhTreeDataNUM->Value;
                        else WhTopPanelWID->Value = WhTreeDataNUM->Value;
                        if (ViewDetailTree->Down && ByGrpBtn->Down && WhTreeDataNUM->Value != 0)
                        {
                            WhTopPanel->Edit();
                            WhTopPanelGRP->Value = WhTreeDataNUM->Value;
                        }
                        else WhTopPanelGRP->Value = "";
                        WhTopPanel->Post();*/
                        //				   POS_GET->CloseOpen(true) ;       // Тормозить
                        //		   	   WMAT_GET_BY_WH->CloseOpen(true) ;   // Тормозить
                   
                    break;

            /*    case 2: cxGrid1Level1->GridView = cxGrid1DBTableView1;
                    cxGridLevel6->GridView = cxGridDBTableView1;
                    GET_RelDocList->DataSource = WayBillListDS;
                    WBTopPanelDate->Edit();
                    if (WhTreeDataID->Value == 48) WBTopPanelDateWTYPE->Value = 4;
                    if (WhTreeDataID->Value == 58) WBTopPanelDateWTYPE->Value = 5;
                    if (WhTreeDataID->Value == 54) WBTopPanelDateWTYPE->Value = -5;
                    if (WhTreeDataID->Value == 104)
                    {
                        WBTopPanelDateWTYPE->Value = 7;
                        cxGrid1Level1->GridView = cxGrid2DBTableView1;
                        cxGridLevel6->GridView = cxGrid7DBTableView1;
                    }

                    WBTopPanelDate->Post();
                    WayBillList->FullRefresh();
                    WayBillList->Refresh();
                    break;*/
            }
         
       //     RefrechItemBtn.PerformClick();
            whContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;
        }

        private void ByGrpBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTree(1);
        }

        private void ByWhBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTree(2);
        }

        private void WhMatGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var dr = WhMatGridView.GetRow(e.FocusedRowHandle) as WhMatGet_Result;

            if (dr != null)
            {
                RemainOnWhGrid.DataSource = DB.SkladBase().WMatGetByWh(dr.MatId, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, wh_list);
            }
            else
            {
                RemainOnWhGrid.DataSource = null;
            }
        }
    }
}
