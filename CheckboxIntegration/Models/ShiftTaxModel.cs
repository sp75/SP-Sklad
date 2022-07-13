using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
   public class ShiftTaxModel
    {
        public Guid id { get; set; }
        public int code { get; set; }
        public string label { get; set; }
        public string symbol { get; set; }
        public int rate { get; set; }
        public int? extra_rate { get; set; }
        public bool included { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public bool? no_vat { get; set; }
        public int sales { get; set; }
        public int returns { get; set; }
        public int sales_turnover { get; set; }
        public int returns_turnover { get; set; }
     }
}
