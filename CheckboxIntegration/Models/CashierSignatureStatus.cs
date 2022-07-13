using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
    public class CashierSignatureStatus
    {
        public bool online { get; set; }
        public SignatureType type { get; set; }
        public bool shift_open_possibility { get; set; }
        public ErrorMessage error { get; set; }
    }
}
