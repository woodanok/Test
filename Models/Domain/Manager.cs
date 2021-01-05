using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersFinder.Models.Domain
{
    public class Manager
    {
        public Int32 id { get; set; }
        public String full_name { get; set; }
        public String short_name { get; set; }
        public String phone_number { get; set; }
        public String mail { get; set; }
    }
}
