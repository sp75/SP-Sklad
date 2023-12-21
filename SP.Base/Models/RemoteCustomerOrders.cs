namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RemoteCustomerOrders
    {

        [Key]
        public int Id { get; set; }

        public int? PosId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid CustomerId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        public int MatId { get; set; }

        public int? WbillId { get; set; }
    }
}
