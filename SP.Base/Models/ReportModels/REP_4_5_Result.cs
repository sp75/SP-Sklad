using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class REP_4_5_Result
    {
        [Key]
        public Nullable<long> N { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Saldo { get; set; }
    }
}
