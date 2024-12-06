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

namespace SP_Sklad.ViewsForm
{
    public partial class frmKagents : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private int _KaKind { get; set; }
        private string _okpo { get; set; }
        public GetKagentList_Result focused_row
        {
            get
            {
                return (KontragentGroupGridView.GetFocusedRow() as GetKagentList_Result);
            }
        }

        public List<GetKagentList_Result> SelectedRows
        {
            get
            {
                var result = new List<GetKagentList_Result>();

                foreach (var item in KontragentGroupGridView.GetSelectedRows())
                {
                    var row = KontragentGroupGridView.GetRow(item) as GetKagentList_Result;

                    result.Add(row);
                }

                return result;
            }
        }

        public frmKagents(int KaKind, string okpo)
        {
            InitializeComponent();
            _db = DB.SkladBase();
            _KaKind = KaKind;
            _okpo = okpo;
        }

        private void frmKaGroup_Load(object sender, EventArgs e)
        {
            KontragentBS.DataSource = DBHelper.KagentsWorkerList.Where(w => (w.KaKind == _KaKind || _KaKind == -1) && (w.OKPO == _okpo || _okpo == "") ).ToList();
        }

        private void frmKaGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
 
        }

        private void KontragentGroupGridView_DoubleClick(object sender, EventArgs e)
        {
            OkButton.PerformClick();
        }
    }
}