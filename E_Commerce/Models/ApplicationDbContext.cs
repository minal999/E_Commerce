using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace E_Commerce.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Cart> cart { get; set; }
    }
}
