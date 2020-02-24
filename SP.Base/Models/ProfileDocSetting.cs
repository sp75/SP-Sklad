namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProfileDocSetting")]
    public partial class ProfileDocSetting
    {
        public int ProfileId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocType { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? AllowZero { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? DefAmount { get; set; }

        public int? AutoNum { get; set; }

        [StringLength(10)]
        public string Prefix { get; set; }

        [StringLength(10)]
        public string Suffix { get; set; }

        public int? Checked { get; set; }

        public int Num { get; set; }
    }
}
