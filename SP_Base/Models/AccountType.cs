namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountType")]
    public partial class AccountType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccountType()
        {
            KAgentAccount = new HashSet<KAgentAccount>();
        }

        [Key]
        public int TypeId { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public int Def { get; set; }

        public int Deleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAgentAccount> KAgentAccount { get; set; }
    }
}
