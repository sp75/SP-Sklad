namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KaAddr")]
    public partial class KaAddr
    {
        [Key]
        public int AddrId { get; set; }

        public int KaId { get; set; }

        public int AddrType { get; set; }

        [StringLength(64)]
        public string Country { get; set; }

        [StringLength(64)]
        public string City { get; set; }

        [StringLength(64)]
        public string District { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(15)]
        public string PostIndex { get; set; }

        [StringLength(64)]
        public string Region { get; set; }

        public int? CityType { get; set; }

        public virtual Kagent Kagent { get; set; }
    }
}
