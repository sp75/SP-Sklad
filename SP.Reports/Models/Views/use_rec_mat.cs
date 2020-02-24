using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Reports.Models
{
    public class use_rec_mat
    {
        public int WbillId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal? RecAmount { get; set; }
    }
}
