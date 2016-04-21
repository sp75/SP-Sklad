using System;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
        public static void DeleteWhere<T>(this DbContext db, Expression<Func<T, bool>> filter)
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
        }

        public static DbContextTransaction CommitRetaining(this DbContextTransaction transaction, BaseEntities db)
        {
            if (transaction.UnderlyingTransaction.Connection != null)
            {
                transaction.Commit();
            }
            transaction =  db.Database.BeginTransaction(IsolationLevel.RepeatableRead);
            return transaction;
        }
    }
}
