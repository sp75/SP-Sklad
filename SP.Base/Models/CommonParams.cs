namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CommonParams
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Nds { get; set; }

        public int? ChargeType { get; set; }

        public int? PoType { get; set; }

        public int? PrintType { get; set; }

        public int? DelToBin { get; set; }

        [StringLength(50)]
        public string Ver { get; set; }

        [StringLength(100)]
        public string TemplatePatch { get; set; }

        public DateTime? EndCalcPeriod { get; set; }

        public DateTime? TrialPeriod { get; set; }
    }
}
