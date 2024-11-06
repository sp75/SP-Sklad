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
using SP_Sklad.Reports;

namespace SP_Sklad.ViewsForm
{
    public partial class frmDocumentViews : DevExpress.XtraEditors.XtraForm
    {
        public v_KAgentDocs focused_row => DocumentGridView.GetFocusedRow() is NotLoadedObject ? null : DocumentGridView.GetFocusedRow() as v_KAgentDocs;
        private List<int?> _doc_list { get; set; }
        private int? _ka_id { get; set; }

        public frmDocumentViews(List<int?> doc_list, int? ka_id = null)
        {
            InitializeComponent();
            _doc_list = doc_list;
            _ka_id = ka_id;
        }

        private void frmKaGroup_Load(object sender, EventArgs e)
        {
 
        }

        private void frmKaGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
 
        }

        private void KontragentGroupGridView_DoubleClick(object sender, EventArgs e)
        {
            OkButton.PerformClick();
        }

        private void KagentListSource_GetQueryable(object sender, DevExpress.Data.Linq.GetQueryableEventArgs e)
        {
            BaseEntities objectContext = new BaseEntities();

            var list = objectContext.v_KAgentDocs.AsQueryable();
           
            if(_doc_list != null)
            {
                list = list.Where(w => _doc_list.Contains(w.WType));
            }

            if(_ka_id != null)
            {
                list = list.Where(w => w.KaId == _ka_id);
            }

            e.QueryableSource = list;

            e.Tag = objectContext;
        }

        private void DocumentGridControl_Click(object sender, EventArgs e)
        {

        }

        private void OkButton_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ExportToXlsx(DocumentGridControl);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_row != null)
            {
                PrintDoc.Show(focused_row.Id, focused_row.WType.Value, DB.SkladBase());
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DocumentGridControl.DataSource = null;
            DocumentGridControl.DataSource = KagentListSource;
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_row != null)
            {
                FindDoc.Find(focused_row.Id, focused_row.WType.Value, focused_row.OnDate);
            }
        }
    }
}