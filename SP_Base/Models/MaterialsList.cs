namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MaterialsList")]
    public partial class MaterialsList
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MatId { get; set; }

        public int? Num { get; set; }

        [StringLength(255)]
        public string Produced { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? NDS { get; set; }

        public int? Serials { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Artikul { get; set; }

        public int? WId { get; set; }

        [StringLength(6)]
        public string MeasuresName { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Deleted { get; set; }

        public int? Archived { get; set; }

        public bool? AutoCalcRecipe { get; set; }
    }
}
