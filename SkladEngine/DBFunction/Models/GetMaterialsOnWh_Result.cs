﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkladEngine.DBFunction.Models
{
    public class GetMaterialsOnWh_Result
    {
        public int MatId { get; set; }
        public int WId { get; set; }
        public string MatName { get; set; }
        public string MsrName { get; set; }
        public decimal Remain { get; set; }
        public decimal Rsv { get; set; }
        public decimal CurRemain { get; set; }
    }
}