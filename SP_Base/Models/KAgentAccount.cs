namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KAgentAccount")]
    public partial class KAgentAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KAgentAccount()
        {
            MoneySaldo = new HashSet<MoneySaldo>();
            PayDoc = new HashSet<PayDoc>();
        }

        [Key]
        public int AccId { get; set; }

        public int KAId { get; set; }

        public int TypeId { get; set; }

        public int BankId { get; set; }

        [Required]
        [StringLength(64)]
        public string AccNum { get; set; }

        public int? Def { get; set; }

        public virtual AccountType AccountType { get; set; }

        public virtual Banks Banks { get; set; }

        public virtual Kagent Kagent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MoneySaldo> MoneySaldo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayDoc> PayDoc { get; set; }
    }
}
