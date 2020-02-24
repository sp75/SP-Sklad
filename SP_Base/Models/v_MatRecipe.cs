namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_MatRecipe
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecId { get; set; }

        [StringLength(255)]
        public string MatName { get; set; }

        public DateTime? OnDate { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal Amount { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(6)]
        public string ShortName { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(64)]
        public string GrpName { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "numeric")]
        public decimal Out { get; set; }

        public int? RType { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool Archived { get; set; }
    }
}
