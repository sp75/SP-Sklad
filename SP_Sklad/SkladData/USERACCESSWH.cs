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
    
    public partial class UserAccessWh
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WId { get; set; }
        public bool UseReceived { get; set; }
    
        public virtual Warehouse Warehouse { get; set; }
        public virtual Users Users { get; set; }
    }
}
