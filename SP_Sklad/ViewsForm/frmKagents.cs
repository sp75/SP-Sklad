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
        public KagentList focused_row
        {
            get
            {
                return (KontragentGroupGridView.GetFocusedRow() as KagentList);
            }
        }

        public frmKagents(int KaKind)
        {
            InitializeComponent();
            _db = DB.SkladBase();
            _KaKind = KaKind;
        }

        private void frmKaGroup_Load(object sender, EventArgs e)
        {
            KontragentBS.DataSource = DBHelper.Kagents.Where(w=> w.KaKind == _KaKind || _KaKind == -1);
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