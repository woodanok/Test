using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CustomersFinder.Models.Domain;
using CustomersFinder.Models.Enums;
using CustomersFinder.Models.Poco;
using CustomrsFinder.Models;
using CustomrsFinder.Models.Domain;
using CustomrsFinder.Models.Emuns;
using CustomrsFinder.Models.Utils;
using CustomrsFinder.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace CustomrsFinder.Controllers
{
    public class CustomerFinderController : Controller
    {
        private IocContainer dependency = null;
        IWebHostEnvironment appEnvironment = null;
        public CustomerFinderController(CustomerFinderDB db, IWebHostEnvironment appEnvironment)
        {
            dependency = new IocContainer(db);
            this.appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var filters = new Filters();
            filters.CustomerTypes = Convert.ToInt32(dependency.GetCoockiesService().Get(HttpContext, "filters.CustomerTypes"));
            var model = await dependency.GetCustomerFinderModelService().GetCustomerFinderModel(1, filters);
            var token = HttpContext.Request.Cookies["JWToken"];
            var userId = Convert.ToInt32(HttpContext.Request.Cookies["userId"]);
            ViewBag.user = await dependency.GetUserService().GetUserByIdAsync(userId, token);
            return View(model);
        }

        //[Authorize(Roles = "admin, receiver, boss, receiverController", AuthenticationSchemes = "Bearer")]
        [HttpGet]
        //[Route("GetPositions")]
        public async Task<IActionResult> GetPositions(Int32 currentPage)
        {
            var result = await dependency.GetCustomerFinderModelService().GetCustomerFinderModel(currentPage);
            return PartialView("TableBody", result);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            var path =  Path.Combine("files", uploadedFile.FileName);
            var filePath = Path.Combine(appEnvironment.WebRootPath, path);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }

            var parseExcel = await dependency.GetWorkWithExcel().ReadFromExcel<List<CustomerInfoFromExcel>>(filePath, GetNewFieldNameByOldName);
            FileInfo file = new FileInfo(filePath);
            if (file.Exists) file.Delete();

            await dependency.GetCustomerInfoService().Insert(parseExcel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SetFilter([FromBody] JToken obj)
        {
            var filters = new Filters();
            filters.CustomerTypes = (Int32)obj["customerType"];
            dependency.GetCoockiesService().Set(HttpContext, "filters.CustomerTypes", filters.CustomerTypes.ToString(), 364);
            var result = await dependency.GetCustomerFinderModelService().GetCustomerFinderModel(1, filters); 
            return PartialView("TableBody", result);
        }

        [HttpGet]
        public IActionResult GetFilter()
        {
            var filters = new Filters();
            filters.CustomerTypes = Convert.ToInt32(dependency.GetCoockiesService().Get(HttpContext, "filters.CustomerTypes"));
            return Json(filters);
        }

        private Dictionary<String, String> FieldDict() => new Dictionary<String, String>
        {
            {"Наименование", "name"},
            {"ИНН", "INN"},
            {"КПП", "KPP"},
            {"ОГРН", "OGRN"},
            {"ФИО руководителя", "boss" },
            {"ИННФЛ руководителя", "INNFL" },
            {"Должность руководителя", "boss_position" },
            {"Номер телефона", "phone_number" },
            {"Электронная почта", "mail" },
            {"Адрес", "address" },
            {"Ссылка на сайт", "site_url" },
            {"Статус", "status" },
            {"Дата регистрации", "date" },
            {"Реестр МСП", "MSP" },
            {"Выручка, тыс. руб", "proceeds" },
            {"Баланс, тыс. руб", "balance" },
            {"Арбитраж (ответчик), тыс. руб", "arbitrage" },
            {"Чистая прибыль/ убыток, тыс. руб", "net_proceeds" },
            {"Основной вид деятельности", "works_type" },
            {"Другие виды деятельности", "another_work_type" },
            {"Предметы закупок (ОКПД2)", "procurement_subject" },
            {"Регион регистрации", "registration_region" },
            {"Полученные лицензии", "got_licenses" },
            {"Лизингополучатель", "leasing" },
            {"Количество сотрудников", "quantity" }
        };

        private String GetNewFieldNameByOldName(String oldName) => FieldDict()[oldName];
    }
}
