using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TsentrstroyAPI.Data
{
    public class InformationTab
    {
        public int Id { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("items")]
        public List<string> Items { get; set; }
    }
}