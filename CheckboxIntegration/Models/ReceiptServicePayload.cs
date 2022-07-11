using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
    public class ReceiptServicePayload
    {
        public Guid id { get; set; }
        public Payment payment { get; set; }
    }
}
