using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace TsentrstroyAPI
{
    public static class WebFiles
    {
        private static IWebHostEnvironment _webHostEnvironment;
        
        private static string _rootPath;
        private static string _productsImagesPath = "ProductsImages";
        
        public static void Initialize(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;

            _rootPath = _webHostEnvironment.WebRootPath;
            _productsImagesPath = Path.Combine(_rootPath, _productsImagesPath);
        }

        public static string GetRootDirectory() => _rootPath;
        public static string GetProductsRootDirectory() => _productsImagesPath;
    }

    public class Image
    {
        public IFormFile Data { get; set; }
    }
}