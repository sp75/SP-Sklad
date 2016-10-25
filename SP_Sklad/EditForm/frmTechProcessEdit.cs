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
    public partial class frmTechProcessEdit : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private int? _ProcId { get; set; }
        private TechProcess tp { get; set; }

        public frmTechProcessEdit(int? ProcId = null)
        {
            InitializeComponent();

            _ProcId = ProcId;
            _db = DB.SkladBase();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
        }

        private void frmTechProcessEdit_Load(object sender, EventArgs e)
        {
            if (_ProcId == null)
            {
                tp = _db.TechProcess.Add(new TechProcess());
            }
            else
            {
                tp = _db.TechProcess.Find(_ProcId);
            }

            TechProcessDS.DataSource = tp;
        }
    }
}
