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

namespace SP_Sklad.MainTabs
{
    public partial class WarehouseUserControl : UserControl
    {
        int wid = 0;
        string wh_list = "*";
        int cur_wtype = 0;

        public bool isDirectList { get; set; }
        public bool isMatList { get; set; }
        public List<CustomMatListWH> custom_mat_list { get; set; }
        public WaybillList wb { get; set; }
        public Object resut { get; set; }

        public class CustomMatListWH : CustomMatList
        {
            public decimal? Discount { get; set; }
            public int? PTypeId { get; set; }
        }

        GetWhTree_Result focused_tree_node { get; set; }
        GetWayBillListWh_Result focused_row { get; set; }

        public WarehouseUserControl()
        {
            InitializeComponent();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void WarehouseUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                custom_mat_list = new List<CustomMatListWH>();
                MatListGridControl.DataSource = custom_mat_list;

                whContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
                OnDateEdit.DateTime = DateTime.Now;

                whKagentList.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().Kagent.Select(s => new { s.KaId, s.Name }));
                whKagentList.EditValue = 0;

                WhComboBox.Properties.DataSource = new List<object>() { new { WId = "*", Name = "Усі" } }.Concat(new BaseEntities().Warehouse.Select(s => new { WId = s.WId.ToString(), s.Name }).ToList());
                WhComboBox.EditValue = "*";

                wbSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                wbSatusList.EditValue = -1;

                wbStartDate.EditValue = DateTime.Now.AddDays(-30);
                wbEndDate.EditValue = DateTime.Now;

                repositoryItemLookUpEdit1.DataSource = DBHelper.WhList();
                repositoryItemLookUpEdit2.DataSource = DB.SkladBase().PriceTypes.ToList();

                GetTree(1);
            }
        }

        void GetTree(int type)
        {
            WHTreeList.DataSource = new BaseEntities().GetWhTree(DBHelper.CurrentUser.UserId, type).ToList();
            WHTreeList.ExpandToLevel(0);
        }

        private void WHTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            CopyItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;
            focused_tree_node = WHTreeList.GetDataRecordByNode(e.Node) as GetWhTree_Result;

            cur_wtype = focused_tree_node.WType != null ? focused_tree_node.WType.Value : 0;
            RefrechItemBtn.PerformClick();
 
            whContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;
        }

        void GetWayBillList(int? wtyp)
        {
            if (wtyp == null && wbSatusList.EditValue == null || WhComboBox.EditValue == null || WHTreeList.FocusedNode == null)
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;

            var dr = WbGridView.GetRow(WbGridView.FocusedRowHandle) as GetWayBillListWh_Result;

            WBGridControl.DataSource = null;
            WBGridControl.DataSource = DB.SkladBase().GetWayBillListWh(satrt_date.Date, end_date.Date.AddDays(1), wtyp, (int)wbSatusList.EditValue, WhComboBox.EditValue.ToString()).ToList().OrderByDescending(o => o.OnDate);

            WbGridView.FocusedRowHandle = FindRowHandleByRowObject(WbGridView, dr);
        }

        private int FindRowHandleByRowObject(GridView view, GetWayBillListWh_Result dr)
        {
            if (dr != null)
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    if (dr.WBillId == (view.GetRow(i) as GetWayBillListWh_Result).WBillId)
                    {
                        return i;
                    }
                }
            }
            return GridControl.InvalidRowHandle;
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
                RemainOnWhGrid.DataSource = DB.SkladBase().WMatGetByWh(dr.MatId, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, wh_list).ToList();
                PosGridControl.DataSource = DB.SkladBase().PosGet(dr.MatId, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, 0, wh_list).ToList();
            }
            else
            {
                RemainOnWhGrid.DataSource = null;
                PosGridControl.DataSource = null;
            }
        }

        private void PosGridControl_Click(object sender, EventArgs e)
        {

        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 2:
                    if (focused_tree_node.Id == 48)
                    {
                        using (var wb_move = new frmWayBillMove())
                        {
                            wb_move.ShowDialog();
                        }
                    }

                    if (focused_tree_node.Id == 58)
                    {
                        using (var wb_on = new frmWBWriteOn())
                        {
                            wb_on.ShowDialog();
                        }
                    }

                    if (focused_tree_node.Id == 54)
                    {
                        using (var wb_on = new frmWBWriteOff())
                        {
                            wb_on.ShowDialog();
                        }
                    }
                    break;
            }

            RefrechItemBtn.PerformClick();
        }

        private void WbGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focused_row = WbGridView.GetRow(e.FocusedRowHandle) as GetWayBillListWh_Result;

            if (focused_row != null)
            {
                using (var db = DB.SkladBase())
                {
                    gridControl2.DataSource = db.GetWaybillDetIn(focused_row.WBillId).ToList();
                    gridControl3.DataSource = db.GetRelDocList(focused_row.DocId).ToList();
                }
            }
            else
            {
                gridControl2.DataSource = null;
                gridControl3.DataSource = null;
            }

            DeleteItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && focused_tree_node.CanDelete == 1);
            ExecuteItemBtn.Enabled = (focused_row != null && focused_row.WType != 2 && focused_row.WType != -16 && focused_row.WType != 16 && focused_tree_node.CanPost == 1);
            EditItemBtn.Enabled = (focused_row != null && focused_tree_node.CanModify == 1);
            CopyItemBtn.Enabled = EditItemBtn.Enabled;
            PrintItemBtn.Enabled = (focused_row != null);
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            using (var db = new BaseEntities())
            {
                switch (focused_tree_node.GType)
                {
                    case 2:
                        WhDocEdit.WBEdit(WbGridView.GetFocusedRow() as GetWayBillListWh_Result);
                        break;

                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WbGridView.GetFocusedRow() as GetWayBillListWh_Result;
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
                        case 2: db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK) where WbillId = {0}", dr.WBillId).FirstOrDefault();
                            break;
                    }
                    if (MessageBox.Show(Resources.delete_wb, "Відалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        switch (focused_tree_node.GType)
                        {
                            case 2:
                                db.DeleteWhere<WaybillList>(w=> w.WbillId ==  dr.WBillId.Value);
                                break;

                        }
                        db.SaveChanges();
                    }
                }
                catch
                {
                    MessageBox.Show(Resources.deadlock);
                }
                /*    finally
                    {
               //         trans.Commit();
                    }*/
            }

            RefrechItemBtn.PerformClick();
        }

        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = view.CalcHitInfo(pt);

            if (info.InRow || info.InRowCell)
            {
                EditItemBtn.PerformClick();
            }
        }

        private void ExecuteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = new BaseEntities())
            {
                switch (focused_tree_node.GType)
                {
                    case 2:
                        if (focused_row == null)
                        {
                            return;
                        }
                        
                        var wb = db.WaybillList.Find(focused_row.WBillId);
                        if (wb != null)
                        {
                            if (wb.Checked == 1)
                            {
                                DBHelper.StornoOrder(db, focused_row.WBillId.Value);
                            }
                            else
                            {
                                DBHelper.ExecuteOrder(db, focused_row.WBillId.Value);
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

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            switch (focused_tree_node.GType.Value)
            {
                case 1:
                    int grp_id = 0;

                    string grp = "";

                    if (ByGrpBtn.Down) grp_id = focused_tree_node.Num;
                    else wid = focused_tree_node.Num;

                    if (ViewDetailTree.Down && ByGrpBtn.Down && focused_tree_node.Num != 0)
                    {
                        grp = focused_tree_node.Num.ToString();
                    }

                  //  WhMatGridControl.DataSource = null;
                  //  WhMatGridControl.DataSource = DB.SkladBase().WhMatGet(grp_id, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, 0, wh_list, 0, grp, DBHelper.CurrentUser.UserId, ViewDetailTree.Down ? 1:0).ToList();
                    WhMatGetBS.DataSource = null;
                    WhMatGetBS.DataSource = DB.SkladBase().WhMatGet(grp_id, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, 0, wh_list, 0, grp, DBHelper.CurrentUser.UserId, ViewDetailTree.Down ? 1 : 0).ToList();
                    break;

                case 2:
                    GetWayBillList(focused_tree_node.WType);
                    /* cxGrid1Level1->GridView = cxGrid1DBTableView1;
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
                      WayBillList->Refresh();*/

                    break;
            }
        }

        private void RefreshWhBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ViewDetailTree_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ByGrpBtn.Down = true;
            RefrechItemBtn.PerformClick();
        }

        private void WhMatGridView_DoubleClick(object sender, EventArgs e)
        {
            if (isMatList)
            {
                AddItem.PerformClick();
            }
            else if (isDirectList)
            {
                var frm = this.Parent as frmCatalog;
                frm.OkButton.PerformClick();
            }
            else
            {
                var row = WhMatGridView.GetFocusedRow() as WhMatGet_Result;
                IHelper.ShowTurnMaterial(row.MatId.Value);
            }
        }

        private void AddItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = WhMatGridView.GetFocusedRow() as WhMatGet_Result;
            var wh_row = WhRemainGridView.GetFocusedRow() as WMatGetByWh_Result;
            var t = (wb.Kagent != null ? wb.Kagent.PTypeId : null);
            var price = DB.SkladBase().GetListMatPrices(row.MatId, wb.CurrId).FirstOrDefault(w => w.PType == t);

            custom_mat_list.Add(new CustomMatListWH
            {
                Num = custom_mat_list.Count() + 1,
                MatId = row.MatId.Value,
                Name = row.MatName,
                Amount = 1,
                Price = price != null ? price.Price : 0,
                WId = wh_row != null ? wh_row.WId.Value : DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId,
                PTypeId = price != null ? price.PType : null,
                Discount = DB.SkladBase().GetDiscount(wb.KaId, row.MatId).FirstOrDefault() ?? 0.00m
            });

            MatListGridView.RefreshData();
        }

        private void DelItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MatListGridView.DeleteSelectedRows();
        }

        private void whKagentList_EditValueChanged(object sender, EventArgs e)
        {
            RefrechItemBtn.PerformClick();
        }

        private void BarCodeEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !String.IsNullOrEmpty(BarCodeEdit.Text))
            {
                var BarCodeSplit = BarCodeEdit.Text.Split('+');
                String kod = BarCodeSplit[0];

                var row = WhMatGetBS.List.OfType<WhMatGet_Result>().ToList().Find(f => f.BarCode == kod);
                var pos = WhMatGetBS.IndexOf(row);
                WhMatGetBS.Position = pos;

                if (row != null && MatListTabPage.PageVisible)
                {
                    if (BarCodeSplit.Count() > 2)
                    {
                        var wh_row = WhRemainGridView.GetFocusedRow() as WMatGetByWh_Result;

                        custom_mat_list.Add(new CustomMatListWH
                        {
                            Num = custom_mat_list.Count() + 1,
                            MatId = row.MatId.Value,
                            Name = row.MatName,
                            Amount = 1,
                            Price = Convert.ToDecimal(BarCodeSplit[1] + "," + BarCodeSplit[2]),
                            WId = wh_row != null ? wh_row.WId.Value : DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId,
                            PTypeId = null,
                            Discount = DB.SkladBase().GetDiscount(wb.KaId, row.MatId).FirstOrDefault() ?? 0.00m
                        });

                        MatListGridView.RefreshData();

                    }
                    else
                    {
                        AddItem.PerformClick();
                    }
                      
                }


                BarCodeEdit.Text = "";
            }
        }

    }
}
