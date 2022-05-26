using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
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
    public partial class frmCarEdit : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private Cars _car { get; set; }
        private Guid? _CarId { get; set; }

        public frmCarEdit(Guid? CarId = null)
        {
            InitializeComponent();
            _CarId = CarId;

            _db = DB.SkladBase();
            current_transaction = _db.Database.BeginTransaction();
        }

        private void frmAttEdit_Load(object sender, EventArgs e)
        {
            DriversLookUpEdit.Properties.DataSource = _db.Kagent.Where(w => w.JobType == 3).Select(s => new { s.KaId, s.Name }).ToList();

            if (_CarId == null)
            {
                _car = _db.Cars.Add(new Cars
                {
                    Id = Guid.NewGuid()
                });

                _db.SaveChanges();

                _CarId = _car.Id;

                Text = "Додати нову машину";
            }
            else
            {
                _car = _db.Cars.Find(_CarId);

                Text = "Властвості машини";
            }

            CarsBS.DataSource = _car;
        }

        private void frmCarEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
            current_transaction.Commit();
        }

        private void DriversLookUpEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                DriversLookUpEdit.EditValue = null;
            }
        }
    }
}
