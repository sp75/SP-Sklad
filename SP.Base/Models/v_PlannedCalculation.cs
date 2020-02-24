namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_PlannedCalculation
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime OnDate { get; set; }

        [StringLength(20)]
        public string Num { get; set; }

        [StringLength(100)]
        public string PersonName { get; set; }

        [StringLength(100)]
        public string EntName { get; set; }

        public string Notes { get; set; }
    }
}
