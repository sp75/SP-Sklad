using System;
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
using SP_Sklad.Common;

namespace SP_Sklad.ViewsForm
{
    public partial class frmWaybillCorrectionsView : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }

        public v_WaybillCorrectionDet focused_row => WaybillCorrectionDetGridView.GetFocusedRow() is NotLoadedObject ? null : WaybillCorrectionDetGridView.GetFocusedRow() as v_WaybillCorrectionDet;

        public frmWaybillCorrectionsView()
        {
            InitializeComponent();
           
        }

        private void frmKaGroup_Load(object sender, EventArgs e)
        {

        }

  
        private void KagentListSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            _db = DB.SkladBase();

            e.QueryableSource = _db.v_WaybillCorrectionDet;

            e.Tag = _db;
        }

        private void PrevievBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(WaybillCorrectionDetGridControl);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaybillCorrectionDetGridControl.DataSource = null;
            WaybillCorrectionDetGridControl.DataSource = WaybillCorrectionSource;
        }
    }
}