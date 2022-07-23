using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TsentrstroyAPI.Data.Requests
{
    public class RegisterRequest
    {
        [Required, JsonPropertyName("email")]
        [EmailAddress] public string Email { get; set; }
        
        [Required, JsonPropertyName("username")]
        public string Username { get; set; }

        [Required, JsonPropertyName("password")]
        public string Password { get; set; }
        
        [Compare("Password"), JsonPropertyName("confirmPassword")]
        public string ConfirmPassword { get; set; }
        
        public UserRole Role { get; set; }
    }
}