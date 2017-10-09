namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_KAgentSaldo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KaId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OnDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Saldo { get; set; }
    }
}
