namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PrintLog")]
    public partial class PrintLog
    {
        [Key]
        public int PlId { get; set; }

        public int? RepId { get; set; }

        public int? UserId { get; set; }

        public DateTime? OnDate { get; set; }

        public int? PrintType { get; set; }

        public Guid? OriginatorId { get; set; }

        public virtual Users Users { get; set; }
    }
}
