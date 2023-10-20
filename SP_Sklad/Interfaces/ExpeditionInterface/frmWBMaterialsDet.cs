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

namespace SP_Sklad.ViewsForm
{
    public partial class frmWBMaterialsDet : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private ExpeditionDet _det { get; set; }


        public frmWBMaterialsDet(ExpeditionDet det)
        {
            InitializeComponent();
            _det = det;
            _db = DB.SkladBase();
        }


        private void frmWBMaterialsDet_Load(object sender, EventArgs e)
        {
            ExpeditionWBMaterialsDetBS.DataSource = _db.v_ExpeditionWBMaterialsDet.AsNoTracking().Where(w => w.Id == _det.Id && w.MId == _det.MId).ToList();
        }
    }
}