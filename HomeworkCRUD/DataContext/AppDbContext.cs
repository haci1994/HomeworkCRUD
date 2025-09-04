using HomeworkCRUD.DataContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HomeworkCRUD.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<HeaderElement> HeaderElements { get; set; }
        public DbSet<Social> Socials { get; set; }
    }
}
