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
    
    public partial class Cars
    {
        public Cars()
        {
            this.Routes = new HashSet<Routes>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public Nullable<int> DriverId { get; set; }
    
        public virtual ICollection<Routes> Routes { get; set; }
        public virtual Kagent Kagent { get; set; }
    }
}