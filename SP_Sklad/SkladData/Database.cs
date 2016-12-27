using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SP_Sklad.Common;

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

            db.Database.ExecuteSqlCommand(delete_sql, parameters);

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

            return db.SaveChanges();
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
