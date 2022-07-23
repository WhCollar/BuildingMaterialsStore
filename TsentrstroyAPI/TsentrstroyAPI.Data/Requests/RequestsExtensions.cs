namespace TsentrstroyAPI.Data.Requests
{
    public static class RequestsExtensions
    {
        public static LoginRequest ConvertToLogin(this RegisterRequest registerRequest)
        {
            LoginRequest loginRequest = new LoginRequest()
            {
                Email = registerRequest.Email,
                Password = registerRequest.Password
            };

            return loginRequest;
        }
    }
}