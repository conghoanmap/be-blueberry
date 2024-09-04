using System.ComponentModel.DataAnnotations.Schema;

namespace blueberry.Models
{
    // Bảng chi tiết đơn hàng
    [Table("OrderDetails")]
    public class OrderDetail
    {
        public int OrderId { get; set; } // Mã đơn hàng
        public string ProductId { get; set; } // Mã sản phẩm
        public int Quantity { get; set; } // Số lượng
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; } // Giá
        public Order Order { get; set; } // Đơn hàng
        public Product Product { get; set; } // Sản phẩm
    }
}