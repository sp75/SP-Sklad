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

namespace SP_Sklad.ViewsForm
{
    public partial class frmDocumentViews : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }

        public v_KAgentDocs focused_row => DocumentGridView.GetFocusedRow() is NotLoadedObject ? null : DocumentGridView.GetFocusedRow() as v_KAgentDocs;
        private List<int?> _doc_list { get; set; }

        public frmDocumentViews(List<int?> doc_list)
        {
            InitializeComponent();
            _db = DB.SkladBase();
            _doc_list = doc_list;
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
            e.QueryableSource = _db.v_KAgentDocs;
            if(_doc_list != null)
            {
                e.QueryableSource = _db.v_KAgentDocs.Where(w => _doc_list.Contains(w.WType));
            }

            e.Tag = _db;
        }

        private void DocumentGridControl_Click(object sender, EventArgs e)
        {

        }
    }
}