namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_KAgentAccount
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KAId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BankId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(64)]
        public string AccNum { get; set; }

        public int? Def { get; set; }

        [StringLength(50)]
        public string MFO { get; set; }

        [StringLength(128)]
        public string BankName { get; set; }

        [StringLength(64)]
        public string TypeName { get; set; }
    }
}
