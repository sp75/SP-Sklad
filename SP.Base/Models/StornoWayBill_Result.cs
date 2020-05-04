namespace SP.Base.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class StornoWayBill_Result
    {
        [Key]
        public Nullable<int> Checked { get; set; }
        public string ErrorMessage { get; set; }
    }
}
