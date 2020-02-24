namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WayBillSvc")]
    public partial class WayBillSvc
    {
        [Key]
        public int PosId { get; set; }

        public int WbillId { get; set; }

        public int CurrId { get; set; }

        public int SvcId { get; set; }

        public int Num { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Price { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BasePrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Norm { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Nds { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Discount { get; set; }

        public int? PersonId { get; set; }

        public int? SvcToPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Total { get; set; }

        [StringLength(100)]
        public string Notes { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual Services Services { get; set; }

        public virtual WaybillList WaybillList { get; set; }
    }
}
