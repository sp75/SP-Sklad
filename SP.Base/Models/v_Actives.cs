namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_Actives
    {
        [Key]
        public DateTime OnDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WhSumm { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Creditors { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Debitors { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Cash { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? CashLess { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Active { get; set; }
    }
}
