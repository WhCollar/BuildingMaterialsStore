using System.Threading.Tasks;
using TsentrstroyAPI.Data;

namespace TsentrstroyAPI.DatabaseActions
{
    public static class CategoryDatabaseActions
    {
        public static async Task Delete(Category category, DatabaseContext databaseContext)
        {
            await OverviewProductDatabaseActions.Delete(category, databaseContext);
            await SubCategoryDatabaseActions.Delete(category, databaseContext);
            
            databaseContext.Categories.Remove(category);
            await databaseContext.SaveChangesAsync();
        }
    }
}