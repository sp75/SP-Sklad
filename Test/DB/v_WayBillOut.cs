namespace Test.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_WayBillOut
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

        [Column(TypeName = "numeric")]
        public decimal? Nds { get; set; }

        public int? PersonId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummPay { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int? DocId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummInCurr { get; set; }

        [StringLength(3)]
        public string CurrName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CurrRate { get; set; }

        [StringLength(100)]
        public string KaName { get; set; }

        public int? KaId { get; set; }

        [StringLength(255)]
        public string KaFullName { get; set; }

        [StringLength(64)]
        public string KaPhone { get; set; }

        [StringLength(100)]
        public string PersonName { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(64)]
        public string City { get; set; }

        [StringLength(64)]
        public string District { get; set; }

        [StringLength(64)]
        public string Country { get; set; }

        [StringLength(15)]
        public string PostIndex { get; set; }

        [StringLength(32)]
        public string Fax { get; set; }

        [StringLength(64)]
        public string Email { get; set; }

        [StringLength(64)]
        public string www { get; set; }

        public int? KType { get; set; }

        [StringLength(15)]
        public string AttNum { get; set; }

        public DateTime? AttDate { get; set; }

        [StringLength(64)]
        public string Received { get; set; }

        public DateTime? ToDate { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DefNum { get; set; }

        public int? EntId { get; set; }

        [StringLength(100)]
        public string EntName { get; set; }

        [StringLength(50)]
        public string KagentGroupName { get; set; }

        [StringLength(64)]
        public string DocType { get; set; }

        public int? PTypeId { get; set; }

        [StringLength(50)]
        public string PTypeName { get; set; }

        [StringLength(50)]
        public string RouteName { get; set; }

        [StringLength(103)]
        public string CarName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DefTotalAmount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExTotalAmount { get; set; }

        public DateTime? ShipmentDate { get; set; }

        public DateTime? ReportingDate { get; set; }

        public Guid? ReceiptId { get; set; }

        [StringLength(64)]
        public string SourceWhName { get; set; }

        [StringLength(64)]
        public string DestinationWhName { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [StringLength(50)]
        public string UpdatedUserName { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Expedition { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Balans { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TotalAmount { get; set; }

        public Guid? KagentId { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsDelivered { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InTransit { get; set; }
    }
}
