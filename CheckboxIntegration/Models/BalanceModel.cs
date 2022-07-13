using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
    public class BalanceModel
    {
        public int initial { get; set; }
        public int balance { get; set; }
        public int cash_sales { get; set; }
        public int card_sales { get; set; }
        public int? discounts_sum { get; set; }
        public int? extra_charge_sum { get; set; }
        public int cash_returns { get; set; }
        public int card_returns { get; set; }
        public int service_in { get; set; }
        public int service_out { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
