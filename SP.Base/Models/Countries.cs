namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Countries
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Countries()
        {
            Materials = new HashSet<Materials>();
        }

        [Key]
        public int CId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(12)]
        public string ShortName { get; set; }

        public int Deleted { get; set; }

        public int? Def { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Materials> Materials { get; set; }
    }
}
