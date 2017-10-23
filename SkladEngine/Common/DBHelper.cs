using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP_Base;
using SP_Base.Models;

namespace SkladEngine.Common
{
    public static class DBHelper
    {
        private static List<Currency> _currency;

        public static DateTime ServerDateTime()
        {
            return Database.SP_BaseModel().Database.SqlQuery<DateTime>("SELECT getdate()").FirstOrDefault();
        }

        public static List<Currency> Currency
        {
            get
            {
                if (_currency == null)
                {
                    using (var db = Database.SP_BaseModel())
                    {
                        _currency = db.Currency.ToList();
                    }
                }
                return _currency;
            }
        }
    }
}
