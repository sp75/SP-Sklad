namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OperLog")]
    public partial class OperLog
    {
        [Key]
        public int OpId { get; set; }

        [Required]
        [StringLength(1)]
        public string OpCode { get; set; }

        public DateTime OnDate { get; set; }

        public int? TabId { get; set; }

        public int? Id { get; set; }

        [StringLength(512)]
        public string DataBefore { get; set; }

        [StringLength(512)]
        public string DataAfter { get; set; }

        public int? FunId { get; set; }

        [StringLength(24)]
        public string DocNum { get; set; }

        public int? UserId { get; set; }

        public Guid? OriginatorId { get; set; }

        public virtual Tables Tables { get; set; }

        public virtual Users Users { get; set; }
    }
}
