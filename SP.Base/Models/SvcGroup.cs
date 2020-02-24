namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SvcGroup")]
    public partial class SvcGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SvcGroup()
        {
            Services = new HashSet<Services>();
        }

        [Key]
        public int GrpId { get; set; }

        public int PId { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public int Deleted { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int? Num { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Services> Services { get; set; }
    }
}
