using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TsentrstroyAPI.Middlewares
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string logResult = $"{context.Request.Path} | {context.Request.Method} | {DateTime.Now}";

            Console.WriteLine(logResult);

            await _next.Invoke(context);
        }
    }
}