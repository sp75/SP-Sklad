﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.SkladData;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using System.Collections;
using SP_Sklad.Common;

namespace SP_Sklad.ViewsForm
{
    public partial class frmSummarySettingMaterialPrices : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private int? _ptype_id { get; set; }
        public GetSummaryMaterialPrices_Result focused_row =>  (SummarySettingMaterialPricesGrid.GetFocusedRow() as GetSummaryMaterialPrices_Result);

        public frmSummarySettingMaterialPrices(int? ptype_id = null)
        {
            InitializeComponent();
            _db = DB.SkladBase();
            _ptype_id = ptype_id;
        }

        private void frmMaterilPrices_Load(object sender, EventArgs e)
        {
            barEditItem1.EditValue = DateTime.Now;

            GetData(DateTime.Now);
        }

        private void GetData(DateTime on_date)
        {
            SummarySettingMaterialPricesGridControl.DataSource = _db.GetSummaryMaterialPrices(on_date).Where(w=> w.PTypeId == _ptype_id).ToList();
        }

        public void FindItem(int mat_id)
        {
            SummarySettingMaterialPricesGrid.ClearColumnsFilter();
            SummarySettingMaterialPricesGrid.ClearFindFilter();

            int rowHandle = SummarySettingMaterialPricesGrid.LocateByValue("MatId", mat_id, OnRowSearchComplete);
            if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
            {
                FocusRow(SummarySettingMaterialPricesGrid, rowHandle);
            }
        }

        void OnRowSearchComplete(object rh)
        {
            int rowHandle = (int)rh;
            if (SummarySettingMaterialPricesGrid.IsValidRowHandle(rowHandle))
            {
                FocusRow(SummarySettingMaterialPricesGrid, rowHandle);
            }
        }

        public void FocusRow(GridView view, int rowHandle)
        {
            view.TopRowIndex = rowHandle;
            view.FocusedRowHandle = rowHandle;
            view.SelectRow(rowHandle);
        }

        private void frmKagentMaterilPrices_Shown(object sender, EventArgs e)
        {
            var p = _db.PriceTypes.FirstOrDefault(w => w.PTypeId == _ptype_id);
            Text = $"Встановлені ціни на товари по ціновій категорії [{p.Name}]";
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (MemoryStream ms_xlsx = new MemoryStream())
            {
                SummarySettingMaterialPricesGrid.ExportToXlsx(ms_xlsx);
                new frmSpreadsheed(ms_xlsx.ToArray()).Show();
            }
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetData((DateTime)barEditItem1.EditValue);
        }

        private void BarCodeBtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var textEdit = sender as TextEdit;

            if (e.Button.Index == 0)
            {
                FindByBarCode(textEdit.Text);
            }
        }

        public void FindByBarCode(string br_code)
        {
            if (!String.IsNullOrEmpty(br_code))
            {
                var bc = DB.SkladBase().v_BarCodes.FirstOrDefault(w => w.BarCode == br_code);

                if (bc != null)
                {
                    FindItem(bc.MatId);
                }
            }
        }

        private void BarCodeBtnEdit_KeyDown(object sender, KeyEventArgs e)
        {
            var textEdit = sender as TextEdit;

            if (e.KeyCode == Keys.Enter && !String.IsNullOrEmpty(textEdit.Text))
            {
                FindByBarCode(textEdit.Text);
                textEdit.Text = "";
                e.Handled = true;
            }
        }

        private void repositoryItemDateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            DateEdit textEditor = (DateEdit)sender;

            GetData(textEditor.DateTime);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dataForReport = new Dictionary<string, IList>();
            var on_date = (DateTime)barEditItem1.EditValue;

            var pl = _db.SettingMaterialPrices.Where(w => w.PTypeId == _ptype_id && w.OnDate <= on_date).OrderByDescending(o => o.OnDate).Take(1).Select(s => new
            {
                PriceTypesName = s.PriceTypes.Name,
                s.PTypeId,
                OnDate = on_date,
                s.Num
            }).ToList();
            var pl_d = _db.GetSummaryMaterialPrices(on_date).Where(w => w.PTypeId == _ptype_id).ToList() ;

            var mat_grp = pl_d.GroupBy(g => new { g.GrpNum, g.GrpName }).Select(s => new
            {
                s.Key.GrpName,
                s.Key.GrpNum
            }).OrderBy(o => o.GrpNum).ToList();

            List<object> realation = new List<object>();
            realation.Add(new
            {
                pk = "GrpName",
                fk = "GrpName",
                master_table = "MatGroup",
                child_table = "PriceListDet"
            });

            dataForReport.Add("PriceList", pl);
            dataForReport.Add("PriceListDet", pl_d);
            dataForReport.Add("MatGroup", mat_grp);
            dataForReport.Add("_realation_", realation);

            IHelper.Print(dataForReport, "SummarySettingMaterialPrices.xlsx");
        }

        private void SummarySettingMaterialPricesGrid_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                SettingMaterialPricesDetPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_row != null)
            {
                new frmMaterialPriceHIstory(focused_row.MatId).ShowDialog();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(focused_row.MatId);
        }
    }
}