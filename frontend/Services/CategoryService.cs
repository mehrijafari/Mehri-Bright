using frontend.Interfaces;
using frontend.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace frontend.Services;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }


    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/categories");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Category>>(_jsonOptions)
                ?? Enumerable.Empty<Category>().ToList();
        }
        catch (Exception)
        {

            throw;
        }
    }
}
