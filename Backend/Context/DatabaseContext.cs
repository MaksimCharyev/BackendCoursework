using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace Backend.Context
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Entities.User> Users { get; set; } = null!;
        public DbSet<Entities.Category> Categories { get; set; } = null!;
        public DbSet<Entities.Shop> Shops { get; set; } = null!;
        public DbSet<Entities.Product> Products { get; set; } = null!;
        public DbSet<Entities.UserMarkedProduct> UserMarkedProducts { get; set; } = null!;
        public DbSet<Entities.ProductHasPrice> ProductHasPrice { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       
        }
        

    }
}
