namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatGroup")]
    public partial class MatGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MatGroup()
        {
            KAMatGroupDiscount = new HashSet<KAMatGroupDiscount>();
            Materials = new HashSet<Materials>();
            MatGroupPrices = new HashSet<MatGroupPrices>();
        }

        [Key]
        public int GrpId { get; set; }

        public int PId { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public int Deleted { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Nds { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Num { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAMatGroupDiscount> KAMatGroupDiscount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Materials> Materials { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatGroupPrices> MatGroupPrices { get; set; }
    }
}
