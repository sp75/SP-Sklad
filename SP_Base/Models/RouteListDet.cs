namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RouteListDet")]
    public partial class RouteListDet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int RouteListId { get; set; }

        public int DriverId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RouteId { get; set; }

        public virtual RouteList RouteList { get; set; }

        public virtual Routes Routes { get; set; }

        public virtual Users Users { get; set; }
    }
}
