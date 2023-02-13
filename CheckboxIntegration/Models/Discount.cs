using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
    public class Discount
    {
        public DiscountTypes type { get; set; }
        public DiscountModes mode { get; set; }
        public decimal value { get; set; }
    }
}
