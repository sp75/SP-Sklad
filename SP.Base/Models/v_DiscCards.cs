namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_DiscCards
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CardId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Num { get; set; }

        public DateTime? ExpireDate { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal OnValue { get; set; }

        [StringLength(100)]
        public string KaName { get; set; }

        [StringLength(255)]
        public string KaFullName { get; set; }
    }
}
