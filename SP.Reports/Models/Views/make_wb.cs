using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Reports.Models
{
    public class make_wb
    {
        public int WbillId { get; set; }
        public string Num { get; set; }
        public DateTime OnDate { get; set; }
        public string RecipeName { get; set; }
        public decimal Amount { get; set; }
    }
}
