namespace SP_Base.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SP_BaseEntities : DbContext
    {
        public SP_BaseEntities(string connection_string)
            : base(connection_string)
        {

        }

        public virtual DbSet<AccountType> AccountType { get; set; }
        public virtual DbSet<Banks> Banks { get; set; }
        public virtual DbSet<BanksPersons> BanksPersons { get; set; }
        public virtual DbSet<CashDesks> CashDesks { get; set; }
        public virtual DbSet<ChargeType> ChargeType { get; set; }
        public virtual DbSet<CityType> CityType { get; set; }
        public virtual DbSet<Commission> Commission { get; set; }
        public virtual DbSet<CommonParams> CommonParams { get; set; }
        public virtual DbSet<CONTRACTS> CONTRACTS { get; set; }
        public virtual DbSet<CONTRDET> CONTRDET { get; set; }
        public virtual DbSet<CONTRPARAMS> CONTRPARAMS { get; set; }
        public virtual DbSet<CONTRRESULTS> CONTRRESULTS { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<CURRENCYRATE> CURRENCYRATE { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<DeboningDet> DeboningDet { get; set; }
        public virtual DbSet<DemandGroup> DemandGroup { get; set; }
        public virtual DbSet<DiscCardGrp> DiscCardGrp { get; set; }
        public virtual DbSet<DiscCards> DiscCards { get; set; }
        public virtual DbSet<DocRels> DocRels { get; set; }
        public virtual DbSet<DocType> DocType { get; set; }
        public virtual DbSet<EnterpriseKagent> EnterpriseKagent { get; set; }
        public virtual DbSet<EnterpriseWorker> EnterpriseWorker { get; set; }
        public virtual DbSet<ENTPARAMS> ENTPARAMS { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<ExtRel> ExtRel { get; set; }
        public virtual DbSet<Functions> Functions { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<KaAddr> KaAddr { get; set; }
        public virtual DbSet<KADiscount> KADiscount { get; set; }
        public virtual DbSet<Kagent> Kagent { get; set; }
        public virtual DbSet<KAgentAccount> KAgentAccount { get; set; }
        public virtual DbSet<KAgentDoc> KAgentDoc { get; set; }
        public virtual DbSet<KAgentPersons> KAgentPersons { get; set; }
        public virtual DbSet<KAgentSaldo> KAgentSaldo { get; set; }
        public virtual DbSet<KAgentTyp> KAgentTyp { get; set; }
        public virtual DbSet<KAKInd> KAKInd { get; set; }
        public virtual DbSet<KAMatDiscount> KAMatDiscount { get; set; }
        public virtual DbSet<KAMatGroupDiscount> KAMatGroupDiscount { get; set; }
        public virtual DbSet<KontragentGroup> KontragentGroup { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<Licenses> Licenses { get; set; }
        public virtual DbSet<MatChange> MatChange { get; set; }
        public virtual DbSet<Materials> Materials { get; set; }
        public virtual DbSet<MatGroup> MatGroup { get; set; }
        public virtual DbSet<MatGroupPrices> MatGroupPrices { get; set; }
        public virtual DbSet<MatPrices> MatPrices { get; set; }
        public virtual DbSet<MatRecDet> MatRecDet { get; set; }
        public virtual DbSet<MatRecipe> MatRecipe { get; set; }
        public virtual DbSet<MatRecipeTechProcDet> MatRecipeTechProcDet { get; set; }
        public virtual DbSet<MatRemains> MatRemains { get; set; }
        public virtual DbSet<Measures> Measures { get; set; }
        public virtual DbSet<MoneySaldo> MoneySaldo { get; set; }
        public virtual DbSet<OperLog> OperLog { get; set; }
        public virtual DbSet<PayDoc> PayDoc { get; set; }
        public virtual DbSet<PayDocType> PayDocType { get; set; }
        public virtual DbSet<PayType> PayType { get; set; }
        public virtual DbSet<PosRel> PosRel { get; set; }
        public virtual DbSet<PosRemains> PosRemains { get; set; }
        public virtual DbSet<PriceList> PriceList { get; set; }
        public virtual DbSet<PriceListDet> PriceListDet { get; set; }
        public virtual DbSet<PriceTypes> PriceTypes { get; set; }
        public virtual DbSet<PrintLog> PrintLog { get; set; }
        public virtual DbSet<ProductionPlanDet> ProductionPlanDet { get; set; }
        public virtual DbSet<ProductionPlans> ProductionPlans { get; set; }
        public virtual DbSet<ProfileDocSetting> ProfileDocSetting { get; set; }
        public virtual DbSet<RepLng> RepLng { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<ReturnRel> ReturnRel { get; set; }
        public virtual DbSet<RouteList> RouteList { get; set; }
        public virtual DbSet<RouteListDet> RouteListDet { get; set; }
        public virtual DbSet<Routes> Routes { get; set; }
        public virtual DbSet<Serials> Serials { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<SettingApp> SettingApp { get; set; }
        public virtual DbSet<SvcGroup> SvcGroup { get; set; }
        public virtual DbSet<Tables> Tables { get; set; }
        public virtual DbSet<TAXES> TAXES { get; set; }
        public virtual DbSet<TAXREESTRTYPE> TAXREESTRTYPE { get; set; }
        public virtual DbSet<TAXWB> TAXWB { get; set; }
        public virtual DbSet<TAXWBDET> TAXWBDET { get; set; }
        public virtual DbSet<TechProcDet> TechProcDet { get; set; }
        public virtual DbSet<TechProcess> TechProcess { get; set; }
        public virtual DbSet<UserAccess> UserAccess { get; set; }
        public virtual DbSet<UserAccessWh> UserAccessWh { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserSettings> UserSettings { get; set; }
        public virtual DbSet<UsersGroup> UsersGroup { get; set; }
        public virtual DbSet<UserTree> UserTree { get; set; }
        public virtual DbSet<UserTreeView> UserTreeView { get; set; }
        public virtual DbSet<ViewLng> ViewLng { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<WaybillDet> WaybillDet { get; set; }
        public virtual DbSet<WayBillDetAddProps> WayBillDetAddProps { get; set; }
        public virtual DbSet<WayBillDetTaxes> WayBillDetTaxes { get; set; }
        public virtual DbSet<WaybillList> WaybillList { get; set; }
        public virtual DbSet<WayBillMake> WayBillMake { get; set; }
        public virtual DbSet<WayBillMakeProps> WayBillMakeProps { get; set; }
        public virtual DbSet<WaybillMove> WaybillMove { get; set; }
        public virtual DbSet<WayBillSvc> WayBillSvc { get; set; }
        public virtual DbSet<WMatTurn> WMatTurn { get; set; }
        public virtual DbSet<PROFCOMMON> PROFCOMMON { get; set; }
        public virtual DbSet<ProfilesSetting> ProfilesSetting { get; set; }
        public virtual DbSet<PROFINTF> PROFINTF { get; set; }
        public virtual DbSet<EnterpriseAccount> EnterpriseAccount { get; set; }
        public virtual DbSet<KagentList> KagentList { get; set; }
        public virtual DbSet<MaterialsList> MaterialsList { get; set; }
        public virtual DbSet<UserTreeAccess> UserTreeAccess { get; set; }
        public virtual DbSet<v_Actives> v_Actives { get; set; }
        public virtual DbSet<v_DiscCards> v_DiscCards { get; set; }
        public virtual DbSet<v_Docs> v_Docs { get; set; }
        public virtual DbSet<v_EnterpriseList> v_EnterpriseList { get; set; }
        public virtual DbSet<v_ErrorLog> v_ErrorLog { get; set; }
        public virtual DbSet<v_GetDocsTree> v_GetDocsTree { get; set; }
        public virtual DbSet<v_KAgentAccount> v_KAgentAccount { get; set; }
        public virtual DbSet<v_KAgentSaldo> v_KAgentSaldo { get; set; }
        public virtual DbSet<v_MatRemains> v_MatRemains { get; set; }
        public virtual DbSet<v_PayDoc> v_PayDoc { get; set; }
        public virtual DbSet<v_PosRemains> v_PosRemains { get; set; }
        public virtual DbSet<v_PriceList> v_PriceList { get; set; }
        public virtual DbSet<v_PriceTypes> v_PriceTypes { get; set; }
        public virtual DbSet<v_ProductionPlanDet> v_ProductionPlanDet { get; set; }
        public virtual DbSet<v_Services> v_Services { get; set; }
        public virtual DbSet<v_TechProcDet> v_TechProcDet { get; set; }
        public virtual DbSet<v_Users> v_Users { get; set; }
        public virtual DbSet<v_WaybillList> v_WaybillList { get; set; }
        public virtual DbSet<v_WhMatRemains> v_WhMatRemains { get; set; }
        public virtual DbSet<v_WorkDate> v_WorkDate { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountType>()
                .HasMany(e => e.KAgentAccount)
                .WithRequired(e => e.AccountType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Banks>()
                .HasMany(e => e.KAgentAccount)
                .WithRequired(e => e.Banks)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChargeType>()
                .HasMany(e => e.PayDoc)
                .WithRequired(e => e.ChargeType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CityType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CityType>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<CommonParams>()
                .Property(e => e.Nds)
                .HasPrecision(15, 8);

            modelBuilder.Entity<CONTRACTS>()
                .Property(e => e.NUM)
                .IsUnicode(false);

            modelBuilder.Entity<CONTRACTS>()
                .Property(e => e.FPATH)
                .IsUnicode(false);

            modelBuilder.Entity<CONTRACTS>()
                .HasOptional(e => e.CONTRPARAMS)
                .WithRequired(e => e.CONTRACTS)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CONTRACTS>()
                .HasOptional(e => e.CONTRRESULTS)
                .WithRequired(e => e.CONTRACTS)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CONTRDET>()
                .Property(e => e.AMOUNT)
                .HasPrecision(15, 8);

            modelBuilder.Entity<CONTRDET>()
                .Property(e => e.PRICE)
                .HasPrecision(15, 8);

            modelBuilder.Entity<CONTRDET>()
                .Property(e => e.NDS)
                .HasPrecision(15, 8);

            modelBuilder.Entity<CONTRDET>()
                .Property(e => e.SHIPPEDAMOUNT)
                .HasPrecision(15, 8);

            modelBuilder.Entity<CONTRPARAMS>()
                .Property(e => e.SUMM)
                .HasPrecision(15, 8);

            modelBuilder.Entity<CONTRPARAMS>()
                .Property(e => e.AMOUNT)
                .HasPrecision(15, 8);

            modelBuilder.Entity<CONTRRESULTS>()
                .Property(e => e.SHIPPEDSUMM)
                .HasPrecision(15, 8);

            modelBuilder.Entity<CONTRRESULTS>()
                .Property(e => e.SHIPPEDAMOUNT)
                .HasPrecision(15, 8);

            modelBuilder.Entity<CONTRRESULTS>()
                .Property(e => e.PAIDSUMM)
                .HasPrecision(15, 8);

            modelBuilder.Entity<Countries>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Countries>()
                .Property(e => e.ShortName)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.CURRENCYRATE)
                .WithRequired(e => e.Currency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.PayDoc)
                .WithRequired(e => e.Currency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.MoneySaldo)
                .WithRequired(e => e.Currency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.PriceList)
                .WithRequired(e => e.Currency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.WayBillSvc)
                .WithRequired(e => e.Currency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CURRENCYRATE>()
                .Property(e => e.ONVALUE)
                .HasPrecision(15, 8);

            modelBuilder.Entity<DeboningDet>()
                .Property(e => e.Amount)
                .HasPrecision(15, 8);

            modelBuilder.Entity<DeboningDet>()
                .Property(e => e.Price)
                .HasPrecision(15, 8);

            modelBuilder.Entity<DiscCardGrp>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<DiscCardGrp>()
                .HasMany(e => e.DiscCards)
                .WithRequired(e => e.DiscCardGrp)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DiscCards>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<DiscCards>()
                .Property(e => e.StartSaldo)
                .HasPrecision(15, 8);

            modelBuilder.Entity<ENTPARAMS>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<ENTPARAMS>()
                .Property(e => e.ADDR)
                .IsUnicode(false);

            modelBuilder.Entity<ENTPARAMS>()
                .Property(e => e.PHONE)
                .IsUnicode(false);

            modelBuilder.Entity<ENTPARAMS>()
                .Property(e => e.OKPO)
                .IsUnicode(false);

            modelBuilder.Entity<ENTPARAMS>()
                .Property(e => e.INN)
                .IsUnicode(false);

            modelBuilder.Entity<ENTPARAMS>()
                .Property(e => e.CERTNUM)
                .IsUnicode(false);

            modelBuilder.Entity<ENTPARAMS>()
                .Property(e => e.KPP)
                .IsUnicode(false);

            modelBuilder.Entity<ENTPARAMS>()
                .Property(e => e.FULLNAME)
                .IsUnicode(false);

            modelBuilder.Entity<Functions>()
                .Property(e => e.ClassName)
                .IsUnicode(false);

            modelBuilder.Entity<KaAddr>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<KaAddr>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<KaAddr>()
                .Property(e => e.District)
                .IsUnicode(false);

            modelBuilder.Entity<KaAddr>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<KaAddr>()
                .Property(e => e.PostIndex)
                .IsUnicode(false);

            modelBuilder.Entity<KaAddr>()
                .Property(e => e.Region)
                .IsUnicode(false);

            modelBuilder.Entity<KADiscount>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 8);

            modelBuilder.Entity<Kagent>()
                .Property(e => e.StartSaldo)
                .HasPrecision(15, 8);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.Commission)
                .WithOptional(e => e.Kagent)
                .HasForeignKey(e => e.KaId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.Commission1)
                .WithOptional(e => e.Kagent1)
                .HasForeignKey(e => e.FirstKaId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.Commission2)
                .WithOptional(e => e.Kagent2)
                .HasForeignKey(e => e.SecondKaId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.Commission3)
                .WithOptional(e => e.Kagent3)
                .HasForeignKey(e => e.ThirdKaId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.CONTRACTS)
                .WithOptional(e => e.Kagent)
                .HasForeignKey(e => e.KAID);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.CONTRACTS1)
                .WithOptional(e => e.Kagent1)
                .HasForeignKey(e => e.PERSONID);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.EnterpriseKagent)
                .WithRequired(e => e.Kagent)
                .HasForeignKey(e => e.EnterpriseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.EnterpriseWorker)
                .WithRequired(e => e.Kagent)
                .HasForeignKey(e => e.EnterpriseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kagent>()
                .HasOptional(e => e.KADiscount)
                .WithRequired(e => e.Kagent)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.PayDoc)
                .WithOptional(e => e.Kagent)
                .HasForeignKey(e => e.KaId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.PayDoc1)
                .WithOptional(e => e.Kagent1)
                .HasForeignKey(e => e.MPersonId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.TAXWB)
                .WithOptional(e => e.Kagent)
                .HasForeignKey(e => e.PERSONID);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.WaybillList)
                .WithOptional(e => e.Kagent)
                .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.WaybillMove)
                .WithOptional(e => e.Kagent)
                .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<Kagent>()
                .HasOptional(e => e.KAgentDoc)
                .WithRequired(e => e.Kagent)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.Routes1)
                .WithOptional(e => e.Kagent1)
                .HasForeignKey(e => e.DriverId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.TAXWB1)
                .WithRequired(e => e.Kagent1)
                .HasForeignKey(e => e.KAID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.TechProcDet)
                .WithOptional(e => e.Kagent)
                .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.WaybillList1)
                .WithOptional(e => e.Kagent1)
                .HasForeignKey(e => e.KaId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.WayBillMake)
                .WithOptional(e => e.Kagent)
                .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.WayBillMakeProps)
                .WithRequired(e => e.Kagent)
                .HasForeignKey(e => e.PersonId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.WaybillList2)
                .WithOptional(e => e.Kagent2)
                .HasForeignKey(e => e.EntId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.WayBillSvc)
                .WithOptional(e => e.Kagent)
                .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<KAgentAccount>()
                .Property(e => e.AccNum)
                .IsUnicode(false);

            modelBuilder.Entity<KAgentDoc>()
                .Property(e => e.DocName)
                .IsUnicode(false);

            modelBuilder.Entity<KAgentDoc>()
                .Property(e => e.DocNum)
                .IsUnicode(false);

            modelBuilder.Entity<KAgentDoc>()
                .Property(e => e.DocSeries)
                .IsUnicode(false);

            modelBuilder.Entity<KAgentDoc>()
                .Property(e => e.DocWhoProduce)
                .IsUnicode(false);

            modelBuilder.Entity<KAgentPersons>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<KAgentPersons>()
                .Property(e => e.Post)
                .IsUnicode(false);

            modelBuilder.Entity<KAgentPersons>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<KAgentPersons>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<KAgentPersons>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<KAgentSaldo>()
                .Property(e => e.Saldo)
                .HasPrecision(15, 4);

            modelBuilder.Entity<KAMatDiscount>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<KAMatGroupDiscount>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<KontragentGroup>()
                .HasMany(e => e.Kagent)
                .WithOptional(e => e.KontragentGroup)
                .HasForeignKey(e => e.GroupId);

            modelBuilder.Entity<Languages>()
                .Property(e => e.ShortName)
                .IsUnicode(false);

            modelBuilder.Entity<Languages>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Languages>()
                .HasMany(e => e.ViewLng)
                .WithRequired(e => e.Languages)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Languages>()
                .HasMany(e => e.RepLng)
                .WithRequired(e => e.Languages)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materials>()
                .Property(e => e.MinReserv)
                .HasPrecision(15, 8);

            modelBuilder.Entity<Materials>()
                .Property(e => e.Weight)
                .HasPrecision(15, 8);

            modelBuilder.Entity<Materials>()
                .Property(e => e.MSize)
                .HasPrecision(15, 8);

            modelBuilder.Entity<Materials>()
                .Property(e => e.NDS)
                .HasPrecision(15, 8);

            modelBuilder.Entity<Materials>()
                .HasMany(e => e.DeboningDet)
                .WithRequired(e => e.Materials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materials>()
                .HasMany(e => e.MatChange)
                .WithRequired(e => e.Materials)
                .HasForeignKey(e => e.ChangeId);

            modelBuilder.Entity<Materials>()
                .HasMany(e => e.MatChange1)
                .WithRequired(e => e.Materials1)
                .HasForeignKey(e => e.MatId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materials>()
                .HasMany(e => e.MatRemains)
                .WithRequired(e => e.Materials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materials>()
                .HasMany(e => e.PosRemains)
                .WithRequired(e => e.Materials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materials>()
                .HasMany(e => e.WMatTurn)
                .WithRequired(e => e.Materials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materials>()
                .HasMany(e => e.MatRecDet)
                .WithRequired(e => e.Materials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materials>()
                .HasMany(e => e.TAXWBDET)
                .WithRequired(e => e.Materials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materials>()
                .HasMany(e => e.WaybillDet)
                .WithRequired(e => e.Materials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materials>()
                .HasMany(e => e.WayBillMakeProps)
                .WithRequired(e => e.Materials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MatGroup>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MatGroup>()
                .Property(e => e.Nds)
                .HasPrecision(15, 8);

            modelBuilder.Entity<MatGroup>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<MatGroup>()
                .Property(e => e.Num)
                .HasPrecision(5, 1);

            modelBuilder.Entity<MatGroupPrices>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MatPrices>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MatRecDet>()
                .Property(e => e.Amount)
                .HasPrecision(15, 6);

            modelBuilder.Entity<MatRecDet>()
                .Property(e => e.Coefficient)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MatRecipe>()
                .Property(e => e.Num)
                .IsUnicode(false);

            modelBuilder.Entity<MatRecipe>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<MatRecipe>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MatRecipe>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<MatRecipe>()
                .Property(e => e.Out)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MatRecipe>()
                .HasMany(e => e.ProductionPlanDet)
                .WithRequired(e => e.MatRecipe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MatRecipeTechProcDet>()
                .Property(e => e.ExpectedOut)
                .HasPrecision(15, 2);

            modelBuilder.Entity<MatRemains>()
                .Property(e => e.Remain)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MatRemains>()
                .Property(e => e.Rsv)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MatRemains>()
                .Property(e => e.AvgPrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MatRemains>()
                .Property(e => e.MinPrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MatRemains>()
                .Property(e => e.MaxPrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MatRemains>()
                .Property(e => e.InWay)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MatRemains>()
                .Property(e => e.Ordered)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MatRemains>()
                .Property(e => e.ORsv)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Measures>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Measures>()
                .HasMany(e => e.Materials)
                .WithRequired(e => e.Measures)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MoneySaldo>()
                .Property(e => e.Saldo)
                .HasPrecision(15, 2);

            modelBuilder.Entity<MoneySaldo>()
                .Property(e => e.SaldoDef)
                .HasPrecision(15, 2);

            modelBuilder.Entity<OperLog>()
                .Property(e => e.OpCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<OperLog>()
                .Property(e => e.DocNum)
                .IsUnicode(false);

            modelBuilder.Entity<PayDoc>()
                .Property(e => e.Total)
                .HasPrecision(15, 2);

            modelBuilder.Entity<PayDoc>()
                .Property(e => e.DocNum)
                .IsUnicode(false);

            modelBuilder.Entity<PayDoc>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<PayDoc>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<PayDoc>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 8);

            modelBuilder.Entity<PayDoc>()
                .Property(e => e.Schet)
                .IsUnicode(false);

            modelBuilder.Entity<PayType>()
                .HasMany(e => e.PayDoc)
                .WithRequired(e => e.PayType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PosRemains>()
                .Property(e => e.Remain)
                .HasPrecision(15, 4);

            modelBuilder.Entity<PosRemains>()
                .Property(e => e.Rsv)
                .HasPrecision(15, 4);

            modelBuilder.Entity<PosRemains>()
                .Property(e => e.AvgPrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<PosRemains>()
                .Property(e => e.InWay)
                .HasPrecision(15, 4);

            modelBuilder.Entity<PosRemains>()
                .Property(e => e.Ordered)
                .HasPrecision(15, 4);

            modelBuilder.Entity<PosRemains>()
                .Property(e => e.OrderedRsv)
                .HasPrecision(15, 4);

            modelBuilder.Entity<PriceListDet>()
                .Property(e => e.Price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<PriceTypes>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PriceTypes>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 8);

            modelBuilder.Entity<PriceTypes>()
                .HasMany(e => e.MatGroupPrices)
                .WithRequired(e => e.PriceTypes)
                .HasForeignKey(e => e.PTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PriceTypes>()
                .HasMany(e => e.MatGroupPrices1)
                .WithOptional(e => e.PriceTypes1)
                .HasForeignKey(e => e.PPTypeId);

            modelBuilder.Entity<PriceTypes>()
                .HasMany(e => e.MatPrices)
                .WithRequired(e => e.PriceTypes)
                .HasForeignKey(e => e.PTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PriceTypes>()
                .HasMany(e => e.MatPrices1)
                .WithOptional(e => e.PriceTypes1)
                .HasForeignKey(e => e.PPTypeId);

            modelBuilder.Entity<ProductionPlanDet>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<ProductionPlanDet>()
                .Property(e => e.Remain)
                .HasPrecision(15, 4);

            modelBuilder.Entity<ProductionPlanDet>()
                .Property(e => e.Total)
                .HasPrecision(15, 4);

            modelBuilder.Entity<ProductionPlans>()
                .HasMany(e => e.ProductionPlanDet)
                .WithOptional(e => e.ProductionPlans)
                .HasForeignKey(e => e.ProductionPlanId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ProfileDocSetting>()
                .Property(e => e.DefAmount)
                .HasPrecision(15, 8);

            modelBuilder.Entity<RepLng>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<RepLng>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<Reports>()
                .HasMany(e => e.RepLng)
                .WithRequired(e => e.Reports)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Routes>()
                .HasMany(e => e.Kagent)
                .WithOptional(e => e.Routes)
                .HasForeignKey(e => e.RouteId);

            modelBuilder.Entity<Routes>()
                .HasMany(e => e.RouteListDet)
                .WithRequired(e => e.Routes)
                .HasForeignKey(e => e.RouteId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Services>()
                .Property(e => e.Price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Services>()
                .Property(e => e.Norm)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Services>()
                .HasMany(e => e.WayBillSvc)
                .WithRequired(e => e.Services)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SettingApp>()
                .Property(e => e.ProfileId)
                .IsFixedLength();

            modelBuilder.Entity<SvcGroup>()
                .HasMany(e => e.Services)
                .WithRequired(e => e.SvcGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAXES>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TAXES>()
                .Property(e => e.ONVALUE)
                .HasPrecision(15, 8);

            modelBuilder.Entity<TAXES>()
                .HasMany(e => e.WayBillDetTaxes)
                .WithRequired(e => e.TAXES)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAXREESTRTYPE>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TAXWB>()
                .Property(e => e.ADDCHARGES)
                .HasPrecision(15, 8);

            modelBuilder.Entity<TAXWB>()
                .Property(e => e.DISCOUNT)
                .HasPrecision(15, 8);

            modelBuilder.Entity<TAXWB>()
                .Property(e => e.SUMMALL)
                .HasPrecision(15, 8);

            modelBuilder.Entity<TAXWB>()
                .Property(e => e.CONDITION)
                .IsUnicode(false);

            modelBuilder.Entity<TAXWB>()
                .Property(e => e.FORM)
                .IsUnicode(false);

            modelBuilder.Entity<TAXWB>()
                .Property(e => e.NDS)
                .HasPrecision(15, 8);

            modelBuilder.Entity<TAXWB>()
                .Property(e => e.NUM)
                .IsUnicode(false);

            modelBuilder.Entity<TAXWB>()
                .Property(e => e.NUM1)
                .IsUnicode(false);

            modelBuilder.Entity<TAXWB>()
                .Property(e => e.NUM2)
                .IsUnicode(false);

            modelBuilder.Entity<TAXWB>()
                .Property(e => e.CONTRACT_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<TAXWB>()
                .Property(e => e.CONTRACT_NUM)
                .IsUnicode(false);

            modelBuilder.Entity<TAXWBDET>()
                .Property(e => e.AMOUNT)
                .HasPrecision(15, 4);

            modelBuilder.Entity<TAXWBDET>()
                .Property(e => e.PRICE)
                .HasPrecision(15, 4);

            modelBuilder.Entity<TAXWBDET>()
                .Property(e => e.NDS)
                .HasPrecision(15, 4);

            modelBuilder.Entity<TAXWBDET>()
                .Property(e => e.TOTAL)
                .HasPrecision(15, 4);

            modelBuilder.Entity<TechProcDet>()
                .Property(e => e.Out)
                .HasPrecision(15, 4);

            modelBuilder.Entity<TechProcDet>()
                .Property(e => e.TareWeight)
                .HasPrecision(15, 4);

            modelBuilder.Entity<TechProcDet>()
                .Property(e => e.OutNetto)
                .HasPrecision(15, 4);

            modelBuilder.Entity<TechProcess>()
                .HasMany(e => e.TechProcDet)
                .WithRequired(e => e.TechProcess)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.OperLog)
                .WithOptional(e => e.Users)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Users>()
                .HasMany(e => e.PrintLog)
                .WithOptional(e => e.Users)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Users>()
                .HasMany(e => e.RouteList)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.RouteListDet)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.DriverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.UserSettings)
                .WithOptional(e => e.Users)
                .WillCascadeOnDelete();

            modelBuilder.Entity<UsersGroup>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.UsersGroup)
                .HasForeignKey(e => e.GroupId);

            modelBuilder.Entity<UserTree>()
                .HasMany(e => e.UserTreeView)
                .WithRequired(e => e.UserTree)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserTreeView>()
                .HasMany(e => e.ViewLng)
                .WithRequired(e => e.UserTreeView)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.DeboningDet)
                .WithRequired(e => e.Warehouse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.Materials)
                .WithOptional(e => e.Warehouse)
                .HasForeignKey(e => e.WId);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.Materials1)
                .WithOptional(e => e.Warehouse1)
                .HasForeignKey(e => e.WId);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.Materials2)
                .WithOptional(e => e.Warehouse2)
                .HasForeignKey(e => e.WId);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.PosRemains)
                .WithRequired(e => e.Warehouse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.ProductionPlanDet)
                .WithRequired(e => e.Warehouse)
                .HasForeignKey(e => e.WhId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.WMatTurn)
                .WithRequired(e => e.Warehouse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.WaybillMove)
                .WithOptional(e => e.Warehouse)
                .HasForeignKey(e => e.DestWId);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.WaybillMove1)
                .WithRequired(e => e.Warehouse1)
                .HasForeignKey(e => e.SourceWid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.WayBillMake)
                .WithRequired(e => e.Warehouse)
                .HasForeignKey(e => e.SourceWId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.Price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.Discount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.Nds)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.Total)
                .HasPrecision(15, 2);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.BasePrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .Property(e => e.AvgInPrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillDet>()
                .HasMany(e => e.ExtRel)
                .WithRequired(e => e.WaybillDet)
                .HasForeignKey(e => e.ExtPosId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WaybillDet>()
                .HasMany(e => e.ExtRel1)
                .WithRequired(e => e.WaybillDet1)
                .HasForeignKey(e => e.IntPosId);

            modelBuilder.Entity<WaybillDet>()
                .HasMany(e => e.PosRel)
                .WithRequired(e => e.WaybillDet)
                .HasForeignKey(e => e.CPosId);

            modelBuilder.Entity<WaybillDet>()
                .HasMany(e => e.PosRel1)
                .WithRequired(e => e.WaybillDet1)
                .HasForeignKey(e => e.PosId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WaybillDet>()
                .HasMany(e => e.PosRemains)
                .WithRequired(e => e.WaybillDet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WaybillDet>()
                .HasMany(e => e.ReturnRel)
                .WithOptional(e => e.WaybillDet)
                .HasForeignKey(e => e.DPosId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<WaybillDet>()
                .HasMany(e => e.ReturnRel1)
                .WithRequired(e => e.WaybillDet1)
                .HasForeignKey(e => e.OutPosId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WaybillDet>()
                .HasMany(e => e.ReturnRel2)
                .WithRequired(e => e.WaybillDet2)
                .HasForeignKey(e => e.PosId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WaybillDet>()
                .HasMany(e => e.ReturnRel3)
                .WithRequired(e => e.WaybillDet3)
                .HasForeignKey(e => e.PPosId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WaybillDet>()
                .HasOptional(e => e.WayBillDetAddProps)
                .WithRequired(e => e.WaybillDet)
                .WillCascadeOnDelete();

            modelBuilder.Entity<WaybillDet>()
                .HasMany(e => e.WMatTurn)
                .WithRequired(e => e.WaybillDet)
                .HasForeignKey(e => e.PosId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WaybillDet>()
                .HasMany(e => e.WMatTurn1)
                .WithOptional(e => e.WaybillDet1)
                .HasForeignKey(e => e.SourceId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<WayBillDetTaxes>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillList>()
                .Property(e => e.SummAll)
                .HasPrecision(15, 2);

            modelBuilder.Entity<WaybillList>()
                .Property(e => e.Nds)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillList>()
                .Property(e => e.SummPay)
                .HasPrecision(15, 2);

            modelBuilder.Entity<WaybillList>()
                .Property(e => e.SummInCurr)
                .HasPrecision(15, 2);

            modelBuilder.Entity<WaybillList>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WaybillList>()
                .HasMany(e => e.WaybillDet)
                .WithRequired(e => e.WaybillList)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WaybillList>()
                .HasMany(e => e.WayBillDetAddProps)
                .WithOptional(e => e.WaybillList)
                .HasForeignKey(e => e.WbMaked);

            modelBuilder.Entity<WaybillList>()
                .HasOptional(e => e.WayBillMake)
                .WithRequired(e => e.WaybillList)
                .WillCascadeOnDelete();

            modelBuilder.Entity<WaybillList>()
                .HasOptional(e => e.WaybillMove)
                .WithRequired(e => e.WaybillList)
                .WillCascadeOnDelete();

            modelBuilder.Entity<WayBillMake>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WayBillSvc>()
                .Property(e => e.Price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WayBillSvc>()
                .Property(e => e.BasePrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WayBillSvc>()
                .Property(e => e.Norm)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WayBillSvc>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WayBillSvc>()
                .Property(e => e.Nds)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WayBillSvc>()
                .Property(e => e.Discount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WayBillSvc>()
                .Property(e => e.Total)
                .HasPrecision(15, 2);

            modelBuilder.Entity<WMatTurn>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WMatTurn>()
                .Property(e => e.CalcAmount)
                .HasPrecision(37, 15);

            modelBuilder.Entity<ProfilesSetting>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<EnterpriseAccount>()
                .Property(e => e.AccNum)
                .IsUnicode(false);

            modelBuilder.Entity<KagentList>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<KagentList>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<KagentList>()
                .Property(e => e.District)
                .IsUnicode(false);

            modelBuilder.Entity<KagentList>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<KagentList>()
                .Property(e => e.PostIndex)
                .IsUnicode(false);

            modelBuilder.Entity<KagentList>()
                .Property(e => e.FullUrADDR)
                .IsUnicode(false);

            modelBuilder.Entity<KagentList>()
                .Property(e => e.FullFactADDR)
                .IsUnicode(false);

            modelBuilder.Entity<KagentList>()
                .Property(e => e.StartSaldo)
                .HasPrecision(15, 8);

            modelBuilder.Entity<KagentList>()
                .Property(e => e.PriceName)
                .IsUnicode(false);

            modelBuilder.Entity<KagentList>()
                .Property(e => e.Saldo)
                .HasPrecision(38, 2);

            modelBuilder.Entity<MaterialsList>()
                .Property(e => e.NDS)
                .HasPrecision(15, 8);

            modelBuilder.Entity<v_Actives>()
                .Property(e => e.WhSumm)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_Actives>()
                .Property(e => e.Creditors)
                .HasPrecision(38, 2);

            modelBuilder.Entity<v_Actives>()
                .Property(e => e.Debitors)
                .HasPrecision(38, 2);

            modelBuilder.Entity<v_Actives>()
                .Property(e => e.Cash)
                .HasPrecision(38, 2);

            modelBuilder.Entity<v_Actives>()
                .Property(e => e.CashLess)
                .HasPrecision(38, 2);

            modelBuilder.Entity<v_Actives>()
                .Property(e => e.Active)
                .HasPrecision(38, 2);

            modelBuilder.Entity<v_DiscCards>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_EnterpriseList>()
                .Property(e => e.StartSaldo)
                .HasPrecision(15, 8);

            modelBuilder.Entity<v_KAgentAccount>()
                .Property(e => e.AccNum)
                .IsUnicode(false);

            modelBuilder.Entity<v_KAgentSaldo>()
                .Property(e => e.Saldo)
                .HasPrecision(38, 2);

            modelBuilder.Entity<v_MatRemains>()
                .Property(e => e.Remain)
                .HasPrecision(38, 4);

            modelBuilder.Entity<v_MatRemains>()
                .Property(e => e.Rsv)
                .HasPrecision(38, 4);

            modelBuilder.Entity<v_MatRemains>()
                .Property(e => e.AvgPrice)
                .HasPrecision(38, 6);

            modelBuilder.Entity<v_MatRemains>()
                .Property(e => e.MinPrice)
                .HasPrecision(38, 10);

            modelBuilder.Entity<v_MatRemains>()
                .Property(e => e.MaxPrice)
                .HasPrecision(38, 10);

            modelBuilder.Entity<v_MatRemains>()
                .Property(e => e.Ordered)
                .HasPrecision(38, 4);

            modelBuilder.Entity<v_MatRemains>()
                .Property(e => e.ORsv)
                .HasPrecision(38, 4);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.Total)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.DocNum)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 8);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.Schet)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.District)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.PostIndex)
                .IsUnicode(false);

            modelBuilder.Entity<v_PayDoc>()
                .Property(e => e.AccNum)
                .IsUnicode(false);

            modelBuilder.Entity<v_PriceTypes>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<v_PriceTypes>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 8);

            modelBuilder.Entity<v_PriceTypes>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<v_ProductionPlanDet>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_ProductionPlanDet>()
                .Property(e => e.Remain)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_ProductionPlanDet>()
                .Property(e => e.Total)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_ProductionPlanDet>()
                .Property(e => e.RecipeAmount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_ProductionPlanDet>()
                .Property(e => e.ResipeOut)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_Services>()
                .Property(e => e.Price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_Services>()
                .Property(e => e.Norm)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_TechProcDet>()
                .Property(e => e.Out)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_TechProcDet>()
                .Property(e => e.TareWeight)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_TechProcDet>()
                .Property(e => e.OutNetto)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.SummAll)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.Nds)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.SummPay)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.SummInCurr)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.District)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.Region)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.PostIndex)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.CType)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.EntAddress)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.EntCity)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.EntDistrict)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.EntRegion)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.EntPostIndex)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.EntCType)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.AddressSel)
                .IsUnicode(false);

            modelBuilder.Entity<v_WaybillList>()
                .Property(e => e.AddressBuy)
                .IsUnicode(false);

            modelBuilder.Entity<v_WhMatRemains>()
                .Property(e => e.Remain)
                .HasPrecision(38, 4);

            modelBuilder.Entity<v_WhMatRemains>()
                .Property(e => e.Rsv)
                .HasPrecision(38, 4);

            modelBuilder.Entity<v_WhMatRemains>()
                .Property(e => e.AvgPrice)
                .HasPrecision(38, 6);

            modelBuilder.Entity<v_WhMatRemains>()
                .Property(e => e.MinPrice)
                .HasPrecision(38, 10);

            modelBuilder.Entity<v_WhMatRemains>()
                .Property(e => e.MaxPrice)
                .HasPrecision(38, 10);

            modelBuilder.Entity<v_WhMatRemains>()
                .Property(e => e.Ordered)
                .HasPrecision(38, 4);

            modelBuilder.Entity<v_WhMatRemains>()
                .Property(e => e.ORsv)
                .HasPrecision(38, 4);
        }
    }
}
