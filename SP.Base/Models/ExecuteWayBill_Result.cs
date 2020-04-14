namespace SP.Base.Models
{
    using System;

    public partial class ExecuteWayBill_Result
    {
        public Nullable<int> Checked { get; set; }
        public Nullable<System.Guid> NewDocId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
