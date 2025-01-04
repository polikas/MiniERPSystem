using Microsoft.EntityFrameworkCore;
using MiniERPSystem.Models;

namespace MiniERPSystem.Data
{
    public class ERPDbContext : DbContext
    {
        public ERPDbContext(DbContextOptions<ERPDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
