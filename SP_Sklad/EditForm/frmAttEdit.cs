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
using SP_Sklad.ViewsForm;

namespace SP_Sklad.EditForm
{
    public partial class frmAttEdit : DevExpress.XtraEditors.XtraForm
    {
        private WaybillList _wb { get; set; }

        public frmAttEdit(WaybillList wb)
        {
            InitializeComponent();
            _wb = wb;

            WaybillListBS.DataSource = wb;
        }

        private void frmAttEdit_Load(object sender, EventArgs e)
        {
            CarsLookUpEdit.Properties.DataSource = DB.SkladBase().Cars.ToList();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _wb.AttDate = null;
            _wb.AttNum = null;
            _wb.Received = null;
            _wb.CarId = null;
        }

        private void CarsLookUpEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var frm = new frmCars();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CarsLookUpEdit.EditValue = frm.focused_row != null ? (Guid?)frm.focused_row.Id : null;

                    CarsLookUpEdit.Properties.DataSource = DB.SkladBase().Cars.AsNoTracking().ToList();
                }
            }
        }

        private void CarsLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (CarsLookUpEdit.EditValue == null || CarsLookUpEdit.EditValue == DBNull.Value)
            {
                return;
            }

            var car_id = (Guid)(CarsLookUpEdit.EditValue);

            _wb.Received = DB.SkladBase().Routes.Where(w => w.CarId == car_id).Select(s => s.Kagent1.Name).FirstOrDefault();
        }
    }
}
