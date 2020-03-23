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


        public static List<Enterprise> EnterpriseList(int current_user_kaid)
        {

            using (var db = Database.SPBase())
            {
                return db.Kagent.Where(w => w.KType == 3 && w.Deleted == 0 && (w.Archived == null || w.Archived == 0))
                        .Join(db.EnterpriseWorker.Where(ew => ew.WorkerId == current_user_kaid), w => w.KaId, ew => ew.EnterpriseId, (w, ew) => new Enterprise
                        {
                            KaId = w.KaId,
                            Name = w.Name,
                            NdsPayer = w.NdsPayer
                        }).ToList();
            }
        }



        public class Enterprise
        {
            public int KaId { get; set; }
            public String Name { get; set; }
            public int NdsPayer { get; set; }
        }
    }

}
