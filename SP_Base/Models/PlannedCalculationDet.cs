namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlannedCalculationDet")]
    public partial class PlannedCalculationDet
    {
        public Guid Id { get; set; }

        public int RecId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProductionPlan { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PlannedProfitability { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RecipeOut { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Amount { get; set; }

        public Guid PlannedCalculationId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SalesPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RecipePrice { get; set; }

        public virtual MatRecipe MatRecipe { get; set; }

        public virtual PlannedCalculation PlannedCalculation { get; set; }
    }
}
