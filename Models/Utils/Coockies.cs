using CustomrsFinder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersFinder.Models.Utils
{
    public class Coockies : Controller
    {
        private IocContainer dependency = null;
        public Coockies(IocContainer dependency) => this.dependency = dependency;

        public void Set(HttpContext httpContext, String name, String value, Int32 days)
        {
            httpContext.Response.Cookies.Append(name, value,
                new CookieOptions
                {
                    MaxAge = TimeSpan.FromDays(days)
                });
        }

        public String Get(HttpContext httpContext, String name)
        {
            return httpContext.Request.Cookies[name];
        }
    }
}
