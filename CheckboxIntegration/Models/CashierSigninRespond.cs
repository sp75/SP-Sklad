﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
    public class CashierSigninRespond
    {
        public string type { get; set; }
        public string token_type { get; set; }
        public string access_token { get; set; }
        public ErrorMessage error { get; set; }

    }
}
