using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.WBForm;
using SP_Sklad.Common;
using SP_Sklad.Properties;
using SP_Sklad.Reports;

using SP_Sklad.ViewsForm;
using System.Drawing;
using DevExpress.XtraGrid;

namespace SP_Sklad.MainTabs
{
    public partial class ucProductionPlanning : DevExpress.XtraEditors.XtraUserControl
    {
        private string reg_layout_path = "ucProductionPlanning\\ProductionPlansGridView";

        private int w_type = 20;
        private int fun_id = 74;

        private UserSettingsRepository user_settings { get; set; }
        private UserAccess user_access { get; set; }

        private ProductionPlansList_Result pp_focused_row => ProductionPlansGridView.GetFocusedRow() as ProductionPlansList_Result;

        public ucProductionPlanning()
        {
            InitializeComponent();
        }

        public void SaveGridLayouts()
        {
            ProductionPlansGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        private void ManufacturingUserControl_Load(object sender, EventArgs e)
        {
            ProductionPlansGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {

                SatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Розпочато виробництво" }, new { Id = 0, Name = "Актуальний" } };
                SatusList.EditValue = -1;

                PlanStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                PlanEndDate.EditValue = DateTime.Now.Date.SetEndDay();

                user_access = DB.SkladBase().UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);

                GetProductionPlans();
            }
        }

        public void NewItem()
        {
            using (var wb_pp = new frmProductionPlans(null))
            {
                wb_pp.ShowDialog();
            }
        }

        public void EditItem()
        {
            if (pp_focused_row == null)
            {
                return;
            }

            using (var f = new frmProductionPlans(pp_focused_row.Id))
            {
                f.ShowDialog();
            }

        }

        public void CopyItem()
        {
            if (pp_focused_row == null)
            {
                return;
            }

            var new_pp = DB.SkladBase().DocCopy(pp_focused_row.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();
            using (var wb_in = new frmProductionPlans(new_pp.out_doc_id))
            {
                wb_in.is_new_record = true;
                wb_in.ShowDialog();
            }
        }

        public void DeleteItem()
        {
            if (pp_focused_row == null)
            {
                return;
            }

            using (var db = new BaseEntities())
            {
                try
                {
                    if (MessageBox.Show(Resources.delete_wb, "Відалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {

                        var pp = db.ProductionPlans.FirstOrDefault(w => w.Id == pp_focused_row.Id && w.SessionId == null);
                        if (pp != null)
                        {
                            db.ProductionPlans.Remove(pp);
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

                if (pp_focused_row == null)
                {
                    return;
                }

                var pp = db.ProductionPlans.Find(pp_focused_row.Id);

                if (pp == null)
                {
                    MessageBox.Show(Resources.not_find_wb);
                    return;
                }
                if (pp.SessionId != null)
                {
                    MessageBox.Show(Resources.deadlock);
                    return;
                }

                var rel = db.GetRelDocList(pp_focused_row.Id).OrderBy(o => o.OnDate).ToList();
                if (rel.Any())
                {
                    MessageBox.Show(Resources.not_storno_wb, "Відміна проводки", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (pp.Checked == 1)
                {
                    pp.Checked = 0;
                }

                db.SaveChanges();
            }
        }

        public void PrintItem()
        {
            PrintDoc.Show(pp_focused_row.Id, 21, DB.SkladBase());
        }

        public void FindItem(Guid id, DateTime on_date)
        {
            ProductionPlansGridView.ClearColumnsFilter();
            ProductionPlansGridView.ClearFindFilter();
            PeriodComboBoxEdit.SelectedIndex = 0;
            PlanStartDate.DateTime = on_date.Date;
            PlanEndDate.DateTime = on_date.Date.SetEndDay();

            SatusList.EditValue = -1;
            GetProductionPlans();

            int rowHandle = ProductionPlansGridView.LocateByValue("Id", id);
            if (rowHandle != GridControl.InvalidRowHandle)
            {
                ProductionPlansGridView.FocusedRowHandle = rowHandle;
            }
        }

        public void GetProductionPlans()
        {
            if (SatusList.EditValue == null)
            {
                return;
            }

            var satrt_date = PlanStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : PlanStartDate.DateTime;
            var end_date = PlanEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : PlanEndDate.DateTime;

            int top_row = ProductionPlansGridView.TopRowIndex;
            ProductionPlansBS.DataSource = DB.SkladBase().ProductionPlansList(satrt_date, end_date, (int)SatusList.EditValue, DBHelper.CurrentUser.KaId).OrderByDescending(o=> o.OnDate).ToList();
            ProductionPlansGridView.TopRowIndex = top_row;
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetProductionPlans();
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            NewItem();

            RefrechItemBtn.PerformClick();
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditItem();


            RefrechItemBtn.PerformClick();
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

        }


        private void PrintItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintItem();
        }


        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CopyItem();
            RefrechItemBtn.PerformClick();
        }


        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(ProductionPlansGridControl);
        }

        private void ProductionPlansGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            if (pp_focused_row != null)
            {
                using (var db = DB.SkladBase())
                {
                    gridControl6.DataSource = db.v_ProductionPlanDet.Where(w => w.ProductionPlanId == pp_focused_row.Id).OrderBy(o => o.Num).ToList();
                    ucRelDocGrid3.GetRelDoc(pp_focused_row.Id);
                }
            }
            else
            {
                gridControl6.DataSource = null;
                ucRelDocGrid3.GetRelDoc(Guid.Empty);
            }

            DeleteItemBtn.Enabled = (pp_focused_row != null && pp_focused_row.Checked == 0 && user_access.CanDelete == 1);
            EditItemBtn.Enabled = (pp_focused_row != null && pp_focused_row.Checked == 0 && user_access.CanModify == 1);
            ExecuteItemBtn.Enabled = (pp_focused_row != null && user_access.CanPost == 1);
            PrintItemBtn.Enabled = (pp_focused_row != null);
            CopyItemBtn.Enabled = (user_access.CanInsert == 1 && pp_focused_row != null);
        }

        private void PlanStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (PlanStartDate.ContainsFocus || PlanEndDate.ContainsFocus  || SatusList.ContainsFocus)
            {
                GetProductionPlans();
            }
          
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            using (var frm = new frmSchedulingOrders())
            {
                frm.ShowDialog();
            }
        }

        private void ProductionPlansGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                GridPopupMenu.ShowPopup(p2);
            }
        }

        private void PeriodComboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            PlanEndDate.DateTime = DateTime.Now.Date.SetEndDay();
            switch (PeriodComboBoxEdit.SelectedIndex)
            {
                case 1:
                    PlanStartDate.DateTime = DateTime.Now.Date;
                    break;

                case 2:
                    PlanStartDate.DateTime = DateTime.Now.Date.StartOfWeek(DayOfWeek.Monday);
                    break;

                case 3:
                    PlanStartDate.DateTime = DateTime.Now.Date.FirstDayOfMonth();
                    break;

                case 4:
                    PlanStartDate.DateTime = new DateTime(DateTime.Now.Year, 1, 1);
                    break;
            }

            GetProductionPlans();
        }
    }
}
