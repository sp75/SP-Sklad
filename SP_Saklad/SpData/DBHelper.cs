using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Saklad.SpData
{
    public static class DBHelper
    {
        public static List<WAREHOUSE> WhList()
        {
            return new BaseEntities().WAREHOUSE.Where(w => w.DELETED == 0).ToList();
        }

    }
}
