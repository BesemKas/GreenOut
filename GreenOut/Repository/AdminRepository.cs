using GreenOut.Data;
using GreenOut.Interfaces;
using GreenOut.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GreenOut.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly GreenOutDbContext _context;

        public AdminRepository(GreenOutDbContext context)
        {
            _context = context;
        }


        //Account --start
        public Task<Account> GetAccountByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccountByIdAsync(int accountID)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccountByPhoneAsync(string phone)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> GetAllAccounts()
        {
            throw new NotImplementedException();
        }
        //account -- end


        //products -- start
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Product.Include(a => a.Category).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryID(int categoryID)
        {
            return await _context.Product.Where(c => c.Category.CategoryID.Equals(categoryID)).ToListAsync();
        }
        public async Task<bool> CategoryExists(int categoryId)
        {
            return await _context.Category.AnyAsync(c => c.CategoryID == categoryId);
        }

        public async Task<Product> GetProductByIDAsync(int id)
        {
            return await _context.Product.Include(c=>c.Category).FirstOrDefaultAsync(i => i.ProductID == id);
        }
        public async Task<Product> GetProductByIDAsyncNoTracking(int id)
        {
            return await _context.Product.Include(c => c.Category).AsNoTracking().FirstOrDefaultAsync(i => i.ProductID == id);
        }


        public IEnumerable<SelectListItem> GetAllCategories()
        {
            var CategorySelectList = _context.Category.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryID.ToString()
            });
            return CategorySelectList;

        }

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="product">The product to be added.</param>
        /// <returns>True if the product is added successfully, false otherwise.</returns>
        public bool Add(Product product)
        {
            _context.Add(product);
            return Save();
        }

        /// <summary>
        /// Deletes a product from the database.
        /// </summary>
        /// <param name="product">The product to be deleted.</param>
        /// <returns>True if the product is deleted successfully, false otherwise.</returns>
        public bool Delete(Product product)
        {
            _context.Remove(product);
            return Save();
        }

        /// <summary>
        /// Saves changes made to the database context.
        /// </summary>
        /// <returns>True if changes are saved successfully, false otherwise.</returns>
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="product">The product to be updated. The ProductID property must be populated for identification.</param>
        /// <returns>True if the product is updated successfully, false otherwise.</returns>
        public bool Update(Product product)
        {
            if (product == null || product.ProductID == 0)
            {
                throw new ArgumentException("Product cannot be null or have an invalid ProductID");
            }

            _context.Update(product);
            return Save();
        }
        //products --end



        //orders --start
        public Task<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }
        public Task<Order> GetOrderByAccountId(int accountID)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int orderID)
        {
            throw new NotImplementedException();
        }
        //orders -- end




        //payment --start

        public Task<Payment> GetAllPayments()
        {
            throw new NotImplementedException();
        }

        public Task<Payment> GetPaymentByOrderId(int orderID)
        {
            throw new NotImplementedException();
        }

        public Task<Payment> GetPaymentsByAccountId(int accountID)
        {
            throw new NotImplementedException();
        }

        //payments -- end


      

    
    }
}
