using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.Domain.Common
{
    public class User
    {
        [JsonPropertyName("id")]
        public Int32 Id { get; set; }
        [JsonPropertyName("login")]
        public String Login { get; set; }
        [JsonPropertyName("mail")]
        public String Mail { get; set; }
        [JsonPropertyName("pass")]
        public String Password { get; set; }
        [JsonPropertyName("short_name")]
        public String ShortName { get; set; }
        [JsonPropertyName("user_position")]
        public UserPosition UserPosition { get; set; } = new UserPosition();
        [JsonPropertyName("user_place_of_work")]
        public UserPlaceOfWork UserPlaceOfWork { get; set; } = new UserPlaceOfWork();
        [JsonPropertyName("user_roles")]
        public UserRole UserRole { get; set; }
        [JsonPropertyName("token")]
        public String token { get; set; }
    }
}
