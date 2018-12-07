using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.FinanseForm;
using SP_Sklad.Properties;
using SP_Sklad.SkladData;
using SP_Sklad.WBForm;

namespace SP_Sklad.Common
{
   public class DocEdit
    {
        private const int DeadlockErrorNumber = 1205;
        private const int LockingErrorNumber = 1222;
        private const int UpdateConflictErrorNumber = 3960;

       public static void WBEdit(GetWayBillList_Result dr)
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
                       var wb = db.WaybillList.FirstOrDefault(f => f.WbillId == dr.WbillId);

                       if (wb == null)
                       {
                           MessageBox.Show(Resources.not_find_wb);
                           return;
                       }

                       if (!(wb.SessionId == null || wb.SessionId == UserSession.SessionId))
                       {
                           MessageBox.Show(Resources.deadlock);
                           return;
                       }

                       if (wb.Checked == 2)
                       {
                           return;
                       }

                       if (wb.Checked == 1)
                       {
                           if (MessageBox.Show(Resources.edit_info, "Відміна проводки", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                           {
                               result = DBHelper.StornoOrder(db, dr.WbillId);
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

                       if (dr.WType == 1 || dr.WType == 16)
                       {
                           using (var wb_in = new frmWayBillIn(dr.WType, wb.WbillId))
                           {
                               wb_in.ShowDialog();
                           }
                       }

                       if (dr.WType == -1 || dr.WType == -16 || dr.WType == 2)
                       {
                           using (var wb_out = new frmWayBillOut(dr.WType, wb.WbillId))
                           {
                               wb_out.ShowDialog();
                           }
                       }

                       if (dr.WType == 6)// Повернення від кліента
                       {
                           using (var wb_re_in = new frmWBReturnIn(dr.WType, wb.WbillId))
                           {
                               wb_re_in.ShowDialog();
                           }
                       }

                       if (dr.WType == -6) //Повернення постачальнику
                       {
                           using (var wb_re_out = new frmWBReturnOut(wb.WbillId))
                           {
                               wb_re_out.ShowDialog();
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

       public static void PDEdit(GetPayDocList_Result pd_row)
       {
           if (pd_row == null)
           {
               return;
           }

           using (var db = new BaseEntities())
           {
               var trans = db.Database.BeginTransaction();
               try
               {
                   var pd = db.Database.SqlQuery<PayDoc>("SELECT * from PayDoc WITH (UPDLOCK, NOWAIT) where PayDocId = {0}", pd_row.PayDocId).FirstOrDefault();
                   if (pd == null)
                   {
                       MessageBox.Show(Resources.not_find_wb);
                       return;
                   }

                   if (pd.Checked == 1)
                   {
                       if (MessageBox.Show(Resources.edit_info, "Відміна проводки", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                       {
                           pd.Checked = 0;
                           db.Entry<PayDoc>(pd).State = System.Data.Entity.EntityState.Modified;
                           db.SaveChanges();
                       }
                   }
                   trans.Commit();

                   if (pd.Checked == 0)
                   {
                       using (var pd_form = new frmPayDoc(pd_row.DocType, pd_row.PayDocId))
                       {
                           pd_form.ShowDialog();
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


       public static void FinDocEdit(MoneyMoveList_Result pd_row)
       {
           if (pd_row == null)
           {
               return;
           }

           using (var db = new BaseEntities())
           {
               var trans = db.Database.BeginTransaction();
               try
               {
                   var pd = db.Database.SqlQuery<PayDoc>("SELECT * from PayDoc WITH (UPDLOCK, NOWAIT) where PayDocId = {0}", pd_row.PayDocId).FirstOrDefault();
                   if (pd == null)
                   {
                       MessageBox.Show(Resources.not_find_wb);
                       return;
                   }

                   if (pd.Checked == 1)
                   {
                       if (MessageBox.Show(Resources.edit_info, "Відміна проводки", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                       {
                           pd.Checked = 0;
                           db.Entry<PayDoc>(pd).State = System.Data.Entity.EntityState.Modified;

                           var pd_to = db.PayDoc.FirstOrDefault(w => w.OperId == pd.OperId);
                           if (pd_to != null)
                           {
                               pd_to.Checked = 0;
                           }

                           db.SaveChanges();
                       }
                   }
                   trans.Commit();

                   if (pd.Checked == 0)
                   {
                       if (pd.DocType == 6)
                       {
                           using (var pd_form = new frmMoneyCorrecting(pd_row.PayDocId))
                           {
                               pd_form.ShowDialog();
                           }
                       }

                       if (pd.DocType == 3)
                       {
                           using (var pd_form = new frmMoneyMove(pd_row.PayDocId))
                           {
                               pd_form.ShowDialog();
                           }
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
