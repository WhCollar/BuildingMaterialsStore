using TsentrstroyAPI.Data;

namespace TsentrstroyAPI.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        
        public string Username { get; set; }
        
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        
        public UserRole Role { get; set; }

        public string RoleName => Role.ToString();
    }
}