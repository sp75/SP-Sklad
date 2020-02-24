namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WayBillDetTaxes
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PosId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaxId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal OnValue { get; set; }

        public virtual TAXES TAXES { get; set; }

        public virtual WaybillDet WaybillDet { get; set; }
    }
}
