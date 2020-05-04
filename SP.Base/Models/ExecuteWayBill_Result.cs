namespace SP.Base.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class ExecuteWayBill_Result
    {
        [Key]
        public Nullable<int> Checked { get; set; }
        public Nullable<System.Guid> NewDocId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
