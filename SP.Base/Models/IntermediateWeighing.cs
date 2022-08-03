namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IntermediateWeighing")]
    public partial class IntermediateWeighing
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IntermediateWeighing()
        {
            IntermediateWeighingDet = new HashSet<IntermediateWeighingDet>();
        }

        public Guid Id { get; set; }

        [StringLength(50)]
        public string Num { get; set; }

        public DateTime OnDate { get; set; }

        public int Checked { get; set; }

        [StringLength(256)]
        public string Notes { get; set; }

        public int PersonId { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public Guid? SessionId { get; set; }

        public int WbillId { get; set; }
        public decimal? Amount { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual WaybillList WaybillList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntermediateWeighingDet> IntermediateWeighingDet { get; set; }
    }
}
