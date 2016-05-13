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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using SP_Sklad.WBForm;
using SP_Sklad.Common;
using SP_Sklad.WBDetForm;
using SP_Sklad.Properties;


namespace SP_Sklad.MainTabs
{
    public partial class ManufacturingUserControl : UserControl
    {
        GetManufactureTree_Result focused_tree_node { get; set; }
        int cur_wtype = 0;

        public ManufacturingUserControl()
        {
            InitializeComponent();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void ManufacturingUserControl_Load(object sender, EventArgs e)
        {
            wbContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
          
       //     wbKagentList.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().Kagent.Select(s => new { s.KaId, s.Name }));
        //    wbKagentList.EditValue = 0;

            WhComboBox.Properties.DataSource = new List<object>() { new { WId = "*", Name = "Усі" } }.Concat(DBHelper.WhList().Select(s => new { WId = s.WId.ToString(), s.Name }).ToList());
            WhComboBox.EditValue = "*";

            wbSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 0, Name = "Актуальний" }, new { Id = 2, Name = "Розпочато виробництво" },new { Id = 1, Name = "Закінчено виробництво" }  };
            wbSatusList.EditValue = -1;

            wbStartDate.EditValue = DateTime.Now.AddDays(-30);
            wbEndDate.EditValue = DateTime.Now;

            DocsTreeList.DataSource = DB.SkladBase().GetManufactureTree(DBHelper.CurrentUser.UserId).ToList();
            DocsTreeList.ExpandAll();
        }

        void GetWayBillList(int wtype)
        {
            if (wbSatusList.EditValue == null || WhComboBox.EditValue == null || DocsTreeList.FocusedNode == null)
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;

            var dr = WbGridView.GetRow(WbGridView.FocusedRowHandle) as WBListMake_Result;

            WBGridControl.DataSource = null;
            WBGridControl.DataSource = DB.SkladBase().WBListMake(satrt_date.Date, end_date.Date.AddDays(1), (int)wbSatusList.EditValue, WhComboBox.EditValue.ToString(), focused_tree_node.Num, wtype).ToList();

            WbGridView.FocusedRowHandle = FindRowHandleByRowObject(WbGridView, dr);
        }

        private int FindRowHandleByRowObject(GridView view, WBListMake_Result dr)
        {
            if (dr != null)
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    if (dr.WbillId == (view.GetRow(i) as WBListMake_Result).WbillId)
                    {
                        return i;
                    }
                }
            }
            return GridControl.InvalidRowHandle;
        }

        private void DocsTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            CopyItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;
            focused_tree_node = DocsTreeList.GetDataRecordByNode(e.Node) as GetManufactureTree_Result;

            cur_wtype = focused_tree_node.WType != null ? focused_tree_node.WType.Value : 0;
            RefrechItemBtn.PerformClick();

            wbContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            switch (focused_tree_node.GType.Value)
            {
                case 3:
                case 1: bar1.Visible = true;
                    GetWayBillList(cur_wtype);

                    break;

                /*    case 2: frmWhPanel->WhTreeData->Locate("ID", ExplorerTreeID->Value, TLocateOptions());
                        dxBarManager1Bar2->Visible = false;
                        break;*/
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
                        break;

                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void WbGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var dr = WbGridView.GetRow(e.FocusedRowHandle) as WBListMake_Result;

            if (dr != null)
            {
                using (var db = DB.SkladBase())
                {
                    TechProcGridControl.DataSource = db.v_TechProcDet.Where(w => w.WbillId == dr.WbillId).OrderBy(o => o.OnDate).ToList(); 
                    gridControl2.DataSource = db.GetWayBillDetOut(dr.WbillId).ToList();
                    gridControl3.DataSource = db.GetRelDocList(dr.DocId).ToList();
                }
            }
            else
            {
                gridControl2.DataSource = null;
                gridControl3.DataSource = null;
            }

            DeleteItemBtn.Enabled = (dr != null && dr.Checked == 0 && focused_tree_node.CanDelete == 1);
            ExecuteItemBtn.Enabled = (dr != null && dr.WType != 2 && dr.WType != -16 && dr.WType != 16 && focused_tree_node.CanPost == 1);
            EditItemBtn.Enabled = (dr != null && focused_tree_node.CanModify == 1);
            CopyItemBtn.Enabled = EditItemBtn.Enabled;
            PrintItemBtn.Enabled = (dr != null);
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
            if (IHelper.isRowDublClick(sender)) barButtonItem2.PerformClick();
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
                        var dr = WbGridView.GetFocusedRow() as WBListMake_Result;
                        if (dr == null)
                        {
                            return;
                        }

                        var wb = db.WaybillList.Find(dr.WbillId);
                        if (wb != null)
                        {
                            if (wb.Checked == 2)
                            {
                                DBHelper.StornoOrder(db, dr.WbillId);
                            }
                            else
                            {
                                DBHelper.ExecuteOrder(db, dr.WbillId);
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
            var dr = WbGridView.GetFocusedRow() as WBListMake_Result;
            if (dr == null)
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
                        case 1: db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK) where WbillId = {0}", dr.WbillId).FirstOrDefault();
                            break;
                    }

                    if (MessageBox.Show(Resources.delete_wb, "Відалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        switch (focused_tree_node.GType)
                        {
                            case 3:
                            case 1:
                                db.DeleteWhere<WaybillList>(w => w.WbillId == dr.WbillId);
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

    }
}
