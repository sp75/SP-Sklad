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
using SP_Sklad.WBForm;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using SP_Sklad.Common;
using SP_Sklad.Properties;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SP_Sklad.EditForm;
using SP_Sklad.Reports;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Designer;
using DevExpress.Data;
using System.Data.Entity;
using SkladEngine.DBFunction;
using SP_Sklad.WBDetForm;

namespace SP_Sklad.MainTabs
{
    public partial class WarehouseUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        GetWhTree_Result focused_tree_node { get; set; }
        public int? set_tree_node { get; set; }
        public int g_type = -1;

        public WarehouseUserControl()
        {
            InitializeComponent();
            whContentTab.Visible = false;
        }
       
        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        private void WarehouseUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
       
                whContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

                if (WHTreeList.DataSource == null)
                {
                    GetTree(1);
                }
                var user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());
            }

            whContentTab.Visible = true;
        }

        void GetTree(int type)
        {
            WHTreeList.DataSource = new BaseEntities().GetWhTree(DBHelper.CurrentUser.UserId, type).Where(w => w.GType == g_type || g_type == -1).ToList();
            WHTreeList.ExpandToLevel(0);

            if (set_tree_node != null)
            {
                WHTreeList.FocusedNode = WHTreeList.FindNodeByFieldValue("Id", set_tree_node);
                set_tree_node = null;
            }
        }

        private void WHTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            CopyItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;
            focused_tree_node = WHTreeList.GetDataRecordByNode(e.Node) as GetWhTree_Result;

            if (focused_tree_node == null)
            {
                return;
            }


            if (focused_tree_node.FunId == 36)
            {
                whContentTab.SelectedTabPageIndex = 3;
            }
            else if(focused_tree_node.FunId == 44)
            {
                whContentTab.SelectedTabPageIndex = 4;
            }
            else if (focused_tree_node.FunId == 41)
            {
                whContentTab.SelectedTabPageIndex = 5;
            }
            else if(focused_tree_node.FunId == 53)
            {
                whContentTab.SelectedTabPageIndex = 6;
            }

            else if(focused_tree_node.GType.Value == 1)
            {
                ucWhMat.by_grp = ByGrpBtn.Down;
                ucWhMat.display_child_groups = ViewDetailTree.Down;
                ucWhMat.focused_tree_node_num = focused_tree_node.Num;
                whContentTab.SelectedTabPageIndex = 7;
                var result = ucWhMat.GetMatOnWh();
            }
            else
            {
                whContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;
            }


            if (focused_tree_node.FunId != null)
            {
                History.AddEntry(new HistoryEntity { FunId = focused_tree_node.FunId.Value, MainTabs = 2 });


                if (WHTreeList.ContainsFocus)
                {
                    Settings.Default.LastFunId = focused_tree_node.FunId.Value;
                }
            }
        }

      

        private void ByGrpBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucWhMat.by_grp = ByGrpBtn.Down;
            GetTree(1);
        }

        private void ByWhBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucWhMat.display_child_groups = ViewDetailTree.Down;
            ucWhMat.by_grp = ByGrpBtn.Down;
            GetTree(2);
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }



        private void ExecuteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
 
        }
    
        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           

        }

        private void ViewDetailTree_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ByGrpBtn.Down = true;
            ucWhMat.display_child_groups = ViewDetailTree.Down;
            ucWhMat.by_grp = ByGrpBtn.Down;
            RefreshWhBtn.PerformClick();

            var result = ucWhMat.GetMatOnWh();
        }

      

        private void AddItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void DelItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }
     
        private void MatTurnInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void RecalcRemainsMatBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        
        }

        private void RecalcRemainsAllMatBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void DeboningMatBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void ShowEmptyItemsCheck_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ShowAllItemsCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }


        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void PrintItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void ShowAllItemsCheck_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }



        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }



        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }


        private void barButtonItem7_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void AddGrpItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

     

        private void WBDetPropBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }
    }
}
