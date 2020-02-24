namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ReportSortedFields
    {
        public Guid Id { get; set; }

        public int? Idx { get; set; }

        [StringLength(50)]
        public string FieldName { get; set; }

        [StringLength(100)]
        public string Caption { get; set; }

        public int? OrderDirection { get; set; }

        public int? RepId { get; set; }
    }
}
