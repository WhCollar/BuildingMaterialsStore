using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using TsentrstroyAPI.Model;
using TsentrstroyAPI.Model.Order;
using TsentrstroyAPI.Services;

namespace TsentrstroyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AcceptOrderController : ControllerBase
    {
        private IConfiguration _configuration;
        
        public AcceptOrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderMessageContent orderMessageContent)
        {
            if (orderMessageContent is null == true)
                return BadRequest("Ошибка формирования заказа: позиции не выбраны");

            RestClient restClient = new RestClient(_configuration.GetValue<string>("TelegramBotServerUrl")); ;
            RestRequest restRequest = new RestRequest("order/accepting", Method.Post) {RequestFormat = DataFormat.Json};

            restRequest.AddBody(orderMessageContent);
            await restClient.ExecuteAsync(restRequest, Method.Post);
            
            SendEmailNotification(orderMessageContent.DetailsOfPayment.Email, "📝 Информация о заказе", new MessageToCustomerContent().GetContent(orderMessageContent));
            SendEmailNotification(_configuration.GetValue<string>("OrganizationMail"), "📦 Новый заказ", OrganizationMailContent(orderMessageContent));
            
            return Ok("Заказ успешно сформирован");
        }

        private async void SendEmailNotification(string recipientAddress, string subject, params string[] content)
        {
            EmailNotificationService notificationService = new EmailNotificationService(recipientAddress, subject, content);
            await notificationService.Send();
        }

        private string OrganizationMailContent(OrderMessageContent orderMessageContent)
        {
            StringBuilder stringBuilder = new StringBuilder();
            DetailsOfPayment detailsOfPayment = orderMessageContent.DetailsOfPayment;

            stringBuilder.Append("<h1>Информация о заказе\n\n</h1>");

            stringBuilder.Append($"<p><i>Заказчик</i>: {detailsOfPayment.FirstName} {detailsOfPayment.SecondName}</p>");
            stringBuilder.Append($"<p><i>Номер телефона</i>: +7{detailsOfPayment.Telephone}</p>");
            stringBuilder.Append($"<p><i>Почта</i>: {detailsOfPayment.Email}</p>");
            
            stringBuilder.Append("<h1 style=\"margin-top:3%;\">Доставка</h1>");
            stringBuilder.Append($"<p><i>Город<i>: {detailsOfPayment.Region}<p> <p><i>Адрес</i>: {detailsOfPayment.Address}<p> <p><i>Индекс</i>: {detailsOfPayment.Postcode}</p>\n");

            stringBuilder.Append("<p><h1 style=\"margin-top:3%;\">Содержание заказа</h1></p>");

            stringBuilder.Append("<table border=\"1\" cellpadding= \"5\" width=\"512\" style=\"border-collapse: collapse;\">");
            stringBuilder.Append("<caption = \"Содержание заказа\">");
            
            stringBuilder.Append("<tr>");
            stringBuilder.Append("<th>№</th>");
            stringBuilder.Append("<th>Название</th>");
            stringBuilder.Append("</tr>");
            
            
            //stringBuilder.Append("<ul>");
            foreach (OrderPosition orderPosition in orderMessageContent.OrderPositions)
                stringBuilder.Append($"<tr><td>{orderPosition.Id}</td> <td><a href={_configuration["Site"]}category/product-ditails/{orderPosition.Id}>{orderPosition.Title}</a></td></tr>");
            //stringBuilder.Append("</ul>");
            
            return stringBuilder.ToString();
        }
        
    }
}