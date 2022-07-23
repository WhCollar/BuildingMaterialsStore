using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TsentrstroyAPI.Data;
using TsentrstroyAPI.Model;

namespace TsentrstroyAPI.DatabaseActions
{
    public static class MoreAtProductDatabaseActions
    {
        public static async Task Delete(Product product, DatabaseContext databaseContext)
        {
            await databaseContext.Products.AsNoTracking().LoadAsync();
            await databaseContext.MoreAtProducts.AsNoTracking().LoadAsync();
            MoreAtProduct moreAtProduct = await databaseContext.MoreAtProducts.OrderBy(m => m.Id).LastOrDefaultAsync(m => m.Product == product);
            
            if(moreAtProduct is null == true)
                return;

            await DeleteFeatureList(moreAtProduct, databaseContext);
            await DeleteAreasOfUse(moreAtProduct, databaseContext);
            
            databaseContext.MoreAtProducts.Remove(moreAtProduct);
            await databaseContext.SaveChangesAsync();
        } 
        
        public static async Task Delete(List<Product> products, DatabaseContext databaseContext)
        {
            foreach (var product in products)
            {
                MoreAtProduct moreAtProduct = await databaseContext.MoreAtProducts.OrderBy(m => m.Id).LastOrDefaultAsync(m => m.Product == product);

                if (moreAtProduct is null == false)
                    await Delete(product, databaseContext);
            }
        }

        public static async Task DeleteAreasOfUse(MoreAtProduct moreAtProduct, DatabaseContext databaseContext)
        {
            await databaseContext.AreasOfUseTabs.AsNoTracking().LoadAsync();

            IQueryable<AreasOfUseTab> areasOfUseTabs = databaseContext.AreasOfUseTabs.Where(a => a.MoreAtProduct == moreAtProduct);
            databaseContext.AreasOfUseTabs.RemoveRange(areasOfUseTabs);
            await databaseContext.SaveChangesAsync();
        }

        public static async Task DeleteFeatureList(MoreAtProduct moreAtProduct, DatabaseContext databaseContext)
        {
            await databaseContext.FeatureListTabs.AsNoTracking().LoadAsync();

            IQueryable<FeatureListTab> featureListTabs = databaseContext.FeatureListTabs.Where(a => a.MoreAtProduct == moreAtProduct);
            databaseContext.FeatureListTabs.RemoveRange(featureListTabs);
            await databaseContext.SaveChangesAsync();
        }
    }
}