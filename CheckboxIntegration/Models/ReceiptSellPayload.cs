using CheckboxIntegration.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
    public class ReceiptSellPayload 
    {
        public Guid id { get; set; }
        public string cashier_name { get; set; }
        public string departament { get; set; }
        public List<Goods> goods { get; set; }
        public DeliveryPayload delivery { get; set; }
        public List<DiscountPayload> discounts { get; set; }
        public List<Payment> payments { get; set; }
        public bool rounding { get; set; }
        public string header { get; set; }
        public string footer { get; set; }
        public string barcode { get; set; }
        public Guid? order_id { get; set; }
        public Guid? related_receipt_id { get; set; }
        public Guid? previous_receipt_id { get; set; }
        public bool technical_return { get; set; }
        public object context { get; set; }
        public bool is_pawnshop { get; set; }
        public object custom { get; set; }
    }
}
