using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TsentrstroyAPI.Extensions
{
    public static class MiddlewareExtensions
    {
        public static async Task<string> ReadBody(this HttpRequest httpRequest)
        {
            httpRequest.EnableBuffering();

            byte[] buffer = new byte[Convert.ToInt32(httpRequest.ContentLength)];
            await httpRequest.Body.ReadAsync(buffer, 0, buffer.Length);
            httpRequest.Body.Position = 0;

            string result = Encoding.UTF8.GetString(buffer);

            Console.WriteLine("\n\n\n");
            Console.WriteLine(result);
            Console.WriteLine("\n\n\n");
            
            return result;
        }
    }
}