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

namespace SP_Sklad
{
    public partial class frmReg : DevExpress.XtraEditors.XtraForm
    {
        string _kay_id { get; set; }
        public frmReg(string kay_id)
        {
            InitializeComponent();

            KayIDEdit.EditValue = kay_id;
            _kay_id = kay_id;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            using (var db = new BaseEntities())
            {
                var lic = db.Licenses.FirstOrDefault(w => w.MacAddress == _kay_id);
                lic.LicencesKay = licEdit.Text;

                db.SaveChanges();
            }
        }
    }
}