namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAXWB")]
    public partial class TAXWB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAXWB()
        {
            TAXWBDET = new HashSet<TAXWBDET>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TWBID { get; set; }

        public int KAID { get; set; }

        public DateTime ONDATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ADDCHARGES { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DISCOUNT { get; set; }

        public int DEFNUM { get; set; }

        public int DELETED { get; set; }

        public int CHECKED { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SUMMALL { get; set; }

        [StringLength(255)]
        public string CONDITION { get; set; }

        [StringLength(255)]
        public string FORM { get; set; }

        public int? PERSONID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NDS { get; set; }

        [Required]
        [StringLength(20)]
        public string NUM { get; set; }

        public int? DOCID { get; set; }

        public int? ENTID { get; set; }

        [StringLength(1)]
        public string NUM1 { get; set; }

        [StringLength(10)]
        public string NUM2 { get; set; }

        [StringLength(64)]
        public string CONTRACT_TYPE { get; set; }

        public DateTime? CONTRACT_DATE { get; set; }

        [StringLength(20)]
        public string CONTRACT_NUM { get; set; }

        public int? TAXREESTRTYPE { get; set; }

        public Guid Id { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual Kagent Kagent1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAXWBDET> TAXWBDET { get; set; }
    }
}
