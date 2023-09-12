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
    public partial class frmTaraEdit : DevExpress.XtraEditors.XtraForm
    {
        int? _tara_id { get; set; }
        int? _tara_grp { get; set; }
        private Tara _tara { get; set; }
        BaseEntities _db { get; set; }

        public frmTaraEdit(int? TaraId = null, int? TaraGrp = null)
        {
            InitializeComponent();
            _tara_id = TaraId;
            _tara_grp = TaraGrp;
            _db = DB.SkladBase();

            MatTypeEdit.Properties.DataSource = new List<object>()
            {
                new { Id = 1, Name = "Рама" },
                new { Id = 2, Name = "Вішало" },
                new { Id = 5, Name = "Візок" },
                new { Id = 6, Name = "Шприць" },
                new { Id = 7, Name = "Гачок" },
                new { Id = 8, Name = "Ящик" }
            };
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


            if (_tara_id == null)
            {
                _tara = _db.Tara.Add(new Tara
                {
                    Name = "",
                    GrpId = _tara_grp.Value,
                    Deleted = 0
                });
            }
            else
            {
                _tara = _db.Tara.Find(_tara_id);
            }

            if (_tara != null)
            {
                GrpIdEdit.Properties.TreeList.DataSource = DB.SkladBase().TaraGroup.Select(s => new { s.GrpId, s.PId, s.Name }).ToList();
                WIdLookUpEdit.Properties.DataSource = DBHelper.WhList;

                TaraBS.DataSource = _tara;
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
            Text = "Тара: " + NameTextEdit.Text;
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

        private void WeightEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var frm = new frmWeightEdit(_tara.Name);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    WeightEdit.EditValue = frm.AmountEdit.Value;
                }
            }
        }
    }
}
