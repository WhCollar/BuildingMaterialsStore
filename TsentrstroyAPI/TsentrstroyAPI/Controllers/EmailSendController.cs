using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TsentrstroyAPI.Services;

namespace TsentrstroyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailSendController : ControllerBase
    {
        private string _domain = "sandbox08b2d18621e74d638ddf4910bf440198.mailgun.org";
        private string _password = "37c60729ddea99bf2fe857c103b798cb-18e06deb-e60b483a";
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            EmailNotificationService notificationService = new EmailNotificationService("sq1qwe203@mail.ru", "Заказ принят в обработку", "wewe");
            await notificationService.Send();
            return Ok();
        }
    }
}