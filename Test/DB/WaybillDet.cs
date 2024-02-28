namespace Test.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WaybillDet")]
    public partial class WaybillDet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WaybillDet()
        {
            RemoteCustomerReturned = new HashSet<RemoteCustomerReturned>();
        }

        [Key]
        public int PosId { get; set; }

        public int WbillId { get; set; }

        public int MatId { get; set; }

        public int? WId { get; set; }

        [Column(TypeName = "numeric")]
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

        public int? Defective { get; set; }

        public virtual Materials Materials { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemoteCustomerReturned> RemoteCustomerReturned { get; set; }

        public virtual WaybillList WaybillList { get; set; }
    }
}
