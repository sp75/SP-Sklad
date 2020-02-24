namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MaterialMeasures
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MatId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        public virtual Materials Materials { get; set; }

        public virtual Measures Measures { get; set; }
    }
}
