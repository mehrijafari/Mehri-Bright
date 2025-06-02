using Mehri_Bright.Entities;

namespace Mehri_Bright.Services
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersForCustomerAsync(int customerid);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(Order order);
        Task<bool> SaveChangesAsync();
    }
}
