namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_wrd_CustomerOrders
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PosId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WbillId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MatId { get; set; }

        public int? WId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        public DateTime? OnDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Num { get; set; }

        public int? Checked { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Total { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? BasePrice { get; set; }

        [StringLength(100)]
        public string Notes { get; set; }

        [Key]
        [Column(Order = 4)]
        public string GrpName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(6)]
        public string MsrName { get; set; }

        [StringLength(255)]
        public string MatName { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GrpId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string KaName { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(50)]
        public string WbNum { get; set; }

        [Key]
        [Column(Order = 9)]
        public DateTime WbOnDate { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WType { get; set; }

        [Key]
        [Column(Order = 11)]
        public Guid KagentId { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WbChecked { get; set; }

        [StringLength(255)]
        public string Artikul { get; set; }

        [StringLength(255)]
        public string WbNotes { get; set; }

        [StringLength(100)]
        public string PersonName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PrevAmount { get; set; }
    }
}
