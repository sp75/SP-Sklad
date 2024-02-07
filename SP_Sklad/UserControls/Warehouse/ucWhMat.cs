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

namespace SP_Sklad.UserControls.Warehouse
{
    public partial class ucWhMat : DevExpress.XtraEditors.XtraUserControl
    {
        public int wid = 0;
        public string wh_list = "*";
        public bool by_grp { get; set; }
        public bool display_child_groups { get; set; }
        public int focused_tree_node_num { get; set; }

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

        public DiscCards disc_card { get; set; }

        public class CustomMatListWH : CustomMatList
        {
            public decimal? Discount { get; set; }
            public int? PTypeId { get; set; }
            public decimal? AvgPrice { get; set; }
            public decimal AccountingAmount { get; set; }
            public int? DiscountType { get; set; }
        }

     //   GetWhTree_Result focused_tree_node { get; set; }

        public int? set_tree_node { get; set; }

        public class checkedWhList
        {
            public string WId { get; set; }
            public string Name { get; set; }
            public bool IsChecked { get; set; }
        }

        public class ExtMatIfo
        {
            public decimal? LastPrice { get; set; }
            public int MatId { get; set; }
        }

        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        public ucWhMat()
        {
            InitializeComponent();
        }

        private void ucWhMat_Load(object sender, EventArgs e)
        {
            WhMatGridView.SaveLayoutToStream(wh_layout_stream);

             WhMatGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + "ucWhMat\\WhMatGridView");

            if (!DesignMode)
            {
                custom_mat_list = new List<CustomMatListWH>();
                MatListGridControl.DataSource = custom_mat_list;

                OnDateEdit.DateTime = DateTime.Now;

                whKagentList.Properties.DataSource = DBHelper.KagentsList;// new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().Kagent.Select(s => new { s.KaId, s.Name }));
                if (whKagentList.EditValue == null || whKagentList.EditValue == DBNull.Value)
                {
                    whKagentList.EditValue = 0;
                }


                var whlist = new BaseEntities().Warehouse.Select(s => new checkedWhList { WId = s.WId.ToString(), Name = s.Name, IsChecked = s.WId == wid }).ToList();
                foreach (var item in whlist)
                {
                    WhCheckedComboBox.Properties.Items.Add(item.WId, item.Name, item.IsChecked ? CheckState.Checked : CheckState.Unchecked, true);
                }


                repositoryItemLookUpEdit1.DataSource = DBHelper.WhList;
                repositoryItemLookUpEdit2.DataSource = DB.SkladBase().PriceTypes.ToList();

                AvgMatPriceGridColumn.Visible = (DBHelper.CurrentUser.ShowPrice == 1);
                AvgMatPriceGridColumn.OptionsColumn.ShowInCustomizationForm = AvgMatPriceGridColumn.Visible;
                SumMatRemainGridColumn.Visible = AvgMatPriceGridColumn.Visible;
                SumMatRemainGridColumn.OptionsColumn.ShowInCustomizationForm = AvgMatPriceGridColumn.Visible;


                bandedGridColumn2.Caption = $"{bandedGridColumn2.Caption}, {DBHelper.NationalCurrency.ShortName}";

                var user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());

                WhMatGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
                MatListGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

               
            }
        }

        public async Task GetMatOnWh()
        {
            int grp_id = 0;

            string grp = "";

            grp_id = by_grp ? focused_tree_node_num : 0;
            wid = by_grp ? 0 : focused_tree_node_num;

            if (wh_list != "*")
            {
                wid = -1;
            }

            if (display_child_groups && by_grp && focused_tree_node_num != 0)
            {
                grp = focused_tree_node_num.ToString();
            }

            WhMatGridView.ShowLoadingPanel();
            int top_row = WhMatGridView.TopRowIndex;
            wh_mat_list = await DB.SkladBase().WhMatGet(grp_id, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, ShowEmptyItemsCheck.Checked ? 1 : 0, wh_list, ShowAllItemsCheck.Checked ? 1 : 0, grp, DBHelper.CurrentUser.UserId, display_child_groups ? 1 : 0).ToListAsync();
            WhMatGetBS.DataSource = wh_mat_list;
            WhMatGridView.TopRowIndex = top_row;
            WhMatGridView.HideLoadingPanel();
        }

        private void RefreshWhBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var result = GetMatOnWh();
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
            var amount = wb.WType != 7 ? 1 : focused_wh_mat.CurRemain.Value;

            AddMatToCustomList(amount, focused_wh_mat);
        }
        private void AddMatToCustomList(decimal amount, WhMatGet_Result wh_mat)
        {
            if (wh_mat == null)
            {
                return;
            }


            var remain_in_wh = DB.SkladBase().MatRemainByWh(wh_mat.MatId, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, wh_list, DBHelper.CurrentUser.UserId).ToList();
            var p_type = (wb.Kontragent != null ? (wb.Kontragent.PTypeId ?? DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId) : DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId);
            var mat_price = DB.SkladBase().GetMatPrice(wh_mat.MatId, wb.CurrId, p_type, wb.KaId).FirstOrDefault();

            var item = custom_mat_list.FirstOrDefault(w => w.MatId == wh_mat.MatId);
            if (item == null)
            {
                var dis = DB.SkladBase().GetDiscount(wb.KaId, wh_mat.MatId).FirstOrDefault();
                var discount = dis.DiscountType == 0 ? (dis.Discount ?? 0) : ((dis.Discount ?? 0) / (mat_price.Price ?? 0) * 100);

                custom_mat_list.Add(new CustomMatListWH
                {
                    Num = custom_mat_list.Count() + 1,
                    MatId = wh_mat.MatId,
                    Name = wh_mat.MatName,
                    Amount = amount,
                    Price = mat_price != null ? (mat_price.Price ?? 0) : 0,
                    WId = remain_in_wh.Any() ? remain_in_wh.First().WId : (DBHelper.WhList.Any(w => w.Def == 1) ? DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId : DBHelper.WhList.FirstOrDefault().WId),
                    PTypeId = mat_price != null ? mat_price.PType : null,
                    Discount = disc_card != null ? disc_card.OnValue : discount,
                    AvgPrice = wh_mat.AvgPrice,
                    AccountingAmount = amount,
                    DiscountType = disc_card != null ? 0 : dis.DiscountType
                });
            }
            else if (wb.WType != 7)
            {
                item.Amount += amount;
            }

            MatListGridView.RefreshData();
            MatListGridView.MoveLast();
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
            if (OnDateEdit.ContainsFocus || whKagentList.ContainsFocus)
            {
                RefreshWhBtn.PerformClick();
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

                if (row == null)
                {
                    using (var db = new BaseEntities())
                    {
                        var bc = db.v_BarCodes.FirstOrDefault(w => w.BarCode == kod);
                        if (bc != null)
                        {
                            row = WhMatGetBS.List.OfType<WhMatGet_Result>().ToList().Find(f => f.MatId == bc.MatId);
                            pos = WhMatGetBS.IndexOf(row);
                            WhMatGetBS.Position = pos;
                        }
                    }
                }

                if (MatListTabPage.PageVisible)
                {
                    if (row != null)
                    {
                        if (BarCodeSplit.Count() > 2)
                        {
                            var price = Convert.ToDecimal(BarCodeSplit[1] + "," + BarCodeSplit[2]);
                            var frm = new frmWeightEdit(row.MatName);
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                var wh_row = WhRemainGridView.GetFocusedRow() as MatRemainByWh_Result;

                                var discount = DB.SkladBase().GetDiscount(wb.KaId, row.MatId).FirstOrDefault();
                                custom_mat_list.Add(new CustomMatListWH
                                {
                                    Num = custom_mat_list.Count() + 1,
                                    MatId = row.MatId,
                                    Name = row.MatName,
                                    Amount = frm.AmountEdit.Value,
                                    Price = price,
                                    WId = wh_row != null ? wh_row.WId : DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId,
                                    PTypeId = null,
                                    Discount = discount.Discount,
                                    DiscountType = discount.DiscountType
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
                        if (BarCodeEdit.Text.Count() == 13)
                        {
                            var ean13 = new EAN13(BarCodeEdit.Text);

                            var row2 = WhMatGetBS.List.OfType<WhMatGet_Result>().ToList().Find(f => f.Artikul == ean13.artikul);
                            var pos2 = WhMatGetBS.IndexOf(row2);

                            WhMatGetBS.Position = pos2;

                            if (row2 != null)
                            {
                                AddMatToCustomList(ean13.amount, focused_wh_mat);
                            }
                        }
                    }
                }


                BarCodeEdit.Text = "";
            }
        }

        private void WhMatGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                MatPopupMenu.ShowPopup(Control.MousePosition);
            }
        }

        private void MatTurnInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_wh_mat == null)
            {
                return;
            }

            IHelper.ShowTurnMaterial(focused_wh_mat.MatId);
        }

        private void MatTurnInfoBtn_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_wh_mat == null)
            {
                return;
            }

            IHelper.ShowMatInfo(focused_wh_mat.MatId);
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_wh_mat == null)
            {
                return;
            }

            IHelper.ShowMatRSV(focused_wh_mat.MatId, DB.SkladBase());
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void DeboningMatBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_wh_mat == null)
            {
                return;
            }

            var rec = DB.SkladBase().MatRecipe.FirstOrDefault(w => w.MatId == focused_wh_mat.MatId && w.RType == 2);
            var wh_remain = WhRemainGridView.GetFocusedRow() as MatRemainByWh_Result;
            if (rec != null)
            {
                using (var f = new frmWBDeboning() { rec_id = rec.RecId, source_wid = wh_remain.WId })
                {
                    f.ShowDialog();
                }

                RefreshWhBtn.PerformClick();
            }
            else MessageBox.Show("Не можливо виконати овалку, не знайдено рецепт!");
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(WhMatGridControl);
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowOrdered(0, 0, focused_wh_mat.MatId);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_wh_mat == null)
            {
                return;
            }

            IHelper.ShowManufacturingMaterial(focused_wh_mat.MatId);
        }

        private void SetPriceBtnItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_wh_mat != null)
            {
                using (var frm = new frmSettingMaterialPrices(mat_id: focused_wh_mat.MatId))
                {
                    frm.ShowDialog();
                }
            }
        }

        private void DelRemainsMatBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void ShowEmptyItemsCheck_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshWhBtn.PerformClick();
        }

        private void ShowAllItemsCheck_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Settings.Default.show_all_mat = ShowAllItemsCheck.Checked;
            Settings.Default.Save();
        }

        private void ShowAllItemsCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshWhBtn.PerformClick();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            wh_layout_stream.Seek(0, System.IO.SeekOrigin.Begin);

            WhMatGridView.RestoreLayoutFromStream(wh_layout_stream);
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

        private void RecalcRemainsPostBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = DB.SkladBase())
            {
                var row = bandedGridView1.GetFocusedRow() as PosGet_Result;

                db.DeleteWhere<PosRemains>(w => w.PosId == row.PosId);

                var pos = db.WMatTurn.Where(w => w.PosId == row.PosId).OrderBy(o => o.OnDate).Select(s => new { s.PosId, s.WId, s.OnDate }).ToList().Distinct();

                foreach (var item in pos)
                {
                    db.SP_RECALC_POSREMAINS(item.PosId, focused_wh_mat.MatId, item.WId, item.OnDate, 0);
                }

                db.SaveChanges();
            }

            RefreshWhBtn.PerformClick();
        }

        private void AddGrpItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (var item in wh_mat_list.Where(w => w.GrpName == focused_wh_mat.GrpName))
            {
                var amount = wb.WType != 7 ? 1 : item.CurRemain.Value;

                AddMatToCustomList(amount, item);
            }
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

        private void WhMatGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
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
                    //  var last_price = DB.SkladBase().GetLastPrice(row.MatId, 0, -1, DateTime.Now).FirstOrDefault();
                    var last_price = new GetLastPrice(row.MatId, 0, -1, DateTime.Now);
                    ExtMatIfoBS.DataSource = new ExtMatIfo
                    {
                        LastPrice = last_price.Price,
                        MatId = row.MatId
                    };

                    break;
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            GetWhBottomInfo(focused_wh_mat);
        }

        public void SaveGridLayouts()
        {
            WhMatGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + "ucWhMat\\WhMatGridView");
        }

        private void WhCheckedComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (WhCheckedComboBox.ContainsFocus)
            {
                wh_list = WhCheckedComboBox.Properties.Items.Any(w => w.CheckState == CheckState.Checked) ? "" : "*";

                foreach (var item in WhCheckedComboBox.Properties.Items.Where(w => w.CheckState == CheckState.Checked))
                {
                    wh_list += item.Value.ToString() + ",";
                }

                RefreshWhBtn.PerformClick();
            }
        }

        private void WhCheckedComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                for (int i = 0; i < WhCheckedComboBox.Properties.Items.Count; i++)
                    WhCheckedComboBox.Properties.Items[i].CheckState = CheckState.Unchecked;

                RefreshWhBtn.PerformClick();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ChartDesigner designer = new ChartDesigner(chartControl1);
            designer.ShowDialog();
        }

        private void chartControl1_DoubleClick(object sender, EventArgs e)
        {
            ChartDesigner designer = new ChartDesigner(chartControl1);
            designer.ShowDialog();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            MatListGridView.DeleteSelectedRows();
        }

        private void WhMatGridView_BeforeLoadLayout(object sender, DevExpress.Utils.LayoutAllowEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.PreviousVersion != view.OptionsLayout.LayoutVersion)
            {
                e.Allow = false;
            }
        }

        private void CopyCellContentsBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Clipboard.SetText(WhMatGridView.GetFocusedDisplayText());
        }
    }
}
