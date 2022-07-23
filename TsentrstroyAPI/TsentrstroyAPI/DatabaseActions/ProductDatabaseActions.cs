using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TsentrstroyAPI.Data;

namespace TsentrstroyAPI.DatabaseActions
{
    public static class ProductDatabaseActions
    {
        public static async Task Delete(Product product, DatabaseContext databaseContext)
        {
            DeleteImage(product);
            await MoreAtProductDatabaseActions.Delete(product, databaseContext);

            databaseContext.Products.Remove(product);
            await databaseContext.SaveChangesAsync();
        }
        
        public static async Task Delete(List<Product> products, DatabaseContext databaseContext)
        {
            DeleteImage(products);
            await MoreAtProductDatabaseActions.Delete(products, databaseContext);

            databaseContext.Products.RemoveRange(products);
            await databaseContext.SaveChangesAsync();
        }

        private static void DeleteImage(List<Product> products)
        {
            foreach (var product in products)
                DeleteImage(product);
        }
        
        private static void DeleteImage(Product product)
        {
            string fullFilePath = Path.Combine(WebFiles.GetRootDirectory(), product.ImageUrl);
            if (File.Exists(fullFilePath) == true)
                File.Delete(fullFilePath);
        }
    }
}