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
using DevExpress.XtraGrid;
using SP_Sklad.Properties;

namespace SP_Sklad.UserControls
{
    public partial class ucExpedition : DevExpress.XtraEditors.XtraUserControl
    {
        BaseEntities _db { get; set; }
        public BarButtonItem EditBtn { get; set; }
        public BarButtonItem DeleteBtn { get; set; }
        public BarButtonItem ExecuteBtn { get; set; }
        public BarButtonItem CopyBtn { get; set; }
        public BarButtonItem PrintBtn { get; set; }

        public v_Expedition row_exp => ExpeditionsGridView.GetFocusedRow() is NotLoadedObject ? null : ExpeditionsGridView.GetFocusedRow() as v_Expedition;
        public v_ExpeditionDet row_smp_det => ExpeditionDetGridView.GetFocusedRow() as v_ExpeditionDet;

        private UserAccess user_access { get; set; }

        private Guid prev_focused_id = Guid.Empty;
        public Guid? find_id;
        bool restore = false;
        public ucExpedition()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            if (row_exp != null && !find_id.HasValue)
            {
                prev_focused_id = row_exp.Id;
            }

            if(find_id.HasValue)
            {
                prev_focused_id = find_id.Value;
                find_id = null;
            }

            restore = true;

            ExpeditionsGridControl.DataSource = null;
            ExpeditionsGridControl.DataSource = ExpeditionsSource;

            GetDetailData();
        }

        public void NewItem()
        {
            using (var frm = new frmExpedition())
            {
                frm.ShowDialog();
            }
        }

        public void ExecuteItem()
        {
            if (row_exp == null)
            {
                return;
            }

            var exp = _db.Expedition.Find(row_exp?.Id);

            if (exp == null)
            {
                MessageBox.Show(Resources.not_find_wb);
                return;
            }
            if (exp.SessionId != null)
            {
                MessageBox.Show(Resources.deadlock);
                return;
            }

            if (exp.Checked == 1)
            {
                exp.Checked = 0;
            }
            else
            {
                exp.Checked = 1;

                foreach (var item in _db.v_ExpeditionDet.Where(w => w.ExpeditionId == exp.Id))
                {
                    if (item.RouteId.HasValue && item.Checked == 1)
                    {
                        var exp_wb = _db.WaybillList.Find(item.WbillId);
                        exp_wb.ShipmentDate = exp.OnDate.AddTicks(item.RouteDuration ?? 0);
                    }
                }
            }

            _db.SaveChanges();
        }

        private void SettingMaterialPricesGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DeleteBtn.Enabled = (row_exp != null && row_exp.Checked == 0 && user_access.CanDelete == 1);
            ExecuteBtn.Enabled = (row_exp != null && user_access.CanPost == 1);
            EditBtn.Enabled = (row_exp != null && row_exp.Checked == 0 && user_access.CanModify == 1);
            CopyBtn.Enabled = (row_exp != null && user_access.CanModify == 1);
            PrintBtn.Enabled = (row_exp != null);

            GetDetailData();

            if (row_exp != null)
            {
                ucRelDocGrid1.GetRelDoc(row_exp.Id);
            }
            else
            {
                ucRelDocGrid1.GetRelDoc(Guid.Empty);
            }
        }


        private void GetDetailData()
        {
            if (row_exp != null)
            {
                ExpeditionDetBS.DataSource = DB.SkladBase().v_ExpeditionDet.Where(w => w.ExpeditionId == row_exp.Id).OrderBy(o => o.CreatedAt).ToList();
            }
            else
            {
                ExpeditionDetBS.DataSource = null;
            }
        }

        private void SettingMaterialPricesUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                _db = new BaseEntities();
                user_access = _db.UserAccess.FirstOrDefault(w => w.FunId == 103 && w.UserId == UserSession.UserId);
            }
        }

        private void SettingMaterialPricesGridView_DoubleClick(object sender, EventArgs e)
        {
            EditBtn.PerformClick();
        }

        private void SettingMaterialPricesSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var list = DB.SkladBase().v_Expedition.AsQueryable();
            e.QueryableSource = list;
        }

        private void SettingMaterialPricesGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (row_exp == null || !restore)
            {
                return;
            }

            int rowHandle = ExpeditionsGridView.LocateByValue("Id", prev_focused_id);
            if (rowHandle != GridControl.InvalidRowHandle)
            {
                ExpeditionsGridView.FocusedRowHandle = rowHandle;
                ExpeditionsGridView.TopRowIndex = rowHandle;
            }

            restore = false;
        }

        private void NewItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            NewItem();
        }

        private void SettingMaterialPricesGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                ExpeditionsPopupMenu.ShowPopup(p2);
            }
        }

        private void DeleteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteBtn.PerformClick();
        }

        private void RefrechItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            GetData();
        }

        private void ExecuteItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExecuteBtn.PerformClick();
        }

        private void PrintItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            PrintBtn.PerformClick();
        }

        private void EditItemBtn_ItemClick(object sender, ItemClickEventArgs e)
        {
             EditBtn.PerformClick();
        }

        private void SettingMaterialPricesDetGrid_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                ExpeditionsDetPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindDoc.Find(row_smp_det.WaybillListId, -1, row_smp_det.OnDate);
        }

        private void ExpeditionsSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var list = DB.SkladBase().v_Expedition.AsQueryable();
            e.QueryableSource = list;
        }

        private void ExpeditionsPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            DeleteItemBtn.Enabled = DeleteBtn.Enabled;
            ExecuteItemBtn.Enabled = ExecuteBtn.Enabled;
            EditItemBtn.Enabled =  EditBtn.Enabled ;
            CopyItemBtn.Enabled = CopyBtn.Enabled ;
            PrintItemBtn.Enabled = PrintBtn.Enabled;

        }
    }
}
