using CustomrsFinder.Models;
using CustomrsFinder.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Receiver.Models;
using Receiver.Models.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Controllers
{
    public class AccountController : Controller
    {
        private IocContainer dependency = null;
        IWebHostEnvironment appEnvironment = null;
        public AccountController(CustomerFinderDB searchDB, IWebHostEnvironment appEnvironment)
        {
            dependency = new IocContainer(searchDB);
            this.appEnvironment = appEnvironment;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginModel model)
        {
            model.Login = model.Login.ToLower().Trim();
            model.Password = model.Password.ToLower().Trim();

            var user = await dependency.GetUserService().GetUserByNameAndPassAsync(model);

            if (user.Id != 0)
            {

                HttpContext.Response.Cookies.Append("JWToken", user.token,
                new CookieOptions
                {
                    MaxAge = TimeSpan.FromDays(7)
                });

                HttpContext.Response.Cookies.Append("userId", user.Id.ToString(), new CookieOptions
                {
                    MaxAge = TimeSpan.FromDays(7)
                });



                switch (user.UserRole.name)
                {
                    case "finderOfCustomers":
                        return RedirectToAction("Index", "CustomerFinder");
                    case "boss":
                        return RedirectToAction("Index", "CustomerFinder");
                    case "receiverController":
                        return RedirectToAction("Index", "CustomerFinder");
                    case "admin":
                        return RedirectToAction("Index", "CustomerFinder");
                }
            }

            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Logoff()
        {
            HttpContext.Response.Cookies.Delete("JWToken");
            HttpContext.Response.Cookies.Delete("userId");
            return Redirect("~/Account/Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CheckSession()
        {
            var token = HttpContext.Request.Cookies["JWToken"];
            if (String.IsNullOrEmpty(token))
            {
                var pathToFile = Path.Combine(appEnvironment.WebRootPath, "files/log.txt");
                var prevStr = await WriteReadLog.Read(pathToFile);
                var newStr = "- " + $"SessionEnd {DateTime.Now.ToString()}" + Environment.NewLine + prevStr;
                WriteReadLog.Write(pathToFile, newStr);
                return Json(new { result = "SessionEnd", url = Url.Action("Index", "Account") });
            }
            return Json("Ok");
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        //[Route("GetToken")]
        public IActionResult GetToken()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (String.IsNullOrEmpty(token)) return Ok("failed");
            else return Ok(HttpContext.Session.GetString("JWToken"));
        }
    }
}
