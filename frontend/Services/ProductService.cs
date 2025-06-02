using frontend.Interfaces;
using frontend.Models;
using frontend.Pages;
using System.Net.Http.Json;
using System.Text.Json;

namespace frontend.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ProductService(HttpClient httpClient) //ctor
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<(Product? newProduct, string? errorMessage, bool success)> CreateNewProductAsync(ProductToCreate productToCreate)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/products", productToCreate);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (null, errorMessage ?? response.ReasonPhrase ?? "Could not add product", false);
                }

                var newProduct = await response.Content.ReadFromJsonAsync<Product>(_jsonOptions);
                return (newProduct, null, true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(bool success, string message)> DeleteProductAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/products/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, errorMessage ?? response.ReasonPhrase ?? $"Could not delete product with id {id}");
                }

                var message = await response.Content.ReadAsStringAsync();
                return (true, message);
            }
            catch (Exception)
            {

                return (false, "An error occurred.");
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/products");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<Product>>(_jsonOptions)
                    ?? Enumerable.Empty<Product>().ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<(Product? product, string errorMessage)> GetProductByIdAsync(int productId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/products/{productId}");
                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    return (null, errorMessage ?? response.ReasonPhrase ?? "Error retrieving product by id");
                }

                var product = await response.Content.ReadFromJsonAsync<Product>(_jsonOptions);
                return (product, string.Empty);
            }
            catch (Exception ex)
            {

                return (null, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> UpdateProductAsync(int id, ProductToUpdate productToUpdate)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/products/{id}", productToUpdate);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (false, errorMessage ?? response.ReasonPhrase ?? "Could not update product.");
                }

                var message = await response.Content.ReadAsStringAsync();
                return (true, message);
            }
            catch (Exception)
            {

                return (false, "An error occurred.");
            }
        }
    }
}
