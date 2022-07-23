using System.Text.Json.Serialization;

namespace TsentrstroyAPI.Data
{
    public class EntityFrameworkBase
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}