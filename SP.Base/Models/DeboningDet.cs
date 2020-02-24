namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DeboningDet")]
    public partial class DeboningDet
    {
        [Key]
        public int DebId { get; set; }

        public int WBillId { get; set; }

        public int MatId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Price { get; set; }

        public int WId { get; set; }

        public virtual Materials Materials { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual WaybillList WaybillList { get; set; }
    }
}
