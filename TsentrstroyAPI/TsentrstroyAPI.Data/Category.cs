using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TsentrstroyAPI.Data
{
    public class Category
    {
        public int Id { get; set; }

        [NotNull, JsonPropertyName("name")]
        public string Name { get; set; }
        
        [NotNull, JsonPropertyName("slug")]
        public string Slug { get; set; }
    }
}