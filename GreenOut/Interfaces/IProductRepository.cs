using GreenOut.Models;

namespace GreenOut.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetByIDAsync(int id);

        Task<IEnumerable<Product>> GetByCategoryID(int categoryID);
    }
}
