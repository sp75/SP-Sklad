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
    public partial class frmOrderedList : DevExpress.XtraEditors.XtraForm
    {
        private bool is_show { get; set; }
        private int _ka_id { get; set; }
        private int _w_type { get; set; }
        private int _mat_id { get; set; }


        public frmOrderedList(int ka_id, int w_type, int mat_id)
        {
            InitializeComponent();
            is_show = false;
            _ka_id = ka_id;
            _w_type = w_type;
            _mat_id = mat_id;
        }

        private void frmOrderedList_Load(object sender, EventArgs e)
        {
            wTypeList.Properties.DataSource = new List<object>() { new { Id = 0, Name = "Усі" } }.Concat(new BaseEntities().DocType.Where(w => w.Id == 16 || w.Id == -16).Select(s => new { s.Id, s.Name })).ToList();
            wTypeList.EditValue = _w_type;

            KagentComboBox.Properties.DataSource = DBHelper.KagentsList;// new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().Kagent.Where(w => w.Archived == null || w.Archived == 0).Select(s => new { s.KaId, s.Name }));
            KagentComboBox.EditValue = _ka_id;

            MatComboBox.Properties.DataSource = new List<object>() { new { MatId = 0, Name = "Усі" } }.Concat(new BaseEntities().Materials.Where(w => w.Deleted == 0).Select(s => new { s.MatId, s.Name }).ToList());
            MatComboBox.EditValue = _mat_id;

            wbStatusList.Properties.DataSource = new List<object>() { new { Id = 0, Name = "Усі замовлення" }, new { Id = 1, Name = "Тільки активні" } };
            wbStatusList.EditValue = 0;

            wbStartDate.DateTime = DateTimeDayOfMonthExtensions.FirstDayOfMonth(DateTime.Now);
            wbEndDate.DateTime = DateTime.Now.AddDays(1);

            GetOrderedList();
        }

        private void GetOrderedList()
        {
            OrderedListBS.DataSource = new BaseEntities().OrderedList(wbStartDate.DateTime, wbEndDate.DateTime, (int)MatComboBox.EditValue, (int)KagentComboBox.EditValue, (int)wTypeList.EditValue, (int)wbStatusList.EditValue, Guid.Empty).ToList();
        }

        private void frmOrderedList_Shown(object sender, EventArgs e)
        {
            is_show = true;
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (!is_show)
            {
                return;
            }

            GetOrderedList();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var path = Path.Combine(Application.StartupPath, "expotr.pdf");
            gridControl1.ExportToPdf(path);

            Process.Start(path);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = bandedGridView1.GetFocusedRow() as OrderedList_Result;
            if (row != null)
            {
                PrintDoc.Show(row.Id, row.WType, DB.SkladBase());
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = bandedGridView1.GetFocusedRow() as OrderedList_Result;
            if (row != null)
            {
                FindDoc.Find(row.Id, row.WType, row.OnDate);
            }
        }
    }
}
