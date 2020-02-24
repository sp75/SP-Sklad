namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_IntermediateWeighingDet
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [StringLength(255)]
        public string MatName { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal Amount { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MatId { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid IntermediateWeighingId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(6)]
        public string MsrName { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string Num { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime OnDate { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Checked { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string PersonName { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WbillId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TaraAmount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Total { get; set; }
    }
}
