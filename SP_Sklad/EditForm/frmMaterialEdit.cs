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

namespace SP_Sklad.EditForm
{
    public partial class frmMaterialEdit : Form
    {
        public frmMaterialEdit()
        {
            InitializeComponent();
        }

        private void frmMaterialEdit_Load(object sender, EventArgs e)
        {
            var tree = new List<CatalogTreeList>();
            tree.Add(new CatalogTreeList { Id = 0, ParentId = 255, Text = "Основна інформація", ImgIdx = 0, TabIdx = 0 });
            tree.Add(new CatalogTreeList { Id = 1, ParentId = 255, Text = "Ціноутворення", ImgIdx = 1, TabIdx = 1 });
            tree.Add(new CatalogTreeList { Id = 2, ParentId = 255, Text = "Оподаткування", ImgIdx = 2, TabIdx = 2 });
            tree.Add(new CatalogTreeList { Id = 3, ParentId = 255, Text = "Взаємозамінність", ImgIdx = 3, TabIdx = 3 });
            tree.Add(new CatalogTreeList { Id = 4, ParentId = 255, Text = "Посвідчення", ImgIdx = 4, TabIdx = 4 });
            tree.Add(new CatalogTreeList { Id = 5, ParentId = 255, Text = "Зображення", ImgIdx = 5, TabIdx = 5 });
            tree.Add(new CatalogTreeList { Id = 6, ParentId = 255, Text = "Примітка", ImgIdx = 6, TabIdx = 6 });
            DirTreeList.DataSource = tree;
        }
    }


}
