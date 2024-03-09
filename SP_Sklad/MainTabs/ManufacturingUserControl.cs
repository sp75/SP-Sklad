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
        private PreparationRawMaterialsList_Result focused_prep_raw_mat_row { get; set; }
        private v_IntermediateWeighing intermediate_weighing_focused_row { get; set; }
        private List<GetManufactureTree_Result> manuf_tree { get; set; }
        private GetManufactureTree_Result intermediate_weighing_access{ get; set; }
        private v_RawMaterialManagement focused_raw_material_management => RawMaterialManagementGridView.GetFocusedRow() is NotLoadedObject ? null : RawMaterialManagementGridView.GetFocusedRow() as v_RawMaterialManagement;

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


                IntermediateWeighingStartDate.EditValue = DateTime.Now.Date;
                IntermediateWeighingEndDate.EditValue = DateTime.Now.Date.SetEndDay();
                lookUpEdit3.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведений" }, new { Id = 0, Name = "Новий" } };
                lookUpEdit3.EditValue = -1;

                manuf_tree = DB.SkladBase().GetManufactureTree(DBHelper.CurrentUser.UserId).ToList();
                intermediate_weighing_access = manuf_tree.FirstOrDefault(w => w.FunId == 83);
              

                RawMaterialManagementStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                RawMaterialManagementEndDate.EditValue = DateTime.Now.Date.SetEndDay();
                RawMaterialManagementStatus.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведений" }, new { Id = 0, Name = "Новий" } };
                RawMaterialManagementStatus.EditValue = -1;

                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, DB.SkladBase());
            
                DocsTreeList.DataSource = manuf_tree;
                DocsTreeList.ExpandAll(); //ExpandToLevel(0);
            }
        }




        void GetIntermediateWeighing()
        {
            int top_row = IntermediateWeighingGridView.TopRowIndex;
            var satrt_date = IntermediateWeighingStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : IntermediateWeighingStartDate.DateTime;
            var end_date = IntermediateWeighingEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : IntermediateWeighingEndDate.DateTime;

            using (var db = DB.SkladBase())
            {
                IntermediateWeighingBS.DataSource = db.v_IntermediateWeighing.AsNoTracking().Where(w=> w.OnDate > satrt_date && w.OnDate <= end_date).OrderBy(o => o.OnDate).ToList();
            }

            IntermediateWeighingGridView.TopRowIndex = top_row;
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

        int restore_row = 0;
        bool restore = false;
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
                  /*  warehouseUserControl1.set_tree_node = focused_tree_node.Id;
                    warehouseUserControl1.WHTreeList.FocusedNode = warehouseUserControl1.WHTreeList.FindNodeByFieldValue("Id", focused_tree_node.Id);
                    bar1.Visible = false;
                    warehouseUserControl1.splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;*/
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
                    GetIntermediateWeighing();
                    break;
                case 8:
                    restore_row = RawMaterialManagementGridView.FocusedRowHandle;
                    restore = true;

                    RawMaterialManagementGridControl.DataSource = null;
                    RawMaterialManagementGridControl.DataSource = RawMaterialManagementSource;
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
                        ManufDocEdit.WBEdit(focused_prep_raw_mat_row.WType, focused_prep_raw_mat_row.WbillId);

                        break;

                    case 7:
                        using (var wb_iw = new frmIntermediateWeighing(intermediate_weighing_focused_row.WbillId, intermediate_weighing_focused_row.Id))
                        {
                            if (wb_iw.ShowDialog() == DialogResult.OK)
                            {
                                RefreshIntermediateWeighing();
                            }
                        }
                        break;

                    case 8:
                        using (var rmm_make = new frmRawMaterialManagement(focused_raw_material_management.Id))
                        {
                           if( rmm_make.ShowDialog() == DialogResult.OK)
                            {
                                ;
                            }
                        }
                        break;
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
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
                        if (focused_prep_raw_mat_row == null)
                        {
                            return;
                        }

                        var wbpm = db.WaybillList.Find(focused_prep_raw_mat_row.WbillId);
                        if (wbpm == null)
                        {
                            MessageBox.Show(Resources.not_find_wb);
                            return;
                        }
                        if (wbpm.SessionId != null)
                        {
                            MessageBox.Show(Resources.deadlock);
                            return;
                        }


                        if (wbpm.Checked == 2)
                        {
                            DBHelper.StornoOrder(db, focused_prep_raw_mat_row.WbillId);

                        }
                        else
                        {
                            DBHelper.ExecuteOrder(db, focused_prep_raw_mat_row.WbillId);
                        }

                        break;

                    case 8:
                        {
                            if (focused_raw_material_management.DocType == 1)
                            {
                                var new_wb_in = ExecuteDocument.ExecuteRawMaterialManagement(focused_raw_material_management.Id, db);

                                if (new_wb_in != Guid.Empty)
                                {
                                    ;
                                   /* using (var f = new frmWayBillIn(1))
                                    {
                                        f.is_new_record = true;
                                        f.doc_id = new_wb_in;
                                        f.ShowDialog();
                                    }*/
                                }
                                else
                                {
                                    MessageBox.Show("Не можливо провести документ, одна із причин, не вказаний постачальник або ціна товара!");
                                }
                            }

                            if (focused_raw_material_management.DocType == -1)
                            {
                                int? new_wb_move = null;
                                Guid new_write_of = Guid.Empty;
                                try
                                {
                                    new_wb_move = ExecuteDocument.ExecuteRawMaterialManagementMove(focused_raw_material_management.Id, db);

                                    if (new_wb_move != null)
                                    {
                                        using (var m_f = new frmWayBillMove(new_wb_move))
                                        {
                                            m_f.is_new_record = true;
                                            if (m_f.ShowDialog() == DialogResult.OK)
                                            {
                                                new_write_of = ExecuteDocument.ExecuteRawMaterialManagementWBWriteOff(focused_raw_material_management.Id, db);

                                                if (new_write_of != Guid.Empty)
                                                {
                                                    using (var f = new frmWBWriteOff())
                                                    {
                                                        f.doc_id = new_write_of;
                                                        f.TurnDocCheckBox.Checked = true;
                                                        f.is_new_record = true;
                                                        f.ShowDialog();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    if (new_wb_move.HasValue)
                                    {
                                        db.DeleteWhere<WaybillList>(w => w.WbillId == new_wb_move);
                                        db.DeleteWhere<WaybillList>(w => w.Id == new_write_of);
                                    }
                                }
                            }

                            break;
                        }
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
            if (  pc_focused_row == null && focused_prep_raw_mat_row == null && intermediate_weighing_focused_row == null && focused_raw_material_management == null)
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
                                var wbp = db.WaybillList.FirstOrDefault(w => w.WbillId == focused_prep_raw_mat_row.WbillId && w.SessionId == null);
                                if (wbp != null)
                                {
                                    db.WaybillList.Remove(wbp);
                                }
                                else
                                {
                                    MessageBox.Show(Resources.deadlock);
                                }
                                break;

                            case 7:
                                var iw_row = db.IntermediateWeighing.FirstOrDefault(w => w.Id == intermediate_weighing_focused_row.Id && w.SessionId == null);
                                if (iw_row != null)
                                {
                                    DB.SkladBase().DeleteWhere<IntermediateWeighing>(w => w.Id == intermediate_weighing_focused_row.Id);
                                }

                                break;

                            case 8:
                                var rmm_row = db.RawMaterialManagement.FirstOrDefault(w => w.Id == focused_raw_material_management.Id && w.SessionId == null);
                                if (rmm_row != null)
                                {
                                    DB.SkladBase().DeleteWhere<RawMaterialManagement>(w => w.Id == focused_raw_material_management.Id);
                                }

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
                        wb_id = focused_prep_raw_mat_row.WbillId;
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
                    PrintDoc.Show(focused_prep_raw_mat_row.Id, focused_prep_raw_mat_row.WType, DB.SkladBase());
                    break;
            }


        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
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

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void gridView3_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                BottomPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void RefreshAtribute(int wbill_id)
        {
            
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }



        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {

        }

        private void RefreshIntermediateWeighing()
        {

        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
           
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

      

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void IntermediateWeighingGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditIntermediateWeighing.PerformClick();
        }

        private void AddIntermediateWeighing_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }
        
        private void barButtonItem8_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void IntermediateWeighingStartDate_EditValueChanged(object sender, EventArgs e)
        {
            GetIntermediateWeighing();
        }

        private void IntermediateWeighingGridView_FocusedRowObjectChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            intermediate_weighing_focused_row = e.Row as v_IntermediateWeighing;

            xtraTabControl6_SelectedPageChanged(sender, null);

            DeleteItemBtn.Enabled = (intermediate_weighing_focused_row != null && intermediate_weighing_focused_row.Checked == 0 && focused_tree_node.CanDelete == 1 && intermediate_weighing_focused_row.WbChecked == 0);
            EditItemBtn.Enabled = (intermediate_weighing_focused_row != null && /*intermediate_weighing_focused_row.Checked == 0 &&*/ focused_tree_node.CanModify == 1 && intermediate_weighing_focused_row.WbChecked == 0);
            CopyItemBtn.Enabled = (focused_tree_node.CanInsert == 1 && intermediate_weighing_focused_row != null);
            ExecuteItemBtn.Enabled = (intermediate_weighing_focused_row != null && focused_tree_node.CanPost == 1);
            PrintItemBtn.Enabled = (intermediate_weighing_focused_row != null);

        }

        private void xtraTabControl6_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (intermediate_weighing_focused_row == null)
            {

                IntermediateWeighingDetBS.DataSource = null;
                ucRelDocGrid5.GetRelDoc(Guid.Empty);
                return;
            }

            using (var db = DB.SkladBase())
            {
                switch (xtraTabControl6.SelectedTabPageIndex)
                {
                    case 0:
                        var list = DB.SkladBase().v_IntermediateWeighingDet.AsNoTracking().Where(w => w.IntermediateWeighingId == intermediate_weighing_focused_row.Id).OrderBy(o => o.CreatedDate).ToList();

                        int top_row = WaybillDetInGridView.TopRowIndex;
                        IntermediateWeighingDetBS.DataSource = list;
                        WaybillDetInGridView.TopRowIndex = top_row;
                        break;

                    case 1:
                        ucRelDocGrid5.GetRelDoc(intermediate_weighing_focused_row.Id);
                        break;
                }
            }
        }

        private void RawMaterialManagementSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            /*     if (focused_tree_node == null)
                 {
                     return;
                 }*/

            int status = (int)RawMaterialManagementStatus.EditValue;

            var _db = DB.SkladBase();

            var rmm = _db.v_RawMaterialManagement.Where(w => w.OnDate >= RawMaterialManagementStartDate.DateTime && w.OnDate <= RawMaterialManagementEndDate.DateTime && (w.Checked == status || status == -1));

            e.QueryableSource = rmm;

            e.Tag = _db;
        }

        private void RawMaterialManagementGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (focused_raw_material_management == null)
            {
                RawMaterialManagementDetGridControl.DataSource = null;
                ucRelDocGrid6.GetRelDoc(Guid.Empty);
            }

            if (!restore)
            {
                return;
            }

            RawMaterialManagementGridView.TopRowIndex = restore_row;
            RawMaterialManagementGridView.FocusedRowHandle = restore_row;
            restore = false;
        }

        private void RawMaterialManagementGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            xtraTabControl7_SelectedPageChanged(sender, null);

            DeleteItemBtn.Enabled = (focused_raw_material_management != null && focused_tree_node.CanDelete == 1 && focused_raw_material_management.Checked == 0);
            EditItemBtn.Enabled = (focused_raw_material_management != null && focused_tree_node.CanModify == 1 && focused_raw_material_management.Checked == 0);
            CopyItemBtn.Enabled = (focused_tree_node.CanInsert == 1 && focused_raw_material_management != null);
            ExecuteItemBtn.Enabled = (focused_raw_material_management != null && focused_tree_node.CanPost == 1 && focused_raw_material_management.Checked == 0);
            PrintItemBtn.Enabled = (focused_raw_material_management != null);
        }

        private void GetRawMaterialManagementDetail()
        {
            if (focused_raw_material_management == null)
            {
                return;
            }

            using (var db = DB.SkladBase())
            {
                RawMaterialManagementDetGridControl.DataSource = db.v_RawMaterialManagementDet.AsNoTracking().Where(w => w.RawMaterialManagementId == focused_raw_material_management.Id).ToList();
            }
        }

        private void xtraTabControl7_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (focused_raw_material_management == null)
            {
                RawMaterialManagementDetGridControl.DataSource = null;
                ucRelDocGrid6.GetRelDoc(Guid.Empty);

                return;
            }

            using (var db = DB.SkladBase())
            {
                switch (xtraTabControl7.SelectedTabPageIndex)
                {
                    case 0:
                        var list = DB.SkladBase().v_RawMaterialManagementDet.AsNoTracking()
                            .Where(w => w.RawMaterialManagementId == focused_raw_material_management.Id)
                            .OrderBy(o => o.OnDate).ToList();

                        RawMaterialManagementDetGridControl.DataSource = list;
                        break;

                    case 1:
                        ucRelDocGrid6.GetRelDoc(focused_raw_material_management.Id);
                        break;

                    case 2:
                        gridControl10.DataSource = db.Database.SqlQuery<pos_rmmd>(@"select x.*, pos.ActualRemain, pos.OnDate, wbd.Amount Total, wb.Num
from
(   
   SELECT 
     rd.PosId,
     rd.BarCode, 
	 sum(case when rmm.DocType = 1 then rd.Amount else 0 end) AmountIn, 
	 sum(case when rmm.DocType = -1 then rd.Amount else 0 end) AmountOut,
	 sum(case when rmm.DocType = 1 then 1 else 0 end) CountIn, 
	 sum(case when rmm.DocType = -1 then 1 else 0 end) CountOut

  FROM RawMaterialManagementDet rd
  join RawMaterialManagement rmm on rmm.Id = rd.RawMaterialManagementId
  where rd.PosId in ( select PosId from RawMaterialManagementDet where RawMaterialManagementId = {0} )
  group by rd.PosId, rd.BarCode
)x
join v_PosRemains pos on pos.PosId = x.PosId
join WaybillDet wbd on wbd.PosId = x.PosId
join WaybillList wb on wb.WbillId = wbd.WbillId", focused_raw_material_management.Id).ToList();
                        break;

                }
            }

        }
        public class pos_rmmd
        {
            public string BarCode { get; set; }
            public decimal AmountIn { get; set; }
            public decimal AmountOut { get; set; }
            public decimal ActualRemain { get; set; }
            public DateTime OnDate { get; set; }
            public decimal Total { get; set; }
            public string Num { get; set; }
            public int CountIn { get; set; }
            public int CountOut { get; set; }
        }

        private void RawMaterialManagementStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (RawMaterialManagementStartDate.ContainsFocus || RawMaterialManagementEndDate.ContainsFocus || RawMaterialManagementStatus.ContainsFocus)
            {
                RefrechItemBtn.PerformClick();
            }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void WayBillMakeDetGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                WbDetPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem10_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (UserSession.production_monitor_frm == null)
            {
                UserSession.production_monitor_frm = new frmProductionMonitor();
                UserSession.production_monitor_frm.Show();
            }
            else
            {
                UserSession.production_monitor_frm.WindowState = FormWindowState.Normal;
                UserSession.production_monitor_frm.Activate();
            }

        }
    }
}
