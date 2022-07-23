using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TsentrstroyAPI.Data.Requests;
using TsentrstroyAPI.Data.Responses;
using TsentrstroyAPI.Model;

namespace TsentrstroyAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Settings _settings;
        private readonly DatabaseContext _databaseContext;

        public AuthenticationService(Settings settings, DatabaseContext databaseContext)
        {
            _settings = settings;
            _databaseContext = databaseContext;
        }

        public (bool success, string content) Register(RegisterRequest registerRequest)
        {
            string email = registerRequest.Email;
            string username = registerRequest.Username;
            string password = registerRequest.Password;
            
            if (_databaseContext.Users.Any(u => u.Email == email))
                return (false, "Email not available");

            User user = new User
            {
                Email = email,
                Username = username,
                PasswordHash = password,
            };

            user.ProvideSaltAndHash();

            _databaseContext.Add(user);
            _databaseContext.SaveChanges();

            return (true, "Successful");
        }

        public (bool success, AuthenticationResponse response) Login(LoginRequest loginRequest)
        {
            string email = loginRequest.Email;
            string password = loginRequest.Password;
            
            User user = _databaseContext.Users.SingleOrDefault(u => u.Email == email);

            if (user == null)
                return (false, new AuthenticationResponse {Error = "Invalid Email"});

            if (user.PasswordHash != AuthenticationHelpers.ComputeHash(password, user.Salt))
                return (false,  new AuthenticationResponse {Error = "Invalid Password"});

            AuthenticationResponse authenticationResponse = new AuthenticationResponse()
            {
                Token = GenerateJwtToken(AssembleClaimsIdentity(user)),
                Role = user.RoleName
            };

            return (true, authenticationResponse);
        }

        private string GenerateJwtToken(ClaimsIdentity subject)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII.GetBytes(_settings.BearerKey);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity AssembleClaimsIdentity(User user)
        {
            ClaimsIdentity subject = new ClaimsIdentity(new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            });

            return subject;
        }
    }

    public static class AuthenticationHelpers
    {
        public static void ProvideSaltAndHash(this User user)
        {
            byte[] salt = GenerateSalt();
            user.Salt = Convert.ToBase64String(salt);
            user.PasswordHash = ComputeHash(user.PasswordHash, user.Salt);
        }

        private static byte[] GenerateSalt()
        {
            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            byte[] salt = new byte[24];
            randomNumberGenerator.GetBytes(salt);

            return salt;
        }

        public static string ComputeHash(string password, string userSalt)
        {
            byte[] salt = Convert.FromBase64String(userSalt);

            using var hashGenerator = new Rfc2898DeriveBytes(password, salt);
            hashGenerator.IterationCount = 10101;

            byte[] bytes = hashGenerator.GetBytes(24);

            return Convert.ToBase64String(bytes);
        }
    }
}