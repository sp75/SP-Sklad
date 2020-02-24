namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductionPlans
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductionPlans()
        {
            ProductionPlanDet = new HashSet<ProductionPlanDet>();
        }

        public Guid Id { get; set; }

        [StringLength(20)]
        public string Num { get; set; }

        public DateTime OnDate { get; set; }

        public int Checked { get; set; }

        public int? EntId { get; set; }

        public int? PersonId { get; set; }

        public string Notes { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public Guid? SessionId { get; set; }

        public int Deleted { get; set; }

        public int? WhId { get; set; }

        public int? ManufId { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual Kagent Kagent1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionPlanDet> ProductionPlanDet { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual Warehouse Warehouse1 { get; set; }
    }
}
