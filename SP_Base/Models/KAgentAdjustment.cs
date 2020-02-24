namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KAgentAdjustment")]
    public partial class KAgentAdjustment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KAgentAdjustment()
        {
            KAgentAdjustmentDet = new HashSet<KAgentAdjustmentDet>();
        }

        public Guid Id { get; set; }

        [StringLength(50)]
        public string Num { get; set; }

        public DateTime OnDate { get; set; }

        public int? DebtKaId { get; set; }

        public int? CreditKaId { get; set; }

        [StringLength(255)]
        public string Reason { get; set; }

        public int Checked { get; set; }

        public int Deleted { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummAll { get; set; }

        public int? PersonId { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int? CurrId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OnValue { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public Guid? SessionId { get; set; }

        public int WriteOffType { get; set; }

        public int OperationType { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummInCurr { get; set; }

        public int WType { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual Kagent Kagent1 { get; set; }

        public virtual Kagent Kagent2 { get; set; }

        public virtual OperationTypes OperationTypes { get; set; }

        public virtual WriteOffTypes WriteOffTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAgentAdjustmentDet> KAgentAdjustmentDet { get; set; }
    }
}
