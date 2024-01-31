using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Product.Infrastructure.Mappings;
using Product.Model.Models;

namespace Product.Infrastructure.Context
{
    public class ProductContext : IdentityDbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        public DbSet<Products> products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductsTypeEntityTypeConfiguration());
                        base.OnModelCreating(modelBuilder);
        }
    }
}
