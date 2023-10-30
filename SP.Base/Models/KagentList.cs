namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KagentList")]
    public partial class KagentList
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KaId { get; set; }

        [StringLength(10)]
        public string KAU { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(64)]
        public string INN { get; set; }

        [StringLength(64)]
        public string OKPO { get; set; }

        [StringLength(64)]
        public string CertNum { get; set; }

        [StringLength(64)]
        public string Phone { get; set; }

        [StringLength(64)]
        public string Email { get; set; }

        [StringLength(64)]
        public string www { get; set; }

        [StringLength(453)]
        public string FullUrADDR { get; set; }

        [StringLength(453)]
        public string FullFactADDR { get; set; }

        [StringLength(32)]
        public string Fax { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Deleted { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [StringLength(32)]
        public string KPP { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NdsPayer { get; set; }

        public DateTime? BirthDate { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KType { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KaKind { get; set; }

        [StringLength(128)]
        public string Job { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? StartSaldo { get; set; }

        public DateTime? StartSaldoDate { get; set; }

        public int? PTypeId { get; set; }

        public int? JobType { get; set; }

        public int? Def { get; set; }

        public int? UserId { get; set; }

        public int? Archived { get; set; }

        [StringLength(50)]
        public string TypKAgent { get; set; }

        [StringLength(50)]
        public string KAgentKind { get; set; }

        [StringLength(64)]
        public string PriceName { get; set; }

        [StringLength(64)]
        public string ContractType { get; set; }

        public DateTime? ContractDate { get; set; }

        [StringLength(20)]
        public string ContractNum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Saldo { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        public Guid? GroupId { get; set; }

        [StringLength(50)]
        public string Login { get; set; }

        [StringLength(128)]
        public string UserName { get; set; }

        [StringLength(128)]
        public string AspNetUserId { get; set; }

        [StringLength(256)]
        public string WebUserName { get; set; }
        public Guid Id { get; set; }
    }
}
