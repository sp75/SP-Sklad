namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_KAgentDocs
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        public int? WType { get; set; }

        [StringLength(50)]
        public string Num { get; set; }

        public DateTime? OnDate { get; set; }

        public int? KaId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Checked { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Nds { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummAll { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummInCurr { get; set; }

        [StringLength(3)]
        public string ShortName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OnValue { get; set; }

        public int? CurrId { get; set; }

        public DateTime? ToDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummPay { get; set; }

        [StringLength(50)]
        public string DocShortName { get; set; }

        [StringLength(64)]
        public string DocTypeName { get; set; }
    }
}
