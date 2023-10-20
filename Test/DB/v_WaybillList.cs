namespace Test.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_WaybillList
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WbillId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Num { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime OnDate { get; set; }

        public int? CurrId { get; set; }

        [StringLength(255)]
        public string Reason { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Checked { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummAll { get; set; }

        [Key]
        [Column(Order = 4)]
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
        public string ShortName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OnValue { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int? KaId { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(64)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string PersonName { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(64)]
        public string City { get; set; }

        [StringLength(64)]
        public string District { get; set; }

        [StringLength(64)]
        public string Region { get; set; }

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
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DefNum { get; set; }

        public int? EntId { get; set; }

        [StringLength(64)]
        public string WhName { get; set; }

        [StringLength(64)]
        public string DestWhName { get; set; }

        [StringLength(10)]
        public string KAU { get; set; }

        [StringLength(100)]
        public string MovPersonName { get; set; }

        [StringLength(20)]
        public string CType { get; set; }

        [StringLength(255)]
        public string EntKaFullName { get; set; }

        [StringLength(64)]
        public string EntKaPhone { get; set; }

        [StringLength(255)]
        public string EntAddress { get; set; }

        [StringLength(64)]
        public string EntCity { get; set; }

        [StringLength(64)]
        public string EntDistrict { get; set; }

        [StringLength(64)]
        public string EntRegion { get; set; }

        [StringLength(15)]
        public string EntPostIndex { get; set; }

        [StringLength(64)]
        public string EntINN { get; set; }

        [StringLength(64)]
        public string EntCertNum { get; set; }

        [StringLength(20)]
        public string EntCType { get; set; }

        [StringLength(502)]
        public string AddressSel { get; set; }

        [StringLength(502)]
        public string AddressBuy { get; set; }

        [StringLength(64)]
        public string EntOKPO { get; set; }

        [Key]
        [Column(Order = 6)]
        public Guid Id { get; set; }

        public string Declaration { get; set; }

        [StringLength(50)]
        public string CarNumber { get; set; }

        [StringLength(50)]
        public string CarName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Balans { get; set; }

        [StringLength(100)]
        public string DriverName { get; set; }
    }
}
