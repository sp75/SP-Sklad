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
    public partial class ucRoutesManagement : DevExpress.XtraEditors.XtraUserControl
    {
        private int fun_id = 106;
        private int wb_fun_id = 64;
        private string reg_layout_path = "ucRoutesManagement\\RoutesGridView";

        public v_DeliveryManagement row_route => RoutesGridView.GetFocusedRow() is NotLoadedObject ? null : RoutesGridView.GetFocusedRow() as v_DeliveryManagement;
        public v_WayBillCustomerOrder row_wb => WbGridView.GetFocusedRow() as v_WayBillCustomerOrder;

        private UserAccess user_access { get; set; }
        private UserAccess wb_user_access { get; set; }

        private int prev_rowHandle = 0;
        int row = 0;
        bool restore = false;
        public ucRoutesManagement()
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
            using (var frm = new frmRouteEdit(row_route.Id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    using (var db = DB.SkladBase())
                    {
                        var r = db.Routes.Find(Convert.ToInt32(row_route.Id));
                        for (int i = 0; i < WbGridView.RowCount; i++)
                        {
                            var dr = WbGridView.GetRow(i) as v_WayBillCustomerOrder;
                            var wb = db.WaybillList.Find(dr.WbillId);
                            if (wb != null)
                            {
                                wb.CarId = r.CarId;
                                wb.DriverId = r.DriverId;
                                wb.Received = r.Kagent1.Name;
                            }
                        }
                        db.SaveChanges();
                    }
                    GetDetailData();
                }
            }
        }

        public void PrintItem()
        {
            if (row_wb == null)
            {
                return;
            }

            PrintDoc.Show(row_wb.Id, row_wb.WType, new BaseEntities());
        }

        public void ExportToExcel()
        {
            IHelper.ExportToXlsx(RoutesGridControl);
        }

        private void GetDetailData()
        {
            if (row_route != null)
            {
                var end_date = DateTime.Now.Date.AddDays(1);

                var top_row = WbGridView.TopRowIndex;
                bindingSource1.DataSource = DB.SkladBase().v_WayBillCustomerOrder
                    .OrderBy(o=> o.OnDate)
                    .Where(w => w.RouteId == row_route.Id && (w.Checked == 0 || w.Checked == 2) && w.WorkerId == DBHelper.CurrentUser.KaId && w.OnDate < end_date)
                    .ToList();
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
                wb_user_access = new BaseEntities().UserAccess.FirstOrDefault(w => w.FunId == wb_fun_id && w.UserId == UserSession.UserId);

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

            if (row_wb == null)
            {
                return;
            }

            if (XtraMessageBox.Show($"Ви дійсно бажаєте видалити замовлення від клієнта {row_wb.Num}?", "Видалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                using (var _db = new BaseEntities())
                {
                    var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == row_wb.WbillId && (w.SessionId == null || w.SessionId == UserSession.SessionId) && w.Checked == 0);
                    if (wb != null)
                    {
                        _db.WaybillList.Remove(wb);

                        _db.SaveChanges();
                    }
                    else
                    {
                        XtraMessageBox.Show(Resources.deadlock);
                    }
                }
            }
            GetDetailData();
           
        }

        private void RefrechItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetData();
        }


        private void PrintItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            PrintItem();
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
            EditItemBtn.Enabled = (row_route != null && user_access.CanModify == 1);

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

        private List<v_WayBillCustomerOrder> GetSelectedCustomerOrder()
        {
            var result = new List<v_WayBillCustomerOrder>();
            if (WbGridView.SelectedRowsCount > 0)
            {
                Int32[] selectedRowHandles = WbGridView.GetSelectedRows();

                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    int selectedRowHandle = selectedRowHandles[i];
                    if (selectedRowHandle >= 0)
                    {
                        var wb_item = WbGridView.GetRow(selectedRowHandle) as v_WayBillCustomerOrder;
                        result.Add(wb_item);
                    }
                }
            }
            return result;
        }

        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            EditWbBtn.PerformClick();
        }

        private void WbGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DeleteItemBtn.Enabled = (row_wb != null && row_wb.Checked == 0 && wb_user_access.CanDelete == 1);
            ExecuteWbBtn.Enabled = (row_wb != null && row_wb.Checked == 0 && wb_user_access.CanPost == 1);
            EditWbBtn.Enabled = (row_wb != null && wb_user_access.CanModify == 1 && row_wb.Checked == 0);
            PrintItemBtn.Enabled = (row_wb != null);
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.PrintSelectedWayBill(-16, GetSelectedCustomerOrder().Select(s => s.Id).ToList());
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            var route_id = IHelper.ShowDirectList(null, 20);
            if (route_id != null)
            {
                using (var db = DB.SkladBase())
                {
                    var r = db.Routes.Find(Convert.ToInt32(route_id));
                    foreach (var wb_item in GetSelectedCustomerOrder())
                    {
                        var wb = db.WaybillList.Find(wb_item.WbillId);
                        if (wb != null)
                        {
                            wb.RouteId = Convert.ToInt32(route_id);
                            wb.CarId = r.CarId;
                            wb.DriverId = r.DriverId;
                            wb.Received = r.Kagent1.Name;
                        }
                    }
                    db.SaveChanges();
                }
                GetDetailData();
            }
        }
    }
}
