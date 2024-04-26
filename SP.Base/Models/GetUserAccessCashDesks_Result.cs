namespace SP.Base.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;


    public partial class GetUserAccessCashDesks_Result
    {
        [Key]
        public int CashId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Def { get; set; }
        public Nullable<int> KaId { get; set; }
        public int Allow { get; set; }
    }
}
