using SP_Sklad.SkladData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad.UserControls
{
    public partial class WaybillTemplateUserControl : UserControl
    {
        public WaybillTemplateUserControl()
        {
            InitializeComponent();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void WaybillTemplateUserControl_Load(object sender, EventArgs e)
        {
            WaybillTemplateBS.DataSource = DB.SkladBase().WaybillTemplate.ToList();
        }
    }
}
