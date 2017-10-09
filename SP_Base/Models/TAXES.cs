namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TAXES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAXES()
        {
            WayBillDetTaxes = new HashSet<WayBillDetTaxes>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TAXID { get; set; }

        [Required]
        [StringLength(128)]
        public string NAME { get; set; }

        [Column(TypeName = "numeric")]
        public decimal ONVALUE { get; set; }

        public int TAXTYPE { get; set; }

        public int DELETED { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillDetTaxes> WayBillDetTaxes { get; set; }
    }
}
