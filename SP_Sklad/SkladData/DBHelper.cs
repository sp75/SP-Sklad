using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Sklad.SkladData
{
    public static class DBHelper
    {
        public static List<WAREHOUSE> WhList()
        {
            return new BaseEntities().WAREHOUSE.Where(w => w.DELETED == 0).ToList();
        }

        public static DateTime ServerDateTime()
        {
            return  new BaseEntities().Database.SqlQuery<DateTime>("SELECT getdate()").FirstOrDefault();
        }

    }
}
