using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.SkladData;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using SP_Sklad.WBForm;
using SP_Sklad.Common;
using SP_Sklad.WBDetForm;
using SP_Sklad.Properties;
using SP_Sklad.Reports;
using System.IO;
using System.Diagnostics;
using SP_Sklad.ViewsForm;
using static SP_Sklad.WBDetForm.frmIntermediateWeighingDet;
using DevExpress.Data;

namespace SP_Sklad.MainTabs
{
    public partial class ManufacturingUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        private GetManufactureTree_Result focused_tree_node { get; set; }

        public ManufacturingUserControl()
        {
            InitializeComponent();
        }

        private void ManufacturingUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                wbContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

                DocsTreeList.DataSource = DB.SkladBase().GetManufactureTree(DBHelper.CurrentUser.UserId).ToList(); 
                DocsTreeList.ExpandAll(); 
            }
        }


        private void DocsTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DocsTreeList.GetDataRecordByNode(e.Node) as GetManufactureTree_Result;
            if (focused_tree_node == null)
            {
                return;
            }

            if (focused_tree_node.GType.Value == 1)
            {
         
                wbContentTab.SelectedTabPageIndex = 1;
                ucManufacturingProducts.GetWBListMake(focused_tree_node.Num);
            }
            else if (focused_tree_node.GType.Value == 3)
            {
           
                wbContentTab.SelectedTabPageIndex = 3;
                ucDeboningProducts.GetDeboningList(focused_tree_node.Num);
            }
            else if (focused_tree_node.GType.Value == 4)
            {
         
                wbContentTab.SelectedTabPageIndex = 4;
            }
            else if (focused_tree_node.FunId == 36)
            {
            
                wbContentTab.SelectedTabPageIndex = 9;
            }
            else if (focused_tree_node.FunId == 44)
            {
       
                wbContentTab.SelectedTabPageIndex = 10;
            }
            else if (focused_tree_node.GType.Value == 6)
            {
        
                wbContentTab.SelectedTabPageIndex = 6;
            }
            else if (focused_tree_node.GType.Value == 7)
            {
         
                wbContentTab.SelectedTabPageIndex = 7;
            }
            else if (focused_tree_node.GType.Value == 8)
            {
            
                wbContentTab.SelectedTabPageIndex = 8;
            }
            else if (focused_tree_node.GType.Value == 5)
            {
             
                wbContentTab.SelectedTabPageIndex = 5;
            }
            else
            {

                wbContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;
            }

            if (focused_tree_node.FunId != null)
            {
                History.AddEntry(new HistoryEntity { FunId = focused_tree_node.FunId.Value, MainTabs = 1 });

                if (DocsTreeList.ContainsFocus)
                {
                    Settings.Default.LastFunId = focused_tree_node.FunId.Value;
                }
            }
        }




    }
}
