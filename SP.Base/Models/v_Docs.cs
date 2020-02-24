namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_Docs
    {
        [Key]
        public Guid DocId { get; set; }

        public int? DocType { get; set; }
    }
}
