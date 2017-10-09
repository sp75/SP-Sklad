namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PROFCOMMON")]
    public partial class PROFCOMMON
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PROFID { get; set; }

        public int? CHECKCURRENCY { get; set; }

        public int? WBOUTAUTOWH { get; set; }

        public int? ACCOUTAUTOWH { get; set; }

        public int? RPTYPE { get; set; }
    }
}
