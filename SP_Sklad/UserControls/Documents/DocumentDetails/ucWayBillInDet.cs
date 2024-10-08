﻿using System;
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
using SP_Sklad.EditForm;

namespace SP_Sklad.UserControls
{
    public partial class ucWayBillInDet : DevExpress.XtraEditors.XtraUserControl
    {
        private int _wbill_id { get; set; }
        private v_WayBillInDet wb_det_focused_row
        {
            get
            {
                return WaybillDetGridView.GetFocusedRow() as v_WayBillInDet;
            }
        }

        public ucWayBillInDet()
        {
            InitializeComponent();
        }

        public void GetDate(int wbill_id)
        {
            _wbill_id = wbill_id;

            var list = new BaseEntities().v_WayBillInDet.AsNoTracking().Where(w => w.WbillId == wbill_id).OrderBy(o => o.Num).ToList();

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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmPosMatTurn(wb_det_focused_row.MatId, wb_det_focused_row.Wid).ShowDialog();
        }


        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                IHelper.ShowRemainsInWh(wb_det_focused_row.Wid.Value, wb_det_focused_row.WhName);
            }
        }
    }
}
