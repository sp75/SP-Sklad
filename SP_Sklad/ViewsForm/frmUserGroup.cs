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

namespace SP_Sklad.ViewsForm
{
    public partial class frmUserGroup : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        public UsersGroup focused_row
        {
            get
            {
                return (UsersGroupGridView.GetFocusedRow() as UsersGroup);
            }
        }

        public frmUserGroup()
        {
            InitializeComponent();
            _db = DB.SkladBase();
        }

        private void frmUserGroup_Load(object sender, EventArgs e)
        {
            UsersGroupBS.DataSource = _db.UsersGroup.ToList();
        }

        private void UsersGroupBS_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = _db.UsersGroup.Add(new UsersGroup() { Id = Guid.NewGuid() });
        }

        private void frmUserGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
            _db.SaveChanges();
        }

        private void UsersGroupGridView_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
            _db.UsersGroup.Remove((e.Row as UsersGroup));
        }
    }
}