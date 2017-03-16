using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class frmKABalans : DevExpress.XtraEditors.XtraForm
    {
        public int _ka_id { get; set; }

        public frmKABalans(int KaId)
        {
            InitializeComponent();
            _ka_id = KaId;
        }


        private void frmKABalans_Load(object sender, EventArgs e)
        {
            wTypeList.Properties.DataSource = new List<object>() { new { Id = 0, Name = "Усі" } }.Concat(new BaseEntities().DocType.Select(s=> new { s.Id , s.Name})).ToList();
            wTypeList.EditValue = 0;

            wbStartDate.DateTime = DateTimeDayOfMonthExtensions.FirstDayOfMonth(DateTime.Now);
            wbEndDate.DateTime = DateTime.Now.AddDays(1);

            GetBalans();
        }

        private void GetBalans()
        {
            DocListBindingSource.DataSource = DB.SkladBase().GetDocList(wbStartDate.DateTime, wbEndDate.DateTime, _ka_id, (int)wTypeList.EditValue).ToList();
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wTypeList.ContainsFocus || wbStartDate.ContainsFocus || wbEndDate.ContainsFocus)
            {
                GetBalans();
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var path = Path.Combine(Application.StartupPath, "expotr.xlsx");
            gridControl1.ExportToXlsx (path);

            Process.Start(path);
        }

        private void bandedGridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
             popupMenu1.ShowPopup(Control.MousePosition);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dir = bandedGridView1.GetFocusedRow() as GetDocList_Result;
            PrintDoc.Show(dir.Id.Value, dir.WType.Value,DB.SkladBase());
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = bandedGridView1.GetFocusedRow() as GetDocList_Result;
            if (row != null)
            {
                FindDoc.Find(row.Id, row.WType, row.OnDate);
            }
        }
    }
}
