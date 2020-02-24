namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatChange")]
    public partial class MatChange
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MatId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ChangeId { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public virtual Materials Materials { get; set; }

        public virtual Materials Materials1 { get; set; }
    }
}
