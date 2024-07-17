using System;
using System.Collections.Generic;
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
using SP_Sklad.Properties;
using SP_Sklad.Reports;
using System.IO;


namespace SP_Sklad.MainTabs
{
    public partial class usPreparationProducts : DevExpress.XtraEditors.XtraUserControl
    {
        private int w_type = -24;
        private int fun_id = 80;
        private string reg_layout_path = "usPreparationProducts\\PreparationRawMaterialsGridView";

        private UserAccess user_access { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private PreparationRawMaterialsList_Result focused_prep_raw_mat_row => PreparationRawMaterialsGridView.GetFocusedRow() as PreparationRawMaterialsList_Result;


        public usPreparationProducts()
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
            PreparationRawMaterialsGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        private void ManufacturingUserControl_Load(object sender, EventArgs e)
        {
            PreparationRawMaterialsGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                PrepRawMatWhList.Properties.DataSource = new List<object>() { new { WId = "*", Name = "Усі" } }.Concat(DBHelper.WhList.Select(s => new { WId = s.WId.ToString(), s.Name }).ToList());
                PrepRawMatWhList.EditValue = "*";

                PrepRawMatStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 0, Name = "Актуальний" }, new { Id = 2, Name = "Розпочато виробництво" }, new { Id = 1, Name = "Закінчено виробництво" } };
                PrepRawMatStatusList.EditValue = -1;

                PrepRawMatStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                PrepRawMatEndDate.EditValue = DateTime.Now.Date.SetEndDay();

                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, DB.SkladBase());
                PreparationRawMaterialsGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

                user_access = DB.SkladBase().UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);
            }
        }


        void PreparationRawMaterials()
        {
            if (PrepRawMatStatusList.EditValue == null || PrepRawMatWhList.EditValue == null )
            {
                return;
            }

            var satrt_date = PrepRawMatStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : PrepRawMatStartDate.DateTime;
            var end_date = PrepRawMatEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : PrepRawMatEndDate.DateTime;

            int top_row = PreparationRawMaterialsGridView.TopRowIndex;
            PreparationRawMaterialsBS.DataSource = DB.SkladBase().PreparationRawMaterialsList(satrt_date, end_date, (int)PrepRawMatStatusList.EditValue, PrepRawMatWhList.EditValue.ToString()).ToList();
            PreparationRawMaterialsGridView.TopRowIndex = top_row;

            SetWBEditorBarBtn();
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PreparationRawMaterials();
        }

        public int? NewItem()
        {
            using (var wb_make = new frmPreparationRawMaterials(null))
            {
                wb_make.ShowDialog();
                return wb_make._wbill_id;
            }
        }

        public void EditItem()
        {
            if (focused_prep_raw_mat_row == null)
            {
                return;
            }

            ManufDocEdit.WBEdit(focused_prep_raw_mat_row.WType, focused_prep_raw_mat_row.WbillId);
        }
        public void DeleteItem()
        {
            if (focused_prep_raw_mat_row == null)
            {
                return;
            }

            using (var db = new BaseEntities())
            {
                try
                {
                    if (MessageBox.Show(Resources.delete_wb, "Видалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {

                        var wbp = db.WaybillList.FirstOrDefault(w => w.WbillId == focused_prep_raw_mat_row.WbillId && w.SessionId == null);
                        if (wbp != null)
                        {
                            db.WaybillList.Remove(wbp);
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
            }
        }

        public void PrintItem()
        {
            PrintDoc.Show(focused_prep_raw_mat_row.Id, focused_prep_raw_mat_row.WType, DB.SkladBase());
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NewItem();
            PreparationRawMaterials();
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditItem();
            PreparationRawMaterials();
        }

        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditItemBtn.PerformClick();
        }

        private void ExecuteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExecuteItem();
            PreparationRawMaterials();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteItem();
            PreparationRawMaterials();
        }

        private void StopProcesBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = DB.SkladBase())
            {
                var wb_id = 0;

                        wb_id = focused_prep_raw_mat_row.WbillId;
                       

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
            PrintItem();
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ;
        }


        private void PreparationRawMaterialsGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void SetWBEditorBarBtn()
        {
            xtraTabControl5_SelectedPageChanged(null, null);

            StopProcesBtn.Enabled = (focused_prep_raw_mat_row != null && focused_prep_raw_mat_row.Checked == 2 && user_access.CanPost == 1);
            DeleteItemBtn.Enabled = (focused_prep_raw_mat_row != null && focused_prep_raw_mat_row.Checked == 0 && user_access.CanDelete == 1);
            EditItemBtn.Enabled = (focused_prep_raw_mat_row != null && focused_prep_raw_mat_row.Checked == 0 && user_access.CanModify == 1);
            CopyItemBtn.Enabled = (focused_prep_raw_mat_row != null && user_access.CanInsert == 1);
            ExecuteItemBtn.Enabled = (focused_prep_raw_mat_row != null && user_access.CanPost == 1);
            PrintItemBtn.Enabled = (focused_prep_raw_mat_row != null);
        }

        private void xtraTabControl5_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (focused_prep_raw_mat_row == null)
            {
                gridControl13.DataSource = null;
                ucRelDocGrid4.GetRelDoc(Guid.Empty);
                return;
            }

            using (var db = DB.SkladBase())
            {
                switch (xtraTabControl5.SelectedTabPageIndex)
                {
                    case 0:
                        gridControl13.DataSource = db.GetWayBillMakeDet(focused_prep_raw_mat_row.WbillId).ToList().OrderBy(o => o.Num).ToList();
                        gridView11.ExpandAllGroups();
                        break;

                    case 1:
                        gridControl15.DataSource = db.v_DeboningDet.AsNoTracking().Where(w => w.WBillId == focused_prep_raw_mat_row.WbillId).ToList();
                        break;

                    case 2:
                        ucRelDocGrid4.GetRelDoc(focused_prep_raw_mat_row.Id);
                        break;
                }
            }
        }

        private void PrepRawMatStartDate_EditValueChanged(object sender, EventArgs e)
        {
            PreparationRawMaterials();
        }

        private void PreparationRawMaterialsGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                popupMenu1.ShowPopup(p2);
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(PreparationRawMaterialsGridControl);
        }

        private void PreparationRawMaterialsGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            SetWBEditorBarBtn();
        }
    }
}
