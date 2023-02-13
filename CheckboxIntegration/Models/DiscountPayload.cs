using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
    public class DiscountPayload
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public DiscountType type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DiscountMode mode { get; set; }

        public decimal value { get; set; }
    }
}
