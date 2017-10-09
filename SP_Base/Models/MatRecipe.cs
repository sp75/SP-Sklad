namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatRecipe")]
    public partial class MatRecipe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MatRecipe()
        {
            MatRecDet = new HashSet<MatRecDet>();
            MatRecipeTechProcDet = new HashSet<MatRecipeTechProcDet>();
            ProductionPlanDet = new HashSet<ProductionPlanDet>();
            WayBillMake = new HashSet<WayBillMake>();
        }

        [Key]
        public int RecId { get; set; }

        [StringLength(20)]
        public string Num { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public DateTime? OnDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int MatId { get; set; }

        public int? RType { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Out { get; set; }

        public virtual Materials Materials { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatRecDet> MatRecDet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatRecipeTechProcDet> MatRecipeTechProcDet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionPlanDet> ProductionPlanDet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillMake> WayBillMake { get; set; }
    }
}
