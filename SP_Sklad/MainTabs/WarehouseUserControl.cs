using System;
using System.Data;
using System.Linq;
using System.Text;
using SP_Sklad.SkladData;
using SP_Sklad.Common;
using SP_Sklad.Properties;


namespace SP_Sklad.MainTabs
{
    public partial class WarehouseUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public GetWhTree_Result focused_tree_node { get; set; }
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
            focused_tree_node = WHTreeList.GetDataRecordByNode(e.Node) as GetWhTree_Result;

            if (focused_tree_node == null)
            {
                return;
            }


            if (focused_tree_node.FunId == 36)
            {
                whContentTab.SelectedTabPageIndex = 2;
            }
            else if(focused_tree_node.FunId == 44)
            {
                whContentTab.SelectedTabPageIndex = 3;
            }
            else if (focused_tree_node.FunId == 41)
            {
                whContentTab.SelectedTabPageIndex = 4;
            }
            else if(focused_tree_node.FunId == 53)
            {
                whContentTab.SelectedTabPageIndex = 5;
            }

            else if(focused_tree_node.GType.Value == 1)
            {
                ucWhMat.by_grp = ByGrpBtn.Down;
                ucWhMat.display_child_groups = ViewDetailTree.Down;
                ucWhMat.focused_tree_node_num = focused_tree_node.Num;
                whContentTab.SelectedTabPageIndex = 1;

                var result = ucWhMat.GetMatOnWh();
               // ucWhMat.GetData();
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
            ViewDetailTree.Enabled = true;

            ucWhMat.by_grp = ByGrpBtn.Down;
            GetTree(1);
        }

        private void ByWhBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            whContentTab.SelectedTabPageIndex = 1;

            ViewDetailTree.Enabled = !ByWhBtn.Down;

            ucWhMat.display_child_groups = ViewDetailTree.Down;
            ucWhMat.by_grp = ByGrpBtn.Down;
            GetTree(2);
        }

        private void ViewDetailTree_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucWhMat.display_child_groups = ViewDetailTree.Down;

             var result = ucWhMat.GetMatOnWh();
          //  ucWhMat.GetData();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            whContentTab.SelectedTabPageIndex = 6;
        }
    }
}
