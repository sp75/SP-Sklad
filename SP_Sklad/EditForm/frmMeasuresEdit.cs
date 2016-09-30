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
    public partial class frmMeasuresEdit : Form
    {
        private BaseEntities _db { get; set; }
        private int? _mid { get; set; }
        private Measures ms { get; set; }

        public frmMeasuresEdit(int? mid = null)
        {
            InitializeComponent();
            _mid = mid;
            _db = DB.SkladBase();
        }

        private void frmMeasuresEdit_Load(object sender, EventArgs e)
        {
            if (_mid == null)
            {
                ms = _db.Measures.Add(new Measures
                {
                    Deleted = 0,
                    Def = _db.Measures.Any(a => a.Def == 1) ? 0 : 1,
                    Name = "",
                    ShortName = ""
                });

            }
            else
            {
                ms = _db.Measures.Find(_mid);
            }

            MeasuresDS.DataSource = ms;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
        }
    }
}
