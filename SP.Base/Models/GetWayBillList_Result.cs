﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace SP.Base.Models
{
    public partial class GetWayBillList_Result
    {
        [Key]
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
        public Nullable<decimal> Balans { get; set; }
        public string DocType { get; set; }
        public string SourceWhName { get; set; }
        public string DestinationWhName { get; set; }
    }
}
