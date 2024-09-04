using blueberry.Dtos.Order;
using blueberry.Models;

namespace blueberry.Mappers
{
    public static class OrderMapper
    {
        public static Order RequestToModel(this OrderRequest request)
        {
            return new Order
            {
                PaymentMethod = request.PaymentMethod,
                Note = request.Note,
                Address = request.Address,
                Phone = request.Phone
            };
        }
        public static OrderDisplay ModelToDisplay(this Order order)
        {
            return new OrderDisplay
            {
                OrderId = order.OrderId,
                OrderBy = order.AppUser.UserName,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                Note = order.Note,
                Address = order.Address,
                Phone = order.Phone,
                OrderDetails = order.OrderDetails.Select(orderDetail => orderDetail.ModelToDisplay()).ToList()
            };
        }
        public static OrderDetail RequestToModel(this int quantity)
        {
            return new OrderDetail
            {
                Quantity = quantity
            };
        }
        public static OrderDetailDisplay ModelToDisplay(this OrderDetail orderDetail)
        {
            return new OrderDetailDisplay
            {
                Quantity = orderDetail.Quantity,
                TotalPrice = orderDetail.TotalPrice,
                Product = orderDetail.Product.ModelToDisplay()
            };
        }
    }
}