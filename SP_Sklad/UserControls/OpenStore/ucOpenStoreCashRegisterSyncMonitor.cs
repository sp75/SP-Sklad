using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.EditForm;
using SP_Sklad.Common;
using DevExpress.XtraTreeList;
using System.IO;
using System.Diagnostics;
using SP_Sklad.Properties;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using SkladEngine.DBFunction;
using SP_Sklad.WBForm;
using DevExpress.XtraGrid;
using OpenStore.Tranzit.Base;

namespace SP_Sklad.UserControls.Warehouse
{
    public partial class ucOpenStoreCashRegisterSyncMonitor : DevExpress.XtraEditors.XtraUserControl
    {
        public v_CashRegisterSyncMonitor row_smp => PaymentGridView.GetFocusedRow() is NotLoadedObject ? null : PaymentGridView.GetFocusedRow() as v_CashRegisterSyncMonitor;

        private int prev_focused_id = 0;
        private int prev_top_row_index = 0;
        private int prev_rowHandle = 0;
        private bool _restore = false;
        public ucOpenStoreCashRegisterSyncMonitor()
        {
            InitializeComponent();
        }

        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                var user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());

                PaymentGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);

                GetData();
            }
        }

        public void GetData(bool restore = true)
        {

            prev_rowHandle = PaymentGridView.FocusedRowHandle;

            if (row_smp != null)
            {
                prev_top_row_index = PaymentGridView.TopRowIndex;
                //     prev_focused_id = row_smp.Id;
            }

            _restore = restore;

            Tranzit_OSEntities objectContext = new Tranzit_OSEntities();
            var list = objectContext.v_CashRegisterSyncMonitor.OrderBy(o=> o.SyncDate).ToList().Where(w => w.SyncDate > DateTime.Now.AddDays(-30)).ToList();

            PaymentGridControl.DataSource = list;

            //    PaymentGridControl.DataSource = null;
            //    PaymentGridControl.DataSource = PaymentSource;
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetData();
        }

        private void barButtonItem11_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(PaymentGridControl);
        }

        private void SettingMaterialPricesSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
         /*   Tranzit_OSEntities objectContext = new Tranzit_OSEntities();
            var area_id = (int)KagentList.EditValue;
            var status = (int)wbStatusList.EditValue;

            var list = objectContext.v_Payment.Where(w => w.OnDate >= wbStartDate.DateTime && w.OnDate <= wbEndDate.DateTime && (w.SAREAID == area_id || area_id == -1) && (status == -1 || status == w.FiscalReceipt) );
            e.QueryableSource = list;
            e.Tag = objectContext;*/
        }

        private void WhPosRemainsGridView_AsyncCompleted(object sender, EventArgs e)
        {
            /*   if (row_smp == null || !_restore)
               {
                   return;
               }

               int rowHandle = WhPosRemainsGridView.LocateByValue("Id", prev_focused_id, OnRowSearchComplete);
               if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
               {
                   FocusRow(WhPosRemainsGridView, rowHandle);
               }
               else
               {
                   WhPosRemainsGridView.FocusedRowHandle = prev_rowHandle;
               }

               _restore = false;*/
            PaymentGridView.ExpandAllGroups();
        }

        void OnRowSearchComplete(object rh)
        {
            int rowHandle = (int)rh;
            if (PaymentGridView.IsValidRowHandle(rowHandle))
            {
                FocusRow(PaymentGridView, rowHandle);
            }
        }

        public void FocusRow(GridView view, int rowHandle)
        {
    /*        view.TopRowIndex = prev_top_row_index == -1 ? rowHandle : prev_top_row_index;
            view.FocusedRowHandle = rowHandle;
            view.SelectRow(rowHandle);*/
        }

        private void WhPosRemainsGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
         /*   if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                PosBottomPopupMenu.ShowPopup(p2);
            }*/
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }

        private void PaymentGridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            var wh_row = PaymentGridView.GetRow(e.RowHandle) as v_CashRegisterSyncMonitor;

            if (wh_row != null && wh_row.SyncDate < DateTime.Now.AddMinutes(-30))
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }
    }
}
