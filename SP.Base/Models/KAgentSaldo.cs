namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KAgentSaldo")]
    public partial class KAgentSaldo
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KAId { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime OnDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Saldo { get; set; }

        public virtual Kagent Kagent { get; set; }
    }
}
