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
using SP_Sklad.SkladData;

namespace SP_Sklad.EditForm
{
    public partial class frmWarehouseEdit : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private int? _wid { get; set; }
        private Warehouse wh { get; set; }
        private DbContextTransaction current_transaction { get; set; }

        public frmWarehouseEdit(int? wid = null)
        {
            InitializeComponent();
            _wid = wid;
            _db = DB.SkladBase();
            current_transaction = _db.Database.BeginTransaction();
        }

        private void frmWarehouseEdit_Load(object sender, EventArgs e)
        {
            if (_wid == null)
            {
                wh = _db.Warehouse.Add(new Warehouse
                {
                    Deleted = 0,
                    Def = _db.Warehouse.Any(a => a.Def == 1) ? 0 : 1,
                    Name = ""
                });

                _db.SaveChanges();
            }
            else
            {
                wh = _db.Warehouse.Find(_wid);
            }

            WarehouseDS.DataSource = wh;

            DefCheckBox.Enabled = !DefCheckBox.Checked;

            UserListBS.DataSource = _db.Users.Where(w => w.IsWorking && w.Kagent.Any() ).Select(s => new UserList
            {
                UserId = s.UserId,
                Name = s.Kagent.FirstOrDefault().Name,
                WhAccess = s.UserAccessWh.Any(a => a.WId == wh.WId)
            }).ToList();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
            current_transaction.Commit();
        }

        private class UserList
        {
            public int UserId { get; set; }
            public string Name { get; set; }
            public bool WhAccess { get; set; }

        }

        private void frmWarehouseEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void UsersGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var row = UsersGridView.GetFocusedRow() as UserList;

            if (e.Column.FieldName == "WhAccess")
            {
                if (Convert.ToBoolean(e.Value))
                {
                    _db.UserAccessWh.Add(new UserAccessWh { UserId = row.UserId, UseReceived = true, WId = wh.WId });
                }
                else
                {
                    _db.UserAccessWh.Remove(_db.UserAccessWh.FirstOrDefault(w=> w.WId == wh.WId && w.UserId == row.UserId));
                }

                _db.SaveChanges();
            }
        }
    }
}
