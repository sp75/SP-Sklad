namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TechProcess")]
    public partial class TechProcess
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TechProcess()
        {
            MatRecipeTechProcDet = new HashSet<MatRecipeTechProcDet>();
            TechProcDet = new HashSet<TechProcDet>();
        }

        [Key]
        public int ProcId { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Kod { get; set; }

        public int? Num { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatRecipeTechProcDet> MatRecipeTechProcDet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TechProcDet> TechProcDet { get; set; }
    }
}
