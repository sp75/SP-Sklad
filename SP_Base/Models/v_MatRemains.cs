namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_MatRemains
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MatId { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime OnDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Remain { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Rsv { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AvgPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MinPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MaxPrice { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Ordered { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ORsv { get; set; }
    }
}
