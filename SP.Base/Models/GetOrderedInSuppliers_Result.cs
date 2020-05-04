namespace SP.Base.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class GetOrderedInSuppliers_Result
    {
        [Key]
        public System.DateTime OnDate { get; set; }
        public string Name { get; set; }
    }
}
