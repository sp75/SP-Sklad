namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KAMatDiscount")]
    public partial class KAMatDiscount
    {
        [Key]
        public Guid DiscId { get; set; }

        public int KAId { get; set; }

        public int MatId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal OnValue { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual Materials Materials { get; set; }
    }
}
