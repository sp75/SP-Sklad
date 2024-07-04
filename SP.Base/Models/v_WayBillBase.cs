using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SP.Base.Models
{
    public partial class v_WayBillBase
    {

        public Guid Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WbillId { get; set; }

        public string Num { get; set; }

        public DateTime OnDate { get; set; }

        public int? CurrId { get; set; }

        [StringLength(255)]
        public string Reason { get; set; }

        public int Checked { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummAll { get; set; }

        public int WType { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Nds { get; set; }

        public int? PersonId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummPay { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SummInCurr { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Balans { get; set; }

        [StringLength(3)]
        public string CurrName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CurrRate { get; set; }

        [StringLength(100)]
        public string KaName { get; set; }

        [StringLength(255)]
        public string KaFullName { get; set; }

        [StringLength(64)]
        public string KaPhone { get; set; }

        [StringLength(100)]
        public string PersonName { get; set; }

        [StringLength(32)]
        public string Fax { get; set; }

        [StringLength(64)]
        public string Email { get; set; }

        [StringLength(64)]
        public string www { get; set; }

        public int? KType { get; set; }

        [StringLength(64)]
        public string Received { get; set; }

        public DateTime? ToDate { get; set; }

        public int DefNum { get; set; }

        public int? EntId { get; set; }

        [StringLength(100)]
        public string EntName { get; set; }

        [StringLength(50)]
        public string KagentGroupName { get; set; }

        [StringLength(64)]
        public string DocTypeName { get; set; }

        public int? PTypeId { get; set; }

        [StringLength(50)]
        public string PTypeName { get; set; }

        public DateTime? ShipmentDate { get; set; }

        public DateTime? ReportingDate { get; set; }

        public Guid? ReceiptId { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [StringLength(50)]
        public string UpdatedUserName { get; set; }

        public int Deleted { get; set; }

        [StringLength(50)]
        public string CarNumber { get; set; }

        [StringLength(50)]
        public string CarName { get; set; }

        [StringLength(100)]
        public string DriverName { get; set; }

        [StringLength(50)]
        public string RouteName { get; set; }

        public long? RouteDuration { get; set; }

        public int? RouteId { get; set; }

        public Guid? CarId { get; set; }

        public int? CustomerId { get; set; }

        public int? DriverId { get; set; }

        [StringLength(128)]
        public string WebUserId { get; set; }

        [StringLength(15)]
        public string AttNum { get; set; }

        public DateTime? AttDate { get; set; }

        public int? KaId { get; set; }

        public Guid? SessionId { get; set; }

        public int? UpdatedBy { get; set; }

        public int? KaArchived { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DefTotalAmount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ExTotalAmount { get; set; }

        public Guid? KagentId { get; set; }
        public int? DeliveredWaybillId { get; set; }
    }
}
