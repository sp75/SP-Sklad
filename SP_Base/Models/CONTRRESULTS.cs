namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CONTRRESULTS
    {
        [Column(TypeName = "numeric")]
        public decimal? SHIPPEDSUMM { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SHIPPEDAMOUNT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PAIDSUMM { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CONTRID { get; set; }

        public virtual CONTRACTS CONTRACTS { get; set; }
    }
}
