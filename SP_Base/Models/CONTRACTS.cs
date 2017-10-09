namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CONTRACTS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CONTRACTS()
        {
            CONTRDET = new HashSet<CONTRDET>();
        }

        [Key]
        public int CONTRID { get; set; }

        public int? KAID { get; set; }

        public DateTime ONDATE { get; set; }

        [Required]
        [StringLength(20)]
        public string NUM { get; set; }

        public int CHECKED { get; set; }

        public int DELETED { get; set; }

        [StringLength(255)]
        public string FPATH { get; set; }

        public int DOCID { get; set; }

        [StringLength(255)]
        public string NOTES { get; set; }

        public int? CURRID { get; set; }

        public int? PDOCID { get; set; }

        public int ISEXTERNAL { get; set; }

        public int? PERSONID { get; set; }

        public int DOCTYPE { get; set; }

        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? ExDocType { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Kagent Kagent1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRDET> CONTRDET { get; set; }

        public virtual CONTRPARAMS CONTRPARAMS { get; set; }

        public virtual CONTRRESULTS CONTRRESULTS { get; set; }
    }
}
