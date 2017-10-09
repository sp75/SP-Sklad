namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PosRel")]
    public partial class PosRel
    {
        public int Id { get; set; }

        public int PosId { get; set; }

        public int CPosId { get; set; }

        public virtual WaybillDet WaybillDet { get; set; }

        public virtual WaybillDet WaybillDet1 { get; set; }
    }
}
