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
using DevExpress.Data;

namespace SP_Sklad.ViewsForm
{
    public partial class frmWaybillDetView : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }

        public v_WaybillDet focused_row => WaybillDetGridView.GetFocusedRow() is NotLoadedObject ? null : WaybillDetGridView.GetFocusedRow() as v_WaybillDet;
        private List<int?> _doc_list { get; set; }
        public frmWaybillDetView(List<int?> doc_list = null)
        {
            InitializeComponent();
            _db = DB.SkladBase();
            _doc_list = doc_list;
        }

        private void frmKaGroup_Load(object sender, EventArgs e)
        {
 
        }

        private void KagentListSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            e.QueryableSource = _db.v_WaybillDet;

            if (_doc_list != null)
            {
                e.QueryableSource = _db.v_WaybillDet.Where(w => _doc_list.Contains(w.WType));
            }
            e.Tag = _db;
        }

        private void WaybillDetGridView_DoubleClick(object sender, EventArgs e)
        {
            OkButton.PerformClick();
        }
    }
}