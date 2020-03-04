namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceList")]
    public partial class PriceList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PriceList()
        {
            PlannedCalculation = new HashSet<PlannedCalculation>();
            PriceListDet = new HashSet<PriceListDet>();
            Kagent = new HashSet<Kagent>();
        }

        [Key]
        public int PlId { get; set; }

        public int CurrId { get; set; }

        public DateTime OnDate { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int Deleted { get; set; }

        public int UseLogo { get; set; }

        public int? DocId { get; set; }

        public int? PTypeId { get; set; }

        public Guid Id { get; set; }

        public virtual Currency Currency { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlannedCalculation> PlannedCalculation { get; set; }

        public virtual PriceTypes PriceTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceListDet> PriceListDet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kagent> Kagent { get; set; }
    }
}
