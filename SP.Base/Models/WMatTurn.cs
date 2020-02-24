namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WMatTurn")]
    public partial class WMatTurn
    {
        public int Id { get; set; }

        public int PosId { get; set; }

        public int WId { get; set; }

        public int MatId { get; set; }

        public DateTime OnDate { get; set; }

        public int TurnType { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        public int? SourceId { get; set; }

        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? CalcAmount { get; set; }

        public int? SupplierId { get; set; }

        public virtual Materials Materials { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual WaybillDet WaybillDet { get; set; }

        public virtual WaybillDet WaybillDet1 { get; set; }
    }
}
