using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;

namespace SP_Sklad.EditForm
{
    public partial class frmWarehouseEdit : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
       private int? _wid { get; set; }
       private Warehouse wh { get; set; }

        public frmWarehouseEdit( int? wid =null)
        {
            InitializeComponent();
            _wid = wid;
            _db = DB.SkladBase();
        }

        private void frmWarehouseEdit_Load(object sender, EventArgs e)
        {
            if (_wid == null)
            {
                wh = _db.Warehouse.Add(new Warehouse
                {
                    Deleted = 0,
                    Def = _db.Warehouse.Any(a=> a.Def == 1) ? 0 :1,
                    Name = ""
                });

            }
            else
            {
                wh = _db.Warehouse.Find(_wid);
            }

            WarehouseDS.DataSource = wh;

            DefCheckBox.Enabled = !DefCheckBox.Checked;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
        }


    }
}
