namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KAMatGroupDiscount")]
    public partial class KAMatGroupDiscount
    {
        [Key]
        public Guid DiscId { get; set; }

        public int KAId { get; set; }

        public int GrpId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal OnValue { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual MatGroup MatGroup { get; set; }
    }
}
