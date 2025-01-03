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

namespace SP_Sklad.ViewsForm
{
    public partial class frmCurrentMaterilPrices : DevExpress.XtraEditors.XtraForm
    {
     //   private BaseEntities _db { get; set; }
        private int? _mat_id { get; set; }
        private int? _PTypeId { get; set; }
        private int? find_id { get; set; }
        public KontragentGroup focused_row
        {
            get
            {
                return (MaterilPricesGridView.GetFocusedRow() as KontragentGroup);
            }
        }

        public frmCurrentMaterilPrices(int? mat_id = null, int? PTypeId = null)
        {
            InitializeComponent();
       //     _db = DB.SkladBase();
            _mat_id = mat_id;
            _PTypeId = PTypeId;
        }

        private void KagentMaterilPricesSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            BaseEntities objectContext = new BaseEntities();

            var qery = objectContext.v_MaterilPrices.AsQueryable();

            if (_mat_id.HasValue)
            {
                qery = qery.Where(w => w.MatId == _mat_id);
            }

            if (_PTypeId.HasValue)
            {
                qery = qery.Where(w => w.PTypeId == _PTypeId);
            }

            e.QueryableSource = qery.AsQueryable();

            e.Tag = objectContext;
        }

        private void KagentMaterilPricesGridView_AsyncCompleted(object sender, EventArgs e)
        {
            if (!find_id.HasValue)
            {
                return;
            }

            int rowHandle = MaterilPricesGridView.LocateByValue("MatId", find_id, OnRowSearchComplete);
            if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
            {
                FocusRow(MaterilPricesGridView, rowHandle);
            }

            find_id = null;
        }

        void OnRowSearchComplete(object rh)
        {
            int rowHandle = (int)rh;
            if (MaterilPricesGridView.IsValidRowHandle(rowHandle))
            {
                FocusRow(MaterilPricesGridView, rowHandle);
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
            if (_mat_id.HasValue)
            {
                colMatName.GroupIndex = 0;
            }
            if (_PTypeId.HasValue)
            {
                colPriceTypeName.GroupIndex = 0;
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (MemoryStream ms_xlsx = new MemoryStream())
            {
                MaterilPricesGridView.ExportToXlsx(ms_xlsx);
                new frmSpreadsheed(ms_xlsx.ToArray()).Show();
            }
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MaterilPricesGrid.DataSource = null;
            MaterilPricesGrid.DataSource = KagentMaterilPricesSource;
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

        public void FindItem(int mat_id)
        {
            find_id = mat_id;
            MaterilPricesGridView.ClearColumnsFilter();
            MaterilPricesGridView.ClearFindFilter();

            RefrechItemBtn.PerformClick();
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
    }
}