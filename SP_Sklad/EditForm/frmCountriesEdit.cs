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
    public partial class frmCountriesEdit : Form
    {
        private BaseEntities _db { get; set; }
        private int? _CId { get; set; }
        private Countries c { get; set; }

        public frmCountriesEdit(int? CId = null)
        {
            InitializeComponent();
            _CId = CId;
            _db = DB.SkladBase();
        }

        private void frmCountriesEdit_Load(object sender, EventArgs e)
        {
            if (_CId == null)
            {
                c = _db.Countries.Add(new Countries
                {
                    Deleted = 0,
                    Def = _db.Countries.Any(a => a.Def == 1) ? 0 : 1,
                    Name = ""
                });

                Text = "Додати нову країну:";
            }
            else
            {
                c = _db.Countries.Find(_CId);

                Text = "Властвості країни:";
            }

            CountriesBS.DataSource = c;

            DefCheckBox.Enabled = !DefCheckBox.Checked;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
        }
    }
}
