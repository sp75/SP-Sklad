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
    public partial class frmWhCatalog : Form
    {
        private int? _gtype { get; set; }
        public frmWhCatalog(int gtype)
        {
            _gtype = gtype;
            InitializeComponent();
        }

        private void frmWhCatalog_Load(object sender, EventArgs e)
        {
            uc.WHTreeList.DataSource = ((List<GetWhTree_Result>)uc.WHTreeList.DataSource).Where(w => w.GType == _gtype).ToList();
            uc.WHTreeList.ExpandToLevel(0);

            if (_gtype == 1)
            {
                if (uc.WHTreeList.FocusedNode != null)
                {
                    Text = "Залишки на складі: " + uc.WHTreeList.FocusedNode.GetDisplayText("Name");
                }
            }
        }
    }
}
