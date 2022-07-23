using System.Text.Json.Serialization;

namespace TsentrstroyAPI.Data
{
    public class Product
    {
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("shortDescription")]
        public string ShortDescription { get; set; }
        
        [JsonPropertyName("fullDescription")]
        public string FullDescription { get; set; }
        
        [JsonPropertyName("manufacturerCompany")]
        public string ManufacturerCompany { get; set; }
        
        [JsonPropertyName("packaging")]
        public string Packaging { get; set; }
        
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; } = true;

        public SubCategory SubCategory { get; set; }
    }
}