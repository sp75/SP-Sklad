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
using SP_Sklad.Common;
using SP_Sklad.SkladData;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBDetForm
{
    public partial class frmWayBillTMCDet : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private int? _PosId { get; set; }
        private WaybillList _wb { get; set; }
        private WayBillTmc _wbt { get; set; }

        public frmWayBillTMCDet(BaseEntities db, int? PosId, WaybillList wb)
        {
            InitializeComponent();

            _db = db;
            _PosId = PosId;
            _wb = wb;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {

            if (_db.Entry<WayBillTmc>(_wbt).State == EntityState.Detached)
            {
                _db.WayBillTmc.Add(_wbt);
            }

            _db.SaveChanges();
        }

        private void frmWayBillTMCDet_Load(object sender, EventArgs e)
        {
            var list_mat = _db.Materials.Where(w => w.TypeId == 4).Select(s => new { s.MatId, s.Name, s.Artikul }).ToList();

            MatComboBox.Properties.DataSource = list_mat;
       //     MatComboBox.EditValue = list_mat.Any() ? list_mat.FirstOrDefault().MatId : MatComboBox.EditValue;

            if (_PosId == null)
            {
                _wbt = new WayBillTmc()
                {
                    WbillId = _wb.WbillId,
                    Amount = 0,
                    TurnType = _wb.WType > 0 ? 1 : -1,
                    Num = _db.GetWaybillDetIn(_wb.WbillId).Count() + 1,
                    MatId = list_mat.Any() ? list_mat.FirstOrDefault().MatId : 0
                };
            }
            else
            {
                _wbt = _db.WayBillTmc.Find(_PosId);
            }

            if (_wbt != null)
            {
                WayBillTmcBS.DataSource = _wbt;
            }

            GetOk();

        }

        bool GetOk()
        {
            bool recult = (MatComboBox.EditValue != DBNull.Value && (int)MatComboBox.EditValue > 0);

            OkButton.Enabled = recult;

            return recult;
        }

        private void MatComboBox_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void AmountEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && AmountEdit.Value > 0)
            {
                OkButton.PerformClick();
            }
        }

        private void frmWayBillTMCDet_Shown(object sender, EventArgs e)
        {
            AmountEdit.Focus();
        }
    }
}