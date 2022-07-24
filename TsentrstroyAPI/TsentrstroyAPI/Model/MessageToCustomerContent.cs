using System.Text;
using TsentrstroyAPI.Model.Order;

namespace TsentrstroyAPI.Model
{
    public class MessageToCustomerContent
    {
        public string GetContent(OrderMessageContent orderMessageContent)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string html = System.IO.File.ReadAllText(@"./wwwroot/MessageToCustomerContentTemplates/Message.html");
            
            for (int i = 0; i < orderMessageContent.OrderPositions.Count; i++)
            {
                string orderPositionTemplate = System.IO.File.ReadAllText(@"./wwwroot//MessageToCustomerContentTemplates/OrderPositionTemplate.html");
                
                orderPositionTemplate = orderPositionTemplate.Replace("#NAME#", $"{orderMessageContent.OrderPositions[i].Title}");
                orderPositionTemplate = orderPositionTemplate.Replace("#ID#", (i + 1).ToString());
                
                stringBuilder.Append(orderPositionTemplate);
            }

            html = html.Replace("#ORDERPOSITIONS#", stringBuilder.ToString());
            html = html.Replace("#DELIVERYADDRESS#", GetDeliveryAddress(stringBuilder, orderMessageContent.DetailsOfPayment));
            
            return html;
        }

        private string GetDeliveryAddress(StringBuilder stringBuilder, DetailsOfPayment detailsOfPayment)
        {
            stringBuilder.Clear();

            stringBuilder.Append($"<p><i>Город</i>: {detailsOfPayment.Region}");
            stringBuilder.Append($"<br><i>Адрес</i>: {detailsOfPayment.Address}</p>");
            stringBuilder.Append($"<p><i>Индекс</i>: {detailsOfPayment.Postcode}</p>");
            
            return stringBuilder.ToString();
        }
    }
}