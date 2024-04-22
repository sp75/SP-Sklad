using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SP.Base.Models
{
    using System;

    public partial class GetRelDocIds_Result
    {
        [Key]
        public System.Guid OriginatorId { get; set; }
        public Nullable<int> DocType { get; set; }
        public int RelType { get; set; }
    }
}
