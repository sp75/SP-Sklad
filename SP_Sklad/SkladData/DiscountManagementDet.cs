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
    
    public partial class DiscountManagementDet
    {
        public System.Guid Id { get; set; }
        public System.Guid DiscountManagementId { get; set; }
        public int KaId { get; set; }
        public int MatId { get; set; }
        public decimal Discount { get; set; }
    
        public virtual Materials Materials { get; set; }
        public virtual Kagent Kagent { get; set; }
    }
}
