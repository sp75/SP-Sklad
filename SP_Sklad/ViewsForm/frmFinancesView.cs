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

namespace SP_Sklad.ViewsForm
{
    public partial class frmFinancesView : DevExpress.XtraEditors.XtraForm
    {
        private int? _gtype { get; set; }
        private int? _id { get; set; }

        public int? Id { get; set; }
        public int? PayTypeId { get; set; }


        public frmFinancesView(int? gtype, int? id = null)
        {
            _gtype = gtype;
            _id = id;

            InitializeComponent();
        }

        private void frmFinancesView_Load(object sender, EventArgs e)
        {
            var q = (List<GetFinancesTree_Result>)fin_uc.FinancesTreeList.DataSource;
            fin_uc.FinancesTreeList.DataSource = q.Where(w => w.PId == 117 || w.PId == 63 || w.PId == 61).ToList();  //(_id == null ? q.Where(w => w.GType == _gtype).ToList() : q.Where(w => w.Id == _id).ToList());
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var node = fin_uc.FinTreeList.GetDataRecordByNode(fin_uc.FinTreeList.FocusedNode) as GetSaldoDetTree_Result;

            if (node.PId == 63 || node.PId == 61)
            {
                Id = node.OriginalId;
                PayTypeId = node.PId == 61 ? 1 : 2;
            }

        }
    }
}
