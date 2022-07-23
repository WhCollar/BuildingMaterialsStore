using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace TsentrstroyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Testt : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            string directory = Directory.GetCurrentDirectory();
            string[] fullPath = {directory, "Media", "Images", "ProductsImages", "Poli", "1.png"};
            
            string combinePath = Path.Combine(fullPath);
            
            Byte[] bytes = System.IO.File.ReadAllBytes(combinePath);
            
            Console.WriteLine("Testt Get");
            
            return Ok("https://26.112.32.64:8000/ProductsImages/Poli/1.png");
        }
    }
}