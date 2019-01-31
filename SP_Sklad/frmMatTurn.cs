using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;

namespace SP_Sklad
{
    public partial class frmMatTurn : DevExpress.XtraEditors.XtraForm
    {
        public int _mat_id { get; set; }
        private bool is_show { get; set; }

        public frmMatTurn(int mat_id)
        {
            InitializeComponent();
            is_show = false;
            _mat_id = mat_id;
        }

        private void frmMatTurn_Load(object sender, EventArgs e)
        {
            wTypeList.Properties.DataSource = new List<object>() { new { Id = 0, Name = "Усі" } }.Concat(new BaseEntities().DocType.Select(s => new { s.Id, s.Name })).ToList();
            wTypeList.EditValue = 0;
            KAgentEdit.Properties.DataSource = DBHelper.KagentsList;// new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().Kagent.Select(s => new { s.KaId, s.Name }));
            KAgentEdit.EditValue = 0;

            wbStartDate.DateTime = DateTimeDayOfMonthExtensions.FirstDayOfMonth(DateTime.Now);
            wbEndDate.DateTime = DateTime.Now.AddDays(1);

            var mat =new BaseEntities().Materials.Find(_mat_id);
            if (mat != null)
            {
                this.Text = "Рух товару: " + mat.Name;
            }
            
            GetTurns();
        }

        private void GetTurns()
        {
            var start_date = wbStartDate.DateTime < SqlDateTime.MinValue.Value ? SqlDateTime.MinValue.Value : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < SqlDateTime.MinValue.Value ? SqlDateTime.MaxValue.Value : wbEndDate.DateTime;

            DocListBindingSource.DataSource = DB.SkladBase().GetMatMove(_mat_id, start_date, end_date, 0, (int)KAgentEdit.EditValue, (int)wTypeList.EditValue, "*", Guid.Empty, DBHelper.CurrentUser.UserId).ToList();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file_format = DBHelper.CurrentUser.ReportFormat;

            if (file_format == "pdf")
            {
                var path = Path.Combine(Application.StartupPath, "expotr.pdf");
                gridControl1.ExportToPdf(path);

                Process.Start(path);
            }
            else if (file_format == "xlsx")
            {
                IHelper.ExportToXlsx(gridControl1);
            }

          
        }

        private void bandedGridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            popupMenu1.ShowPopup(Control.MousePosition);
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (!is_show)
            {
                return;
            }

            GetTurns();
        }

        private void frmMatTurn_Shown(object sender, EventArgs e)
        {
            is_show = true;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = bandedGridView1.GetFocusedRow() as GetMatMove_Result;
            if ( row != null )
            {
                PrintDoc.Show(row.Id.Value, row.WType.Value, DB.SkladBase());
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = bandedGridView1.GetFocusedRow() as GetMatMove_Result;
            if ( row != null )
            {
                FindDoc.Find(row.Id, row.WType, row.OnDate);
            }
        }
    }
}
