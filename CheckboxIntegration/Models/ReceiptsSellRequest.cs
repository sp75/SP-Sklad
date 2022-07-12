using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
    public class Delivery
    {
        public string email { get; set; }
        public string phone { get; set; }
    }

    public class ReceiptsSellRequest
    {
        public Guid id { get; set; }
        public string cashier_name { get; set; }
        public string departament { get; set; }
        public List<Good> goods { get; set; }
        public Delivery delivery { get; set; }
        public List<object> discounts { get; set; }
        public List<Payment> payments { get; set; }
        public bool rounding { get; set; }
        public bool technical_return { get; set; }
        public bool is_pawnshop { get; set; }
        public string barcode { get; set; }
    }


}
