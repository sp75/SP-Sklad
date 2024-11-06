using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SkladEngine.ModelViews;
using SP_Sklad.Common;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.SkladData
{
    public static class DB
    {
        public static BaseEntities SkladBase()
        {
            return new BaseEntities();
        }
    }

    public static class DataContextExtension
    {
        public static DbContext DeleteWhere<T>(this DbContext db, Expression<Func<T, bool>> filter)
            where T : class
        {
            var query = db.Set<T>().Where(filter);

            var select_sql = query.ToString();
            var delete_sql = "DELETE [Extent1] " + select_sql.Substring(select_sql.IndexOf("FROM", StringComparison.Ordinal));

            var internal_query =
                query.GetType()
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(field => field.Name == "_internalQuery")
                    .Select(field => field.GetValue(query))
                    .First();
            var object_query =
                internal_query.GetType()
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(field => field.Name == "_objectQuery")
                    .Select(field => field.GetValue(internal_query))
                    .First() as ObjectQuery;
            var parameters = object_query.Parameters.Select(p => new SqlParameter(p.Name, p.Value)).ToArray();

            var result = db.Database.ExecuteSqlCommand(delete_sql, parameters);

            return db;
        }

        public static DbContextTransaction CommitRetaining(this DbContextTransaction transaction, BaseEntities db)
        {
            db.SaveChanges();
            if (transaction.UnderlyingTransaction.Connection != null)
            {
                transaction.Commit();
            }
            transaction =  db.Database.BeginTransaction(/*IsolationLevel.RepeatableRead*/);
            return transaction;
        }

        public static int Save(this BaseEntities db, int wb_id)
        {
            using (var _db = DB.SkladBase())
            {
                var wb = _db.WaybillList.FirstOrDefault(w => w.WbillId == wb_id);
                if (wb != null && wb.SessionId != UserSession.SessionId)
                {
                    throw new Exception("Не можливо зберегти документ, тільки перегляд.");
                }
            }

            try
            {
                return db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Get the current entity values and the values in the database
                var entry = ex.Entries.Single();
                var currentValues = entry.CurrentValues;
                var databaseValues = entry.GetDatabaseValues();

                var cv = EFAuditingHelperMethods.GetAsJson(currentValues);
                var dbv = EFAuditingHelperMethods.GetAsJson(databaseValues);

                using (var dbl = new BaseEntities())
                {
                    var dd = db.ErrorLog.Add(new ErrorLog
                    {
                        Message = ex.Message,
                        OnDate = DateTime.Now,
                        StackTrace = "currentValues: " + cv + Environment.NewLine + "databaseValues: " + dbv,
                        Title = "Обработка конфликтов параллелизма",
                        UserId = DBHelper.CurrentUser.UserId,
                    });

                    db.SaveChanges();
                }

                throw ex;
            }
        }

        public static bool IsAnyChanges(this BaseEntities context)
        {
            context.ChangeTracker.DetectChanges();

            return context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).Any();
        }

        public static void UndoAllChanges(this BaseEntities context)
        {
            //detect all changes (probably not required if AutoDetectChanges is set to true)
            context.ChangeTracker.DetectChanges();

            foreach (var dbEntityEntry in context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList())
            {
                if (dbEntityEntry.Entity == null) continue;

                switch (dbEntityEntry.State)
                {
                    case EntityState.Modified:
                        dbEntityEntry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        dbEntityEntry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        dbEntityEntry.Reload();
                        break;
                    default: break;
                }
            }

            //get all entries that are changed
            /*    var entries = context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList();

                //somehow try to discard changes on every entry
                foreach (var dbEntityEntry in entries)
                {
                    var entity = dbEntityEntry.Entity;

                    if (entity == null) continue;

                    if (dbEntityEntry.State == EntityState.Added)
                    {
                        //if entity is in Added state, remove it. (there will be problems with Set methods if entity is of proxy type, in that case you need entity base type
                        var set = context.Set(entity.GetType());
                        set.Remove(entity);
                    }
                    else if (dbEntityEntry.State == EntityState.Modified)
                    {
                        //entity is modified... you can set it to Unchanged or Reload it form Db??
                        dbEntityEntry.Reload();
                    }
                    else if (dbEntityEntry.State == EntityState.Deleted)
                        //entity is deleted... not sure what would be the right thing to do with it... set it to Modifed or Unchanged
                        dbEntityEntry.State = EntityState.Modified;
                }*/
        }

        public static DataTable Ext_ToDataTable<T>(this IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;
            FieldInfo[] oField = null;
            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = rec.GetType().GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) &&
                             (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                    oField = rec.GetType().GetFields();
                    foreach (FieldInfo fieldInfo in oField)
                    {
                        Type colType = fieldInfo.FieldType;

                        if ((colType.IsGenericType) &&
                             (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(fieldInfo.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                if (oProps != null)
                {
                    foreach (PropertyInfo pi in oProps)
                    {
                        dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                    }
                }
                if (oField != null)
                {
                    foreach (FieldInfo fieldInfo in oField)
                    {
                        dr[fieldInfo.Name] = fieldInfo.GetValue(rec) ?? DBNull.Value;
                    }
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
    }
}
