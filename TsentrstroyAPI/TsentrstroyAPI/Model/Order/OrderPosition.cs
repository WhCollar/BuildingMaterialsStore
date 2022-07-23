using System.Text.Json.Serialization;

namespace TsentrstroyAPI.Model.Order
{
    public struct OrderPosition
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}