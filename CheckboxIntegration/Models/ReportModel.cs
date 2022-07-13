using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
   public class ReportModel : BaseModel
    {
        public Guid id { get; set; }
        public int serial { get; set; }
        public bool is_z_report { get; set; }
        public object payments { get; set; }
        public object taxes { get; set; }
        public int sell_receipts_count { get; set; }
        public int return_receipts_count { get; set; }
        public int cash_withdrawal_receipts_count { get; set; }
        public int transfers_count { get; set; }
        public int transfers_sum { get; set; }
        public int balance { get; set; }
        public int initial { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public int? discounts_sum { get; set; }
        public int? extra_charge_sum { get; set; }
    }
}
