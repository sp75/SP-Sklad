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
using SP_Sklad.Common;
using SP_Sklad.WBDetForm;
using SP_Sklad.WBForm;
using DevExpress.Data;
using DevExpress.XtraGrid;

namespace SP_Sklad.UserControls
{
    public partial class ucWayBillOutDet : DevExpress.XtraEditors.XtraUserControl
    {
        private int _wbill_id { get; set; }
        private v_WayBillOutDet wb_det_focused_row
        {
            get
            {
                return WaybillDetGridView.GetFocusedRow() as v_WayBillOutDet;
            }
        }

        public ucWayBillOutDet()
        {
            InitializeComponent();
        }

        public void GetDate(int wbill_id)
        {
            _wbill_id = wbill_id;

            var list = new BaseEntities().v_WayBillOutDet.Where(w => w.WbillId == wbill_id).OrderBy(o => o.Num).ToList();

            gridColumn37.Caption = "Сума в валюті, " + list.FirstOrDefault()?.CurrName;
            gridControl2.DataSource = list;
        }

        private void WaybillDetGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                WbDetPopupMenu.ShowPopup(p2);
            }
        }

        private void WayBillInDetUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                gridColumn50.Caption = "Сума в нац. валюті, " + DBHelper.NationalCurrency.ShortName;
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(wb_det_focused_row.MatId);
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowTurnMaterial(wb_det_focused_row.MatId);
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(wb_det_focused_row.MatId, DB.SkladBase());
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (!IHelper.FindMatInWH(wb_det_focused_row.MatId))
            {
                MessageBox.Show(string.Format("На даний час товар <{0}> на складі вдсутній!", wb_det_focused_row.MatName));
            }
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!IHelper.FindMatInDir(wb_det_focused_row.MatId))
            {
                MessageBox.Show(string.Format("Товар <{0}> в довіднику вдсутній, можливо він перебуває в архіві!", wb_det_focused_row.MatName));
            }
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetDate(_wbill_id);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmWayBillDetIn(DB.SkladBase(), wb_det_focused_row.PosId, DB.SkladBase().WaybillList.Find(wb_det_focused_row.WbillId)))
            {
                frm.OkButton.Visible = false;
                frm.ShowDialog();
            }
        }

        private void WaybillCorrectionDetBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var frm = new frmWaybillCorrection(wb_det_focused_row.PosId))
            {
                frm.ShowDialog();
            }
        }

        private void WbDetPopupMenu_BeforePopup(object sender, CancelEventArgs e)
        {
            var wb = new BaseEntities().WaybillList.Find(_wbill_id);

            WaybillCorrectionDetBtn.Enabled = (wb.WType == -1 && DBHelper.is_buh && wb.Checked == 1);
        }

        private void WaybillDetGridView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {

            if (e.SummaryProcess == CustomSummaryProcess.Finalize && gridControl2.DataSource != null)
            {
                var def_m = DBHelper.MeasuresList.FirstOrDefault(w => w.Def == 1);

                GridSummaryItem item = e.Item as GridSummaryItem;

                if (item.FieldName == "Amount")
                {
                    var mat_list = gridControl2.DataSource as List<v_WayBillOutDet>;
                    if (mat_list != null)
                    {
                        var amount_sum = mat_list.Where(w => w.MId == def_m.MId).Sum(s => s.Amount);
                        e.TotalValue = amount_sum.ToString() + " " + def_m.ShortName;//Math.Round(amount_sum + ext_sum, 2);
                    }
                }
            }
        }
    }
}
