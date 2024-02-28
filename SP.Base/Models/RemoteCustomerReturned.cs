namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RemoteCustomerReturned")]
    public partial class RemoteCustomerReturned
    {
        public int Id { get; set; }

        public int? PosId { get; set; }

        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        public int MatId { get; set; }

        public int? WbillId { get; set; }

        public Guid CustomerId { get; set; }

        public int OutPosId { get; set; }

        public virtual Materials Materials { get; set; }

        public virtual WaybillDet WaybillDet { get; set; }

        public virtual WaybillList WaybillList { get; set; }
    }
}
