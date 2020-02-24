namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RepLng")]
    public partial class RepLng
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RepId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LangId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(1024)]
        public string Notes { get; set; }

        public virtual Languages Languages { get; set; }

        public virtual Reports Reports { get; set; }
    }
}
