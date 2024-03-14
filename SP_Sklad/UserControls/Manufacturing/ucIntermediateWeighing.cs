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
    public partial class ucIntermediateWeighing : DevExpress.XtraEditors.XtraUserControl
    {
        private int w_type = 24;
        private int fun_id = 83;
        private string reg_layout_path = "ucIntermediateWeighing\\IntermediateWeighingGridView";

        private UserAccess user_access { get; set; }
        private UserSettingsRepository user_settings { get; set; }
        private v_IntermediateWeighing intermediate_weighing_focused_row => IntermediateWeighingGridView.GetFocusedRow() as v_IntermediateWeighing;

        public ucIntermediateWeighing()
        {
            InitializeComponent();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void ManufacturingUserControl_Load(object sender, EventArgs e)
        {
            IntermediateWeighingGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                IntermediateWeighingStartDate.EditValue = DateTime.Now.Date;
                IntermediateWeighingEndDate.EditValue = DateTime.Now.Date.SetEndDay();
                lookUpEdit3.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведений" }, new { Id = 0, Name = "Новий" } };
                lookUpEdit3.EditValue = -1;

                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, DB.SkladBase());
                user_access = DB.SkladBase().UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);
            }
        }
        public void SaveGridLayouts()
        {
            IntermediateWeighingGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        public void NewItem()
        {
            using (var frm = new frmManufacturing(DB.SkladBase(), 0))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    using (var wb_iw = new frmIntermediateWeighing(frm.wb_focused_row.WbillId, null))
                    {
                        wb_iw.ShowDialog();
                    }
                }
            }
        }

        public void DeleteItem()
        {
            if (intermediate_weighing_focused_row == null)
            {
                return;
            }

            using (var db = new BaseEntities())
            {
                try
                {
                    if (MessageBox.Show(Resources.delete_wb, "Відалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        var iw_row = db.IntermediateWeighing.FirstOrDefault(w => w.Id == intermediate_weighing_focused_row.Id && w.SessionId == null);
                        if (iw_row != null)
                        {
                            db.DeleteWhere<IntermediateWeighing>(w => w.Id == intermediate_weighing_focused_row.Id);
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


        void GetIntermediateWeighing()
        {
            int top_row = IntermediateWeighingGridView.TopRowIndex;
            var satrt_date = IntermediateWeighingStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : IntermediateWeighingStartDate.DateTime;
            var end_date = IntermediateWeighingEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : IntermediateWeighingEndDate.DateTime;

            using (var db = DB.SkladBase())
            {
                IntermediateWeighingBS.DataSource = db.v_IntermediateWeighing.AsNoTracking().Where(w => w.OnDate > satrt_date && w.OnDate <= end_date).OrderBy(o => o.OnDate).ToList();
            }

            IntermediateWeighingGridView.TopRowIndex = top_row;

            SetWBEditorBarBtn();
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetIntermediateWeighing();
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NewItem();
            GetIntermediateWeighing();
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var wb_iw = new frmIntermediateWeighing(intermediate_weighing_focused_row.WbillId, intermediate_weighing_focused_row.Id))
            {
                if (wb_iw.ShowDialog() == DialogResult.OK)
                {
                    GetIntermediateWeighing();
                }
            }

        }

        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteItem();

            GetIntermediateWeighing();
        }


        private void IntermediateWeighingStartDate_EditValueChanged(object sender, EventArgs e)
        {
            GetIntermediateWeighing();
        }

        private void IntermediateWeighingGridView_FocusedRowObjectChanged_1(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void SetWBEditorBarBtn()
        {
            xtraTabControl6_SelectedPageChanged(null, null);

            DeleteItemBtn.Enabled = (intermediate_weighing_focused_row != null && intermediate_weighing_focused_row.Checked == 0 && user_access.CanDelete == 1 && intermediate_weighing_focused_row.WbChecked == 0);
            EditItemBtn.Enabled = (intermediate_weighing_focused_row != null && /*intermediate_weighing_focused_row.Checked == 0 &&*/ user_access.CanModify == 1 && intermediate_weighing_focused_row.WbChecked == 0);
            CopyItemBtn.Enabled = (intermediate_weighing_focused_row != null && user_access.CanInsert == 1);
            ExecuteItemBtn.Enabled = (intermediate_weighing_focused_row != null && user_access.CanPost == 1);
            PrintItemBtn.Enabled = (intermediate_weighing_focused_row != null);
        }

        private void xtraTabControl6_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (intermediate_weighing_focused_row == null)
            {

                IntermediateWeighingDetBS.DataSource = null;
                ucRelDocGrid5.GetRelDoc(Guid.Empty);
                return;
            }

            using (var db = DB.SkladBase())
            {
                switch (xtraTabControl6.SelectedTabPageIndex)
                {
                    case 0:
                        var list = DB.SkladBase().v_IntermediateWeighingDet.AsNoTracking().Where(w => w.IntermediateWeighingId == intermediate_weighing_focused_row.Id).OrderBy(o => o.CreatedDate).ToList();

                        int top_row = WaybillDetInGridView.TopRowIndex;
                        IntermediateWeighingDetBS.DataSource = list;
                        WaybillDetInGridView.TopRowIndex = top_row;
                        break;

                    case 1:
                        ucRelDocGrid5.GetRelDoc(intermediate_weighing_focused_row.Id);
                        break;
                }
            }
        }

        private void IntermediateWeighingGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(IntermediateWeighingGridControl); ;
        }


        private void IntermediateWeighingGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                GridPopupMenu.ShowPopup(p2);
            }
        }
    }
}
