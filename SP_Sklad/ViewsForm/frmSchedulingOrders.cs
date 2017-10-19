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

namespace SP_Sklad.ViewsForm
{
    public partial class frmSchedulingOrders : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
 
        public frmSchedulingOrders()
        {
            _db = new BaseEntities();

            InitializeComponent();
        }

        private void frmSchedulingOrders_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpEdit1.DataSource = _db.MatRecipe.Where(w => w.RType == 1).Select(s => new
            {
                RecId = s.RecId,
                Name = s.Name,
                Amount = s.Amount,
                MatName = s.Materials.Name,
                MatId = s.MatId,
                Out = s.Out
            }).ToList();

            SchedulingOrdersBS.DataSource = _db.SchedulingOrders.OrderBy(o=> o.MatRecipe.Name).ToList();
        }

        private void SchedulingGridView_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = e.Row as SchedulingOrders;
            if (row.Id == Guid.Empty)
            {
                row.Id = Guid.NewGuid();
                _db.SchedulingOrders.Add(row);
            }
            else
            {
                _db.Entry<SchedulingOrders>(row).State = System.Data.Entity.EntityState.Modified;
            }

            _db.SaveChanges();
        }

        private void SchedulingGridView_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
            var row = e.Row as SchedulingOrders;
            _db.SchedulingOrders.Remove(row);
            _db.SaveChanges();
        }
    }
}