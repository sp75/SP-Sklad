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
    
    public partial class v_BankStatements
    {
        public System.Guid Id { get; set; }
        public string Num { get; set; }
        public System.DateTime OnDate { get; set; }
        public string Notes { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.Guid> SessionId { get; set; }
        public Nullable<int> EntId { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public string PersonName { get; set; }
        public Nullable<int> PersonId { get; set; }
        public int Checked { get; set; }
        public string AccNum { get; set; }
        public string ChargeTypeName { get; set; }
    }
}
