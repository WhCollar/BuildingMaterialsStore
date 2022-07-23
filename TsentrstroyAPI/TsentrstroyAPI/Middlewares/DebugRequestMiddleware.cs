using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TsentrstroyAPI.Extensions;

namespace TsentrstroyAPI.Middlewares
{
    public class DebugRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public DebugRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Request.ReadBody();
            
            await _next.Invoke(context);
        }
    }
}