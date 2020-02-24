namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_KAgentAdjustment
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Num { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Checked { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummAll { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummInCurr { get; set; }

        [StringLength(100)]
        public string DebtKaName { get; set; }

        [StringLength(100)]
        public string CreditKaName { get; set; }

        [StringLength(100)]
        public string PersonName { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime OnDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(3)]
        public string CurrName { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WType { get; set; }

        [StringLength(50)]
        public string WriteOffTypeName { get; set; }

        [StringLength(50)]
        public string OperationTypeName { get; set; }
    }
}
