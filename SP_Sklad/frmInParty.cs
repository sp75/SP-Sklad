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

namespace SP_Sklad
{
    public partial class frmInParty : Form
    {
        public frmInParty(List<GetPosIn_Result> pos_in)
        {
            InitializeComponent();

            InPartyGridControl.DataSource = pos_in;
        }

        private void frmInParty_Load(object sender, EventArgs e)
        {
            InPartyGridView.ExpandAllGroups();
        }

        private void WaybillDetOutGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var dr = InPartyGridView.GetRow(e.RowHandle) as GetPosIn_Result;
            if (dr == null)
            {
                return;
            }

            if (e.Column.FieldName == "GetAll")
            {
                if (dr.GetAll == 1) dr.Amount = dr.FullRemain;
                else dr.Amount = 0;
            }

            if (e.Column.FieldName == "Amount")
            {
                dr.GetAll = 0;
            }

            InPartyGridView.RefreshRow(e.RowHandle);
        }
    }
}
