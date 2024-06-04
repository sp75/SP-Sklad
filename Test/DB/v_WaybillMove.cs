namespace Test.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_WaybillMove
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WbillId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Num { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime OnDate { get; set; }

        public int? CurrId { get; set; }

        [StringLength(255)]
        public string Reason { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Checked { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummAll { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WType { get; set; }

        public int? PersonId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummPay { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummInCurr { get; set; }

        [StringLength(100)]
        public string PersonName { get; set; }

        public int? EntId { get; set; }

        [StringLength(100)]
        public string EntName { get; set; }

        [StringLength(64)]
        public string DocTypeName { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [StringLength(50)]
        public string UpdatedUserName { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Deleted { get; set; }

        [StringLength(15)]
        public string AttNum { get; set; }

        public DateTime? AttDate { get; set; }

        public Guid? SessionId { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DefTotalAmount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExTotalAmount { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FromWId { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(64)]
        public string FromWh { get; set; }

        public int? ToWId { get; set; }

        [StringLength(64)]
        public string ToWh { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkerId { get; set; }
    }
}
