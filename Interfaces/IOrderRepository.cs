using blueberry.Models;

namespace blueberry.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> getAllAsync(string? userId);
        Task<Order> CreateAsync(Order order);
        Task<Order> OrderApproval(string orderId);
        Task<Order> DeleteAsync(int orderId);
        Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId);
        Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail);
    }
}