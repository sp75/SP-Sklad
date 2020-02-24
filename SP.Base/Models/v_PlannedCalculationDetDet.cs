namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_PlannedCalculationDetDet
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [StringLength(255)]
        public string RecipeName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string MsrName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ProductionPlan { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PlannedProfitability { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RecipeOut { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Amount { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid PlannedCalculationId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RecipeCount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? RecipePrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SalesPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Profitability { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PlansPrice { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MatId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(64)]
        public string MatGroupName { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GrpId { get; set; }
    }
}
