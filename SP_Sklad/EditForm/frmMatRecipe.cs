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

            TreeListBindingSource.DataSource = tree;

            if (_rec_id == null)
            {
                _mr = _db.MatRecipe.Add(new MatRecipe
                {
                    Amount = 0,
                    MatId = _db.Materials.FirstOrDefault().MatId,
                    OnDate = DateTime.Now,
                    RType = _r_type
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
            _db.SaveChanges();

            var focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as CatalogTreeList;

            if(focused_tree_node.ParentId ==0)
            {
                MatRecDetBS.DataSource = _db.MatRecDet.Find(focused_tree_node.DataSetId);
            }

            if (focused_tree_node.Id == 0)
            {
                var list = _db.MatRecDet.Where(w => w.RecId == _mr.RecId).Select(s => new
                {
                    s.DetId,
                    s.Materials.Name,
                    s.Materials.Measures.ShortName,
                    s.Amount,
                    s.Coefficient

                }).ToList();
                MatRecDetGridControl.DataSource = list;
            }

            xtraTabControl1.SelectedTabPageIndex = focused_tree_node.TabIdx;
        }

        private void GetRecDetail()
        {
            var list = _db.MatRecDet.Where(w => w.RecId == _mr.RecId).Select(s => new
            {
                s.DetId,
                s.Materials.Name,
                s.Materials.Measures.ShortName,
                s.Amount,
                s.Coefficient

            }).ToList();
            MatRecDetGridControl.DataSource = list;

            tree.RemoveAll(r => r.ParentId == 0);
            foreach (var item in list)
            {
                tree.Add(new CatalogTreeList
                {
                    Id = tree.Count + 1, //item.DetId,
                    ParentId = 0,
                    Text = item.Name,
                    ImgIdx = 2,
                    TabIdx = 2,
                    DataSetId = item.DetId

                });
            }
            DirTreeList.RefreshDataSource();
            DirTreeList.ExpandAll();
        }

        private void AddRecDetBtn_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 2;
            var new_det = _db.MatRecDet.Add(new MatRecDet { RecId = _mr.RecId, Amount = 0, Coefficient = 0, MatId = DB.SkladBase().MaterialsList.FirstOrDefault().MatId });
            MatRecDetBS.DataSource = new_det;
            _db.SaveChanges();
            GetRecDetail();

            DirTreeList.FocusedNode = DirTreeList.GetNodeList().FirstOrDefault(w => Convert.ToInt32(w.GetValue("DataSetId")) == new_det.DetId);
        }

        private void DelRecDetBtn_Click(object sender, EventArgs e)
        {
            dynamic det_item = MatRecDetGridView.GetFocusedRow();
            if (det_item == null) return;

           _db.MatRecDet.Remove(_db.MatRecDet.Find(det_item.DetId));
           _db.SaveChanges();

           GetRecDetail();
        }

        private void EditRecDetBtn_Click(object sender, EventArgs e)
        {
            dynamic det_item = MatRecDetGridView.GetFocusedRow();
            if (det_item == null) return;

            xtraTabControl1.SelectedTabPageIndex = 2;
            MatRecDetBS.DataSource = _db.MatRecDet.Find(det_item.DetId);

            DirTreeList.FocusedNode = DirTreeList.GetNodeList().FirstOrDefault(w => Convert.ToInt32(w.GetValue("DataSetId")) == det_item.DetId);
        }

        private void MatRecDetGridView_DoubleClick(object sender, EventArgs e)
        {
            EditRecDetBtn.PerformClick();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
          //  xtraTabControl1.SelectedTabPageIndex = 1;
            _db.MatRecDet.Remove(MatRecDetBS.DataSource as MatRecDet);
            _db.SaveChanges();

            GetRecDetail();
            simpleButton11.PerformClick();
        }

        private void MatLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (MatLookUpEdit.ContainsFocus)
            {
                var rd = MatRecDetBS.DataSource as MatRecDet;
                rd.MatId = Convert.ToInt32(MatLookUpEdit.EditValue);
                _db.SaveChanges();
                GetRecDetail();
            }
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {

            DirTreeList.FocusedNode = DirTreeList.GetNodeList().FirstOrDefault(w => Convert.ToInt32(w.GetValue("Id")) == 0);
        }

        private void frmMatRecipe_Shown(object sender, EventArgs e)
        {
            if (_mr.RType == 1)
            {
                Text = "Властивості рецепту: " + MatRecLookUpEdit.Text;
                labelControl11.Visible = false;
                calcEdit1.Visible = false;
                gridColumn1.Visible = false;
            }
            if (_mr.RType == 2)
            {
                Text = "Властивості обвалки: " + MatRecLookUpEdit.Text;
            }

            var isDoc = _db.WayBillMake.Any(a => a.RecId == _mr.RecId);
            MatRecLookUpEdit.Enabled = !isDoc;
            simpleButton2.Enabled = !isDoc;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {

            _db.SaveChanges();
            current_transaction.Commit();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MatRecLookUpEdit.EditValue = IHelper.ShowDirectList(MatRecLookUpEdit.EditValue, 5);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            MatLookUpEdit.EditValue = IHelper.ShowDirectList(MatLookUpEdit.EditValue, 5);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo((int)MatRecLookUpEdit.EditValue);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowTurnMaterial((int)MatRecLookUpEdit.EditValue);
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV((int)MatRecLookUpEdit.EditValue, _db);
        }

    }
}
