//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkladData.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class v_TechProcDet
    {
        public int DetId { get; set; }
        public int WbillId { get; set; }
        public decimal Out { get; set; }
        public int ProcId { get; set; }
        public string Notes { get; set; }
        public Nullable<int> PersonId { get; set; }
        public System.DateTime OnDate { get; set; }
        public string Name { get; set; }
        public string PersonName { get; set; }
    }
}