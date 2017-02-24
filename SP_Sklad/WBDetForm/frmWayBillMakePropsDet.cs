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

namespace SP_Sklad.WBDetForm
{
    public partial class frmWayBillMakePropsDet : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private int _wbill_id { get; set; }
        private WayBillMakeProps prop { get; set; }
        private int? _DetId { get; set; }

        public frmWayBillMakePropsDet(int wbill_id, int? DetId = null)
        {
            InitializeComponent();

            _wbill_id = wbill_id;
            _DetId = DetId;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
        }

        private void frmWayBillMakePropsDet_Load(object sender, EventArgs e)
        {
            _db = new BaseEntities();
            MatComboBox.Properties.DataSource = _db.MaterialsList.Select(s=> new {s.MatId, s.Name}).ToList();


            if (_DetId == null)
            {
                prop = _db.WayBillMakeProps.Add(new WayBillMakeProps
                {
                    WbillId = _wbill_id,
                    OnDate = DateTime.Now,
                    PersonId = DBHelper.CurrentUser.KaId.Value,
                    Amount = 1
                });
            }
            else
            {
                prop = _db.WayBillMakeProps.Find(_DetId.Value);
                prop.PersonId = DBHelper.CurrentUser.KaId.Value;
            }

            WayBillMakePropsBS.DataSource = prop;
        }

        private void frmWayBillMakePropsDet_FormClosed(object sender, FormClosedEventArgs e)
        {
            _db.Dispose();
        }
    }
}