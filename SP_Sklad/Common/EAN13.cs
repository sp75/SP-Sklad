using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Sklad.Common
{
    public class EAN13
    {
        public EAN13(string bar_code)
        {
            day = Convert.ToInt32(bar_code.Substring(0, 2));
            mounth = Convert.ToInt32(bar_code.Substring(2, 2));
            artikul = bar_code.Substring(4, 3);
            amount = Convert.ToInt32(bar_code.Substring(7, 5)) / 1000.00m;
        }

        public int day { get; set; }
        public int mounth { get; set; }
        public string artikul { get; set; }
        public decimal amount { get; set; }
    }
}
