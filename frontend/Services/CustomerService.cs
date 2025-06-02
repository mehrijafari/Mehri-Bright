

using frontend.Interfaces;
using frontend.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace frontend.Services;

public class CustomerService : ICustomerService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public CustomerService(HttpClient httpClient) //ctor
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<(bool success, string message, Customer? customer)> CreateCustomerAsync(CustomerForCreation customerForCreation)
    {
        try
        {

            var response = await _httpClient.PostAsJsonAsync("api/customers", customerForCreation);
            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();

                return (false, errorMessage ?? response.ReasonPhrase ?? "Error creating customer", null);
            }

            var createdCustomer = await response.Content.ReadFromJsonAsync<Customer>(_jsonOptions);
            return (true, "Customer created successfully", createdCustomer);
        }
        catch (Exception ex)
        {

            return (false, $"An error occured: {ex.Message}", null);
        }
    }


    public async Task<(Customer? customer, string errorMessage)> GetCustomerByEmailAsync(string email)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/customers/by-email/{email}");

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                return (null, errorMessage ?? response.ReasonPhrase ?? "Error retrieving customer");

            }

            var customer = await response.Content.ReadFromJsonAsync<Customer>(_jsonOptions);
            return (customer, string.Empty);
        }
        catch (Exception ex)
        {

            return (null, $"An error occured: {ex.Message}");
        }





    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/customers");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Customer>>(_jsonOptions)
                ?? Enumerable.Empty<Customer>().ToList();

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<(bool success, string message)> UpdateCustomerAsync(int customerId, CustomerToUpdate customerToUpdate)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/customers/{customerId}", customerToUpdate);

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                return (false, errorMessage ?? response.ReasonPhrase ?? "Error updating customer");
            }

            return (true, "Customer updated successfully");
        }
        catch (Exception ex)
        {

            return (false, $"An error occurred: {ex.Message}");
        }



    }
}
