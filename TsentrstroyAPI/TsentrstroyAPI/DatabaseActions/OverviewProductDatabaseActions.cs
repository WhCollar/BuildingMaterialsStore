using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TsentrstroyAPI.Data;

namespace TsentrstroyAPI.DatabaseActions
{
    public static class OverviewProductDatabaseActions
    {
        public static async Task Delete(Category category, DatabaseContext databaseContext)
        {
            OverviewProduct overviewProduct = await databaseContext.OverviewProducts.FirstOrDefaultAsync(o => o.Category == category);
            
            if(overviewProduct is null == true)
                return;
            
            RemoveImage(overviewProduct.ImageUrl);
            
            databaseContext.OverviewProducts.Remove(overviewProduct);
            await databaseContext.SaveChangesAsync();
        }

        private static void RemoveImage(string path)
        {
            string sourcePath = Path.Combine(WebFiles.GetRootDirectory(), path);

            if (File.Exists(sourcePath) == true)
                File.Delete(sourcePath);
        }
    }
}