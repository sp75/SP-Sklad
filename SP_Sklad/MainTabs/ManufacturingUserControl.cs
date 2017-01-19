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


namespace SP_Sklad.MainTabs
{
    public partial class ManufacturingUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        GetManufactureTree_Result focused_tree_node { get; set; }
        WBListMake_Result focused_row { get; set; }
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

                WhComboBox.Properties.DataSource = new List<object>() { new { WId = "*", Name = "Усі" } }.Concat(DBHelper.WhList().Select(s => new { WId = s.WId.ToString(), s.Name }).ToList());
                WhComboBox.EditValue = "*";
                DebWhComboBox.Properties.DataSource = WhComboBox.Properties.DataSource;
                DebWhComboBox.EditValue = "*";

                wbSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 0, Name = "Актуальний" }, new { Id = 2, Name = "Розпочато виробництво" }, new { Id = 1, Name = "Закінчено виробництво" } };
                wbSatusList.EditValue = -1;
                DebSatusList.Properties.DataSource = wbSatusList.Properties.DataSource;
                DebSatusList.EditValue = -1;

                wbStartDate.EditValue = DateTime.Now.AddDays(-30);
                wbEndDate.EditValue = DateTime.Now;
                DebStartDate.EditValue = DateTime.Now.AddDays(-30);
                DebEndDate.EditValue = DateTime.Now;

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
            WBListMakeBS.DataSource = DB.SkladBase().WBListMake(satrt_date.Date, end_date.Date.AddDays(1), (int)wbSatusList.EditValue, WhComboBox.EditValue.ToString(), focused_tree_node.Num, -20).ToList();
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
            DeboningBS.DataSource = DB.SkladBase().WBListMake(satrt_date.Date, end_date.Date.AddDays(1), (int)DebSatusList.EditValue, DebWhComboBox.EditValue.ToString(), focused_tree_node.Num, -22).ToList();
            DeboningGridView.TopRowIndex = top_row;
        }

        private void DocsTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            CopyItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;
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

            switch (focused_tree_node.GType.Value)
            {
                case 1: bar1.Visible = true;
                    GetWBListMake();
                    break;

                case 2:
                    whUserControl.set_tree_node = focused_tree_node.Id;
                    whUserControl.WHTreeList.FocusedNode = whUserControl.WHTreeList.FindNodeByFieldValue("Id", focused_tree_node.Id);
                    bar1.Visible = false;
                    whUserControl.splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
                    break;

                case 3:
                    bar1.Visible = true;
                    GetDeboningList();
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

                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             var dr = WbGridView.GetRow(WbGridView.FocusedRowHandle) as WBListMake_Result;

             var f = new frmTechProcDet(dr.WbillId);
             if (f.ShowDialog() == DialogResult.OK)
             {
                 RefreshTechProcDet(dr.WbillId);
             }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = TechProcGridView.GetRow(TechProcGridView.FocusedRowHandle) as v_TechProcDet;
            if (dr != null)
            {
                var f = new frmTechProcDet(dr.WbillId, dr.DetId);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    RefreshTechProcDet(dr.WbillId);
                }
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             var dr = TechProcGridView.GetRow(TechProcGridView.FocusedRowHandle) as v_TechProcDet;
             DB.SkladBase().DeleteWhere<TechProcDet>(w => w.DetId == dr.DetId).SaveChanges();

             RefreshTechProcDet(dr.WbillId);
        }

        private void RefreshTechProcDet(int wbill_id)
        {
            TechProcGridControl.DataSource = DB.SkladBase().v_TechProcDet.Where(w => w.WbillId == wbill_id).OrderBy(o => o.OnDate).ToList(); 
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
                        if (wb != null)
                        {
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
                        }
                        else
                        {
                            MessageBox.Show(Resources.not_find_wb);
                        }
                        break;
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_row == null)
            {
                return;
            }

            using (var db = new BaseEntities())
            {
                try
                {
                    switch (focused_tree_node.GType)
                    {
                        case 3:
                        case 1: db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK) where WbillId = {0}", focused_row.WbillId).FirstOrDefault();
                            break;
                    }

                    if (MessageBox.Show(Resources.delete_wb, "Відалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        switch (focused_tree_node.GType)
                        {
                            case 3:
                            case 1:
                                db.DeleteWhere<WaybillList>(w => w.WbillId == focused_row.WbillId);
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
            var wbl = DB.SkladBase().WaybillList.FirstOrDefault(w=> w.WbillId == focused_row.WbillId);
            if(wbl == null)
            {
                return;
            }

            if (wbl.Checked == 2 )
            {
                using (var f = new frmWBWriteOn())
                {
                    var result = f._db.ExecuteWayBill(wbl.WbillId, null).ToList().FirstOrDefault();
                 //   f.NumEdit.Text = new BaseEntities().GetCounter("wb_write_on").FirstOrDefault();
                    f.doc_id = result.NewDocId;
                    f.TurnDocCheckBox.Checked = true;
                    f.ShowDialog();
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
                    var doc = DB.SkladBase().DocCopy(focused_row.Id).FirstOrDefault();
                    using (var wb_in = new frmWBManufacture(doc.out_wbill_id))
                    {
                        wb_in.ShowDialog();
                    }
                    break;

                case 3:
                    var doc2 = DB.SkladBase().DocCopy(focused_row.Id).FirstOrDefault();
                    using (var wb_in = new frmWBDeboning(doc2.out_wbill_id))
                    {
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

            PrintDoc.Show(row.Id.Value, row.DocType.Value, DB.SkladBase());
        }

        private void WbGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            focused_row = e.Row as WBListMake_Result;

            if (focused_row != null)
            {
                using (var db = DB.SkladBase())
                {
                    TechProcGridControl.DataSource = db.v_TechProcDet.Where(w => w.WbillId == focused_row.WbillId).OrderBy(o => o.Num).ToList();
                    gridControl2.DataSource = db.GetWayBillDetOut(focused_row.WbillId).ToList();
                    gridView2.ExpandAllGroups();
                    gridControl3.DataSource = db.GetRelDocList(focused_row.Id).OrderBy(o=> o.OnDate).ToList();
                    ManufacturedPosGridControl.DataSource = db.GetManufacturedPos(focused_row.Id).ToList();
                }
            }
            else
            {
                TechProcGridControl.DataSource = null;
                gridControl2.DataSource = null;
                gridControl3.DataSource = null;
                ManufacturedPosGridControl.DataSource = null;
            }

            StopProcesBtn.Enabled = (focused_row != null && focused_row.Checked == 2 && focused_tree_node.CanPost == 1);
            DeleteItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && focused_tree_node.CanDelete == 1);
            EditItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && focused_tree_node.CanModify == 1);
            AddTechProcBtn.Enabled = (focused_row != null && focused_row.Checked != 1 && focused_tree_node.CanModify == 1);
            DelTechProcBtn.Enabled = (AddTechProcBtn.Enabled && TechProcGridView.DataRowCount > 0);
            EditTechProcBtn.Enabled = DelTechProcBtn.Enabled;
            CopyItemBtn.Enabled = (focused_row != null && focused_tree_node.CanModify == 1);
            //  OkButton->Enabled =  !WayBillList->IsEmpty();
            ExecuteItemBtn.Enabled = (focused_row != null && focused_tree_node.CanPost == 1);
            PrintItemBtn.Enabled = (focused_row != null);
        }

        private void DeboningGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            focused_row = ((GridView)sender).GetRow(e.FocusedRowHandle) as WBListMake_Result;

            if (focused_row != null)
            {
                using (var db = DB.SkladBase())
                {
                    gridControl4.DataSource = db.GetWayBillDetOut(focused_row.WbillId).ToList();
                    gridControl5.DataSource = db.GetRelDocList(focused_row.Id).OrderBy(o => o.OnDate).ToList();

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
                }
            }
            else
            {
                gridControl4.DataSource = null;
                gridControl5.DataSource = null;
            }

            StopProcesBtn.Enabled = (focused_row != null && focused_row.Checked == 2 && focused_tree_node.CanPost == 1);
            DeleteItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && focused_tree_node.CanDelete == 1);
            EditItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && focused_tree_node.CanModify == 1);
            CopyItemBtn.Enabled = (focused_row != null && focused_tree_node.CanModify == 1);
            ExecuteItemBtn.Enabled = (focused_row != null && focused_tree_node.CanPost == 1);
            PrintItemBtn.Enabled = (focused_row != null);
        }

    }
}
