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

namespace SP_Sklad.ViewsForm
{
    public partial class frmIntermediateWeighingList : DevExpress.XtraEditors.XtraForm
    {
        public GetWayBillMakeDet_Result focused_row
        {
            get
            {
                return (UsersGroupGridView.GetFocusedRow() as GetWayBillMakeDet_Result);
            }
        }

        public frmIntermediateWeighingList(List<GetWayBillMakeDet_Result> list)
        {
            InitializeComponent();
            GetWayBillMakeDetBS.DataSource = list;
        }

        private void frmUserGroup_Load(object sender, EventArgs e)
        {
           
        }

        private void frmUserGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }

    }
}