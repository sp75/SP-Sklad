using SP.Base;
using SP.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkladEngine.Common
{
    public static class DBHelper
    {
        private static List<Currency> _currency;
        private static List<KagentList> _kagents;

        public static DateTime ServerDateTime()
        {
            return Database.SPBase().Database.SqlQuery<DateTime>("SELECT getdate()").FirstOrDefault();
        }

        public static List<Currency> Currency
        {
            get
            {
                if (_currency == null)
                {
                    using (var db = Database.SPBase())
                    {
                        _currency = db.Currency.ToList();
                    }
                }
                return _currency;
            }
        }
     }
   
}
