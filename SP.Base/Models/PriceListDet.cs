namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceListDet")]
    public partial class PriceListDet
    {
        [Key]
        public int PlDetId { get; set; }

        public int? MatId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        public int PlId { get; set; }

        public int? GrpId { get; set; }

        public int? PlDetType { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Discount { get; set; }

        public int? Num { get; set; }

        public virtual PriceList PriceList { get; set; }
    }
}
