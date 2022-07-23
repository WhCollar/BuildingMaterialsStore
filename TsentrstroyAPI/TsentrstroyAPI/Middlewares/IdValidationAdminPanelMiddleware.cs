using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TsentrstroyAPI.Extensions;

namespace TsentrstroyAPI.Middlewares
{
    public class IdValidationAdminPanelMiddleware
    {
        private readonly RequestDelegate _next;

        public IdValidationAdminPanelMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "POST" || context.QueryContainsKey("id") == true)
            {
                await _next.Invoke(context);
                return;
            }

            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid data (id) passed by the request");
        }
    }
}