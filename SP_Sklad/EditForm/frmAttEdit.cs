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
    public partial class frmAttEdit : DevExpress.XtraEditors.XtraForm
    {
        private WaybillList _wb { get; set; }

        public frmAttEdit(WaybillList wb)
        {
            InitializeComponent();
            _wb = wb;

            WaybillListBS.DataSource = wb;
        }

        private void frmAttEdit_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _wb.AttDate = null;
            _wb.AttNum = null;
            _wb.Received = null;
        }
    }
}
