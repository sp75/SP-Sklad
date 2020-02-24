namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KAgentAdjustmentDet")]
    public partial class KAgentAdjustmentDet
    {
        public Guid Id { get; set; }

        public int? Idx { get; set; }

        public Guid? KAgentAdjustmentId { get; set; }

        public Guid? DocId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Saldo { get; set; }

        public virtual KAgentAdjustment KAgentAdjustment { get; set; }
    }
}
