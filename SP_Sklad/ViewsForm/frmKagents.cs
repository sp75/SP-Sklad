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
        public v_Kagent focused_row
        {
            get
            {
                return (KontragentGroupGridView.GetFocusedRow() as v_Kagent);
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
            KontragentBS.DataSource = _db.v_Kagent.Where(w => (w.KaKind == _KaKind || _KaKind == -1) && (w.OKPO == _okpo || _okpo == "")).ToList();
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