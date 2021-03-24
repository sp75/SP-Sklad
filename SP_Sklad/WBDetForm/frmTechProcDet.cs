using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.Common;
using SP_Sklad.EditForm;
using SP_Sklad.SkladData;

namespace SP_Sklad.WBDetForm
{
    public partial class frmTechProcDet : DevExpress.XtraEditors.XtraForm
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
            _db = new BaseEntities();

            using (var s = new UserSettingsRepository(UserSession.UserId, _db))
            {
                AmountEdit.ReadOnly = !(s.AccessEditWeight == "1");
            }
        }

        private void frmTechProcDet_Load(object sender, EventArgs e)
        {
            TechProcessCB.Properties.DataSource = _db.TechProcess.OrderBy(o => o.Num).Select(s => new { s.ProcId, s.Name, s.Num, s.Kod }).ToList();
            PersonComboBox.Properties.DataSource = DBHelper.Persons;

            TechProcessCB.Properties.Buttons[1].Enabled = DBHelper.CanViewUserTreeNode("frmTechProcessEdit");

            var ext_list = _db.Tara.Where(w => w.TypeId == 2).Select(s => new { MatId = s.TaraId, s.Name, s.Artikul }).ToList();
            ExtMatComboBox.Properties.DataSource = ext_list;
            ExtMatComboBox2.Properties.DataSource = ext_list;

            TareMatEdit.Properties.DataSource = _db.Tara.Where(w => w.TypeId == 5).Select(s => new { MatId = s.TaraId, s.Name, s.Artikul, s.Weight }).ToList();

            if (_DetId == null)
            {
                tp_d = _db.TechProcDet.Add(new TechProcDet()
                {
                    Num = 0,//(_db.TechProcDet.Where(w => w.WbillId == _wbill_id).Select(s => (int?)s.Num).Max() ?? 0) + 1,
                    WbillId = _wbill_id,
                    PersonId = DBHelper.CurrentUser.KaId,
                    OnDate = DateTime.Now,
                    Out = 0//_db.WaybillList.Find(_wbill_id).WayBillMake.Amount
                });
            }
            else
            {
                tp_d = _db.TechProcDet.Find(_DetId.Value);
                tp_d.PersonId = DBHelper.CurrentUser.KaId;
            }

            TechProcDetBS.DataSource = tp_d;

            var r = _db.WaybillList.Where(w => w.WType == -20 && (w.Checked == 0 || w.Checked == 2) && w.WbillId != _wbill_id).SelectMany(s => s.TechProcDet).Where(w => w.MatId != null).Select(s => s.MatId).ToList();
            MatComboBox.Properties.DataSource = _db.Tara.Where(w => w.TypeId == 1 && (!r.Contains(w.TaraId) || w.TaraId == tp_d.MatId)).Select(s => new { MatId = s.TaraId, s.Name, s.Artikul }).ToList();
            //   MatComboBox.Properties.DataSource = _db.Materials.Where(w => w.TypeId == 1 /*&& (!r.Contains(w.MatId) || w.MatId == tp_d.MatId)*/).Select(s => new { s.MatId, s.Name, s.Artikul }).ToList();

            if (ext_list.Any())
            {
                ExtMatComboBox.EditValue = ext_list.FirstOrDefault().MatId;
                if (ext_list.Count() > 1)
                {
                    ExtMatComboBox2.EditValue = ext_list[1].MatId;
                }
            }

            ValidateForm();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (_db.WaybillList.Any(a => a.WbillId == _wbill_id))
            {
                _db.SaveChanges();
            }
        }

        private void frmTechProcDet_FormClosed(object sender, FormClosedEventArgs e)
        {
            _db.Dispose();
        }

        private void OnDateEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                OnDateEdit.EditValue = DBHelper.ServerDateTime();
            }
        }

        private void TechProcessCB_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                TechProcessCB.EditValue = IHelper.ShowDirectList(TechProcessCB.EditValue, 14);
            }
        }

        private void PersonComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                PersonComboBox.EditValue = IHelper.ShowDirectList(PersonComboBox.EditValue, 3);
            }
        }

        private void AmountEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var frm = new frmWeightEdit(TechProcessCB.Text, 1);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    AmountEdit.EditValue = frm.AmountEdit.Value;
                }
            }

            if (e.Button.Index == 2)
            {
                var frm = new frmWeightEdit(TechProcessCB.Text, 2);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    AmountEdit.EditValue = frm.AmountEdit.Value;
                }
            }
        }

        private void frmTechProcDet_Shown(object sender, EventArgs e)
        {
            AmountEdit.Focus();
        }

        private void TechProcessCB_EditValueChanged(object sender, EventArgs e)
        {
            var row = TechProcessCB.GetSelectedDataRow() as dynamic;
            if (row != null)
            {
                tp_d.Num = row.Num != null ? row.Num : (_db.TechProcDet.Where(w => w.WbillId == _wbill_id).Select(s => (int?)s.Num).Max() ?? 0) + 1;
                tp_d.Out = row.Kod == "finish" && tp_d.Out == 0 ? _db.GetMakeAmount(_wbill_id).FirstOrDefault().AmountOut.Value : tp_d.Out;
            }

            ValidateForm();
        }

        private void AmountEdit_MouseUp(object sender, MouseEventArgs e)
        {
            ((CalcEdit)sender).SelectAll();
        }

        private void ValidateForm()
        {
            OkButton.Enabled = (TechProcessCB.GetSelectedDataRow() != null) && (OnDateEdit.DateTime > SqlDateTime.MinValue.Value) && AmountEdit.Value > 0;
        }

        private void OnDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            ValidateForm();
        }
    }
}
