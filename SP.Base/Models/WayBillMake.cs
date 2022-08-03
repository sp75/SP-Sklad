namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WayBillMake")]
    public partial class WayBillMake
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WbillId { get; set; }

        public int SourceWId { get; set; }

        public int? RecId { get; set; }

        public int? PersonId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        public decimal? AmountByRecipe { get; set; }
        public int? RecipeCount { get; set; }
        public DateTime? EndProductionDate { get; set; }
        public decimal? ShippedAmount { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual MatRecipe MatRecipe { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual WaybillList WaybillList { get; set; }
    }
}
