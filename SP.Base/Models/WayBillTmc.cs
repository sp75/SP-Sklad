namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WayBillTmc")]
    public partial class WayBillTmc
    {
        [Key]
        public int PosId { get; set; }

        public int Num { get; set; }

        public int? Checked { get; set; }

        public int WbillId { get; set; }

        public int MatId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        public int TurnType { get; set; }

        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? CalcAmount { get; set; }

        public virtual Materials Materials { get; set; }

        public virtual WaybillList WaybillList { get; set; }
    }
}
