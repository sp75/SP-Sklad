using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class REP_15_Result
    {
        [Key]
        public Nullable<System.DateTime> OnDate { get; set; }
        public Nullable<decimal> AmountOut { get; set; }
        public Nullable<decimal> SummOut { get; set; }
        public Nullable<decimal> Income { get; set; }
        public Nullable<decimal> ReturnSumm { get; set; }
        public Nullable<decimal> ReturnAmount { get; set; }
        public Nullable<decimal> OAOnDate { get; set; }
    }
}
