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
using SP_Sklad.ViewsForm;

namespace SP_Sklad.EditForm
{
    public partial class frmRouteEdit : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private int? _id { get; set; }
        private Routes r { get; set; }

        public frmRouteEdit(int? Id = null)
        {
            InitializeComponent();
            _id = Id;
            _db = DB.SkladBase();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
        }

        private void frmRouteEdit_Load(object sender, EventArgs e)
        {
            if (_id == null)
            {
                r = _db.Routes.Add(new Routes());
            }
            else
            {
                r = _db.Routes.Find(_id);
            }

            RoutesBS.DataSource = r;

            DriversLookUpEdit.Properties.DataSource = _db.Kagent.Where(w => w.JobType == 3).Select(s => new Kontragent { KaId = s.KaId, Name = s.Name }).ToList();
            CarsLookUpEdit.Properties.DataSource =  _db.Cars.ToList();
        }

     
        private void CarsLookUpEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var frm = new frmCars();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    CarsLookUpEdit.EditValue = frm.focused_row != null ? (Guid?)frm.focused_row.Id : null;

                    CarsLookUpEdit.Properties.DataSource = DB.SkladBase().Cars.AsNoTracking().ToList();
                }
            }
        }

        private void DriversLookUpEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                DriversLookUpEdit.EditValue = null;
            }
        }

        private void DriversLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (DriversLookUpEdit.ContainsFocus)
            {
                if (DriversLookUpEdit.EditValue != null)
                {
                    var row = DriversLookUpEdit.GetSelectedDataRow() as Kontragent;
                    var car = _db.Cars.FirstOrDefault(w => w.DriverId == row.KaId);

                    r.CarId = car != null ? (Guid?)car.Id : null;
                }
                else
                {
                    r.CarId = null;
                }
            }
        }
    }
}