namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KADiscount")]
    public partial class KADiscount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KAId { get; set; }

        public int DiscForAll { get; set; }

        [Column(TypeName = "numeric")]
        public decimal OnValue { get; set; }

        public int DiscCustom { get; set; }

        public virtual Kagent Kagent { get; set; }
    }
}
