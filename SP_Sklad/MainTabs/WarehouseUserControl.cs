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


namespace SP_Sklad.MainTabs
{
    public partial class WarehouseUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        int wid = 0;
        string wh_list = "*";
        int cur_wtype = 0;

        public bool isDirectList { get; set; }
        public bool isMatList { get; set; }
        public List<CustomMatListWH> custom_mat_list { get; set; }
        public List<WhMatGet_Result> wh_mat_list { get; set; }
        public WaybillList wb { get; set; }
        public Object resut { get; set; }

        public WhMatGet_Result focused_wh_mat
        {
            get { return WhMatGridView.GetFocusedRow() as WhMatGet_Result; }
        }

        public GetWayBillListWh_Result focused_wb
        {
            get { return WbGridView.GetFocusedRow() as GetWayBillListWh_Result; }
        }

        public DiscCards disc_card { get; set; }

        public class CustomMatListWH : CustomMatList
        {
            public decimal? Discount { get; set; }
            public int? PTypeId { get; set; }
            public decimal? AvgPrice { get; set; }
        }

        GetWhTree_Result focused_tree_node { get; set; }
        GetWayBillListWh_Result focused_row { get; set; }

        public int? set_tree_node { get; set; }

        public WarehouseUserControl()
        {
            InitializeComponent();
            whContentTab.Visible = false;
        }
        public class checkedWhList
        {
            public string WId { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }
        }
        private void WarehouseUserControl_Load(object sender, EventArgs e)
        {
            WbGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + "WarehouseUserControl\\WbGridView");
            WhMatGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + "WarehouseUserControl\\WhMatGridView");

            if (!DesignMode)
            {
                custom_mat_list = new List<CustomMatListWH>();
                MatListGridControl.DataSource = custom_mat_list;

                whContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
                OnDateEdit.DateTime = DateTime.Now;

                whKagentList.Properties.DataSource = DBHelper.KagentsList;// new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().Kagent.Select(s => new { s.KaId, s.Name }));
                if (whKagentList.EditValue == null || whKagentList.EditValue == DBNull.Value)
                {
                    whKagentList.EditValue = 0;
                }

                WhComboBox.Properties.DataSource = new List<checkedWhList>() { new checkedWhList { WId = "*", Name = "Усі", IsChecked = false } }.Concat(new BaseEntities().Warehouse.Select(s => new checkedWhList { WId = s.WId.ToString(), Name = s.Name, IsChecked = false }).ToList());
                WhComboBox.EditValue = "*";


                var whlist = new BaseEntities().Warehouse.Select(s => new checkedWhList { WId = s.WId.ToString(), Name = s.Name, IsChecked = false }).ToList();
                foreach (var item in whlist)
                {
                    checkedComboBoxEdit1.Properties.Items.Add(item.WId, item.Name, item.IsChecked ? CheckState.Checked : CheckState.Unchecked, true);
                }

                wbSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                wbSatusList.EditValue = -1;

                wbStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                wbEndDate.EditValue = DateTime.Now.Date.SetEndDay();

                repositoryItemLookUpEdit1.DataSource = DBHelper.WhList;
                repositoryItemLookUpEdit2.DataSource = DB.SkladBase().PriceTypes.ToList();

                AvgMatPriceGridColumn.Visible = (DBHelper.CurrentUser.ShowPrice == 1);
                AvgMatPriceGridColumn.OptionsColumn.ShowInCustomizationForm = AvgMatPriceGridColumn.Visible;
                SumMatRemainGridColumn.Visible = AvgMatPriceGridColumn.Visible;
                SumMatRemainGridColumn.OptionsColumn.ShowInCustomizationForm = AvgMatPriceGridColumn.Visible;

                if (WHTreeList.DataSource == null)
                {
                    GetTree(1);
                }

                bandedGridColumn2.Caption = $"{bandedGridColumn2.Caption}, {DBHelper.NationalCurrency.ShortName}";

                var user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());

                WbGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
                WhMatGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
            }

            whContentTab.Visible = true;
        }

        void GetTree(int type)
        {
            WHTreeList.DataSource = new BaseEntities().GetWhTree(DBHelper.CurrentUser.UserId, type).ToList();
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

            cur_wtype = focused_tree_node.WType != null ? focused_tree_node.WType.Value : 0;


       /*     if (focused_tree_node.GType.Value == 1 && wh_mat_list != null && ByGrpBtn.Down && wh_mat_list.Any())
            {
                if (ViewDetailTree.Down && ByGrpBtn.Down && focused_tree_node.Num != 0)
                {
                    var grp = DB.SkladBase().GetMatGroupTree(focused_tree_node.Num).ToList().Select(s => s.GrpId);
                    WhMatGetBS.DataSource = wh_mat_list.Where(w => grp.Contains(w.OutGrpId));
                }
                else
                {
                    WhMatGetBS.DataSource = wh_mat_list.Where(w => w.OutGrpId == focused_tree_node.Num || focused_tree_node.Num == 0);
                }
            }
            else
            {
                RefrechItemBtn.PerformClick();
            }*/

            RefrechItemBtn.PerformClick();
 
            whContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;

 
            if (focused_tree_node.FunId != null)
            {
                History.AddEntry(new HistoryEntity { FunId = focused_tree_node.FunId.Value, MainTabs = 2 });


                if (WHTreeList.ContainsFocus)
                {
                    Settings.Default.LastFunId = focused_tree_node.FunId.Value;
                }
            }
        }

        public void GetWayBillList(int? wtyp)
        {
            if (wtyp == null && wbSatusList.EditValue == null || WhComboBox.EditValue == null || WHTreeList.FocusedNode == null)
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;

            int top_row = WbGridView.TopRowIndex;
            GetWayBillListWhBS.DataSource = DB.SkladBase().GetWayBillListWh(satrt_date, end_date, wtyp, (int)wbSatusList.EditValue, WhComboBox.EditValue.ToString(), DBHelper.CurrentUser.KaId).OrderByDescending(o => o.OnDate).ToList();
            WbGridView.TopRowIndex = top_row;
        }

        private void ByGrpBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTree(1);
        }

        private void ByWhBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTree(2);
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

                    if (focused_tree_node.Id == 104)
                    {
                        using (var wb_inv = new frmWBInventory())
                        {
                            wb_inv.ShowDialog();
                        }
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
                    if (MessageBox.Show(Resources.delete_wb, "Видалити докумен", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        switch (focused_tree_node.GType)
                        {
                            case 2:
                                var wb = db.WaybillList.FirstOrDefault(w => w.WbillId == dr.WBillId.Value && w.SessionId == null);
                                if (wb != null)
                                {
                                    db.WaybillList.Remove(wb);
                                }
                                else
                                {
                                    MessageBox.Show(Resources.deadlock);
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

                        if (wb.Checked == 1)
                        {
                            DBHelper.StornoOrder(db, focused_row.WBillId.Value);
                        }
                        else
                        {
                            DBHelper.ExecuteOrder(db, focused_row.WBillId.Value);
                        }

                        break;
                }
            }

            RefrechItemBtn.PerformClick();
        }
        private async Task GetMatOnWh()
        {
            int grp_id = 0;

            string grp = "";

            grp_id = ByGrpBtn.Down ? focused_tree_node.Num : 0;
            wid = ByGrpBtn.Down ? 0 : focused_tree_node.Num;
           
            if(wh_list != "*")
            {
                wid = -1;
            }

            if (ViewDetailTree.Down && ByGrpBtn.Down && focused_tree_node.Num != 0)
            {
                grp = focused_tree_node.Num.ToString();
            }

            WhMatGridView.BeginUpdate();
            int top_row = WhMatGridView.TopRowIndex;
            wh_mat_list = await DB.SkladBase().WhMatGet(grp_id, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, ShowEmptyItemsCheck.Checked ? 1 : 0, wh_list, ShowAllItemsCheck.Checked ? 1 : 0, grp, DBHelper.CurrentUser.UserId, ViewDetailTree.Down ? 1 : 0).ToListAsync();
            WhMatGetBS.DataSource = wh_mat_list;
            WhMatGridView.TopRowIndex = top_row;
            WhMatGridView.EndUpdate();
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
                  /*  int grp_id = 0;

                    string grp = "";

                    grp_id = ByGrpBtn.Down ? focused_tree_node.Num : 0;
                    wid = ByGrpBtn.Down ? 0 : focused_tree_node.Num;
                    if (wid == 0 && WhComboBox2.EditValue.ToString() != "*")
                    {
                        wid = Convert.ToInt32(WhComboBox2.EditValue);
                    }

                    if (ViewDetailTree.Down && ByGrpBtn.Down && focused_tree_node.Num != 0)
                    {
                        grp = focused_tree_node.Num.ToString();
                    }

                    int top_row = WhMatGridView.TopRowIndex;
              //      var wh_ids = String.Join(",", DB.SkladBase().UserAccessWh.Where(w => w.UserId == DBHelper.CurrentUser.UserId).Select(s => s.WId).ToList());
                    wh_mat_list = DB.SkladBase().WhMatGet(grp_id, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, ShowEmptyItemsCheck.Checked ? 1 : 0, wh_list, ShowAllItemsCheck.Checked ? 1 : 0, grp, DBHelper.CurrentUser.UserId, ViewDetailTree.Down ? 1 : 0).ToList();
                    WhMatGetBS.DataSource = wh_mat_list;
                    WhMatGridView.TopRowIndex = top_row;*/
                    var result = GetMatOnWh();
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
                var frm = this.Parent as frmWhCatalog;
                frm.OkButton.PerformClick();
            }
            else
            {
                if (focused_wh_mat != null)
                {
                    IHelper.ShowTurnMaterial(focused_wh_mat.MatId);
                }
            }
        }

        private void AddItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddMatToCustomList(1);
        }

        private void DelItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MatListGridView.CloseEditor();

            var list = custom_mat_list.Where(w => w.Check).ToList();
            list.ForEach(f => { custom_mat_list.Remove(f); });

            MatListGridView.RefreshData();
        }

        private void whKagentList_EditValueChanged(object sender, EventArgs e)
        {
            if (OnDateEdit.ContainsFocus || whKagentList.ContainsFocus )
            {
                RefrechItemBtn.PerformClick();
            }
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
                        var price = Convert.ToDecimal(BarCodeSplit[1] + "," + BarCodeSplit[2]);
                        var frm = new frmWeightEdit(row.MatName);
                        frm.PriceEdit.EditValue = price;

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            var wh_row = WhRemainGridView.GetFocusedRow() as MatRemainByWh_Result;

                            custom_mat_list.Add(new CustomMatListWH
                            {
                                Num = custom_mat_list.Count() + 1,
                                MatId = row.MatId,
                                Name = row.MatName,
                                Amount = frm.AmountEdit.Value,
                                Price = frm.PriceEdit.Value,
                                WId = wh_row != null ? wh_row.WId : DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId,
                                PTypeId = null,
                                Discount = DB.SkladBase().GetDiscount(wb.KaId, row.MatId).FirstOrDefault() ?? 0.00m
                            });

                            MatListGridView.RefreshData();
                        }

                    }
                    else
                    {
                        AddItem.PerformClick();
                    }

                }
                else
                {
                    if (e.KeyChar == 13 && !String.IsNullOrEmpty(BarCodeEdit.Text) && BarCodeEdit.Text.Count() == 13)
                    {
                        var ean13 = new EAN13(BarCodeEdit.Text);

                        var row2 = WhMatGetBS.List.OfType<WhMatGet_Result>().ToList().Find(f => f.Artikul == ean13.artikul);
                        var pos2 = WhMatGetBS.IndexOf(row2);
                        WhMatGetBS.Position = pos2;

                        if (row2 != null && MatListTabPage.PageVisible)
                        {
                            AddMatToCustomList(ean13.amount);
                        }
                    }
                }




                BarCodeEdit.Text = "";
            }
        }

        private void WhMatGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            MatPopupMenu.ShowPopup(Control.MousePosition); 
        }

        private void MatTurnInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_wh_mat == null)
            {
                return;
            }

             IHelper.ShowTurnMaterial(focused_wh_mat.MatId);
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_wh_mat == null)
            {
                return;
            }

            IHelper.ShowMatRSV(focused_wh_mat.MatId, DB.SkladBase());
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_wh_mat == null)
            {
                return;
            }

            IHelper.ShowMatInfo(focused_wh_mat.MatId);
        }

        private void RecalcRemainsMatBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            using (var db = DB.SkladBase())
            {
                db.DeleteWhere<PosRemains>(w => w.MatId == focused_wh_mat.MatId);

                var pos = db.WMatTurn.Where(w => w.MatId == focused_wh_mat.MatId).OrderBy(o => o.OnDate).Select(s => new { s.PosId, s.WId, s.OnDate }).ToList().Distinct();

                foreach (var item in pos)
                {
                    db.SP_RECALC_POSREMAINS(item.PosId, focused_wh_mat.MatId, item.WId, item.OnDate, 0);
                }

                db.SaveChanges();
            }

            //     DB.SkladBase().RecalcRemainsMat(focused_wh_mat.MatId);
            RefreshWhBtn.PerformClick();
        }

        private void RecalcRemainsAllMatBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String errRECALC = "";

            for (int i = 0; WhMatGridView.RowCount > i; ++i)
            {
                var row = WhMatGridView.GetRow(i) as WhMatGet_Result;
                try
                {
                    DB.SkladBase().RecalcRemainsMat(row.MatId);
                }
                catch
                {
                    errRECALC += row.MatName + ", ";
                }
            }
            if (errRECALC != "") MessageBox.Show("Не вдалось перерахувати залишки по деяким позиціям: " + errRECALC);
            else MessageBox.Show("Залишки по всім позиціям перераховано!");

            RefreshWhBtn.PerformClick();
        }

        private void DeboningMatBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_wh_mat == null)
            {
                return;
            }

            var rec = DB.SkladBase().MatRecipe.FirstOrDefault(w => w.MatId == focused_wh_mat.MatId && w.RType == 2);
            var wh_remain =  WhRemainGridView.GetFocusedRow() as MatRemainByWh_Result ;
            if (rec != null)
            {
                using (var f = new frmWBDeboning() { rec_id = rec.RecId, source_wid = wh_remain.WId})
                {
                    f.ShowDialog();
                }

                RefreshWhBtn.PerformClick();
            }
            else MessageBox.Show("Не можливо виконати овалку, не знайдено рецепт!");
        }

        private void ShowEmptyItemsCheck_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshWhBtn.PerformClick();
        }

        private void ShowAllItemsCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshWhBtn.PerformClick();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            GetWhBottomInfo(focused_wh_mat);
        }

        private void GetWhBottomInfo(WhMatGet_Result row)
        {
            if (row == null)
            {
                RemainOnWhGrid.DataSource = null;
                PosGridControl.DataSource = null;
                GetOrderedBS.DataSource = null;
                MatChangeGridControl.DataSource = null;
                return;
            }

            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:

                    RemainOnWhGrid.DataSource = DB.SkladBase().MatRemainByWh(row.MatId, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, wh_list, DBHelper.CurrentUser.UserId).ToList();
                    break;
                case 1:

                    PosGridControl.DataSource = DB.SkladBase().PosGet(row.MatId, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, 0, wh_list, DBHelper.CurrentUser.UserId).OrderBy(o => o.OnDate).ToList();
                    break;
                case 2:

                    GetOrderedBS.DataSource = DB.SkladBase().GetOrdered(row.MatId, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, 0, wh_list).ToList();
                    break;
                case 3:
                    MatChangeGridControl.DataSource = DB.SkladBase().GetMatChange(row.MatId).ToList();
                    break;

                case 4:
                    gridColumn49.ColumnEdit.ReadOnly = !(new UserSettingsRepository(DBHelper.CurrentUser.UserId, DB.SkladBase()).AccessEditPrice);
                    break;

                case 5:
                    gridControl1.DataSource = DB.SkladBase().GetUsedMaterials(row.MatId, OnDateEdit.DateTime.Date.AddDays(1), -1).ToList();
                    break;

                case 6: // Dovgo
                    var data_chart = DB.SkladBase().REP_15(DateTime.Now.AddDays(-90), DateTime.Now, 0, row.MatId).OrderBy(o => o.OnDate).ToList();
                    REP_15BS.DataSource = data_chart;

                    /*     if (data_chart.Count > 0)
                         {
                             StackedBarSeriesView myView = ((StackedBarSeriesView)chartControl1.Series["OutLine"].View);
                             TrendLine tl = (TrendLine)myView.Indicators[0];
                             tl.Point1.Argument = data_chart.Min(m => m.OnDate);
                             tl.Point2.Argument = data_chart.Max(m => m.OnDate);
                         }*/
                    break;

                case 7:
                    var last_price = DB.SkladBase().GetLastPrice(row.MatId, 0, -1, DateTime.Now).FirstOrDefault();
                    ExtMatIfoBS.DataSource = new ExtMatIfo
                    {
                        LastPrice = last_price != null ?  last_price.Price : 0
                    };

                    break;
            }
        }




        private void DocsPopupMenu_Popup(object sender, EventArgs e)
        {
            if (focused_wb.WType != 7)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
        }

        private void WbGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.DocsPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wbl = DB.SkladBase().WaybillList.FirstOrDefault(w => w.WbillId == focused_wb.WBillId);
            if (wbl == null)
            {
                return;
            }

            if (wbl.Checked == 1 && !DB.SkladBase().GetRelDocList(wbl.Id).Any(w => w.DocType == 5) && wbl.WType == 7)
            {
                using (var f = new frmWBWriteOn())
                {
                    var result = f._db.ExecuteWayBill(wbl.WbillId, 5, DBHelper.CurrentUser.KaId).ToList().FirstOrDefault();
                    f.doc_id = result.NewDocId;
                    f.TurnDocCheckBox.Checked = true;
                    f.is_new_record = true;
                    f.ShowDialog();
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wbl = DB.SkladBase().WaybillList.FirstOrDefault(w => w.WbillId == focused_wb.WBillId);
            if (wbl == null)
            {
                return;
            }

            if (wbl.Checked == 1 && !DB.SkladBase().GetRelDocList(wbl.Id).Any(w => w.DocType == -5) && wbl.WType == 7)
            {
                using (var f = new frmWBWriteOff())
                {
                    var result = f._db.ExecuteWayBill(wbl.WbillId, -5, DBHelper.CurrentUser.KaId).ToList().FirstOrDefault();
                    f.doc_id = result.NewDocId;
                    f.TurnDocCheckBox.Checked = true;
                    f.is_new_record = true;
                    f.ShowDialog();
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void PrintItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 2:
                    var dr = WbGridView.GetFocusedRow() as GetWayBillListWh_Result;
                    if (dr == null)
                    {
                        return;
                    }

                    PrintDoc.Show(dr.Id.Value, dr.WType.Value, DB.SkladBase());
                    break;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowOrdered(0, 0, focused_wh_mat.MatId);
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 2:
                    var dr = WbGridView.GetFocusedRow() as GetWayBillListWh_Result;
                    var doc = DB.SkladBase().DocCopy(dr.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();

                    if (cur_wtype == 5) //Ввведення залишків
                    {
                        using (var wb_in = new frmWBWriteOn(doc.out_wbill_id))
                        {
                            wb_in.ShowDialog();
                        }

                    }
                    if (cur_wtype == -5)  //Акти списання товару
                    {
                        using (var wb_in = new frmWBWriteOff(doc.out_wbill_id))
                        {
                            wb_in.ShowDialog();
                        }
                    }

                    if (cur_wtype == 4) // Накладна переміщення
                    {
                        using (var wb_re_in = new frmWayBillMove(doc.out_wbill_id))
                        {
                            wb_re_in.ShowDialog();
                        }
                    }

                    break;
            }

            RefrechItemBtn.PerformClick();
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
         //   if (OnDateEdit.ContainsFocus || whKagentList.ContainsFocus)
       //     {
                RefrechItemBtn.PerformClick();
       //     }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = gridView3.GetFocusedRow() as GetRelDocList_Result;
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

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = gridView3.GetFocusedRow() as GetRelDocList_Result;
            PrintDoc.Show(row.Id, row.DocType.Value, DB.SkladBase());
        }

        private void WbGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            focused_row = e.Row as GetWayBillListWh_Result;
            WayBillListInfoBS.DataSource = focused_row;

            if (focused_row != null)
            {
                using (var db = DB.SkladBase())
                {
                    gridControl2.DataSource = db.GetWaybillDetIn(focused_row.WBillId).ToList();
                    gridControl3.DataSource = db.GetRelDocList(focused_row.Id).OrderBy(o => o.OnDate).ToList();
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

        private void WhMatGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            GetWhBottomInfo(focused_wh_mat);
        }

        private void WhMatGridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if ( e.RowHandle < 0 )
            {
                return;
            }

            var wh_row = WhMatGridView.GetRow(e.RowHandle) as WhMatGet_Result;

            if (wh_row != null && wh_row.Remain < wh_row.MinReserv)
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void ShowAllItemsCheck_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Settings.Default.show_all_mat = ShowAllItemsCheck.Checked;
            Settings.Default.Save();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Ви дійсно бажаєте видалити історію по залишкам ?", focused_wh_mat.MatName, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                using (var db = DB.SkladBase())
                {
                    var dt = DateTime.Now.Date;

                    var min_date_post = db.v_PosRemains.Where(w => w.MatId == focused_wh_mat.MatId).Select(s => s.OnDate).ToList();

                    if (min_date_post.Count > 0 && min_date_post.Min() < dt)
                    {
                        dt = min_date_post.Min().Date.AddDays(-1);
                    }

                    var pos = db.Database.ExecuteSqlCommand(@"
                         delete from [PosRemains]
                         where PosId IN (
                           SELECT PosId 
                           FROM [PosRemains]
                           where Remain = 0 and Ordered=0 and  MatId = {0} and OnDate <= {1}
                           group by PosId)", focused_wh_mat.MatId, dt);

                    db.SaveChanges();
                }
            }
        }

        private void bandedGridView1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                PosBottomPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = bandedGridView1.GetFocusedRow() as PosGet_Result;
            FindDoc.Find(row.Id, row.DocType, row.OnDate);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = bandedGridView1.GetFocusedRow() as PosGet_Result;
            PrintDoc.Show(row.Id.Value, row.DocType.Value, DB.SkladBase());
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ChartDesigner designer = new ChartDesigner(chartControl1);
            designer.ShowDialog();
        }

        private void MatListGridView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void WhMatGridView_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == CustomSummaryProcess.Finalize && wh_mat_list != null)
            {
                var def_m = DBHelper.MeasuresList.FirstOrDefault(w => w.Def == 1);

                GridSummaryItem item = e.Item as GridSummaryItem;

                if (item.FieldName == "Remain")
                {
                    var amount_sum = wh_mat_list.Where(w => w.MId == def_m.MId).Sum(s => s.Remain ?? 0);

                    /*  var ext_sum = _db.WaybillDet.Where(w => w.WbillId == _wbill_id && w.Materials.MId != def_m.MId)
                          .Select(s => new { MaterialMeasures = s.Materials.MaterialMeasures.Where(f => f.MId == def_m.MId), s.Amount }).ToList()
                          .SelectMany(sm => sm.MaterialMeasures, (k, n) => new { k.Amount, MeasureAmount = n.Amount }).Sum(su => su.MeasureAmount * su.Amount);*/

                    e.TotalValue = amount_sum.ToString() + " " + def_m.ShortName;//Math.Round(amount_sum + ext_sum, 2);
                }
            }
        }

        public class ExtMatIfo
        {
            public decimal? LastPrice { get; set; }
        }
        public void SaveGridLayouts()
        {
            WbGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + "WarehouseUserControl\\WbGridView");
            WhMatGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + "WarehouseUserControl\\WhMatGridView");
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            MatListGridView.DeleteSelectedRows();
        }

        private void AddMatItem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            AddMatToCustomList(1);
        }

        private void AddMatToCustomList(decimal amount)
        {
            if (focused_wh_mat == null)
            {
                return;
            }


            var remain_in_wh = DB.SkladBase().MatRemainByWh(focused_wh_mat.MatId, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, wh_list, DBHelper.CurrentUser.UserId).ToList();
            var p_type = (wb.Kontragent != null ? (wb.Kontragent.PTypeId ?? DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId) : DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId);
            var mat_price = DB.SkladBase().GetListMatPrices(focused_wh_mat.MatId, wb.CurrId, p_type).FirstOrDefault();

            custom_mat_list.Add(new CustomMatListWH
            {
                Num = custom_mat_list.Count() + 1,
                MatId = focused_wh_mat.MatId,
                Name = focused_wh_mat.MatName,
                Amount = amount,
                Price = mat_price != null ? (mat_price.Price ?? 0) : 0,
                WId = remain_in_wh.Any() ? remain_in_wh.First().WId : (DBHelper.WhList.Any(w => w.Def == 1) ? DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId : DBHelper.WhList.FirstOrDefault().WId),
                PTypeId = mat_price != null ? mat_price.PType : null,
                Discount = disc_card != null ? disc_card.OnValue : (DB.SkladBase().GetDiscount(wb.KaId, focused_wh_mat.MatId).FirstOrDefault() ?? 0.00m),
                AvgPrice = focused_wh_mat.AvgPrice
            });

            MatListGridView.RefreshData();
            MatListGridView.MoveLast();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_wh_mat == null)
            {
                return;
            }

            IHelper.ShowManufacturingMaterial(focused_wh_mat.MatId);
        }

        private void checkedComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (checkedComboBoxEdit1.ContainsFocus)
            {
                wh_list = checkedComboBoxEdit1.Properties.Items.Any(w => w.CheckState == CheckState.Checked) ? "" : "*";

                foreach (var item in checkedComboBoxEdit1.Properties.Items.Where(w => w.CheckState == CheckState.Checked))
                {
                    wh_list += item.Value.ToString() + ",";
                }

                RefrechItemBtn.PerformClick();
            }
        }

        private void checkedComboBoxEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                for (int i = 0; i < checkedComboBoxEdit1.Properties.Items.Count; i++)
                    checkedComboBoxEdit1.Properties.Items[i].CheckState = CheckState.Unchecked;

                RefrechItemBtn.PerformClick();
            }
        }
    }
}
