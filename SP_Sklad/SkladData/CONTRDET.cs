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
    
    public partial class CONTRDET
    {
        public int CDID { get; set; }
        public Nullable<int> MATID { get; set; }
        public Nullable<decimal> AMOUNT { get; set; }
        public Nullable<decimal> PRICE { get; set; }
        public Nullable<int> CURRID { get; set; }
        public Nullable<decimal> NDS { get; set; }
        public Nullable<decimal> SHIPPEDAMOUNT { get; set; }
        public int CONTRID { get; set; }
    
        public virtual CONTRACTS CONTRACTS { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Materials Materials { get; set; }
    }
}
