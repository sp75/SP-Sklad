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
    public partial class frmKaGroup : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        public KontragentGroup focused_row
        {
            get
            {
                return (KontragentGroupGridView.GetFocusedRow() as KontragentGroup);
            }
        }

        public frmKaGroup()
        {
            InitializeComponent();
            _db = DB.SkladBase();
        }

        private void frmKaGroup_Load(object sender, EventArgs e)
        {
            KontragentGroupBS.DataSource = _db.KontragentGroup.ToList();
        }

        private void WhRemainGridView_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
            _db.KontragentGroup.Remove((e.Row as KontragentGroup));
        }

        private void frmKaGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
            _db.SaveChanges();
        }

        private void KontragentGroupBS_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = _db.KontragentGroup.Add(new KontragentGroup() { Id = Guid.NewGuid() });
        }
    }
}