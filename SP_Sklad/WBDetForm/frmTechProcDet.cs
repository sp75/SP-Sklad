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

namespace SP_Sklad.WBDetForm
{
    public partial class frmTechProcDet : Form
    {
        BaseEntities _db { get; set; }
        private int _wbill_id { get; set; }
        private TechProcDet tp_d { get; set; }
        private int? _DetId { get; set; }

        public frmTechProcDet(int wbill_id, int? DetId = null)
        {
            InitializeComponent();

            _wbill_id = wbill_id;
            _DetId = DetId;
        }

        private void frmTechProcDet_Load(object sender, EventArgs e)
        {
            _db = new BaseEntities();

            TechProcessCB.Properties.DataSource = _db.TechProcess.Select(s => new { s.ProcId, s.Name }).ToList();
            PersonComboBox.Properties.DataSource = DBHelper.Persons;

            if (_DetId == null)
            {
                tp_d = _db.TechProcDet.Add(new TechProcDet()
                {
                    WbillId = _wbill_id,
                    PersonId = DBHelper.CurrentUser.KaId,
                    OnDate = DateTime.Now,
                    Out = _db.WaybillList.Find(_wbill_id).WayBillMake.Amount
                });
            }
            else
            {
                tp_d = _db.TechProcDet.Find(_DetId.Value);
            }
             
            TechProcessCB.DataBindings.Add(new Binding("EditValue", tp_d, "ProcId"));
            AmountEdit.DataBindings.Add(new Binding("EditValue", tp_d, "Out"));
            OnDateEdit.DataBindings.Add(new Binding("EditValue", tp_d, "OnDate"));
            PersonComboBox.DataBindings.Add(new Binding("EditValue", tp_d, "PersonId", true, DataSourceUpdateMode.OnValidation));
            NotesTextEdit.DataBindings.Add(new Binding("EditValue", tp_d, "Notes", true, DataSourceUpdateMode.OnValidation));
        }

        private void NowDateBtn_Click(object sender, EventArgs e)
        {
            OnDateEdit.DateTime = DateTime.Now;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var ss = _db.Entry<TechProcDet>(tp_d).State;
            _db.SaveChanges();
        }

        private void frmTechProcDet_FormClosed(object sender, FormClosedEventArgs e)
        {
            _db.Dispose();
        }
    }
}
