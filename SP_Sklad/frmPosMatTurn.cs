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
using SP_Sklad.ViewsForm;

namespace SP_Sklad
{
    public partial class frmPosMatTurn : DevExpress.XtraEditors.XtraForm
    {
        public int _mat_id { get; set; }
        private bool is_show { get; set; }
        private int? _wid { get; set; }

        public frmPosMatTurn(int mat_id, int? wid = null)
        {
            InitializeComponent();
            is_show = false;
            _mat_id = mat_id;
            _wid = wid;
        }

        private void frmMatTurn_Load(object sender, EventArgs e)
        {
            WHComboBox.Properties.DataSource = DBHelper.WhList;
            WHComboBox.EditValue = _wid;

            wbStartDate.DateTime = DateTimeDayOfMonthExtensions.FirstDayOfMonth(DateTime.Now);
            wbEndDate.DateTime = DateTime.Now.AddDays(1);

            var mat = new BaseEntities().Materials.Find(_mat_id);
            if (mat != null)
            {
                this.Text = "Історія партій товару: " + mat.Name;
            }

            GetTurns();
        }

        private void GetTurns()
        {
            var start_date = wbStartDate.DateTime < SqlDateTime.MinValue.Value ? SqlDateTime.MinValue.Value : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < SqlDateTime.MinValue.Value ? SqlDateTime.MaxValue.Value : wbEndDate.DateTime.SetEndDay();
            var wid = WHComboBox.EditValue != null ? (int)WHComboBox.EditValue : -1;


            DocListBindingSource.DataSource = DB.SkladBase().WMatTurn.Where(w => w.OnDate > start_date && w.OnDate <= end_date && w.MatId == _mat_id && (w.WId == wid || wid == -1)).OrderBy(o => o.OnDate).ToList();
            bandedGridView1.ExpandAllGroups();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var file_format = DBHelper.CurrentUser.ReportFormat;

            if (file_format == "pdf")
            {
                var path = Path.Combine(Application.StartupPath, "expotr.pdf");
                bandedGridView1.ExportToPdf(path);

                Process.Start(path);
            }
            else if (file_format == "xlsx")
            {
                using (MemoryStream ms_xlsx = new MemoryStream())
                {
                    bandedGridView1.ExportToXlsx(ms_xlsx);
                    new frmSpreadsheed(ms_xlsx.ToArray()).Show();
                }
            }


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
            var row = bandedGridView1.GetFocusedRow() as WMatTurn;
            if (row != null)
            {
                PrintDoc.Show(row.WaybillDet1.WaybillList.Id, row.WaybillDet1.WaybillList.WType, DB.SkladBase());
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = bandedGridView1.GetFocusedRow() as WMatTurn;
            if (row != null)
            {
                FindDoc.Find(row.WaybillDet1.WaybillList.Id, row.WaybillDet1.WaybillList.WType, row.WaybillDet1.WaybillList.OnDate);
            }
        }
    }
}
