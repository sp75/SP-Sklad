﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Sklad.IntermediateWeighingInterface.Views
{
    public class make_det
    {
        public int Rn { get; set; }
        public string MatName { get; set; }
        public string MsrName { get; set; }
        public decimal? AmountByRecipe { get; set; }
        public decimal? TotalWeighted { get; set; }
        public int MatId { get; set; }
        public int WbillId { get; set; }
        public int? RecipeCount { get; set; }
        public int IntermediateWeighingCount { get; set; }
        public decimal? TotalWeightByRecipe { get; set; }
        public int? RecId { get; set; }
        public Guid IntermediateWeighingId { get; set; }
        public string IntermediateWeighingNum { get; set; }
        public Guid? IntermediateWeighingDetId { get; set; }
        public string IntermediateWeighingDetTotal { get; set; }
        public byte[] img { get; set; }
        public decimal? IntermediateWeighingAmount { get; set; }
    }
}
