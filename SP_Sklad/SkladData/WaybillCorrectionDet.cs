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
    
    public partial class WaybillCorrectionDet
    {
        public System.Guid Id { get; set; }
        public int Checked { get; set; }
        public Nullable<int> OldMatId { get; set; }
        public Nullable<int> NewMatId { get; set; }
        public Nullable<decimal> OldPrice { get; set; }
        public Nullable<decimal> NewPrice { get; set; }
        public int PosId { get; set; }
        public string Notes { get; set; }
        public Nullable<System.Guid> WaybillCorrectionId { get; set; }
        public Nullable<decimal> OldDiscount { get; set; }
        public Nullable<decimal> NewDiscount { get; set; }
    
        public virtual Materials Materials { get; set; }
        public virtual Materials Materials1 { get; set; }
        public virtual WaybillCorrection WaybillCorrection { get; set; }
        public virtual WaybillDet WaybillDet { get; set; }
    }
}
