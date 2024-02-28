namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Materials
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Materials()
        {
            CONTRDET = new HashSet<CONTRDET>();
            DeboningDet = new HashSet<DeboningDet>();
            IntermediateWeighingDet = new HashSet<IntermediateWeighingDet>();
            KAMatDiscount = new HashSet<KAMatDiscount>();
            MatChange = new HashSet<MatChange>();
            MatChange1 = new HashSet<MatChange>();
            MaterialMeasures = new HashSet<MaterialMeasures>();
            MatRemains = new HashSet<MatRemains>();
            PosRemains = new HashSet<PosRemains>();
            WMatTurn = new HashSet<WMatTurn>();
            MatRecDet = new HashSet<MatRecDet>();
            MatRecipe = new HashSet<MatRecipe>();
            WayBillTmc = new HashSet<WayBillTmc>();
            MatPrices = new HashSet<MatPrices>();
            TAXWBDET = new HashSet<TAXWBDET>();
            WaybillDet = new HashSet<WaybillDet>();
            WayBillMakeProps = new HashSet<WayBillMakeProps>();
            RemoteCustomerReturned = new HashSet<RemoteCustomerReturned>();
        }

        [Key]
        public int MatId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public int MId { get; set; }

        public int? Num { get; set; }

        public int Def { get; set; }

        public int Deleted { get; set; }

        public int? GrpId { get; set; }

        public int? WId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MinReserv { get; set; }

        public int? CId { get; set; }

        public int? DemandCat { get; set; }

        [StringLength(64)]
        public string BarCode { get; set; }

        [StringLength(255)]
        public string Producer { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Weight { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MSize { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NDS { get; set; }

        public int? Serials { get; set; }

        public int? Archived { get; set; }

        [StringLength(255)]
        public string Artikul { get; set; }

        [StringLength(255)]
        public string LabelDescr { get; set; }

        public int? TypeId { get; set; }

        public DateTime? DateAdded { get; set; }

        public DateTime? DateModified { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [StringLength(50)]
        public string CF1 { get; set; }

        [StringLength(50)]
        public string CF2 { get; set; }

        [StringLength(128)]
        public string CF3 { get; set; }

        [StringLength(128)]
        public string CF4 { get; set; }

        [StringLength(128)]
        public string CF5 { get; set; }

        public byte[] BMP { get; set; }

        public int DecPlaces { get; set; }

        [StringLength(50)]
        public string InvNumber { get; set; }

        [StringLength(50)]
        public string SerialNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRDET> CONTRDET { get; set; }

        public virtual Countries Countries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeboningDet> DeboningDet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntermediateWeighingDet> IntermediateWeighingDet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAMatDiscount> KAMatDiscount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatChange> MatChange { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatChange> MatChange1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaterialMeasures> MaterialMeasures { get; set; }

        public virtual MatGroup MatGroup { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual Warehouse Warehouse1 { get; set; }

        public virtual Warehouse Warehouse2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatRemains> MatRemains { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PosRemains> PosRemains { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WMatTurn> WMatTurn { get; set; }

        public virtual Measures Measures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatRecDet> MatRecDet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatRecipe> MatRecipe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillTmc> WayBillTmc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatPrices> MatPrices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAXWBDET> TAXWBDET { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WaybillDet> WaybillDet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillMakeProps> WayBillMakeProps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemoteCustomerReturned> RemoteCustomerReturned { get; set; }
    }
}
