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
    
    public partial class WayBillTmc
    {
        public int PosId { get; set; }
        public int Num { get; set; }
        public Nullable<int> Checked { get; set; }
        public int WbillId { get; set; }
        public int MatId { get; set; }
        public decimal Amount { get; set; }
        public int TurnType { get; set; }
        public Nullable<decimal> CalcAmount { get; set; }
    
        public virtual Materials Materials { get; set; }
        public virtual WaybillList WaybillList { get; set; }
    }
}