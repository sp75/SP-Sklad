using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SP.Base.Models
{
    public partial class v_WayBillReturnСustomerDet
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Num { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PosId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WbillId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MatId { get; set; }

        public int? Wid { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Discount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Nds { get; set; }

        public int? CurrId { get; set; }

        public DateTime? OnDate { get; set; }

        public int? PtypeId { get; set; }

        public int? Checked { get; set; }

        [StringLength(255)]
        public string MatName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(6)]
        public string MsrName { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(64)]
        public string WhName { get; set; }

        [StringLength(255)]
        public string Artikul { get; set; }

        [StringLength(3)]
        public string CurrName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OnValue { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PosType { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Norm { get; set; }

        [StringLength(255)]
        public string Producer { get; set; }

        [StringLength(64)]
        public string GTD { get; set; }

        [StringLength(64)]
        public string CertNum { get; set; }

        public DateTime? CertDate { get; set; }

        public int? Serials { get; set; }

        [StringLength(64)]
        public string BarCode { get; set; }

        public int? GrpId { get; set; }

        [StringLength(128)]
        public string Country { get; set; }

        public int? Archived { get; set; }

        [StringLength(64)]
        public string SerialNo { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FullPrice { get; set; }

        public int? SvcToPrice { get; set; }

        public int? WbMaked { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Total { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BasePrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SumNds { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(64)]
        public string GrpName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DiscountPrice { get; set; }

        [StringLength(100)]
        public string Notes { get; set; }

        public DateTime? UpdateAt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TotalInCurrency { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MId { get; set; }
    }
}
