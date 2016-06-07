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
    public partial class frmMaterialEdit : Form
    {
        int? _mat_id { get; set; }
        private Materials _mat { get; set; }
        public virtual ObservableListSource<Materials> Products { get { return _products; } } 

        public frmMaterialEdit(int? MatId =null)
        {
            InitializeComponent();
            _mat_id = MatId;
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
                _mat = new Materials()
                {

                };
            }
            else
            {
                _mat = DB.SkladBase().Materials.Find(_mat_id);
            }
           
            if (_mat != null)
            {
                GrpIdEdit.Properties.DataSource = DB.SkladBase().Materials.Where(w => w.MatId == _mat_id).ToList();
                GrpIdEdit.Properties.TreeList.DataSource = _mat;// DB.SkladBase().MatGroup.Select(s => new { s.GrpId, s.PId, s.Name }).ToList();
                MsrComboBox.Properties.DataSource = DBHelper.MeasuresList;
                WIdLookUpEdit.Properties.DataSource = DBHelper.WhList();

                NameTextEdit.DataBindings.Add(new Binding("EditValue", _mat, "Name", true, DataSourceUpdateMode.OnValidation));
                ArtikulEdit.DataBindings.Add(new Binding("EditValue", _mat, "Artikul", true, DataSourceUpdateMode.OnValidation));
                GrpIdEdit.DataBindings.Add(new Binding("EditValue", _mat, "GrpId", true, DataSourceUpdateMode.OnValidation));
                MsrComboBox.DataBindings.Add(new Binding("EditValue", _mat, "MId"));
                WIdLookUpEdit.DataBindings.Add(new Binding("EditValue", _mat, "WId", true, DataSourceUpdateMode.OnValidation));
                MinSizeEdit.DataBindings.Add(new Binding("EditValue", _mat, "MSize", true, DataSourceUpdateMode.OnValidation));
                SerialsCheckEdit.DataBindings.Add(new Binding("EditValue", _mat, "Serials", true, DataSourceUpdateMode.OnValidation));
                BarCodeEdit.DataBindings.Add(new Binding("EditValue", _mat, "BarCode", true, DataSourceUpdateMode.OnValidation));
            }

        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as CatalogTreeList;

            xtraTabControl1.SelectedTabPageIndex = focused_tree_node.TabIdx;
        }
    }


}
