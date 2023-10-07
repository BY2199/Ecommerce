using Microsoft.EntityFrameworkCore;
using EcommerceWeb_Razor.Models;

namespace EcommerceWeb_Razor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Palantir", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Sofi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Roku", DisplayOrder = 3 }
                );
        }
    }
}
