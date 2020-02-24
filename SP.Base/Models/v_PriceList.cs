namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_PriceList
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CurrId { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime OnDate { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Deleted { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UseLogo { get; set; }

        public int? DocId { get; set; }

        public int? PTypeId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(3)]
        public string CurrName { get; set; }

        [Key]
        [Column(Order = 6)]
        public Guid Id { get; set; }
    }
}
