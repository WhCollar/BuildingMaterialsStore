using Microsoft.EntityFrameworkCore;
using TsentrstroyAPI.Data;
using TsentrstroyAPI.Model;

namespace TsentrstroyAPI
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        
        public virtual DbSet<Product> Products { get; set; }
        
        public virtual DbSet<AreasOfUseTab> AreasOfUseTabs { get; set; }
        public virtual DbSet<FeatureListTab> FeatureListTabs { get; set; } 
        
        public virtual DbSet<MoreAtProduct> MoreAtProducts { get; set;}

        public virtual DbSet<OverviewProduct> OverviewProducts { get; set; }
    }
}