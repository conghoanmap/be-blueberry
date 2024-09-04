namespace blueberry.Dtos.Order
{
    public class OrderDisplay
    {
        public int OrderId { get; set; } // Mã đơn hàng
        public string OrderBy { get; set; } // Người đặt hàng
        public DateTime OrderDate { get; set; } // Ngày đặt hàng
        public string OrderStatus { get; set; } // Trạng thái đơn hàng
        public string PaymentMethod { get; set; } = string.Empty; // Phương thức thanh toán
        public bool PaymentStatus { get; set; } // Trạng thái thanh toán
        public string Note { get; set; } = string.Empty; // Ghi chú
        public string Address { get; set; } = string.Empty; // Địa chỉ
        public string Phone { get; set; } = string.Empty; // Số điện thoại
        public List<OrderDetailDisplay> OrderDetails { get; set; } // Chi tiết đơn hàng
    }
}