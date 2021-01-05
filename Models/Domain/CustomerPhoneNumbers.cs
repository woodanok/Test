using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomersFinder.Models.Domain
{
    public class CustomerPhoneNumbers
    {
        public Int32 id { get; set; }
        public String number { get; set; }
        public Int32 is_actual_id { get; set; }
        public Int32 customer_info_id { get; set; }
    }
}
