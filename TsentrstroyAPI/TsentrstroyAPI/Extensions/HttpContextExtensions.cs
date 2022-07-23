using Microsoft.AspNetCore.Http;

namespace TsentrstroyAPI.Extensions
{
    public static class HttpContextExtensions
    {
        public static bool QueryContainsKey(this HttpContext httpContext, string key)
        {
            return httpContext.Request.Query.ContainsKey(key);
        }

        public static IFormFileCollection FormFiles(this HttpContext httpContext)
        {
            return httpContext.Request.Form.Files;
        }

        public static IFormFile GetFile(this HttpContext httpContext, string name)
        {
            return httpContext.FormFiles()[name];
        }

        public static T GetForm<T>(this HttpContext httpContext, string name) where T : class
        {
            return httpContext.Request.Form[name] as T;
        }
    }
}