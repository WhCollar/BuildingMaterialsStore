using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TsentrstroyAPI.Model.Order
{
    public class OrderMessageContent 
    {
        [JsonPropertyName("detailsOfPayment")]
        public DetailsOfPayment DetailsOfPayment { get; set; } 
        
        [JsonPropertyName("orderPositions")]
        public List<OrderPosition> OrderPositions { get; set; }
    }
}