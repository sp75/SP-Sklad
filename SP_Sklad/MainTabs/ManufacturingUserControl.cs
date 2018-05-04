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


namespace SP_Sklad.MainTabs
{
    public partial class ManufacturingUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        GetManufactureTree_Result focused_tree_node { get; set; }
        WBListMake_Result focused_row { get; set; }
        ProductionPlansList_Result pp_focused_row { get; set; }
        int _cur_wtype = 0;

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

                WhComboBox.Properties.DataSource = new List<object>() { new { WId = "*", Name = "Усі" } }.Concat(DBHelper.WhList.Select(s => new { WId = s.WId.ToString(), s.Name }).ToList());
                WhComboBox.EditValue = "*";
                DebWhComboBox.Properties.DataSource = WhComboBox.Properties.DataSource;
                DebWhComboBox.EditValue = "*";

                wbSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 0, Name = "Актуальний" }, new { Id = 2, Name = "Розпочато виробництво" }, new { Id = 1, Name = "Закінчено виробництво" } };
                wbSatusList.EditValue = -1;
                DebSatusList.Properties.DataSource = wbSatusList.Properties.DataSource;
                DebSatusList.EditValue = -1;
                lookUpEdit2.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Розпочато виробництво" }, new { Id = 0, Name = "Актуальний" } };
                lookUpEdit2.EditValue = -1;

                wbStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                wbEndDate.EditValue = DateTime.Now.Date.SetEndDay();
                DebStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                DebEndDate.EditValue = DateTime.Now.Date.SetEndDay();

                PlanStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                PlanEndDate.EditValue = DateTime.Now.Date.SetEndDay();

                DocsTreeList.DataSource = DB.SkladBase().GetManufactureTree(DBHelper.CurrentUser.UserId).ToList();
                DocsTreeList.ExpandAll(); //ExpandToLevel(0);
            }
        }

        void GetWBListMake()
        {
            if (wbSatusList.EditValue == null || WhComboBox.EditValue == null || DocsTreeList.FocusedNode == null)
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;

            var dr = WbGridView.GetRow(WbGridView.FocusedRowHandle) as WBListMake_Result;

            int top_row = WbGridView.TopRowIndex;
            WBListMakeBS.DataSource = DB.SkladBase().WBListMake(satrt_date, end_date, (int)wbSatusList.EditValue, WhComboBox.EditValue.ToString(), focused_tree_node.Num, -20).ToList();
            WbGridView.TopRowIndex = top_row;
        }

        void GetDeboningList()
        {
            if (DebSatusList.EditValue == null || DebWhComboBox.EditValue == null || DocsTreeList.FocusedNode == null)
            {
                return;
            }

            var satrt_date = DebStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : DebStartDate.DateTime;
            var end_date = DebEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : DebEndDate.DateTime;

            int top_row = DeboningGridView.TopRowIndex;
            DeboningBS.DataSource = DB.SkladBase().WBListMake(satrt_date, end_date, (int)DebSatusList.EditValue, DebWhComboBox.EditValue.ToString(), focused_tree_node.Num, -22).ToList();
            DeboningGridView.TopRowIndex = top_row;
        }

        void GetProductionPlans()
        {
            if (lookUpEdit2.EditValue == null /*|| DebWhComboBox.EditValue == null*/ || DocsTreeList.FocusedNode == null)
            {
                return;
            }

            var satrt_date = PlanStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : PlanStartDate.DateTime;
            var end_date = PlanEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : PlanEndDate.DateTime;

            int top_row = ProductionPlansGridView.TopRowIndex;
            ProductionPlansBS.DataSource = DB.SkladBase().ProductionPlansList(satrt_date, end_date, (int)lookUpEdit2.EditValue, DBHelper.CurrentUser.KaId).ToList();
            ProductionPlansGridView.TopRowIndex = top_row;
        }

        private void DocsTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            CopyItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;
            AddTechProcBtn.Enabled = false;
            DelTechProcBtn.Enabled = false;
            EditTechProcBtn.Enabled = false; 
            focused_tree_node = DocsTreeList.GetDataRecordByNode(e.Node) as GetManufactureTree_Result;

            _cur_wtype = focused_tree_node.WType != null ? focused_tree_node.WType.Value : 0;
            RefrechItemBtn.PerformClick();

            wbContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;

            if (focused_tree_node.FunId != null)
            {
                History.AddEntry(new HistoryEntity { FunId = focused_tree_node.FunId.Value, MainTabs = 1 });
            }
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            bar1.Visible = true;

            switch (focused_tree_node.GType.Value)
            {
                case 1:
                    GetWBListMake();
                    break;

                case 2:
                    whUserControl.set_tree_node = focused_tree_node.Id;
                    whUserControl.WHTreeList.FocusedNode = whUserControl.WHTreeList.FindNodeByFieldValue("Id", focused_tree_node.Id);
                    bar1.Visible = false;
                    whUserControl.splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
                    break;

                case 3:
                    GetDeboningList();
                    break;

                case 4:
                    GetProductionPlans();
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
                        ManufDocEdit.WBEdit(WbGridView.GetFocusedRow() as WBListMake_Result);
                        break;

                    case 3:
                        ManufDocEdit.WBEdit(DeboningGridView.GetFocusedRow() as WBListMake_Result);
                        break;

                    case 4:
                        var row = ProductionPlansGridView.GetFocusedRow() as ProductionPlansList_Result;

                        using (var f = new frmProductionPlans(row.Id))
                        {
                            f.ShowDialog();
                        }

                        break;

                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WbGridView.GetRow(WbGridView.FocusedRowHandle) as WBListMake_Result;

            if (dr == null)
            {
                return;
            }

            using (var f = new frmTechProcDet(dr.WbillId))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (DB.SkladBase().WaybillList.Any(a => a.WbillId == dr.WbillId))
                    {
                        RefreshTechProcDet(dr.WbillId);
                    }
                    else
                    {
                        RefrechItemBtn.PerformClick();
                    }
                }
            }

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = TechProcGridView.GetRow(TechProcGridView.FocusedRowHandle) as v_TechProcDet;
            if (dr != null)
            {
                using (var f = new frmTechProcDet(dr.WbillId, dr.DetId))
                {
                    var row = WbGridView.GetFocusedRow() as WBListMake_Result;
                    f.OkButton.Enabled = ( row.Checked != 1 );
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        RefreshTechProcDet(dr.WbillId);
                    }
                }
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Ви дійсно бажаєте видалити запис !", "Видалення запису", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                var dr = TechProcGridView.GetRow(TechProcGridView.FocusedRowHandle) as v_TechProcDet;
                DB.SkladBase().DeleteWhere<TechProcDet>(w => w.DetId == dr.DetId).SaveChanges();

                RefreshTechProcDet(dr.WbillId);
            }
        }

        private void RefreshTechProcDet(int wbill_id)
        {
            TechProcDetBS.DataSource = DB.SkladBase().v_TechProcDet.Where(w => w.WbillId == wbill_id).OrderBy(o => o.Num).ToList();
        }

        private void TechProcGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditTechProcBtn.PerformClick();
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
                    case 3:
                    case 1:
                        if (focused_row == null)
                        {
                            return;
                        }

                        var wb = db.WaybillList.Find(focused_row.WbillId);
                        if (wb == null)
                        {
                            MessageBox.Show(Resources.not_find_wb);
                            return;
                        }
                        if (wb.SessionId != null)
                        {
                            MessageBox.Show(Resources.deadlock);
                            return;
                        }


                        if (wb.Checked == 2)
                        {
                            if ((wb.WType == -20 && (focused_row.ShippedAmount ?? 0) == 0) || wb.WType == -22)
                            {
                                DBHelper.StornoOrder(db, focused_row.WbillId);
                            }
                            else if (wb.WType == -20)
                            {
                                MessageBox.Show("Частина товару вже відгружена на склад!");
                            }
                        }
                        else
                        {
                            DBHelper.ExecuteOrder(db, focused_row.WbillId);
                        }

                        break;

                    case 4:
                        if (pp_focused_row == null)
                        {
                            return;
                        }

                        var pp = db.ProductionPlans.Find(pp_focused_row.Id);

                        if (pp == null)
                        {
                            MessageBox.Show(Resources.not_find_wb);
                            return;
                        }
                        if (pp.SessionId != null)
                        {
                            MessageBox.Show(Resources.deadlock);
                            return;
                        }

                        var rel = db.GetRelDocList(pp_focused_row.Id).OrderBy(o => o.OnDate).ToList();
                        if (rel.Any())
                        {
                            MessageBox.Show(Resources.not_storno_wb, "Відміна проводки", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (pp.Checked == 1 )
                        {
                            pp.Checked = 0;
                        }

                        db.SaveChanges();
                        break;
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_row == null && pp_focused_row == null)
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
                            case 3:

                            case 1:
                                var wb = db.WaybillList.FirstOrDefault(w => w.WbillId == focused_row.WbillId && w.SessionId == null);
                                if (wb != null)
                                {
                                    db.WaybillList.Remove(wb);
                                }
                                else
                                {
                                    MessageBox.Show(Resources.deadlock);
                                }
                                break;

                            case 4:
                                var pp = db.ProductionPlans.FirstOrDefault(w=> w.Id == pp_focused_row.Id && w.SessionId == null);
                                if (pp != null)
                                {
                                    db.ProductionPlans.Remove(pp);
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
                var wbl = db.WaybillList.FirstOrDefault(w => w.WbillId == focused_row.WbillId);
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

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            GetWBListMake();
        }

        private void DebStartDate_EditValueChanged(object sender, EventArgs e)
        {
            GetDeboningList();
        }

        private void WbGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                popupMenu1.ShowPopup(p2);
            }
        }

        private void PrintItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintDoc.Show(focused_row.Id, focused_row.WType, DB.SkladBase());
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowOrdered(0, 0, focused_row.MatId);
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 1:
                    var doc = DB.SkladBase().DocCopy(focused_row.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();
                    using (var wb_in = new frmWBManufacture(doc.out_wbill_id))
                    {
                        wb_in.is_new_record = true;
                        wb_in.ShowDialog();
                    }
                    break;

                case 3:
                    var doc2 = DB.SkladBase().DocCopy(focused_row.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();
                    using (var wb_in = new frmWBDeboning(doc2.out_wbill_id))
                    {
                        wb_in.is_new_record = true;
                        wb_in.ShowDialog();
                    }
                    break;
            }

            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetRelDocList_Result row = new GetRelDocList_Result();

            if (gridView3.Focus())
            {
                row = gridView3.GetFocusedRow() as GetRelDocList_Result;
            }
            else if (gridView5.Focus())
            {
                row = gridView5.GetFocusedRow() as GetRelDocList_Result;
            }
            else if (gridView7.Focus())
            {
                row = gridView7.GetFocusedRow() as GetRelDocList_Result;
            }

            FindDoc.Find(row.Id, row.DocType, row.OnDate);
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
            GetRelDocList_Result row = new GetRelDocList_Result();

            if (gridView3.Focus())
            {
                row = gridView3.GetFocusedRow() as GetRelDocList_Result;
            }
            else if (gridView5.Focus())
            {
                row = gridView5.GetFocusedRow() as GetRelDocList_Result;
            }

            PrintDoc.Show(row.Id, row.DocType.Value, DB.SkladBase());
        }

        private void WbGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            focused_row = e.Row as WBListMake_Result;

            xtraTabControl2_SelectedPageChanged(sender, null);

            StopProcesBtn.Enabled = (focused_row != null && focused_row.Checked == 2 && focused_tree_node.CanPost == 1);
            DeleteItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && focused_tree_node.CanDelete == 1);
            EditItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && focused_tree_node.CanModify == 1);
            AddTechProcBtn.Enabled = (focused_row != null && focused_row.Checked != 1 && focused_tree_node.CanModify == 1);
            DelTechProcBtn.Enabled = (AddTechProcBtn.Enabled && TechProcGridView.DataRowCount > 0);
            EditTechProcBtn.Enabled = (focused_row != null && focused_tree_node.CanModify == 1 && TechProcGridView.DataRowCount > 0 /*&& focused_row.Checked != 1*/); 
            CopyItemBtn.Enabled = (focused_row != null && focused_tree_node.CanModify == 1);
            //  OkButton->Enabled =  !WayBillList->IsEmpty();
            ExecuteItemBtn.Enabled = (focused_row != null && focused_tree_node.CanPost == 1);
            PrintItemBtn.Enabled = (focused_row != null);
        }

        private void DeboningGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            focused_row = ((GridView)sender).GetRow(e.FocusedRowHandle) as WBListMake_Result;

            xtraTabControl1_SelectedPageChanged(sender, null);

            StopProcesBtn.Enabled = (focused_row != null && focused_row.Checked == 2 && focused_tree_node.CanPost == 1);
            DeleteItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && focused_tree_node.CanDelete == 1);
            EditItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && focused_tree_node.CanModify == 1);
            CopyItemBtn.Enabled = (focused_row != null && focused_tree_node.CanModify == 1);
            ExecuteItemBtn.Enabled = (focused_row != null && focused_tree_node.CanPost == 1);
            PrintItemBtn.Enabled = (focused_row != null);
        }

        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             var dr = WbGridView.GetRow(WbGridView.FocusedRowHandle) as WBListMake_Result;

             var f = new frmWayBillMakePropsDet(dr.WbillId);
             if (f.ShowDialog() == DialogResult.OK)
             {
                 RefreshAtribute(dr.WbillId);
             }
        }

        private void RefreshAtribute(int wbill_id)
        {
            AttributeGridControl.DataSource = DB.SkladBase().WayBillMakeProps.Where(w => w.WbillId == wbill_id).Select(s => new
            {
                s.Id,
                s.Materials.Name,
                s.OnDate,
                s.Amount,
                PersonName = s.Kagent.Name,
                s.WbillId,
                Weight = s.Amount * s.Materials.Weight
            }).OrderBy(o => o.OnDate).ToList();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = AttributeGridView.GetFocusedRow() as dynamic;
            int id = dr.Id;

            DB.SkladBase().DeleteWhere<WayBillMakeProps>(w => w.Id == id);

            RefreshAtribute(dr.WbillId);
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = AttributeGridView.GetFocusedRow() as dynamic;
            if (dr != null)
            {
                var f = new frmWayBillMakePropsDet(dr.WbillId, dr.Id);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    RefreshAtribute(dr.WbillId);
                }
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var path = Path.Combine(Application.StartupPath, "expotr.xlsx");
            WbGridView.ExportToXlsx(path);

            Process.Start(path);
        }

        private void ProductionPlansGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            pp_focused_row = ((GridView)sender).GetRow(e.FocusedRowHandle) as ProductionPlansList_Result;

            if (pp_focused_row != null)
            {
                using (var db = DB.SkladBase())
                {
                    gridControl6.DataSource = db.v_ProductionPlanDet.Where(w => w.ProductionPlanId == pp_focused_row.Id).OrderBy(o => o.Num).ToList();
                    gridControl7.DataSource = db.GetRelDocList(pp_focused_row.Id).OrderBy(o => o.OnDate).ToList();
                }
            }
            else
            {
                gridControl6.DataSource = null;
                gridControl7.DataSource = null;
              
            }

            DeleteItemBtn.Enabled = (pp_focused_row != null && pp_focused_row.Checked == 0 && focused_tree_node.CanDelete == 1);
            EditItemBtn.Enabled = (pp_focused_row != null && pp_focused_row.Checked == 0 && focused_tree_node.CanModify == 1);
            CopyItemBtn.Enabled = (pp_focused_row != null && focused_tree_node.CanModify == 1);
            ExecuteItemBtn.Enabled = (pp_focused_row != null && focused_tree_node.CanPost == 1);
            PrintItemBtn.Enabled = (pp_focused_row != null);
        }

        private void PlanStartDate_EditValueChanged(object sender, EventArgs e)
        {
            GetProductionPlans();
        }

        private void TechProcGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            using (var db = new BaseEntities())
            {
                if (e.Column.FieldName == "Notes")
                {
                    var row = TechProcGridView.GetFocusedRow() as v_TechProcDet;
                    var wbd = db.TechProcDet.FirstOrDefault(w => w.DetId == row.DetId);
                    wbd.Notes = Convert.ToString(e.Value);
                }

                db.SaveChanges();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            using (var frm = new frmSchedulingOrders())
            {
                frm.ShowDialog();
            }
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (focused_row == null)
            {
                TechProcDetBS.DataSource = null;
                gridControl2.DataSource = null;
                gridControl3.DataSource = null;
                ManufacturedPosGridControl.DataSource = null;
                return;
            }

            using (var db = DB.SkladBase())
            {
                switch (xtraTabControl2.SelectedTabPageIndex)
                {
                    case 0:
                        RefreshTechProcDet(focused_row.WbillId);
                        break;

                    case 1:
                        RefreshAtribute(focused_row.WbillId);
                        break;

                    case 2:
                        gridControl2.DataSource = db.GetWayBillDetOut(focused_row.WbillId).ToList().OrderBy(o => o.Num).ToList();
                        gridView2.ExpandAllGroups();
                        break;

                    case 3:
                        gridControl3.DataSource = db.GetRelDocList(focused_row.Id).OrderBy(o => o.OnDate).ToList();
                        break;

                    case 4:
                        ManufacturedPosGridControl.DataSource = db.GetManufacturedPos(focused_row.Id).ToList();
                        break;
                }
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (focused_row == null)
            {
                gridControl4.DataSource = null;
                gridControl5.DataSource = null;
                return;
            }

            using (var db = DB.SkladBase())
            {
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        DeboningDetGridControl.DataSource = db.DeboningDet.Where(w => w.WBillId == focused_row.WbillId).Select(s => new SP_Sklad.WBForm.frmWBDeboning.DeboningDetList
                        {
                            DebId = s.DebId,
                            WBillId = s.WBillId,
                            MatId = s.MatId,
                            Amount = s.Amount,
                            Price = s.Price,
                            WId = s.WId,
                            MatName = s.Materials.Name,
                            Total = s.Amount * s.Price,
                            WhName = s.Warehouse.Name
                        }).ToList();
                        break;

                    case 1:
                        gridControl4.DataSource = db.GetWayBillDetOut(focused_row.WbillId).ToList().OrderBy(o => o.Num).ToList();
                        break;

                    case 2:
                        gridControl5.DataSource = db.GetRelDocList(focused_row.Id).OrderBy(o => o.OnDate).ToList();
                        break;
                }
            }
        }
    }
}
