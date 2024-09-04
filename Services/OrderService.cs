using blueberry.Data;
using blueberry.Interfaces;
using blueberry.Models;
using Microsoft.EntityFrameworkCore;

namespace blueberry.Services
{
    public class OrderService : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateAsync(Order order)
        {
            if(order.PaymentMethod.Equals("Thanh toán khi nhận hàng"))
            {
                order.PaymentStatus = false;
            }
            else
            {
                order.PaymentStatus = true;
            }
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var product = await _context.Products.FindAsync(orderDetail.ProductId);
                    orderDetail.TotalPrice = (product.Price * orderDetail.Quantity) * (100 - product.Discount) / 100;
                    await _context.OrderDetails.AddAsync(orderDetail);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return orderDetail;
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

        public async Task<Order> DeleteAsync(int orderId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var order = await _context.Orders.FindAsync(orderId);
                    if (order == null)
                    {
                        return null;
                    }
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return order;
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

        public async Task<List<Order>> getAllAsync(string? userId)
        {
            if (userId == null)
            {
                return await _context.Orders.ToListAsync();
            }
            return await _context.Orders.Include(od => od.OrderDetails).ThenInclude(p => p.Product).Where(order => order.AppUserId == userId).ToListAsync();
        }

        public async Task<List<OrderDetail>> GetOrderDetailsAsync(int orderId)
        {
            return await _context.OrderDetails.Include(p => p.Product).Where(orderDetail => orderDetail.OrderId == orderId).ToListAsync();
        }


        public async Task<Order> OrderApproval(string orderId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var order = await _context.Orders.FirstOrDefaultAsync(order => order.OrderId.ToString() == orderId);
                    if (order == null)
                    {
                        return null;
                    }
                    order.OrderStatus = "Đơn hàng đã được xác nhận";
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return order;
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

    }
}