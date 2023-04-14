using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class GetDiscount_Result
    {

        public Nullable<decimal> Discount { get; set; }
        [Key]
        public Nullable<int> DiscountType { get; set; }
    }
}
