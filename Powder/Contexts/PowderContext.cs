using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Powder.Entities;

namespace Powder.Contexts
{
    public class PowderContext:IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDb; database=Powder; integrated security=false;");
            //optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDb; database=Powder; user id=s; password=1;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasMany(i => i.ProductCategories).WithOne(i => i.Product).HasForeignKey(i=> i.ProductId);
            modelBuilder.Entity<Category>().HasMany(i => i.ProductCategories).WithOne(i => i.Category).HasForeignKey(i=> i.CategoryId);
            modelBuilder.Entity<ProductCategory>().HasIndex(i=> new
            {
                i.ProductId,
                i.CategoryId,
            }).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

}
 