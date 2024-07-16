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
using DevExpress.XtraBars;
using DevExpress.Data;
using SP_Sklad.Common;
using DevExpress.XtraGrid;
using SP_Sklad.WBForm;
using System.Data.Entity;
using DevExpress.XtraEditors;
using SP_Sklad.Properties;
using SP_Sklad.Reports;
using SP_Sklad.ViewsForm;
using SP_Sklad.FinanseForm;
using DevExpress.XtraGrid.Views.Grid;

namespace SP_Sklad.UserControls
{
    public partial class ucDocumentDetails : DevExpress.XtraEditors.XtraUserControl
    {
        private string reg_layout_path = "ucDocumentDetails\\WaybillDetGridView";

        private v_WaybillDet wb_focused_row => WaybillDetGridView.GetFocusedRow() is NotLoadedObject ? null : WaybillDetGridView.GetFocusedRow() as v_WaybillDet;
        
        private UserSettingsRepository user_settings { get; set; }
     //   private FocusGridRow fgr;

        public ucDocumentDetails()
        {
            InitializeComponent();
            WaybillDetGridControl.DataSource = null;

            StartDateEditItem.EditValue = DateTime.Now.Date.AddDays(-1);
            EndDateEditItem.EditValue = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

        //    fgr = new FocusGridRow(WaybillDetGridView);
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            WaybillDetGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        public void ExportToExcel()
        {
            IHelper.ExportToXlsx(WaybillDetGridControl);
        }

        private void WayBillInSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            List<int> w_type = new List<int>() { -1, 1, 6, -6 };
            var start_dt = (DateTime)StartDateEditItem.EditValue;
            var end_dt = (DateTime)EndDateEditItem.EditValue;

            BaseEntities objectContext = new BaseEntities();
            var list = objectContext.v_WaybillDet.Where(w => ((w.PosParent ?? 0) == 0) && w_type.Contains(w.WType) && w.WbChecked == 1 && w.WbOnDate >= start_dt && w.WbOnDate < end_dt).OrderByDescending(o => o.WbOnDate);
                            
            e.QueryableSource = list;
            e.Tag = objectContext;
        }

        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        private void WayBillInUserControl_Load(object sender, EventArgs e)
        {
            WaybillDetGridView.SaveLayoutToStream(wh_layout_stream);

            WaybillDetGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());
                WaybillDetGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
            }
        }

        public void GetData(bool restore = true)
        {
       //     fgr.SetPrevData((wb_focused_row?.WbillId ?? 0), restore);

            WaybillDetGridControl.DataSource = null;
            WaybillDetGridControl.DataSource = WayBillIDetSource;
        }


        private void WbGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (wb_focused_row == null )
            {
                return;
            }

      //      fgr.SetRowFocus("PosId");
        }


        private void RefrechItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetData();
        }

        private void EditItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
           
        }

        private void WbGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if(e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                WbListPopupMenu.ShowPopup(p2);
            }
        }

        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
  
        }

        private void WbListPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
          
        }

        private void WbGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
        
        }

        private void WbGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
          
        }


        private void WayBillInSource_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (e.Tag == null)
                return;

            ((BaseEntities)e.Tag).Dispose();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            wh_layout_stream.Seek(0, System.IO.SeekOrigin.Begin);

            WaybillDetGridView.RestoreLayoutFromStream(wh_layout_stream);
        }


        private void ExportToExcelBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportToExcel();
        }


        private void StartDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
