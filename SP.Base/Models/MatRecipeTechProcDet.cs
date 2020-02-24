namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatRecipeTechProcDet")]
    public partial class MatRecipeTechProcDet
    {
        public int Id { get; set; }

        public int RecId { get; set; }

        public int ProcId { get; set; }

        public int? Num { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExpectedOut { get; set; }

        public virtual MatRecipe MatRecipe { get; set; }

        public virtual TechProcess TechProcess { get; set; }
    }
}
