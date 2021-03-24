namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaraGroup")]
    public partial class TaraGroup
    {
        [Key]
        public int GrpId { get; set; }

        public int PId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int? Num { get; set; }
    }
}
