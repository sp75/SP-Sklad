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

namespace SP_Sklad.UserControls
{
    public partial class SettingMaterialPricesUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        BaseEntities _db { get; set; }
        public BarButtonItem EditBtn { get; set; }
        public BarButtonItem DeleteBtn { get; set; }
        public BarButtonItem ExecuteBtn { get; set; }
        public BarButtonItem CopyBtn { get; set; }
        public BarButtonItem PrintBtn { get; set; }

        public v_SettingMaterialPrices row_smp => SettingMaterialPricesGridView.GetFocusedRow() is NotLoadedObject ? null : SettingMaterialPricesGridView.GetFocusedRow() as v_SettingMaterialPrices;
        public v_SettingMaterialPricesDet row_smp_det => SettingMaterialPricesDetGrid.GetFocusedRow() as v_SettingMaterialPricesDet;

        private UserAccess user_access { get; set; }

        int row = 0;
        bool restore = false;
        public SettingMaterialPricesUserControl()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            row = SettingMaterialPricesGridView.FocusedRowHandle;
            restore = true;

            SettingMaterialPricesGridControl.DataSource = null;
            SettingMaterialPricesGridControl.DataSource = SettingMaterialPricesSource;

            GetDetailData();
        }

        public void NewItem()
        {
            using (var frm = new frmSettingMaterialPrices())
            {
                frm.ShowDialog();
            }
        }

        public void ExecuteItem()
        {
            if (row_smp.Id == null)
            {
                return;
            }

            var smp = _db.SettingMaterialPrices.Find(row_smp.Id);

            if (smp == null)
            {
                MessageBox.Show(Resources.not_find_wb);
                return;
            }
            if (smp.SessionId != null)
            {
                MessageBox.Show(Resources.deadlock);
                return;
            }


            if (smp.OnDate.Date < DBHelper.ServerDateTime().Date)
            {
                XtraMessageBox.Show("Проводити та сторнувати цей документ заборонено", "Провести документ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (smp.Checked == 1)
            {
                smp.Checked = 0;
            }
            else
            {
                smp.Checked = 1;
            }

            _db.SaveChanges();
        }

        private void SettingMaterialPricesGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DeleteBtn.Enabled = (row_smp != null && row_smp.Checked == 0 && user_access.CanDelete == 1);
            ExecuteBtn.Enabled = (row_smp != null && user_access.CanPost == 1);
            EditBtn.Enabled = (row_smp != null && row_smp.Checked == 0 && user_access.CanModify == 1);
            CopyBtn.Enabled = (row_smp != null && user_access.CanModify == 1);
            PrintBtn.Enabled = (row_smp != null);

            GetDetailData();
        }


        private void GetDetailData()
        {
            if (row_smp != null)
            {
                SettingMaterialPricesDetBS.DataSource = DB.SkladBase().v_SettingMaterialPricesDet.Where(w => w.SettingMaterialPricesId == row_smp.Id).ToList();
            }
            else
            {
                SettingMaterialPricesDetBS.DataSource = null;
            }
        }

        private void SettingMaterialPricesUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                _db = new BaseEntities();
                user_access = _db.UserAccess.FirstOrDefault(w => w.FunId == 97 && w.UserId == UserSession.UserId);
            }
        }

        private void SettingMaterialPricesGridView_DoubleClick(object sender, EventArgs e)
        {
            EditBtn.PerformClick();
        }

        private void SettingMaterialPricesSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var list = DB.SkladBase().v_SettingMaterialPrices.AsQueryable();
            e.QueryableSource = list;
        }

        private void SettingMaterialPricesGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (!restore)
            {
                return;
            }

            SettingMaterialPricesGridView.TopRowIndex = row;
            SettingMaterialPricesGridView.FocusedRowHandle = row;
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
                SettingMaterialPricesPopupMenu.ShowPopup(p2);
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
                SettingMaterialPricesDetPopupMenu.ShowPopup(p2);
            }
        }

        private void HistoryBtnItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (row_smp_det != null)
            {
                new frmMaterialPriceHIstory(row_smp_det.MatId).ShowDialog();
            }
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.ShowTurnMaterial(row_smp_det.MatId);
        }

        private void MatIfoBtnItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(row_smp_det.MatId);
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(row_smp_det.MatId, DB.SkladBase());
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            new frmKagentMaterilPrices().ShowDialog();
        }
    }
}
