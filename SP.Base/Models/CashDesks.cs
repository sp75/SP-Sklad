namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CashDesks
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CashDesks()
        {
            MoneySaldo = new HashSet<MoneySaldo>();
            PayDoc = new HashSet<PayDoc>();
            UserAccessCashDesks = new HashSet<UserAccessCashDesks>();
        }

        [Key]
        public int CashId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public int Deleted { get; set; }

        public int? Def { get; set; }

        public int? EnterpriseId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MoneySaldo> MoneySaldo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayDoc> PayDoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAccessCashDesks> UserAccessCashDesks { get; set; }
    }
}
