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
    
    public partial class GetTradeWayBillList_Result
    {
        public System.Guid Id { get; set; }
        public int WbillId { get; set; }
        public string Num { get; set; }
        public System.DateTime OnDate { get; set; }
        public Nullable<int> CurrId { get; set; }
        public string Reason { get; set; }
        public int Checked { get; set; }
        public Nullable<decimal> SummAll { get; set; }
        public int WType { get; set; }
        public Nullable<decimal> nds { get; set; }
        public Nullable<int> PersonId { get; set; }
        public Nullable<decimal> SummPay { get; set; }
        public string Notes { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<decimal> SummInCurr { get; set; }
        public string CurrName { get; set; }
        public Nullable<decimal> CurrRate { get; set; }
        public string KaName { get; set; }
        public Nullable<int> KaId { get; set; }
        public string KaFullName { get; set; }
        public string KaPhone { get; set; }
        public string PersonName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
        public string PostIndex { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string www { get; set; }
        public Nullable<int> KType { get; set; }
        public string AttNum { get; set; }
        public Nullable<System.DateTime> AttDate { get; set; }
        public string Received { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public int DefNum { get; set; }
        public Nullable<int> EntId { get; set; }
        public string EntName { get; set; }
        public string KagentGroupName { get; set; }
        public string DocType { get; set; }
        public Nullable<int> PTypeId { get; set; }
        public string PTypeName { get; set; }
        public Nullable<System.DateTime> ShipmentDate { get; set; }
        public Nullable<System.DateTime> ReportingDate { get; set; }
        public Nullable<System.Guid> ReceiptId { get; set; }
        public string FiscalCode { get; set; }
        public string CustomerName { get; set; }
        public Nullable<decimal> Balans { get; set; }
    }
}