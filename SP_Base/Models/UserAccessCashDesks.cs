namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserAccessCashDesks
    {
        public Guid Id { get; set; }

        public int UserId { get; set; }

        public int CashId { get; set; }

        public int Def { get; set; }

        public virtual CashDesks CashDesks { get; set; }

        public virtual Users Users { get; set; }
    }
}
