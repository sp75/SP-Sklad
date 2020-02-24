namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductionPlanDet")]
    public partial class ProductionPlanDet
    {
        public Guid Id { get; set; }

        public int? Num { get; set; }

        public int RecId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Amount { get; set; }

        public Guid? ProductionPlanId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Remain { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Total { get; set; }

        public int WhId { get; set; }

        public virtual MatRecipe MatRecipe { get; set; }

        public virtual ProductionPlans ProductionPlans { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}
