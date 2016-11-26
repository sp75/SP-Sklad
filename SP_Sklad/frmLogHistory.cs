using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using SP_Sklad.Common;
using SP_Sklad.SkladData;

namespace SP_Sklad
{
    public partial class frmLogHistory : DevExpress.XtraEditors.XtraForm
    {
        private int? _tab_id { get; set; }
        private int? _id { get; set; }

        public frmLogHistory(int? tab_id , int? id)
        {
            InitializeComponent();
            _tab_id = tab_id;
            _id = id;
        }

        private void frmLogHistory_Load(object sender, EventArgs e)
        {
            var list = DB.SkladBase().OperLog.Where(w => w.Id == _id && w.TabId == _tab_id).ToList().Select(s => new
            {
                s.OpId,
                s.OpCode,
                Name = s.Users != null ? s.Users.Name : "",
                s.OnDate,
                DataBefore = IHelper.ConvertLogData(s.DataBefore),
                DataAfter = IHelper.ConvertLogData(s.DataAfter)
            }).OrderBy(o => o.OnDate).ToList();

            OprLogGridControl.DataSource = list;
        }

        private void OprLogGridView_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e)
        {
            if (!isRowMouseDoubleClicked) return;
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.RowHeight = 100;
            } 

        }

        bool isRowMouseDoubleClicked = false;
        private void OprLogGridView_DoubleClick(object sender, EventArgs e)
        {
            isRowMouseDoubleClicked = true;
            GridView view = sender as GridView;
            view.LayoutChanged();           
        }
    }
}
