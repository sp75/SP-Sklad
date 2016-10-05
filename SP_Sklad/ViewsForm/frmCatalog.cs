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
        private int? _id { get; set; }

        public frmCatalog(int? gtype, int? id=null)
        {
            _gtype = gtype;
            _id = id;
            InitializeComponent();
        }

        private void frmCatalog_Load(object sender, EventArgs e)
        {
            var q = (List<GetDirTree_Result>)uc.DirTreeBS.DataSource;
            uc.DirTreeBS.DataSource = (_id == null ? q.Where(w => w.GType == _gtype).ToList() : q.Where(w => w.Id == _id).ToList());
        }
    }
}
