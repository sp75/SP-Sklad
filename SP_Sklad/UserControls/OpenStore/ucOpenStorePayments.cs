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
    public partial class ucOpenStorePayments : DevExpress.XtraEditors.XtraUserControl
    {
        public v_Payment row_smp => PaymentGridView.GetFocusedRow() is NotLoadedObject ? null : PaymentGridView.GetFocusedRow() as v_Payment;

        private int prev_focused_id = 0;
        private int prev_top_row_index = 0;
        private int prev_rowHandle = 0;
        private bool _restore = false;
        public ucOpenStorePayments()
        {
            InitializeComponent();
        }

        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                //wbStartDate.EditValue = DateTime.Now.Date;
                //wbEndDate.EditValue = DateTime.Now.Date.SetEndDay();
                PeriodComboBoxEdit.SelectedIndex = 1;

                wbStatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Фіскальні" }, new { Id = 0, Name = "Не фіскальні" } };
                wbStatusList.EditValue = -1;

                repositoryItemLookUpEdit1.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 0, Name = "Готівка" }, new { Id = 1, Name = "Платіжна картка" }, new { Id = 2, Name = "Бонуси" }, new { Id = 4, Name = "Не фіскальний" } };

                KagentList.Properties.DataSource = new Tranzit_OSEntities().SAREA.ToList();

                var user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());

                PaymentGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
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

            var area = KagentList.GetSelectedDataRow() as SAREA;

            if(area == null)
            {
                return;
            }

            var status = (int)wbStatusList.EditValue;

            Tranzit_OSEntities objectContext = new Tranzit_OSEntities();
            var list = objectContext.v_Payment.Where(w => w.OnDate >= wbStartDate.DateTime && w.OnDate <= wbEndDate.DateTime && w.SAREAID == area.SAREAID && (status == -1 || (status == 0 && w.FRECNUM =="0") || (status ==1 && w.FRECNUM != "0"))).ToList();

            PaymentGridControl.DataSource = list;

            PaymentGridView.ExpandAllGroups();

            //     WhPosRemainsGridControl.DataSource = null;
            //    WhPosRemainsGridControl.DataSource = SalesSource;
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
            Tranzit_OSEntities objectContext = new Tranzit_OSEntities();
       //     var satrt_date = PDStartDate.DateTime;
       //     var end_date = PDEndDate.DateTime;

            var list = objectContext.v_Sales.Where(w => w.OnDate >= wbStartDate.DateTime && w.OnDate <= wbEndDate.DateTime);
            e.QueryableSource = list;
            e.Tag = objectContext;
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

            GetData();
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbStartDate.ContainsFocus)
            {
                GetData();
            }
        }

        private void wbEndDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbEndDate.ContainsFocus)
            {
                GetData();
            }
        }

        private void KagentList_EditValueChanged(object sender, EventArgs e)
        {
            if (KagentList.ContainsFocus)
            {
                GetData();
            }
        }

        private void wbStatusList_EditValueChanged(object sender, EventArgs e)
        {
            if (wbStatusList.ContainsFocus)
            {
                GetData();
            }
        }
    }
}
