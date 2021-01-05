using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomersFinder.Models.Domain
{
    public class PositionEvents
    {
        //[JsonPropertyName("id")]
        public int id { get; set; }
        //[JsonPropertyName("name")]
        public string name { get; set; }
        //[JsonPropertyName("customer_info_id")]
        public int customer_info_id { get; set; }
        //[JsonPropertyName("user_id")]
        public int user_id { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
    }
}
