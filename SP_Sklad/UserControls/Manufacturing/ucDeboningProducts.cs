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
    public partial class ucDeboningProducts : DevExpress.XtraEditors.XtraUserControl
    {
        private string reg_layout_path = "ucDeboningProducts\\DeboningGridView";

        private int w_type = -22;
        private int fun_id = 72;
        private int _grp_id { get; set; }

        private UserSettingsRepository user_settings { get; set; }
        private UserAccess user_access { get; set; }

        private v_DeboningProducts focused_row => DeboningGridView.GetFocusedRow() as v_DeboningProducts;

        public int grp_id { get; set; }
        private int prev_focused_id = 0;
        private int prev_top_row_index = 0;
        private int prev_rowHandle = 0;
        private int? find_id { get; set; }
        private bool _restore { get; set; }

        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        public ucDeboningProducts()
        {
            InitializeComponent();
        }

        public void SaveGridLayouts()
        {
            DeboningGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        private void ManufacturingUserControl_Load(object sender, EventArgs e)
        {
            DeboningGridView.SaveLayoutToStream(wh_layout_stream);
            DeboningGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                DebWhComboBox.Properties.DataSource = new List<WhList>() { new WhList { WId = -1, Name = "Усі" } }.Concat(DBHelper.WhList).ToList();
                DebWhComboBox.EditValue = -1;
                DebSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 0, Name = "Актуальний" }, new { Id = 2, Name = "Розпочато виробництво" }, new { Id = 1, Name = "Закінчено виробництво" } };
                DebSatusList.EditValue = -1;

                DebStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                DebEndDate.EditValue = DateTime.Now.Date.SetEndDay();

                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, DB.SkladBase());

                DeboningGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

                user_access = DB.SkladBase().UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);
            }
        }

        public void NewItem()
        {
            using (var wb_make = new frmWBDeboning(null))
            {
                wb_make.ShowDialog();
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

        public void CopyItem()
        {
            if (focused_row == null)
            {
                return;
            }

            var doc2 = DB.SkladBase().DocCopy(focused_row.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();
            using (var wb_in = new frmWBDeboning(doc2.out_wbill_id))
            {
                wb_in.is_new_record = true;
                wb_in.ShowDialog();
            }
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

                        var wb = db.WaybillList.FirstOrDefault(w => w.WbillId == focused_row.WbillId && w.SessionId == null);
                        if (wb != null)
                        {
                            db.WaybillList.Remove(wb);
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
                    DBHelper.StornoOrder(db, focused_row.WbillId);
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

            DeboningGridView.ClearColumnsFilter();
            DeboningGridView.ClearFindFilter();
            PeriodComboBoxEdit.SelectedIndex = 0;
            DebStartDate.DateTime = on_date.Date;
            DebEndDate.DateTime = on_date.Date.SetEndDay();
            DebWhComboBox.EditValue = -1;
            DebSatusList.EditValue = -1;

            GetDeboningList(true);
            /*    int rowHandle = DeboningGridView.LocateByValue("Id", id);
                if (rowHandle != GridControl.InvalidRowHandle)
                {
                    DeboningGridView.FocusedRowHandle = rowHandle;
                }*/
        }

        public void GetDeboningList(int grp_id)
        {
            _grp_id = grp_id;
            GetDeboningList(false);
        }

        public void GetDeboningList(bool restore)
        {
            _restore = restore;

            /*   if (DebSatusList.EditValue == null || DebWhComboBox.EditValue == null)
               {
                   return;
               }

               var satrt_date = DebStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : DebStartDate.DateTime;
               var end_date = DebEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : DebEndDate.DateTime;

               int top_row = DeboningGridView.TopRowIndex;
               DeboningBS.DataSource = DB.SkladBase().WBListMake(satrt_date, end_date, (int)DebSatusList.EditValue, DebWhComboBox.EditValue.ToString(), grp_id, -22, UserSession.UserId).ToList();
               DeboningGridView.TopRowIndex = top_row;*/

            prev_rowHandle = DeboningGridView.FocusedRowHandle;

            if (focused_row != null && !find_id.HasValue)
            {
                prev_top_row_index = DeboningGridView.TopRowIndex;
                prev_focused_id = focused_row.WbillId;
            }

            if (find_id.HasValue)
            {
                prev_top_row_index = -1;
                prev_focused_id = find_id.Value;
                find_id = null;
            }

            _restore = restore;

            DeboningGridControl.DataSource = null;
            DeboningGridControl.DataSource = DeboningProductsSource;

            SetWBEditorBarBtn();
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetDeboningList(true);
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NewItem();
            GetDeboningList(false);
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditItem();
            GetDeboningList(true);
        }

        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditItemBtn.PerformClick();
        }

        private void ExecuteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExecuteItem();

            RefrechItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteItem();

            RefrechItemBtn.PerformClick();
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


        private void DebStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (DebStartDate.ContainsFocus || DebEndDate.ContainsFocus || DebWhComboBox.ContainsFocus || DebSatusList.ContainsFocus)
            {
                GetDeboningList(false);
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
            CopyItem();
            GetDeboningList(true);
        }

        private void DeboningGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void SetWBEditorBarBtn()
        {
            xtraTabControl1_SelectedPageChanged(null, null);

            StopProcesBtn.Enabled = (focused_row != null && focused_row.Checked == 2 && user_access.CanPost == 1);
            DeleteItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && user_access.CanDelete == 1);
            EditItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && user_access.CanModify == 1);
            CopyItemBtn.Enabled = (user_access.CanInsert == 1 && focused_row != null);
            ExecuteItemBtn.Enabled = (focused_row != null && user_access.CanPost == 1);
            PrintItemBtn.Enabled = (focused_row != null);
        }


        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (focused_row == null)
            {
                gridControl4.DataSource = null;
                ucRelDocGrid2.GetRelDoc(Guid.Empty);
                return;
            }

            using (var db = DB.SkladBase())
            {
                switch (xtraTabControl1.SelectedTabPageIndex)
                {
                    case 0:
                        DeboningDetGridControl.DataSource = db.v_DeboningDet.Where(w => w.WBillId == focused_row.WbillId).ToList();
                        break;

                    case 1:
                        gridControl4.DataSource = db.GetWayBillDetOut(focused_row.WbillId).ToList().OrderBy(o => o.Num).ToList();
                        break;

                    case 2:
                        ucRelDocGrid2.GetRelDoc(focused_row.Id);
                        break;
                }
            }
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

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(DeboningGridControl);
        }

        private void PeriodComboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            DebEndDate.DateTime = DateTime.Now.Date.SetEndDay();
            switch (PeriodComboBoxEdit.SelectedIndex)
            {
                case 1:
                    DebStartDate.DateTime = DateTime.Now.Date;
                    break;

                case 2:
                    DebStartDate.DateTime = DateTime.Now.Date.StartOfWeek(DayOfWeek.Monday);
                    break;

                case 3:
                    DebStartDate.DateTime = DateTime.Now.Date.FirstDayOfMonth();
                    break;

                case 4:
                    DebStartDate.DateTime = new DateTime(DateTime.Now.Year, 1, 1);
                    break;
            }

            GetDeboningList(false);
        }

        private void DeboningProductsSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (DebSatusList.EditValue == null || DebWhComboBox.EditValue == null)
            {
                return;
            }

            var satrt_date = DebStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : DebStartDate.DateTime;
            var end_date = DebEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : DebEndDate.DateTime;

            var status = (int)DebSatusList.EditValue;
            var wh_id = (int)DebWhComboBox.EditValue;


            BaseEntities objectContext = new BaseEntities();
            var list = objectContext.v_DeboningProducts.Where(w => w.WType == w_type && w.OnDate > satrt_date && w.OnDate <= end_date && (w.Checked == status || status == -1) && (w.WId == wh_id || wh_id == -1) && (_grp_id == 0 || w.GrpId == _grp_id) && w.UserId == UserSession.UserId);
            e.QueryableSource = list;
            e.Tag = objectContext;
        }

        private void DeboningGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void DeboningGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (focused_row == null || !_restore)
            {
                return;
            }

            int rowHandle = DeboningGridView.LocateByValue("WbillId", prev_focused_id, OnRowSearchComplete);
            if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
            {
                FocusRow(DeboningGridView, rowHandle);
            }
            else
            {
                DeboningGridView.FocusedRowHandle = prev_rowHandle;
            }

            _restore = false;
        }

        void OnRowSearchComplete(object rh)
        {
            int rowHandle = (int)rh;
            if (DeboningGridView.IsValidRowHandle(rowHandle))
            {
                FocusRow(DeboningGridView, rowHandle);
            }
        }

        public void FocusRow(GridView view, int rowHandle)
        {
            view.TopRowIndex = prev_top_row_index == -1 ? rowHandle : prev_top_row_index;
            view.FocusedRowHandle = rowHandle;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            wh_layout_stream.Seek(0, System.IO.SeekOrigin.Begin);

            DeboningGridView.RestoreLayoutFromStream(wh_layout_stream);
        }
    }
}
