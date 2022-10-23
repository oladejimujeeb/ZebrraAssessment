using Microsoft.EntityFrameworkCore;
using ProductWebAPI.AppContext;
using ProductWebAPI.Interface;
using ProductWebAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductWebAPI.Implmentation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product= await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> UpdateProduct(Product product, int id)
        {
            var comp = await _context.Products.FindAsync(id);
            if (comp != null)
            {
                var update = _context.Products.Update(comp);
                await _context.SaveChangesAsync();
                return update.Entity;
            }
            return null;
        }

      
    }
}
