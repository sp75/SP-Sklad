//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SP_Sklad.SkladData
{
    using System;
    using System.Collections.Generic;
    
    public partial class KaAddr
    {
        public int AddrId { get; set; }
        public int KaId { get; set; }
        public int AddrType { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string PostIndex { get; set; }
        public string Region { get; set; }
        public Nullable<int> CityType { get; set; }
    
        public virtual Kagent Kagent { get; set; }
    }
}
