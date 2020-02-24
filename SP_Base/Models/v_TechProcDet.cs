namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_TechProcDet
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DetId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WbillId { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal Out { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProcId { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int? PersonId { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime OnDate { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(100)]
        public string PersonName { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Num { get; set; }

        public int? ExtMatAmount { get; set; }

        [StringLength(255)]
        public string RamaName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TareWeight { get; set; }

        [StringLength(20)]
        public string Kod { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? OutNetto { get; set; }
    }
}
