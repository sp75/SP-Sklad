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
    
    public partial class v_RawMaterialManagement
    {
        public System.Guid Id { get; set; }
        public string Num { get; set; }
        public System.DateTime OnDate { get; set; }
        public string Notes { get; set; }
        public string WhName { get; set; }
        public string PersonName { get; set; }
        public int Checked { get; set; }
        public int DocType { get; set; }
        public string DocTypeName { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string KaName { get; set; }
    }
}
