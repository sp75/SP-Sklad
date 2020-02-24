namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WaybillList")]
    public partial class WaybillList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WaybillList()
        {
            Commission = new HashSet<Commission>();
            DeboningDet = new HashSet<DeboningDet>();
            IntermediateWeighing = new HashSet<IntermediateWeighing>();
            TechProcDet = new HashSet<TechProcDet>();
            WaybillDet = new HashSet<WaybillDet>();
            WayBillDetAddProps = new HashSet<WayBillDetAddProps>();
            WayBillTmc = new HashSet<WayBillTmc>();
            WayBillMakeProps = new HashSet<WayBillMakeProps>();
            WayBillSvc = new HashSet<WayBillSvc>();
        }

        [Key]
        public int WbillId { get; set; }

        [Required]
        [StringLength(20)]
        public string Num { get; set; }

        public DateTime OnDate { get; set; }

        public int? KaId { get; set; }

        public int? CurrId { get; set; }

        [StringLength(15)]
        public string AttNum { get; set; }

        public DateTime? AttDate { get; set; }

        [StringLength(255)]
        public string Reason { get; set; }

        public int Checked { get; set; }

        public int Deleted { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummAll { get; set; }

        public int WType { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Nds { get; set; }

        public int? PersonId { get; set; }

        [StringLength(64)]
        public string Received { get; set; }

        public DateTime? ToDate { get; set; }

        public int DefNum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummPay { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int? DocId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummInCurr { get; set; }

        public int? EntId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OnValue { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public Guid? SessionId { get; set; }

        public Guid Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commission> Commission { get; set; }

        public virtual Currency Currency { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeboningDet> DeboningDet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntermediateWeighing> IntermediateWeighing { get; set; }

        public virtual Kagent Kagent { get; set; }

        public virtual Kagent Kagent1 { get; set; }

        public virtual Kagent Kagent2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TechProcDet> TechProcDet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WaybillDet> WaybillDet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillDetAddProps> WayBillDetAddProps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillTmc> WayBillTmc { get; set; }

        public virtual WayBillMake WayBillMake { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillMakeProps> WayBillMakeProps { get; set; }

        public virtual WaybillMove WaybillMove { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillSvc> WayBillSvc { get; set; }
    }
}
