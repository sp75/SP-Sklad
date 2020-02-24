namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChargeType")]
    public partial class ChargeType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChargeType()
        {
            PayDoc = new HashSet<PayDoc>();
        }

        [Key]
        public int CTypeId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public int Deleted { get; set; }

        public int Def { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayDoc> PayDoc { get; set; }
    }
}
