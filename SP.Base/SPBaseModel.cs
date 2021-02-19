namespace SP.Base.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;

    public partial class SPBaseModel : DbContext  
    {
        public SPBaseModel(string connection_string)
            : base(connection_string) 
        {
        }
        public SPBaseModel()
           : base("name=SPBaseModel")
        {
        }

        public virtual DbSet<AccountType> AccountType { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
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
        public virtual DbSet<CurrencyRate> CurrencyRate { get; set; }
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
        public virtual DbSet<IntermediateWeighing> IntermediateWeighing { get; set; }
        public virtual DbSet<IntermediateWeighingDet> IntermediateWeighingDet { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<KaAddr> KaAddr { get; set; }
        public virtual DbSet<KADiscount> KADiscount { get; set; }
        public virtual DbSet<Kagent> Kagent { get; set; }
        public virtual DbSet<KAgentAccount> KAgentAccount { get; set; }
        public virtual DbSet<KAgentActReconciliation> KAgentActReconciliation { get; set; }
        public virtual DbSet<KAgentAdjustment> KAgentAdjustment { get; set; }
        public virtual DbSet<KAgentAdjustmentDet> KAgentAdjustmentDet { get; set; }
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
        public virtual DbSet<MaterialMeasures> MaterialMeasures { get; set; }
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
        public virtual DbSet<OperationTypes> OperationTypes { get; set; }
        public virtual DbSet<OperLog> OperLog { get; set; }
        public virtual DbSet<OrderedRels> OrderedRels { get; set; }
        public virtual DbSet<Packaging> Packaging { get; set; }
        public virtual DbSet<PayDoc> PayDoc { get; set; }
        public virtual DbSet<PayDocType> PayDocType { get; set; }
        public virtual DbSet<PayType> PayType { get; set; }
        public virtual DbSet<PlannedCalculation> PlannedCalculation { get; set; }
        public virtual DbSet<PlannedCalculationDet> PlannedCalculationDet { get; set; }
        public virtual DbSet<PosRel> PosRel { get; set; }
        public virtual DbSet<PosRemains> PosRemains { get; set; }
        public virtual DbSet<PriceList> PriceList { get; set; }
        public virtual DbSet<PriceListDet> PriceListDet { get; set; }
        public virtual DbSet<PriceTypes> PriceTypes { get; set; }
        public virtual DbSet<PrintLog> PrintLog { get; set; }
        public virtual DbSet<ProductionPlanDet> ProductionPlanDet { get; set; }
        public virtual DbSet<ProductionPlans> ProductionPlans { get; set; }
        public virtual DbSet<PROFCOMMON> PROFCOMMON { get; set; }
        public virtual DbSet<ProfileDocSetting> ProfileDocSetting { get; set; }
        public virtual DbSet<ProfilesSetting> ProfilesSetting { get; set; }
        public virtual DbSet<RepLng> RepLng { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<ReportSortedFields> ReportSortedFields { get; set; }
        public virtual DbSet<ReturnRel> ReturnRel { get; set; }
        public virtual DbSet<RouteList> RouteList { get; set; }
        public virtual DbSet<RouteListDet> RouteListDet { get; set; }
        public virtual DbSet<Routes> Routes { get; set; }
        public virtual DbSet<SchedulingOrders> SchedulingOrders { get; set; }
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
        public virtual DbSet<UserAccessCashDesks> UserAccessCashDesks { get; set; }
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
        public virtual DbSet<WayBillTmc> WayBillTmc { get; set; }
        public virtual DbSet<WhMatRemains> WhMatRemains { get; set; }
        public virtual DbSet<WMatTurn> WMatTurn { get; set; }
        public virtual DbSet<WriteOffTypes> WriteOffTypes { get; set; }
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
        public virtual DbSet<v_IntermediateWeighingDet> v_IntermediateWeighingDet { get; set; }
        public virtual DbSet<v_KAgentAccount> v_KAgentAccount { get; set; }
        public virtual DbSet<v_KAgentAdjustment> v_KAgentAdjustment { get; set; }
        public virtual DbSet<v_KAgentAdjustmentDet> v_KAgentAdjustmentDet { get; set; }
        public virtual DbSet<v_KAgentDocs> v_KAgentDocs { get; set; }
        public virtual DbSet<v_KAgentSaldo> v_KAgentSaldo { get; set; }
        public virtual DbSet<v_MatRecipe> v_MatRecipe { get; set; }
        public virtual DbSet<v_MatRemains> v_MatRemains { get; set; }
        public virtual DbSet<v_PayDoc> v_PayDoc { get; set; }
        public virtual DbSet<v_PlannedCalculation> v_PlannedCalculation { get; set; }
        public virtual DbSet<v_PlannedCalculationDetDet> v_PlannedCalculationDetDet { get; set; }
        public virtual DbSet<v_PosRemains> v_PosRemains { get; set; }
        public virtual DbSet<v_PriceList> v_PriceList { get; set; }
        public virtual DbSet<v_PriceTypes> v_PriceTypes { get; set; }
        public virtual DbSet<v_ProductionPlanDet> v_ProductionPlanDet { get; set; }
        public virtual DbSet<v_Services> v_Services { get; set; }
        public virtual DbSet<v_TechProcDet> v_TechProcDet { get; set; }
        public virtual DbSet<v_Users> v_Users { get; set; }
        public virtual DbSet<v_WaybillList> v_WaybillList { get; set; }
        public virtual DbSet<v_WorkDate> v_WorkDate { get; set; }

        public virtual DbSet<REP_1_Result> REP_1_Result { get; set; }
      
        public virtual DbSet<REP_2_Result> REP_2_Result { get; set; }



        public virtual ObjectResult<REP_1_Result> REP_1(DateTime from_date, DateTime to_date, int? grp_id, int? ka_id, string wh, string doc_types, int? user_id)
        {
            var from_dateParameter = new ObjectParameter("from_date", from_date);
            var to_dateParameter = new ObjectParameter("to_date", to_date);
            var grp_idParameter = grp_id.HasValue ? new ObjectParameter("grp_id", grp_id) : new ObjectParameter("grp_id", typeof(int));
            var ka_idParameter = ka_id.HasValue ? new ObjectParameter("ka_id", ka_id) : new ObjectParameter("ka_id", typeof(int));
            var whParameter = wh != null ? new ObjectParameter("wh", wh) : new ObjectParameter("wh", typeof(string));
            var doc_typesParameter = doc_types != null ? new ObjectParameter("doc_types", doc_types) : new ObjectParameter("doc_types", typeof(string));
            var user_idParameter = user_id.HasValue ? new ObjectParameter("user_id", user_id) : new ObjectParameter("user_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<REP_1_Result>("REP_1", from_dateParameter, to_dateParameter, grp_idParameter, ka_idParameter, whParameter, doc_typesParameter, user_idParameter);
        }

        public virtual ObjectResult<REP_2_Result> REP_2(DateTime from_date, DateTime to_date, int? grp_id, int? ka_id, string wh, string doc_types, int? wb_status, int? user_id)
        {
            var from_dateParameter = new ObjectParameter("from_date", from_date);
            var to_dateParameter = new ObjectParameter("to_date", to_date);

            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var whParameter = wh != null ?
                new ObjectParameter("wh", wh) :
                new ObjectParameter("wh", typeof(string));

            var doc_typesParameter = doc_types != null ?
                new ObjectParameter("doc_types", doc_types) :
                new ObjectParameter("doc_types", typeof(string));

            var wb_statusParameter = wb_status.HasValue ?
                new ObjectParameter("wb_status", wb_status) :
                new ObjectParameter("wb_status", typeof(int));

            var user_idParameter = user_id.HasValue ?
                new ObjectParameter("user_id", user_id) :
                new ObjectParameter("user_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<REP_2_Result>("REP_2", from_dateParameter, to_dateParameter, grp_idParameter, ka_idParameter, whParameter, doc_typesParameter, wb_statusParameter, user_idParameter);
        }

        public virtual DbSet<WhMatGet_Result> WhMatGet_Result { get; set; }
        [DbFunction("SPBaseModel", "WhMatGet")]
        public virtual IQueryable<WhMatGet_Result> WhMatGet(Nullable<int> grp_id, Nullable<int> wid, Nullable<int> ka_id, Nullable<System.DateTime> on_date, Nullable<int> get_empty, string wh, Nullable<int> show_all_mats, string grp, Nullable<int> user_id, Nullable<int> get_child_node)
        {
            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var widParameter = wid.HasValue ?
                new ObjectParameter("wid", wid) :
                new ObjectParameter("wid", typeof(int));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var on_dateParameter = on_date.HasValue ?
                new ObjectParameter("on_date", on_date) :
                new ObjectParameter("on_date", typeof(System.DateTime));

            var get_emptyParameter = get_empty.HasValue ?
                new ObjectParameter("get_empty", get_empty) :
                new ObjectParameter("get_empty", typeof(int));

            var whParameter = wh != null ?
                new ObjectParameter("wh", wh) :
                new ObjectParameter("wh", typeof(string));

            var show_all_matsParameter = show_all_mats.HasValue ?
                new ObjectParameter("show_all_mats", show_all_mats) :
                new ObjectParameter("show_all_mats", typeof(int));

            var grpParameter = grp != null ?
                new ObjectParameter("grp", grp) :
                new ObjectParameter("grp", typeof(string));

            var user_idParameter = user_id.HasValue ?
                new ObjectParameter("user_id", user_id) :
                new ObjectParameter("user_id", typeof(int));

            var get_child_nodeParameter = get_child_node.HasValue ?
                new ObjectParameter("get_child_node", get_child_node) :
                new ObjectParameter("get_child_node", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<WhMatGet_Result>("[SPBaseModel].[WhMatGet](@grp_id, @wid, @ka_id, @on_date, @get_empty, @wh, @show_all_mats, @grp, @user_id, @get_child_node)", grp_idParameter, widParameter, ka_idParameter, on_dateParameter, get_emptyParameter, whParameter, show_all_matsParameter, grpParameter, user_idParameter, get_child_nodeParameter);
        }

        public virtual DbSet<REP_3_14_Result> REP_3_14_Result { get; set; }
        public virtual ObjectResult<REP_3_14_Result> REP_3_14(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> grp_id, Nullable<int> ka_id, string wh, string doc_types, Nullable<int> user_id, Nullable<System.Guid> ka_grp_id)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var whParameter = wh != null ?
                new ObjectParameter("wh", wh) :
                new ObjectParameter("wh", typeof(string));

            var doc_typesParameter = doc_types != null ?
                new ObjectParameter("doc_types", doc_types) :
                new ObjectParameter("doc_types", typeof(string));

            var user_idParameter = user_id.HasValue ?
                new ObjectParameter("user_id", user_id) :
                new ObjectParameter("user_id", typeof(int));

            var ka_grp_idParameter = ka_grp_id.HasValue ?
                new ObjectParameter("ka_grp_id", ka_grp_id) :
                new ObjectParameter("ka_grp_id", typeof(System.Guid));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<REP_3_14_Result>("REP_3_14", from_dateParameter, to_dateParameter, grp_idParameter, ka_idParameter, whParameter, doc_typesParameter, user_idParameter, ka_grp_idParameter);
        }

        public virtual DbSet<REP_4_25_Result> REP_4_25_Result { get; set; }
        public virtual ObjectResult<REP_4_25_Result> REP_4_25(DateTime? from_date, Nullable<System.DateTime> to_date, Nullable<int> grp_id, Nullable<int> ka_id, string wh, string doc_types, Nullable<int> user_id)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var whParameter = wh != null ?
                new ObjectParameter("wh", wh) :
                new ObjectParameter("wh", typeof(string));

            var doc_typesParameter = doc_types != null ?
                new ObjectParameter("doc_types", doc_types) :
                new ObjectParameter("doc_types", typeof(string));

            var user_idParameter = user_id.HasValue ?
                new ObjectParameter("user_id", user_id) :
                new ObjectParameter("user_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<REP_4_25_Result>("REP_4_25", from_dateParameter, to_dateParameter, grp_idParameter, ka_idParameter, whParameter, doc_typesParameter, user_idParameter);
        }

        public virtual DbSet<REP_4_5_Result> REP_4_5_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_4_5")]
        public virtual IQueryable<REP_4_5_Result> REP_4_5(Nullable<System.DateTime> on_date)
        {
            var on_dateParameter = on_date.HasValue ?
                new ObjectParameter("on_date", on_date) :
                new ObjectParameter("on_date", typeof(System.DateTime));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_4_5_Result>("[SPBaseModel].[REP_4_5](@on_date)", on_dateParameter);
        }

        public virtual DbSet<REP_10_Result> REP_10_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_10")]
        public virtual IQueryable<REP_10_Result> REP_10(DateTime? from_date, DateTime? to_date, int? grp_id, string wh, int? show_all_mat, int? user_id)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var whParameter = wh != null ?
                new ObjectParameter("wh", wh) :
                new ObjectParameter("wh", typeof(string));

            var show_all_matParameter = show_all_mat.HasValue ?
                new ObjectParameter("show_all_mat", show_all_mat) :
                new ObjectParameter("show_all_mat", typeof(int));

            var user_idParameter = user_id.HasValue ?
                new ObjectParameter("user_id", user_id) :
                new ObjectParameter("user_id", typeof(int));
            var dddd = GetType().Name;
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_10_Result>("[SPBaseModel].[REP_10](@from_date, @to_date, @grp_id, @wh, @show_all_mat, @user_id)", from_dateParameter, to_dateParameter, grp_idParameter, whParameter, show_all_matParameter, user_idParameter);
        }

        public virtual DbSet<GetDocList_Result> GetDocList_Result { get; set; }
        [DbFunction("SPBaseModel", "GetDocList")]
        public virtual IQueryable<GetDocList_Result> GetDocList(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> ka_id, Nullable<int> w_type)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var w_typeParameter = w_type.HasValue ?
                new ObjectParameter("w_type", w_type) :
                new ObjectParameter("w_type", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetDocList_Result>("[SPBaseModel].[GetDocList](@from_date, @to_date, @ka_id, @w_type)", from_dateParameter, to_dateParameter, ka_idParameter, w_typeParameter);
        }

        public virtual DbSet<GetMatMove_Result> GetMatMove_Result { get; set; }
        [DbFunction("SPBaseModel", "GetMatMove")]
        public virtual IQueryable<GetMatMove_Result> GetMatMove(int? mat_id, DateTime? from_date, DateTime? to_date, int? wid, int? ka_id, int? w_type, string wh, Guid? ka_grp_id, int? user_id)
        {
            var mat_idParameter = mat_id.HasValue ?
                new ObjectParameter("mat_id", mat_id) :
                new ObjectParameter("mat_id", typeof(int));

            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(DateTime));

            var widParameter = wid.HasValue ?
                new ObjectParameter("wid", wid) :
                new ObjectParameter("wid", typeof(int));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var w_typeParameter = w_type.HasValue ?
                new ObjectParameter("w_type", w_type) :
                new ObjectParameter("w_type", typeof(int));

            var whParameter = wh != null ?
                new ObjectParameter("wh", wh) :
                new ObjectParameter("wh", typeof(string));

            var ka_grp_idParameter = ka_grp_id.HasValue ?
                new ObjectParameter("ka_grp_id", ka_grp_id) :
                new ObjectParameter("ka_grp_id", typeof(Guid));

            var user_idParameter = user_id.HasValue ?
                new ObjectParameter("user_id", user_id) :
                new ObjectParameter("user_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetMatMove_Result>("[SPBaseModel].[GetMatMove](@mat_id, @from_date, @to_date, @wid, @ka_id, @w_type, @wh, @ka_grp_id, @user_id)", mat_idParameter, from_dateParameter, to_dateParameter, widParameter, ka_idParameter, w_typeParameter, whParameter, ka_grp_idParameter, user_idParameter);
        }

      
        public virtual DbSet<GetWayBillDetOut_Result> GetWayBillDetOut_Result { get; set; }
        [DbFunction("SPBaseModel", "GetWayBillDetOut")]
        public virtual IQueryable<GetWayBillDetOut_Result> GetWayBillDetOut(Nullable<int> wbill_id)
        {
            var wbill_idParameter = wbill_id.HasValue ?
                new ObjectParameter("wbill_id", wbill_id) :
                new ObjectParameter("wbill_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetWayBillDetOut_Result>("[SPBaseModel].[GetWayBillDetOut](@wbill_id)", wbill_idParameter);
        }

        public virtual DbSet<REP_13_Result> REP_13_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_13")]
        public virtual IQueryable<REP_13_Result> REP_13(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> grp_id, Nullable<int> ka_id, string wh, Nullable<int> only_return, string grp)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var whParameter = wh != null ?
                new ObjectParameter("wh", wh) :
                new ObjectParameter("wh", typeof(string));

            var only_returnParameter = only_return.HasValue ?
                new ObjectParameter("only_return", only_return) :
                new ObjectParameter("only_return", typeof(int));

            var grpParameter = grp != null ?
                new ObjectParameter("grp", grp) :
                new ObjectParameter("grp", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_13_Result>("[SPBaseModel].[REP_13](@from_date, @to_date, @grp_id, @ka_id, @wh, @only_return, @grp)", from_dateParameter, to_dateParameter, grp_idParameter, ka_idParameter, whParameter, only_returnParameter, grpParameter);
        }

        public virtual DbSet<REP_15_Result> REP_15_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_15")]
        public virtual IQueryable<REP_15_Result> REP_15(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> ka_id, Nullable<int> mat_id)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var mat_idParameter = mat_id.HasValue ?
                new ObjectParameter("mat_id", mat_id) :
                new ObjectParameter("mat_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_15_Result>("[SPBaseModel].[REP_15](@from_date, @to_date, @ka_id, @mat_id)", from_dateParameter, to_dateParameter, ka_idParameter, mat_idParameter);
        }

        public virtual DbSet<REP_16_Result> REP_16_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_16")]
        public virtual IQueryable<REP_16_Result> REP_16(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> ka_id, Nullable<int> ctype, Nullable<int> showall)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var ctypeParameter = ctype.HasValue ?
                new ObjectParameter("ctype", ctype) :
                new ObjectParameter("ctype", typeof(int));

            var showallParameter = showall.HasValue ?
                new ObjectParameter("showall", showall) :
                new ObjectParameter("showall", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_16_Result>("[SPBaseModel].[REP_16](@from_date, @to_date, @ka_id, @ctype, @showall)", from_dateParameter, to_dateParameter, ka_idParameter, ctypeParameter, showallParameter);
        }

        public virtual DbSet<REP_17_Result> REP_17_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_17")]
        public virtual IQueryable<REP_17_Result> REP_17(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> grp_id, Nullable<int> ka_id)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_17_Result>("[SPBaseModel].[REP_17](@from_date, @to_date, @grp_id, @ka_id)", from_dateParameter, to_dateParameter, grp_idParameter, ka_idParameter);
        }

        public virtual DbSet<REP_20_Result> REP_20_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_20")]
        public virtual IQueryable<REP_20_Result> REP_20(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> grp_id, Nullable<int> ka_id)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_20_Result>("[SPBaseModel].[REP_20](@from_date, @to_date, @grp_id, @ka_id)", from_dateParameter, to_dateParameter, grp_idParameter, ka_idParameter);
        }

        public virtual DbSet<MatRemainByWh_Result> MatRemainByWh_Result { get; set; }
        [DbFunction("SPBaseModel", "MatRemainByWh")]
        public virtual IQueryable<MatRemainByWh_Result> MatRemainByWh(Nullable<int> mat_id, Nullable<int> w_id, Nullable<int> ka_id, Nullable<System.DateTime> on_date, string wh, Nullable<int> user_id)
        {
            var mat_idParameter = mat_id.HasValue ?
                new ObjectParameter("mat_id", mat_id) :
                new ObjectParameter("mat_id", typeof(int));

            var w_idParameter = w_id.HasValue ?
                new ObjectParameter("w_id", w_id) :
                new ObjectParameter("w_id", typeof(int));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var on_dateParameter = on_date.HasValue ?
                new ObjectParameter("on_date", on_date) :
                new ObjectParameter("on_date", typeof(System.DateTime));

            var whParameter = wh != null ?
                new ObjectParameter("wh", wh) :
                new ObjectParameter("wh", typeof(string));

            var user_idParameter = user_id.HasValue ?
                new ObjectParameter("user_id", user_id) :
                new ObjectParameter("user_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<MatRemainByWh_Result>("[SPBaseModel].[MatRemainByWh](@mat_id, @w_id, @ka_id, @on_date, @wh, @user_id)", mat_idParameter, w_idParameter, ka_idParameter, on_dateParameter, whParameter, user_idParameter);
        }


        public virtual DbSet<GetPayDocList_Result> GetPayDocList_Result { get; set; }
        public virtual ObjectResult<GetPayDocList_Result> GetPayDocList(string doc_type, DateTime? from_date, DateTime? to_date, int? ka_id, int? @checked, int? pay_type, int? person_id)
        {
            var doc_typeParameter = doc_type != null ?
                new ObjectParameter("doc_type", doc_type) :
                new ObjectParameter("doc_type", typeof(string));

            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var checkedParameter = @checked.HasValue ?
                new ObjectParameter("checked", @checked) :
                new ObjectParameter("checked", typeof(int));

            var pay_typeParameter = pay_type.HasValue ?
                new ObjectParameter("pay_type", pay_type) :
                new ObjectParameter("pay_type", typeof(int));

            var person_idParameter = person_id.HasValue ?
                new ObjectParameter("person_id", person_id) :
                new ObjectParameter("person_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetPayDocList_Result>("GetPayDocList", doc_typeParameter, from_dateParameter, to_dateParameter, ka_idParameter, checkedParameter, pay_typeParameter, person_idParameter);
        }

        public virtual DbSet<WBListMake_Result> WBListMake_Result { get; set; }
        public virtual ObjectResult<WBListMake_Result> WBListMake(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> is_checked, string wh, Nullable<int> grp_id, Nullable<int> w_type)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var is_checkedParameter = is_checked.HasValue ?
                new ObjectParameter("is_checked", is_checked) :
                new ObjectParameter("is_checked", typeof(int));

            var whParameter = wh != null ?
                new ObjectParameter("wh", wh) :
                new ObjectParameter("wh", typeof(string));

            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var w_typeParameter = w_type.HasValue ?
                new ObjectParameter("w_type", w_type) :
                new ObjectParameter("w_type", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WBListMake_Result>("WBListMake", from_dateParameter, to_dateParameter, is_checkedParameter, whParameter, grp_idParameter, w_typeParameter);
        }

        public virtual DbSet<StornoWayBill_Result> StornoWayBill_Result { get; set; }
        public virtual ObjectResult<StornoWayBill_Result> StornoWayBill(Nullable<int> wbill_id)
        {
            var wbill_idParameter = wbill_id.HasValue ?
                new ObjectParameter("wbill_id", wbill_id) :
                new ObjectParameter("wbill_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<StornoWayBill_Result>("StornoWayBill", wbill_idParameter);
        }

        public virtual DbSet<ExecuteWayBill_Result> ExecuteWayBill_Result { get; set; }
        public virtual ObjectResult<ExecuteWayBill_Result> ExecuteWayBill(Nullable<int> wBILLID, Nullable<int> nEW_WTYPE, Nullable<int> person_id)
        {
            var wBILLIDParameter = wBILLID.HasValue ?
                new ObjectParameter("WBILLID", wBILLID) :
                new ObjectParameter("WBILLID", typeof(int));

            var nEW_WTYPEParameter = nEW_WTYPE.HasValue ?
                new ObjectParameter("NEW_WTYPE", nEW_WTYPE) :
                new ObjectParameter("NEW_WTYPE", typeof(int));

            var person_idParameter = person_id.HasValue ?
                new ObjectParameter("person_id", person_id) :
                new ObjectParameter("person_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ExecuteWayBill_Result>("ExecuteWayBill", wBILLIDParameter, nEW_WTYPEParameter, person_idParameter);
        }

        public virtual DbSet<GetOrderedInSuppliers_Result> GetOrderedInSuppliers_Result { get; set; }
        public virtual ObjectResult<GetOrderedInSuppliers_Result> GetOrderedInSuppliers(Nullable<int> wbill_id)
        {
            var wbill_idParameter = wbill_id.HasValue ?
                new ObjectParameter("wbill_id", wbill_id) :
                new ObjectParameter("wbill_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOrderedInSuppliers_Result>("GetOrderedInSuppliers", wbill_idParameter);
        }


        public virtual DbSet<GetUserAccessCashDesks_Result> GetUserAccessCashDesks_Result { get; set; }
        [DbFunction("SPBaseModel", "GetUserAccessCashDesks")]
        public virtual IQueryable<GetUserAccessCashDesks_Result> GetUserAccessCashDesks(Nullable<int> user_id)
        {
            var user_idParameter = user_id.HasValue ?
                new ObjectParameter("user_id", user_id) :
                new ObjectParameter("user_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetUserAccessCashDesks_Result>("[SPBaseModel].[GetUserAccessCashDesks](@user_id)", user_idParameter);
        }

        public virtual DbSet<REP_27_Result> REP_27_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_27")]
        public virtual IQueryable<REP_27_Result> REP_27(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> ka_id, Nullable<int> grp_id, Nullable<int> mat_id, Nullable<System.Guid> ka_grp_id, Nullable<int> person_id)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var mat_idParameter = mat_id.HasValue ?
                new ObjectParameter("mat_id", mat_id) :
                new ObjectParameter("mat_id", typeof(int));

            var ka_grp_idParameter = ka_grp_id.HasValue ?
                new ObjectParameter("ka_grp_id", ka_grp_id) :
                new ObjectParameter("ka_grp_id", typeof(System.Guid));

            var person_idParameter = person_id.HasValue ?
                new ObjectParameter("person_id", person_id) :
                new ObjectParameter("person_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_27_Result>("[SPBaseModel].[REP_27](@from_date, @to_date, @ka_id, @grp_id, @mat_id, @ka_grp_id, @person_id)", from_dateParameter, to_dateParameter, ka_idParameter, grp_idParameter, mat_idParameter, ka_grp_idParameter, person_idParameter);
        }

        public virtual DbSet<OrderedList_Result> OrderedList_Result { get; set; }
        [DbFunction("SPBaseModel", "OrderedList")]
        public virtual IQueryable<OrderedList_Result> OrderedList(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> mat_id, Nullable<int> ka_id, Nullable<int> w_type, Nullable<int> active, Nullable<System.Guid> ka_grp_id)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var mat_idParameter = mat_id.HasValue ?
                new ObjectParameter("mat_id", mat_id) :
                new ObjectParameter("mat_id", typeof(int));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var w_typeParameter = w_type.HasValue ?
                new ObjectParameter("w_type", w_type) :
                new ObjectParameter("w_type", typeof(int));

            var activeParameter = active.HasValue ?
                new ObjectParameter("active", active) :
                new ObjectParameter("active", typeof(int));

            var ka_grp_idParameter = ka_grp_id.HasValue ?
                new ObjectParameter("ka_grp_id", ka_grp_id) :
                new ObjectParameter("ka_grp_id", typeof(System.Guid));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<OrderedList_Result>("[SPBaseModel].[OrderedList](@from_date, @to_date, @mat_id, @ka_id, @w_type, @active, @ka_grp_id)", from_dateParameter, to_dateParameter, mat_idParameter, ka_idParameter, w_typeParameter, activeParameter, ka_grp_idParameter);
        }

        public virtual DbSet<REP_29_Result> REP_29_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_29")]
        public virtual IQueryable<REP_29_Result> REP_29(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> ka_id, Nullable<int> grp_id, string wh)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var whParameter = wh != null ?
                new ObjectParameter("wh", wh) :
                new ObjectParameter("wh", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_29_Result>("[SPBaseModel].[REP_29](@from_date, @to_date, @ka_id, @grp_id, @wh)", from_dateParameter, to_dateParameter, ka_idParameter, grp_idParameter, whParameter);
        }

        public virtual DbSet<REP_31_Result> REP_31_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_31")]
        public virtual IQueryable<REP_31_Result> REP_31(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> grp_id, Nullable<int> mat_id)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var mat_idParameter = mat_id.HasValue ?
                new ObjectParameter("mat_id", mat_id) :
                new ObjectParameter("mat_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_31_Result>("[SPBaseModel].[REP_31](@from_date, @to_date, @grp_id, @mat_id)", from_dateParameter, to_dateParameter, grp_idParameter, mat_idParameter);
        }

        public virtual DbSet<REP_32_Result> REP_32_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_32")]
        public virtual IQueryable<REP_32_Result> REP_32(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_32_Result>("[SPBaseModel].[REP_32](@from_date, @to_date)", from_dateParameter, to_dateParameter);
        }

        public virtual DbSet<MoneyOnDate_Result> MoneyOnDate_Result { get; set; }
        [DbFunction("SPBaseModel", "MoneyOnDate")]
        public virtual IQueryable<MoneyOnDate_Result> MoneyOnDate(DateTime? on_date)
        {
            var on_dateParameter = on_date.HasValue ?
                new ObjectParameter("on_date", on_date) :
                new ObjectParameter("on_date", typeof(System.DateTime));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<MoneyOnDate_Result>("[SPBaseModel].[MoneyOnDate](@on_date)", on_dateParameter);
        }



        public virtual DbSet<REP_33_Result> REP_33_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_33")]
        public virtual IQueryable<REP_33_Result> REP_33(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> grp_id, Nullable<int> mat_id)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var mat_idParameter = mat_id.HasValue ?
                new ObjectParameter("mat_id", mat_id) :
                new ObjectParameter("mat_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_33_Result>("[SPBaseModel].[REP_33](@from_date, @to_date, @grp_id, @mat_id)", from_dateParameter, to_dateParameter, grp_idParameter, mat_idParameter);
        }

        public virtual DbSet<REP_35_Result> REP_35_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_35")]
        public virtual IQueryable<REP_35_Result> REP_35(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> grp_id, Nullable<int> mat_id)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var mat_idParameter = mat_id.HasValue ?
                new ObjectParameter("mat_id", mat_id) :
                new ObjectParameter("mat_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_35_Result>("[SPBaseModel].[REP_35](@from_date, @to_date, @grp_id, @mat_id)", from_dateParameter, to_dateParameter, grp_idParameter, mat_idParameter);
        }

        public virtual DbSet<REP_37_Result> REP_37_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_37")]
        public virtual IQueryable<REP_37_Result> REP_37(Nullable<int> wh_id, Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date)
        {
            var wh_idParameter = wh_id.HasValue ?
                new ObjectParameter("wh_id", wh_id) :
                new ObjectParameter("wh_id", typeof(int));

            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_37_Result>("[SPBaseModel].[REP_37](@wh_id, @from_date, @to_date)", wh_idParameter, from_dateParameter, to_dateParameter);
        }

        public virtual DbSet<REP_39_Result> REP_39_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_39")]
        public virtual IQueryable<REP_39_Result> REP_39(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, Nullable<int> grp_id, Nullable<int> ka_id, string wh, string doc_types, Nullable<int> user_id, Nullable<System.Guid> ka_grp_id)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var grp_idParameter = grp_id.HasValue ?
                new ObjectParameter("grp_id", grp_id) :
                new ObjectParameter("grp_id", typeof(int));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var whParameter = wh != null ?
                new ObjectParameter("wh", wh) :
                new ObjectParameter("wh", typeof(string));

            var doc_typesParameter = doc_types != null ?
                new ObjectParameter("doc_types", doc_types) :
                new ObjectParameter("doc_types", typeof(string));

            var user_idParameter = user_id.HasValue ?
                new ObjectParameter("user_id", user_id) :
                new ObjectParameter("user_id", typeof(int));

            var ka_grp_idParameter = ka_grp_id.HasValue ?
                new ObjectParameter("ka_grp_id", ka_grp_id) :
                new ObjectParameter("ka_grp_id", typeof(System.Guid));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_39_Result>("[SPBaseModel].[REP_39](@from_date, @to_date, @grp_id, @ka_id, @wh, @doc_types, @user_id, @ka_grp_id)", from_dateParameter, to_dateParameter, grp_idParameter, ka_idParameter, whParameter, doc_typesParameter, user_idParameter, ka_grp_idParameter);
        }

        public virtual DbSet<REP_41_Result> REP_41_Result { get; set; }
        [DbFunction("SPBaseModel", "REP_41")]
        public virtual IQueryable<REP_41_Result> REP_41(DateTime? from_date, Guid? ka_grp_id)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var ka_grp_idParameter = ka_grp_id.HasValue ?
                new ObjectParameter("ka_grp_id", ka_grp_id) :
                new ObjectParameter("ka_grp_id", typeof(System.Guid));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<REP_41_Result>("[SPBaseModel].[REP_41](@from_date, @ka_grp_id)", from_dateParameter, ka_grp_idParameter);
        }

        public virtual DbSet<GetUsedMaterials_Result> GetUsedMaterials_Result { get; set; }
        [DbFunction("SPBaseModel", "GetUsedMaterials")]
        public virtual IQueryable<GetUsedMaterials_Result> GetUsedMaterials(Nullable<int> mat_id, Nullable<System.DateTime> on_date, Nullable<int> ka_id)
        {
            var mat_idParameter = mat_id.HasValue ?
                new ObjectParameter("mat_id", mat_id) :
                new ObjectParameter("mat_id", typeof(int));

            var on_dateParameter = on_date.HasValue ?
                new ObjectParameter("on_date", on_date) :
                new ObjectParameter("on_date", typeof(System.DateTime));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetUsedMaterials_Result>("[SPBaseModel].[GetUsedMaterials](@mat_id, @on_date, @ka_id)", mat_idParameter, on_dateParameter, ka_idParameter);
        }

        public virtual DbSet<GetWayBillList_Result> GetWayBillList_Result { get; set; }
        [DbFunction("SPBaseModel", "GetWayBillList")]
        public virtual IQueryable<GetWayBillList_Result> GetWayBillList(Nullable<System.DateTime> from_date, Nullable<System.DateTime> to_date, string w_type, Nullable<int> @checked, Nullable<int> ka_id, Nullable<int> show_null_balance, string wh, Nullable<int> person_id)
        {
            var from_dateParameter = from_date.HasValue ?
                new ObjectParameter("from_date", from_date) :
                new ObjectParameter("from_date", typeof(System.DateTime));

            var to_dateParameter = to_date.HasValue ?
                new ObjectParameter("to_date", to_date) :
                new ObjectParameter("to_date", typeof(System.DateTime));

            var w_typeParameter = w_type != null ?
                new ObjectParameter("w_type", w_type) :
                new ObjectParameter("w_type", typeof(string));

            var checkedParameter = @checked.HasValue ?
                new ObjectParameter("checked", @checked) :
                new ObjectParameter("checked", typeof(int));

            var ka_idParameter = ka_id.HasValue ?
                new ObjectParameter("ka_id", ka_id) :
                new ObjectParameter("ka_id", typeof(int));

            var show_null_balanceParameter = show_null_balance.HasValue ?
                new ObjectParameter("show_null_balance", show_null_balance) :
                new ObjectParameter("show_null_balance", typeof(int));

            var whParameter = wh != null ?
                new ObjectParameter("wh", wh) :
                new ObjectParameter("wh", typeof(string));

            var person_idParameter = person_id.HasValue ?
                new ObjectParameter("person_id", person_id) :
                new ObjectParameter("person_id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetWayBillList_Result>("[SPBaseModel].[GetWayBillList](@from_date, @to_date, @w_type, @checked, @ka_id, @show_null_balance, @wh, @person_id)", from_dateParameter, to_dateParameter, w_typeParameter, checkedParameter, ka_idParameter, show_null_balanceParameter, whParameter, person_idParameter);
        }

        public virtual ObjectResult<string> GetDocNum(string name)
        {
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("GetDocNum", nameParameter);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new CodeFirstStoreFunctions.FunctionsConvention<SPBaseModel>("dbo"));

            modelBuilder.Entity<AccountType>()
                .HasMany(e => e.KAgentAccount)
                .WithRequired(e => e.AccountType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

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
                .HasMany(e => e.CurrencyRate)
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

            modelBuilder.Entity<CurrencyRate>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

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

            modelBuilder.Entity<IntermediateWeighingDet>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<IntermediateWeighingDet>()
                .Property(e => e.TaraAmount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<IntermediateWeighingDet>()
                .Property(e => e.Total)
                .HasPrecision(15, 4);

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
                .HasMany(e => e.IntermediateWeighing)
                .WithRequired(e => e.Kagent)
                .HasForeignKey(e => e.PersonId)
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
                .HasMany(e => e.KAgentActReconciliation)
                .WithOptional(e => e.Kagent)
                .HasForeignKey(e => e.DebtKaId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.KAgentActReconciliation1)
                .WithOptional(e => e.Kagent1)
                .HasForeignKey(e => e.CreditKaId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.KAgentActReconciliation2)
                .WithOptional(e => e.Kagent2)
                .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.KAgentAdjustment)
                .WithOptional(e => e.Kagent)
                .HasForeignKey(e => e.DebtKaId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.KAgentAdjustment1)
                .WithOptional(e => e.Kagent1)
                .HasForeignKey(e => e.CreditKaId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.KAgentAdjustment2)
                .WithOptional(e => e.Kagent2)
                .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.ProductionPlans)
                .WithOptional(e => e.Kagent)
                .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<Kagent>()
                .HasMany(e => e.ProductionPlans1)
                .WithOptional(e => e.Kagent1)
                .HasForeignKey(e => e.EntId);

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

            modelBuilder.Entity<Kagent>()
              .HasMany(e => e.PriceList)
              .WithMany(e => e.Kagent)
              .Map(m => m.ToTable("PriceListKagent").MapLeftKey("KagentId").MapRightKey("PriceListId"));

            modelBuilder.Entity<KAgentAccount>()
                .Property(e => e.AccNum)
                .IsUnicode(false);

            modelBuilder.Entity<KAgentActReconciliation>()
                .Property(e => e.SummAll)
                .HasPrecision(15, 2);

            modelBuilder.Entity<KAgentActReconciliation>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<KAgentActReconciliation>()
                .Property(e => e.SummInCurr)
                .HasPrecision(15, 2);

            modelBuilder.Entity<KAgentAdjustment>()
                .Property(e => e.SummAll)
                .HasPrecision(15, 2);

            modelBuilder.Entity<KAgentAdjustment>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<KAgentAdjustment>()
                .Property(e => e.SummInCurr)
                .HasPrecision(15, 2);

            modelBuilder.Entity<KAgentAdjustment>()
                .HasMany(e => e.KAgentAdjustmentDet)
                .WithOptional(e => e.KAgentAdjustment)
                .WillCascadeOnDelete();

            modelBuilder.Entity<KAgentAdjustmentDet>()
                .Property(e => e.Saldo)
                .HasPrecision(15, 2);

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

            modelBuilder.Entity<MaterialMeasures>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Materials>()
                .Property(e => e.MinReserv)
                .HasPrecision(15, 2);

            modelBuilder.Entity<Materials>()
                .Property(e => e.Weight)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Materials>()
                .Property(e => e.MSize)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Materials>()
                .Property(e => e.NDS)
                .HasPrecision(15, 2);

            modelBuilder.Entity<Materials>()
                .HasMany(e => e.DeboningDet)
                .WithRequired(e => e.Materials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materials>()
                .HasMany(e => e.IntermediateWeighingDet)
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
                .HasMany(e => e.WayBillTmc)
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
                .Property(e => e.Deviation)
                .HasPrecision(15, 4);

            modelBuilder.Entity<MatRecipe>()
                .HasMany(e => e.PlannedCalculationDet)
                .WithRequired(e => e.MatRecipe)
                .WillCascadeOnDelete(false);

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
                .HasMany(e => e.MaterialMeasures)
                .WithRequired(e => e.Measures)
                .WillCascadeOnDelete(false);

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

            modelBuilder.Entity<OperationTypes>()
                .HasMany(e => e.KAgentAdjustment)
                .WithRequired(e => e.OperationTypes)
                .HasForeignKey(e => e.OperationType)
                .WillCascadeOnDelete(false);

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
                .HasPrecision(15, 4);

            modelBuilder.Entity<PayDoc>()
                .Property(e => e.Schet)
                .IsUnicode(false);

            modelBuilder.Entity<PayType>()
                .HasMany(e => e.PayDoc)
                .WithRequired(e => e.PayType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlannedCalculationDet>()
                .Property(e => e.ProductionPlan)
                .HasPrecision(15, 4);

            modelBuilder.Entity<PlannedCalculationDet>()
                .Property(e => e.PlannedProfitability)
                .HasPrecision(15, 4);

            modelBuilder.Entity<PlannedCalculationDet>()
                .Property(e => e.RecipeOut)
                .HasPrecision(15, 4);

            modelBuilder.Entity<PlannedCalculationDet>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<PlannedCalculationDet>()
                .Property(e => e.SalesPrice)
                .HasPrecision(15, 2);

            modelBuilder.Entity<PlannedCalculationDet>()
                .Property(e => e.RecipePrice)
                .HasPrecision(15, 2);

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

            modelBuilder.Entity<PosRemains>()
                .Property(e => e.ActualRemain)
                .HasPrecision(17, 4);

            modelBuilder.Entity<PriceListDet>()
                .Property(e => e.Price)
                .HasPrecision(15, 4);

            modelBuilder.Entity<PriceListDet>()
                .Property(e => e.Discount)
                .HasPrecision(15, 2);

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

            modelBuilder.Entity<ProfilesSetting>()
                .Property(e => e.Name)
                .IsUnicode(false);

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

            modelBuilder.Entity<SchedulingOrders>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

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
                .HasMany(e => e.ProductionPlans)
                .WithOptional(e => e.Warehouse)
                .HasForeignKey(e => e.WhId);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.ProductionPlans1)
                .WithOptional(e => e.Warehouse1)
                .HasForeignKey(e => e.ManufId);

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
                .HasMany(e => e.OrderedRels)
                .WithRequired(e => e.WaybillDet)
                .HasForeignKey(e => e.OrdPosId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WaybillDet>()
                .HasMany(e => e.OrderedRels1)
                .WithRequired(e => e.WaybillDet1)
                .HasForeignKey(e => e.OutPosId);

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

            modelBuilder.Entity<WayBillTmc>()
                .Property(e => e.Amount)
                .HasPrecision(15, 2);

            modelBuilder.Entity<WayBillTmc>()
                .Property(e => e.CalcAmount)
                .HasPrecision(26, 2);

            modelBuilder.Entity<WhMatRemains>()
                .Property(e => e.Remain)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WhMatRemains>()
                .Property(e => e.Rsv)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WhMatRemains>()
                .Property(e => e.AvgPrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WhMatRemains>()
                .Property(e => e.MinPrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WhMatRemains>()
                .Property(e => e.MaxPrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WhMatRemains>()
                .Property(e => e.Ordered)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WhMatRemains>()
                .Property(e => e.ORsv)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WMatTurn>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<WMatTurn>()
                .Property(e => e.CalcAmount)
                .HasPrecision(37, 15);

            modelBuilder.Entity<WriteOffTypes>()
                .HasMany(e => e.KAgentAdjustment)
                .WithRequired(e => e.WriteOffTypes)
                .HasForeignKey(e => e.WriteOffType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EnterpriseAccount>()
                .Property(e => e.AccNum)
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
                .HasPrecision(15, 2);

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

            modelBuilder.Entity<v_IntermediateWeighingDet>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_IntermediateWeighingDet>()
                .Property(e => e.TaraAmount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_IntermediateWeighingDet>()
                .Property(e => e.Total)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_KAgentAccount>()
                .Property(e => e.AccNum)
                .IsUnicode(false);

            modelBuilder.Entity<v_KAgentAdjustment>()
                .Property(e => e.SummAll)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_KAgentAdjustment>()
                .Property(e => e.SummInCurr)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_KAgentAdjustmentDet>()
                .Property(e => e.SummAll)
                .HasPrecision(21, 8);

            modelBuilder.Entity<v_KAgentAdjustmentDet>()
                .Property(e => e.SummInCurr)
                .HasPrecision(33, 8);

            modelBuilder.Entity<v_KAgentAdjustmentDet>()
                .Property(e => e.SummPay)
                .HasPrecision(33, 8);

            modelBuilder.Entity<v_KAgentAdjustmentDet>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_KAgentAdjustmentDet>()
                .Property(e => e.Saldo)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_KAgentDocs>()
                .Property(e => e.Nds)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_KAgentDocs>()
                .Property(e => e.SummAll)
                .HasPrecision(21, 8);

            modelBuilder.Entity<v_KAgentDocs>()
                .Property(e => e.SummInCurr)
                .HasPrecision(33, 8);

            modelBuilder.Entity<v_KAgentDocs>()
                .Property(e => e.OnValue)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_KAgentDocs>()
                .Property(e => e.SummPay)
                .HasPrecision(33, 8);

            modelBuilder.Entity<v_KAgentSaldo>()
                .Property(e => e.Saldo)
                .HasPrecision(38, 2);

            modelBuilder.Entity<v_MatRecipe>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_MatRecipe>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<v_MatRecipe>()
                .Property(e => e.GrpName)
                .IsUnicode(false);

            modelBuilder.Entity<v_MatRecipe>()
                .Property(e => e.Out)
                .HasPrecision(15, 4);

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
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_MatRemains>()
                .Property(e => e.MaxPrice)
                .HasPrecision(15, 4);

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

            modelBuilder.Entity<v_PlannedCalculationDetDet>()
                .Property(e => e.ProductionPlan)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_PlannedCalculationDetDet>()
                .Property(e => e.PlannedProfitability)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_PlannedCalculationDetDet>()
                .Property(e => e.RecipeOut)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_PlannedCalculationDetDet>()
                .Property(e => e.Amount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_PlannedCalculationDetDet>()
                .Property(e => e.RecipeCount)
                .HasPrecision(38, 0);

            modelBuilder.Entity<v_PlannedCalculationDetDet>()
                .Property(e => e.RecipePrice)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_PlannedCalculationDetDet>()
                .Property(e => e.SalesPrice)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_PlannedCalculationDetDet>()
                .Property(e => e.Price)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_PlannedCalculationDetDet>()
                .Property(e => e.Profitability)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_PlannedCalculationDetDet>()
                .Property(e => e.PlansPrice)
                .HasPrecision(15, 2);

            modelBuilder.Entity<v_PlannedCalculationDetDet>()
                .Property(e => e.MatGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<v_PosRemains>()
                .Property(e => e.Remain)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_PosRemains>()
                .Property(e => e.Rsv)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_PosRemains>()
                .Property(e => e.AvgPrice)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_PosRemains>()
                .Property(e => e.Ordered)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_PosRemains>()
                .Property(e => e.OrderedRsv)
                .HasPrecision(15, 4);

            modelBuilder.Entity<v_PosRemains>()
                .Property(e => e.ActualRemain)
                .HasPrecision(17, 4);

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
        }
    }
}
