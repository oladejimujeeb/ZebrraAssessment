using ProductWebAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductWebAPI.Interface
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int id);
        Task<bool> AddProduct(Product product);
        Task<bool> DeleteProduct(int id);
        Task<Product> UpdateProduct(Product product, int id);
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
