namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_IntermediateWeighing
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Num { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime OnDate { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Checked { get; set; }

        [StringLength(256)]
        public string Notes { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonId { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public Guid? SessionId { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WbillId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Amount { get; set; }

        [StringLength(255)]
        public string RecipeName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(20)]
        public string WbNum { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string PersonName { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WbChecked { get; set; }
    }
}
