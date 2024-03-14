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
        private UserSettingsRepository user_settings { get; set; }

        private GetManufactureTree_Result focused_tree_node { get; set; }
        private v_PlannedCalculation pc_focused_row { get; set; }

        private List<GetManufactureTree_Result> manuf_tree { get; set; }
        private GetManufactureTree_Result intermediate_weighing_access{ get; set; }

        private int _cur_wtype = 0;

        public ManufacturingUserControl()
        {
            InitializeComponent();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void ManufacturingUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                wbContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

                dateEdit2.EditValue = DateTime.Now.Date.AddDays(-30);
                dateEdit1.EditValue = DateTime.Now.Date.SetEndDay();

                manuf_tree = DB.SkladBase().GetManufactureTree(DBHelper.CurrentUser.UserId).ToList();
                intermediate_weighing_access = manuf_tree.FirstOrDefault(w => w.FunId == 83);

                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, DB.SkladBase());
            
                DocsTreeList.DataSource = manuf_tree;
                DocsTreeList.ExpandAll(); //ExpandToLevel(0);
            }
        }


        void GetPlannedCalculation()
        {
            var satrt_date = dateEdit2.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : dateEdit2.DateTime;
            var end_date = dateEdit1.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : dateEdit1.DateTime;

            int top_row = PlannedCalculationGridView.TopRowIndex;
            PlannedCalculationBS.DataSource = DB.SkladBase().v_PlannedCalculation.AsNoTracking().Where(w=> w.OnDate >=satrt_date && w.OnDate <= end_date).OrderByDescending(o=> o.OnDate).ToList();
            PlannedCalculationGridView.TopRowIndex = top_row;
        }

        

        private void DocsTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DocsTreeList.GetDataRecordByNode(e.Node) as GetManufactureTree_Result;
            if (focused_tree_node == null)
            {
                return;
            }

            NewItemBtn.Enabled = (focused_tree_node != null && focused_tree_node.CanInsert == 1);
            CopyItemBtn.Enabled = false;
            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;
            AddTechProcBtn.Enabled = false;
            DelTechProcBtn.Enabled = false;
            EditTechProcBtn.Enabled = false;

            AddIntermediateWeighing.Enabled = false;
            EditIntermediateWeighing.Enabled = false;
            DelIntermediateWeighing.Enabled = false;

            _cur_wtype = focused_tree_node.WType != null ? focused_tree_node.WType.Value : 0;

            if (focused_tree_node.GType.Value == 1)
            {
                bar1.Visible = false;
                wbContentTab.SelectedTabPageIndex = 1;
                ucManufacturingProducts.GetWBListMake(focused_tree_node.Num);
            }
            else if (focused_tree_node.GType.Value == 3)
            {
                bar1.Visible = false;
                wbContentTab.SelectedTabPageIndex = 3;
                ucDeboningProducts.GetDeboningList(focused_tree_node.Num);
            }
            else if (focused_tree_node.GType.Value == 4)
            {
                bar1.Visible = false;
                wbContentTab.SelectedTabPageIndex = 4;
            }
            else if (focused_tree_node.FunId == 36)
            {
                bar1.Visible = false;
                wbContentTab.SelectedTabPageIndex = 9;
            }
            else if (focused_tree_node.FunId == 44)
            {
                bar1.Visible = false;
                wbContentTab.SelectedTabPageIndex = 10;
            }
            else if (focused_tree_node.GType.Value == 6)
            {
                bar1.Visible = false;
                wbContentTab.SelectedTabPageIndex = 6;
            }
            else if (focused_tree_node.GType.Value == 7)
            {
                bar1.Visible = false;
                wbContentTab.SelectedTabPageIndex = 7;
            }
            else if (focused_tree_node.GType.Value == 8)
            {
                bar1.Visible = false;
                wbContentTab.SelectedTabPageIndex = 8;
            }
            else
            {
                RefrechItemBtn.PerformClick();

                bar1.Visible = true;

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

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

       //     bar1.Visible = true;

            switch (focused_tree_node.GType.Value)
            {
                case 1:
                    
                    break;

                case 2:

                    break;

                case 3:
                  
                    break;

                case 4:
                  
                    break;

                case 5:
                    GetPlannedCalculation();
                    break;

                case 6:
                    break;

                case 7:
                    break;
                case 8:

                    break;
            }
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            switch (focused_tree_node.GType.Value)
            {
                case 1:
                    using (var wb_make = new frmWBManufacture(null))
                    {
                        wb_make.ShowDialog();
                    }
                    break;

                case 3:
                    using (var wb_make = new frmWBDeboning(null))
                    {
                        wb_make.ShowDialog();
                    }
                    break;

                case 4:
                    using (var wb_pp = new frmProductionPlans(null))
                    {
                        wb_pp.ShowDialog();
                    }
                    break;

                case 5:
                    using (var wb_pc = new frmPlannedCalculation())
                    {
                        wb_pc.ShowDialog();
                    }
                    break;

                case 6:
                    using (var wb_make = new frmPreparationRawMaterials(null))
                    {
                        wb_make.ShowDialog();
                    }
                    break;

                case 7:

                    using (var frm = new frmManufacturing(DB.SkladBase(), 0))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            using (var wb_iw = new frmIntermediateWeighing(frm.wb_focused_row.WbillId, null))
                            {
                                wb_iw.ShowDialog();
                            }
                        }
                    }
                    break;

                case 8:
                    using (var rmm_make = new frmRawMaterialManagement(null))
                    {
                        rmm_make.ShowDialog();
                    }
                    break;
            }

            RefrechItemBtn.PerformClick();
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = new BaseEntities())
            {
                switch (focused_tree_node.GType)
                {
                    case 1:

                        break;

                    case 3:

                        break;

                    case 4:
                        
                        break;

                    case 5:
                        var pc_row = PlannedCalculationGridView.GetFocusedRow() as v_PlannedCalculation;

                        using (var f = new frmPlannedCalculation(pc_row.Id))
                        {
                            f.ShowDialog();
                        }

                        break;

                    case 6:
                    

                        break;

                    case 7:
                     
                        break;

                    case 8:
                       
                        break;
                }
            }

            RefrechItemBtn.PerformClick();
        }


        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditItemBtn.PerformClick();
        }

        private void ExecuteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = new BaseEntities())
            {
                switch (focused_tree_node.GType)
                {

                    case 4:
                       
                        break;

                    case 6:
                        
                        break;

                    case 8:
                      
                    case 9:
                        {

                            break;
                        }
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (  pc_focused_row == null )
            {
                return;
            }

            using (var db = new BaseEntities())
            {
                try
                {
                    if (MessageBox.Show(Resources.delete_wb, "Відалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        switch (focused_tree_node.GType)
                        {
                            case 1:
                               
                                break;

                            case 3:
                                
                                break;

                            case 4:

                                break;

                            case 5:

                                var pc_row = db.PlannedCalculation.FirstOrDefault(w => w.Id == pc_focused_row.Id && w.SessionId == null);

                                if (pc_row != null)
                                {
                                    db.DeleteWhere<PlannedCalculation>(w => w.Id == pc_focused_row.Id);
                                }
                                break;

                            case 6:
                              
                                break;

                            case 7:
                              

                                break;

                            case 8:
                             

                                break;

                        }
                        db.SaveChanges();
                    }
                }
                catch
                {
                    MessageBox.Show(Resources.deadlock);
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void StopProcesBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = DB.SkladBase())
            {
                var wb_id = 0;
                switch (focused_tree_node.GType)
                {
                    case 1:
                    case 3:
                     
                        break;

                    case 6:
                    
                        break;

                }

                var wbl = db.WaybillList.FirstOrDefault(w => w.WbillId == wb_id);
                if (wbl == null)
                {
                    return;
                }

                if (wbl.Checked == 2)
                {
                    using (var f = new frmWBWriteOn())
                    {
                        var result = f._db.ExecuteWayBill(wbl.WbillId, null, DBHelper.CurrentUser.KaId).ToList().FirstOrDefault();
                        f.is_new_record = true;
                        f.doc_id = result.NewDocId;
                        f.TurnDocCheckBox.Checked = true;
                        f.ShowDialog();
                    }
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void PrintItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            switch (focused_tree_node.GType.Value)
            {
                case 1:
                   
                    break;

                case 3:
                   
                    break;

                case 4:
                  
                    break;

                case 5:
                    PrintDoc.Show(pc_focused_row.Id, 22, DB.SkladBase());
                    break;

                case 6:
                 
                    break;
            }


        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
                    
                    break;

                case 3:
                    
                    break;

                case 4:
                  
                    break;
                case 5:
                    var pc_copy = DB.SkladBase().DocCopy(pc_focused_row.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();

                    using (var pc = new frmPlannedCalculation(pc_copy.out_doc_id))
                    {
                        pc.is_new_record = true;
                        pc.ShowDialog();
                    }
                    break;
            }

            RefrechItemBtn.PerformClick();
        }


        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {
            GetPlannedCalculation();
        }

        private void PlannedCalculationGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            pc_focused_row = ((GridView)sender).GetRow(e.FocusedRowHandle) as v_PlannedCalculation;

            if (pc_focused_row != null)
            {
                using (var db = DB.SkladBase())
                {
                    gridControl9.DataSource = db.v_PlannedCalculationDetDet.AsNoTracking().Where(w => w.PlannedCalculationId == pc_focused_row.Id).OrderBy(o => o.RecipeName).ToList();
                }
            }
            else
            {
                gridControl9.DataSource = null;
            }

            DeleteItemBtn.Enabled = (pc_focused_row != null && focused_tree_node.CanDelete == 1);
            EditItemBtn.Enabled = (pc_focused_row != null && focused_tree_node.CanModify == 1);
            CopyItemBtn.Enabled = (focused_tree_node.CanInsert == 1 && pc_focused_row != null);
            ExecuteItemBtn.Enabled = (pc_focused_row != null && focused_tree_node.CanPost == 1);
            PrintItemBtn.Enabled = (pc_focused_row != null);
        }

    }
}
