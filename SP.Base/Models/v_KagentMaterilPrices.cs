using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SP.Base.Models
{
    public partial class v_KagentMaterilPrices
    {
        [Key]
        [Column(Order = 0)]
        public int MatId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int KaId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<int> PTypeId { get; set; }
    }
}
