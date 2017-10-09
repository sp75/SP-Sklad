namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Banks
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Banks()
        {
            KAgentAccount = new HashSet<KAgentAccount>();
            BanksPersons = new HashSet<BanksPersons>();
        }

        [Key]
        public int BankId { get; set; }

        [Required]
        [StringLength(50)]
        public string MFO { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(64)]
        public string www { get; set; }

        [StringLength(64)]
        public string CorAcc { get; set; }

        public int Def { get; set; }

        public int Deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAgentAccount> KAgentAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BanksPersons> BanksPersons { get; set; }
    }
}
