using Microsoft.AspNetCore.Mvc;
using TsentrstroyAPI.Data.Requests;
using TsentrstroyAPI.Data.Responses;
using TsentrstroyAPI.Services;

namespace TsentrstroyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        public readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult Get() => Ok();
        
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            (bool success, string content) = _authenticationService.Register(registerRequest);

            if (success == false)
                return BadRequest(content);

            return Login(registerRequest.ConvertToLogin());
        }
        
        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            (bool success, AuthenticationResponse response) = _authenticationService.Login(loginRequest);

            if (success == false)
                return BadRequest(response.Error);

            return Ok(response);
        }
    }
}