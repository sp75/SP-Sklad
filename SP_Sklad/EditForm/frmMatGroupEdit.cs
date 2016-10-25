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
    public partial class frmMatGroupEdit : DevExpress.XtraEditors.XtraForm
    {
        int? _grp_id { get; set; }
        int? _pid { get; set; }
        private MatGroup _mg { get; set; }
        private BaseEntities _db { get; set; }
        private DbContextTransaction current_transaction { get; set; }

        public frmMatGroupEdit(int? GrpId = null, int? PId = null)
        {
            InitializeComponent();

            _grp_id = GrpId;
            _pid = PId;
            _db = DB.SkladBase();
            current_transaction = _db.Database.BeginTransaction();
        }

        private void frmMatGroupEdit_Load(object sender, EventArgs e)
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            var tree = new List<CatalogTreeList>();
            tree.Add(new CatalogTreeList { Id = 0, ParentId = 255, Text = "Основна інформація", ImgIdx = 0, TabIdx = 0 });
            tree.Add(new CatalogTreeList { Id = 1, ParentId = 255, Text = "Ціноутворення ", ImgIdx = 1, TabIdx = 1 });
            tree.Add(new CatalogTreeList { Id = 2, ParentId = 255, Text = "Оподаткування", ImgIdx = 2, TabIdx = 2 });
            tree.Add(new CatalogTreeList { Id = 3, ParentId = 255, Text = "Примітка", ImgIdx = 3, TabIdx = 3 });
            DirTreeList.DataSource = tree;

            if (_grp_id == null)
            {
                _mg = _db.MatGroup.Add(new MatGroup
                {
                    Deleted = 0,
                    Nds = 0,
                    PId = 0,
                    Name = ""
                });
                _db.SaveChanges();

                _mg.PId = _pid ?? _mg.GrpId;
            }
            else
            {
                _mg = _db.MatGroup.Find(_grp_id);
             }

            if (_mg != null)
            {
                checkEdit4.Checked = (_mg.GrpId == _mg.PId);

                GrpIdEdit.Properties.TreeList.DataSource = DB.SkladBase().MatGroup.Select(s => new { s.GrpId, s.PId, s.Name, ImageIndex = 8 }).ToList();

                MatGroupDS.DataSource = _mg;
            }

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
            current_transaction.Commit();
        }

        private void frmMatGroupEdit_FormClosed(object sender, FormClosedEventArgs e)
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

        private void frmMatGroupEdit_Shown(object sender, EventArgs e)
        {
            this.Text = "Група товарів: " + textEdit10.Text;
        }
    }
}
