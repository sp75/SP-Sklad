namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CurrencyRate")]
    public partial class CurrencyRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RateId { get; set; }

        public int CurrId { get; set; }

        public DateTime OnDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal OnValue { get; set; }

        public virtual Currency Currency { get; set; }
    }
}
