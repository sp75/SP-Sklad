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
using DevExpress.XtraGrid.Views.Grid;
using System.IO;

namespace SP_Sklad.ViewsForm
{
    public partial class frmMaterialPriceHIstory : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private int _mat_id { get; set; }
        private Materials mat { get; set; }

        public frmMaterialPriceHIstory(int mat_id)
        {
            _mat_id = mat_id;

            InitializeComponent();
            _db = DB.SkladBase();
            mat = _db.Materials.Find(_mat_id);
        }

        private void frmMaterialPriceHIstory_Load(object sender, EventArgs e)
        {
            Text += " " + mat.Name;
            chartControl1.Titles[0].Text = Text;
       //     chartControl1.BackImage.Image = Image.FromStream(new MemoryStream(mat.BMP));

            SettingMaterialPricesDetBS.DataSource = _db.v_SettingMaterialPricesDet.Where(w => w.MatId == _mat_id).OrderBy(o => o.CreatedAt).ToList();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            chartControl1.ShowRibbonPrintPreview();// Print();
        }
    }
}