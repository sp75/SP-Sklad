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

namespace SP_Sklad.UserControls.Warehouse
{
    public partial class ucWhPosRemains : DevExpress.XtraEditors.XtraUserControl
    {
        public v_WhPosRemains row_smp => WhPosRemainsGridView.GetFocusedRow() is NotLoadedObject ? null : WhPosRemainsGridView.GetFocusedRow() as v_WhPosRemains;

        private int prev_focused_id = 0;
        private int prev_top_row_index = 0;
        private int prev_rowHandle = 0;
        private bool _restore = false;
        public ucWhPosRemains()
        {
            InitializeComponent();
        }

        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {

            if (!DesignMode)
            {
                var user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, new BaseEntities());

                WhPosRemainsGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
            }
        }

        public void GetData(bool restore = true)
        {

            prev_rowHandle = WhPosRemainsGridView.FocusedRowHandle;

            if (row_smp != null )
            {
                prev_top_row_index = WhPosRemainsGridView.TopRowIndex;
                prev_focused_id = row_smp.Id;
            }

            _restore = restore;

            WhPosRemainsGridControl.DataSource = null;
            WhPosRemainsGridControl.DataSource = WhPosRemainsSource;
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetData();
        }


        private void barButtonItem11_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(WhPosRemainsGridControl);
        }

        private void ServicesGridView_DoubleClick(object sender, EventArgs e)
        {
         
        }

        private void SettingMaterialPricesSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            var list = DB.SkladBase().v_WhPosRemains.AsQueryable();
            e.QueryableSource = list;
        }

        private void WhPosRemainsGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (row_smp == null || !_restore)
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

            _restore = false;
        }

        void OnRowSearchComplete(object rh)
        {
            int rowHandle = (int)rh;
            if (WhPosRemainsGridView.IsValidRowHandle(rowHandle))
            {
                FocusRow(WhPosRemainsGridView, rowHandle);
            }
        }

        public void FocusRow(GridView view, int rowHandle)
        {
            view.TopRowIndex = prev_top_row_index == -1 ? rowHandle : prev_top_row_index;
            view.FocusedRowHandle = rowHandle;
            view.SelectRow(rowHandle);
        }

        private void WhPosRemainsGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                PosBottomPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            FindDoc.Find(row_smp.WaybillListId, row_smp.WType, row_smp.OnDate);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = DB.SkladBase())
            {
                db.DeleteWhere<PosRemains>(w => w.PosId == row_smp.PosId);

                var pos = db.WMatTurn.Where(w => w.PosId == row_smp.PosId).OrderBy(o => o.OnDate).Select(s => new { s.PosId, s.WId, s.OnDate }).ToList().Distinct();

                foreach (var item in pos)
                {
                    db.SP_RECALC_POSREMAINS(item.PosId, row_smp.MatId, item.WId, item.OnDate, 0);
                }

                db.SaveChanges();
            }

            GetData();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IHelper.FindMatInWH(row_smp.MatId))
            {
                MessageBox.Show(string.Format("На даний час товар <{0}> на складі вдсутній!", row_smp.MatName));
            }
        }
    }
}
