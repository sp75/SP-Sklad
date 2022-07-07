using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
   public class ErrorMessage
    {
        public List<Detail> detail { get; set; }
        public string message { get; set; }
    }

    public class Detail
    {
        public List<string> loc { get; set; }
        public string msg { get; set; }
        public string type { get; set; }
    }
}
