namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CONTRPARAMS
    {
        public DateTime TODATE { get; set; }

        public int? ISMANUALSUMM { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SUMM { get; set; }

        public int? ISMANUALAMOUNT { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AMOUNT { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CONTRID { get; set; }

        public virtual CONTRACTS CONTRACTS { get; set; }
    }
}
