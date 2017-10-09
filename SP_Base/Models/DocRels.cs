namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DocRels
    {
        [Key]
        [Column(Order = 0)]
        public Guid OriginatorId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid RelOriginatorId { get; set; }
    }
}
