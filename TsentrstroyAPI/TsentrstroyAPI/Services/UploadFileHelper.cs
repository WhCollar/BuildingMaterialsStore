using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TsentrstroyAPI.Services
{
    public class UploadFileHelper
    {
        public async Task<string> UploadFile(IFormFile image, string saveFolder)
        {
            if (image is null == true)
                return null;
            
            string extension = Path.GetExtension(image.FileName);
            string randomName = Path.GetRandomFileName();

            string fileName = Path.ChangeExtension(randomName, extension);
            
            string saveDirectory = Path.Combine(saveFolder, fileName);
            string savePath = Path.Combine(WebFiles.GetRootDirectory(), saveDirectory);
            
            using (FileStream fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            
            return $"{saveFolder}/{fileName}";
        }

        public void RemoveFile(string path)
        {
            string sourcePath = Path.Combine(WebFiles.GetRootDirectory(), path);

            if (File.Exists(sourcePath) == true)
                File.Delete(sourcePath);
        }
    }
}