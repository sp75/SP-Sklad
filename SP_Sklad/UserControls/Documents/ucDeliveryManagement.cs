﻿using System;
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
using SP_Sklad.Common;
using SP_Sklad.WBForm;
using DevExpress.Data;
using SP_Sklad.ViewsForm;
using SP_Sklad.Properties;
using DevExpress.XtraEditors;
using SP_Sklad.Reports;
using System.Collections;
using SP_Sklad.EditForm;

namespace SP_Sklad.UserControls
{
    public partial class ucDeliveryManagement : DevExpress.XtraEditors.XtraUserControl
    {
        private int fun_id = 106;
        private string reg_layout_path = "ucDeliveryManagement\\RoutesGridView";

        public v_DeliveryManagement row_route => RoutesGridView.GetFocusedRow() is NotLoadedObject ? null : RoutesGridView.GetFocusedRow() as v_DeliveryManagement;
        public v_WayBillCustomerOrder row_wb => WbGridView.GetFocusedRow() as v_WayBillCustomerOrder;

        private UserAccess user_access { get; set; }

        private int prev_rowHandle = 0;
        int row = 0;
        bool restore = false;
        public ucDeliveryManagement()
        {
            InitializeComponent();
        }

     
        protected override void OnCreateControl()
        {
    
            base.OnCreateControl();

            if (!DesignMode)
            {
                this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
            }
        }
    
        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            RoutesGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        public void GetData()
        {
            DeliveryManagementBS.DataSource = new BaseEntities().v_DeliveryManagement.AsNoTracking().ToList();
            GetDetailData();
        }

        public void EditItem()
        {
            new frmRouteEdit(row_route.Id).ShowDialog();
        }

        public void PrintItem()
        {
         //   PrintDoc.SettingMaterialPricesReport(row_route.Id, _db);
        }

        public void ExportToExcel()
        {
            IHelper.ExportToXlsx(RoutesGridControl);
        }

        private void GetDetailData()
        {
            if (row_route != null)
            {
                var top_row = WbGridView.TopRowIndex;
                bindingSource1.DataSource = DB.SkladBase().v_WayBillCustomerOrder.OrderBy(o=> o.OnDate).Where(w => w.RouteId == row_route.Id && w.Checked == 0 && w.WorkerId == DBHelper.CurrentUser.KaId).ToList();
                WbGridView.TopRowIndex = top_row;
            }
            else
            {
                bindingSource1.DataSource = null;
            }
        }

        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        private void SettingMaterialPricesUserControl_Load(object sender, EventArgs e)
        {
            barEditItem1.EditValue = DateTime.Now;

            if (!DesignMode)
            {
                RoutesGridView.SaveLayoutToStream(wh_layout_stream);
                RoutesGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

                user_access = new BaseEntities().UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);

                GetData();
            }
        }

        private void NewItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
        /*    NewItem();
            GetData();*/
        }

        private void SettingMaterialPricesGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
      
        }

        private void DeleteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
   //         DeleteItem();
    //        GetData();
        }

        private void RefrechItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetData();
        }


        private void PrintItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
         //   PrintItem();
        }

        private void EditItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditItem();
            GetData();
        }


        private void CopyItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
    //        CopyItem();
    //        GetData();
        }

  
        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportToExcel();
        }

        private void RoutesGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            /*     DeleteItemBtn.Enabled = (row_route != null &&  user_access.CanDelete == 1);


          ExecuteItemBtn.Enabled = (row_route != null && user_access.CanPost == 1);


          EditItemBtn.Enabled = (row_route != null &&  user_access.CanModify == 1);


          CopyItemBtn.Enabled = (row_route != null && user_access.CanModify == 1);

          PrintItemBtn.Enabled = (row_route != null);
          PrintItemBtn2.Enabled = (row_route != null);*/

            GetDetailData();
        }

        private void barButtonItem1_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            if (row_wb == null)
            {
                return;
            }

            DocEdit.WBEdit(row_wb.WbillId, row_wb.WType);

            GetDetailData();
        }

        private void barButtonItem3_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            var wbl = DB.SkladBase().WaybillList.FirstOrDefault(w => w.WbillId == row_wb.WbillId);
            if (wbl == null)
            {
                return;
            }

            if (wbl.Checked == 0)
            {
                using (var f = new frmWayBillOut(-1, null))
                {
                    var result = f._db.ExecuteWayBill(wbl.WbillId, null, DBHelper.CurrentUser.KaId).ToList().FirstOrDefault();
                    f.doc_id = result.NewDocId;
                    f.is_new_record = true;
                    f.ShowDialog();
                }
            }

            GetDetailData();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetDetailData();
        }

        private void RoutesGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void RoutesGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
                  if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                DeliveryManagementPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindDoc.Find(row_wb.Id, row_wb.WType, row_wb.OnDate);
        }

        private void WbGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                WayBillPopupMenu.ShowPopup(p2);
            }
        }

        private void repositoryItemDateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            var btn_edit = sender as DateEdit;

            if (WbGridView.SelectedRowsCount > 0)
            {
                Int32[] selectedRowHandles = WbGridView.GetSelectedRows();

                using (var db = DB.SkladBase())
                {
                    for (int i = 0; i < selectedRowHandles.Length; i++)
                    {
                        int selectedRowHandle = selectedRowHandles[i];
                        if (selectedRowHandle >= 0)
                        {
                            var wb_item = WbGridView.GetRow(selectedRowHandle) as v_WayBillCustomerOrder;
                            var wb = db.WaybillList.Find(wb_item.WbillId);
                            if (wb != null)
                            {
                                wb.OnDate = btn_edit.DateTime;
                            }
                        }
                    }
                    db.SaveChanges();
                }

                GetDetailData();
            }
        }
    }
}
