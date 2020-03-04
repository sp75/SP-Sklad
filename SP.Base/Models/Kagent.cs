namespace SP.Base.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kagent")]
    public partial class Kagent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kagent()
        {
            Commission = new HashSet<Commission>();
            Commission1 = new HashSet<Commission>();
            Commission2 = new HashSet<Commission>();
            Commission3 = new HashSet<Commission>();
            CONTRACTS = new HashSet<CONTRACTS>();
            CONTRACTS1 = new HashSet<CONTRACTS>();
            Customers = new HashSet<Customers>();
            DiscCards = new HashSet<DiscCards>();
            EnterpriseKagent = new HashSet<EnterpriseKagent>();
            EnterpriseWorker = new HashSet<EnterpriseWorker>();
            IntermediateWeighing = new HashSet<IntermediateWeighing>();
            KaAddr = new HashSet<KaAddr>();
            KAMatDiscount = new HashSet<KAMatDiscount>();
            KAMatGroupDiscount = new HashSet<KAMatGroupDiscount>();
            PayDoc = new HashSet<PayDoc>();
            PayDoc1 = new HashSet<PayDoc>();
            TAXWB = new HashSet<TAXWB>();
            WaybillList = new HashSet<WaybillList>();
            WaybillMove = new HashSet<WaybillMove>();
            KAgentAccount = new HashSet<KAgentAccount>();
            KAgentPersons = new HashSet<KAgentPersons>();
            KAgentActReconciliation = new HashSet<KAgentActReconciliation>();
            KAgentActReconciliation1 = new HashSet<KAgentActReconciliation>();
            KAgentActReconciliation2 = new HashSet<KAgentActReconciliation>();
            KAgentAdjustment = new HashSet<KAgentAdjustment>();
            KAgentAdjustment1 = new HashSet<KAgentAdjustment>();
            KAgentAdjustment2 = new HashSet<KAgentAdjustment>();
            KAgentSaldo = new HashSet<KAgentSaldo>();
            ProductionPlans = new HashSet<ProductionPlans>();
            ProductionPlans1 = new HashSet<ProductionPlans>();
            Routes1 = new HashSet<Routes>();
            TAXWB1 = new HashSet<TAXWB>();
            TechProcDet = new HashSet<TechProcDet>();
            WaybillList1 = new HashSet<WaybillList>();
            WayBillMake = new HashSet<WayBillMake>();
            WayBillMakeProps = new HashSet<WayBillMakeProps>();
            WaybillList2 = new HashSet<WaybillList>();
            WayBillSvc = new HashSet<WayBillSvc>();
            PriceList = new HashSet<PriceList>();
        }

        [Key]
        public int KaId { get; set; }

        [Required]
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

        [StringLength(32)]
        public string Fax { get; set; }

        public int Deleted { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        [StringLength(32)]
        public string KPP { get; set; }

        public int NdsPayer { get; set; }

        public DateTime? BirthDate { get; set; }

        public int KType { get; set; }

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

        [StringLength(10)]
        public string KAU { get; set; }

        [StringLength(64)]
        public string ContractType { get; set; }

        public DateTime? ContractDate { get; set; }

        [StringLength(20)]
        public string ContractNum { get; set; }

        public int? RouteId { get; set; }

        public Guid? GroupId { get; set; }

        public Guid Id { get; set; }

        public int? PayTypeId { get; set; }

        public int? CashDeskId { get; set; }

        public string Declaration { get; set; }

        [StringLength(128)]
        public string AspNetUserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commission> Commission { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commission> Commission1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commission> Commission2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Commission> Commission3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRACTS> CONTRACTS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRACTS> CONTRACTS1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customers> Customers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiscCards> DiscCards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnterpriseKagent> EnterpriseKagent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EnterpriseWorker> EnterpriseWorker { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntermediateWeighing> IntermediateWeighing { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KaAddr> KaAddr { get; set; }

        public virtual KADiscount KADiscount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAMatDiscount> KAMatDiscount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAMatGroupDiscount> KAMatGroupDiscount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayDoc> PayDoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayDoc> PayDoc1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAXWB> TAXWB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WaybillList> WaybillList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WaybillMove> WaybillMove { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAgentAccount> KAgentAccount { get; set; }

        public virtual KAgentDoc KAgentDoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAgentPersons> KAgentPersons { get; set; }

        public virtual KontragentGroup KontragentGroup { get; set; }

        public virtual Routes Routes { get; set; }

        public virtual Users Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAgentActReconciliation> KAgentActReconciliation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAgentActReconciliation> KAgentActReconciliation1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAgentActReconciliation> KAgentActReconciliation2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAgentAdjustment> KAgentAdjustment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAgentAdjustment> KAgentAdjustment1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAgentAdjustment> KAgentAdjustment2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KAgentSaldo> KAgentSaldo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionPlans> ProductionPlans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionPlans> ProductionPlans1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Routes> Routes1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAXWB> TAXWB1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TechProcDet> TechProcDet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WaybillList> WaybillList1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillMake> WayBillMake { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillMakeProps> WayBillMakeProps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WaybillList> WaybillList2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WayBillSvc> WayBillSvc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceList> PriceList { get; set; }
    }
}
