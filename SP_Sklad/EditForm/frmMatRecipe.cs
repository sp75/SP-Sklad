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
    public partial class frmMatRecipe : Form
    {
        int? _rec_id { get; set; }
        int? _r_type { get; set; }
        private MatRecipe _mr { get; set; }
        private MatRecDet mat_rec_det { get; set; }
        private BaseEntities _db { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private List<CatalogTreeList> tree { get; set; }

        public frmMatRecipe(int? RType = null ,int? RecId = null )
        {
            InitializeComponent();

            _rec_id = RecId;
            _r_type = RType;
            _db = DB.SkladBase();
            current_transaction = _db.Database.BeginTransaction();
            tree = new List<CatalogTreeList>();
        }

        private void frmMatRecipe_Load(object sender, EventArgs e)
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            tree.Add(new CatalogTreeList { Id = -2, ParentId = -2, Text = "Основна інформація", ImgIdx = 0, TabIdx = 0 });
            tree.Add(new CatalogTreeList { Id = 0, ParentId = -1, Text = "Позиції", ImgIdx = 1, TabIdx = 1 });

            bindingSource1.DataSource = tree;

            if (_rec_id == null)
            {
                _mr = _db.MatRecipe.Add(new MatRecipe
                {
                    Amount = 0,
                    MatId = _db.Materials.FirstOrDefault().MatId,
                    OnDate = DateTime.Now,
                    RType = 0
                });

                _db.SaveChanges();
            }
            else
            {
                _mr = _db.MatRecipe.Find(_rec_id);
            }

            if (_mr != null)
            {
                MatLookUpEdit.Properties.DataSource = DB.SkladBase().MaterialsList.ToList();
                MatRecLookUpEdit.Properties.DataSource = MatLookUpEdit.Properties.DataSource;

                MatRecipeBindingSource.DataSource = _mr;
                GetRecDetail();
            }
        }

        private void frmMatRecipe_FormClosed(object sender, FormClosedEventArgs e)
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

        private void GetRecDetail()
        {
            var list = _db.MatRecDet.Where(w => w.RecId == _mr.RecId).Select(s => new { s.DetId, s.Materials.Name, s.Materials.Measures.ShortName, s.Amount, s.Coefficient }).ToList();
            MatRecDetGridControl.DataSource = list;

            tree.RemoveAll(r => r.ParentId == 0);
            foreach (var item in list)
            {
                tree.Add(new CatalogTreeList { Id = item.DetId, ParentId = 0, Text = item.Name, ImgIdx = 2, TabIdx = 2, DataSetId = item.DetId });
            }

            DirTreeList.ExpandAll();
        }

    }
}
