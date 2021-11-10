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
            CarsLookUpEdit.Properties.DataSource = GetCars();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _wb.AttDate = null;
            _wb.AttNum = null;
            _wb.Received = null;
            _wb.CarId = null;
            _wb.RouteId = null;
        }

        private void CarsLookUpEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                var frm = new frmCars();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CarsLookUpEdit.EditValue = frm.focused_row != null ? (Guid?)frm.focused_row.Id : null;

                    CarsLookUpEdit.Properties.DataSource = GetCars();
                }
            }
        }

        private void CarsLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (CarsLookUpEdit.EditValue == null || CarsLookUpEdit.EditValue == DBNull.Value || CarsLookUpEdit.GetSelectedDataRow() == null)
            {
                return;
            }

            var sr = CarsLookUpEdit.GetSelectedDataRow() as CarsList;

            _wb.Received = sr.DriverName;
            _wb.RouteId = sr.RouteId;
        }

        private List<CarsList> GetCars()
        {
            return DB.SkladBase().Cars.Where(w => w.Routes.Any()).Select(s => new CarsList
            {
                Id = s.Id,
                Name = s.Name,
                RouteName = s.Routes.FirstOrDefault().Name,
                RouteId = s.Routes.FirstOrDefault().Id,
                DriverName = s.Routes.FirstOrDefault().Kagent1.Name,
                Number = s.Number
            }).ToList();
        }

        public class CarsList
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string RouteName { get; set; }
            public int? RouteId { get; set; }
            public string DriverName { get; set; }
            public string Number { get; set; }
        }
    }
}
