namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CONTRDET")]
    public partial class CONTRDET
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CDID { get; set; }

        public int? MATID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AMOUNT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PRICE { get; set; }

        public int? CURRID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NDS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SHIPPEDAMOUNT { get; set; }

        public int CONTRID { get; set; }

        public virtual CONTRACTS CONTRACTS { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Materials Materials { get; set; }
    }
}
