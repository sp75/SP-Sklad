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
            CarsLookUpEdit.Properties.DataSource = GetRoute();
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
         /*   if (e.Button.Index == 1)
            {
                var frm = new frmCars();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CarsLookUpEdit.EditValue = frm.focused_row != null ? (Guid?)frm.focused_row.Id : null;

                    CarsLookUpEdit.Properties.DataSource = GetRoute();
                }
            }*/
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
            _wb.CarId = sr.Id;
        }

        private List<CarsList> GetRoute()
        {
            return DB.SkladBase().Routes.Where(w=> w.CarId != null).Select(s => new CarsList
            {
                Id = s.Cars.Id,
                Name = s.Cars.Name,
                RouteId = s.Id,
                RouteName = s.Name,
                DriverName = s.Kagent1.Name,
                Number = s.Cars.Number
            }).ToList();
        }

        public class CarsList
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string RouteName { get; set; }
            public int RouteId { get; set; }
            public string DriverName { get; set; }
            public string Number { get; set; }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var id = IHelper.ShowDirectList(null, 3);
            if (id != null)
            {
                using (var _db = new BaseEntities())
                {
                    int k = Convert.ToInt32(id);

                    var ka = _db.Kagent.FirstOrDefault(w => w.KaId == k);

                    ReceivedTextEdit.Text = ka.Name;
                }
            }
        }
    }
}
