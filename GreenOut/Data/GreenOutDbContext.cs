using GreenOut.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GreenOut.Data
{
    public class GreenOutDbContext : IdentityDbContext<Account>
    {
        public GreenOutDbContext(DbContextOptions<GreenOutDbContext> options) :base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product>  Products { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders{ get; set; }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<GreenOut.Models.ProductEditViewModel> ProductEditViewModel { get; set; } = default!;





    }
}
