using System;
using System.ComponentModel.DataAnnotations;

namespace SP.Base.Models
{
    public partial class REP_33_Result
    {
        [Key]
        public Guid Id { get; set; }
        public string Num { get; set; }
        public System.DateTime OnDate { get; set; }
        public Nullable<decimal> SummAll { get; set; }
        public string RecipeName { get; set; }
        public string MatName { get; set; }
        public decimal AmountIn { get; set; }
        public int MatId { get; set; }
        public string MsrName { get; set; }
        public int MId { get; set; }
        public Nullable<System.DateTime> OnDateOut { get; set; }
        public Nullable<decimal> AmountOut { get; set; }
        public Nullable<System.DateTime> WriteOnDate { get; set; }
        public Nullable<decimal> Price { get; set; }
        public decimal OutPlan { get; set; }
        public Nullable<decimal> HeatTreatmentAmount { get; set; }
        public Nullable<decimal> CookingAmount { get; set; }
        public Nullable<decimal> FormationAmount { get; set; }
        public Nullable<decimal> FinishAmount { get; set; }
        public decimal? ThermoLossOut { get; set; }
        public decimal? ThermoLossDeviation { get; set; }
        public decimal? Deviation { get; set; }
        public Nullable<decimal> OsadkaAmount { get; set; }
        public Nullable<decimal> AmountWater { get; set; }
    }
}
