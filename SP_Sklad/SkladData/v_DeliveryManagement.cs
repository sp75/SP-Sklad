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
    
    public partial class v_DeliveryManagement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> DriverId { get; set; }
        public Nullable<System.Guid> CarId { get; set; }
        public Nullable<long> Duration { get; set; }
        public string CarName { get; set; }
        public string CarNumber { get; set; }
        public Nullable<decimal> СarryingСapacity { get; set; }
        public string DriverName { get; set; }
        public Nullable<decimal> ParcentСarryingСapacity { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
    }
}
