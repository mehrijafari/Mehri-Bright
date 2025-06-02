using Mehri_Bright.DbContexts;
using Mehri_Bright.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Mehri_Bright.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly WebLabb2Context _context;

        public CustomerRepository(WebLabb2Context context) //ctor
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public bool CustomerEmailExists(string email)
        {
            var customerEmailExists = _context.Customers.Where(c => c.Email == email).FirstOrDefault();
            if (customerEmailExists == null)
            {
                return false;
            }

            return true;
        }

        public bool CustomerExists(int id)
        {
            var exists = _context.Customers.Any(c => c.Id == id);
            return exists;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerAsync(int customerId)
        {
            return await _context.Customers.Where(c => c.Id == customerId).FirstOrDefaultAsync();
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers.Where(c => c.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer); 
        }

    }
}
