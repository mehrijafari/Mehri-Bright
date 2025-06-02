using frontend.Models;

namespace frontend.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<(Product? product, string errorMessage)> GetProductByIdAsync(int productId);
    Task<(Product? newProduct, string? errorMessage, bool success)> CreateNewProductAsync(ProductToCreate productToCreate);
    Task<(bool success, string message)> UpdateProductAsync(int id, ProductToUpdate productToUpdate);
    Task<(bool success, string message)> DeleteProductAsync(int id);
}
