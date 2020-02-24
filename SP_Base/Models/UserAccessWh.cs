namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAccessWh")]
    public partial class UserAccessWh
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int WId { get; set; }

        public bool UseReceived { get; set; }

        public virtual Users Users { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}
