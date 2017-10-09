namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReturnRel")]
    public partial class ReturnRel
    {
        public int Id { get; set; }

        public int PosId { get; set; }

        public int OutPosId { get; set; }

        public int PPosId { get; set; }

        public int? DPosId { get; set; }

        public virtual WaybillDet WaybillDet { get; set; }

        public virtual WaybillDet WaybillDet1 { get; set; }

        public virtual WaybillDet WaybillDet2 { get; set; }

        public virtual WaybillDet WaybillDet3 { get; set; }
    }
}
