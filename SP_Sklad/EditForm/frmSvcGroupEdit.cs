using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.SkladData;

namespace SP_Sklad.EditForm
{
    public partial class frmSvcGroupEdit : Form
    {
        int? _grp_id { get; set; }
        int? _pid { get; set; }
        private SvcGroup _mg { get; set; }
        private BaseEntities _db { get; set; }
        private DbContextTransaction current_transaction { get; set; }

        public frmSvcGroupEdit(int? GrpId = null, int? PId = null)
        {
            InitializeComponent();

            _grp_id = GrpId;
            _pid = PId;
            _db = DB.SkladBase();
            current_transaction = _db.Database.BeginTransaction();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
            current_transaction.Commit();
        }

        private void frmSvcGroupEdit_Load(object sender, EventArgs e)
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            var tree = new List<CatalogTreeList>();
            tree.Add(new CatalogTreeList { Id = 0, ParentId = 255, Text = "Основна інформація", ImgIdx = 0, TabIdx = 0 });
            tree.Add(new CatalogTreeList { Id = 1, ParentId = 255, Text = "Примітка", ImgIdx = 1, TabIdx = 1 });
            DirTreeList.DataSource = tree;

            if (_grp_id == null)
            {
                _mg = _db.SvcGroup.Add(new SvcGroup
                {
                    Deleted = 0,
                    PId = 0,
                    Name = ""
                });
                _db.SaveChanges();

                _mg.PId = _pid ?? _mg.GrpId;
            }
            else
            {
                _mg = _db.SvcGroup.Find(_grp_id);
            }

            if (_mg != null)
            {
                checkEdit4.Checked = (_mg.GrpId == _mg.PId);

                GrpIdEdit.Properties.TreeList.DataSource = DB.SkladBase().SvcGroup.Select(s => new { s.GrpId, s.PId, s.Name }).ToList();

                SvcGroupDS.DataSource = _mg;
            }
        }

        private void frmSvcGroupEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as CatalogTreeList;

            xtraTabControl1.SelectedTabPageIndex = focused_tree_node.TabIdx;
        }

        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit4.Checked)
            {
                _mg.PId = _mg.GrpId;
            }
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit3.ContainsFocus && checkEdit3.Checked && GrpIdEdit.EditValue != DBNull.Value)
            {
                _mg.PId = (int)GrpIdEdit.EditValue;
            }
        }

        private void frmSvcGroupEdit_Shown(object sender, EventArgs e)
        {
            this.Text = "Група послуг: " + textEdit10.Text;
        }
    }
}
