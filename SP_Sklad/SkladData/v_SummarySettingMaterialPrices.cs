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
    
    public partial class v_SummarySettingMaterialPrices
    {
        public int MatId { get; set; }
        public int PTypeId { get; set; }
        public string Name { get; set; }
        public string MsrName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string GrpName { get; set; }
        public int GrpId { get; set; }
        public Nullable<decimal> GrpNum { get; set; }
        public Nullable<System.DateTime> LastDocDate { get; set; }
        public string Artikul { get; set; }
    }
}
