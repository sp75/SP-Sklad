namespace Test.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_PayDoc
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PayDocId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocType { get; set; }

        public int? ExDocType { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime OnDate { get; set; }

        public int? KaId { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
        public decimal Total { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PTypeId { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CurrId { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Deleted { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(20)]
        public string DocNum { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Checked { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WithNDS { get; set; }

        [StringLength(255)]
        public string Reason { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int? MPersonId { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CTypeId { get; set; }

        public int? AccId { get; set; }

        public int? CashId { get; set; }

        public Guid? OperId { get; set; }

        public int? DocId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OnValue { get; set; }

        [StringLength(10)]
        public string Schet { get; set; }

        [StringLength(3)]
        public string CurrName { get; set; }

        [StringLength(100)]
        public string KaName { get; set; }

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

        [StringLength(128)]
        public string ChargeName { get; set; }

        [StringLength(50)]
        public string PayTypeName { get; set; }

        [StringLength(128)]
        public string CashdName { get; set; }

        [StringLength(64)]
        public string AccNum { get; set; }

        [StringLength(128)]
        public string BankName { get; set; }

        [StringLength(10)]
        public string KAU { get; set; }

        [Key]
        [Column(Order = 11)]
        public Guid Id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummInCurr { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ActualSummInCurr { get; set; }

        [StringLength(64)]
        public string KaAccNum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BankCommission { get; set; }

        public int? EntId { get; set; }

        [StringLength(100)]
        public string EntName { get; set; }

        [StringLength(50)]
        public string KaGroupName { get; set; }

        [StringLength(64)]
        public string DocTypeName { get; set; }

        [StringLength(128)]
        public string SourceType { get; set; }

        public DateTime? ReportingDate { get; set; }

        public Guid? KagentId { get; set; }
    }
}
