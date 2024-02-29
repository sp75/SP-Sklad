namespace SP.Base.Models
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
            ExtRel = new HashSet<ExtRel>();
            ExtRel1 = new HashSet<ExtRel>();
            OrderedRels = new HashSet<OrderedRels>();
            OrderedRels1 = new HashSet<OrderedRels>();
            PosRel = new HashSet<PosRel>();
            PosRel1 = new HashSet<PosRel>();
            PosRemains = new HashSet<PosRemains>();
            ReturnRel = new HashSet<ReturnRel>();
            ReturnRel1 = new HashSet<ReturnRel>();
            ReturnRel2 = new HashSet<ReturnRel>();
            ReturnRel3 = new HashSet<ReturnRel>();
            Serials = new HashSet<Serials>();
            WayBillDetTaxes = new HashSet<WayBillDetTaxes>();
            WMatTurn = new HashSet<WMatTurn>();
            WMatTurn1 = new HashSet<WMatTurn>();
            RemoteCustomerReturnedOutPosId = new HashSet<RemoteCustomerReturned>();
            RemoteCustomerReturnedPosId = new HashSet<RemoteCustomerReturned>();
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

        public virtual Currency Currency { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExtRel> ExtRel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExtRel> ExtRel1 { get; set; }

        public virtual Materials Materials { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderedRels> OrderedRels { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderedRels> OrderedRels1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PosRel> PosRel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PosRel> PosRel1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PosRemains> PosRemains { get; set; }

        public virtual PriceTypes PriceTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReturnRel> ReturnRel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReturnRel> ReturnRel1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReturnRel> ReturnRel2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReturnRel> ReturnRel3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Serials> Serials { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual WaybillList WaybillList { get; set; }

        public virtual WayBillDetAddProps WayBillDetAddProps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillDetTaxes> WayBillDetTaxes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WMatTurn> WMatTurn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WMatTurn> WMatTurn1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemoteCustomerReturned> RemoteCustomerReturnedOutPosId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemoteCustomerReturned> RemoteCustomerReturnedPosId { get; set; }
    }
}
