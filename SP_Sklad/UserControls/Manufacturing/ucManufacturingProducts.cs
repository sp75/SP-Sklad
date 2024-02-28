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
    public partial class ucManufacturingProducts : DevExpress.XtraEditors.XtraUserControl
    {
        private int w_type = -20;
        private int fun_id = 68;
        private string reg_layout_path = "ucManufacturingProducts\\WbGridView";

        private UserAccess user_access { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private int _grp_id { get; set; }
        private v_ManufacturingProducts focused_row => WbGridView.GetFocusedRow() as v_ManufacturingProducts ;
        private UserAccess intermediate_weighing_access { get; set; }

        private int prev_focused_id = 0;
        private int prev_top_row_index = 0;
        private int prev_rowHandle = 0;
        private int? find_id { get; set; }
        private bool _restore { get; set; } 

        public ucManufacturingProducts()
        {
            InitializeComponent();
        }

        private void ManufacturingUserControl_Load(object sender, EventArgs e)
        {
            WbGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);
            if (!DesignMode)
            {
                var wh_list = new List<WhList>() { new WhList { WId = -1, Name = "Усі" } }.Concat(DBHelper.WhList).ToList();
                WhComboBox.Properties.DataSource = wh_list;
                WhComboBox.EditValue = -1;

                wbSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 0, Name = "Актуальний" }, new { Id = 2, Name = "Розпочато виробництво" }, new { Id = 1, Name = "Закінчено виробництво" } };
                wbSatusList.EditValue = -1;

                wbStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                wbEndDate.EditValue = DateTime.Now.Date.SetEndDay();

           //     manuf_tree = DB.SkladBase().GetManufactureTree(DBHelper.CurrentUser.UserId).ToList();
                intermediate_weighing_access = DB.SkladBase().UserAccess.FirstOrDefault(w => w.FunId == 83 && w.UserId == UserSession.UserId);// manuf_tree.FirstOrDefault(w => w.FunId == 83);
                xtraTabPage19.PageVisible = intermediate_weighing_access?.CanView == 1;

                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, DB.SkladBase());
                WbGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

                user_access = DB.SkladBase().UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);
            }
        }

        public void GetWBListMake(int grp_id)
        {
            _grp_id = grp_id;
            GetWBListMake(false);
        }

        public void GetWBListMake(bool restore)
        {
            /*   if (wbSatusList.EditValue == null || WhComboBox.EditValue == null)
               {
                   return;
               }

               var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
               var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;

               int top_row = WbGridView.TopRowIndex;
               WBListMakeBS.DataSource = DB.SkladBase().WBListMake(satrt_date, end_date, (int)wbSatusList.EditValue, WhComboBox.EditValue.ToString(), grp_id, w_type, UserSession.UserId).ToList();
               WbGridView.TopRowIndex = top_row;*/

            prev_rowHandle = WbGridView.FocusedRowHandle;

            if (focused_row != null && !find_id.HasValue)
            {
                prev_top_row_index = WbGridView.TopRowIndex;
                prev_focused_id = focused_row.WbillId;
            }

            if (find_id.HasValue)
            {
                prev_top_row_index = -1;
                prev_focused_id = find_id.Value;
                find_id = null;
            }

            _restore = restore;

            WBGridControl.DataSource = null;
            WBGridControl.DataSource = ManufacturingProductsSource;

            SetWBEditorBarBtn();
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetWBListMake(true);
        }


        public int? NewItem()
        {
            using (var wb_make = new frmWBManufacture(null))
            {
                wb_make.ShowDialog();

                return wb_make._wbill_id;
            }
        }

        public void EditItem()
        {
            if (focused_row == null)
            {
                return;
            }

            ManufDocEdit.WBEdit(focused_row.WType, focused_row.WbillId);
        }

        public int? CopyItem()
        {
            var doc = DB.SkladBase().DocCopy(focused_row.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();
            using (var wb_in = new frmWBManufacture(doc.out_wbill_id))
            {
                wb_in.is_new_record = true;
                if (wb_in.ShowDialog() == DialogResult.OK)
                {
                    ;
                }
            }


            return doc.out_wbill_id;
        }

        public void DeleteItem()
        {
            if (focused_row == null)
            {
                return;
            }

            using (var db = new BaseEntities())
            {
                try
                {
                    if (MessageBox.Show(Resources.delete_wb, "Відалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {

                        var wb1 = db.WaybillList.FirstOrDefault(w => w.WbillId == focused_row.WbillId && w.SessionId == null);
                        if (wb1 != null)
                        {
                            if (!db.IntermediateWeighing.Any(w => w.WbillId == wb1.WbillId))
                            {
                                db.WaybillList.Remove(wb1);
                            }
                            else
                            {
                                MessageBox.Show(Resources.not_storno_wb);
                            }
                        }
                        else
                        {
                            MessageBox.Show(Resources.deadlock);
                        }

                        db.SaveChanges();
                    }
                }
                catch
                {
                    MessageBox.Show(Resources.deadlock);
                }
            }
        }

        public void ExecuteItem()
        {
            using (var db = new BaseEntities())
            {

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
                    if ((wb.WType == w_type && (focused_row.ShippedAmount ?? 0) == 0))
                    {
                        DBHelper.StornoOrder(db, focused_row.WbillId);
                    }
                    else if (wb.WType == w_type)
                    {
                        MessageBox.Show("Частина товару вже відгружена на склад!");
                    }
                }
                else
                {
                    DBHelper.ExecuteOrder(db, focused_row.WbillId);
                }


            }
        }

        public void PrintItem()
        {
            PrintDoc.Show(focused_row.Id, focused_row.WType, DB.SkladBase());
        }


        public void FindItem(Guid id, DateTime on_date)
        {
            find_id = new BaseEntities().WaybillList.FirstOrDefault(w => w.Id == id).WbillId;

            WbGridView.ClearColumnsFilter();
            WbGridView.ClearFindFilter();
            PeriodComboBoxEdit.SelectedIndex = 0;
            wbStartDate.DateTime = on_date.Date;
            wbEndDate.DateTime = on_date.Date.SetEndDay();
            WhComboBox.EditValue = -1;
            wbSatusList.EditValue = -1;
            GetWBListMake(true);
/*
            int rowHandle = WbGridView.LocateByValue("Id", id);
            if (rowHandle != GridControl.InvalidRowHandle)
            {
                WbGridView.FocusedRowHandle = rowHandle;
            }*/
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            find_id = NewItem();

            GetWBListMake(false);
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditItem();

           GetWBListMake(true);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_row == null)
            {
                return;
            }

            using (var f = new frmTechProcDet(focused_row.WbillId))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    if (DB.SkladBase().WaybillList.Any(a => a.WbillId == focused_row.WbillId))
                    {
                        RefreshTechProcDet(focused_row.WbillId);
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

                    f.OkButton.Enabled = (focused_row.Checked != 1);
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
            TechProcDetBS.DataSource = null;
            TechProcDetBS.DataSource = DB.SkladBase().v_TechProcDet.AsNoTracking().Where(w => w.WbillId == wbill_id).OrderBy(o => o.Num).ToList();
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
            ExecuteItem();

            GetWBListMake(true);
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteItem();
            GetWBListMake(true);
        }

        private void StopProcesBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = DB.SkladBase())
            {
                var wb_id = focused_row.WbillId;

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

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbStartDate.ContainsFocus)
            {
                GetWBListMake(false);
            }
        }

        private void WbGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                WbGridPopupMenu.ShowPopup(p2);
            }
        }

        private void PrintItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintItem();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowOrdered(0, 0, focused_row.MatId);
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            find_id = CopyItem();
            GetWBListMake(true);
        }

        private void WbGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {

            SetWBEditorBarBtn();
        }

        private void SetWBEditorBarBtn()
        {
            xtraTabControl2_SelectedPageChanged(null, null);

            StopProcesBtn.Enabled = (focused_row != null && focused_row.Checked == 2 && user_access.CanPost == 1);
            DeleteItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && user_access.CanDelete == 1);
            EditItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && user_access.CanModify == 1);
            CopyItemBtn.Enabled = (focused_row != null && user_access.CanInsert == 1);
            ExecuteItemBtn.Enabled = (focused_row != null && user_access.CanPost == 1);
            PrintItemBtn.Enabled = (focused_row != null);
            AddTechProcBtn.Enabled = (focused_row != null && focused_row.Checked != 1 && user_access.CanModify == 1);
            AddIntermediateWeighing.Enabled = (focused_row != null && focused_row.Checked == 0 && intermediate_weighing_access?.CanInsert == 1);
        }

        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var f = new frmWayBillMakePropsDet(focused_row.WbillId);
            if (f.ShowDialog() == DialogResult.OK)
            {
                RefreshAtribute(focused_row.WbillId);
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
            IHelper.ExportToXlsx(WBGridControl);
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

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (focused_row == null)
            {
                TechProcDetBS.DataSource = null;
                WayBillMakeDetGridControl.DataSource = null;
                ucRelDocGrid1.GetRelDoc(Guid.Empty);
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
                        WayBillMakeDetGridControl.DataSource = db.GetWayBillMakeDet(focused_row.WbillId).ToList().OrderBy(o => o.Num).ToList();
                        WayBillMakeDetGridView.ExpandAllGroups();
                        break;

                    case 3:
                        ucRelDocGrid1.GetRelDoc(focused_row.Id);
                        break;

                    case 4:
                        ManufacturedPosGridControl.DataSource = db.Database.SqlQuery<GetManufacturedPos_Result>(string.Format("select * from GetManufacturedPos('{0}')", focused_row.Id)).ToList();
                        break;

                    case 5:
                        RefreshIntermediateWeighing();
                        break;
                }
            }
        }

        private void RefreshIntermediateWeighing()
        {
            if (focused_row == null)
            {
                return;
            }

            int top_row = IntermediateWeighingByWbGridView.TopRowIndex;

            using (var db = DB.SkladBase())
            {
                IntermediateWeighingByWBBS.DataSource = db.IntermediateWeighing.Where(w => w.WbillId == focused_row.WbillId).OrderBy(o => o.OnDate).Select(s => new IntermediateWeighingView
                {
                    Id = s.Id,
                    OnDate = s.OnDate,
                    Checked = s.Checked,
                    Num = s.Num,
                    PersonName = s.Kagent.Name,
                    Amount = s.Amount
                }).ToList();
            }

            IntermediateWeighingByWbGridView.TopRowIndex = top_row;
        }

        public class IntermediateWeighingView
        {
            public Guid Id { get; set; }
            public DateTime OnDate { get; set; }
            public int Checked { get; set; }
            public string Num { get; set; }
            public string PersonName { get; set; }
            public Decimal? Amount { get; set; }
        }


        private void TechProcGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DelTechProcBtn.Enabled = ((focused_row != null && focused_row.Checked != 1 && user_access.CanModify == 1) && TechProcGridView.DataRowCount > 0);
            EditTechProcBtn.Enabled = (focused_row != null && user_access.CanModify == 1 && TechProcGridView.DataRowCount > 0 /*&& focused_row.Checked != 1*/);
        }

        public void SaveGridLayouts()
        {
            WbGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dynamic dr = IntermediateWeighingByWbGridView.GetFocusedRow();
            if (dr != null)
            {
                using (var wb_iw = new frmIntermediateWeighing(focused_row.WbillId, dr.Id))
                {
                    if (wb_iw.ShowDialog() == DialogResult.OK)
                    {
                        RefreshIntermediateWeighing();
                    }
                }
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Ви дійсно бажаєте видалити проміжкове зважування!", "Видалення запису", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                dynamic dr = IntermediateWeighingByWbGridView.GetFocusedRow();
                if (dr != null)
                {
                    Guid id = dr.Id;
                    DB.SkladBase().DeleteWhere<IntermediateWeighing>(w => w.Id == id);
                    RefreshIntermediateWeighing();
                }
            }
        }

        private void IntermediateWeighingGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditIntermediateWeighing.PerformClick();
        }

        private void AddIntermediateWeighing_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var wb_iw = new frmIntermediateWeighing(focused_row.WbillId, null))
            {
                if (wb_iw.ShowDialog() == DialogResult.OK)
                {
                    RefreshIntermediateWeighing();
                }
            }
        }

        private void IntermediateWeighingGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DelIntermediateWeighing.Enabled = ((focused_row != null && focused_row.Checked == 0 && intermediate_weighing_access?.CanDelete == 1) && IntermediateWeighingByWbGridView.DataRowCount > 0);
            EditIntermediateWeighing.Enabled = (focused_row != null && focused_row.Checked == 0 && intermediate_weighing_access?.CanModify == 1 && IntermediateWeighingByWbGridView.DataRowCount > 0);
        }

        private void WbGridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            /*      if (e.RowHandle < 0)
                  {
                      return;
                  }

                  if (wh_row != null )
                  {
                    //  var mat_out = wh_row.AmountIn > 0 ? (wh_row.AmountOut / wh_row.AmountIn) * 100.00m : 0;
                      if ((Math.Abs(wh_row.MatRecipeOut - wh_row.MatOut.Value) > wh_row.Deviation) && wh_row.WType != 0)
                      {
                          e.Appearance.ForeColor = Color.Red;
                      }
                  }*/
        }

        private void barButtonItem8_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var mat_list = DB.SkladBase().GetWayBillMakeDet(focused_row.WbillId).OrderBy(o => o.Num).ToList().Select(s => new make_det
            {
                MatName = s.MatName,
                MsrName = s.MsrName,
                AmountByRecipe = s.AmountByRecipe,
                AmountIntermediateWeighing = s.AmountIntermediateWeighing,
                MatId = s.MatId,
                WbillId = focused_row.WbillId,
                //      RecipeCount = wbm.RecipeCount,
                IntermediateWeighingCount = DB.SkladBase().v_IntermediateWeighingDet.Where(w => w.WbillId == focused_row.WbillId && w.MatId == s.MatId).Count(),
                //      TotalWeightByRecipe = wbm.AmountByRecipe
            }).ToList();

            using (var f = new frmIntermediateWeighingList(mat_list))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    ;
                }
            }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int pos_id = 0;
            switch (xtraTabControl2.SelectedTabPageIndex)
            {
                case 2:
                    var row = WayBillMakeDetGridView.GetFocusedRow() as GetWayBillMakeDet_Result;
                    pos_id = row.PosId;
                    break;

                case 4:
                    var row2 = ManufacturedPosGridView.GetFocusedRow() as GetManufacturedPos_Result;
                    pos_id = row2.PosId;
                    break;
            }

            var can_modify = (user_access.CanModify == 1 && user_access.CanPost == 1);
            IHelper.ShowWayBillDetInfo(pos_id, can_modify);

            RefrechItemBtn.PerformClick();
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

        private void PeriodComboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            wbEndDate.DateTime = DateTime.Now.Date.SetEndDay();
            switch (PeriodComboBoxEdit.SelectedIndex)
            {
                case 1:
                    wbStartDate.DateTime = DateTime.Now.Date;
                    break;

                case 2:
                    wbStartDate.DateTime = DateTime.Now.Date.StartOfWeek(DayOfWeek.Monday);
                    break;

                case 3:
                    wbStartDate.DateTime = DateTime.Now.Date.FirstDayOfMonth();
                    break;

                case 4:
                    wbStartDate.DateTime = new DateTime(DateTime.Now.Year, 1, 1);
                    break;
            }

            GetWBListMake(false);
        }

        private void wbEndDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbEndDate.ContainsFocus)
            {
                GetWBListMake(false);
            }
        }

        private void WhComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (WhComboBox.ContainsFocus)
            {
                GetWBListMake(false);
            }
        }

        private void wbSatusList_EditValueChanged(object sender, EventArgs e)
        {
            if (wbSatusList.ContainsFocus)
            {
                GetWBListMake(false);
            }
        }

        private void ManufacturingProductsSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (wbSatusList.EditValue == null || WhComboBox.EditValue == null)
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;
            var status = (int)wbSatusList.EditValue;
            var wh_id = (int)WhComboBox.EditValue;


            BaseEntities objectContext = new BaseEntities();
            var list = objectContext.v_ManufacturingProducts.Where(w => w.WType == w_type && w.OnDate > satrt_date && w.OnDate <= end_date && (w.Checked == status || status == -1) && (w.WId == wh_id || wh_id ==-1) && (_grp_id ==0 || w.GrpId == _grp_id) && w.UserId == UserSession.UserId);
            e.QueryableSource = list;
            e.Tag = objectContext;
        }

        private void ManufacturingProductsSource_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (e.Tag == null)
                return;

            ((BaseEntities)e.Tag).Dispose();
        }

        private void WbGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (focused_row == null || !_restore)
            {
                return;
            }

            int rowHandle = WbGridView.LocateByValue("WbillId", prev_focused_id, OnRowSearchComplete);
            if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
            {
                FocusRow(WbGridView, rowHandle);
            }
            else
            {
                WbGridView.FocusedRowHandle = prev_rowHandle;
            }

            _restore = false;
        }

        void OnRowSearchComplete(object rh)
        {
            int rowHandle = (int)rh;
            if (WbGridView.IsValidRowHandle(rowHandle))
            {
                FocusRow(WbGridView, rowHandle);
            }
        }

        public void FocusRow(GridView view, int rowHandle)
        {
            view.TopRowIndex = prev_top_row_index == -1 ? rowHandle : prev_top_row_index;
            view.FocusedRowHandle = rowHandle;
        }

        private void WbGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void WhComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                WhComboBox.EditValue = IHelper.ShowDirectList(WhComboBox.EditValue, 2);
            }
        }
    }
}
