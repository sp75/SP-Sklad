using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
    public class CashRegisterModel
    {
        public Guid id { get; set; }
        public string fiscal_number { get; set; }
        public bool active { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
