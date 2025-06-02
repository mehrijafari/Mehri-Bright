using Mehri_Bright.DbContexts;
using Mehri_Bright.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mehri_Bright.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebLabb2Context _context;

        public ProductRepository(WebLabb2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
        }


        public async Task DeleteProduct(Product product)
        {
           _context.Products.Remove(product);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();

        }

        public async Task<Product?> GetProductAsync(int id)
        {
            return await _context.Products.Include(p => p.Category).Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) >= 0;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
