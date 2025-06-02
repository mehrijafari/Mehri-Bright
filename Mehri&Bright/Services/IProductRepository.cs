using Mehri_Bright.Entities;

namespace Mehri_Bright.Services
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductAsync(int id);
        Task AddProduct(Product product);
        Task<bool> SaveChangesAsync();
        Task DeleteProduct(Product product);
        Task UpdateProductAsync(Product product);
        
    }
}
