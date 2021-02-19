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
        public Kagent focused_row
        {
            get
            {
                return (KontragentGroupGridView.GetFocusedRow() as Kagent);
            }
        }

        public frmKagents()
        {
            InitializeComponent();
            _db = DB.SkladBase();
        }

        private void frmKaGroup_Load(object sender, EventArgs e)
        {
            KontragentBS.DataSource = _db.Kagent.ToList();
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