using GreenOut.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GreenOut.Interfaces
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductByIDAsync(int id);
        Task<Product> GetProductByIDAsyncNoTracking(int id);

        Task<IEnumerable<Product>> GetProductsByCategoryID(int categoryID);

        public IEnumerable<SelectListItem> GetAllCategories();

        public Task<bool> CategoryExists(int categoryId);

        bool Add(Product product);
        bool Update(Product product);
        bool Delete(Product product);
        bool Save();

        Task<IEnumerable<Account>> GetAllAccounts();
        Task<Account> GetAccountByIdAsync(int accountID);
        Task<Account> GetAccountByEmailAsync(string email);
        Task<Account> GetAccountByPhoneAsync(string phone);

        Task<Order> GetAllOrders();
        Task<Order> GetOrderByIdAsync(int orderID);
        Task<Order> GetOrderByAccountId(int accountID);

        Task<Payment> GetAllPayments();
        Task<Payment> GetPaymentByOrderId(int orderID);

        Task<Payment> GetPaymentsByAccountId(int accountID);
    }
}
