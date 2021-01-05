using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomersFinder.Models;
using CustomersFinder.Models.Domain;
using CustomrsFinder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CustomrsFinder.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainTableController : Controller
    {
        private IocContainer dependency = null;
        public MainTableController(CustomerFinderDB searchDB)
        {
            dependency = new IocContainer(searchDB);
        }

        [Route("SetPhoneStatus")]
        [HttpPost]
        public async Task<IActionResult> SetPhoneStatus([FromBody] JToken obj)
        {
            var status = obj["status"].ToString();
            var phoneId = (Int32)obj["phoneId"];
            await dependency.GetCustomerPhoneNumbersService().UpdateStatusById(phoneId, status);
            return Ok("");
        }

        [Route("SetComments")]
        [HttpPost]
        public async Task<IActionResult> SetComments([FromBody] JToken obj)
        {
            var comments = obj["comments"].ToString();
            var posId = (Int32)obj["posId"];
            await dependency.GetCustomerInfoService().UpdateCommentsById(comments, posId);

            var userId = Convert.ToInt32(HttpContext.Request.Cookies["userId"]);
            await dependency.GetPositionEventsService().Insert(new PositionEvents
            {
                name = $"Добавлен новый комментарий: {comments}",
                customer_info_id = posId,
                user_id = userId
            });

            return Ok("");
        }

        [Route("GetStatuses")]
        [HttpGet]
        public async Task<IActionResult> GetStatuses()
        {
            var result = await dependency.GetPositionStatusService().GetAll();
            return Ok(result);
        }

        [Route("SendMail")]
        [HttpGet]
        public async Task<IActionResult> SendMail(Int32 managetId, int posId)
        {
            var manager = await dependency.GetManagerService().GetById(managetId);
            var message = await dependency.GetCustomerInfoService().GetMailBodyByPosId(posId);
            await dependency.GetMailService().Send(manager.mail, message);
            var userId = Convert.ToInt32(HttpContext.Request.Cookies["userId"]);
            await dependency.GetPositionEventsService().Insert(new PositionEvents
            {
                name = $"Контакт отправлен на почту менеджеру: {manager.short_name}",
                customer_info_id = posId,
                user_id = userId
            });

            return Json($"Сообщение сформировано и отправленно сотруднику \"{manager.short_name}\"");
        }

        [HttpGet]
        [Route("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(int posId, int statusId)
        {
            var userId = Convert.ToInt32(HttpContext.Request.Cookies["userId"]);
            await dependency.GetCustomerInfoService().ChangeStatusById(posId, statusId);
            var status = await dependency.GetPositionStatusService().GetById(statusId);
            await dependency.GetPositionEventsService().Insert(new PositionEvents
            {
                name = $"Статус изменён на: {status.Name}",
                customer_info_id = posId,
                user_id = userId
            });
            return Ok();
        }
    }
}
