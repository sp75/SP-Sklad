namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PriceTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PriceTypes()
        {
            MatGroupPrices = new HashSet<MatGroupPrices>();
            MatGroupPrices1 = new HashSet<MatGroupPrices>();
            MatPrices = new HashSet<MatPrices>();
            MatPrices1 = new HashSet<MatPrices>();
            PriceList = new HashSet<PriceList>();
            WaybillDet = new HashSet<WaybillDet>();
        }

        [Key]
        public int PTypeId { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Column(TypeName = "numeric")]
        public decimal OnValue { get; set; }

        public int Num { get; set; }

        public int Def { get; set; }

        public int Deleted { get; set; }

        public int? PPTypeId { get; set; }

        public int? ExtraType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatGroupPrices> MatGroupPrices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatGroupPrices> MatGroupPrices1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatPrices> MatPrices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatPrices> MatPrices1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceList> PriceList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WaybillDet> WaybillDet { get; set; }
    }
}
