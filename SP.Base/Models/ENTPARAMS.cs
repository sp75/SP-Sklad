namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ENTPARAMS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(255)]
        public string NAME { get; set; }

        [StringLength(255)]
        public string ADDR { get; set; }

        [StringLength(48)]
        public string PHONE { get; set; }

        [StringLength(64)]
        public string OKPO { get; set; }

        [StringLength(64)]
        public string INN { get; set; }

        [StringLength(24)]
        public string CERTNUM { get; set; }

        [StringLength(32)]
        public string KPP { get; set; }

        public int NDSPAYER { get; set; }

        [StringLength(255)]
        public string FULLNAME { get; set; }
    }
}
