using GreenOut.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GreenOut.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<CartItem>> GetAllCartItems(int id);
        Task<ShoppingCart> GetCartByAccountAsyncIdNoTracking(string id);
        Task<IEnumerable<CartItem>> Checkout(int id);
        Product GetProductByID(int id);

        bool CreateCart(ShoppingCart cart);
        bool CreateOrder(Order order);



        bool AddtoCart(CartItem item);
        bool AddtoOrder(OrderItem item);

        bool Update(CartItem item);
        bool Delete(CartItem item);
        bool Save();



      
        Task<IEnumerable<Order>> GetOrdersByAccountId(int id);
        Task<Order> GetOrderByIdAsync(int orderID);

    

        Task<Payment> GetPaymentsByAccountId(int accountID);
    }
}
