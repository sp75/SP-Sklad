using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.SkladData;

namespace SP_Sklad.EditForm
{
    public partial class frmServicesEdit : DevExpress.XtraEditors.XtraForm
    {
        int? _svc_id { get; set; }
        int? _svc_grp { get; set; }
        private Services _svc { get; set; }
        BaseEntities _db { get; set; }

        public frmServicesEdit(int? SvcId = null, int? SvcGrp = null)
        {
            InitializeComponent();
            _svc_id = SvcId;
            _svc_grp = SvcGrp;
            _db = DB.SkladBase();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
        }

        private void frmServicesEdit_Load(object sender, EventArgs e)
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            var tree = new List<CatalogTreeList>();
            tree.Add(new CatalogTreeList { Id = 0, ParentId = 255, Text = "Основна інформація", ImgIdx = 0, TabIdx = 0 });
            tree.Add(new CatalogTreeList { Id = 1, ParentId = 255, Text = "Примітка", ImgIdx = 1, TabIdx = 1 });
            DirTreeList.DataSource = tree;


            if (_svc_id == null)
            {
                _svc = _db.Services.Add(new Services
                {
                    Name = "",
                    MId = DBHelper.MeasuresList.FirstOrDefault(w => w.Def == 1).MId,
                    GrpId = _svc_grp.Value,
                    Price = 0
                });
            }
            else
            {
                _svc = _db.Services.Find(_svc_id);
            }

            if (_svc != null)
            {
                GrpIdEdit.Properties.TreeList.DataSource = DB.SkladBase().SvcGroup.Select(s => new { s.GrpId, s.PId, s.Name }).ToList();
                MsrComboBox.Properties.DataSource = DBHelper.MeasuresList;

                ServicesBS.DataSource = _svc;
            }

        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as CatalogTreeList;

            xtraTabControl1.SelectedTabPageIndex = focused_tree_node.TabIdx;
        }

        private void frmServicesEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            _db.Dispose();
        }

        private void NameTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            Text = "Товар: " + NameTextEdit.Text;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ArtikulEdit.Text = NameTextEdit.Text;
            ArtikulEdit.Focus();
        }

        private void UpTextBtn_Click(object sender, EventArgs e)
        {
            NameTextEdit.Text = ArtikulEdit.Text;
            NameTextEdit.Focus();
        }

        private void checkEdit1_EditValueChanged(object sender, EventArgs e)
        {
            labelControl3.Visible = checkEdit1.Checked;
            NormEdit.Visible = checkEdit1.Checked;
        }

        private void MsListBtn_Click(object sender, EventArgs e)
        {
            MsrComboBox.EditValue = IHelper.ShowDirectList(MsrComboBox.EditValue, 12);
        }
    }
}
