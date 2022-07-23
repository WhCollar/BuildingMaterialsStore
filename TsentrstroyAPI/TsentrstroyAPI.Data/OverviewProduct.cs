using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TsentrstroyAPI.Data
{
    public class OverviewProduct
    {
        public int Id { get; set; }

        [NotNull, JsonPropertyName("title")]
        public string Title { get; set; }
        
        [NotNull, JsonPropertyName("subtitle")]
        public string Subtitle { get; set; }
        
        [NotNull, JsonPropertyName("description")]
        public string Description { get; set; }
        
        [NotNull, JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }
    }
}