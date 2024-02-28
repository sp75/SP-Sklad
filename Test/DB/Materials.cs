namespace Test.DB
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
            RemoteCustomerReturned = new HashSet<RemoteCustomerReturned>();
            WaybillDet = new HashSet<WaybillDet>();
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

        public int? RawMaterialTypeId { get; set; }

        public int? UpdatedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemoteCustomerReturned> RemoteCustomerReturned { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WaybillDet> WaybillDet { get; set; }
    }
}
