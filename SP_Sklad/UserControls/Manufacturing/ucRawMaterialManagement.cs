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
using SP_Sklad.WBForm;
using SP_Sklad.Common;
using SP_Sklad.Properties;
using System.IO;
using System.Diagnostics;

using DevExpress.Data;

namespace SP_Sklad.MainTabs
{
    public partial class ucRawMaterialManagement : DevExpress.XtraEditors.XtraUserControl
    {
        private string reg_layout_path = "ucRawMaterialManagement\\RawMaterialManagementGridView";

        private int w_type = 28;
        private int fun_id = 92;

        private UserSettingsRepository user_settings { get; set; }
        private UserAccess user_access { get; set; }

        private v_RawMaterialManagement focused_raw_material_management => RawMaterialManagementGridView.GetFocusedRow() is NotLoadedObject ? null : RawMaterialManagementGridView.GetFocusedRow() as v_RawMaterialManagement;

        int restore_row = 0;
        bool restore = false;

        public ucRawMaterialManagement()
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
            RawMaterialManagementGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        private void ManufacturingUserControl_Load(object sender, EventArgs e)
        {
            RawMaterialManagementGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                RawMaterialManagementStartDate.EditValue = DateTime.Now.Date.AddDays(-1);
                RawMaterialManagementEndDate.EditValue = DateTime.Now.Date.SetEndDay();
                RawMaterialManagementStatus.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведений" }, new { Id = 0, Name = "Новий" } };
                RawMaterialManagementStatus.EditValue = -1;

                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, DB.SkladBase());
                user_access = DB.SkladBase().UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);
            }
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            restore_row = RawMaterialManagementGridView.FocusedRowHandle;
            restore = true;

            RawMaterialManagementGridControl.DataSource = null;
            RawMaterialManagementGridControl.DataSource = RawMaterialManagementSource;
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var rmm_make = new frmRawMaterialManagement(null))
            {
                rmm_make.ShowDialog();
            }


            RefrechItemBtn.PerformClick();
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = new BaseEntities())
            {
                using (var rmm_make = new frmRawMaterialManagement(focused_raw_material_management.Id))
                {
                    if (rmm_make.ShowDialog() == DialogResult.OK)
                    {
                        ;
                    }
                }
            }

            RefrechItemBtn.PerformClick();
        }



        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditItemBtn.PerformClick();
        }

        private void ExecuteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = new BaseEntities())
            {
                if (focused_raw_material_management.DocType == 1)
                {
                    var new_wb_in = ExecuteDocument.ExecuteRawMaterialManagement(focused_raw_material_management.Id, db);

                    if (new_wb_in != Guid.Empty)
                    {
                        ;
                        /* using (var f = new frmWayBillIn(1))
                         {
                             f.is_new_record = true;
                             f.doc_id = new_wb_in;
                             f.ShowDialog();
                         }*/
                    }
                    else
                    {
                        MessageBox.Show("Не можливо провести документ, одна із причин, не вказаний постачальник або ціна товара!");
                    }
                }

                if (focused_raw_material_management.DocType == -1)
                {
                    int? new_wb_move = null;
                    Guid new_write_of = Guid.Empty;
                    try
                    {
                        new_wb_move = ExecuteDocument.ExecuteRawMaterialManagementMove(focused_raw_material_management.Id, db);

                        if (new_wb_move != null)
                        {
                            using (var m_f = new frmWayBillMove(new_wb_move))
                            {
                                m_f.is_new_record = true;
                                if (m_f.ShowDialog() == DialogResult.OK)
                                {
                                    new_write_of = ExecuteDocument.ExecuteRawMaterialManagementWBWriteOff(focused_raw_material_management.Id, db);

                                    if (new_write_of != Guid.Empty)
                                    {
                                        using (var f = new frmWBWriteOff())
                                        {
                                            f.doc_id = new_write_of;
                                            f.TurnDocCheckBox.Checked = true;
                                            f.is_new_record = true;
                                            f.ShowDialog();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        if (new_wb_move.HasValue)
                        {
                            db.DeleteWhere<WaybillList>(w => w.WbillId == new_wb_move);
                            db.DeleteWhere<WaybillList>(w => w.Id == new_write_of);
                        }
                    }
                }


                RefrechItemBtn.PerformClick();
            }
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_raw_material_management == null)
            {
                return;
            }

            using (var db = new BaseEntities())
            {
                try
                {
                    if (MessageBox.Show(Resources.delete_wb, "Відалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {

                        var rmm_row = db.RawMaterialManagement.FirstOrDefault(w => w.Id == focused_raw_material_management.Id && w.SessionId == null);
                        if (rmm_row != null)
                        {
                            DB.SkladBase().DeleteWhere<RawMaterialManagement>(w => w.Id == focused_raw_material_management.Id);
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

        private void StopProcesBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void PrintItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ;
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ;
        }

        private void RawMaterialManagementSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            int status = (int)RawMaterialManagementStatus.EditValue;

            var _db = DB.SkladBase();

            var rmm = _db.v_RawMaterialManagement.Where(w => w.OnDate >= RawMaterialManagementStartDate.DateTime && w.OnDate <= RawMaterialManagementEndDate.DateTime && (w.Checked == status || status == -1));

            e.QueryableSource = rmm;

            e.Tag = _db;
        }

        private void RawMaterialManagementGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (focused_raw_material_management == null)
            {
                RawMaterialManagementDetGridControl.DataSource = null;
                ucRelDocGrid6.GetRelDoc(Guid.Empty);
            }

            if (!restore)
            {
                return;
            }

            RawMaterialManagementGridView.TopRowIndex = restore_row;
            RawMaterialManagementGridView.FocusedRowHandle = restore_row;
            restore = false;
        }

        private void RawMaterialManagementGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            xtraTabControl7_SelectedPageChanged(sender, null);

            DeleteItemBtn.Enabled = (focused_raw_material_management != null && user_access.CanDelete == 1 && focused_raw_material_management.Checked == 0);
            EditItemBtn.Enabled = (focused_raw_material_management != null && user_access.CanModify == 1 && focused_raw_material_management.Checked == 0);
            CopyItemBtn.Enabled = (focused_raw_material_management != null && user_access.CanInsert == 1);
            ExecuteItemBtn.Enabled = (focused_raw_material_management != null && user_access.CanPost == 1 && focused_raw_material_management.Checked == 0);
            PrintItemBtn.Enabled = (focused_raw_material_management != null);
        }


        private void xtraTabControl7_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (focused_raw_material_management == null)
            {
                RawMaterialManagementDetGridControl.DataSource = null;
                ucRelDocGrid6.GetRelDoc(Guid.Empty);

                return;
            }

            using (var db = DB.SkladBase())
            {
                switch (xtraTabControl7.SelectedTabPageIndex)
                {
                    case 0:
                        var list = DB.SkladBase().v_RawMaterialManagementDet.AsNoTracking()
                            .Where(w => w.RawMaterialManagementId == focused_raw_material_management.Id)
                            .OrderBy(o => o.OnDate).ToList();

                        RawMaterialManagementDetGridControl.DataSource = list;
                        break;

                    case 1:
                        ucRelDocGrid6.GetRelDoc(focused_raw_material_management.Id);
                        break;

                    case 2:
                        gridControl10.DataSource = db.Database.SqlQuery<pos_rmmd>(@"select x.*, pos.ActualRemain, pos.OnDate, wbd.Amount Total, wb.Num
from
(   
   SELECT 
     rd.PosId,
     rd.BarCode, 
	 sum(case when rmm.DocType = 1 then rd.Amount else 0 end) AmountIn, 
	 sum(case when rmm.DocType = -1 then rd.Amount else 0 end) AmountOut,
	 sum(case when rmm.DocType = 1 then 1 else 0 end) CountIn, 
	 sum(case when rmm.DocType = -1 then 1 else 0 end) CountOut

  FROM RawMaterialManagementDet rd
  join RawMaterialManagement rmm on rmm.Id = rd.RawMaterialManagementId
  where rd.PosId in ( select PosId from RawMaterialManagementDet where RawMaterialManagementId = {0} )
  group by rd.PosId, rd.BarCode
)x
join v_PosRemains pos on pos.PosId = x.PosId
join WaybillDet wbd on wbd.PosId = x.PosId
join WaybillList wb on wb.WbillId = wbd.WbillId", focused_raw_material_management.Id).ToList();
                        break;

                }
            }

        }

        public class pos_rmmd
        {
            public string BarCode { get; set; }
            public decimal AmountIn { get; set; }
            public decimal AmountOut { get; set; }
            public decimal ActualRemain { get; set; }
            public DateTime OnDate { get; set; }
            public decimal Total { get; set; }
            public string Num { get; set; }
            public int CountIn { get; set; }
            public int CountOut { get; set; }
        }

        private void RawMaterialManagementStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (RawMaterialManagementStartDate.ContainsFocus || RawMaterialManagementEndDate.ContainsFocus || RawMaterialManagementStatus.ContainsFocus)
            {
                RefrechItemBtn.PerformClick();
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(RawMaterialManagementGridControl);
        }
    }
}
