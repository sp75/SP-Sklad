namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_PosRemains
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PosId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MatId { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal Remain { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "numeric")]
        public decimal Rsv { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AvgPrice { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "numeric")]
        public decimal Ordered { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "numeric")]
        public decimal OrderedRsv { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ActualRemain { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(20)]
        public string Num { get; set; }

        [Key]
        [Column(Order = 9)]
        public DateTime OnDate { get; set; }
    }
}
