namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KAgentActReconciliation")]
    public partial class KAgentActReconciliation
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Num { get; set; }

        public DateTime OnDate { get; set; }

        public int WType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDdate { get; set; }

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

        [Column(TypeName = "numeric")]
        public decimal? SummInCurr { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual Kagent Kagent1 { get; set; }

        public virtual Kagent Kagent2 { get; set; }
    }
}
