namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_PriceTypes
    {
        [Key]
        [Column(Order = 0)]
        public int PTypeId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal OnValue { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Num { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Def { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Deleted { get; set; }

        public int? PPTypeId { get; set; }

        public int? ExtraType { get; set; }

        [StringLength(128)]
        public string PtName { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(7)]
        public string TypeName { get; set; }
    }
}
