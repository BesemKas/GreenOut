using GreenOut.Data;
using GreenOut.Interfaces;
using GreenOut.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace GreenOut.Repository
{
    public class AccountRepository : IAccountRepository
    {

        private readonly GreenOutDbContext _context;

        public AccountRepository(GreenOutDbContext context)
        {
            _context = context;
        }


        public bool CreateCart(ShoppingCart cart)
        {
            _context.Add(cart);
            return Save();
        }



        public  IEnumerable<CartItem> GetAllCartItems(int id)
        {
            return _context.CartItems.Include(p=>p.Product).Where(i => i.CartID.Equals(id));
        }



        public ShoppingCart GetCartByAccountIdNoTracking(string id)
        {
            return _context.ShoppingCarts.AsNoTracking().FirstOrDefault(s => s.AccountID == id);
        }
        

        public Task<Order> GetOrderByIdAsync(int orderID)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOrdersByAccountId(int id)
        {
            throw new NotImplementedException();
        }



        public Task<Payment> GetPaymentsByAccountId(int accountID)
        {
            throw new NotImplementedException();
        }

    
        public bool AddtoCart(CartItem item)
        {
            _context.Add(item);
            return Save();
        }
        public async Task<bool> AddtoOrder(OrderItem item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool Delete(CartItem item)
        {
            _context.Remove(item);
            return Save();
        }

 
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(CartItem item)
        {
            if (item == null || item.ProductID == 0)
            {
                throw new ArgumentException("Product cannot be null or have an invalid ProductID");
            }

            _context.Update(item);
            return Save();
        }

        public async Task<IEnumerable<CartItem>> Checkout(int id)
        {
            return _context.CartItems.Include(p => p.Product).Where(i => i.CartID.Equals(id));
        }

        public bool CreateOrder(Order order)
        {
            _context.Add(order);
            return Save();
        }

        public async Task<Product> GetProductByIDAsyncNoTracking(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(i => i.ProductID == id);
        }
    }
}
