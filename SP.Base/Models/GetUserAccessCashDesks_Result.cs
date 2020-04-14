namespace SP.Base.Models
{
    using System;

    public partial class GetUserAccessCashDesks_Result
    {
        public int CashId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Def { get; set; }
        public Nullable<int> EnterpriseId { get; set; }
        public int Allow { get; set; }
    }
}
