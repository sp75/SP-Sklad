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
    public partial class frmWaybillTemplateView : DevExpress.XtraEditors.XtraForm
    {
        private int? _id { get; set; }

        public frmWaybillTemplateView(int? id=null)
        {
            _id = id;
            InitializeComponent();

            waybillTemplateUserControl1.isDirectList = true;
        }

        private void frmWaybillTemplateView_Load(object sender, EventArgs e)
        {
            waybillTemplateUserControl1.GetDataList();
        }
    }
}
