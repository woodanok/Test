using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomrsFinder.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "ISSUER"; 
        public const string AUDIENCE = "AUDIENCE"; 
        const string KEY = "KEY!";   
        public const int LIFETIME = 480; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
