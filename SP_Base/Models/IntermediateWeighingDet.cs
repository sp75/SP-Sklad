namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IntermediateWeighingDet")]
    public partial class IntermediateWeighingDet
    {
        public Guid Id { get; set; }

        public int MatId { get; set; }

        public Guid IntermediateWeighingId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaraAmount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Total { get; set; }

        public virtual IntermediateWeighing IntermediateWeighing { get; set; }

        public virtual Materials Materials { get; set; }
    }
}
