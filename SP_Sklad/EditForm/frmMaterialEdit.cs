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
    public partial class frmMaterialEdit : DevExpress.XtraEditors.XtraForm
    {
        int? _mat_id { get; set; }
        int? _mat_grp { get; set; }
        private Materials _mat { get; set; }
        private BaseEntities _db { get; set; }
        private DbContextTransaction current_transaction { get; set; }

        public frmMaterialEdit(int? MatId =null, int? MatGrp = null)
        {
            InitializeComponent();
            _mat_id = MatId;
            _mat_grp = MatGrp;
            _db = DB.SkladBase();
            current_transaction = _db.Database.BeginTransaction();
        }

        private void frmMaterialEdit_Load(object sender, EventArgs e)
        {
            xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            var tree = new List<CatalogTreeList>();
            tree.Add(new CatalogTreeList { Id = 0, ParentId = 255, Text = "Основна інформація", ImgIdx = 0, TabIdx = 0 });
            tree.Add(new CatalogTreeList { Id = 1, ParentId = 255, Text = "Ціноутворення", ImgIdx = 1, TabIdx = 1 });
            tree.Add(new CatalogTreeList { Id = 2, ParentId = 255, Text = "Оподаткування", ImgIdx = 2, TabIdx = 2 });
            tree.Add(new CatalogTreeList { Id = 3, ParentId = 255, Text = "Взаємозамінність", ImgIdx = 3, TabIdx = 3 });
            tree.Add(new CatalogTreeList { Id = 4, ParentId = 255, Text = "Посвідчення", ImgIdx = 4, TabIdx = 4 });
            tree.Add(new CatalogTreeList { Id = 5, ParentId = 255, Text = "Зображення", ImgIdx = 5, TabIdx = 5 });
            tree.Add(new CatalogTreeList { Id = 6, ParentId = 255, Text = "Примітка", ImgIdx = 6, TabIdx = 6 });
            DirTreeList.DataSource = tree;


            if (_mat_id == null)
            {
                _mat = _db.Materials.Add(new Materials()
                {
                    Archived = 0,
                    Serials = 0,
                    MId = DBHelper.MeasuresList.FirstOrDefault(w => w.Def == 1).MId,
                    WId = DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId,
                    CId = DBHelper.CountersList.FirstOrDefault(w => w.Def == 1).CId,
                    NDS = 0,
                    GrpId = _mat_grp
                });
                _db.SaveChanges();
                _mat_id = _mat.MatId;
            }
            else
            {
                _mat = _db.Materials.Find(_mat_id);
                _mat.DateModified = DateTime.Now;
            }
           
            if (_mat != null)
            {
                GrpIdEdit.Properties.TreeList.DataSource =  DB.SkladBase().MatGroup.Select(s => new { s.GrpId, s.PId, s.Name }).ToList();
                MsrComboBox.Properties.DataSource = DBHelper.MeasuresList;
                WIdLookUpEdit.Properties.DataSource = DBHelper.WhList();
                ProducerLookUpEdit.Properties.DataSource = DB.SkladBase().Materials.Select(s => new { s.Producer }).Distinct().ToList();
                CIdLookUpEdit.Properties.DataSource = DBHelper.CountersList;

                MaterialsBS.DataSource = _mat;

                MatPriceGridControl.DataSource = _db.GetMatPriceTypes(_mat_id).ToList().Select(s => new
                {
                    s.PTypeId,
                    s.Name,
                    s.TypeName,
                    s.IsIndividually,
                    Summary = (s.PPTypeId == null || s.ExtraType == 2 || s.ExtraType == 3)
                                                ? ((s.PPTypeId == null) ? s.OnValue.Value.ToString("0.00") + "% на ціну прихода" : (s.ExtraType == 2) ? s.OnValue.Value.ToString("0.00") + "% на категорію " + s.PtName : (s.ExtraType == 3) ? s.OnValue.Value.ToString("0.00") + "% на прайс-лист " + s.PtName : "")
                                                : s.OnValue.Value.ToString("0.00") + "% від " + s.PtName
                });
             }

        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as CatalogTreeList;

            xtraTabControl1.SelectedTabPageIndex = focused_tree_node.TabIdx;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
            current_transaction.Commit();
        }

        private void frmMaterialEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void frmMaterialEdit_Shown(object sender, EventArgs e)
        {
            
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

        private void WhBtn_Click(object sender, EventArgs e)
        {
            NameTextEdit.Text = ArtikulEdit.Text;
            NameTextEdit.Focus();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(_mat_id.Value, _db);
        }

        private void NowDateBtn_Click(object sender, EventArgs e)
        {

            MsrComboBox.EditValue = IHelper.ShowDirectList(MsrComboBox.EditValue, 12);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            BarCodeEdit.EditValue = NameTextEdit.Text.ToArray().Sum(s=> Convert.ToInt32(s));
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            CIdLookUpEdit.EditValue = IHelper.ShowDirectList(CIdLookUpEdit.EditValue, 7);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowTurnMaterial(_mat_id.Value);
        }
    }


}
