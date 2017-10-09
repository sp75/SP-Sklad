namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Licenses
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string LicencesKay { get; set; }

        [StringLength(50)]
        public string MacAddress { get; set; }

        [StringLength(50)]
        public string IpAddress { get; set; }

        [StringLength(50)]
        public string MachineName { get; set; }
    }
}
