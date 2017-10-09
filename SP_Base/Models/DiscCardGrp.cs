namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DiscCardGrp")]
    public partial class DiscCardGrp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiscCardGrp()
        {
            DiscCards = new HashSet<DiscCards>();
        }

        [Key]
        public int GrpId { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public int PId { get; set; }

        public int? DiscType { get; set; }

        public DateTime? ExpireDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OnValue { get; set; }

        public int? AutoNum { get; set; }

        [StringLength(50)]
        public string Prefix { get; set; }

        public int? CurrNum { get; set; }

        [StringLength(50)]
        public string Suffix { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiscCards> DiscCards { get; set; }
    }
}
