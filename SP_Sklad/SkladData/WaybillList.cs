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
    
    public partial class WaybillList
    {
        public WaybillList()
        {
            this.Commission = new HashSet<Commission>();
            this.DeboningDet = new HashSet<DeboningDet>();
            this.TechProcDet = new HashSet<TechProcDet>();
            this.WaybillDet = new HashSet<WaybillDet>();
            this.WayBillDetAddProps = new HashSet<WayBillDetAddProps>();
            this.WayBillSvc = new HashSet<WayBillSvc>();
            this.WayBillMakeProps = new HashSet<WayBillMakeProps>();
            this.WayBillTmc = new HashSet<WayBillTmc>();
            this.IntermediateWeighing = new HashSet<IntermediateWeighing>();
            this.AdditionalCostsDet = new HashSet<AdditionalCostsDet>();
            this.DeboningWeighing = new HashSet<DeboningWeighing>();
            this.ExpeditionDet = new HashSet<ExpeditionDet>();
        }
    
        public int WbillId { get; set; }
        public string Num { get; set; }
        public System.DateTime OnDate { get; set; }
        public Nullable<int> KaId { get; set; }
        public Nullable<int> CurrId { get; set; }
        public string AttNum { get; set; }
        public Nullable<System.DateTime> AttDate { get; set; }
        public string Reason { get; set; }
        public int Checked { get; set; }
        public int Deleted { get; set; }
        public Nullable<decimal> SummAll { get; set; }
        public int WType { get; set; }
        public Nullable<decimal> Nds { get; set; }
        public Nullable<int> PersonId { get; set; }
        public string Received { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public int DefNum { get; set; }
        public Nullable<decimal> SummPay { get; set; }
        public string Notes { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<decimal> SummInCurr { get; set; }
        public Nullable<int> EntId { get; set; }
        public Nullable<decimal> OnValue { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.Guid> SessionId { get; set; }
        public System.Guid Id { get; set; }
        public string WebUserId { get; set; }
        public Nullable<int> PTypeId { get; set; }
        public Nullable<System.Guid> CarId { get; set; }
        public Nullable<int> RouteId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> DriverId { get; set; }
        public Nullable<System.DateTime> ReportingDate { get; set; }
        public Nullable<System.DateTime> ShipmentDate { get; set; }
        public Nullable<System.Guid> ReceiptId { get; set; }
        public Nullable<int> AdditionalDocTypeId { get; set; }
        public Nullable<int> DeliveredWaybillId { get; set; }
    
        public virtual ICollection<Commission> Commission { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual ICollection<DeboningDet> DeboningDet { get; set; }
        public virtual ICollection<TechProcDet> TechProcDet { get; set; }
        public virtual ICollection<WaybillDet> WaybillDet { get; set; }
        public virtual ICollection<WayBillDetAddProps> WayBillDetAddProps { get; set; }
        public virtual WayBillMake WayBillMake { get; set; }
        public virtual WaybillMove WaybillMove { get; set; }
        public virtual ICollection<WayBillSvc> WayBillSvc { get; set; }
        public virtual ICollection<WayBillMakeProps> WayBillMakeProps { get; set; }
        public virtual ICollection<WayBillTmc> WayBillTmc { get; set; }
        public virtual ICollection<IntermediateWeighing> IntermediateWeighing { get; set; }
        public virtual ICollection<AdditionalCostsDet> AdditionalCostsDet { get; set; }
        public virtual Kagent Kagent { get; set; }
        public virtual Kagent Kontragent { get; set; }
        public virtual Kagent Kagent2 { get; set; }
        public virtual ICollection<DeboningWeighing> DeboningWeighing { get; set; }
        public virtual ICollection<ExpeditionDet> ExpeditionDet { get; set; }
        public virtual AdditionalDocType AdditionalDocType { get; set; }
    }
}
