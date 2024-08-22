//ERROR HANDLING to be implemented at later stage when this actually works
//ERROR HANDLING DONE FOR UPDATE


using GreenOut.Data;  // Add documentation for the GreenOut.Data namespace
using GreenOut.Interfaces;  // Add documentation for the GreenOut.Interfaces namespace
using GreenOut.Models;  // Add documentation for the GreenOut.Models namespace
using Microsoft.EntityFrameworkCore;  // No documentation typically needed for standard libraries

namespace GreenOut.Repository
{
    /// <summary>
    /// This class implements the IProductRepository interface and provides methods
    /// for interacting with Product data in the database.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly GreenOutDbContext _context;

        /// <summary>
        /// Initializes a new instance of the ProductRepository class.
        /// </summary>
        /// <param name="context">The GreenOutDbContext instance used for database access.</param>
        public ProductRepository(GreenOutDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>An asynchronous task that returns a list of all products.</returns>
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.Include(a => a.Category).ToListAsync();
        }

        /// <summary>
        /// Retrieves all products belonging to a specific category.
        /// </summary>
        /// <param name="categoryID">The ID of the category to filter by.</param>
        /// <returns>An asynchronous task that returns a list of products in the specified category.</returns>
        public async Task<IEnumerable<Product>> GetByCategoryID(int categoryID)
        {
            return await _context.Products.Where(c => c.Category.CategoryID.Equals(categoryID)).ToListAsync();
        }

        /// <summary>
        /// Retrieves a product from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>An asynchronous task that returns the product with the specified ID, or null if not found.</returns>
        public async Task<Product> GetByIDAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(i => i.ProductID == id);
        }



    }
}
