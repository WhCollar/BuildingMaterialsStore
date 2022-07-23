using System.Text.Json.Serialization;

namespace TsentrstroyAPI.Data.Responses
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        
        public string Role { get; set; }
        
        [JsonIgnore]
        public string Error { get; set; }
    }
}