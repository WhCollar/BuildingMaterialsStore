using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TsentrstroyAPI.Data.AdminPanelJsons.Product
{
    public class CategoryJsonStruct
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
            
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("slug")]
        public string Slug { get; set; }
            
        public List<SubCategoryJsonStruct> SubCategories { get; set; }
    }
}