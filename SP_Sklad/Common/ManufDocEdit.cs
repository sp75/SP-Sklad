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
    class ManufDocEdit
    {
        private const int DeadlockErrorNumber = 1205;
        private const int LockingErrorNumber = 1222;
        private const int UpdateConflictErrorNumber = 3960;

        public static void WBEdit(int WType, int WbillId)
        {
            int? result = 0;

          /*  if (dr == null)
            {
                return;
            }*/

            using (var db = new BaseEntities())
            {
                try
                {
                    var wb = db.WaybillList.FirstOrDefault(f => f.WbillId == WbillId);

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
                            result = DBHelper.StornoOrder(db, WbillId);
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
               

                    if (WType == -20)
                    {
                        using (var wb_make = new frmWBManufacture(WbillId))
                        {
                            wb_make.ShowDialog();
                        }
                    }

                    if (WType == -22)
                    {
                        using (var wb_make = new frmWBDeboning(WbillId))
                        {
                            wb_make.ShowDialog();
                        }
                    }

                    if (WType == -24)
                    {
                        using (var wb_make = new frmPreparationRawMaterials(WbillId))
                        {
                            wb_make.ShowDialog();
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
