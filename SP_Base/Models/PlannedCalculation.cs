namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PlannedCalculation")]
    public partial class PlannedCalculation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlannedCalculation()
        {
            PlannedCalculationDet = new HashSet<PlannedCalculationDet>();
        }

        public Guid Id { get; set; }

        [StringLength(20)]
        public string Num { get; set; }

        public DateTime OnDate { get; set; }

        public int? EntId { get; set; }

        public Guid? SessionId { get; set; }

        public string Notes { get; set; }

        public int? PersonId { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public int? PlId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlannedCalculationDet> PlannedCalculationDet { get; set; }

        public virtual PriceList PriceList { get; set; }
    }
}
