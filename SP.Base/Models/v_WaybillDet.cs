namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_WaybillDet
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PosId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WbillId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MatId { get; set; }

        public int? WId { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "numeric")]
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

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Num { get; set; }

        public int? Checked { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OnValue { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Total { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BasePrice { get; set; }

        public DateTime? Expires { get; set; }

        public int? PosKind { get; set; }

        public int? PosParent { get; set; }

        public int? MsrUnitId { get; set; }

        public int? DiscountKind { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AvgInPrice { get; set; }

        [StringLength(100)]
        public string Notes { get; set; }

        public DateTime? UpdateAt { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(64)]
        public string GrpName { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(6)]
        public string MsrName { get; set; }

        [StringLength(255)]
        public string MatName { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GrpId { get; set; }

        public byte[] BMP { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(100)]
        public string KaName { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(50)]
        public string WbNum { get; set; }

        [Key]
        [Column(Order = 10)]
        public DateTime WbOnDate { get; set; }
        public int WType { get; set; }
        public Guid KagentId { get; set; }
        public int WbChecked { get; set; }
        public string Artikul { get; set; }
        public string WbNotes { get; set; }
        public string WhName { get; set; }
        public string PersonName { get; set; }
        public int? Defective { get; set; }
        public string KagentGroupName { get; set; }
        public string DocTypeName { get; set; }
    }
}
