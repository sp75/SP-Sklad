namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Services
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Services()
        {
            WayBillSvc = new HashSet<WayBillSvc>();
        }

        [Key]
        public int SvcId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(80)]
        public string Artikul { get; set; }

        public int GrpId { get; set; }

        public int IsTransport { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        public int? CurrId { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int Deleted { get; set; }

        public int IsNormed { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Norm { get; set; }

        public int? MId { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Measures Measures { get; set; }

        public virtual SvcGroup SvcGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillSvc> WayBillSvc { get; set; }
    }
}
