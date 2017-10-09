namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DiscCards
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DiscCards()
        {
            WayBillDetAddProps = new HashSet<WayBillDetAddProps>();
        }

        [Key]
        public int CardId { get; set; }

        [Required]
        [StringLength(50)]
        public string Num { get; set; }

        public int DiscType { get; set; }

        public DateTime? ExpireDate { get; set; }

        public int GrpId { get; set; }

        public int? KaId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal OnValue { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int Deleted { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StartSaldo { get; set; }

        public virtual DiscCardGrp DiscCardGrp { get; set; }

        public virtual Kagent Kagent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillDetAddProps> WayBillDetAddProps { get; set; }
    }
}
