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
    public partial class ucPlannedCalculation : DevExpress.XtraEditors.XtraUserControl
    {
        private string reg_layout_path = "ucPlannedCalculation\\PlannedCalculationGridView";

        private int w_type = 21;
        private int fun_id = 76;

        private UserSettingsRepository user_settings { get; set; }
        private UserAccess user_access { get; set; }
        private v_PlannedCalculation pc_focused_row { get; set; }

        public ucPlannedCalculation()
        {
            InitializeComponent();
        }


        private void ManufacturingUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                dateEdit2.EditValue = DateTime.Now.Date.AddDays(-30);
                dateEdit1.EditValue = DateTime.Now.Date.SetEndDay();

                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, DB.SkladBase());
                user_access = DB.SkladBase().UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);
            }
        }


        void GetPlannedCalculation()
        {
            var satrt_date = dateEdit2.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : dateEdit2.DateTime;
            var end_date = dateEdit1.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : dateEdit1.DateTime;

            int top_row = PlannedCalculationGridView.TopRowIndex;
            PlannedCalculationBS.DataSource = DB.SkladBase().v_PlannedCalculation.AsNoTracking().Where(w => w.OnDate >= satrt_date && w.OnDate <= end_date).OrderByDescending(o => o.OnDate).ToList();
            PlannedCalculationGridView.TopRowIndex = top_row;
        }



        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            GetPlannedCalculation();

        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            using (var wb_pc = new frmPlannedCalculation())
            {
                wb_pc.ShowDialog();
            }


            RefrechItemBtn.PerformClick();
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var pc_row = PlannedCalculationGridView.GetFocusedRow() as v_PlannedCalculation;

            using (var f = new frmPlannedCalculation(pc_row.Id))
            {
                f.ShowDialog();
            }

            RefrechItemBtn.PerformClick();
        }


        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditItemBtn.PerformClick();
        }

        private void ExecuteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            RefrechItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pc_focused_row == null)
            {
                return;
            }

            using (var db = new BaseEntities())
            {
                try
                {
                    if (MessageBox.Show(Resources.delete_wb, "Видалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {


                        var pc_row = db.PlannedCalculation.FirstOrDefault(w => w.Id == pc_focused_row.Id && w.SessionId == null);

                        if (pc_row != null)
                        {
                            db.DeleteWhere<PlannedCalculation>(w => w.Id == pc_focused_row.Id);
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


        private void PrintItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PrintDoc.Show(pc_focused_row.Id, 22, DB.SkladBase());
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            var pc_copy = DB.SkladBase().DocCopy(pc_focused_row.Id, DBHelper.CurrentUser.KaId).FirstOrDefault();

            using (var pc = new frmPlannedCalculation(pc_copy.out_doc_id))
            {
                pc.is_new_record = true;
                pc.ShowDialog();
            }

            RefrechItemBtn.PerformClick();
        }


        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {
            GetPlannedCalculation();
        }

        private void PlannedCalculationGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            pc_focused_row = ((GridView)sender).GetRow(e.FocusedRowHandle) as v_PlannedCalculation;

            if (pc_focused_row != null)
            {
                using (var db = DB.SkladBase())
                {
                    gridControl9.DataSource = db.v_PlannedCalculationDetDet.AsNoTracking().Where(w => w.PlannedCalculationId == pc_focused_row.Id).OrderBy(o => o.RecipeName).ToList();
                }
            }
            else
            {
                gridControl9.DataSource = null;
            }

            DeleteItemBtn.Enabled = (pc_focused_row != null && user_access.CanDelete == 1);
            EditItemBtn.Enabled = (pc_focused_row != null && user_access.CanModify == 1);
            CopyItemBtn.Enabled = (user_access.CanInsert == 1 && pc_focused_row != null);
            ExecuteItemBtn.Enabled = (pc_focused_row != null && user_access.CanPost == 1);
            PrintItemBtn.Enabled = (pc_focused_row != null);
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(gridControl8);
        }
    }
}
