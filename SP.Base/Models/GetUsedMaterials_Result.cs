using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class GetUsedMaterials_Result
    {
        [Key]
        public Nullable<int> KaId { get; set; }
        public string KaName { get; set; }
        public Nullable<decimal> Remain { get; set; }
        public string MsrName { get; set; }
        public string MatName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string InvNumber { get; set; }
    }
}
