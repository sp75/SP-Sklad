namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SchedulingOrders
    {
        public Guid Id { get; set; }

        public int RecId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Amount { get; set; }

        public virtual MatRecipe MatRecipe { get; set; }
    }
}
