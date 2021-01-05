using CustomersFinder.Models.Domain;
using CustomersFinder.Models.ViewModels.ExtendedPosition;
using CustomrsFinder.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersFinder.Controllers
{
    public class ExtendedPositionController : Controller
    {
        private IocContainer dependency = null;
        public ExtendedPositionController(CustomerFinderDB db)
        {
            dependency = new IocContainer(db);
        }

        public async Task<IActionResult> Index(int posId)
        {
            var model = await dependency.GetExtendedPositionModelService().GetByPosId(posId);
            ViewBag.Name = model.CustomerInfo.name;
            return View(model);
        }

        public async Task<IActionResult> AddComments(int posId, string comments)
        {
            await dependency.GetCustomerInfoService().UpdateCommentsById(comments, posId);
            var userId = Convert.ToInt32(HttpContext.Request.Cookies["userId"]);
            var token = HttpContext.Request.Cookies["JWToken"];
            var user = await dependency.GetUserService().GetUserByIdAsync(userId, token);

            var posEvent = new PositionEvents
            {
                name = $"Добавлен новый комментарий: {comments}",
                customer_info_id = posId,
                user_id = userId
            };

            await dependency.GetPositionEventsService().Insert(posEvent);

            return PartialView("RowPartial", new ExtendedPositionRowModel { EventName = posEvent.name, UserName =  user.ShortName, Date = posEvent.date });
        }
    }
}
