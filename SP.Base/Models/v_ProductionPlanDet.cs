namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_ProductionPlanDet
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Remain { get; set; }

        public Guid? ProductionPlanId { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal Total { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WhId { get; set; }

        [StringLength(255)]
        public string MatName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(64)]
        public string WhName { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(6)]
        public string MsrName { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecId { get; set; }

        public int? Num { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal RecipeAmount { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal ResipeOut { get; set; }
    }
}
