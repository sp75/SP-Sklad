namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_Services
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SvcId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Name { get; set; }

        [StringLength(80)]
        public string Artikul { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GrpId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsTransport { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        public int? CurrId { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Deleted { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsNormed { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Norm { get; set; }

        public int? MId { get; set; }

        [StringLength(64)]
        public string MeasuresName { get; set; }

        [StringLength(64)]
        public string SvcGroupName { get; set; }
    }
}
