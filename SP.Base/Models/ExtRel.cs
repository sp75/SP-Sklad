namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExtRel")]
    public partial class ExtRel
    {
        public int Id { get; set; }

        public int IntPosId { get; set; }

        public int ExtPosId { get; set; }

        public virtual WaybillDet WaybillDet { get; set; }

        public virtual WaybillDet WaybillDet1 { get; set; }
    }
}
