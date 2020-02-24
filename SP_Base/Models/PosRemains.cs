namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PosRemains
    {
        public int Id { get; set; }

        public int PosId { get; set; }

        public int WId { get; set; }

        public int MatId { get; set; }

        public DateTime OnDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Remain { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Rsv { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AvgPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? InWay { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Ordered { get; set; }

        [Column(TypeName = "numeric")]
        public decimal OrderedRsv { get; set; }

        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? ActualRemain { get; set; }

        public int? SupplierId { get; set; }

        public virtual Materials Materials { get; set; }

        public virtual WaybillDet WaybillDet { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}
