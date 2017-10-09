namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MoneySaldo")]
    public partial class MoneySaldo
    {
        public int Id { get; set; }

        public int SaldoType { get; set; }

        public DateTime OnDate { get; set; }

        public int? AccId { get; set; }

        public int CurrId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Saldo { get; set; }

        public int? CashId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? SaldoDef { get; set; }

        public virtual CashDesks CashDesks { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual KAgentAccount KAgentAccount { get; set; }
    }
}
