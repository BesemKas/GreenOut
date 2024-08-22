using GreenOut.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenOut.Data
{
    public class GreenOutDbContext : DbContext
    {
        public GreenOutDbContext(DbContextOptions<GreenOutDbContext> options) :base(options)
        {
            
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product>  Product { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Order> Order { get; set; }

        public DbSet<Payment> Payment { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<GreenOut.Models.ProductEditViewModel> ProductEditViewModel { get; set; } = default!;





    }
}
