namespace Test.DB
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
            RemoteCustomerReturned = new HashSet<RemoteCustomerReturned>();
            WaybillDet = new HashSet<WaybillDet>();
        }

        [Key]
        public int WbillId { get; set; }

        [Required]
        [StringLength(50)]
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

        [StringLength(128)]
        public string WebUserId { get; set; }

        public int? PTypeId { get; set; }

        public Guid? CarId { get; set; }

        public int? RouteId { get; set; }

        public int? CustomerId { get; set; }

        public int? DriverId { get; set; }

        public DateTime? ReportingDate { get; set; }

        public DateTime? ShipmentDate { get; set; }

        public Guid? ReceiptId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RemoteCustomerReturned> RemoteCustomerReturned { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WaybillDet> WaybillDet { get; set; }
    }
}
