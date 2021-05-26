using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class REP_23_Result
    {
        [Key]
        public Guid Id { get; set; }
        public string DocNum { get; set; }
        public System.DateTime OnDate { get; set; }
        public Nullable<int> KaId { get; set; }
        public string KaName { get; set; }
        public decimal Total { get; set; }
        public string ChargeName { get; set; }
        public int CTypeId { get; set; }
        public string EmployeeName { get; set; }
        public int DocType { get; set; }
        public string PayTypeName { get; set; }
    }
}
