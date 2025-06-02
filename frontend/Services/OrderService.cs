using frontend.Interfaces;
using frontend.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace frontend.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public OrderService(HttpClient httpClient) //ctor
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<(Order? order, string? message, bool success)> CreateOrder(CreateOrder createOrder)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/orders", createOrder);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return (null, errorMessage ?? response.ReasonPhrase ?? "Could not place order", false);
            }

            var createdOrder = await response.Content.ReadFromJsonAsync<Order>(_jsonOptions);
            return (createdOrder, null, true);
        }
        catch (Exception ex)
        {

            return (null, $"An error occured: {ex.Message}", false);
        }
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/orders");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Order>>(_jsonOptions)
                ?? Enumerable.Empty<Order>().ToList();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<(Order? order, string? message)> SearchOrderByIdAsync(int orderId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/orders/by-id/{orderId}");
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return (null, errorMessage ?? response.ReasonPhrase ?? $"Could not retrieve order {orderId}");
            }

            var order = await response.Content.ReadFromJsonAsync<Order>(_jsonOptions);
            return (order, null);
        }
        catch (Exception ex)
        {

            return (null, $"An error occurred: {ex.Message}");
        }
    }
}
