﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.SkladData;
using DevExpress.XtraBars;
using SP_Sklad.Common;
using SP_Sklad.Reports;
using SP_Sklad.Properties;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;
using SP_Sklad.ViewsForm;
using SP_Sklad.WBForm;

namespace SP_Sklad.UserControls
{
    public partial class ucKAgentAdjustment : DevExpress.XtraEditors.XtraUserControl
    {
        public int w_type { get; set; }
        public int fun_id { get; set; }
        private string reg_layout_path = "ucKAgentAdjustment\\KAgentAdjustmentGridView";
        BaseEntities _db { get; set; }

        public v_KAgentAdjustment focused_row => KAgentAdjustmentGridView.GetFocusedRow() is NotLoadedObject ? null : KAgentAdjustmentGridView.GetFocusedRow() as v_KAgentAdjustment;

        private UserAccess user_access { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private Guid prev_focused_id = Guid.Empty;
        private int prev_top_row_index = 0;
        private int prev_rowHandle = 0;

        private Guid? find_id { get; set; }
        private bool restore = false;

        public ucKAgentAdjustment()
        {
            InitializeComponent();
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            KAgentAdjustmentGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        private void ucProjectManagement_Load(object sender, EventArgs e)
        {
            KAgentAdjustmentGridView.SaveLayoutToStream(wh_layout_stream);
            KAgentAdjustmentGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                _db = new BaseEntities();
                user_access = _db.UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);

                GetData();
            }
        }

        public void NewItem()
        {
            using (var frm = new frmKAgentAdjustment(w_type))
            {
                frm.ShowDialog();
            }
        }

        public void CopyItem()
        {
            ;
        }

        public void EditItem()
        {
            if (focused_row == null)
            {
                return;
            }

            using (var frm = new frmKAgentAdjustment(w_type, focused_row.Id))
            {
                frm.ShowDialog();
            }
        }

        public void DeleteItem()
        {
            if (XtraMessageBox.Show($"Ви дійсно бажаєте видалити документ {focused_row.Num}?", "Видалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                var adj = _db.KAgentAdjustment.Find(focused_row.Id);

                if (adj != null)
                {
                    _db.KAgentAdjustment.Remove(adj);
                    _db.SaveChanges();
                }
                else
                {
                    MessageBox.Show(string.Format("Документ #{0} не знайдено", focused_row.Num));
                }
            }
        }

        public void ExecuteItem()
        {
            
            var kadj = _db.KAgentAdjustment.Find(focused_row.Id);
            if (kadj != null)
            {
                if (kadj.OnDate > _db.CommonParams.First().EndCalcPeriod)
                {
                    kadj.Checked = focused_row.Checked == 0 ? 1 : 0;
                    _db.SaveChanges();
                }
                else
                {
                    XtraMessageBox.Show("Період вже закритий. Змініть дату документа!", "Відміна/Проведення корегуючого документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show(string.Format("Документ #{0} не знайдено", focused_row.Num));
            }
        }

        public void PrintItem()
        {
            ;
        }

        public void FindItem(Guid id, DateTime on_date)
        {
            find_id = id;
            KAgentAdjustmentGridView.ClearColumnsFilter();
            KAgentAdjustmentGridView.ClearFindFilter();
            ucDocumentFilterPanel.ClearFindFilter(on_date);

            GetData();
        }

        public void ExportToExcel()
        {
            IHelper.ExportToXlsx(KAgentAdjustmentGridControl);
        }

        public void GetData()
        {
            prev_rowHandle = KAgentAdjustmentGridView.FocusedRowHandle;

            if (focused_row != null && !find_id.HasValue)
            {
                prev_top_row_index = KAgentAdjustmentGridView.TopRowIndex;
                prev_focused_id = focused_row.Id;
            }

            if (find_id.HasValue)
            {
                prev_top_row_index = -1;
                prev_focused_id = find_id.Value;
                find_id = null;
            }

            restore = true;

            KAgentAdjustmentGridControl.DataSource = null;
            KAgentAdjustmentGridControl.DataSource = GgridDataSource;

            SetWBEditorBarBtn();
        }




        private void SetWBEditorBarBtn()
        {
            xtraTabControl4_SelectedPageChanged(null, null);

            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            CopyItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;

            if (focused_row == null)
            {
                return;
            }

            DeleteItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && user_access.CanDelete == 1);
            ExecuteItemBtn.Enabled = (focused_row != null && user_access.CanPost == 1);
            EditItemBtn.Enabled = (focused_row != null && user_access.CanModify == 1 && focused_row.Checked == 0);
            CopyItemBtn.Enabled = (focused_row != null && user_access.CanModify == 1);
            PrintItemBtn.Enabled = (focused_row != null);

            if (focused_row?.Checked == 0) ExecuteItemBtn.ImageIndex = 16;
            else ExecuteItemBtn.ImageIndex = 6;
        }

        private void PMGridPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            WbHistoryBtn.Enabled = IHelper.GetUserAccess(39)?.CanView == 1;
        }

    
        void OnRowSearchComplete(object rh)
        {
            int rowHandle = (int)rh;
            if (KAgentAdjustmentGridView.IsValidRowHandle(rowHandle))
            {
                FocusRow(KAgentAdjustmentGridView, rowHandle);
            }
        }

        public void FocusRow(GridView view, int rowHandle)
        {
            view.TopRowIndex = prev_top_row_index == -1 ? rowHandle : prev_top_row_index;
            view.FocusedRowHandle = rowHandle;
        }


        private void NewItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            NewItem();
            GetData();
        }

        private void CopyItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            CopyItem();
            GetData();
        }

        private void WbHistoryBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
         /*   using (var frm = new frmLogHistory(24, wb_focused_row.WbillId))
            {
                frm.ShowDialog();
            }*/
        }

        private void ExportToExcelBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportToExcel();
        }

        private void DeleteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteItem();
            GetData();
        }

        private void RefrechItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetData();
        }

        private void ExecuteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExecuteItem();
            GetData();
        }

        private void EditItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditItem();
            GetData();
        }

        private void PrintItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            PrintItem();
        }

        private void RestoreSettingsGridBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            wh_layout_stream.Seek(0, System.IO.SeekOrigin.Begin);

            KAgentAdjustmentGridView.RestoreLayoutFromStream(wh_layout_stream);
        }


        private void KAgentAdjustmentSource_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (e.Tag == null)
                return;

            ((BaseEntities)e.Tag).Dispose();
        }

        private void KAgentAdjustmentSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            BaseEntities objectContext = new BaseEntities();
            var list = objectContext.v_KAgentAdjustment.Where(w => w.OnDate >= ucDocumentFilterPanel.StartDate && w.OnDate <= ucDocumentFilterPanel.EndDate && (ucDocumentFilterPanel.StatusId == -1 || w.Checked == ucDocumentFilterPanel.StatusId) && (w.WType == w_type || w_type == -1));
            e.QueryableSource = list;

            e.Tag = objectContext;
        }

        private void KAgentAdjustmentGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (focused_row == null || !restore)
            {
                return;
            }

            int rowHandle = KAgentAdjustmentGridView.LocateByValue("Id", prev_focused_id, OnRowSearchComplete);
            if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
            {
                FocusRow(KAgentAdjustmentGridView, rowHandle);
            }
            else
            {
                KAgentAdjustmentGridView.FocusedRowHandle = prev_rowHandle;
            }

            restore = false;
        }

        private void xtraTabControl4_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (focused_row == null)
            {
                gridControl5.DataSource = null;
                ucRelDocGrid3.GetRelDoc(Guid.Empty);
                vGridControl3.DataSource = null;

                return;
            }

            switch (xtraTabControl4.SelectedTabPageIndex)
            {
                case 0:

                    gridControl5.DataSource = _db.v_KAgentAdjustmentDet.AsNoTracking().Where(w => w.KAgentAdjustmentId == focused_row.Id).OrderBy(o => o.Idx).ToList();
                    break;

                case 1:
                    vGridControl3.DataSource = _db.v_KAgentAdjustment.AsNoTracking().Where(w=> w.Id == focused_row.Id).ToList();
                    break;

                case 2:
                    ucRelDocGrid3.GetRelDoc(focused_row.Id);
                    break;
            }
        }

        private void KAgentAdjustmentGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void KAgentAdjustmentGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void KAgentAdjustmentGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItem();
            GetData();
        }

        private void KAgentAdjustmentGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                GridPopupMenu.ShowPopup(p2);
            }
        }

        private void ucDocumentFilterPanel_FilterChanged(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
