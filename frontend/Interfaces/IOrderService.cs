using frontend.Models;

namespace frontend.Interfaces;

public interface IOrderService
{
    Task<(Order? order, string? message, bool success)> CreateOrder(CreateOrder createOrder);
    Task<List<Order>> GetAllOrdersAsync();
    Task<(Order? order, string? message)> SearchOrderByIdAsync(int orderId);
}
