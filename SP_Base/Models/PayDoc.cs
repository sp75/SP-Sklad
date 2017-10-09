namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PayDoc")]
    public partial class PayDoc
    {
        public int PayDocId { get; set; }

        public int DocType { get; set; }

        public DateTime OnDate { get; set; }

        public int? KaId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Total { get; set; }

        public int PTypeId { get; set; }

        public int CurrId { get; set; }

        public int Deleted { get; set; }

        [Required]
        [StringLength(20)]
        public string DocNum { get; set; }

        public int Checked { get; set; }

        public int WithNDS { get; set; }

        [StringLength(255)]
        public string Reason { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int? MPersonId { get; set; }

        public int CTypeId { get; set; }

        public int? AccId { get; set; }

        public int? CashId { get; set; }

        public int? OperId { get; set; }

        public int? DocId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OnValue { get; set; }

        [StringLength(10)]
        public string Schet { get; set; }

        public int? UpdatedBy { get; set; }

        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? ExDocType { get; set; }

        public int? EntId { get; set; }

        public virtual CashDesks CashDesks { get; set; }

        public virtual ChargeType ChargeType { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual Kagent Kagent1 { get; set; }

        public virtual KAgentAccount KAgentAccount { get; set; }

        public virtual PayType PayType { get; set; }
    }
}
