using Mehri_Bright.Entities;

namespace Mehri_Bright.Services
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerAsync(int customerId);
        Task AddCustomer(Customer customer);
        Task<bool> SaveChangesAsync();
        Task<Customer?> GetCustomerByEmailAsync(string email);
        Task UpdateCustomerAsync(Customer customer);
        bool CustomerEmailExists(string email);
        bool CustomerExists(int id);
    }
}
