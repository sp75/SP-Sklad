namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderedRels
    {
        public int Id { get; set; }

        public int OrdPosId { get; set; }

        public int OutPosId { get; set; }

        public virtual WaybillDet WaybillDet { get; set; }

        public virtual WaybillDet WaybillDet1 { get; set; }
    }
}
