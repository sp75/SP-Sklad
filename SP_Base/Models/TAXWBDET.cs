namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAXWBDET")]
    public partial class TAXWBDET
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TWBDETID { get; set; }

        public int TWBID { get; set; }

        public int MATID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal AMOUNT { get; set; }

        public DateTime ONDATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal PRICE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NDS { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TOTAL { get; set; }

        public virtual Materials Materials { get; set; }

        public virtual TAXWB TAXWB { get; set; }
    }
}
