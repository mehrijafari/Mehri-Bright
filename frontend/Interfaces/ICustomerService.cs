using frontend.Models;

namespace frontend.Interfaces
{
    public interface ICustomerService
    {

        Task<List<Customer>> GetCustomersAsync();
        Task<(Customer? customer, string errorMessage)> GetCustomerByEmailAsync(string email);
        Task<(bool success, string message, Customer? customer)> CreateCustomerAsync(CustomerForCreation customerForCreation);
        Task<(bool success, string message)> UpdateCustomerAsync(int customerId, CustomerToUpdate customerToUpdate);
  
    }
}
