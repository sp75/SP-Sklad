namespace SP_Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EnterpriseAccount")]
    public partial class EnterpriseAccount
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KaId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(64)]
        public string AccNum { get; set; }

        [Key]
        [Column(Order = 4)]
        public string BankName { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string MFO { get; set; }

        [StringLength(64)]
        public string OKPO { get; set; }

        public int? Def { get; set; }
    }
}
