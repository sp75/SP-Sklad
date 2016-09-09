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
    public partial class frmCatalog : Form
    {
        private int? _gtype { get; set; }

        public frmCatalog(int gtype)
        {
            _gtype = gtype;
            InitializeComponent();
        }

        private void frmCatalog_Load(object sender, EventArgs e)
        {
            var q = (List<GetDirTree_Result>)uc.DirTreeBS.DataSource;
            uc.DirTreeBS.DataSource = q.Where(w => w.GType == _gtype).ToList();
        }
    }
}
