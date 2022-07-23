using TsentrstroyAPI.Data.Requests;
using TsentrstroyAPI.Data.Responses;

namespace TsentrstroyAPI.Services
{
    public interface IAuthenticationService
    {
        (bool success, string content) Register(RegisterRequest registerRequest);
        (bool success, AuthenticationResponse response) Login(LoginRequest loginRequest);
    }
}