﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class OrderedList_Result
    {
        [Key]
        public System.Guid Id { get; set; }
        public int PosId { get; set; }
        public int WbillId { get; set; }
        public int WType { get; set; }
        public string Num { get; set; }
        public System.DateTime OnDate { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<int> KaId { get; set; }
        public string KaName { get; set; }
        public int WId { get; set; }
        public string WhName { get; set; }
        public int MatId { get; set; }
        public string MatName { get; set; }
        public string Artikul { get; set; }
        public decimal Amount { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> OnValue { get; set; }
        public Nullable<int> CurrId { get; set; }
        public string CurrencyName { get; set; }
        public int Checked { get; set; }
        public string MsrName { get; set; }
        public Nullable<int> GrpId { get; set; }
        public string BarCode { get; set; }
        public string TypeName { get; set; }
    }
}
