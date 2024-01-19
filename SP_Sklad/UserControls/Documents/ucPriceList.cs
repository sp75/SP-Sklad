using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.SkladData;
using DevExpress.XtraBars;
using SP_Sklad.Common;
using SP_Sklad.Reports;
using SP_Sklad.Properties;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;
using SP_Sklad.ViewsForm;
using SP_Sklad.WBForm;

namespace SP_Sklad.UserControls
{
    public partial class ucPriceList : DevExpress.XtraEditors.XtraUserControl
    {
        private int w_type = 10;
        private int fun_id = 37;
        private string reg_layout_path = "ucPriceList\\PriceListGridView";
        BaseEntities _db { get; set; }
        public BarButtonItem ExtEditBtn { get; set; }
        public BarButtonItem ExtDeleteBtn { get; set; }
        public BarButtonItem ExtExecuteBtn { get; set; }
        public BarButtonItem ExtCopyBtn { get; set; }
        public BarButtonItem ExtPrintBtn { get; set; }

        private v_PriceList focused_row => PriceListGridView.GetFocusedRow() is NotLoadedObject ? null : PriceListGridView.GetFocusedRow() as v_PriceList;

        private UserAccess user_access { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private Guid prev_focused_id = Guid.Empty;
        private int prev_top_row_index = 0;
        private int prev_rowHandle = 0;

        private Guid? find_id { get; set; }
        private bool restore = false;

        private List<KaTemplateList> ka_template_list { get; set; }
        private class KaTemplateList
        {
            public bool Check { get; set; }
            public int KaId { get; set; }
            public string KaName { get; set; }
        }

        public ucPriceList()
        {
            InitializeComponent();
            ka_template_list = new List<KaTemplateList>();
        }

        System.IO.Stream wh_layout_stream = new System.IO.MemoryStream();
        private void ucProjectManagement_Load(object sender, EventArgs e)
        {
            PriceListGridView.SaveLayoutToStream(wh_layout_stream);
            PriceListGridView.RestoreLayoutFromRegistry(IHelper.reg_layout_path + reg_layout_path);

            if (!DesignMode)
            {
                _db = new BaseEntities();
                user_access = _db.UserAccess.FirstOrDefault(w => w.FunId == fun_id && w.UserId == UserSession.UserId);

                GetData();
            }
        }

        public void NewItem()
        {
            using (var frm = new frmPriceList())
            {
                frm.ShowDialog();
            }
        }

        public void CopyItem()
        {
            using (var frm = new frmMessageBox("Інформація", Resources.wb_copy))
            {
                if (!frm.user_settings.NotShowMessageCopyDocuments && frm.ShowDialog() != DialogResult.Yes)
                {
                    return;
                }
            }

            var pl_id = DB.SkladBase().CopyPriceList(focused_row.PlId).FirstOrDefault();

            using (var frm = new frmPriceList(pl_id))
            {
                frm.ShowDialog();
            }
        }

        public void EditItem()
        {
            if (focused_row == null)
            {
                return;
            }

            using (var pl_frm = new frmPriceList(focused_row.PlId))
            {
                pl_frm.ShowDialog();
            }
        }

        public void DeleteItem()
        {
            var adj = _db.PriceList.Find(focused_row.PlId);

            if (adj != null)
            {
                _db.PriceList.Remove(adj);
                _db.SaveChanges();
            }
            else
            {
                MessageBox.Show(string.Format("Прайс-лист {0} не знайдено!", focused_row.Name));
            }
        }

        public void ExecuteItem()
        {
            ;
        }

        public void PrintItem()
        {
            PrintDoc.Show(focused_row.Id, 10, _db);
        }

        public void FindItem(Guid id, DateTime on_date)
        {
            find_id = id;
            StartDate.DateTime = on_date.Date;
            EndDate.DateTime = on_date.Date.SetEndDay();

            GetData();
        }

        public void ExportToExcel()
        {
            IHelper.ExportToXlsx(PriceListGridControl);
        }

        public void GetData()
        {
            prev_rowHandle = PriceListGridView.FocusedRowHandle;

            if (focused_row != null && !find_id.HasValue)
            {
                prev_top_row_index = PriceListGridView.TopRowIndex;
                prev_focused_id = focused_row.Id;
            }

            if (find_id.HasValue)
            {
                prev_top_row_index = -1;
                prev_focused_id = find_id.Value;
                find_id = null;
            }

            restore = true;

            PriceListGridControl.DataSource = null;
            PriceListGridControl.DataSource = GgridDataSource;

            SetWBEditorBarBtn();
        }

        public void SaveGridLayouts()
        {
            PriceListGridView.SaveLayoutToRegistry(IHelper.reg_layout_path + reg_layout_path);
        }

        private void ProjectManagementStartDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (StartDate.ContainsFocus)
            {
                GetData();
            }
        }

        private void EndDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (EndDate.ContainsFocus)
            {
                GetData();
            }
        }

        private void SetWBEditorBarBtn()
        {
            xtraTabControl3_SelectedPageChanged(null, null);

            ExtDeleteBtn.Enabled = false;
            ExtExecuteBtn.Enabled = false;
            ExtEditBtn.Enabled = false;
            ExtCopyBtn.Enabled = false;
            ExtPrintBtn.Enabled = false;

            if (focused_row == null)
            {
                return;
            }

            ExtDeleteBtn.Enabled = (focused_row != null && user_access.CanDelete == 1);
            ExtExecuteBtn.Enabled = false;
            ExtEditBtn.Enabled = (focused_row != null && user_access.CanModify == 1 );
            ExtCopyBtn.Enabled = (focused_row != null && user_access.CanModify == 1);
            ExtPrintBtn.Enabled = (focused_row != null);
        }

        private void GridPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            DeleteItemBtn.Enabled = ExtDeleteBtn.Enabled;
            ExecuteItemBtn.Enabled = ExtExecuteBtn.Enabled;
            EditItemBtn.Enabled = ExtEditBtn.Enabled;
            CopyItemBtn.Enabled = ExtCopyBtn.Enabled;
            PrintItemBtn.Enabled = ExtPrintBtn.Enabled;

            WbHistoryBtn.Enabled = IHelper.GetUserAccess(39)?.CanView == 1;
        }

    
        void OnRowSearchComplete(object rh)
        {
            int rowHandle = (int)rh;
            if (PriceListGridView.IsValidRowHandle(rowHandle))
            {
                FocusRow(PriceListGridView, rowHandle);
            }
        }

        public void FocusRow(GridView view, int rowHandle)
        {
            view.TopRowIndex = prev_top_row_index == -1 ? rowHandle : prev_top_row_index;
            view.FocusedRowHandle = rowHandle;
        }


        private void NewItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            NewItem();
            GetData();
        }

        private void CopyItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            CopyItem();
            GetData();
        }

        private void WbHistoryBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
         /*   using (var frm = new frmLogHistory(24, wb_focused_row.WbillId))
            {
                frm.ShowDialog();
            }*/
        }

        private void ExportToExcelBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportToExcel();
        }

        private void DeleteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteItem();
            GetData();
        }

        private void RefrechItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetData();
        }

        private void ExecuteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExecuteItem();
            GetData();
        }

        private void EditItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditItem();
            GetData();
        }

        private void PrintItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            PrintItem();
        }

        private void RestoreSettingsGridBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            wh_layout_stream.Seek(0, System.IO.SeekOrigin.Begin);

            PriceListGridView.RestoreLayoutFromStream(wh_layout_stream);
        }


        private void GgridDataSource_DismissQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            if (e.Tag == null)
                return;

            ((BaseEntities)e.Tag).Dispose();
        }

        private void GgridDataSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {

            var satrt_date = StartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : StartDate.DateTime;
            var end_date = EndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : EndDate.DateTime;

            BaseEntities objectContext = new BaseEntities();
            var list = objectContext.v_PriceList.Where(w => w.OnDate >= satrt_date && w.OnDate <= end_date);
            e.QueryableSource = list;

            e.Tag = objectContext;
        }

        private void PriceListGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (focused_row == null || !restore)
            {
                return;
            }

            int rowHandle = PriceListGridView.LocateByValue("Id", prev_focused_id, OnRowSearchComplete);
            if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
            {
                FocusRow(PriceListGridView, rowHandle);
            }
            else
            {
                PriceListGridView.FocusedRowHandle = prev_rowHandle;
            }

            restore = false;
        }


        private void PriceListGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void PriceListGridView_ColumnFilterChanged(object sender, EventArgs e)
        {
            SetWBEditorBarBtn();
        }

        private void PriceListGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItem();
            GetData();
        }

        private void PriceListGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                GridPopupMenu.ShowPopup(p2);
            }
        }

        private void xtraTabControl3_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (focused_row == null)
            {
                gridControl1.DataSource = null;
                ka_template_list.Clear();

                return;
            }

            switch (xtraTabControl3.SelectedTabPageIndex)
            {
                case 0:

                    gridControl1.DataSource = _db.v_PriceListDet.AsNoTracking().Where(w => w.PlId == focused_row.PlId).OrderBy(o => o.Num).ToList();
                    break;

                case 1:
                    ka_template_list = _db.PriceList.FirstOrDefault(w => w.PlId == focused_row.PlId).Kagent.Select(s => new KaTemplateList
                    {
                        Check = true,
                        KaId = s.KaId,
                        KaName = s.Name
                    }).ToList();


                    KaTemplateListGridControl.DataSource = ka_template_list;
                    break;
            }

        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            KaTemplateListGridView.CloseEditor();

            using (var db = DB.SkladBase())
            {
                int wb_count = 0;
                foreach (var kagent in ka_template_list.Where(w => w.Check))
                {

                    var _wb = db.WaybillList.Add(new WaybillList()
                    {
                        Id = Guid.NewGuid(),
                        WType = -16,
                        OnDate = DBHelper.ServerDateTime(),
                        Num = db.GetDocNum("wb(-16)").FirstOrDefault(),
                        CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                        OnValue = 1,
                        PersonId = DBHelper.CurrentUser.KaId,
                        EntId = DBHelper.Enterprise.KaId,
                        UpdatedBy = DBHelper.CurrentUser.UserId,
                        Nds = 0,
                        KaId = kagent.KaId,
                        Notes = focused_row.Notes,
                        Kontragent = db.Kagent.Find(kagent.KaId)
                    });

                    if (_wb.Kontragent.RouteId.HasValue)
                    {
                        var r = _db.Routes.FirstOrDefault(w => w.Id == _wb.Kontragent.RouteId);
                        _wb.CarId = r.CarId;
                        _wb.RouteId = _wb.Kontragent.RouteId;
                        _wb.Received = r.Kagent1 != null ? r.Kagent1.Name : "";
                        _wb.DriverId = r.Kagent1 != null ? (int?)r.Kagent1.KaId : null;
                    }


                    db.SaveChanges();

                    db.CreateOrderByPriceList(_wb.WbillId, focused_row.PlId);

                    ++wb_count;
                }

                MessageBox.Show(string.Format("Створено {0} замовлень !", wb_count));
            }
        }
    }
}
