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
    
    public partial class WaybillTemplateDet
    {
        public System.Guid Id { get; set; }
        public System.Guid WaybillTemplateId { get; set; }
        public int MatId { get; set; }
        public Nullable<int> Num { get; set; }
        public string Notes { get; set; }
        public Nullable<int> WId { get; set; }
    
        public virtual Materials Materials { get; set; }
        public virtual WaybillTemplate WaybillTemplate { get; set; }
    }
}
