using System.ComponentModel.DataAnnotations.Schema;

namespace blueberry.Models
{
    // Bảng đơn hàng
    [Table("Orders")]
    public class Order
    {
        public int OrderId { get; set; } // Mã đơn hàng
        public string AppUserId { get; set; } // Mã người dùng
        public DateTime OrderDate { get; set; } = DateTime.Now; // Ngày đặt hàng
        public string OrderStatus { get; set; } = "Chờ xác nhận"; // Trạng thái đơn hàng
        public string PaymentMethod { get; set; } = string.Empty; // Phương thức thanh toán
        public bool PaymentStatus { get; set; } // Trạng thái thanh toán
        public string Address { get; set; } = string.Empty; // Địa chỉ
        public string Phone { get; set; } = string.Empty; // Số điện thoại
        public string Note { get; set; } = string.Empty; // Ghi chú
        public AppUser AppUser { get; set; } // Người dùng
        public List<OrderDetail> OrderDetails { get; set; } // Chi tiết đơn hàng
    }
}