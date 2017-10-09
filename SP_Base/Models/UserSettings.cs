namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserSettings
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(128)]
        public string Value { get; set; }

        public virtual Users Users { get; set; }
    }
}
