using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TsentrstroyAPI.Data;

namespace TsentrstroyAPI.DatabaseActions
{
    public static class SubCategoryDatabaseActions
    {
        public static async Task Delete(SubCategory subCategory, DatabaseContext databaseContext)
        {
            List<Product> products = await databaseContext.Products.Where(p => p.SubCategory == subCategory).ToListAsync();
            await ProductDatabaseActions.Delete(products, databaseContext);

            databaseContext.SubCategories.Remove(subCategory);
            await databaseContext.SaveChangesAsync();
        }

        public static async Task Delete(Category category, DatabaseContext databaseContext)
        {
            List<SubCategory> subCategories = await databaseContext.SubCategories.Where(s => s.Category == category).ToListAsync();

            foreach (var subCategory in subCategories)
                await Delete(subCategory, databaseContext);
        }
    }
}