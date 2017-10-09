namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatRecDet")]
    public partial class MatRecDet
    {
        [Key]
        public int DetId { get; set; }

        public int RecId { get; set; }

        public int MatId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Coefficient { get; set; }

        public virtual Materials Materials { get; set; }

        public virtual MatRecipe MatRecipe { get; set; }
    }
}
