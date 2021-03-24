namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tara")]
    public partial class Tara
    {
        public int TaraId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Artikul { get; set; }

        public int? Num { get; set; }

        public int Deleted { get; set; }

        public int? GrpId { get; set; }

        public int? WId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Weight { get; set; }

        public int? TypeId { get; set; }

        public DateTime? DateAdded { get; set; }

        public DateTime? DateModified { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [StringLength(50)]
        public string InvNumber { get; set; }
    }
}
