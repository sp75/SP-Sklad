using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.SkladData;

namespace SP_Sklad
{
    public partial class frmMatRSV : Form
    {
        private int _mat_id { get; set; }
        private BaseEntities _db { get; set; }
        public frmMatRSV(int MatId , BaseEntities db)
        {
            InitializeComponent();
            _mat_id = MatId;
            _db = db;

        }

        private void frmMatRSV_Load(object sender, EventArgs e)
        {
            wTypeList.Properties.DataSource = new List<object>() { new { Id = 0, Name = "Усі" } }.Concat(new BaseEntities().DocType.Select(s => new { s.Id, s.Name })).ToList();
            wTypeList.EditValue = 0;

            KagentComboBox.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(_db.Kagent.Select(s => new { s.KaId, s.Name }));
            KagentComboBox.EditValue = 0;
            
            wbStartDate.DateTime = DateTimeDayOfMonthExtensions.FirstDayOfMonth(DateTime.Now);
            wbEndDate.DateTime = DateTime.Now.AddDays(1);

            GetRsv();
        }
        private void GetRsv()
        {
            DocListBindingSource.DataSource = _db.GetMatRsv( _mat_id, (int)KagentComboBox.EditValue,wbStartDate.DateTime, wbEndDate.DateTime, (int)wTypeList.EditValue).ToList();
        }
    }
}
