using System.ComponentModel.DataAnnotations.Schema;

namespace blueberry.Models
{
    // Bảng giỏ hàng
    [Table("Carts")]
    public class Cart
    {
        public string CartId { get; set; } // Mã giỏ hàng
        public string AppUserId { get; set; } // Mã người dùng
        public string ProductId { get; set; } // Mã sản phẩm
        public required int Quantity { get; set; } = 1; // Số lượng
        public decimal TotalPrice { get; set; } // Tổng tiền
        public AppUser AppUser { get; set; } // Người dùng
        public Product Product { get; set; } // Sản phẩm
    }
}