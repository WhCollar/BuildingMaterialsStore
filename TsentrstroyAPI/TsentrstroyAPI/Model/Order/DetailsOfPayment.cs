using System.Text.Json.Serialization;

namespace TsentrstroyAPI.Model.Order
{
    public class DetailsOfPayment
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("secondName")]
        public string SecondName { get; set; }
        
        [JsonPropertyName("address")]
        public string Address { get; set; }
        
        [JsonPropertyName("region")]
        public string Region { get; set; }
        
        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }
        
        [JsonPropertyName("telephone")]
        public string Telephone { get; set; }
        
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}