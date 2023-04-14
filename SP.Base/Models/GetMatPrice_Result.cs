using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class GetMatPrice_Result
    {
        [Key]
        public Nullable<int> PType { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> CurrId { get; set; }
        public string CurrName { get; set; }
    }
}
