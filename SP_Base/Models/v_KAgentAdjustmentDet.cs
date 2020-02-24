namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_KAgentAdjustmentDet
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        public int? WType { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string Num { get; set; }

        public DateTime? OnDate { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummAll { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummInCurr { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummPay { get; set; }

        public Guid? KAgentAdjustmentId { get; set; }

        public Guid? DocId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OnValue { get; set; }

        [StringLength(3)]
        public string CurrName { get; set; }

        [StringLength(50)]
        public string DocShortName { get; set; }

        [StringLength(64)]
        public string DocTypeName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Saldo { get; set; }

        public int? Idx { get; set; }
    }
}
