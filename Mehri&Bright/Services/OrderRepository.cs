using Mehri_Bright.DbContexts;
using Mehri_Bright.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Mehri_Bright.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebLabb2Context _context;

        public OrderRepository(WebLabb2Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderProducts)
                .ThenInclude(o => o.Product)
                .Include(o => o.Customer)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersForCustomerAsync(int customerid)
        {
            var orderForCustomer = await _context.Orders.Include(o => o.OrderProducts)
                .ThenInclude(o => o.Product)
                .Where(o => o.CustomerId == customerid)
                .ToListAsync();

            return orderForCustomer;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
