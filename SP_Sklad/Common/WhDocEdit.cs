using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Properties;
using SP_Sklad.SkladData;
using SP_Sklad.WBForm;

namespace SP_Sklad.Common
{
    class WhDocEdit
    {
        private const int DeadlockErrorNumber = 1205;
        private const int LockingErrorNumber = 1222;
        private const int UpdateConflictErrorNumber = 3960;

        public static void WBEdit(GetWayBillListWh_Result dr)
        {
            int? result = 0;

            if (dr == null)
            {
                return;
            }

            using (var db = new BaseEntities())
            {
                try
                {
                    var wb = db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK, NOWAIT) where WbillId = {0}", dr.WBillId).FirstOrDefault();

                    if (wb == null)
                    {
                        MessageBox.Show(Resources.not_find_wb);
                        return;
                    }

                    if (wb.SessionId != null)
                    {
                        MessageBox.Show(Resources.deadlock);
                        return;
                    }

                    if (wb.Checked == 1)
                    {
                        if (MessageBox.Show(Resources.edit_info, "Відміна проводки", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            result = DBHelper.StornoOrder(db, dr.WBillId.Value);
                        }
                        else
                        {
                            result = 1;
                        }
                    }

                    if (result == 1)
                    {
                        return;
                    }

                    if (dr.WType == 4 )
                    {
                        using (var wb_in = new frmWayBillMove( wb.WbillId))
                        {
                            wb_in.ShowDialog();
                        }
                    }

                    if (dr.WType == 5)
                    {
                        using (var wb_write_on = new frmWBWriteOn(wb.WbillId))
                        {
                            wb_write_on.ShowDialog();
                        }
                    }

                    if (dr.WType == -5)
                    {
                        using (var wb_on = new frmWBWriteOff(wb.WbillId))
                        {
                            wb_on.ShowDialog();
                        }
                    }

                    if (dr.WType == 7 )
                    {
                        using (var wb_on = new frmWBInventory(wb.WbillId))
                        {
                            wb_on.ShowDialog();
                        }
                    }

                }

                catch (EntityCommandExecutionException exception)
                {
                    var e = exception.InnerException as SqlException;
                    if (e != null)
                    {
                        if (!e.Errors.Cast<SqlError>().Any(error =>
                               (error.Number == DeadlockErrorNumber) ||
                               (error.Number == LockingErrorNumber) ||
                               (error.Number == UpdateConflictErrorNumber)))
                        {
                            MessageBox.Show(e.Message);
                        }
                        else
                        {
                            MessageBox.Show(Resources.deadlock);
                        }
                    }
                    else
                    {
                        MessageBox.Show(exception.Message);
                    }

                    return;
                }
            }
        }

    }
}
