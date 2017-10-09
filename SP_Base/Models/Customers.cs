namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customers
    {
        public Guid Id { get; set; }

        public int KaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Ligin { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public DateTime? LastLogin { get; set; }

        public bool? IsOnline { get; set; }

        public virtual Kagent Kagent { get; set; }
    }
}
