namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CURRENCYRATE")]
    public partial class CURRENCYRATE
    {
        public int CURRID { get; set; }

        public DateTime ONDATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ONVALUE { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RATEID { get; set; }

        public virtual Currency Currency { get; set; }
    }
}
